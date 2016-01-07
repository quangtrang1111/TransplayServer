using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace MyAPI.Models
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {

        public AuthContext()
            : base("AuthContext")
        {
           
        }
      
    }
}