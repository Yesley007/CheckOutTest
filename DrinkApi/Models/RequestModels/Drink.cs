using System.ComponentModel.DataAnnotations;

namespace DrinkApi.Models
{
    public class Drink
    {
        [Required]
        public string DrinkName { get; set; }
        [Required]
        [Range(-50,100)]
        public int DrinkCount { get; set; }
    }

}