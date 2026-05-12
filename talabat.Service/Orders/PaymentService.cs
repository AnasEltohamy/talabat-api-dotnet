using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entites.Order_Aggregate;
using talabat.Core.Entites.Products;
using talabat.Core.Repositores.Contract;
using talabat.Core.Services.Contract;
using talabat.Repository.Data.Store.Specifications.SpecOfOrders;
using talabat.Repository.Data.Store.Specifications.SpecOfProducts;
using Product = talabat.Core.Entites.Products.Product;

namespace talabat.Service.Orders
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService
            (
                IConfiguration configuration ,
                IBasketRepository basketRepository ,
                IUnitOfWork unitOfWork
                
            )
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            
        }



        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"]; 


            var basket = await _basketRepository.GetBasketAsync(basketId);

            if (basket is null)
            {
                return null;
            }
            else
            {
                var shippingPrice = 0m;

                // 01 Chick DeliveryMethod
                if (basket.DeliveryMethodId.HasValue)
                {
                    var deliveryMethod = await _unitOfWork.
                        CreateRepo<DeliveryMethod>().
                        GetByIDAsync(basket.DeliveryMethodId.Value);

                    basket.ShippingPrice = deliveryMethod.Cost;
                    shippingPrice = deliveryMethod.Cost;
                }

                // 02 Chick The Items
                if (basket?.Items.Count > 0)
                {
                    foreach (var item in basket.Items)
                    {
                        var Spec = new ProductWithCategoryAndBrandWithSpecification(item.Id);
                        var Product = await _unitOfWork.CreateRepo<Product>().GetByIdWithSpecAsync(Spec);
                        if (item.Price != Product.Price 
                            || item.productName != Product.Name 
                            || item.PictureUrl != Product.PictureUrl 
                            || item.Brand != Product.Brand.Name
                            || item.type != Product.Category.Name 
                            )
                        {
                            item.Price = Product.Price;
                            item.productName = Product.Name;
                            item.PictureUrl = Product.PictureUrl;
                            item.Brand = Product.Brand?.Name;
                            item.type = Product.Category?.Name;
                        }

                    }
                }


                // 03 Chick The PaymentIntent
                //info 
                // (class) PaymentIntent
                // (class) PaymentIntentService  => (Method ) CreateAsync(typeOf PaymentIntentCreateOptions )

                var service = new PaymentIntentService();
                PaymentIntent intent;

                if (string.IsNullOrEmpty(basket.PaymentIntentId))  // Create
                {
                    

                    var Opt = new PaymentIntentCreateOptions()
                    {
                        //Amount = (long) basket.Items.Sum(Item => Item.Price *100 * Item.Quantity * 100) + (long) shippingPrice * 100,
                        Amount = (long)(basket.Items.Sum(Item => Item.Price * Item.Quantity) + shippingPrice) * 100,

                        Currency = "USD",
                        PaymentMethodTypes = new List<string>() { "card" }
                    };

                    intent = await service.CreateAsync(Opt);

                    basket.PaymentIntentId = intent.Id;
                    basket.ClientSecret = intent.ClientSecret;



                }
                else  // Update 
                {
                    var Opt = new PaymentIntentUpdateOptions()
                    {
                 
                        //Amount = (long) basket.Items.Sum(Item => Item.Price * 100 * Item.Quantity * 100) + (long)shippingPrice * 100,
                        Amount = (long)(basket.Items.Sum(Item => Item.Price * Item.Quantity) + shippingPrice) * 100,
                    };
                  
                    await service.UpdateAsync( basket.PaymentIntentId ,Opt);
                 
                }
                

                await _basketRepository.UpdateBasketAsync(basket);
                return basket;


            }



        }

        

        public async Task<Order> UpdatePaymentIntentSucceededOrFailed(string paymentIntentId, bool IsSuccess)
        {
            var spec = new OrderWithPaymentIntentSpecification(paymentIntentId);
            var order = await _unitOfWork.CreateRepo<Order>().GetByIdWithSpecAsync(spec);

            if (IsSuccess)
                order.Status = OrderStatus.PaymentReceived;
            else
                order.Status = OrderStatus.PaymentFai1ed;

            _unitOfWork.CreateRepo<Order>().Update(order);

            await _unitOfWork.CompleteAsync();

            return order;

        }
    }
}
