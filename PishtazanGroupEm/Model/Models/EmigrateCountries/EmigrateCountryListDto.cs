using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models.EmigrateCountries
{
  public  class EmigrateCountryListDto: BaseEntity<int>
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

        #endregion ##################

        #region  ########################### Methods ################

        #endregion

    }
}
