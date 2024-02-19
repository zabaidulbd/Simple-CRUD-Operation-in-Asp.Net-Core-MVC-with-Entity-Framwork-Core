using System;

namespace ListCRUD.Models
{
    public class PersonalInfo : EntityBase
    {
        public Int64 Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string PasportNo { get; set; }
        public string NID { get; set; }
    }
}
