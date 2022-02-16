
using System.ComponentModel.DataAnnotations;

namespace Zadanie4.Models
{
    public class Animal
    {   
        [Required(ErrorMessage = "You have to enter data")]
        [MaxLength(200, ErrorMessage = "Data to long")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You have to enter data")]
        [MaxLength(200, ErrorMessage = "Data to long")]
        public string Description { get; set; }
        [Required(ErrorMessage = "You have to enter data")]
        [MaxLength(200, ErrorMessage = "Data to long")]
        public string Category { get; set; }
        [Required(ErrorMessage = "You have to enter data ")]
        [MaxLength(200, ErrorMessage = "Data to long")]
        public string Area { get; set; }
    }
}
