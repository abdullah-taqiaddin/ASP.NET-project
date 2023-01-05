using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Models
{
    public class Authorship
    {
        [Key]
        public int ID { get; set; }

        public int BookId { get; set; }
        public int AuthorId { get; set; }

        public eRole Role { get; set; } = eRole.Author;

        ///////////////
        public Book Book { get; set; }
        public Author Author { get; set; }
    }
}
