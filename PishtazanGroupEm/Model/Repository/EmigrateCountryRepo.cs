using Model.DAL;
using Model.Models.Countries;
using Model.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Model.Entities;
using Model.Service;

namespace Model.Repository
{
    public class EmigrateCountryRepo : IEmigrateCountryService
    {

        #region ################################ Dependencies ########################################

        private readonly ApplicationDbContext _context;

        private readonly IUnitOfWork _unitOfWork;

        public EmigrateCountryRepo(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }
        #endregion #######################


        #region ############################ Methods ################################################

        /// <summary>
        /// کشورهایی که برای انها نوع مهاجرت وارد نشده است را برمیکرداند
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CountryListDto> CountriesList()
        {
            IQueryable<Country> query = (from co in _context.Countries
                                         join emc in _context.EmigrateCountries on co.Id equals emc.CountryId
                                         select co);
            ///تفاضل دومجموعه را برمیگرداند
            ///کشورهایی که در جدول نوع مهاجرت - کشور ثبت نشده اند
            IQueryable<Country> exceptCo = _context.Countries.Except(query);
            IEnumerable<CountryListDto> exceptCountries = Mapper.Map<IEnumerable<CountryListDto>>(exceptCo);
            return exceptCountries;
        }

        /// <summary>
        /// کشورهایی که برای انها نوع مهاجرت وارد شده است را برمیکرداند
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CountryListDto> CountriesListIndex()
        {
            ///کشورهایی که در جدول نوع مهاجرت - کشور ثبت شده اند --اسامی را منحصر به فرد . غیر تکراری برمیگرداند
            IQueryable<Country> query = (from co in _context.Countries
                                         join emc in _context.EmigrateCountries on co.Id equals emc.CountryId
                                         select co).Distinct();

            /// قبول نمیکند Iquerable  در گنترلر از نوع 
            IEnumerable<CountryListDto> lstCountries = Mapper.Map<IEnumerable<CountryListDto>>(query);
            return lstCountries;
        }












        #endregion ###########################
    }
}

