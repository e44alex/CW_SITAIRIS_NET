using System;

namespace CW_SITAIRIS.Models
{
    public class OrderInfo
    {
        public int orderId { get; set; }
        public int carId { get; set; }
        public String carName { get; set; }
        public int clientId { get; set; }
        public String clientName { get; set; }
        public DateTime dateOpened { get; set; }
        public DateTime dateClosed { get; set; }


        public OrderInfo()
        {
        }

        public OrderInfo(Order order)
        {
            this.clientId = order.clientId;
            this.orderId = order.orderId;
            this.carId = order.carId;
            this.dateClosed = order.dateClosed;
            this.dateOpened = order.dateOpened;
        }
    }
}