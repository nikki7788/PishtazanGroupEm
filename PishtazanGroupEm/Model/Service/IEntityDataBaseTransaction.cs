using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Service
{
    /// <summary>
    /// لایه سرویس تراکنش
    /// </summary>
    public interface IEntityDataBaseTransaction : IDisposable
    {
        /// <summary>
        /// اجرای عملیات
        /// </summary>
        void Commit();


        /// <summary>
        /// رگشت عملیات
        /// </summary>
        void Rollback();


    }
}
