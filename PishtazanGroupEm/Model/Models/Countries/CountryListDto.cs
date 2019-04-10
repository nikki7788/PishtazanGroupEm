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
    }
}
//todo:ولیدیشن ها نیاز ندارد لیست را نمایش میدهد
//ایا به نویگیشن پراپرتی ها نیاز است؟