using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationDbContext:IdentityDbContext<ApplicationUsers,ApplicationRoles,string>
    {

    }
}
