using System.ComponentModel.DataAnnotations;

namespace SACLA_App.Models
{
    public class TopicModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Maximum of 100 characters allowed")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public IEnumerable<PaperModel> Papers { get; set; }
    }
}
