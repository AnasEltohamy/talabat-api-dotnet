using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace talabat.Core.Entites.Order_Aggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value ="Pending")]
        Pending,
        [EnumMember(Value = "Payment Received")]
        PaymentReceived,
        [EnumMember(Value = "Payment Fai1ed")]
        PaymentFai1ed,
    }
}
