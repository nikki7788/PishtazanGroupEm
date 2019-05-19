using Model.Entities;
using Model.OwnedTypeClasses;
using Model.PublicClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models.Countries
{
    /// <summary>
    /// ویومدل کشورها
    /// </summary>
    public class CountryCreateDto : BaseEntity<int>
    {


        #region ####### properties ####################

        /// <summary>
        /// نام کشور
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(250, MinimumLength = 2, ErrorMessage = PublicConst.LengthMessage)]
        [RegularExpression(@"[0-9A-Zا-ی ء ؤ ئ ةأإآa-z_\s\-\(\)\.]+", ErrorMessage = PublicConst.DangrouseMessageForBadCharachter)]
        [Display(Name = "نام کشور")]
        public string Name { get; set; }




        /// <summary>
        /// چکیده خلاصه و توضیحات درمورد کشور
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(500, MinimumLength = 3, ErrorMessage = PublicConst.LengthMessage)]
        //[RegularExpression(@"[0-9A-Zا-ی ء ؤ ئ ةأإآa-z_\s\-\(\)\.]+", ErrorMessage = PublicConst.DangrouseMessageForBadCharachter)]
        [Display(Name = "چکیده توضیحات کشور")]

        public string Abstract { get; set; }







        /// <summary>
        /// توضیحات کشور
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        //[StringLength(5000, MinimumLength = 3, ErrorMessage = PublicConst.LengthMessage)]
        //[RegularExpression(@"[0-9A-Zا-ی ء ؤ ئ ةأإآa-z_\s\-\(\)\.]+", ErrorMessage = PublicConst.DangrouseMessageForBadCharachter)]
        [Display(Name = "توضیحات ")]
        public string Description { get; set; }


        /// <summary>
        /// نام تصویر شاخص کشور
        /// </summary>
        /// تصویر سایز کوچک شاخص هر کشور
        ///  تصویری که برای هر کشور نمایش می دهیم در صفحه اصلی یا جاهای دیگر
        // [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [Display(Name = " تصویر شاخص کشور")]
        public string IndexImage { get; set; }


        /// <summary>
        ///  هزینه انجام  گزینه هاوخدمت های اختیاری برای مهاجرت کاری
        /// این هزینه هزینه انجام خدمت توسط موسسه است  و برای تمام کشورها یکسان است
        /// </summary>
        ///هزینه  انجام هر خدمت برای تمام کشور ها یکسان است
        ///این هزینه هزینه انجام خدمت توسط موسسه است  

        public SkillWorkingOptions SkillWorkingOption { get; set; }



        /// <summary>
        ///  هزینه انجام  گزینه هاوخدمت های اختیاری برای مهاجرت توریستی
        /// این هزینه هزینه انجام خدمت توسط موسسه است نه هزینه خدمت
        /// </summary>
        ///هزینه  انتخاب هر خدمت برای هرکشور متفاوت است
        ///این هزینه هزینه انجام خدمت توسط موسسه است نه هزینه خدمت 
       // [AutoMapper.IgnoreMap]
        public TouristOptions TouristOption { get; set; }



        #endregion ###########

        #region ######################## Navigation properties ######################

        //todo:روش صرف نظر گردن از نویگیشن پراپرتی ها

        public virtual ICollection<CountryCoverVideo> CountryCoverVideos { get; set; }

        public virtual ICollection<CountryCoverImage> CountryCoverImages { get; set; }

        public virtual ICollection<EmigrateCountry> EmigrateCountries { get; set; }

        #endregion########################

    }
    //todo:ولیدیشن ها مانده
    //ایا به نویگیشن پراپرتی ها نیاز است؟اگر نگذاریم خطا میدهد هنگام مپینگ
    //روش نادیده گرفتن پراپرتی هایی که در کلاس اصلی وجود دارد ولی منادر ویومدل نمیخاهیم ایجاد کنیم

    //tod:روش صرف نطر کردن از پراپرتی که نمیخواهیم
    // public TouristOptions TouristOption { get; set; }
}
