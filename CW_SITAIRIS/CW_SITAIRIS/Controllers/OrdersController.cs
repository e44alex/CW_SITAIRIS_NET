using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CW_SITAIRIS.Controllers.LoginController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CW_SITAIRIS.Models;
using CW_SITAIRIS.Models.AppDBContext;
using Microsoft.VisualBasic;

namespace CW_SITAIRIS.Controllers
{
    public class OrdersController : Controller
    {
        private static AppDBContext _context;

        public OrdersController(AppDBContext context)
        {
            _context = context;
        }


        public static Task<Car> getCar(Order order)
        {
            return _context.Cars.Where(x => x.idCar == order.carId).FirstOrDefaultAsync();
        }

        public static Task<User> getUser(Order order)
        {
            return _context.users.Where(x => x.idUser == order.clientId).FirstOrDefaultAsync();
        }


        // GET: Orders/5
        public async Task<IActionResult> Index(int? userId, DateTime? date)
        {
            if (userId == null)
            {
                if (date == null)
                {
                    return View(await _context.Orders.ToListAsync());
                }
                else
                {
                    DateTime dateStart = (DateTime) date.Value.Subtract(date.Value.TimeOfDay);
                    DateTime dateEnd = dateStart.AddDays(1);

                    return View(await _context.Orders.Where(x=>x.dateClosed >= dateStart && x.dateClosed < dateEnd).ToListAsync()); 
                }
            }
            else
            {
                if (date == null)
                {
                    return View(await _context.Orders.Where(x => x.clientId == userId).ToListAsync());
                }
                else
                {
                    DateTime dateStart = (DateTime)date.Value.Subtract(date.Value.TimeOfDay);
                    DateTime dateEnd = dateStart.AddDays(1);

                    return View(await _context.Orders.Where(x => x.dateClosed >= dateStart && x.dateClosed < dateEnd && x.clientId == userId).ToListAsync());
                }
            }
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.orderId == id);
            if (order == null)
            {
                return NotFound();
            }
            else
            {
                OrderInfo info = new OrderInfo(order);

                info.carName = getCar(order).Result.ToString();
                info.clientName = getUser(order).Result.name;

                return View(info);
            }
        }

        // GET: Orders/Create
        public IActionResult Create(int userId, int carId)
        {
            var order = new Order()
            {
                clientId = userId,
                carId = carId,
                dateOpened = DateTime.Now,
                dateClosed = DateTime.Today.AddDays(1)
            };
            return View(order);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("orderId,carId,clientId,managerId,dateOpened,dateClosed")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Orders", order.clientId );
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("orderId,carId,clientId,managerId,dateOpened,dateClosed")] Order order)
        {
            if (id != order.orderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.orderId))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.orderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.orderId == id);
        }
    }
}
