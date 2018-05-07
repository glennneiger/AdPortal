using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AdPortal.Infrastructure.Command;
using AdPortal.Infrastructure.Services;
using AdPortal.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdPortal.MVC.Controllers
{
    public class HomeController : MvcControllerBase
    {
        private readonly IAdsService _adsService;
        private readonly IUserService _userService;
        public HomeController(ICommandDispatcher dispatcher, IHttpContextAccessor accesor, 
        IAdsService adsService, IUserService userService) : base(dispatcher, accesor)
        {
            _adsService = adsService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _adsService.BrowseAsync());
        } 
        public async Task<IActionResult> UserDetails(Guid id)
        {
            return View(await _adsService.GetUserDetailsDTOAsync(id));
        } 

        public async Task<IActionResult> Details(Guid id)
        {
            return View(await _adsService.GetAdDTOAsync(id));
        } 

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}