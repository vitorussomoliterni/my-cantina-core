using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCantinaCore.DataAccess.Models
{
    public class Consumer
    {
        public Consumer()
        {
            ConsumerBottles = new List<ConsumerBottle>();
            Reviews = new List<Review>();
        }

        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Email { get; set; }
        public List<ConsumerBottle> ConsumerBottles { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
