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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            StudyGroupFormModel? model = await studyGroupService.GetForEditAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            if (!await studyGroupService.IsCreatorAsync(id, GetUserId()!))
            {
                return Forbid();
            }

            model.Subjects = await studyGroupService.GetSubjectsAsync();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudyGroupFormModel model)
        {
            if (!await studyGroupService.IsCreatorAsync(id, GetUserId()!))
            {
                return Forbid();
            }

            if (!ModelState.IsValid)
            {
                model.Subjects = await studyGroupService.GetSubjectsAsync();

                return View(model);
            }

            bool isEdited = await studyGroupService.EditAsync(id, model);

            if (!isEdited)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            StudyGroupViewModel? group =
                await studyGroupService.GetByIdAsync(id, GetUserId());

            if (group == null)
            {
                return NotFound();
            }

            if (!group.IsCreator)
            {
                return Forbid();
            }

            return View(group);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!await studyGroupService.IsCreatorAsync(id, GetUserId()!))
            {
                return Forbid();
            }

            bool isDeleted = await studyGroupService.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Join(int id)
        {
            bool isJoined = await studyGroupService.JoinAsync(id, GetUserId()!);

            if (!isJoined)
            {
                TempData["Error"] = "You cannot join this group. It may be full or you are already a member.";
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Leave(int id)
        {
            bool hasLeft = await studyGroupService.LeaveAsync(id, GetUserId()!);

            if (!hasLeft)
            {
                TempData["Error"] = "You cannot leave this group.";
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        private string? GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}