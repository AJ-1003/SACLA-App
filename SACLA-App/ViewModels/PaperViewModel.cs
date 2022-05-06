using Microsoft.AspNetCore.Mvc.Rendering;
using SACLA_App.Areas.Identity.Data;
using SACLA_App.Models;
using System.ComponentModel.DataAnnotations;

namespace SACLA_App.ViewModels
{
    public class PaperViewModel
    {
        //public int Id { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "Maximum of 100 characters allowed")]
        //[DataType(DataType.Text)]
        //public string Title { get; set; }
        //[Required]
        //[StringLength(1500, ErrorMessage = "Maximum of 1500 characters allowed")]
        //[DataType(DataType.MultilineText)]
        //public string Abstract { get; set; }

        //public ApplicationUser Author { get; set; }
        //public IEnumerable<TopicModel> Topics { get; set; }

        public PaperModel? Paper { get; set; }
        public TopicModel? Topic { get; set; }
    }
}
