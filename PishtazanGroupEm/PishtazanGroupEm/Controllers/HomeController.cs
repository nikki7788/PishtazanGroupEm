using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.Models.Countries;
using Model.UnitOfWork;
using PishtazanGroupEm.Models;

namespace PishtazanGroupEm.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task< IActionResult> Index()
        {
            var model = await _unitOfWork.CountryRepUW.GetAsync();
            return View(model);
            //  return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(CountryCreatDto dto)
        //{
        //    //CountryDto cdo = new CountryDto()
        //    //{
        //    //    Name = "امریکا",
        //    //    Description = "ندارد"
        //    //};
        //   await _unitOfWork.CountryRepUW.CreateAsync(dto);
        //    await _unitOfWork.Save();
        //    return RedirectToAction("Index");
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
