using Model.Entities;
using Model.Models.Countries;
using Model.Models.CountryCover_Images;
using Model.Models.CountryCoverImages;
using Model.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.UnitOfWork
{
    /// <summary>
    ///  UnitOfWork اینترفیس برای دسترسی کنترلر به  
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// برای جدول کشورها CRUD پیاده سازی عملیات 
        /// </summary>
        CrudAppService<Country, CountryListDto, CountryCreateDto, CountryCreateDto> CountryRepUW { get; }



        /// <summary>
        /// برای جدول تصاویر کشور ها CRUD پیاده سازی عملیات 
        /// </summary>
        CrudAppService<CountryCoverImage,CountryCoverImageDto,CountryCoverImageDto,CountryCoverImageDto> CountryCoverImageRepoUW { get; }


        /// <summary>
        /// برای جدول ویدوهای کشور ها CRUD پیاده سازی عملیات 
        /// </summary>
        CrudAppService<CountryCoverVideo, CountryCoverVideoDto, CountryCoverVideoDto, CountryCoverVideoDto> CountryCoverVideoRepoUW { get; }


        /// <summary>
        ///  ذخیره تغییرات و اطلاعات
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}
