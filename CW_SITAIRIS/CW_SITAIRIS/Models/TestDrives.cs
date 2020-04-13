using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.Features;

namespace CW_SITAIRIS.Models
{
    public class TestDrives
    {
        [Key] public int idDrive { get; set; }
        public int userId { get; set; }
        public int carId { get; set; }
        public DateTime date { get; set; }
    }
    
}