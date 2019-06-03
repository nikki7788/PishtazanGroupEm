using Model.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{

    /// <summary>
    /// 
    /// </summary>
    public class CountryRepo
    {

        #region ################################ Dependencies ########################################

        private readonly ApplicationDbContext _context;

        public CountryRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion #######################


        #region ############################ Methods ################################################






        #endregion ###########################

    }
}
