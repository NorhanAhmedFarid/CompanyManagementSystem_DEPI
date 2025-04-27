using AutoMapper;
using CompanyG02.BLL.Interfaces;
using CompanyG02.BLL.Repositories;
using CompanyG02.DAL.Models;
using CompanyG02.PL.Helper;
using CompanyG02.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace CompanyG02.PL.Controllers
{
	[Authorize]

	public class EmployeeController : Controller
    {
       
        private readonly IUnitOfWok unitOfWok;
        private readonly IMapper mapper;

        public EmployeeController(IUnitOfWok unitOfWok, IMapper mapper) //Ask ClR For Create Object From Employee Repository
        {
           
            this.unitOfWok = unitOfWok;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue))
            {
                employees =await unitOfWok.EmployeeRepository.GetAll();
            }
            else
            {

                employees = unitOfWok.EmployeeRepository.GetEmpByName(SearchValue);
            }
            //1.viewData =>KeyValuePair[Dictionary Object]
            //Transfer Data From Controller [Action] To It's View
            //View To LayOut
            //.Net Framework 3.5
            //ViewData["Message"] = "Hello From View Data";
            ////2.ViewBag=>Dynamic Property [Based on Dynamic Key Word]
            ////Transfer Data From Controller [Action] To It's View
            ////View To LayOut
            ////.Net Framework 4.0
            //ViewBag.Message= "Hello From ViewBag";



            var MappedEmp = mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(employees);

            return View(MappedEmp);
        }
        public IActionResult Create()
        {
           // ViewBag.Department = departmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)//server side validation
            {
                //Mapping
                //Manual Mappind
                //Employee mappedEmployee =new Employee()
                //{
                //    Name = employeeVM.Name,
                //    Age = employeeVM.Age,
                //    Salary = employeeVM.Salary,
                //    Email = employeeVM.Email,
                //    PhoneNumber = employeeVM.PhoneNumber,
                //    Adress = employeeVM.Adress,
                //    IsActive = employeeVM.IsActive,
                //    HireDate = employeeVM.HireDate,
                //    DepartmentId = employeeVM.DepartmentId
                //};
                //Auto Mapper
                var MappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                MappedEmp.ImageName = DocumentSetting.UploadImage(employeeVM.Image, "Images");
                  await  unitOfWok.EmployeeRepository.Add(MappedEmp);
                int Result =await unitOfWok.Compelete();
                if(Result>0)
                {
            //3.TempData=>Dictionary Object
            //Transfer Data From Action To Action
            TempData["Message"] = "Employee Is Created!";

                }
                return RedirectToAction(nameof(Index));
              

               // employeeRepository.Add(employee);

            }

            return View(employeeVM);
        }
        public async Task<IActionResult>Details(int? id, string ViewName = "Details")
        {

            if (id is null)
                return BadRequest();
            var employee =await unitOfWok.EmployeeRepository.Get(id.Value);
            if (employee is null)
                return NotFound();
            var MappedEmp = mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(ViewName, MappedEmp);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");

            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)//server side validation
            {
                try
                {
                    var MappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                    unitOfWok.EmployeeRepository.Update(MappedEmp);
                    await unitOfWok.Compelete();
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    //1.Log Exception
                    //2.Friendly Message

                }

            }
            return View(employeeVM);
        }
        [HttpGet]

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete([FromRoute] int? id, EmployeeViewModel employeeVM)
        {

            if (id != employeeVM.Id)
                return BadRequest();
            try
            {
                var MappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                if (MappedEmp.ImageName is not null)
                {
                    DocumentSetting.DeleteFile(MappedEmp.ImageName, "Images");
                }
                unitOfWok.EmployeeRepository.Delete(MappedEmp);
                unitOfWok.Compelete();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(employeeVM);

            }

        }
    }
}
