using ClassSimulation2.Areas.Admin.ViewModels.Position;
using ClassSimulation2.DAL.Context;
using ClassSimulation2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassSimulation2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PositionController : Controller
    {
        private readonly AppDbContext _db;

        public PositionController(AppDbContext db)
        { 
            _db= db;
        }
        public async Task<IActionResult> Index()
        {
            List<Position> positions = await _db.Positions.ToListAsync();
            return View(positions);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(CreatePositionVM positionVM)
        {
            if(!ModelState.IsValid) return View(positionVM);
            Position? position = new Position()
            {
                Name = positionVM.Name,
            };
            await _db.Positions.AddAsync(position);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult>Delete(int? id)
        {
            if (id is null) return NotFound();
            Position? position = await _db.Positions.FindAsync(id);
            if (position is null) return NotFound();
            position.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Restore(int? id)
        {
            if (id is null) return NotFound();
            Position? position = await _db.Positions.FindAsync(id);
            if (position is null) return NotFound();
            position.IsDeleted = false;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult>Update(int? id)
        {
            if (id is null) return NotFound();
            Position? position = await _db.Positions.FindAsync(id);
            if (position is null) return NotFound();
            UpdatePositionVM positionVM = new UpdatePositionVM()
            {
                Id = position.Id,
                Name = position.Name,
            };
            await _db.SaveChangesAsync();
            return View(positionVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdatePositionVM positionVM)
        {
            if (!ModelState.IsValid) return View(positionVM);
            Position? oldPosition = await _db.Positions.FindAsync(positionVM.Id);
           if (oldPosition is null) return NotFound();
           oldPosition.Id= positionVM.Id;
            oldPosition.Name= positionVM.Name;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
