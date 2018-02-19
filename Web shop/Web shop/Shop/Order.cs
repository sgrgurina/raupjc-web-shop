using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Shop
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public List<ItemOrderInfo> OrderInformation { get; set; }

        public string BuyerName { get; set; }
        public string BuyerSurname { get; set; }
        public string ContactEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderPlacedTime { get; set; }

        public Order(string buyerName, string buyerSurname, string contactEmail, string phoneNumber, string adress)
        {
            OrderId = Guid.NewGuid();
            OrderInformation = new List<ItemOrderInfo>();
            BuyerName = buyerName;
            BuyerSurname = buyerSurname;
            ContactEmail = contactEmail;
            PhoneNumber = phoneNumber;
            Adress = adress;
            OrderPlacedTime = DateTime.Now;
            
        }

        public Order()
        {
        }

        public void AddItemOrderInfo(ItemOrderInfo orderInfo)
        {
            OrderInformation.Add(orderInfo);
        }

        public void CalculateTotalPrice()
        {
            TotalPrice = OrderInformation.Select(o => o.Price).Sum();
        }
    }
}
