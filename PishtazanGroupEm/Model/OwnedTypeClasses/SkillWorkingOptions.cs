using Microsoft.EntityFrameworkCore;
using Model.PublicClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Model.OwnedTypeClasses
{

    /// <summary>
    /// جدول هزینه انجام  گزینه هاوخدمت های اختیاری برای مهاجرت کاری
    /// این هزینه هزینه انجام خدمت توسط موسسه است  و برای تمام کشورها یکسان است
    /// </summary>
    ///هزینه  انجام هر خدمت برای تمام کشور ها یکسان است
    ///این هزینه هزینه انجام خدمت توسط موسسه است  
    ///  است که در کلاس و جدول کشور برای  پراپرتی مورد نظراز  نوع این کلاس تعریف میشود complexType جدول از نوع    
   // [ComplexType]
    [Owned]
    public class SkillWorkingOptions
    {

        #region ############# Constructors #############
        public SkillWorkingOptions()
        {
         
        }
        #endregion#########

        #region ############## Properties #########################
        /// <summary>
        /// هزینه
        /// cv رزومه 
        /// </summary>

        //[Required(ErrorMessage = PublicConst.EnterMessage)]
        [Range(0, 1000000, ErrorMessage = PublicConst.RangeMessage)]
        [Display(Name = " CV هزینه")]
        public int MakingCVPrice { get; set; } 



        /// <summary>
        /// هزینه
        /// cover letter ساخت کاورلتر
        /// </summary>
       // [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [Range(0, 1000000, ErrorMessage = PublicConst.RangeMessage)]
        [Display(Name = " Cover letter هزینه")]
        public int MakingCoverLetterPrice { get; set; } 


        /// <summary>
        /// هزینه
        /// ساخت لینکدین
        /// </summary>
       // [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [Range(0, 1000000, ErrorMessage = PublicConst.RangeMessage)]
        [Display(Name = " LinkedIn  هزینه ساخت")]
        public int MakingLinkedInPrice { get; set; } 


        /// <summary>
        /// هزینه
        /// پیداکردن شغل مناسب و فرستادن رزومه
        /// </summary>
        //[Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [Range(0, 1000000, ErrorMessage = PublicConst.RangeMessage)]
        [Display(Name = "  هزینه یافتن شغل و فرستادن رزومه")]
        public int FindingJobCVPrice { get; set; } 


        #endregion##################################
    }
}
