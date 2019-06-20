using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    /// <summary>
    /// جدول نوع مهاجرت
    /// </summary>
    [Table("EmigrationTypes")]
    public class EmigrationType:BaseEntity<int>
    {
        #region ############# Constructors #############

        #endregion#########

        #region ############## Properties #########################

        /// <summary>
        /// نام نوع مهاجرت
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// چکیده توضیحات نوع مهاجرت
        /// </summary>
        public string Abstract { get; set; }

        /// <summary>
        /// توضیحات نوع مهاجرت
        /// </summary>
        public string Description { get; set; }

        #endregion#############

        #region #################### Navigation Properties ########################


        public virtual ICollection<EmigrateCountry> EmigrateCountries { get; set; }

        #endregion ##################

        #region  ########################### Methods ################

        #endregion
    }
}
