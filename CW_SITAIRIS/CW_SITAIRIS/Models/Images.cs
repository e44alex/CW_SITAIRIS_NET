using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace CW_SITAIRIS.Models
{
    public class Images
    {
        [Key]
        public int idCar { get; set; }
        public byte[] Image { get; set; }
    }
}