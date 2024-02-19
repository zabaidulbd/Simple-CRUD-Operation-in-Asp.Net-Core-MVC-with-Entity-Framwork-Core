using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ListCRUD.Models.PersonalInfoViewModel
{
    public class PersonalInfoCRUDViewModel : EntityBase
    {
        public Int64 Id { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        [DisplayName("Mobile No")]
        public string MobileNo { get; set; }
        [Required]
        public string Email { get; set; }
        [DisplayName("Pasport No")]
        public string PasportNo { get; set; }
        [DisplayName("National ID No")]
        public string NID { get; set; }


        public static implicit operator PersonalInfoCRUDViewModel(PersonalInfo _PersonalInfo)
        {
            return new PersonalInfoCRUDViewModel
            {
                Id = _PersonalInfo.Id,
                FirstName = _PersonalInfo.FirstName,
                LastName = _PersonalInfo.LastName,
                DateOfBirth = _PersonalInfo.DateOfBirth,
                City = _PersonalInfo.City,
                Country = _PersonalInfo.LastName,
                MobileNo = _PersonalInfo.MobileNo,
                Email = _PersonalInfo.Email,
                PasportNo = _PersonalInfo.PasportNo,
                NID = _PersonalInfo.NID,

                CreatedDate = _PersonalInfo.CreatedDate,
                ModifiedDate = _PersonalInfo.ModifiedDate,
                CreatedBy = _PersonalInfo.CreatedBy,
                ModifiedBy = _PersonalInfo.ModifiedBy,
                Cancelled = _PersonalInfo.Cancelled
            };
        }

        public static implicit operator PersonalInfo(PersonalInfoCRUDViewModel vm)
        {
            return new PersonalInfo
            {
                Id = vm.Id,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                DateOfBirth = vm.DateOfBirth,
                City = vm.City,
                Country = vm.LastName,
                MobileNo = vm.MobileNo,
                Email = vm.Email,
                PasportNo = vm.PasportNo,
                NID = vm.NID,

                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled
            };
        }
    }
}
