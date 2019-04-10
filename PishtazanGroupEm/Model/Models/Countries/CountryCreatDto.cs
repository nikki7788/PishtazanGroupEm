using AutoMapper;
using Model.Entities;
using Model.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.Countries
{
    /// <summary>
    /// ویومدل کشورها
    /// </summary>
    public class CountryCreatDto : BaseEntity<int>
    {

        #region ####### properties ####################

        /// <summary>
        /// نام کشور
        /// </summary>
        public string Name { get; set; }


        ///// <summary>
        /// توضیحات کشور
        /// </summary>
        public string Description { get; set; }



        #endregion ###########

        #region ######################## Navigation properties ######################

        //todo:روش صرف نظر گردن از نویگیشن پراپرتی ها
        public virtual ICollection<CountryCoverImage> CountryCoverImages { get; set; }

        public virtual ICollection<CountryCoverVideo> CountryCoverVideos { get; set; }

        public virtual ICollection<EmigrateCountry> EmigrateCountries { get; set; }

        #endregion########################

    }
}
//todo:ولیدیشن ها مانده
//ایا به نویگیشن پراپرتی ها نیاز است؟اگر نگذاریم خطا میدهد هنگام مپینگ
//روش نادیده گرفتن پراپرتی هایی که در کلاس اصلی وجود دارد ولی منادر ویوندل نمیخاهیم ایجاد کنیم