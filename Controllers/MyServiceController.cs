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

    public class MyServiceController : Controller
    {

        private readonly IUnitOfWok unitOfWok;
        private readonly IMapper mapper;

        public MyServiceController(IUnitOfWok unitOfWok, IMapper mapper)
        {

            this.unitOfWok = unitOfWok;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {//Get All
            IEnumerable<MyService> MyService;

            MyService = await unitOfWok.MyServiceRepository.GetAll();
            var MappedSer = mapper.Map<IEnumerable<MyService>, IEnumerable<MyServiceViewModel>>(MyService);

            return View(MappedSer);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MyServiceViewModel MyServiceVM)
        {
            if (ModelState.IsValid)
            {

                var MappedSer = mapper.Map<MyServiceViewModel, MyService>(MyServiceVM);
                await unitOfWok.MyServiceRepository.Add(MappedSer);
                int Result = await unitOfWok.Compelete();
                if (Result > 0)
                {

                    TempData["Message"] = "Service Is Created!";

                }
                return RedirectToAction(nameof(Index));



            }

            return View(MyServiceVM);
        }
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {

            if (id is null)
                return BadRequest();
            var services = await unitOfWok.MyServiceRepository.Get(id.Value);
            if (services is null)
                return NotFound();
            var MappedProj = mapper.Map<MyService, MyServiceViewModel>(services);
            return View(ViewName, MappedProj);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, MyServiceViewModel projectVM)
        {
            if (id != projectVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedProj = mapper.Map<MyServiceViewModel, MyService>(projectVM);

                    unitOfWok.MyServiceRepository.Update(MappedProj);
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

        public IActionResult Delete([FromRoute] int? id, MyServiceViewModel projectVM)
        {

            if (id != projectVM.Id)
                return BadRequest();
            try
            {
                var MappedProj = mapper.Map<MyServiceViewModel, MyService>(projectVM);

                unitOfWok.MyServiceRepository.Delete(MappedProj);
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
