using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyMates.Services.Interfaces;
using StudyMates.ViewModels;

namespace StudyMates.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService sessionService;
        private readonly IGroupsService groupsService;

        public SessionController(ISessionService sessionService, IGroupsService groupsService)
        {
            this.sessionService = sessionService;
            this.groupsService = groupsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int groupId)
        {
            StudySessionListViewModel? model =
                await sessionService.GetByGroupAsync(groupId, GetUserId());

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create(int groupId)
        {
            if (!await groupsService.IsCreatorAsync(groupId, GetUserId()!))
            {
                return Forbid();
            }

            StudySessionFormModel model = new StudySessionFormModel
            {
                StartsOn = DateTime.Now.Date.AddDays(1).AddHours(18)
            };

            ViewBag.GroupId = groupId;

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int groupId, StudySessionFormModel model)
        {
            if (!await groupsService.IsCreatorAsync(groupId, GetUserId()!))
            {
                return Forbid();
            }

            if (model.StartsOn <= DateTime.Now)
            {
                ModelState.AddModelError(nameof(model.StartsOn),
                    "The session must start in the future.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.GroupId = groupId;

                return View(model);
            }

            await sessionService.CreateAsync(groupId, model);

            return RedirectToAction(nameof(Index), new { groupId });
        }

        private string? GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}