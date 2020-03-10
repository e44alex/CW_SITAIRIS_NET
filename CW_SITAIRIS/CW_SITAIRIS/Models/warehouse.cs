using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CW_SITAIRIS.Models
{
    public class Warehouse
    {
        [Key]
        public int idWarehouse { get; set; }
        public string address { get; set; }
        public int amountOfCar { get; set; }
    }
}
