﻿using System;

namespace MyCantinaCore.Commands.Consumer
{
    public class UpdateConsumerCommand
    {
        public int Id { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
    }
}