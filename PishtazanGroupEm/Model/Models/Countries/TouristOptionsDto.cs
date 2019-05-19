using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models.Countries
{
    //[ComplexType]
    [Owned]
    class TouristOptionsDto
    {
        /// <summary>
        /// جدول هزینه انجام  گزینه هاوخدمت های اختیاری برای مهاجرت توریستی
        /// این هزینه هزینه انجام خدمت توسط موسسه است نه هزینه خدمت
        /// </summary>
        ///هزینه  انتخاب هر خدمت برای هرکشور متفاوت است
        ///این هزینه هزینه انجام خدمت توسط موسسه است نه هزینه خدمت 
        ///  است که در کلاس و جدول کشور برای  پراپرتی مورد نظراز  نوع این کلاس تعریف میشود complexType جدول از نوع    

        #region ############# Constructors #############
        public TouristOptionsDto()
        {

        }
        #endregion#########

        #region ############## Properties #########################
        /// <summary>
        /// هزینه
        /// رزرو هتل
        /// </summary>

        [Display(Name = "  هزینه رزرو هتل")]
        public int BookingHotel { get; set; } = 0;

        /// <summary>
        /// هزینه
        /// گرفتن دعوت نامه
        /// </summary>

        [Display(Name = "  هزینه گرفتن دعوتنامه")]
        public int TakingInvitation { get; set; } = 0;


        /// <summary>
        /// هزینه
        /// گرفتن بلیط قطار
        /// </summary>

        [Display(Name = "   هزینه گرفتن بلیط قطار")]
        public int TakingTrainTicket { get; set; } = 0;


        /// <summary>
        /// هزینه
        /// رزرو هواپیما
        /// </summary>

        [Display(Name = " هزینه رزرو هواپیما")]
        public int BookingPlane { get; set; } = 0;


        /// <summary>
        /// هزینه
        /// گرفتن وقت مصاحبه سفارت
        /// </summary>

        [Display(Name = "  هزینه گرفتن وقتن مصاحبه")]
        public int TakingEmbassyInterview { get; set; } = 0;


        /// <summary>
        /// هزینه
        /// برنامه ریزی سفر
        /// </summary>

        [Display(Name = " هزینه برنامه ریزی سفر")]
        public int TravelArrangment { get; set; } = 0;

        #endregion##################################

    }
}
