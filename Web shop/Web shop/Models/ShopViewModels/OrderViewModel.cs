using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Models.ShopViewModels
{
    public class OrderViewModel
    {
        public Guid OrderId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerSurname { get; set; }
        public string ContactEmail { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderPlacedTime { get; set; }

        public OrderViewModel(Guid orderId, string buyerName, string buyerSurname, string contactEmail, decimal totalPrice, DateTime orderPlacedTime)
        {
            OrderId = orderId;
            BuyerName = buyerName;
            BuyerSurname = buyerSurname;
            ContactEmail = contactEmail;
            TotalPrice = totalPrice;
            OrderPlacedTime = orderPlacedTime;
        }
    }
}
