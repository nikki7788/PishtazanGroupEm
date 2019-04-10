using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseEntity<TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        public TKey Id { get; set; }
        
    }
    //public abstract class BaseEntity : BaseEntity<int>
    //{

    //}
}
