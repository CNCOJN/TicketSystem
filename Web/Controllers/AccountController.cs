using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands;
using Application.Dtos;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Register(CancellationToken cancellationToken)
        {
            IEnumerable<RoleDto> response = await _mediator.Send(new GetAllRolesQuery(), cancellationToken);
            return View(response);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Verify(VerifyDto dto, CancellationToken cancellationToken)
        {
            bool response = await _mediator.Send(new VerifyAccountCommand(dto), cancellationToken);
            return response ? RedirectToAction("Index", "Ticket") : RedirectToAction("Login", "Account");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> VerifyRegistration(RegisterFormDto dto, CancellationToken cancellationToken)
        {
            int response = await _mediator.Send(new PostAccountCommand(dto), cancellationToken);
            return response < 1 ? RedirectToAction("Register", "Account") : RedirectToAction("Login", "Account");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]        
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            await _mediator.Send(new LogoutCommand(), cancellationToken);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}