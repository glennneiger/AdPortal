using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AdPortal.Infrastructure.Command;
using AdPortal.Infrastructure.Command.AdminAd;
using AdPortal.Infrastructure.Command.UserAd;
using AdPortal.Infrastructure.DTO;
using AdPortal.Infrastructure.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdPortal.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : MvcControllerBase
    {
        private readonly IAdsService _adsService;
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;
        public AdminController(ICommandDispatcher dispatcher, IHttpContextAccessor accesor,
        IAdsService adsService, IMapper mapper, IAdminService adminService) : base(dispatcher, accesor)
        {
            _adsService = adsService;
            _mapper = mapper;
            _adminService = adminService;
        }
        
        public async Task<IActionResult> Index()
        {
            return View(await _adminService.BrowseAllAsync());
        } 

        public async Task<IActionResult> ChangeStatus(Guid id)
        {
            var ad = await _adsService.GetAdDTOAsync(id);
            return View(ad);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(AdDTO ad)
        {
            var changeCommand = new ChangeAdStatus(){Id= ad.Id, Status= ad.Status};
            await DispatcheAsync<ChangeAdStatus>(changeCommand);
            return RedirectToAction("Index","Admin");
   
        }
    }
}