using ASP_Task2.Entities;
using ASP_Task2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace ASP_Task2.Controllers
{
	public class HomeController : Controller
	{
		public static List<Product> Products { get; set; } = new List<Product>
		{
			new Product
			{
				ID = 1,
				Name = "Test",
				Description = "Test",
				Discount = 10,
				Price = 3,
				Image = "https://onapples.com/uploads/varieties/3y3v9tyf8h96.png",
			},

			new Product
			{
				ID = 2,
				Name = "Apple",
				Description = "an apple.",
				Discount = 15,
				Price = 2,
				Image = "https://onapples.com/uploads/varieties/3y3v9tyf8h96.png",
			},
		};

		public IActionResult Index()
		{
			var viewModel = new ProductViewModel
			{
				Products = Products
			};

			return View(viewModel);
		}

		[HttpGet]
		public IActionResult Update(int id)
		{
			id -= 1;

			var product = Products[id];
			var viewModel = new ProductUpdateViewModel
			{
				Product = product
			};

			return View(viewModel);
		}

		[HttpPost]
		public IActionResult Update(ProductUpdateViewModel viewModel, int id)
		{
			id -= 1;

			for (int i = 0; i < Products.Count; i++)
			{
				if (Products[i].ID == viewModel.Product.ID)
				{
					id = Products[i].ID;
					break;
				}
			}

			var product = Products[id];
			product.Name = viewModel.Product.Name;
			product.Description = viewModel.Product.Description;
			product.Discount = viewModel.Product.Discount;
			product.Price = viewModel.Product.Price;
			product.Image = viewModel.Product.Image;

			return RedirectToAction("Index", "Home");
		}

		public IActionResult Delete(int id)
		{
			id -= 1;

			var product = Products[id];
			Products.Remove(product);
			for (int i = id; i < Products.Count; i++)
			{
				Products[i].ID--;
			}

			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public IActionResult Add()
		{
			var viewModel = new ProductAddViewModel
			{
				Product = new Product()
			};

			return View(viewModel);
		}

		[HttpPost]
		public IActionResult Add(ProductAddViewModel viewModel)
		{
			Products.Add(viewModel.Product);
			viewModel.Product.ID = Products.Count;
            return RedirectToAction("Index", "Home");
        }
	}
}
