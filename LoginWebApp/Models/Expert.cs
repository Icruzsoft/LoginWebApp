using System;

namespace LoginWebApp.Models
{
    public class Expert
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string EducationLevel { get; set; }
        public string LearningMethod { get; set; }
        public string CVPath { get; set; }
        public byte[] CVContent { get; set; } // Para almacenar el contenido del CV en formato binario
        public string CVExtension { get; set; } // Para almacenar la extensión del archivo CV
        public int? UserTypeId { get; set; } 
        public UserType UserType { get; set; }
    }
}
