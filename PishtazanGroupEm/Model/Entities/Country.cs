﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    /// <summary>
    /// جدول کشورها
    /// </summary>
    [Table("Country")]
    public class Country:BaseEntity<int>
    {
        #region ############# Constructors #############

        #endregion#########

        #region ############## Properties #########################


        /// <summary>
        /// نام کشور
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// توضیحات کشور
        /// </summary>

        public string  Description  { get; set; }

        #endregion#############

        #region #################### Navigation Properties ########################

        public virtual ICollection<CountryCoverImage> CountryCoverImages { get; set; }

        public virtual ICollection<CountryCoverVideo> CountryCoverVideos { get; set; }

        public virtual ICollection<EmigrateCountry> EmigrateCountries { get; set; }

        #endregion ##################

        #region  ########################### Methods ################

        #endregion
    }
}
//todo:ولیدشن اگر نیاز داشتبنویسم.نیاز ندارد