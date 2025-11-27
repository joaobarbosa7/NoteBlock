using System;
using System.Collections.Generic;
using System.Text;
using NoteBlock.Domain.SeedWork;

namespace NoteBlock.Domain.Models
{
    public abstract class User : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
