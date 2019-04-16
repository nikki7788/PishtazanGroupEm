using AutoMapper;
using Model.DAL;
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
    /// برای هر موجودیت و جدول Crud پیاده سازی 
    /// </summary>
    public class UnitOfWork:IDisposable,IUnitOfWork
    {
        #region ################## Dependencies #####################################
        private readonly ApplicationDbContext _context;
        //private readonly IMapper _mapper;
        public UnitOfWork(ApplicationDbContext context/*,IMapper mapper*/)
        {
            _context = context;
            //_mapper = mapper;
        }

        #endregion #######################



        #region############################# Fields ################################### 

        //نباید باشد readonly  
        private CrudAppService<Country, CountryListDto, CountryCreatDto, CountryCreatDto> _countryRepUW;

     


        #endregion #######################




        #region ############################ properties #############################################
        /// <summary>
        /// کشور ها
        ///   IUnitOfWork پیاده سازی اعضای اینترفیس   
        ///  Country برای کلاس و جدول CRUD پیاده سازی کلاس 
        /// </summary>
        public CrudAppService<Country, CountryListDto, CountryCreatDto, CountryCreatDto> CountryRepUW
        {
            //فقط خواندنی
            get
            {
                if (_countryRepUW == null)
                {
                    _countryRepUW = new CrudAppService<Country, CountryListDto, CountryCreatDto, CountryCreatDto>(_context/*,_mapper*/);
                }
                return _countryRepUW;
            }
        }


        #endregion ##########################



        #region ############## methods #############################

        /// <summary>
        /// متد ذخیره کردن در دیتابیس--
        ///  IUnitOfWork پیاده سازی اعضای اینترفیس
        /// </summary>
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        //public  void Save()
        //{
        //     _context.SaveChanges();
        //}



        /// <summary>
        ///  بعد از اتمام کار کلاس ارتباط با دیتابیس را ازبین میبرد و قطع میکند 
        /// IDisposable  متد مربوط به اینترفیس  
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion ##########################
    }
}
