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




        #endregion ##########################
    }
}
