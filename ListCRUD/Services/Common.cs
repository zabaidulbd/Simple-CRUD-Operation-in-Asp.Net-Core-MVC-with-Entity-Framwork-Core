using ListCRUD.Data;
using ListCRUD.Models.PersonalInfoViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;

namespace ListCRUD.Services
{
    public class Common : ICommon
    {
        private readonly IWebHostEnvironment _iHostingEnvironment;
        private readonly ApplicationDbContext _context;
        public Common(IWebHostEnvironment iHostingEnvironment, ApplicationDbContext context)
        {
            _iHostingEnvironment = iHostingEnvironment;
            _context = context;
        }
        public string UploadedFile(IFormFile ProfilePicture)
        {
            string ProfilePictureFileName = null;
            if (ProfilePicture != null)
            {
                string uploadsFolder = Path.Combine(_iHostingEnvironment.ContentRootPath, "wwwroot\\upload");
                ProfilePictureFileName = Guid.NewGuid().ToString() + "_" + ProfilePicture.FileName;
                string filePath = Path.Combine(uploadsFolder, ProfilePictureFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ProfilePicture.CopyTo(fileStream);
                }
            }
            return ProfilePictureFileName;
        }



        public IQueryable<PersonalInfoGridiewModel> GetPersonalInfoList()
        {
            try
            {
                return (from _PersonalInfo in _context.PersonalInfo
                        where _PersonalInfo.Cancelled == false
                        select new PersonalInfoGridiewModel
                        {
                            Id = _PersonalInfo.Id,
                            FirstName = _PersonalInfo.FirstName,
                            LastName = _PersonalInfo.LastName,
                            DateOfBirth = _PersonalInfo.DateOfBirth.ToString(),
                            City = _PersonalInfo.City,
                            Country = _PersonalInfo.LastName,
                            MobileNo = _PersonalInfo.MobileNo,
                            Email = _PersonalInfo.Email,
                            CreatedDate = _PersonalInfo.CreatedDate.ToString(),
                        }).OrderByDescending(x => x.CreatedDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InitAppData()
        {
            CommonData _CommonData = new CommonData();
            _context.PersonalInfo.AddRange(_CommonData.GetPersonalInfoList());
            _context.SaveChanges();
        }
    }
}
