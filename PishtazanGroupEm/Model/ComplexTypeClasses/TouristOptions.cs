using Microsoft.EntityFrameworkCore;
using Model.PublicClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.ComplexTypeClasses
{
    /// <summary>
    /// جدول هزینه انجام  گزینه هاوخدمت های اختیاری برای مهاجرت توریستی
    /// این هزینه هزینه انجام خدمت توسط موسسه است نه هزینه خدمت
    /// </summary>
    ///هزینه  انتخاب هر خدمت برای هرکشور متفاوت است
    ///این هزینه هزینه انجام خدمت توسط موسسه است نه هزینه خدمت 
    ///  است که در کلاس و جدول کشور برای  پراپرتی مورد نظراز  نوع این کلاس تعریف میشود complexType جدول از نوع    

    //[ComplexType]
    [Owned]
    public class TouristOptions
    {
        #region ############# Constructors #############
        public TouristOptions()
        {

        }
        #endregion#########

        #region ############## Properties #########################
        /// <summary>
        /// هزینه
        /// رزرو هتل
        /// </summary>

        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        public int BookingHotel { get; set; }

        /// <summary>
        /// هزینه
        /// گرفتن دعوت نامه
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        public int TakingInvitation { get; set; }


        /// <summary>
        /// هزینه
        /// گرفتن بلیط قطار
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        public int TakingTrainTicket { get; set; }


        /// <summary>
        /// هزینه
        /// رزرو هواپیما
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        public int BookingPlane { get; set; }


        /// <summary>
        /// هزینه
        /// گرفتن وقت مصاحبه سفارت
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        public int TakingEmbassyInterview { get; set; }


        /// <summary>
        /// هزینه
        /// برنامه ریزی سفر
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        public int TravelArrangment { get; set; }

        #endregion##################################

    }
}
