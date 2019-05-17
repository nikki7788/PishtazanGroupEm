using Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Models.CountryCoverImages
{
    /// <summary>
    /// ویو مدل تصاویر کشور
    /// </summary>
    public class CountryCoverImageDto:BaseEntity<int>
    {
        #region ############# Constructors #############

        #endregion#########

        #region ############## Properties #########################

        /// <summary>
        /// نام فایل تصویری کشور
        /// </summary>
        /// 
        [Display(Name =" تصویر کشور")]
        public string ImageName { get; set; }


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
