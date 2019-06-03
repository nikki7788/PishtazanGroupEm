using Model.Models.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Service
{
    /// <summary>
    /// لایه سرویس برای پیاده ساری متد های خاص کشور
    /// </summary>
   public interface ICountryService
    {
        /// <summary>
        /// کشور متناطر با شناسه دریافتی را برای ویرایش برمیکرداند
        /// </summary>
        /// <param name="Id">شناسه کشور</param>
        /// <returns></returns>
         Task<CountryCreateDto> GetEditByIdAsync(int Id);
    }
}
