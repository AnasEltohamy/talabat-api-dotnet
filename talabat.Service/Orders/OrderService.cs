using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entites.Order_Aggregate;
using talabat.Core.Entites.Products;
using talabat.Core.Repositores.Contract;
using talabat.Core.Services.Contract;
using talabat.Repository.Data.Store.Specifications.SpecOfOrders;
using addressAggregate = talabat.Core.Entites.Order_Aggregate.Address;
using Product = talabat.Core.Entites.Products.Product;

namespace talabat.Service.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="basketRepository">Repository responsible for managing shopping baskets.</param>
        /// <param name="unitOfWork">Unit of Work used to coordinate database transactions.</param>
        /// <param name="paymentService">Service responsible for Stripe payment operations.</param>
        public OrderService(IBasketRepository basketRepository,IUnitOfWork unitOfWork , IPaymentService paymentService)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }





        /// <summary>
        /// Creates a new order for a specific user based on the provided basket.
        /// The method validates product prices from the database, calculates the subtotal,
        /// attaches the selected delivery method, and links the order to a Stripe PaymentIntent.
        /// </summary>
        /// <param name="buyerEmail">The email address of the buyer placing the order.</param>
        /// <param name="basketId">The identifier of the shopping basket.</param>
        /// <param name="deleveryMethodId">The selected delivery method identifier.</param>
        /// <param name="address">The shipping address of the buyer.</param>
        /// <param name="paymentIntentId">The Stripe PaymentIntent identifier associated with the order.</param>
        /// <returns>
        /// Returns the created <see cref="Order"/> if the operation succeeds;
        /// otherwise, returns <c>null</c>.
        /// </returns>
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deleveryMethodId, addressAggregate address , string paymentIntentId)
        {
            // 1. Retrieve the basket using the provided basket identifier
            var Basket = await _basketRepository.GetBasketAsync(basketId);



            // 2. Convert basket items into order items using real product data from the database
            var OrderItems = new List<OrderItem>();

            if (Basket?.Items?.Count()>0)
            {
                foreach (var item in Basket.Items)
                {
                    var Product = await _unitOfWork.CreateRepo<Product>().GetByIDAsync(item.Id);

                    var ProductItemOrder = new ProductItemOrder(item.Id ,Product.Name , Product.PictureUrl );

                    var OrderItem = new OrderItem(ProductItemOrder, Product.Price, item.Quantity);
                    OrderItems.Add(OrderItem);
                }
            }

            // 3. Calculate the subtotal based on product price and quantity
            var SupTotal = OrderItems.Sum(OI => OI.Price * OI.Quantity);


            // 4. Retrieve the selected delivery method from the database
            var DeliveryMethod = await _unitOfWork.CreateRepo<DeliveryMethod>().GetByIDAsync(deleveryMethodId);


            // 5. Check if an order already exists for the same PaymentIntent

            var orderSpec = new OrderWithPaymentIntentSpecification(Basket.PaymentIntentId);
            var paymentIntentExisting = await _unitOfWork.CreateRepo<Order>().GetEntityWithSpecAsync(orderSpec);
            if (paymentIntentExisting != null)
            {
                _unitOfWork.CreateRepo<Order>().Delete(paymentIntentExisting);
                await _paymentService.CreateOrUpdatePaymentIntentAsync(Basket.Id);
            };

            // 6. Create a new order instance

            var Order = new Order(buyerEmail,address, DeliveryMethod, OrderItems, SupTotal , Basket.PaymentIntentId);
           


            // 7. Persist the order in the database
             await _unitOfWork.CreateRepo<Order>().AddAsync(Order);

            var Count = await _unitOfWork.CompleteAsync();
            if (Count <= 0) return null;
            return Order;
        }











        /// <summary>
        /// Retrieves a specific order for a given user by order identifier.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order.</param>
        /// <param name="buyerEmail">The email address of the order owner.</param>
        /// <returns>
        /// Returns the matching <see cref="Order"/> if found; otherwise, returns <c>null</c>.
        /// </returns>
        public Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var productRepo = _unitOfWork.CreateRepo<Order>();

            var Spec = new OrderSpecification(orderId , buyerEmail);

            return productRepo.GetByIdWithSpecAsync(Spec);

          
        }






        /// <summary>
        /// Retrieves all orders associated with a specific user.
        /// </summary>
        /// <param name="buyerEmail">The email address of the user.</param>
        /// <returns>
        /// Returns a read-only list of <see cref="Order"/> objects.
        /// </returns>
        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var productRepo = _unitOfWork.CreateRepo<Order>();
            var Spec = new OrderSpecification(buyerEmail);
            return productRepo.GetAllWithSpecAsync(Spec);
        }





        /// <summary>
        /// Retrieves all available delivery methods.
        /// </summary>
        /// <returns>
        /// Returns a read-only list of <see cref="DeliveryMethod"/> objects.
        /// </returns>
        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.CreateRepo<DeliveryMethod>().GetAllAsync();    
        
        }

        

    }
}
