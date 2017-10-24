using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AdPortal.Infrastructure.Command;
using AdPortal.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdPortal.MVC.Controllers
{
    public class MvcControllerBase : Controller
    {
       
        private readonly ICommandDispatcher CommandDispatcher;
        private readonly IHttpContextAccessor _accessor; 

        protected Guid UserId => 
            Guid.Parse(HttpContext.User.Claims
                .FirstOrDefault(x=>x.Type ==ClaimTypes.NameIdentifier ).Value);

        

        public MvcControllerBase(ICommandDispatcher dispatcher, IHttpContextAccessor accesor)
        {
            CommandDispatcher = dispatcher;
            _accessor = accesor;
        }

        protected async Task DispatcheAsync<T>(T command) where T : ICommand
        {
            if(command is IAuthenticatedCommand authenticatedcommand)
            {
                authenticatedcommand.UserId = UserId;
            }
            await CommandDispatcher.DispatcheAsync(command);
        }
    }
}