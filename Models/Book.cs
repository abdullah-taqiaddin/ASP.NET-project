using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Please Enter the Book's Name")]
        [Display(Name = "Title")]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Please Enter a price")]
        [Display(Name = "Price")]
        [Range(1, 500.99, ErrorMessage = "A books Price cant exceed 500.99$ and Cant be Below 1$!")]
        public double Price { get; set; }

        [Display(Name = "Catagory")]
        public eCatagory Catagory { get; set; } = eCatagory.Action;
        public string Cover { get; set; } = "/images/default.png";

        [DataType(DataType.Date)]
        public DateTime ReleseDate { get; set; }
        [StringLength(30)]
        [MinLength(3)]
        public string Publsiher { get; set; }

        ///////////////
        public IEnumerable<Authorship> Authorships { get; set; }


    }
}
