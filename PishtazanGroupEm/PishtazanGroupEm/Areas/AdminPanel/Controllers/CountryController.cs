﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public CountryController(IUnitOfWork unitOfWork, IUploadingService uploadingService)
        {
            _unitOfWork = unitOfWork;
            _uploadingService = uploadingService;
        }

        #endregion#################

        #region ####################### Actions #################################################

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _unitOfWork.CountryRepUW.GetAsync(null, c => c.OrderByDescending(cu => cu.Id));
            return View(model);
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
                        if (item.ContentType == "video/mp4" || item.ContentType == "video/mkv"|| item.ContentType == "video/Matroska" || item.ContentType == "video/x-matroska"
                            || item.ContentType == "image/webm"|| item.ContentType == "image/ogg" || item.ContentType == "image/3gp")
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
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost, ActionName("CreateCountry")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCountryConfirm(CountryCreateDto model,  List<string> images, List<string> videos)
            
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
                    /////////////////////-----------برای جلوگیری از خزای نال دادن در دیتابیس----------------////////////////////
                    if (model.SkillWorkingOption==null)
                    {
                        SkillWorkingOptions sk = new SkillWorkingOptions();
                        //اگر هزینه خدمت های اختیاری برای مهاجرت کاری نال بود
                        sk.FindingJobCVPrice = 0;
                        sk.MakingCoverLetterPrice = 0;
                        sk.MakingCVPrice = 0;
                        sk.MakingLinkedInPrice = 0;
                        model.SkillWorkingOption = sk;
                    }
                    if (model.TouristOption==null)
                    {
                        //اگر هزینه خدمت های اختیاری برای مهاجرت توریستی نال بود
                        TouristOptions ts = new TouristOptions();
                        ts.BookingHotel = 0;
                        ts.BookingPlane = 0;
                        ts.TakingEmbassyInterview = 0;
                        ts.TakingInvitation = 0;
                        ts.TakingTrainTicket = 0;
                        ts.TravelArrangment = 0;
                        model.TouristOption = ts;
                    }
                    ///////////-------------------------///////////
          
                    await _unitOfWork.CountryRepUW.CreateAsync(model);
                    await _unitOfWork.SaveAsync();
                    //############------#############

                    //-######################--CoverImage ثبت نام تمجموعه تصاویر کشور در دول تصاویر---#####################################

                    if (images.Count != 0)
                    {
                        for (int i = 0; i < images.Count; i++)
                        {

                            CountryCoverImageDto coverImageDto = new CountryCoverImageDto
                            {
                                ImageName = images[i],
                                CountryId = model.Id
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

                            CountryCoverVideoDto coverVideoDto = new CountryCoverVideoDto
                            {
                                VideoName = videos[i],
                                CountryId = model.Id
                            };

                            await _unitOfWork.CountryCoverVideoRepoUW.CreateAsync(coverVideoDto);
                            await _unitOfWork.SaveAsync();
                        }
                    }

                    //-######################--#####################################

                    return Json(new { status = "success", message = model.Name  });
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
               // return Json(new { message = ex });
               throw ex;
            }
        }



        #endregion#################
    }
}