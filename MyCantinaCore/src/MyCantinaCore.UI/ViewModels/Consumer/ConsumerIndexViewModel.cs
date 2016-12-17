using System.ComponentModel.DataAnnotations;

namespace MyCantinaCore.UI.ViewModels.Consumer
{
    public class ConsumerIndexViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
