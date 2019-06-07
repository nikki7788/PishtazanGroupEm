using Model.Entities;
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
        /// کشور و مجموع تصایر و ویدوهای کشور متناطر با شناسه دریافتی را برمیکرداند
        /// </summary>
        /// <param name="id">آیدی کشوردریافتی از کنترلر</param>
        /// <returns>یک ویومدل برمیگرداند</returns>
        /// tuple<x,y,...>
        /// اگر خروجی متد را اینگونه در نطر بگیریم میتوانیم چندین خروجی از متد بگیریم
        ///     فقط با متد های غی همزمان کار میکند
        /// که بامتد های غیر همزمان کارنمیکنند از این استفاده میکنیمref , out به جای
        Task<Tuple<List<CountryCoverImage>, List<CountryCoverVideo>, CountryCreateDto>> GetEditByIdAsync(int Id);
    }
}
