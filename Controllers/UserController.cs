using AutoMapper;
using CompanyG02.BLL.Interfaces;
using CompanyG02.BLL.Repositories;
using CompanyG02.DAL.Models;
using CompanyG02.PL.Helper;
using CompanyG02.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyG02.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;

        public UserController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IMapper mapper)
        {
			this.userManager = userManager;
			this.signInManager = signInManager;
            this.mapper = mapper;
        }
		public async Task<IActionResult> Index(string email)
		{
			if (string.IsNullOrEmpty(email))
			{
				var users = await userManager.Users.Select(U => new UserViewModel()
				{
					Id = U.Id,
					FName = U.FName,
					LName = U.LName,
					Email = U.Email,
					PhoneNumber = U.PhoneNumber,
					Roles = userManager.GetRolesAsync(U).Result
				}).ToListAsync();
				return View(users);
			}
			else
			{
				var user = await userManager.FindByEmailAsync(email);
				var MappedUser = new UserViewModel()
				{
					Id = user.Id,
					FName = user.FName,
					LName = user.LName,
					Email = user.Email,
					PhoneNumber = user.PhoneNumber,
					Roles = userManager.GetRolesAsync(user).Result
				};

			return View(new List<UserViewModel>() { MappedUser});
			}

		}
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {

            if (id is null)
                return BadRequest();
            var user = await userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();
            var MappedUser = mapper.Map<ApplicationUser, UserViewModel>(user);
            return View(ViewName, MappedUser);
        }
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel userVM)
        {
            if (id != userVM.Id)
                return BadRequest();
            if (ModelState.IsValid)//server side validation
            {
                try
                {
                    var user = await userManager.FindByIdAsync(id);
                    user.FName = userVM.FName;
                    user.LName = userVM.LName;
                    user.PhoneNumber = userVM.PhoneNumber;
                    user.Email = userVM.Email;

            await userManager.UpdateAsync(user);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    //1.Log Exception
                    //2.Friendly Message

                }

            }
            return View(userVM);
        }
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete([FromRoute] string id, UserViewModel userVm)
        {

            if (id != userVm.Id)
                return BadRequest();
            try
            { 
                var User= await userManager.FindByIdAsync(id);
                await userManager.DeleteAsync(User);
                        return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(userVm);

            }

        }
    }
}

