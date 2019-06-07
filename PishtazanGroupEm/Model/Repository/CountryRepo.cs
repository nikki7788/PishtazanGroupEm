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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Model.UnitOfWork;
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

        private readonly IHostingEnvironment _iHosting;

        private readonly IUnitOfWork _unitOfWork;

        public CountryRepo(ApplicationDbContext context, IHostingEnvironment iHosting
            , IUnitOfWork unitOfWork)
        {
            _context = context;

            _iHosting = iHosting;

            _unitOfWork = unitOfWork;
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

            var coverImg = await (from c in _context.Countries
                                  join co in _context.CountryCoverImages
                                  on c.Id equals co.CountryId
                                  where c.Id == Id
                                  select co).ToListAsync();
            var coverVideo = await (from c in _context.Countries
                                    join co in _context.CountryCoverVideos
                                    on c.Id equals co.CountryId
                                    where c.Id == Id
                                    select co).ToListAsync();

            return new Tuple<List<CountryCoverImage>, List<CountryCoverVideo>, CountryCreateDto>(coverImg, coverVideo, countryDto);
        }



        /// <summary>
        /// حذف تصاویر و ویدوها و تصویر شاخص کشور از روت سایت
        /// </summary>
        /// <param name="Id">شتاسه کشور</param>
        /// <returns></returns>
        /// تصاویر محتوا حذف نمیشوند
        public async Task<Tuple<IQueryable<CountryCoverImage>, IQueryable<CountryCoverVideo>>>
            DeleteRootFile(int Id)
        {
            try
            {
                IQueryable<CountryCoverImage> covImg = null;
                IQueryable<CountryCoverVideo> coVid = null;
                
                Country query = await _context.Countries.FindAsync(Id);

                if (query != null)
                {

                    ///---------------------- حذف تصویر شاخص کشور-------------------
                    
                    ///اگرتصویر شاخص تصویر پیش فرض نبود
                    if ( query.IndexImage != "384e5d169c.png")
                    {
                        
                        string fileName = query.IndexImage;

                        ///حذف تصویر عادی شاخص کشور
                        var dirPathNor = Path.Combine(_iHosting.WebRootPath + "\\upload\\country\\indexImage\\normalImage\\" + fileName);
                        File.Delete(dirPathNor);

                        ///حذف تصویر شاخص کشور بند انگشتی 
                        var dirPathThumb = Path.Combine(_iHosting.WebRootPath + "\\upload\\country\\indexImage\\thumbnailImage\\" + fileName);
                        File.Delete(dirPathThumb);
                    }

                    ///------------------ حذف مجموعه تصاویر کشور ----------------
                    var coverImg = (from c in _context.Countries
                                    join co in _context.CountryCoverImages
                                    on c.Id equals co.CountryId
                                    where c.Id == Id
                                    select co);
                    covImg = coverImg;
                    if (coverImg != null)
                    {
                        foreach (var item in coverImg)
                        {
                            ///حذف تصویر شاخص کشور
                            var dirPathImg = Path.Combine(_iHosting.WebRootPath + "\\upload\\country\\images\\" + item.ImageName);
                            File.Delete(dirPathImg);
                        }
                    }

                    ///------------------------------ حذف جموعه ویدو های کشور --------------------------------
                    IQueryable<CountryCoverVideo> coverVideo = (from c in _context.Countries
                                                                join co in _context.CountryCoverVideos
                                                                on c.Id equals co.CountryId
                                                                where c.Id == Id
                                                                select co);
                    coVid = coverVideo;

                    if (coverVideo != null)
                    {
                        foreach (var item in coverVideo)
                        {
                            ///حذف تصویر شاخص کشور
                            var dirPathVid = Path.Combine(_iHosting.WebRootPath + "\\upload\\country\\videos\\" + item.VideoName);
                            File.Delete(dirPathVid);
                        }
                    }
                    ///---------------------------------------------------------



                }
                return new Tuple<IQueryable<CountryCoverImage>, IQueryable<CountryCoverVideo>>(covImg, coVid);

            }
            catch (ArgumentNullException ex)
            {

                throw ex;
            }
            catch (ArgumentException ex)
            {

                throw ex;
            }
            catch (DirectoryNotFoundException ex)
            {

                throw ex;
            }
            catch (PathTooLongException ex)
            {

                throw ex;
            }
            catch (IOException ex)
            {

                throw ex;
            }
            catch (NotSupportedException ex)
            {

                throw ex;
            }

            catch (UnauthorizedAccessException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }




        /// <summary>
        /// حذف تصاویر و ویدوهای کشور از دیتابیس
        /// </summary>
        /// <param name="model">مدل دریافتی جدول تصاویر و ویدوها</param>
        /// countrycoverimage - countrycvoervideo
        /// <returns></returns>
        public async Task DeleteCoverVideoAndImage(Tuple<IQueryable<CountryCoverImage>,
            IQueryable<CountryCoverVideo>> model)
        {

            var covImg =await model.Item1.ToListAsync();
            var covVid =await model.Item2.ToListAsync();
             foreach (var coverImg in covImg)
            {
                ///  countryCoverImage حذف فایل تصویر کشور از جدول مجموعه تصاویر
                await _unitOfWork.CountryCoverImageRepoUW.DeleteById(coverImg.Id);

                //if (_context.Entry(coverImg).State == EntityState.Detached)
                //{
                //    _context.Attach(coverImg);
                //}
                //_context.Remove(coverImg);

                await _unitOfWork.SaveAsync();
            }

            foreach (var coverVideo in covVid)
            {
                ///  countryCoverVideo حذف ویدوهای کشور از جدول مجموعه تصاویر
               await _unitOfWork.CountryCoverVideoRepoUW.DeleteById(coverVideo.Id);
            
                await _unitOfWork.SaveAsync();

            }
        }

        //todo:چگونگی انتقال متغیر و اطلاعات بین تابع ها در ریپازیتوری
        //انواع متغیر ها        
        #endregion ###########################

    }
}
