using Application.Commands;
using Application.Dtos;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page, CancellationToken cancellationToken)
        {
            IEnumerable<TicketDto> response = await _mediator.Send(new GetAllTicketsByPageQuery(page), cancellationToken);
            return View(response);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> GetFirstOrLastPage(int page, CancellationToken cancellationToken)
        {
            IEnumerable<TicketDto> response = await _mediator.Send(new GetAllTicketsByPageQuery(page), cancellationToken);
            return View("Index", response);
        }

        [Authorize(Roles = "QA,PM,ADMIN")]
        [HttpGet]
        public async Task<IActionResult> PostTicketForm(CancellationToken cancellationToken)
        {
            PostTicketFormInfoDto response = await _mediator.Send(new PostTicketFormQuery(), cancellationToken);
            return View(response);
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> PostTicket(PostTicketFormDto dto,CancellationToken cancellationToken)
        {
            int response = await _mediator.Send(new PostTicketCommand(dto), cancellationToken);
            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Resolved(int id, int page, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateTicketStatusByIdCommand(id), cancellationToken);
            return RedirectToAction("Index", new { page = page });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(int id, CancellationToken cancellationToken)
        {
            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Previous(int page, CancellationToken cancellationToken)
        {
            page = page <= 1 ? 1 : --page;
            return RedirectToAction("Index", new { page = page });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Next(int page, int totalPages, CancellationToken cancellationToken)
        {
            page = page >= totalPages ? totalPages : ++page;
            return RedirectToAction("Index", new { page = page });
        }
    }
}
