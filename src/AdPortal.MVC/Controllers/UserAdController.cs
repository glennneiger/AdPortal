using System;
using System.Linq;
using System.Threading.Tasks;
using AdPortal.Infrastructure.Command;
using AdPortal.Infrastructure.Command.UserAd;
using AdPortal.Infrastructure.DTO;
using AdPortal.Infrastructure.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdPortal.MVC.Controllers
{
    [Authorize]
    public class UserAdController : MvcControllerBase
    {    
   
        private readonly IUserAdService _userAdService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public UserAdController(IUserAdService userAdService, ICommandDispatcher dispatcher, 
        IHttpContextAccessor accesor, IMapper mapper, ICategoryService categoryService) 
        : base(dispatcher,accesor)
        {
            _userAdService = userAdService;
            _mapper = mapper;
            _categoryService = categoryService;
        }


        public async Task<IActionResult> Index()
        {
             return View(await _userAdService.GetAllAdsDTOAsync(UserId));
        } 
        
        public async Task<IActionResult> CreateAd()
        {
            var categories = await _categoryService.BrowseDTOAsync();
            ViewBag.Categories = categories;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAd(AdDTO ad)
        {
            
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.BrowseDTOAsync();
                ViewBag.Categories = categories;
                return View(ad);
            }
            

            await DispatcheAsync<CreateUserAd>(_mapper.Map<CreateUserAd>(ad));
            return RedirectToAction("Index","UserAd");
        }

        public async Task<IActionResult> EditAd(Guid id)
        {
            var categories = await _categoryService.BrowseDTOAsync();
            ViewBag.Categories = categories;

            var ad =  await _userAdService.GetAdDTOAsync(UserId,id);
            return View(ad);
        }

        [HttpPost]
        public async Task<IActionResult> EditAd(AdDTO ad)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.BrowseDTOAsync();
                ViewBag.Categories = categories;
                return View(ad);
            }
            await DispatcheAsync<EditUserAd>(_mapper.Map<EditUserAd>(ad));
            return RedirectToAction("Index","UserAd");
   
        }

        public async Task<IActionResult> DeleteAd(Guid id)
        {
            var ad =  await _userAdService.GetAdDTOAsync(UserId,id);
            return View(ad);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAd(RemoveUserAd command)
        {
            await DispatcheAsync<RemoveUserAd>(command);
            return RedirectToAction("Index","UserAd");
        }

        public async Task<IActionResult> AdDetails(Guid id)
        {
             var addto = await _userAdService.GetAdDTOAsync(UserId,id);
             return View(addto);
        }

    }
}