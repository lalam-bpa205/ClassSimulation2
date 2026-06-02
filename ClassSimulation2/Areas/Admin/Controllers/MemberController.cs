using ClassSimulation2.Areas.Admin.ViewModels.Member;
using ClassSimulation2.DAL.Context;
using ClassSimulation2.Models;
using ClassSimulation2.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassSimulation2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MemberController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public MemberController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Member> members = await _db.Members
                .Include(m => m.Position)
                .ToListAsync();
            return View(members);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateMemberVM memberVM)
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();
            if (!ModelState.IsValid) return View(memberVM);
            if (memberVM.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "File is required");
                return View();
            }
            if (memberVM.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "File must be an image");
                return View();
            }
            if (memberVM.ImageFile.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("ImageFile", "File cannot be exceed 2MB");
                return View();
            }
            Member? member = new Member()
            {
                Name = memberVM.Name,
                Surname = memberVM.Surname,
                Description = memberVM.Description,
                PositionId = memberVM.PositionId,
                ImageUrl = memberVM.ImageFile.SaveImage(_env, "uploads/members")
            };
            await _db.Members.AddAsync(member);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();
            if (id is null) return NotFound();
            Member? member = await _db.Members
                .Include(m => m.Position)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member is null) return NotFound();
            member.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Restore(int? id)
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();
            if (id is null) return NotFound();
            Member? member = await _db.Members
                .Include(m => m.Position)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member is null) return NotFound();
            member.IsDeleted = false;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
