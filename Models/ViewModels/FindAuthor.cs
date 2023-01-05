using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Models.ViewModels
{
    public class FindAuthor
    {
        [Required]
        public string Name { get; set; }

        //result
        public IEnumerable<Author> Authors { get; set; }
    }
}
