using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models.Countries;
using Model.OwnedTypeClasses;
using Model.Service;
using Model.UnitOfWork;
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
            CreatCountryMViewModel model = new CreatCountryMViewModel();
            return PartialView("_CreateCountryPartial", model);
        }

        /// <summary>
        /// اپلود کردن تصویر و ویدو برای کشور
        /// </summary>
        /// <param name="files">فایل دریافتی از کاربر برای آپلودکردن</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadFile(IEnumerable<IFormFile> files)
        {
            if (files.Count() != 0)
            {
                string imagePath = "";
                string thumbnailImagePath =null;
                bool typeValidate = false; //برمیگرداند true  درصورت درست بودن فرمت فایل و حجم فایل 
                byte typeF = 0; // درصورتی که فایل عکس باشد 0 برمیگرداند و درصورتی که فیلم باشد 1 برمیگرداند

                //چک کردن نوع فایلها و حجم ان
                foreach (var item in files)
                {

                    //اگر فایل ها تصویر بودند
                    if (item.ContentType == "image/jpg" || item.ContentType == "image/jpeg" || item.ContentType == "image/pjpeg"
                        || item.ContentType == "image/gif" || item.ContentType == "image/x-png" || item.ContentType == "image/png")
                    {

                        //چک کردن حجم فایل
                        if (item != null && item.Length > 0 && item.Length <= 10240000)
                        {

                            //درصورت صحیح بودن تمام شرط ها
                            typeValidate = true;

                        }
                        else
                        {
                            // اگر حجم فایل بیش از حد مجاز باشد یا یکی از فایل ها نال باشد
                            return Json(new { status = "empty", message = "حجم فایل بیش از حد مجاز است یا یکی از فایل هامشکل دارد" });
                        }

                    }

                    //اگر فایل ها فیلم بودند
                    else if (item.ContentType == "video/mp4" || item.ContentType == "video/mkv" || item.ContentType == "image/webm"
                        || item.ContentType == "image/ogg" || item.ContentType == "image/3gp")
                    {
                        //چک کردن حجم فایل
                        if (item != null && item.Length > 0 && item.Length <= 30720000)
                        {

                            //درصورت صحیح بودن تمام شرط ها
                            typeValidate = true;

                            typeF = 1;
                        }
                        else
                        {
                            // اگر حجم فایل بیش از حد مجاز باشد یا یکی از فایل ها نال باشد
                            return Json(new { status = "empty", message = "حجم فایل بیش از حد مجاز است یا یکی از فایل هامشکل دارد" });
                        }

                    }
                }
                if (files.Count() == 1 && typeValidate == true)
                {
                    //فایل عکس است و تصویر شاخص کشور است

                    //مسیر ذخیره تصویرعادی شاخص کشور
                    imagePath = "upload//country//indexImage//normalImage//";

                    //مسیر ذخیره تصویربند انگشتی شاخص کشور
                    thumbnailImagePath = "upload//country//indexImage//thumbnailImage//";
                }
                else if (files.Count() != 1 && typeValidate == true)
                {
                    //مسیر ذخیره تصاویر  کشور
                    imagePath = "upload//country//images//";
                }
                else if (files.Count() != 1 && typeValidate == true && typeF == 1)
                {
                    //مسیر ذخیره ویدوهای  کشور
                    imagePath = "upload//country//videos//";
                }


                //دریافت مجموعه نام فایلهای آپلود شده از لایه سرویس و متد آپلود کردن تصویر
                List<string> fileNames = await _uploadingService.UploadFiles(files, imagePath, thumbnailImagePath);

                return Json(new { status = "success", message = "فایل با موفقیت آپلود شد", imageName = fileNames });
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
        public async Task<IActionResult> CreateCountryConfirm(CreatCountryMViewModel model, string indexImg, List<string> imgs, List<string> videos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.CountryRepUW.CreateAsync(model.Country);

                }
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }



        #endregion#################
    }
}