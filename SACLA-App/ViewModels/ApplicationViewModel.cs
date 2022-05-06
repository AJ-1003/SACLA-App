using SACLA_App.Models;

namespace SACLA_App.ViewModels
{
    public class ApplicationViewModel
    {
        public IEnumerable<PaperModel> Papers { get; set; }
        public IEnumerable<TopicModel> Topics { get; set; }
    }
}
