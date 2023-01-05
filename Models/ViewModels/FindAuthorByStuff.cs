using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Models.ViewModels
{
    public class FindAuthorByStuff
    {
        [Required]
        public string Name { get; set; }
        public eRole Role { get; set; } = eRole.Author;

        //result
        public IEnumerable<Authorship> Authorships { get; set; }
    }
}
