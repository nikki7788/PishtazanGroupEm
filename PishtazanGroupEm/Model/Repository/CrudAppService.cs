using Microsoft.EntityFrameworkCore;
using Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper.QueryableExtensions;
using System.Threading.Tasks;
using AutoMapper;
using Model.Infrastructure;

namespace Model.Repository
{
    /// <summary>
    /// کلاس مشترک عملیات 
    /// ویرایش - ایجاد-خواندن -حذف
    /// </summary>
    /// <typeparam name="TEntity">نام جدول</typeparam>
    /// <typeparam name="TEntityDto">نام ویومدل لیست </typeparam>
    /// این ویو مدل برای همه حالتن های ویرایش و ایجاد و لیست اطلاعات استفاده میشود اگر همه ویومدل ها پاپرتی های یکسان داشته باشند
    /// <typeparam name="TCreateDto">نام ویو مدل ایجاد   </typeparam>
    /// اگر باویو مدل ویرایش یکی بود برای هر دو یکی ویو مدل ایجاد میکنیم و نام آن را در هردو مینویسیم
    /// اگر سه ویو مدل یکی بودند ویو مدل لیست را به جای هر سه مینویسیم
    /// <typeparam name="TEditDto">نام ویومدل ویرایش </typeparam>
    public class CrudAppService<TEntity, TEntityDto, TCreateDto, TEditDto>
        where TEntity : class
        where TEntityDto : class
        where TCreateDto : class
        where TEditDto : class

    {

        #region############################### Dependencies ###############################################

        private readonly ApplicationDbContext _context;

        private DbSet<TEntity> _table;    //e.g. _context.categories....

        //private readonly IMapper _mapper;


        public CrudAppService(ApplicationDbContext context)
        {
            _context = context;
            _table = context.Set<TEntity>();

        }
        //public CrudAppService(IMapper mapper) 
        //{
        //    _mapper = mapper;

        //}

        #endregion##################################################################################


        /// <summary>
        ///   ایجاد یک رکورد جدید
        /// </summary>
        /// <param name="entityDto">ویو مدل ایجاد اگر وجود داشت</param>
        /// اگر ویو مدل ایجاد وجود نداشت از ویو مدل لیست استفاه میکنیم
        /// <returns></returns>
        public virtual async Task CreateAsync(TCreateDto entityDto)
        {
            //BaseDto<TCreateDto, TEntity> baseDto = new BaseDto<TCreateDto, TEntity>();
            //TEntity entity = baseDto.ToEntity();

            TEntity entity = Mapper.Map<TEntity>(entityDto);
            await _table.AddAsync(entity);

        }

        //public virtual void Create(TEntity entity)
        //{
        //    _table.Add(entity);
        //}





        /// <summary>
        /// آپدیت یک رکورد
        /// </summary>
        /// <param name="entityDto">ویومدل ویرایش اگر وجود داشت</param>
        /// اگر وجودنداشت از ویو مدل ایجاد و اگر ایجاد وجود نداشت از لیست
        public virtual void Update(TEditDto entityDto)
        {
            var entity = Mapper.Map<TEntity>(entityDto);
            _table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }





        /// <summary>
        /// یک رکورد مربوط ای دی مورد نظر را برمیکرداند
        /// </summary>
        /// <param name="id">آیدی دریافتی از کنترلر</param>
        /// <returns>یک ویومدل برمیگرداند</returns>
        public virtual async Task<TEntityDto> GetByIdAsync(object id)
        {

            var entity = await _table.FindAsync(id);

            //var entityDto = BaseDto<TEntityDto,TEntity>.FromEntity(entity);

            //TEntityDto entityDto = BaseDto<TEntityDto, TEntity>.FromEntity(entity);
            var entityDto = Mapper.Map<TEntityDto>(entity);
            return entityDto;
        }







        /// <summary>
        /// لیستی  از کورد ها را میاورد
        /// </summary>
        /// <param name="whereIf"></param>
        /// <param name="orderByIf"></param>
        /// <param name="joinString"></param>
        /// <returns>یک لییستی از ویو مدل برمیگرداند</returns>
        public virtual async Task<IEnumerable<TEntityDto>> GetAsync(Expression<Func<TEntity, bool>> whereIf = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderByIf = null,
           string joinString = "")
        {

            IQueryable<TEntity> query = _table;

            if (whereIf != null)
            {
                query = query.Where(whereIf);
            }

            if (orderByIf != null)
            {

                query = orderByIf(query);
            }

            if (joinString != "")
            {
                foreach (var item in joinString.Split(','))
                {
                    //مانند جوین عمل میکند
                    //همه اطلاعات  به یکباره میاورد
                    //eager loading
                    query = query.Include(item);
                }

            }
            //لیستی از ویو مدل نمایش لیست برمیکرداند
            IEnumerable<TEntityDto> entityDto = Mapper.Map<IEnumerable<TEntityDto>>(await query.ToListAsync());
            return entityDto;
            //todo:ویو مدل های ورودی متد را اگه نیاز بود باید تغییر نوع بدهم
        }







        /// <summary>
        /// حذف یک کورد
        /// مثلا میگوییم دسته بندی فوتبال را حذف کن
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(TEntity entity)
        {
            //var entry = _context.Entry(entity);

            //if (entry.State == EntityState.Detached)
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _table.Attach(entity);
            }
            _table.Remove(entity);
        }


        /// <summary>
        /// حذف یک رکورد براساس آیدی
        /// </summary>
        /// <param name="id"></param>
        public virtual async Task DeletById(object id)
        {
            TEntityDto entityDto = await GetByIdAsync(id);
            TEntity entity = Mapper.Map<TEntity>(entityDto);
            Delete(entity);

        }







        /// <summary>
        /// ذخیره اطلاعات و تغییرات در دیتابیس
        /// </summary>
        public virtual async Task SaveAsync()
        {
            //todo: را اینجا بنویسیم یا درکنترلر کدام بهتر است؟ try catch 
            await _context.SaveChangesAsync();
        }
        //public virtual async void Save()
        //{
        //    await _context.SaveChangesAsync();
        //}
    }
}

//todo:روش بهتر برای مپینگ اگر زمان بود