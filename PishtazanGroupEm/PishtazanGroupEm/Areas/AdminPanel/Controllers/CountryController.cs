using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.DAL;
using Model.Entities;
using Model.Models.Countries;
using Model.Models.CountryCover_Images;
using Model.Models.CountryCoverImages;
using Model.OwnedTypeClasses;
using Model.Service;
using Model.UnitOfWork;
using Newtonsoft.Json;
using PishtazanGroupEm.Areas.AdminPanel.Models.ViewModels;

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

        private readonly IUploadingService _uploadingService;

        private readonly ICountryService _countrySerice;

        public CountryController(IUnitOfWork unitOfWork, IUploadingService uploadingService
            , ICountryService countryService)
        {
            _unitOfWork = unitOfWork;
            _uploadingService = uploadingService;
            _countrySerice = countryService;
        }

        #endregion#################

        #region ####################### Actions #################################################

        /// <summary>
        /// نمایش لیست کشور ها
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //نمایش پیام حذف با موفقیت انجام شد
            //if (TempData["Message"]!= null)
            //{
            //   ViewBag.status = TempData["Message"].ToString();

            //}
            try
            {
                var model = await _unitOfWork.CountryRepUW.GetAsync(null, c => c.OrderByDescending(cu => cu.Id));

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
        /// اپلود کردن تصویر و ویدو برای کشور
        /// </summary>
        /// <param name="files">فایل دریافتی از کاربر برای آپلودکردن</param>
        /// <param name="inputId">نام آیدی دریافتی اینپوتی که روی ان کلیک شده استبرای انتخاب تصویر</param>
        /// برای اینکه تشخیص دهیم روی دکمه اپلو.د تصویر شاخص کلیک شده است یا مجموعه تصاویر یا ویدوها
        /// <returns></returns>
        /// اطالاعات به صورت زیر ارسال شده است
        /// data{files:"asdff.jpg",inputId:"#videoFiles"}
        [HttpPost]
        public async Task<IActionResult> UploadFile(IEnumerable<IFormFile> files, string inputId)
        {
            if (files.Count() != 0)
            {
                string imagePath = "";
                string thumbnailImagePath = null;


                //چک کردن نوع فایلها و حجم ان
                foreach (var item in files)
                {
                    if (inputId == "#indexFiles" || inputId == "#imgFiles")
                    {
                        //اگر فایل ها تصویر بودند
                        if (item.ContentType == "image/jpg" || item.ContentType == "image/jpeg" || item.ContentType == "image/pjpeg"
                            || item.ContentType == "image/gif" || item.ContentType == "image/x-png" || item.ContentType == "image/png")
                        {

                            //چک کردن حجم فایل برحسب بایت
                            //تا حجم 10مگابایت
                            if (item == null || item.Length <= 0 || item.Length >= 10240000)
                            {

                                // اگر حجم فایل بیش از حد مجاز باشد یا یکی از فایل ها نال باشد
                                return Json(new { status = "empty", message = "حجم فایل بیش از حد مجاز است یا یکی از فایل هامشکل دارد" });
                            }


                        }
                        else
                        {
                            return Json(new { status = "typeError", message = "فرمت فایل ها یا یکی از فایل های انتخابی جز فایل های مجاز  نیست" });

                        }
                    }

                    //اگر فایل ها فیلم بودند
                    else if (inputId == "#videoFiles")
                    {
                        if (item.ContentType == "video/mp4" || item.ContentType == "video/mkv" || item.ContentType == "video/Matroska" || item.ContentType == "video/x-matroska"
                            || item.ContentType == "image/webm" || item.ContentType == "image/ogg" || item.ContentType == "image/3gp")
                        {

                            //چک کردن حجم فایل برحسب بایت
                            //تا حجم ۱ گیگ
                            if (item == null || item.Length <= 0 || item.Length >= 1000720000)
                            {
                                // اگر حجم فایل بیش از حد مجاز باشد یا یکی از فایل ها نال باشد
                                return Json(new { status = "empty", message = "حجم فایل بیش از حد مجاز است یا یکی از فایل هامشکل دارد" });
                            }

                        }
                        else
                        {
                            return Json(new { status = "typeError", message = "فرمت فایل ها یا یکی از فایل های انتخابی جز فایل های مجاز  نیست" });

                        }
                    }
                }
                if (inputId == "#indexFiles")
                {
                    //فایل عکس است و تصویر شاخص کشور است

                    //مسیر ذخیره تصویرعادی شاخص کشور
                    imagePath = "upload//country//indexImage//normalImage//";

                    //مسیر ذخیره تصویربند انگشتی شاخص کشور
                    thumbnailImagePath = "upload//country//indexImage//thumbnailImage//";
                    //دریافت مجموعه نام فایلهای آپلود شده از لایه سرویس و متد آپلود کردن تصویر
                    List<string> indexImgName = await _uploadingService.UploadFiles(files, imagePath, thumbnailImagePath);

                    ViewBag.indexImgNM = indexImgName;
                    return Json(new { status = "success", message = "فایل با موفقیت آپلود شد", indexImagName = indexImgName });

                }
                else if (inputId == "#imgFiles")
                {
                    //مسیر ذخیره تصاویر  کشور
                    imagePath = "upload//country//images//";

                    //دریافت مجموعه نام فایلهای آپلود شده از لایه سرویس و متد آپلود کردن تصویر
                    List<string> ImageFileNames = await _uploadingService.UploadFiles(files, imagePath, thumbnailImagePath);

                    return Json(new { status = "success", message = "فایل با موفقیت آپلود شد", imageNames = ImageFileNames });
                }
                else if (inputId == "#videoFiles")
                {
                    //مسیر ذخیره ویدوهای  کشور
                    imagePath = "upload//country//videos//";

                    //دریافت مجموعه نام فایلهای آپلود شده از لایه سرویس و متد آپلود کردن تصویر
                    List<string> videoFileNames = await _uploadingService.UploadFiles(files, imagePath, thumbnailImagePath);

                    return Json(new { status = "success", message = "فایل با موفقیت آپلود شد", videoNames = videoFileNames });
                }

            }

            //اگر تصویری برای اپلود انتخاب نشود
            return Json(new { status = "empty", message = "تصویری برای آپلود انتخاب نشده است" });
        }



        /// <summary>
        /// نمایش مودال افزودن کشور
        /// </summary>
        /// <returns></returns>
        ///todo: دارند باید از روی کلاس انها نمونه ساخت و بعد مقدار دهی گرد owned  برای مقدار دهی پراپرتی هایی که ازنوع کلاس هایی که اتریبیوت 
        public IActionResult CreateCountry()
        {
            //CreatCountryMViewModel model = new CreatCountryMViewModel();
            CountryCreateDto model = new CountryCreateDto();

            return PartialView("_CreateCountryPartial", model);
        }






        /// <summary>
        /// فزودن کشور متد پست
        /// </summary>
        /// <param name="model">مدل دریافتی از ویو</param>
        /// <param name="images">نام تصاویر دریافتی از ویو</param>
        /// <param name="videos">نام ویودوهای دریافتی از ویو</param>
        /// <returns></returns>

        [HttpPost, ActionName("CreateCountry")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCountryConfirm(CountryCreateDto model, List<string> images, List<string> videos)

        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var ImagesNM = ViewBag.ImagesNM;
                    if (ModelState.IsValid)
                    {
                        //###################------ایجاد کشور در جدول کشور ها -----#########################

                        if (model.IndexImage == null)
                        {
                            //اگر تصویر شاخص آپلود نشده بود تصویر پیش فرض را اپلود کند 
                            model.IndexImage = "384e5d169c.jpg";

                        }

                        if (model.SkillWorkingOption == null)
                        {
                            //مقدار پیش فرض هر پراپرتی را صفر میدهد-مقدار پیش فرض متغیر اینت
                            SkillWorkingOptions skOp = new SkillWorkingOptions();
                            model.SkillWorkingOption = skOp;
                        }
                        //---------------- برای اینکه در مپ کردن خطا ندهد چون مقدار نال میگیرد این کلاس و خطا میدهد--------------

                        //todo:صرف نطر کردن از پاپرتی ک در ویومدل نمیدهیم ولی در مدل وجود دارد
                        if (model.TouristOption == null)
                        {
                            //مقدار پیش فرض هر پراپرتی را صفر میدهد-مقدار پیش فرض متغیر اینت
                            TouristOptions tOp = new TouristOptions();
                            model.TouristOption = tOp;
                        }

                        //------------------------------

                        await _unitOfWork.CountryRepUW.CreateAsync(model);
                        await _unitOfWork.SaveAsync();
                        //############------#############

                        //-######################--CoverImage ثبت نام تمجموعه تصاویر کشور در دول تصاویر---#####################################

                        if (images.Count != 0)
                        {
                            //--------------------------- بدست اوردن شناسه کشوری که میخواهیم تصاویر را برای ان ثبت کنیم در جدول تصاویر --------------------------------
                            var country = await _unitOfWork.CountryRepUW.GetAsync(c => c.Name == model.Name);
                            int countryId = country.FirstOrDefault().Id;
                            //-----------------------------------------------------------
                            for (int i = 0; i < images.Count; i++)
                            {

                                CountryCoverImageDto coverImageDto = new CountryCoverImageDto
                                {
                                    ImageName = images[i],
                                    CountryId = countryId
                                };
                                await _unitOfWork.CountryCoverImageRepoUW.CreateAsync(coverImageDto);
                                await _unitOfWork.SaveAsync();
                            }
                        }

                        //-######################--#####################################

                        //------######################----CoverVideo ثبت نام تمجموعه تصاویر کشور در دول تصاویر--------######################----------

                        if (videos.Count != 0)
                        {
                            for (int i = 0; i < videos.Count; i++)
                            {
                                //--------------------------- بدست اوردن شناسه کشوری که میخواهیم ویدوها را برای ان ثبت کنیم در جدول ویدوها --------------------------------
                                var country = await _unitOfWork.CountryRepUW.GetAsync(c => c.Name == model.Name);
                                int countryId = country.FirstOrDefault().Id;
                                //-----------------------------------------------------------

                                CountryCoverVideoDto coverVideoDto = new CountryCoverVideoDto
                                {
                                    VideoName = videos[i],
                                    CountryId = countryId
                                };

                                await _unitOfWork.CountryCoverVideoRepoUW.CreateAsync(coverVideoDto);
                                await _unitOfWork.SaveAsync();
                            }
                        }

                        //-######################--#####################################

                        ///درصورت خطا ندادن اعمال روی دیتابیس
                        ///اجرای تراکنش و اعمال تمام تغییرات در دیتابیس
                        transaction.Commit();

                        return Json(new { status = "success", message = model.Name });
                    }

                    // ---------------اگر ولیدیشن رعایت نشده بود-------

                    //-----اگر ولیدیشن رعایت نشده بود عکس آپلود شده دوباره نمایش داده شود ------------------
                    //و نام ان برای ذخیره در دیتابیس بماند و نیاز ب اپدیت مجدد نباشد
                    if (model.IndexImage != null)
                    {
                        // این روش بدون کوکی است
                        //در کنترلر یوزر و اکشن ایجاد از کوکی استفاده کرده ام
                        ViewBag.indexImgNM = model.IndexImage;
                    }
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

                    //ModelState.AddModelError("Password", "نام کاربری یا رمزعبور اشتباه است");
                    //return View(model);

                    //todo:ولیدیشن ها را اعلام خطا کنم در ویو
                    //return Json(new { status = "fail", message = "خطایی رخ داده است دوباره تلاش کنید" });
                    // return View(model);

                }
                catch (DbUpdateConcurrencyException ex)
                {

                    throw ex;
                }
                catch (DbUpdateException ex)
                {

                    throw ex;
                }
                catch (Exception ex)
                {
                    ///درصورت بوجود امدن خطا در عملیات روی دیتابیس
                    ///تمام عمنلیات به عقب برمیکردد
                    transaction.Rollback();

                    // return Json(new { message = ex });
                    throw ex;
                }
            }
        }



        /// <summary>
        /// نمایش مودال ویرایش کشور
        /// </summary>
        /// <param name="Id">شناسه کشور</param>
        /// <returns></returns>
        ///todo: دارند باید از روی کلاس انها نمونه ساخت و بعد مقدار دهی گرد owned  برای مقدار دهی پراپرتی هایی که ازنوع کلاس هایی که اتریبیوت 
        [HttpGet]
        public async Task<IActionResult> EditCountry(int Id)
        {
            if (Id == 0)
            {
                return RedirectToAction("Index");
            }

            //دریافت مدل کشور و تصاویر و ویدو ها
            var complexModel = await _countrySerice.GetEditByIdAsync(Id);

            //مدل کشور
            CountryCreateDto model = complexModel.Item3;

            //ارسال مجموعه تصاویر کشور
            ViewBag.coverImages = complexModel.Item1;

            //ارسال مجموعه ویدوهای کشور
            ViewBag.coverVideos = complexModel.Item2;


            if (model == null)
            {
                return RedirectToAction("Index");

            }

            return PartialView("_EditCountryPartial", model);
        }





        /// <summary>
        /// ویرایش کشور متد پست
        /// </summary>
        /// <returns></returns>
        [HttpPost, ActionName("EditCountry")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCountryConfirm(CountryCreateDto model, List<string> images, List<string> videos)

        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {


                try
                {
                    //var xc = ViewBag.coverImages;
                    var ImagesNM = ViewBag.ImagesNM;
                    if (ModelState.IsValid)
                    {
                        //###################------ایجاد کشور در جدول کشور ها -----#########################

                        if (model.SkillWorkingOption == null)
                        {
                            //مقدار پیش فرض هر پراپرتی را صفر میدهد-مقدار پیش فرض متغیر اینت
                            SkillWorkingOptions skOp = new SkillWorkingOptions();
                            model.SkillWorkingOption = skOp;
                        }
                        //---------------- برای اینکه در مپ کردن خطا ندهد چون مقدار نال میگیرد این کلاس و خطا میدهد--------------

                        //todo:صرف نطر کردن از پاپرتی ک در ویومدل نمیدهیم ولی در مدل وجود دارد
                        if (model.TouristOption == null)
                        {
                            //مقدار پیش فرض هر پراپرتی را صفر میدهد-مقدار پیش فرض متغیر اینت
                            TouristOptions tOp = new TouristOptions();
                            model.TouristOption = tOp;
                        }

                        //------------------------------

                        _unitOfWork.CountryRepUW.Update(model);
                        await _unitOfWork.SaveAsync();
                        //############------#############

                        //-######################--CoverImage ثبت نام تمجموعه تصاویر کشور در دول تصاویر---#####################################

                        if (images.Count != 0)
                        {
                            //--------------------------- بدست اوردن شناسه کشوری که میخواهیم تصاویر را برای ان ثبت کنیم در جدول تصاویر --------------------------------
                            var country = await _unitOfWork.CountryRepUW.GetAsync(c => c.Name == model.Name);
                            int countryId = country.FirstOrDefault().Id;
                            //-----------------------------------------------------------
                            for (int i = 0; i < images.Count; i++)
                            {

                                CountryCoverImageDto coverImageDto = new CountryCoverImageDto
                                {
                                    ImageName = images[i],
                                    CountryId = countryId
                                };
                                await _unitOfWork.CountryCoverImageRepoUW.CreateAsync(coverImageDto);
                                await _unitOfWork.SaveAsync();
                            }
                        }

                        //-######################--#####################################

                        //------######################----CoverVideo ثبت نام تمجموعه تصاویر کشور در دول تصاویر--------######################----------

                        if (videos.Count != 0)
                        {
                            for (int i = 0; i < videos.Count; i++)
                            {
                                //--------------------------- بدست اوردن شناسه کشوری که میخواهیم ویدوها را برای ان ثبت کنیم در جدول ویدوها --------------------------------
                                var country = await _unitOfWork.CountryRepUW.GetAsync(c => c.Name == model.Name);
                                int countryId = country.FirstOrDefault().Id;
                                //-----------------------------------------------------------

                                CountryCoverVideoDto coverVideoDto = new CountryCoverVideoDto
                                {
                                    VideoName = videos[i],
                                    CountryId = countryId
                                };

                                await _unitOfWork.CountryCoverVideoRepoUW.CreateAsync(coverVideoDto);
                                await _unitOfWork.SaveAsync();
                            }
                        }

                        //-######################--#####################################

                        ///درصورت خطا ندادن اعمال روی دیتابیس
                        ///اجرای تراکنش و اعمال تمام تغییرات در دیتابیس
                        transaction.Commit();

                        return Json(new { status = "success", message = model.Name });
                    }

                    // ---------------اگر ولیدیشن رعایت نشده بود-------

                    //-----اگر ولیدیشن رعایت نشده بود عکس آپلود شده دوباره نمایش داده شود ------------------
                    //و نام ان برای ذخیره در دیتابیس بماند و نیاز ب اپدیت مجدد نباشد
                    if (model.IndexImage != null)
                    {
                        // این روش بدون کوکی است
                        //در کنترلر یوزر و اکشن ایجاد از کوکی استفاده کرده ام
                        ViewBag.indexImgNM = model.IndexImage;
                    }
                    //---------------------------------------------------------
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

                    //ModelState.AddModelError("Password", "نام کاربری یا رمزعبور اشتباه است");
                    //return View(model);

                    //todo:ولیدیشن ها را اعلام خطا کنم در ویو
                    //return Json(new { status = "fail", message = "خطایی رخ داده است دوباره تلاش کنید" });
                    // return View(model);

                }
                catch (DbUpdateConcurrencyException ex)
                {

                    throw ex;
                }
                catch (DbUpdateException ex)
                {

                    throw ex;
                }
                catch (Exception ex)
                {

                    ///درصورت بوجود امدن خطا در عملیات روی دیتابیس
                    ///تمام عمنلیات به عقب برمیکردد
                    transaction.Rollback();

                    // return Json(new { message = ex });
                    throw ex;
                }
            }
        }



        /// <summary>
        /// نمایش مودال حذف
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DeleteCountry(int Id)
        {
            var model = await _unitOfWork.CountryRepUW.GetByIdAsync(Id);
            ViewBag.countryName = model.Name;
            return PartialView("_DeleteCountryPartial", model.Name);
        }

        /// <summary>
        /// حذف کشور
        /// </summary>
        /// <param name="Id">شناسه کشور</param>
        /// <returns></returns>
        /// حذف کور به همراه تصویر اخص و تصاویر و ویدوهای ان به جز تصاویر محتوای کشور
        [HttpPost, ActionName("DeleteCountry")]
        public async Task<IActionResult> DeleteConfirm(int Id)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {


                try
                {

                    ///-------------حذف تصاویر و ویدوها و تصویر شاخص کشور از روت سایت-------------------

                    ///تصاویر محتوا حذف نمیشوند
                    ///حذف فایل های مرتبط با کشور از روت سایت
                    await _countrySerice.DeleteRootFile(Id);


                    ///--------------- countryCoverimage - countrycovervideo حذف ویدوها و تصاویر از جدول های -------------------------------
                    await _countrySerice.DeleteCoverdImage(Id);
                    await _countrySerice.DeleteCoverVideo(Id);


                    ///--------------حذف کشور------------------------
                    await _unitOfWork.CountryRepUW.DeleteById(Id);
                    await _unitOfWork.CountryRepUW.SaveAsync();

                    ///درصورت خطا ندادن اعمال روی دیتابیس
                    ///اجرای تراکنش و اعمال تمام تغییرات در دیتابیس
                    transaction.Commit();

                    //نمایش پیام حذف با موفقیت انجام شد
                    // TempData["Message"] = "success";
                    //TempData.Keep("Message");

                    //return Json(new { status = "success", countryName = countryName });
                    //return RedirectToAction(nameof(Index), new { status = "success" });    
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateConcurrencyException ex)
                {

                    throw ex;
                }
                catch (DbUpdateException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    ///درصورت بوجود امدن خطا در عملیات روی دیتابیس
                    ///تمام عمنلیات به عقب برمیکردد
                    transaction.Rollback();
                    throw ex;
                }


            }
        }

        ///todo:  کلی و اخری کافی است catch   یک رول بک نوشت با برای   catch  آیا نیاز است برای هر 
        ///todo:ویرایش کشور  تکمیل نشده است
        ///todo:نمایش پیام حذف با موفقیت انجام نشده ات
        ///todo:temp data and session
        ///todo:حذف تصویر شاخص و سپس اپدیت تصویر جدید در ویرایش
        ///todo:حذف ویدو و تصاویر و نمایش انها برای انتخاب توسط کاربر برای حذف  در ویرایش
        ///toso:اعمال فیلتر برای مثلا ۳تا ویدو و تصویر اخر اپلود شده برای کشور درویو اصلی سایت 

        #endregion#################
    }
}