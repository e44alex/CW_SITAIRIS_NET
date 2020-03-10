using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CW_SITAIRIS.Models
{
    public class User
    {
        [Key]
        public int idUser { get; set; }
        public UserRole role { get; set; }
        public string name { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public DateTime birthdate { get; set; }
        public string proneNumber { get; set; }
        public string email { get; set; }
    }
}
