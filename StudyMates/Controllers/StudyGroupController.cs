using Microsoft.AspNetCore.Mvc;
using StudyMates.Services;
using StudyMates.Services.Interfaces;
using StudyMates.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace StudyMates.Controllers
{
    public class StudyGroupController : Controller
    {
        private readonly IGroupsService studyGroupService;

        public StudyGroupController(IGroupsService studyGroupService)
        {
            this.studyGroupService = studyGroupService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<StudyGroupViewModel> groups =
                await studyGroupService.GetAllAsync(GetUserId());

            return View(groups);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            StudyGroupViewModel? group =
                await studyGroupService.GetByIdAsync(id, GetUserId());

            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            StudyGroupFormModel model = new StudyGroupFormModel
            {
                Subjects = await studyGroupService.GetSubjectsAsync()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudyGroupFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Subjects = await studyGroupService.GetSubjectsAsync();

                return View(model);
            }

            await studyGroupService.CreateAsync(model, GetUserId()!);

            return RedirectToAction(nameof(Index));
        }

        private string? GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}