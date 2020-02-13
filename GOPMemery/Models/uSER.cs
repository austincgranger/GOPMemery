using System;

namespace GOPMemery.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string UserUsername { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserAllergies { get; set; }
    }
}