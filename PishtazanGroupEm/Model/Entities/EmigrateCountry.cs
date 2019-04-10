using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Entities
{
    /// <summary>
    /// جدول واسط بین جدول نوع مهاجرت و کشورها
    /// </summary>
    [Table("EmigrateCountry")]
    public class EmigrateCountry : BaseEntity<int>
    {
        #region ############# Constructors #############

        #endregion#########

        #region ############## Properties #########################

        /// <summary>
        /// شناسه کشور
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// شناسه نوع مهاجرت
        /// </summary>
        public int EmigrationTypeId { get; set; }



        #endregion#############

        #region #################### Navigation Properties ########################

        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; }


        [ForeignKey(nameof(EmigrationTypeId))]
        public virtual EmigrationType EmigrationType { get; set; }

        #endregion ##################

        #region  ########################### Methods ################

        #endregion
    }
}
