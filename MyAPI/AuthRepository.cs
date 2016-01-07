using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyAPI
{
    public class AuthRepository : IDisposable
    {
        private AuthContext _ctx;

        private UserManager<IdentityUser> _userManager;

        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
            
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<IdentityResult> UpdateUser(string email, string newPass)
        {
            IdentityUser user = await _userManager.FindByNameAsync(email);

            if(user == null)
            {
                return new IdentityResult("User chưa có");
            }
            var result = await _userManager.RemovePasswordAsync(user.Id);

            result = await _userManager.AddPasswordAsync(user.Id, newPass);

            return result;
        }


        public bool IsUserExist(string email)
        {
            IdentityUser user =  _userManager.FindByName(email);

            if(user != null)
            {
                return true;
            }
            return false;

        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

       

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}