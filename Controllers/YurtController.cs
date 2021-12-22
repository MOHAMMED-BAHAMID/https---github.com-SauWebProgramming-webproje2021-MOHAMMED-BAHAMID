using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProje.Models;

namespace WebProje.Controllers
{
    public class YurtController : Controller
    {
        private readonly AppDBContext _context;

        public YurtController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Yurt
        public async Task<IActionResult> Index()
        {
            //var yurt = await _context.Yurt.Include(y => y.YurtFotos).ToListAsync();
            var yurt = await _context.YurtFoto.Include(y => y.Yurt).ToListAsync();
            return View(yurt);
        }

        // GET: Yurt/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var yurt = await _context.Yurt
            //    .FirstOrDefaultAsync(m => m.YurtID == id);
            var yurt = await _context.YurtFoto
                .Include(y => y.Yurt)
                .FirstOrDefaultAsync(m => m.YurtID == id);
            if (yurt == null)
            {
                return NotFound();
            }

            return View(yurt);
        }

        // GET: Yurt/CreateYurt
        public IActionResult CreateYurt()
        {
            return View();
        }

        // POST: Yurt/CreateYurt
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateYurt([Bind("YurtID,Yurt_Name,address,Type,Price,Available_place,contract,Details")] Yurt yurt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yurt);
                await _context.SaveChangesAsync();
                return await AddFoto(yurt.YurtID);
            }
            return View(yurt);
        }

        // GET: Yurt/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yurt = await _context.Yurt.FindAsync(id);
            if (yurt == null)
            {
                return NotFound();
            }
            return View(yurt);
        }

        // POST: Yurt/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("YurtID,Yurt_Name,address,Type,Price,Available_place,contract,Details")] Yurt yurt)
        {
            if (id != yurt.YurtID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yurt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YurtExists(yurt.YurtID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(yurt);
        }

        // GET: Yurt/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yurt = await _context.Yurt
                .FirstOrDefaultAsync(m => m.YurtID == id);
            if (yurt == null)
            {
                return NotFound();
            }

            return View(yurt);
        }

        // POST: Yurt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yurt = await _context.Yurt.FindAsync(id);
            _context.Yurt.Remove(yurt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YurtExists(int id)
        {
            return _context.Yurt.Any(e => e.YurtID == id);
        }


        public async Task<IActionResult> AddFoto(int? id)
        {
            var yurt = await _context.Yurt.FindAsync(id);
            if (yurt == null)
            {
                return NotFound();
            }
            int Yurtid=Convert.ToInt32(id);
            return View("AddFoto",new YurtFoto {YurtID=Yurtid});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFoto([Bind("FotoID,FotoDir,YurtID")] YurtFoto yurtFoto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yurtFoto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["YurtID"] = new SelectList(_context.Yurt, "YurtID", "Details", yurtFoto.YurtID);
            return View(yurtFoto);
        }

    }
}
