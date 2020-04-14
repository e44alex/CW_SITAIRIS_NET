using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CW_SITAIRIS.Models
{
    public class Car
    {
        [Key]
        public int idCar { get; set; }
        public string country { get; set; }
        public string color { get; set; }
        public string mark { get; set; }
        public string model { get; set; }
        public string engine { get; set; }
        public float price { get; set; }
        public string built { get; set; } // complectation of car
        //public int year { get; set; }
        public int warehouseId { get; set; }
        public string picture { get; set; }


        public override string ToString()
        {
            return mark + " " + model + " " + engine + " " + built;
        }
    }
}
