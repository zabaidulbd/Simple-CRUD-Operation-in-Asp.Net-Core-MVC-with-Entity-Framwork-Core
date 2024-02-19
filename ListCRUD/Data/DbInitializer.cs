using System.Linq;
using System.Threading.Tasks;
using ListCRUD.Services;

namespace ListCRUD.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, ICommon _iCommon)
        {
            context.Database.EnsureCreated();
            if (context.PersonalInfo.Any())
            {
                return;
            }
            else
            {
                _iCommon.InitAppData();
            }
        }
    }
}
