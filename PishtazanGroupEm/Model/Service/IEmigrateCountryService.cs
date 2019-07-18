using Model.Entities;
using Model.Models.Countries;
using Model.Models.CountryCover_Images;
using Model.Models.CountryCoverImages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Service
{
    /// <summary>
    /// لایه سرویس برای پیاده ساری متد های خاص نوع مهاجرت - کشور
    /// </summary>
    public interface IEmigrateCountryService
    {

        /// <summary>
        /// کشورهایی که برای انها نوع مهاجرت وارد نشده است را برمیکرداند
        /// ///کشورهایی که در جدول نوع مهاجرت - کشور ثبت نشده اند
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        IEnumerable<CountryListDto> CountriesList();


    }
}
