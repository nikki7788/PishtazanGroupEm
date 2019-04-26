using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.Models.Countries;
using Model.OwnedTypeClasses;
using Model.UnitOfWork;

namespace PishtazanGroupEm.Areas.AdminPanel.Controllers
{

    /// <summary>
    /// کنترلر کشور ها
    /// </summary>

    [Area("AdminPanel")]
    public class CountryController : Controller
    {
        #region ####################### Dependencies #################################################

        private readonly IUnitOfWork _unitOfWork;
        public CountryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion#################

        #region ####################### Actions #################################################

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model=  await _unitOfWork.CountryRepUW.GetAsync(null,c=>c.OrderByDescending(cu=>cu.Id));
            return View(model);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ///todo: دارند باید از روی کلاس انها نمونه ساخت و بعد مقدار دهی گرد owned  برای مقدار دهی پراپرتی هایی که ازنوع کلاس هایی که اتریبیوت 
        //[HttpGet]
        //public IActionResult CreateCountry()
        //{
            
        //    CountryCreatDto model = new CountryCreatDto();
            
        //    return PartialView("_CreateCountryPartial",model);
        //}

        public async Task<IActionResult> CreateCountry()
        {
            CountryCreatDto model = new CountryCreatDto();
            return PartialView("_CreateCountryPartial",model);
        }

        #endregion#################
    }
}