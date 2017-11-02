using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AdPortal.Infrastructure.Command;
using AdPortal.Infrastructure.Command.Users;
using AdPortal.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdPortal.MVC.Controllers
{
    public class UserController : MvcControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILoginManager _logger;
        public UserController(IUserService userService, ICommandDispatcher dispatcher, 
        ILoginManager logger, IHttpContextAccessor accesor)
        :base(dispatcher, accesor)
        {
            _userService = userService;
            _logger = logger;
            {
                
            }
        }

        public async Task<IActionResult> UserDetails(string email)
        {
            var user = await _userService.GetUserDTO(email);
            return View(user);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task Register(CreateUser command)
        {
           await DispatcheAsync<CreateUser>(command);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login command)
        {
            try
            {
                await DispatcheAsync<Login>(command);
                await HttpContext.SignInAsync(_logger.GetAuthName(),await _logger.GetClaimsPrincipalAsync(command.Email,"user"));
                 ViewBag.errorOccured = false;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.errorOccured = true;
                ViewBag.errorText = ex.Message;
                return View();
            }
           
            
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(_logger.GetAuthName());   
            return RedirectToAction("Login","User");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword command)
        {
            try
            {
                await DispatcheAsync<ChangePassword>(command);
                ViewBag.errorOccured = false;
                return RedirectToAction("Index", "UserAd");
            }
            catch (Exception ex)
            {
                ViewBag.errorOccured = true;
                ViewBag.errorText = ex.Message;
                return View();
            }
        }
    }
}