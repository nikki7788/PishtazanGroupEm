using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.EmigrationType
{
    /// <summary>
    /// ویو مدل نوع مهاجرت
    /// </summary>
    public class EmigrationTypeDto:BaseEntity<int>
    {
    

        #region ############## Properties #########################

        /// <summary>
        /// نام نوع مهاجرت
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// توضیحات نوع مهاجرت
        /// </summary>
        public string Description { get; set; }

        #endregion#############

        #region #################### Navigation Properties ########################


        public virtual ICollection<EmigrateCountry> EmigrateCountries { get; set; }

        #endregion ##################

    }
}
//todo:ولیدیشن نیاز داره