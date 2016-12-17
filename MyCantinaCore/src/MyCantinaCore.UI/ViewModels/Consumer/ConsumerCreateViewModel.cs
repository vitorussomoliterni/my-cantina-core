using System;
using System.ComponentModel.DataAnnotations;

namespace MyCantinaCore.UI.ViewModels.Consumer
{
    public class ConsumerCreateViewModel
    {
        public ConsumerCreateViewModel()
        {
            DateOfBirth = new int[3];
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int[] DateOfBirth { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}
