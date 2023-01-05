using System.ComponentModel.DataAnnotations;

namespace Final_Project
{
    public enum eRole
    {
        Author =1 ,
        [Display(Name = "CO-Author")]
        CoAuthor = 2
    }
}