using System;

namespace LoginWebApp.Models
{
    public class Client
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool Restore { get; set; }
        public bool Confirmed { get; set; }
        public string Token { get; set; }
        public bool TermsAccepted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserTypeId { get; set; }
        public UserType UserType { get; set; }
    }
}
