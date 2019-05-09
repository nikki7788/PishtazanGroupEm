using Model.OwnedTypeClasses;
using Model.PublicClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    /// <summary>
    /// جدول کشورها
    /// </summary>
    [Table("Country")]
    public class Country:BaseEntity<int> 
    {
        #region ############# Constructors #############
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public Country()
        {

        }
        #endregion#########

        #region ############## Properties #########################


        /// <summary>
        /// نام کشور
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(250, MinimumLength = 2, ErrorMessage = PublicConst.LengthMessage)]
      // [RegularExpression(@"[0-9A-Zا-ی ء ؤ ئ ةأإآa-z_\s\-\(\)\.]+", ErrorMessage = PublicConst.DangrouseMessageForBadCharachter)]
        public string Name { get; set; }




        /// <summary>
        /// توضیحات کشور
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        //[StringLength(5000, MinimumLength = 3, ErrorMessage = PublicConst.LengthMessage)]
        //[RegularExpression(@"[0-9A-Zا-ی ء ؤ ئ ةأإآa-z_\s\-\(\)\.]+", ErrorMessage = PublicConst.DangrouseMessageForBadCharachter)]
        public string  Description  { get; set; }



        /// <summary>
        /// نام تصویر شاخص کشور
        /// </summary>
        /// تصویر سایز کوچک شاخص هر کشور
        ///  تصویری که برای هر کشور نمایش می دهیم در صفحه اصلی یا جاهای دیگر
        public string IndexImage { get; set; }


        /// <summary>
        ///  هزینه انجام  گزینه هاوخدمت های اختیاری برای مهاجرت کاری
        /// این هزینه هزینه انجام خدمت توسط موسسه است  و برای تمام کشورها یکسان است
        /// </summary>
        ///هزینه  انجام هر خدمت برای تمام کشور ها یکسان است
        ///این هزینه هزینه انجام خدمت توسط موسسه است  
        ///todo: دارند باید از روی کلاس انها نمونه ساخت و بعد مقدار دهی گرد owned  برای مقدار دهی پراپرتی هایی که ازنوع کلاس هایی که اتریبیوت 
        public SkillWorkingOptions SkillWorkingOption { get; set; }




        /// <summary>
        ///  هزینه انجام  گزینه هاوخدمت های اختیاری برای مهاجرت توریستی
        /// این هزینه هزینه انجام خدمت توسط موسسه است نه هزینه خدمت
        /// </summary>
        ///هزینه  انتخاب هر خدمت برای هرکشور متفاوت است
        ///این هزینه هزینه انجام خدمت توسط موسسه است نه هزینه خدمت 
        public TouristOptions TouristOption { get; set; }

        #endregion#############

        #region #################### Navigation Properties ########################

        public virtual ICollection<creaetcountrydto> CountryCoverImages { get; set; }

        public virtual ICollection<CountryCoverVideo> CountryCoverVideos { get; set; }

        public virtual ICollection<EmigrateCountry> EmigrateCountries { get; set; }

        #endregion ##################

        #region  ########################### Methods ################

        #endregion
    }
}
//todo:ولیدشن اگر نیاز داشتبنویسم.نیاز ندارد
