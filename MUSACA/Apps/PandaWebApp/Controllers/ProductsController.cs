using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExamWeb.Models;
using ExamWeb.Models.Enums;
using ExamWeb.ViewModels.Products;
using SIS.HTTP.Responses;
using SIS.MvcFramework;

namespace ExamWeb.Controllers
{
    public class ProductsController:BaseController
    {
        [HttpGet("/Products/Create")]
        [Authorize("Admin")]
        public IHttpResponse Create()
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);
            if (!User.IsLoggedIn || user.Role != Role.Admin)
            {
                return this.View("/Users/Login");
            }
            return this.View("/Products/Create");
        }

        [Authorize("Admin")]
        [HttpPost("/Products/Create")]
        public IHttpResponse Create(CreateProductViewModel model)
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);
            if (!User.IsLoggedIn || user.Role != Role.Admin)
            {
                return this.View("/Users/Login");
            }
            var picture = string.Empty;
            if (model.Picture == null)
            {
                picture = "https://pngimage.net/wp-content/uploads/2018/06/icon-product-png-1.png";
            }
            var product = new Product
            {
                Name = model.Name,
                Barcode = model.Barcode,
                Picture = picture,
                Price = model.Price
            };

            this.Db.Products.Add(product);
            this.Db.SaveChanges();
            return this.Redirect("/Products/All");
        }

        [Authorize()]
        [HttpGet("/Products/All")]
        public IHttpResponse All()
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);
            if (!User.IsLoggedIn )
            {
                return this.View("/Users/Login");
            }

            var products = this.Db.Products
                .Select(x => new OneProductViewModel
                {
                    Barcode = x.Barcode,
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    Price = x.Price
                }).ToList();
            var model=new AllProductsListViewModel();
            model.Products = products;

            return this.View("/Products/All", model);
        }


    }
}
