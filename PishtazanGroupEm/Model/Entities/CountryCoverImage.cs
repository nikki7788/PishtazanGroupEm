using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    /// <summary>
    /// جدول تصاویر کشورها
    /// </summary>
    /// 
    [Table("CountryCoverImage")]
    public class CountryCoverImage : BaseEntity<int>
    {
        #region ############# Constructors #############

        #endregion#########

        #region ############## Properties #########################
        /// <summary>
        /// نام فایل تصویری کشور
        /// </summary>
        /// 

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
