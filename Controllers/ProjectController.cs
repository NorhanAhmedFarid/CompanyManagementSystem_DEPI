using AutoMapper;
using CompanyG02.BLL.Interfaces;
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

    public class ProjectController : Controller
    {

        private readonly IUnitOfWok unitOfWok;
        private readonly IMapper mapper;

        public ProjectController(IUnitOfWok unitOfWok, IMapper mapper)
        {

            this.unitOfWok = unitOfWok;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {//Get All
            IEnumerable<Project> projects;

            projects = await unitOfWok.ProjectRepository.GetAll();
            var MappedDep = mapper.Map<IEnumerable<Project>, IEnumerable<ProjectsViewModel>>(projects);

            return View(MappedDep);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProjectsViewModel projectsVM)
        {
            if (ModelState.IsValid)//server side validation
            {

                var MappedProj = mapper.Map<ProjectsViewModel, Project>(projectsVM);
                await unitOfWok.ProjectRepository.Add(MappedProj);
                int Result = await unitOfWok.Compelete();
                if (Result > 0)
                {

                    TempData["Message"] = "Project Is Created!";

                }
                return RedirectToAction(nameof(Index));



            }

            return View(projectsVM);
        }
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {

            if (id is null)
                return BadRequest();
            var projects = await unitOfWok.ProjectRepository.Get(id.Value);
            if (projects is null)
                return NotFound();
            var MappedProj = mapper.Map<Project, ProjectsViewModel>(projects);
            return View(ViewName, MappedProj);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, ProjectsViewModel projectVM)
        {
            if (id != projectVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedProj = mapper.Map<ProjectsViewModel, Project>(projectVM);

                    unitOfWok.ProjectRepository.Update(MappedProj);
                    await unitOfWok.Compelete();
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);


                }

            }
            return View(projectVM);
        }
        [HttpGet]

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete([FromRoute] int? id, ProjectsViewModel projectVM)
        {

            if (id != projectVM.Id)
                return BadRequest();
            try
            {
                var MappedProj = mapper.Map<ProjectsViewModel, Project>(projectVM);

                unitOfWok.ProjectRepository.Delete(MappedProj);
                unitOfWok.Compelete();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(projectVM);

            }

        }
    }
}
