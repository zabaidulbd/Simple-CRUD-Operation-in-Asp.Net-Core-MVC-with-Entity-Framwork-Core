using ListCRUD.Models.PersonalInfoViewModel;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace ListCRUD.Services
{
    public interface ICommon
    {
        string UploadedFile(IFormFile ProfilePicture);
        void InitAppData();
        IQueryable<PersonalInfoGridiewModel> GetPersonalInfoList();
    }
}
