using System;
using System.ComponentModel.DataAnnotations;

namespace CW_SITAIRIS.Models
{
    public class Order
    {
        [Key]
        public int orderId { get; set; }
        public int carId { get; set; }
        public int clientId { get; set; }
        public int managerId { get; set; }
        public DateTime dateOpened { get; set; }
        public DateTime dateClosed { get; set; }
    }
}