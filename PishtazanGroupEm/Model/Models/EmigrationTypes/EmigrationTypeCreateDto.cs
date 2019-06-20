using Model.Entities;
using Model.PublicClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models.EmigrationTypes
{
    /// <summary>
    /// ویو مدل ایجاد و ویرایش نوع مهاجرت
    /// </summary>
 public   class EmigrationTypeCreateDto : BaseEntity<int>
    {
        #region ############## Properties #########################

        /// <summary>
        /// نام نوع مهاجرت
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(250, MinimumLength = 2, ErrorMessage = PublicConst.LengthMessage)]
        [RegularExpression(@"[0-9A-Zا-ی ء ؤ ئ ةأإآa-z_\s\-\(\)\.]+", ErrorMessage = PublicConst.DangrouseMessageForBadCharachter)]
        [Display(Name = "نوع مهاجرت")]
        public string Name { get; set; }

        /// <summary>
        /// چکیده توضیحات نوع مهاجرت
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(500, MinimumLength = 2, ErrorMessage = PublicConst.LengthMessage)]
        // [RegularExpression(@"[0-9A-Zا-ی ء ؤ ئ ةأإآa-z_\s\-\(\)\.]+", ErrorMessage = PublicConst.DangrouseMessageForBadCharachter)]
        [Display(Name = "چکیده ")]
        public string Abstract { get; set; }

        /// <summary>
        /// توضیحات نوع مهاجرت
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(250, MinimumLength = 2, ErrorMessage = PublicConst.LengthMessage)]
        //[RegularExpression(@"[0-9A-Zا-ی ء ؤ ئ ةأإآa-z_\s\-\(\)\.]+", ErrorMessage = PublicConst.DangrouseMessageForBadCharachter)]
        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        #endregion#############

        #region #################### Navigation Properties ########################


        public virtual ICollection<EmigrateCountry> EmigrateCountries { get; set; }

        #endregion ##################
    }
}
