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
        Task<Tuple<List<CountryCoverImageDto>, List<CountryCoverVideoDto>, CountryCreateDto>> GetEditByIdAsync(int Id);





        /// <summary>
        /// حذف تصاویر و ویدوها و تصویر شاخص کشور از روت سایت
        /// </summary>
        /// <param name="Id">شتاسه کشور</param>
        /// <returns></returns>
        /// تصاویر محتوا حذف نمیشوند
        Task DeleteRootFile(int Id);



        /// <summary>
        /// حذف تصاویر  از دیتابیس
        /// </summary>
        /// <param name="Id">شناسه کشور</param>
        /// <returns></returns>
        Task DeleteCoverdImage(int Id);




        /// <summary>
        /// حذف ویدوها  از دیتابیس
        /// </summary>
        /// <param name="Id">شناسه کشور</param>
        /// <returns></returns>
        Task DeleteCoverVideo(int Id);






    }
}
