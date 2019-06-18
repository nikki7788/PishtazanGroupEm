using Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.EmigrationTypes
{
    /// <summary>
    /// ویو مدل لیست نوع مهاجرت
    /// </summary>
    public class EmigrationTypeListDto : BaseEntity<int>
    {


        #region ############## Properties #########################

        /// <summary>
        /// نام نوع مهاجرت
        /// </summary>
        [Display(Name = "نوع مهاجرت")]

        public string Name { get; set; }


        /// <summary>
        /// توضیحات نوع مهاجرت
        /// </summary>
        [Display(Name = "توضیحات")]

        public string Description { get; set; }

        #endregion#############

        #region #################### Navigation Properties ########################


    //    public virtual ICollection<EmigrateCountry> EmigrateCountries { get; set; }

        #endregion ##################

    }
}
//todo:ولیدیشن نیاز داره