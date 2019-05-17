using Model.Entities;
using Model.Models.Countries;
using Model.Models.CountryCover_Images;
using Model.Models.CountryCoverImage;
using Model.OwnedTypeClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PishtazanGroupEm.Areas.AdminPanel.Models.ViewModels
{
    /// <summary>
    /// ویو مدل افزودن کشور
    /// </summary>
    ///  برای افزودن کشور و تصاویر و ویدوهای آن همزمان و در یک ویو
    public class CreatCountryMViewModel
    {
       
        
        /// <summary>
        /// افزودن کشور
        /// ویومدل کشورها
        /// </summary>
        public CountryCreateDto Country { get; set; }
        
        
        
        /// <summary>
        /// افزودن مجموعه ای از ویدو ها برای هر کشور
        /// </summary>
        [Display(Name = "ویدوهای کشور")]
        public List<CountryCoverVideoDto> Videos { get; set; }



        /// <summary>
        /// افزودن مجموعه ای از تصاویر برای هر کشور
        /// </summary>
        [Display(Name = " تصاویر کشور")]
        public ICollection<CountryCoverImage> Images { get; set; }








    }
}
