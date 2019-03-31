using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationDbContext:IdentityDbContext<ApplicationUsers,ApplicationRoles,string>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option):base(option)
        {

        }

    }
}
