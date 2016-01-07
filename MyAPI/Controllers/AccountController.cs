using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using MyAPI.Models;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Web.Http.Cors;
using System.IO;
using System.Text;
using Facebook;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyAPI.Controllers
{
   
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        #region Helper
        public HttpResponseMessage CreateResponse<T>(HttpStatusCode statusCode, T data)
        {
            return Request.CreateResponse(statusCode, data);
        }

        public HttpResponseMessage CreateResponse(HttpStatusCode statusCode)
        {
            return Request.CreateResponse(statusCode);
        }
        #endregion

        private string defaulPassword = "khongcopass";
        private AuthRepository _repo = null;

        public AccountController()
        {
            _repo = new AuthRepository();


         
       

        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _repo.RegisterUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            DAOs.User ctx = new DAOs.User();
            ctx.Scores = 0;
            ctx.Email = userModel.UserName;
            ctx.Name = "tên";
            GLOBAL.db.Users.Add(ctx);
            GLOBAL.db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }



        [AllowAnonymous]
        [Route("getfacebookresponse")]
        public async Task<IHttpActionResult> getFacebookResponse([FromUri]string code)
        {

            
            var facebook = new FacebookGraphAPI(code);
            // Get user profile data
            var data = facebook.GetObject("me", null);
            string email = data["email"].ToString();


            IdentityResult result = await _repo.UpdateUser(email,code);

            IHttpActionResult errorResult = GetErrorResult(result);

            if(errorResult == null)
            {
                return Ok(); }

            else
            {
                UserModel userModel = new UserModel();
                userModel.ConfirmPassword = code;
                userModel.Password = code;
                userModel.UserName = email;

             

                result = await _repo.RegisterUser(userModel);

                 errorResult = GetErrorResult(result);

                if (errorResult != null)
                {
                    return errorResult;
                }

                DAOs.User ctx = new DAOs.User();
                ctx.Scores = 0;
                ctx.Email = userModel.UserName;
                ctx.Name = "tên";
                GLOBAL.db.Users.Add(ctx);
                GLOBAL.db.SaveChanges();

                return Ok();
            }
            
        }

        [Authorize]
        [HttpPost, HttpOptions]
        [Route("addscore")]
        public HttpResponseMessage AddScore([FromBody]string email)
        {
            if (!_repo.IsUserExist(email))
            {
                return CreateResponse(HttpStatusCode.BadRequest);
            }

            DAOs.User user = GLOBAL.db.Users.Single(x => x.Email == email);

            if (user != null)
            {
                user.Scores++;
                GLOBAL.db.SaveChanges();
                return CreateResponse(HttpStatusCode.OK, user.Scores);
            }
            else
            {
                DAOs.User ctx = new DAOs.User();
                ctx.Scores = 0;
                ctx.Email = email;
                ctx.Name = "tên";
                GLOBAL.db.Users.Add(ctx);
                GLOBAL.db.SaveChanges();
                return CreateResponse(HttpStatusCode.OK, 0);
            }
            
        }

        [Authorize]
        [HttpPost, HttpOptions]
        [Route("getprofile")]
        public HttpResponseMessage GetProfile([FromBody]string email)
        {
            if(!_repo.IsUserExist(email))
            {
                return CreateResponse(HttpStatusCode.BadRequest);
            }

            DAOs.User user = GLOBAL.db.Users.Single(x => x.Email == email);

            if (user != null)
            {
                return CreateResponse(HttpStatusCode.OK, user);
            }
            else
            {
                DAOs.User ctx = new DAOs.User();
                ctx.Scores = 0;
                ctx.Email = email;
                ctx.Name = "tên";
                GLOBAL.db.Users.Add(ctx);
                GLOBAL.db.SaveChanges();
                return CreateResponse(HttpStatusCode.OK, ctx);
            }
        }


        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }


            return null;
        }

       
    }
}
