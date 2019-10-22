using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
	[Authorize]
		public class HomeController : Controller
	{
		private IEmployee _employee;
		private readonly IHostingEnvironment hostingEnvironment;

		public HomeController(IEmployee employee, IHostingEnvironment hostingEnvironment)
		{
			_employee = employee;
			this.hostingEnvironment = hostingEnvironment;
		}
		private string ProcessUploadedFile(CreateViewModel model)
		{
			string uniqueFileName = null;

			if (model.Photo != null)
			{
				string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
				uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
				string filePath = Path.Combine(uploadsFolder, uniqueFileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					model.Photo.CopyTo(fileStream);
				}
			}

			return uniqueFileName;
		}
		public IActionResult Index([FromQuery(Name ="page")] int? page, [FromQuery(Name ="pageSize")]int pageSize = 2)
		{
			ViewBag.currentPage = page ?? 1;
			IEnumerable<Employee> model = _employee.GetEmployees(page??1, pageSize);
						return View(model);
		}

		public IActionResult Details(int Id)
		{
			Employee model = _employee.GetEmployee(Id);
			if (model == null)
			{
				Response.StatusCode = 404;
				return View("EmployeeNotFound", Id);
			}
			return View(model);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpGet]
		public IActionResult Delete(int Id)
		{
			Employee model = _employee.GetEmployee(Id);
			return View(model);
		}
		[HttpPost]public IActionResult Delete(Employee model)
		{
			_employee.Delete(model.Id);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ViewResult Edit(int Id)
		{
			Employee employee = _employee.GetEmployee(Id);
			EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
			{
				Id = employee.Id,
				Name = employee.Name,
				Email = employee.Email,
				Department = employee.Department,
				ExistingPhotoPath = employee.PhotoPath
			};
			return View(employeeEditViewModel);
		}


		[HttpPost]
		public IActionResult Edit(EmployeeEditViewModel model)
		{
			if (ModelState.IsValid)
			{
				Employee employee = _employee.GetEmployee(model.Id);
				// Update the employee object with the data in the model object
				employee.Name = model.Name;
				employee.Email = model.Email;
				employee.Department = model.Department;

				// If the user wants to change the photo, a new photo will be
				// uploaded and the Photo property on the model object receives
				// the uploaded photo. If the Photo property is null, user did
				// not upload a new photo and keeps his existing photo
				if (model.Photo != null)
				{
					// If a new photo is uploaded, the existing photo must be
					// deleted. So check if there is an existing photo and delete
					if (model.ExistingPhotoPath != null)
					{
						string filePath = Path.Combine(hostingEnvironment.WebRootPath,
							"images", model.ExistingPhotoPath);
						System.IO.File.Delete(filePath);
					}
					// Save the new photo in wwwroot/images folder and update
					// PhotoPath property of the employee object which will be
					// eventually saved in the database
					employee.PhotoPath = ProcessUploadedFile(model);
				}

				// Call update method on the repository service passing it the
				// employee object to update the data in the database table
				Employee updatedEmployee = _employee.Update(employee);

				return RedirectToAction("index");
			}
			return View();
		}

		[HttpPost]
		public IActionResult Create(CreateViewModel model)
		{
			if (ModelState.IsValid)
			{
				string uniqueFileName = null;

				// If the Photo property on the incoming model object is not null, then the user
				// has selected an image to upload.
				if (model.Photo != null)
				{
					// The image must be uploaded to the images folder in wwwroot
					// To get the path of the wwwroot folder we are using the inject
					// HostingEnvironment service provided by ASP.NET Core
					string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
					// To make sure the file name is unique we are appending a new
					// GUID value and and an underscore to the file name
					uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
					string filePath = Path.Combine(uploadsFolder, uniqueFileName);
					// Use CopyTo() method provided by IFormFile interface to
					// copy the file to wwwroot/images folder
					model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
				}

				Employee newEmployee = new Employee
				{
					Name = model.Name,
					Email = model.Email,
					Department = model.Department,
					// Store the file name in PhotoPath property of the employee object
					// which gets saved to the Employees database table
					PhotoPath = uniqueFileName
				};

				_employee.ADD(newEmployee);
				return RedirectToAction("details", new { id = newEmployee.Id });
			}
		
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
