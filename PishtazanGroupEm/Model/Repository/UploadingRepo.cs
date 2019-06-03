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


        private readonly IHostingEnvironment _appEnvironment;
        public UploadingRepo(IHostingEnvironment appEnvironment)
        {
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
                    catch (NotSupportedException ex)
                    {

                        throw ex;
                    }
                    catch (SecurityException ex)
                    {

                        throw ex;
                    }
                    catch (FileNotFoundException ex)
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
                    catch (ArgumentNullException ex) 
                    {

                        throw ex;
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
            catch (ArgumentNullException ex)
            {

                throw ex;
            }
            catch (ArgumentException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {
                //ModelState.AddModelError("UserImage", "خطایی رخ داده است");
                throw ex;
            }


        }
        #endregion#####################
    }
}
