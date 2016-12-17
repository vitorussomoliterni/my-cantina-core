﻿using System;

namespace MyCantinaCore.UI.ViewModels.Consumer
{
    public class ConsumerDetailsViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
    }
}
