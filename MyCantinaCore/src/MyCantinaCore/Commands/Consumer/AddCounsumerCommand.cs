﻿using System;

namespace MyCantinaCore.Commands.Consumer
{
    public class AddCounsumerCommand
    {
        public string FristName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
    }
}
