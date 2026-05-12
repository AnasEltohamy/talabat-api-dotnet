using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entites.Order_Aggregate;

namespace talabat.Repository.Data.Store.Specifications.SpecOfOrders
{
    public class OrderSpecification: BaseSpecifications<Order>
    {

        //Get All Orders For Spacific user
        public OrderSpecification(string buyerEmail) : base(O=>O.BuyerEmail == buyerEmail)
        {
            Includes.Add(O=>O.DeliveryMethod);
            Includes.Add(O => O.Items);

            orderByDec(O=>O.OrderDate);
        }



        //Get Spacific Order For Spacific user
        public OrderSpecification(int Id , string buyerEmail):base(O=>O.Id == Id && O.BuyerEmail== buyerEmail)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }




    }
}
