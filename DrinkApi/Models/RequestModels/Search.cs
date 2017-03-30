using System.ComponentModel.DataAnnotations;

namespace DrinkApi.Models
{
    public class Search
    {
        [Required]
        [Range(1,500)]
        public int PageSize { get; set; }
        [Required]
        [Range(1,999)]
        public int PageNumber { get; set; }
        public string OrderBy { get; set; }
        public string SortDirection { get; set; }
    }
}