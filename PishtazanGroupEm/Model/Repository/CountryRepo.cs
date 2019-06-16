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
using Model.Models.CountryCoverImages;
using Model.Models.CountryCover_Images;
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
        public async Task<Tuple<List<CountryCoverImageDto>, List<CountryCoverVideoDto>, CountryCreateDto>> GetEditByIdAsync(int Id)
        {
            Country country = await _context.Countries.FindAsync(Id);
            CountryCreateDto countryDto = Mapper.Map<CountryCreateDto>(country);
            List<CountryCoverImageDto> coverImg =  _unitOfWork.CountryCoverImageRepoUW.GetAsync(c => c.CountryId == Id).Result.ToList();
            List<CountryCoverVideoDto> coverVideo =  _unitOfWork.CountryCoverVideoRepoUW.GetAsync(c => c.CountryId == Id).Result.ToList();




            return new Tuple<List<CountryCoverImageDto>, List<CountryCoverVideoDto>, CountryCreateDto>(coverImg, coverVideo, countryDto);
        }



        /// <summary>
        /// حذف تصاویر و ویدوها و تصویر شاخص کشور از روت سایت
        /// </summary>
        /// <param name="Id">شتاسه کشور</param>
        /// <returns></returns>
        /// تصاویر محتوا حذف نمیشوند
        public async Task  DeleteRootFile(int Id)
        {
            try
            {
           

                Country query = await _context.Countries.FindAsync(Id);

                if (query != null)
                {

                    ///---------------------- حذف تصویر شاخص کشور-------------------

                    ///اگرتصویر شاخص تصویر پیش فرض نبود
                    if (query.IndexImage != "384e5d169c.png")
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
                    IEnumerable<CountryCoverImageDto> countryImage = await _unitOfWork.CountryCoverImageRepoUW.GetAsync(c => c.CountryId == Id);

                    if (countryImage != null)
                    {
                        foreach (var item in countryImage)
                        {
                            ///حذف تصویر شاخص کشور
                            var dirPathImg = Path.Combine(_iHosting.WebRootPath + "\\upload\\country\\images\\" + item.ImageName);
                            File.Delete(dirPathImg);
                        }
                    }

                    ///------------------------------ حذف جموعه ویدو های کشور --------------------------------
                    IEnumerable<CountryCoverVideoDto> CoverVideo = await _unitOfWork.CountryCoverVideoRepoUW.GetAsync(c => c.CountryId == Id);

                    if (CoverVideo != null)
                    {
                        foreach (var item in CoverVideo)
                        {
                            ///حذف تصویر شاخص کشور
                            var dirPathVid = Path.Combine(_iHosting.WebRootPath + "\\upload\\country\\videos\\" + item.VideoName);
                            File.Delete(dirPathVid);
                        }
                    }
                    ///---------------------------------------------------------
                }

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
        /// حذف تصاویر  از دیتابیس
        /// </summary>
        /// <param name="Id">شناسه کشور</param>
        /// <returns></returns>
        public async Task DeleteCoverdImage(int Id)
        {

            IEnumerable<CountryCoverImageDto> countryImage = await _unitOfWork.CountryCoverImageRepoUW.GetAsync(c => c.CountryId == Id);

            foreach (var item in countryImage)
            {
                await _unitOfWork.CountryCoverImageRepoUW.DeleteById(item.Id);

            }

            await _unitOfWork.SaveAsync();
        }


        /// <summary>
        /// حذف ویدوها  از دیتابیس
        /// </summary>
        /// <param name="Id">شناسه کشور</param>
        /// <returns></returns>
        public async Task DeleteCoverVideo(int Id)
        {

            IEnumerable<CountryCoverVideoDto> countryImage = await _unitOfWork.CountryCoverVideoRepoUW.GetAsync(c => c.CountryId == Id);

            foreach (var item in countryImage)
            {
                await _unitOfWork.CountryCoverVideoRepoUW.DeleteById(item.Id);

            }

            await _unitOfWork.SaveAsync();
        }


        //todo:چگونگی انتقال متغیر و اطلاعات بین تابع ها در ریپازیتوری
        //انواع متغیر ها        
        #endregion ###########################

    }
}
