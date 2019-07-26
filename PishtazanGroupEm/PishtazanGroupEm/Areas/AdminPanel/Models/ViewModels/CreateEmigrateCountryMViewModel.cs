using Model.PublicClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PishtazanGroupEm.Areas.AdminPanel.Models.ViewModels
{
    public class CreateEmigrateCountryMViewModel
    {
        #region ########### Properties ###########


        /// <summary>
        /// شناسه کشور
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [Display(Name = " کشور")]
        public int CountryId { get; set; }



        /// <summary>
        /// شناسه نوع مهاجرت
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [Display(Name = "نوع مهاجرت")]
        public List<int> EmigrationTypeId { get; set; }

        //public List<string> EmigrationType { get; set; }


        #endregion
    }
}
