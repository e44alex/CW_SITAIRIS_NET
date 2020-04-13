using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CW_SITAIRIS.Models;
using CW_SITAIRIS.Models.AppDBContext;

namespace CW_SITAIRIS.Controllers
{
    public class TestDrivesController : Controller
    {
        private readonly AppDBContext _context;

        public TestDrivesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: TestDrives
        public async Task<IActionResult> Index(DateTime? date)
        {
            if (date != null)
            {
                date.Value.Subtract(date.Value.TimeOfDay);
                DateTime nextDay = date.Value.AddDays(1);

                List<TestDrives> list = await _context.TestDrives.Where(x => x.date >= date && x.date < nextDay).ToListAsync();

                List<TestDriveInfo> infos = new List<TestDriveInfo>();

                foreach (TestDrives drives in list)
                {
                    infos.Add(await getDriveInfo(drives));
                }

                return View(infos);
            }
            else
            {
                List<TestDrives> list = await _context.TestDrives.ToListAsync();

                List<TestDriveInfo> infos = new List<TestDriveInfo>();

                foreach (TestDrives drives in list)
                {
                    infos.Add(await getDriveInfo(drives));
                }

                return View(infos);
            }
        }

        private async Task<TestDriveInfo> getDriveInfo(TestDrives testDrives)
        {
            TestDriveInfo info = new TestDriveInfo();

            info.date = testDrives.date;

            User user = await _context.users.FirstOrDefaultAsync(u => u.idUser == testDrives.userId);
            Car car = await _context.Cars.FirstOrDefaultAsync(u => u.idCar == testDrives.carId);

            info.carName = car.mark + " " + car.model + " " + car.built;
            info.clientFio = user.name;

            return info;
        }

        // GET: TestDrives/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testDrives = await _context.TestDrives
                .FirstOrDefaultAsync(m => m.idDrive == id);
            if (testDrives == null)
            {
                return NotFound();
            }


            return View(await getDriveInfo(testDrives));
        }

        // GET: TestDrives/Create
        public IActionResult Create(int userId, int carId)
        {

            TestDrives testDrives = new TestDrives()
            {
                carId = carId,
                userId = userId,
                date = DateTime.Now
            };

            return View(testDrives);
        }

        // POST: TestDrives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idDrive,userId,carId,date")] TestDrives testDrives)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testDrives);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Cars");
            }
            return View(testDrives);
        }

        // GET: TestDrives/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testDrives = await _context.TestDrives.FindAsync(id);
            if (testDrives == null)
            {
                return NotFound();
            }
            return View(testDrives);
        }

        // POST: TestDrives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idDrive,userId,carId,date")] TestDrives testDrives)
        {
            if (id != testDrives.idDrive)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testDrives);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestDrivesExists(testDrives.idDrive))
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
            return View(testDrives);
        }

        // GET: TestDrives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testDrives = await _context.TestDrives
                .FirstOrDefaultAsync(m => m.idDrive == id);
            if (testDrives == null)
            {
                return NotFound();
            }

            return View(testDrives);
        }

        // POST: TestDrives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testDrives = await _context.TestDrives.FindAsync(id);
            _context.TestDrives.Remove(testDrives);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestDrivesExists(int id)
        {
            return _context.TestDrives.Any(e => e.idDrive == id);
        }
    }
}
