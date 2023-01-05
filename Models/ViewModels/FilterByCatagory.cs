using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Models.ViewModels
{
    public class FilterByCatagory
    {
        public eCatagory Catagory { get; set; } = eCatagory.Action;

        //result
        public IEnumerable<Book> Books { get; set; }
    }
}
