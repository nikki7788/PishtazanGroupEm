using Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Models.CountryCover_Video
{
    /// <summary>
    /// ویو مدل ویدوهای کشور
    /// </summary>
   public class CountryCoverVideoDto:BaseEntity<int>
    {

        #region ############# Constructors #############

        #endregion#########

        #region ############## Properties #########################
        /// <summary>
        /// نام فایل ویدویی کشور
        /// </summary>
        /// 
        [Display(Name = " ویدوهای کشور")]
        public string VideoName { get; set; }


        /// <summary>
        /// شناسه کشور
        /// </summary>
        public int CountryId { get; set; }

        #endregion ###########

        #region #################### Navigation Properties ########################

        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; }

        #endregion###########
    }
}
