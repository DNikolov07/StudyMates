using Microsoft.AspNetCore.Mvc;
using StudyMates.Services;
using StudyMates.Services.Interfaces;
using StudyMates.ViewModels;
using System.Security.Claims;

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

        private string? GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}