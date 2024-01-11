using System.Collections.Generic;

namespace LoginWebApp.Models
{
    public class UserType
    {
        public int UserTypeId { get; set; }
        public string TypeName { get; set; }
        public int IdentityIdentity { get; set; }
        public int IdentitySeed { get; set; }
        public int IdentityIncrement { get; set; }
        public List<Client> Clients { get; set; }
        public List<Expert> Experts { get; set; }
    }
}
