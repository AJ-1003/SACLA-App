using SACLA_App.Areas.Identity.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SACLA_App.Models
{
    public class PaperModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Maximum of 100 characters allowed")]
        [DataType(DataType.Text)]
        public string Title { get; set; }
        [Required]
        [StringLength(1500, ErrorMessage = "Maximum of 1500 characters allowed")]
        [DataType(DataType.MultilineText)]
        public string Abstract { get; set; }
        public DateTime DateSubmitted { get; set; }
        public ApplicationUser Author { get; set; }
        public int TopicId { get; set; }

        public string AuthorId { get; set; }
        public TopicModel Topic { get; set; }
    }
}