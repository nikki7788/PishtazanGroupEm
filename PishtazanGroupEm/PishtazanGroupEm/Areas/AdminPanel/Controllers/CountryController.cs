using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        //public async Task<IActionResult> Create()
        //{

        //    return PartialView();
        //}

        #endregion#################
    }
}