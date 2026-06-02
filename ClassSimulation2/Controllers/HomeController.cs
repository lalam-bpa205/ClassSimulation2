using ClassSimulation2.DAL.Context;
using ClassSimulation2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassSimulation2.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db=db;
        }
        public async Task<IActionResult> Index()
        {
            List<Member>members=await _db.Members
                .Include(m=>m.Position)
                .ToListAsync();
            return View(members);
        }
        public async Task<IActionResult>Details(int? id)
        {
            if (id is null) return NotFound();
            Member? member=await _db.Members
                .Include(m => m.Position)
                .FirstOrDefaultAsync(m=>m.Id==id);
            if (member is null) return NotFound();
            return View(member);
        }
    }
}
