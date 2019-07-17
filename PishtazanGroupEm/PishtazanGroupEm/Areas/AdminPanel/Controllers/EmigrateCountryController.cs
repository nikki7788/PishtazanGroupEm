using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Models.Countries;
using Model.Models.EmigrateCountries;
using Model.UnitOfWork;
using PishtazanGroupEm.Areas.AdminPanel.Models.ViewModels;

namespace PishtazanGroupEm.Areas.AdminPanel.Controllers
{
    /// <summary>
    /// کنترلر کشور - نوع مهاجرت
    /// </summary>
    /// برای هر کشور انوع مهاجرت مشخص میشود
    [Area("AdminPanel")]
    public class EmigrateCountryController : Controller
    {
        #region ###################### Dependencies ###########################

        private readonly IUnitOfWork _unitOfWork;

        public EmigrateCountryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion############



        #region ###################### Actions ###########################


        /// <summary>
        /// نمایش لیست کشور ها به همراه انواع مهاجرت انها
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<EmigrateCountryListDto> model = await _unitOfWork.EmigrationCountryRepoUW.GetAsync(null, e => e.OrderByDescending(em => em.Id));
                return View(model);

            }
            catch (ArgumentNullException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        /// <summary>
        /// نمایش مودال افزودن انواع مهاجرت به کشور
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CreateEmigrateCountry()
        {
            try
            {

                CreateEmigrateCountryMViewModel model = new CreateEmigrateCountryMViewModel();
                ///---------------------- دریافت مقادیر کشورهای ثبت شده در جدول کشور - مهاجرت ---------------------
                var emCountry = await _unitOfWork.EmigrationCountryRepoUW.GetAsync();
                var lstEmCountry = emCountry.GroupBy(c => c.CountryId);

                ///---------------------- دریافت مقادیر کشورهای ثبت شده در جدول کشور --------------------
                var countries = await _unitOfWork.CountryRepUW.GetAsync();
                List<CountryListDto> lstOfCountries = countries.ToList();

                ///---------------------- افزودن تمام کشورهای موجود در جدول کشورها به یک متغیر --------------------
              
                ///---------- برای عدم نمایش کشورهایی که نوع مهاجرت برای انها ثبت شده است در کمبوباکس ایجاد ------------
                List<CountryListDto> selectedcountries = new List<CountryListDto>();
                selectedcountries.AddRange(lstOfCountries);
                foreach (var emc in lstEmCountry)
                {
                    foreach (var co in lstOfCountries)
                    {
                        if (co.Id == emc.Key)
                        {
                            ///--------------   حذف کشورهایی که برای انها نوع مهاجرت مشخص کردیم از لیست متغیر  --------
                            selectedcountries.Remove(co);
                        }

                    }
                }

                ///ارسال نام کشور ها و نام انواع مهاجرت به ویو برای نمایش در لیست بازشو =کمبوباکس
                ViewBag.listOfCountry = selectedcountries;
                ViewBag.listOfEmgType = await _unitOfWork.EmigrationTypeRepoUW.GetAsync();

                return PartialView("_CreateEmigrateCountryPartial", model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost, ActionName("CreateEmigrateCountry")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmgCoConfirm(CreateEmigrateCountryMViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    foreach (var item in model.EmigrationTypeId)
                    {
                        EmigrateCountryCreateDto EmgCountry = new EmigrateCountryCreateDto
                        {
                            CountryId = model.CountryId,
                            EmigrationTypeId = item
                        };
                        await _unitOfWork.EmigrationCountryRepoUW.CreateAsync(EmgCountry);
                        await _unitOfWork.SaveAsync();

                    }

                    return Json(new { status = "success", message = "عملیات با موفقیت انجام شد" });
                }
                else
                {
                    // ---------------اگر ولیدیشن رعایت نشده بود-------

                    ///ارسال نام کشور ها و نام انواع مهاجرت به ویو برای نمایش در لیست بازشو =کمبوباکس
                    ViewBag.listOfCountry = await _unitOfWork.CountryRepUW.GetAsync();
                    ViewBag.listOfEmgType = await _unitOfWork.EmigrationTypeRepoUW.GetAsync();

                    //-------------------------- خطاهای ورودی  هارا برمیکرداند-------------------------------
                    //display validation with jquery ajax
                    var errorMessage = new List<string>();
                    var errorKeys = new List<string>();
                    foreach (var validation in ViewData.ModelState.Values)
                    {
                        errorMessage.AddRange(validation.Errors.Select(error => error.ErrorMessage));

                    }
                    foreach (var modelStateKey in ViewData.ModelState.Keys)
                    {
                        var modelStateVal = ViewData.ModelState[modelStateKey];
                        foreach (var error in modelStateVal.Errors)
                        {
                            errorKeys.Add(modelStateKey);
                            // You may log the errors if you want
                        }
                    }
                    return Json(new { status = "validationError", message = "ورودی های خود رادوباره بررسی کنید", errorMessages = errorMessage, errorKey = errorKeys });

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


        #endregion############
    }
}
