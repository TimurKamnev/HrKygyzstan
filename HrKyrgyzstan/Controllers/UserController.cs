﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using HrKyrgyzstan.Interfaces;
using HrKyrgyzstan.Areas.Identity.Data;
using HrKyrgyzstan.Models;
using Microsoft.EntityFrameworkCore;
using HrKyrgyzstan.Context;

namespace HrKyrgyzstan.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<CreatedUser> _signInManager;

        public UserController(AppDbContext appDbContext ,IUnitOfWork unitOfWork, SignInManager<CreatedUser> signInManager)
        {
            _appDbContext = appDbContext;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var users = _unitOfWork.User.GetUsers();
            return View(users);
        }

        [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        public async Task<IActionResult> Edit(string id)
        {
            var user = _unitOfWork.User.GetUser(id);
            var roles = _unitOfWork.Role.GetRoles();

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

            var roleItems = roles.Select(role =>
                new SelectListItem(
                    role.Name,
                    role.Id,
                    userRoles.Any(ur => ur.Contains(role.Name)))).ToList();

            var vm = new EditUserViewModel
            {
                User = user,
                Roles = roleItems
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]

        public async Task<IActionResult> OnPostAsync(EditUserViewModel data)
        {
            var user = _unitOfWork.User.GetUser(data.User.Id);
            if (user == null)
            {
                return NotFound();
            }

            var userRolesInDb = await _signInManager.UserManager.GetRolesAsync(user);

            var rolesToAdd = new List<string>();
            var rolesToDelete = new List<string>();

            foreach (var role in data.Roles)
            {
                var assignedInDb = userRolesInDb.FirstOrDefault(ur => ur == role.Text);
                if (role.Selected)
                {
                    if (assignedInDb == null)
                    {
                        rolesToAdd.Add(role.Text);
                    }
                }
                else
                {
                    if (assignedInDb != null)
                    {
                        rolesToDelete.Add(role.Text);
                    }
                }
            }

            if (rolesToAdd.Any())
            {
                await _signInManager.UserManager.AddToRolesAsync(user, rolesToAdd);
            }

            if (rolesToDelete.Any())
            {
                await _signInManager.UserManager.RemoveFromRolesAsync(user, rolesToDelete);
            }

            user.FirstName = data.User.FirstName;
            user.LastName = data.User.LastName;
            user.Email = data.User.Email;

            _unitOfWork.User.UpdateUser(user);

            return RedirectToAction("Edit", new { id = user.Id });
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _appDbContext.Users
               .FirstOrDefaultAsync(x => x.Id == id);
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.User.DeleteUser(user);

            return Ok();
        }
        
    }
}
