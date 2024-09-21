using MiaTicket.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Stragegy
{
    public interface IOrderPriceStragegy
    {
        double CalculateTotalPrice(List<OrderTicket> items, double discountValue = 0);
    }


    public class DefaultPricingStrategy : IOrderPriceStragegy
    {
        public double CalculateTotalPrice(List<OrderTicket> items, double discountValue = 0)
        {
            return items.Sum(item => item.Price * item.Quantity);
        }
    }

    public class PercentageVoucherPricingStrategy : IOrderPriceStragegy
    {
        public double CalculateTotalPrice(List<OrderTicket> items, double discountValue = 0)
        {
            var subTotal = items.Sum(item => item.Price * item.Quantity);
            var percent = discountValue / 100;
            var total = subTotal * percent;
            return total;
        }
    }

    public class FixedAmountVoucherPricingStrategy : IOrderPriceStragegy
    {
        public double CalculateTotalPrice(List<OrderTicket> items, double discountValue = 0)
        {
            var subTotal = items.Sum(item => item.Price * item.Quantity);
            var total = subTotal - discountValue;
            return total > 0 ? total : 0 ;
        }
    }
}
