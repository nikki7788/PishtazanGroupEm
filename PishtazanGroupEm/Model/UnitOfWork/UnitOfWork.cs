using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Model.DAL;
using Model.Entities;
using Model.Models.Countries;
using Model.Models.CountryCover_Images;
using Model.Models.CountryCoverImages;
using Model.Repository;
using Model.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.UnitOfWork
{
    /// <summary>
    /// برای هر موجودیت و جدول Crud پیاده سازی 
    /// </summary>
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        #region ################## Dependencies #####################################
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _iHosting;

        //private readonly IMapper _mapper;

        public UnitOfWork(ApplicationDbContext context, IHostingEnvironment iHosting/*,IMapper mapper*/)
        {
            _context = context;
            _iHosting = iHosting;
            //_mapper = mapper;
        }

        #endregion #######################



        #region############################# Fields ################################### 

        //نباید باشد readonly  

        private CrudAppService<Country, CountryListDto, CountryCreateDto, CountryCreateDto> _countryRepUW;

        private CrudAppService<CountryCoverImage, CountryCoverImageDto, CountryCoverImageDto, CountryCoverImageDto> _countryCoverImageRepoUW;


        private CrudAppService<CountryCoverVideo, CountryCoverVideoDto, CountryCoverVideoDto, CountryCoverVideoDto> _countryCoverVideoRepoUW;




        #endregion #######################




        #region ############################ properties #############################################
  
        
        /// <summary>
        /// کشور ها
        ///   IUnitOfWork پیاده سازی اعضای اینترفیس   
        ///  Country برای کلاس و جدول CRUD پیاده سازی کلاس 
        /// </summary>
        public CrudAppService<Country, CountryListDto, CountryCreateDto, CountryCreateDto> CountryRepUW
        {
            //فقط خواندنی
            get
            {
                if (_countryRepUW == null)
                {
                    _countryRepUW = new CrudAppService<Country, CountryListDto, CountryCreateDto, CountryCreateDto>(_context, _iHosting/*,_mapper*/);
                }
                return _countryRepUW;
            }
        }




        /// <summary>
        /// تصاویر کشور ها
        ///   IUnitOfWork پیاده سازی اعضای اینترفیس   
        ///  CountryCoverImage برای کلاس و جدول CRUD پیاده سازی کلاس 
        /// </summary>
        public CrudAppService<CountryCoverImage, CountryCoverImageDto, CountryCoverImageDto, CountryCoverImageDto> CountryCoverImageRepoUW
        {
            //فقط خواندنی
            get
            {
                if (_countryCoverImageRepoUW == null)
                {
                    _countryCoverImageRepoUW = new CrudAppService<CountryCoverImage, CountryCoverImageDto, CountryCoverImageDto, CountryCoverImageDto>(_context, _iHosting/*,_mapper*/);
                }
                return _countryCoverImageRepoUW;
            }
        }





        /// <summary>
        /// ویدو های کشور ها
        ///   IUnitOfWork پیاده سازی اعضای اینترفیس   
        ///  CountryCovervideo برای کلاس و جدول CRUD پیاده سازی کلاس 
        /// </summary>
        public CrudAppService<CountryCoverVideo, CountryCoverVideoDto, CountryCoverVideoDto, CountryCoverVideoDto> CountryCoverVideoRepoUW
        {
            //فقط خواندنی
            get
            {
                if (_countryCoverVideoRepoUW == null)
                {
                    _countryCoverVideoRepoUW = new CrudAppService<CountryCoverVideo, CountryCoverVideoDto, CountryCoverVideoDto, CountryCoverVideoDto>(_context, _iHosting/*,_mapper*/);
                }
                return _countryCoverVideoRepoUW;
            }
        }




        /// <summary>
        ///مدیریت تراکنش
        ///   IUnitOfWork پیاده سازی اعضای اینترفیس   
        ///  Transaction پیاده سازی لایه سرویس 
        /// </summary>
        public IEntityDataBaseTransaction BeginTransaction()
        {
            return new EntityDataBaseTransaction(_context);
        }


        #endregion ##########################



        #region ############## methods #############################

        /// <summary>
        /// متد ذخیره کردن در دیتابیس--
        ///  IUnitOfWork پیاده سازی اعضای اینترفیس
        /// </summary>
        public async Task SaveAsync()
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
        public void Dispose() => _context.Dispose();
        //public void Dispose()
        //{
        //    _context.Dispose();
        //}

        #endregion ##########################
    }
}
