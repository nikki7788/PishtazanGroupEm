using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    /// <summary>
    /// جدول فایل های ویدویی کشور ها
    /// </summary>
    [Table("CountryCoverVideos")]
    public class CountryCoverVideo : BaseEntity<int>
    {
        #region ############# Constructors #############

        #endregion#########

        #region ############## Properties #########################
        /// <summary>
        /// نام فایل ویدویی کشور
        /// </summary>
        /// 
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
