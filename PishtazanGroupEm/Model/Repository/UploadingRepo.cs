using InsertShowImage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Model.DAL;
using Model.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class UploadingRepo : IUploadingService
    {

        #region ################################ Dependencies ########################################

        private readonly ApplicationDbContext _context;

        private readonly IHostingEnvironment _appEnvironment;
        public UploadingRepo(ApplicationDbContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }
        #endregion #######################


        #region ############################ Methods ################################################






        /// <summary>
        /// متد آپلود کردن فایل و تصویر
        /// </summary>
        /// <param name="files">فایل های دریافتی برای آپلود از اکشن</param>
        /// <param name="imagePath">مسیر ذخیره تصویرعادی </param>
        /// <param name="thumbnailImagePath">مسیر ذخیره تصویر بندانگشتی ر صورت نیاز</param>
        /// مه اکشن ها ممکن است به این سایز تصویر نیاز نداشته باشند
        /// <returns><param name="fileName">نام فایل را به عنوان خروجی برمیگرداند</param></returns>
        public async Task<List<string>> UploadFiles(IEnumerable<IFormFile> files, string imagePath, string thumbnailImagePath=null)
        {
            //todo:catch - مدیریت خطا به درستی انجام شود
            //todo:using -try catch - ایا هنگام استفاده از یوزینگ ترای کچ هم نیاز است

            try
            {
                var upload = Path.Combine(_appEnvironment.WebRootPath, imagePath);
                string fileName = "";
                List<string> fileNames = new List<string>();

                foreach (var file in files)
                {

                    fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                    fileNames.Add(fileName);
                    try
                    {
                        using (var fileStream = new FileStream(Path.Combine(upload, fileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                    catch (NotSupportedException)
                    {

                        throw;
                    }
                    catch (SecurityException)
                    {

                        throw;
                    }
                    catch (FileNotFoundException)
                    {

                        throw;
                    }
                    catch (DirectoryNotFoundException)
                    {

                        throw;
                    }
                    catch (PathTooLongException)
                    {

                        throw;
                    }

                    catch (IOException ex)
                    {

                        throw ex;
                    }
                    catch (ArgumentNullException)
                    {

                        throw;
                    }

                    ////---------------------- تغییر سایز عکس و ذخیره برای حالت  بندانگشتی ----------------------------////

                    //اگرتصویر بند انگشتی نیاز بود
                    if (thumbnailImagePath != null)
                    {
                        ImageResizer imgThumb = new ImageResizer();
                        imgThumb.Resize(upload + fileName, Path.Combine(_appEnvironment.WebRootPath, thumbnailImagePath) + fileName);
                    }

                    //----------------------------------------------//
                }
                //نام تصویر اپلود شده را برمیگردند
                //return fileName;
                return fileNames;

            }
            catch (ArgumentNullException)
            {

                throw;
            }
            catch (ArgumentException)
            {

                throw;
            }
            catch (Exception)
            {
                //ModelState.AddModelError("UserImage", "خطایی رخ داده است");
                throw;
            }


        }
        #endregion#####################
    }
}
