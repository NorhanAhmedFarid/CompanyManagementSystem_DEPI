using AutoMapper;
using CompanyG02.BLL.Interfaces;
using CompanyG02.BLL.Repositories;
using CompanyG02.DAL.Models;
using CompanyG02.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyG02.PL.Controllers
{
	[Authorize]

	public class DepartmentController : Controller
    {
        private readonly IUnitOfWok unitOfWok;
        private readonly IMapper mapper;

        public DepartmentController(IUnitOfWok unitOfWok, IMapper mapper)
        {
            this.unitOfWok = unitOfWok;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {//Get All
            IEnumerable<Department> departments;

             departments =await unitOfWok.DepartmentRepository.GetAll();
            var MappedDep = mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);

            return View(MappedDep);
        }
        public IActionResult Create()
        {

        return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel DepartmentVM)
        {
            if(ModelState.IsValid)//server side validation
            {
                var MappedDep =  mapper.Map<DepartmentViewModel, Department>(DepartmentVM);


                  await  unitOfWok.DepartmentRepository.Add(MappedDep);
                int Result = await unitOfWok.Compelete();
                if (Result>0)
                {
                    TempData["Message"] = "Department Is Created!";

                }
                return RedirectToAction(nameof(Index));

            }
            return View(DepartmentVM);
        }
        public async Task<IActionResult> Details(int? id , string ViewName= "Details") 
        {

            if (id is null)
                return BadRequest();
            var department =await unitOfWok.DepartmentRepository.Get(id.Value);
            if (department is null)
                return NotFound();
            var MappedDep = mapper.Map<Department, DepartmentViewModel>(department);

            return View(ViewName, MappedDep);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");

            //if (id is null)
            //    return BadRequest();
            //var Department = departmentRepository.Get(id.Value);
            //if (Department is null)
            //    return NotFound();
             //return View(Department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int? id, DepartmentViewModel DepartmentVM)
        {
            if (id != DepartmentVM.Id)
                return BadRequest();
            if(ModelState.IsValid)//server side validation
            {
                try
                {
                    var MappedEmp = mapper.Map<DepartmentViewModel, Department>(DepartmentVM);

                    unitOfWok.DepartmentRepository.Update(MappedEmp);
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
            return View(DepartmentVM);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete([FromRoute] int? id, DepartmentViewModel DepartmentVM)
        {

            if (id != DepartmentVM.Id)
                return BadRequest();
            try
                {
                var MappedEmp = mapper.Map<DepartmentViewModel, Department>(DepartmentVM);

                unitOfWok.DepartmentRepository.Delete(MappedEmp);
              await  unitOfWok.Compelete();
                return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                     
            return View(DepartmentVM);

                }

            }
        }
    }

