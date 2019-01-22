using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExamWeb.Models;
using ExamWeb.Models.Enums;
using ExamWeb.ViewModels;
using ExamWeb.ViewModels.Receipts;
using ExamWeb.ViewModels.Users;
using SIS.HTTP.Cookies;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using SIS.MvcFramework.Services;

namespace ExamWeb.Controllers
{
   public class UsersController:BaseController
    {
        private readonly IHashService hashService;
        public UsersController(IHashService hashService)
        {
            this.hashService = hashService;
        }
        [HttpGet("/Users/Login")]
        public IHttpResponse Login()
        {
            return this.View("/Users/Login");
        }

        [HttpGet("/Users/Register")]
        public IHttpResponse Register()
        {
            return this.View("/Users/Register");
        }

        [HttpPost("/Users/Register")]
        public IHttpResponse DoRegister(RegisterViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Username) || model.Username.Trim().Length < 4)
            {
                return this.BadRequestError("Please provide valid username with length of 4 or more characters.");
            }

            if (string.IsNullOrWhiteSpace(model.Email) || model.Email.Trim().Length < 4)
            {
                return this.BadRequestError("Please provide valid email with length of 4 or more characters.");
            }

            if (this.Db.Users.Any(x => x.Username == model.Username.Trim()))
            {
                return this.BadRequestError("User with the same name already exists.");
            }

            if (string.IsNullOrWhiteSpace(model.Password) || model.Password.Length < 6)
            {
                return this.BadRequestError("Please provide password of length 6 or more.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return this.BadRequestError("Passwords do not match.");
            }

       
            var hashedPassword = this.hashService.Hash(model.Password);
            var role = Role.User;

            if (!this.Db.Users.Any())
            {
                role = Role.Admin;
            }

    
            var user = new User
            {
                Username = model.Username.Trim(),
               
                 Email = model.Email.Trim(),
                Password = hashedPassword,
                Role = role
            };
            this.Db.Users.Add(user);

            try
            {
                this.Db.SaveChanges();
            }
            catch (Exception e)
            {

                return this.ServerError(e.Message);
            }

        
            return this.Redirect("/Users/Login");
        }

        [HttpPost("/Users/Login")]
        public IHttpResponse DoLogin(LoginViewModel model)
        {
            var hashedPassword = this.hashService.Hash(model.Password);

            var user = this.Db.Users.FirstOrDefault(x => x.Username == model.Username && x.Password == hashedPassword);
            if (user == null)
            {
                return this.BadRequestError("Invalid username or password.");
            }

            var mvcUser = new MvcUserInfo
            {
                Username = user.Username,
                Role = user.Role.ToString(),

            };
            var cookieContent = this.UserCookieService.GetUserCookie(mvcUser);

            var cookie = new HttpCookie(".auth-cakes", cookieContent, 7) { HttpOnly = true };
            this.Response.Cookies.Add(cookie);
            return this.Redirect("/");
        }

        [HttpGet("/Users/Logout")]
        public IHttpResponse Logout()
        {
            if (!this.Request.Cookies.ContainsCookie(".auth-cakes"))
            {
                return this.Redirect("/");
            }

            var cookie = this.Request.Cookies.GetCookie(".auth-cakes");
            cookie.Delete();
            this.Response.Cookies.Add(cookie);
            return this.Redirect("/");
        }

        [Authorize()]
        [HttpGet("/Users/Profile")]
        public IHttpResponse All()
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);
            if (!User.IsLoggedIn)
            {
                return this.View("/Users/Login");
            }
            var receipts = this.Db.Receipts.Where(x => x.Cashier == user)
                .Select(x => new OneReceiptsViewModel
                {
                    Cashier = x.Cashier.Username,
                    Id = x.Id.ToString(),
                    IssuedOn = x.IssuedOn.ToString("dd/MM/yyyy"),
                    Total = x.Orders.Select(y => y.Product.Price * y.Quantity).Sum(),

                }).ToList();
            var model = new AllReceiptsViewModel
            {
                Receipts = receipts
            };
            return this.View("/Users/Profile", model);
        }

    }
}
