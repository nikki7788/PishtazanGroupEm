using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.Models.EmigrationTypes;
using Model.UnitOfWork;


namespace PishtazanGroupEm.Areas.AdminPanel.Controllers
{
    /// <summary>
    /// کنترلر نوع مهاجرت
    /// </summary>

    [Area("AdminPanel")]

    public class EmigrationTypeController : Controller
    {


        #region################## Dependencies #############################

        private readonly IUnitOfWork _unitOfWork;
        public EmigrationTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion ################################

        #region################## Actions #############################


        /// <summary>
        /// نمایش انواع مهاجرت ها
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<EmigrationTypeListDto> model = await _unitOfWork.EmigrationTypeRepoUW.GetAsync(null, e => e.OrderByDescending(em => em.Id));
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
        /// نمایش مودال افزودن نوع مهاجرت
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateEmigrationType()
        {
            EmigrationTypeCreateDto model = new EmigrationTypeCreateDto();

            return PartialView("_CreateEmigrationTypePartial", model);
        }



        /// <summary>
        /// افزودن نوع مهاجرت متد پست
        /// </summary>
        /// <param name="model">مدل دریافتی از ویو</param>
        /// <returns></returns>
        [HttpPost, ActionName("CreateEmigrationType")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmigrationTypeConfirm(EmigrationTypeCreateDto model)

        {
            try
            {

                if (ModelState.IsValid)
                {

                    ///##############-- ایجاد نوع مهاجرت --#################

                    await _unitOfWork.EmigrationTypeRepoUW.CreateAsync(model);
                    await _unitOfWork.SaveAsync();

                    ///############------#############

                    return Json(new { status = "success", message = model.Name });

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



        /// <summary>
        /// نمایش مودال ویرایش نوع مهاجرت
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> EditEmigrationType(int Id)
        {
            EmigrationTypeCreateDto model = await _unitOfWork.EmigrationTypeRepoUW.GetEditByIdAsync(Id);
            return PartialView("_EditEmigrationTypePartial", model);
        }



        /// <summary>
        /// ویرایش نوع مهاجرت متد پست
        /// </summary>
        /// <param name="model">مدل دریافتی از ویو</param>
        /// <returns></returns>
        [HttpPost, ActionName("EditEmigrationType")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmigrationTypeConfirm(EmigrationTypeCreateDto model)

        {
            try
            {

                if (ModelState.IsValid)
                {

                    ///##############-- ویرایش نوع مهاجرت --#################

                    _unitOfWork.EmigrationTypeRepoUW.Update(model);
                    await _unitOfWork.SaveAsync();

                    ///############------#############

                    return Json(new { status = "success", message = model.Name });

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



        /// <summary>
        /// حذف نوع مهاجرت
        /// </summary>
        /// <param name="Id">شناسه نوع مهاجرت</param>
        /// <returns></returns>
        /// swal in view and ajax حذف به کمک 
        [HttpPost]
        public async Task<IActionResult> DeleteEmigrationType(int delId)
        {
            if (delId != 0)
            {
                await _unitOfWork.EmigrationTypeRepoUW.DeleteByIdAsync(delId);
                await _unitOfWork.EmigrationTypeRepoUW.SaveAsync();
                return Json(new { status = "success" });
            }
            
            return Json(new { status = "fail" });

        }

        #endregion ##########################


    }

}
