using Model.ComplexTypeClasses;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.Countries
{
    /// <summary>
    /// ویو مدل برای نمایش لیست کشور ها
    /// </summary>
    public class CountryListDto:BaseEntity<int>
    {


        /// <summary>
        /// نام کشور
        /// </summary>
        public string Name { get; set; }




        /// <summary>
        /// توضیحات کشور
        /// </summary>
        public string Description { get; set; }



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
        public SkillWorkingOptions SkillWorkingOption { get; set; }




        /// <summary>
        ///  هزینه انجام  گزینه هاوخدمت های اختیاری برای مهاجرت توریستی
        /// این هزینه هزینه انجام خدمت توسط موسسه است نه هزینه خدمت
        /// </summary>
        ///هزینه  انتخاب هر خدمت برای هرکشور متفاوت است
        ///این هزینه هزینه انجام خدمت توسط موسسه است نه هزینه خدمت 
        public TouristOptions TouristOption { get; set; }
    }
}
//todo:ولیدیشن ها نیاز ندارد لیست را نمایش میدهد
//ایا به نویگیشن پراپرتی ها نیاز است؟