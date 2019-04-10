using Model.Entities;
using Model.Models.Countries;
using Model.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.UnitOfWork
{
    /// <summary>
    ///  UnitOfWork اینترفیس برای دسترسی کنترلر به  
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// برای جدول کشورها CRUD پیاده سازی عملیات 
        /// </summary>
        CrudAppService<Country, CountryListDto, CountryCreatDto, CountryCreatDto> CountryRepUW { get; }

        /// <summary>
        ///  ذخیره تغییرات و اطلاعات
        /// </summary>
        /// <returns></returns>
        Task Save();
    }
}
