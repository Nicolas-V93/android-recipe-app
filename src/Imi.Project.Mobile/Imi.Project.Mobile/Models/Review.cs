using System;

namespace Imi.Project.Mobile.Models
{
    public class Review
    {
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime CreationDate { get; set; }
        public User User { get; set; }
    }
}
