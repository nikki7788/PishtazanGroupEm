using Microsoft.EntityFrameworkCore.Storage;
using Model.DAL;
using Model.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Repository
{
    /// <summary>
    /// مدیریت تراکنش
    /// </summary>
    public class EntityDataBaseTransaction : IEntityDataBaseTransaction
    {
        #region############################### Dependencies ###############################################

        private readonly IDbContextTransaction _transaction;


        /// <summary>
        /// تراکنش را در کانستراکتور مقدار دهی کردیم
        /// بعد از صدا زدن کلاس تراکنش مقدار دهی و شروع میشود
        /// </summary>
        /// <param name="context">نمونه دیتابیس</param>
        public EntityDataBaseTransaction(ApplicationDbContext context)
        {
            _transaction = context.Database.BeginTransaction();
        }

        #endregion##################################################################################

        #region############################### Methods ###############################################

        /// <summary>
        /// اجرای عملیات
        /// </summary>
        public void Commit()
        {
            _transaction.Commit();
        }

        /// <summary>
        /// برکشت عملیات
        /// </summary>
        public void Rollback()
        {
            _transaction.Rollback();
        }


        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _transaction.Dispose();
        }

        #endregion################################################################################

    }
}
