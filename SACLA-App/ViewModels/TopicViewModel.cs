using System.ComponentModel.DataAnnotations;

namespace SACLA_App.ViewModels
{
    public class TopicViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Maximum of 100 characters allowed")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
    }
}
