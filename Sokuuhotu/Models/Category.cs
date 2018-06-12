
using System.ComponentModel.DataAnnotations;

namespace BAW.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Image { get; set; }




    }
}