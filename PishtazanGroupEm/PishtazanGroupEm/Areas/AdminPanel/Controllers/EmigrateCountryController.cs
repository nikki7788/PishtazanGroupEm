using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Models.Countries;
using Model.Models.EmigrateCountries;
using Model.PublicClasses;
using Model.Service;
using Model.UnitOfWork;
using Newtonsoft.Json;
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
        private readonly IEmigrateCountryService _emgCountryService;

        public EmigrateCountryController(IUnitOfWork unitOfWork, IEmigrateCountryService emgCountryService)
        {
            _unitOfWork = unitOfWork;
            _emgCountryService = emgCountryService;
        }

        #endregion############



        #region ###################### Actions ###########################


        /// <summary>
        /// نمایش لیست کشور ها به همراه انواع مهاجرت انها
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            try
            {

                IEnumerable<CountryListDto> model = _emgCountryService.CountriesListIndex();
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

                /////-----ارسال نام کشور ها و نام انواع مهاجرت به ویو برای نمایش در لیست بازشو =کمبوباکس---
                //////کشورهایی که در جدول نوع مهاجرت - کشور ثبت نشده اند
                ///--------------- ارسال اسامی کشورهایی که اطلاعات نوع مهاجرت برای انها ثبت نشده است -------------
                ViewBag.listOfCountry = _emgCountryService.CountriesList();
                ViewBag.listOfEmgType = await _unitOfWork.EmigrationTypeRepoUW.GetAsync();

                return PartialView("_CreateEmigrateCountryPartial", model);

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
        /// افزودن انواع مهاجرت به کشور
        /// </summary>
        /// <param name="model">مدل دریافتی از ویو</param>
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
                        EmigrateCountryCreateDto emgCountry = new EmigrateCountryCreateDto
                        {
                            CountryId = model.CountryId,
                            EmigrationTypeId = item
                        };

                        await _unitOfWork.EmigrateCountryRepoUW.CreateAsync(emgCountry);
                        await _unitOfWork.EmigrateCountryRepoUW.SaveAsync();
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



        /// <summary>
        ///   نمایش مودال ویرایش نوع مهاجرت - کشور
        /// </summary>
        /// <param name="Id">شناسه کشور</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> EditEmigrateCountry(int Id)
        {
            CreateEmigrateCountryMViewModel model = new CreateEmigrateCountryMViewModel();

            ///دریافت اطلاعات=انواع مهجرت هر کشور ثبت شده برای کشور مورد  نطر
            IEnumerable<EmigrateCountryCreateDto> m = await _unitOfWork.EmigrateCountryRepoUW.GetEditAsync(c => c.CountryId == Id);

            //TempData["emId"] = m;
            //ViewBag.mId = m;

            List<int> me = new List<int>();
            ///ارسال مقادیر انخاب شده نوع مهاجرت 
            foreach (var item in m)
            {
                me.Add(item.EmigrationTypeId);
            }

            ///مقداردهی ویو مدل با مقادیر بدست امده 
            model.CountryId = Id;
            model.EmigrationTypeId = me;


            ///ارسال انواع مهاجرت
            ViewBag.listOfEmgType = await _unitOfWork.EmigrationTypeRepoUW.GetAsync();
            ///ارسال کشور انتخاب شده برای ویرایش
            ViewBag.listOfCountry = await _unitOfWork.CountryRepUW.GetEditByIdAsync(Id);



            return PartialView("_EditEmigrateCountryPartial", model);
        }



        /// <summary>
        /// ویرایش نوع مهاجرت-کشور متد پست
        /// </summary>
        /// <param name="model">مدل دریافتی از ویو</param>
        /// <returns></returns>
        [HttpPost, ActionName("EditEmigrateCountry")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmigrateCountryConfirm(CreateEmigrateCountryMViewModel model)

        {
            try
            {

                if (ModelState.IsValid)
                {
                    ///##############-   ویرایش نوع مهاجرت - کشور  -#################
                    ///روش ۲ تمام نوع مهاجرت ها را پاک میکنیم و توع مهاجرت های جدید را ثبت میکنیم
                    ///ـــــــــــــــــــــــــ روش ۱ ـــــــــــــــــــــــــ
                    ///انواع مهاجرت ثبت شده در ویایش را در خود نگه مید ارد برای مقایسه با نوع مهجرت های قبل از ویرایش
                    /// هر دو طرف باید از یک نوع مجموعه باشند exception دستور 
                    List<EmigrateCountryCreateDto> emgLst = new List<EmigrateCountryCreateDto>();
                    foreach (var item in model.EmigrationTypeId)
                    {
                        EmigrateCountryCreateDto emgCountry = new EmigrateCountryCreateDto
                        {
                            CountryId = model.CountryId,
                            EmigrationTypeId = item
                        };
                        emgLst.Add(emgCountry);
                    }
                    ///ــــــــــــــــــــــــــــــــــــــــــــــــــ

                    ///دریافت اطلاعات=انواع مهاجرت ثبت شده برای کشور موردنطر قبل از ویرایش
                    ///برای مقایسه با انواع مهاجرت ثبت شده در ویرایش و حذف نوع مهاجرت حذف شده برای کشور و افزودن و ثبت نوع مهاجرت جدید در دیتابیس
                    IEnumerable<EmigrateCountryCreateDto> em = await _unitOfWork.EmigrateCountryRepoUW.GetEditAsync(c => c.CountryId == model.CountryId);
                  
                    ///افزودن و ثبت نوع مهاجرت جدید موجود درویرایش در داخل دیتابیس
                    var exNew = emgLst.ExceptBy(em, a => a.EmigrationTypeId);

                    foreach (var item in exNew)
                    {
                        ///افزودن نوه مهجرت های جدید به کشورمورد نطر
                        EmigrateCountryCreateDto emgCo = new EmigrateCountryCreateDto
                        {
                            CountryId = model.CountryId,
                            EmigrationTypeId = item.EmigrationTypeId
                        };
                        await _unitOfWork.EmigrateCountryRepoUW.CreateAsync(emgCo);
                        await _unitOfWork.SaveAsync();
                    }


                    ///حذف نوع مهاجرت حذف شده برای کشور مورد نظر در ویرایش از دیتابیس
                    var ex = em.ExceptBy(emgLst, a => a.EmigrationTypeId);
                    foreach (var item in ex)
                    {
                        await _unitOfWork.EmigrateCountryRepoUW.DeleteByIdAsync(item.Id);
                        await _unitOfWork.SaveAsync();
                    }

                    return Json(new { status = "success", message = "عملیات با موفقیت انجام شد" });

                    ///############------#############
                }

                // ---------------اگر ولیدیشن رعایت نشده بود-------

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
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion############
    }
}
