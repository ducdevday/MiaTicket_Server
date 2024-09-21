using MiaTicket.BussinessLogic.Stragegy;
using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Factory
{
    public class OrderPricingFactory
    {
        public static IOrderPriceStragegy GetPriceStragegy(VoucherType? type) {
            switch (type) {
                case VoucherType.Percentage:
                    return new PercentageVoucherPricingStrategy();
                case VoucherType.FixedAmount:
                    return new FixedAmountVoucherPricingStrategy();
                default:
                    return new DefaultPricingStrategy();
            }
        }
    }
}
