using EmployeeManagment.Models;
using EmployeeManagment.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace EmployeeManagment.Controllers
{
    public class HomeController : Controller 
    {
        private IEmployeeRepository _employeeRepository;
        private IHostingEnvironment hostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository,
                              IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        public ViewResult Index()
        {
            var employee = _employeeRepository.GetAllEmployee();
            return View(employee);
        }


        public ViewResult Details(int id)
        {
            
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(id),
                PageTitle = "Employee Details"
            };

            return View(homeDetailsViewModel);

        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                //if(model.Photo != null && model.Photo.Count >0)
                //{foreach (IFormFile photo in model.Photo)
                //    {
                //        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                //        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                //        string filePath = System.IO.Path.Combine(uploadsFolder, uniqueFileName);
                //        photo.CopyTo(new System.IO.FileStream(filePath, FileMode.Create));
                //    }
                //}


                if (model.Photo != null )
                {
                   
                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                        string filePath = System.IO.Path.Combine(uploadsFolder, uniqueFileName);
                        model.Photo.CopyTo(new System.IO.FileStream(filePath, FileMode.Create));
                  
                }
                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                _employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }

            return View();
        }

        //[HttpPost]
        //public IActionResult Create(Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Employee newEmployee = _employeeRepository.Add(employee);
        //        return RedirectToAction("details", new { id = newEmployee.Id });
        //    }

        //    return View();               
        //}

    }
}
