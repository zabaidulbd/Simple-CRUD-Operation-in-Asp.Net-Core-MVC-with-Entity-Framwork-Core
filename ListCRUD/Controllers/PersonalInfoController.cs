using ListCRUD.Data;
using ListCRUD.Models;
using ListCRUD.Models.PersonalInfoViewModel;
using ListCRUD.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace ListCRUD.Controllers
{
    public class PersonalInfoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;

        public PersonalInfoController(ApplicationDbContext context, ICommon iCommon)
        {
            _context = context;
            _iCommon = iCommon;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetDataTabelData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();

                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnAscDesc = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int resultTotal = 0;

                var _GetGridItem = _iCommon.GetPersonalInfoList();
                //Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnAscDesc)))
                {
                    _GetGridItem = _GetGridItem.OrderBy(sortColumn + " " + sortColumnAscDesc);
                }

                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    searchValue = searchValue.ToLower();
                    _GetGridItem = _GetGridItem.Where(obj => obj.Id.ToString().ToLower().Contains(searchValue)
                    || obj.FirstName.ToLower().Contains(searchValue)
                    || obj.LastName.ToLower().Contains(searchValue)
                    || obj.DateOfBirth.ToLower().Contains(searchValue)
                    || obj.City.ToLower().Contains(searchValue)
                    || obj.Country.ToLower().Contains(searchValue)
                    || obj.MobileNo.ToLower().Contains(searchValue)
                    || obj.Email.ToLower().Contains(searchValue)
                    || obj.CreatedDate.ToLower().Contains(searchValue));
                }

                resultTotal = _GetGridItem.Count();

                var result = _GetGridItem.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = resultTotal, recordsTotal = resultTotal, data = result });

            }
            catch (Exception) { throw; }
        }


        public async Task<IActionResult> Details(long? id)
        {
            if (id == null) return NotFound();
            PersonalInfoCRUDViewModel vm = await _context.PersonalInfo.FirstOrDefaultAsync(m => m.Id == id);
            if (vm == null) return NotFound();
            return PartialView("_Details", vm);
        }

        public async Task<IActionResult> AddEdit(int id)
        {
            PersonalInfoCRUDViewModel vm = new PersonalInfoCRUDViewModel();
            if (id > 0) vm = await _context.PersonalInfo.Where(x => x.Id == id).SingleOrDefaultAsync();
            return PartialView("_AddEdit", vm);
        }

        [HttpPost]
        public async Task<JsonResult> AddEdit(PersonalInfoCRUDViewModel vm)
        {
            try
                {
                    PersonalInfo _PersonalInfo = new PersonalInfo();
                        if (vm.Id > 0)
                        {
                            _PersonalInfo = await _context.PersonalInfo.FindAsync(vm.Id);

                            vm.CreatedDate = _PersonalInfo.CreatedDate;
                            vm.CreatedBy = _PersonalInfo.CreatedBy;
                            vm.ModifiedDate = DateTime.Now;
                            vm.ModifiedBy = "Admin";
                            _context.Entry(_PersonalInfo).CurrentValues.SetValues(vm);
                            await _context.SaveChangesAsync();
                            return new JsonResult("Personal Info Updated Successfully. ID: " + _PersonalInfo.Id);
                        }
                        else
                        {
                            _PersonalInfo = vm;
                            _PersonalInfo.CreatedDate = DateTime.Now;
                            _PersonalInfo.ModifiedDate = DateTime.Now;
                            _PersonalInfo.CreatedBy = "Admin";
                            _PersonalInfo.ModifiedBy = "Admin";
                            _context.Add(_PersonalInfo);
                            await _context.SaveChangesAsync();                           
                            return new JsonResult("Personal Info Created Successfully. ID: " + _PersonalInfo.Id);
                        }
                }
                catch (Exception) { throw; }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Int64 id)
        {
            try
            {
                var _PersonalInfo = await _context.PersonalInfo.FindAsync(id);
                _PersonalInfo.ModifiedDate = DateTime.Now;
                _PersonalInfo.ModifiedBy = "Admin";
                _PersonalInfo.Cancelled = true;

                _context.Update(_PersonalInfo);
                await _context.SaveChangesAsync();
                return new JsonResult(_PersonalInfo);
            }
            catch (Exception) { throw; }
        }
    }
}

