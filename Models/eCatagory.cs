using System.ComponentModel.DataAnnotations;

namespace Final_Project
{
    public enum eCatagory
    {
        Action = 1,
        [Display(Name = "Science Fiction")]
        ScienceFiction = 2,
        Fantasy = 3,
        [Display(Name = "Historical Fiction")]
        HistoricalFiction = 4
    }
}