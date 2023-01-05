using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Must Be less than 50 characters")]
        [Column(TypeName = "varChar(50)")]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime DOB { get; set; } = DateTime.Now;


        public int Phone { get; set; }

        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }


        ///////////////
        public IEnumerable<Authorship> Authorships { get; set; }
    }
}
