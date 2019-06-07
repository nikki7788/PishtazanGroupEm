using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model.DAL;
using Model.Entities;
using Model.Models.Countries;
using Model.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

//todo:برای فع خطا روی جوین زدن باید 
//using System.Linq;
//وارد شود
namespace Model.Repository
{

    /// <summary>
    /// متد ها ولایهسرویس خاص کشور
    /// </summary>
    public class CountryRepo : ICountryService
    {

        #region ################################ Dependencies ########################################

        private readonly ApplicationDbContext _context;

        public CountryRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion #######################


        #region ############################ Methods ################################################



        /// <summary>
        /// کشور و مجموع تصایر و ویدوهای کشور متناطر با شناسه دریافتی را برمیکرداند
        /// </summary>
        /// <param name="id">آیدی کشوردریافتی از کنترلر</param>
        /// <returns>یک ویومدل برمیگرداند</returns>
        /// tuple<x,y,...>
        /// اگر خروجی متد را اینگونه در نطر بگیریم میتوانیم چندین خروجی از متد بگیریم
        ///     فقط با متد های غی همزمان کار میکند
        /// که بامتد های غیر همزمان کارنمیکنند از این استفاده میکنیمref , out به جای
        public async Task<Tuple<List<CountryCoverImage>, List<CountryCoverVideo>, CountryCreateDto>> GetEditByIdAsync(int Id)
        {
            Country country = await _context.Countries.FindAsync(Id);
            CountryCreateDto countryDto = Mapper.Map<CountryCreateDto>(country);

            var coverImg =await (from c in _context.Countries
                         join co in _context.CountryCoverImages
                         on c.Id equals co.CountryId
                         where c.Id == Id
                         select co).ToListAsync();
            var coverVideo =await (from c in _context.Countries
                         join co in _context.CountryCoverVideos
                         on c.Id equals co.CountryId
                         where c.Id == Id
                         select co).ToListAsync();

            return new Tuple<List<CountryCoverImage>, List<CountryCoverVideo>, CountryCreateDto>(coverImg, coverVideo, countryDto);
        }

        #endregion ###########################

    }
}
