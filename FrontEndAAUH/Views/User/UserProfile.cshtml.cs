using FrontEndAAUH.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEndAAUH.Views.User
{
    public class IndexModel : PageModel
    {
        public Questionnaire questionnaire { get; set; }
        public Answer answer { get; set; }
        public void OnGet()
        {
        }
    }
}
