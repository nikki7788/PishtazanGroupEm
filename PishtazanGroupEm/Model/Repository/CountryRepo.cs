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

namespace Model.Repository
{

    /// <summary>
    /// متد ها ولایهسرویس خاص کشور
    /// </summary>
    public class CountryRepo : ICountryService
    {

        #region ################################ Dependencies ########################################

        private  ApplicationDbContext _context;

        public CountryRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion #######################


        #region ############################ Methods ################################################



        /// <summary>
        /// کشور متناطر با شناسه دریافتی را برمیکرداند
        /// </summary>
        /// <param name="id">آیدی کشوردریافتی از کنترلر</param>
        /// <returns>یک ویومدل برمیگرداند</returns>
        public async Task<CountryCreateDto> GetEditByIdAsync(int Id)
        {
            Country country = await _context.Countries.FindAsync(Id);
            CountryCreateDto countryDto = Mapper.Map<CountryCreateDto>(country);


            return countryDto;

        }


        #endregion ###########################

    }
}
