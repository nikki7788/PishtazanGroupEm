using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.PublicClasses
{
    /// <summary>
    /// کلاس عمومی برای مودال ها
    /// </summary>
    public class BootstrapModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public string Arialabelledby { get; set; }



        /// <summary>
        /// متن داخل مودال را دریافت میکند و نمایش می دهد
        /// </summary>
        public string Message { get; set; }



        /// <summary>
        ///small,medium,large
        ///  سایز مودال را تعیین میکند
        /// </summary>
        public ModalSize Size { get; set; }


        /// <summary>
        ///برای مقدار دهی و سویچ کردن بین حالت های مختلف سایز مودال...
        ///</summary>
        ///پراپرتی  بالا = size
        ///این پراپرتی فقط خواندنی است  و فقط مقدار وارد شده برای سایز مودال توسط کاربر را که در پراپرتی بالا گرفته است 
        ///میخواند و روی حالت انتخابی کاربر سوییچ میکند

        public string ModalSizeClass

        {
            get
            {
                switch (Size)
                {
                    
                    ///وقتی از 
                    /// استفاده می کنیم دیگر نیازی به   return
                    ///نیست چون وقتی ریترن اجرا شود دیگر دستور ادامه پیدانمیکند break 
                    ///when we use return ,we don't need break ;because when return is run switch case jumped out of statment
                    ///هر دو یک کار انجام میدهند اینجا
                    ///return "modal-lg" equals return("modal-lg") here
                    case ModalSize.Large:
                        return ("modal-lg");
                        
                    case ModalSize.Small:
                        return ("modal-sm");

                    case ModalSize.Medium:
                    default: return ("");
                        ///به جای نوشتن حالت متوسط میتوانستیم 
                        ///حالت پیش فرض را خالی برگردانیم که یعنی اگرهیچکدام از حالت های دیگر یعنی بزرگ و کوچک نبود حالت پیش فرض اجرا شود
                        ///default:return ("");
                }
            }
        }
    }
}
