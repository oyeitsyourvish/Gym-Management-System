using GymManagementSystem.Data;
using GymManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Controllers
{
    public class MembershipPlansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MembershipPlansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MembershipPlans
        public async Task<IActionResult> Index()
        {
            return View(await _context.MembershipPlans.ToListAsync());
        }

        // GET: MembershipPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipPlan = await _context.MembershipPlans
                .FirstOrDefaultAsync(m => m.MembershipPlanId == id);
            if (membershipPlan == null)
            {
                return NotFound();
            }

            return View(membershipPlan);
        }

        // GET: MembershipPlans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MembershipPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MembershipPlanId,PlanName,DurationInMonths,Price")] MembershipPlan membershipPlan)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(membershipPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(membershipPlan);
        }


        // GET: MembershipPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipPlan = await _context.MembershipPlans.FindAsync(id);
            if (membershipPlan == null)
            {
                return NotFound();
            }
            return View(membershipPlan);
        }

        // POST: MembershipPlans/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MembershipPlanId,PlanName,DurationInMonths,Price")] MembershipPlan membershipPlan)
        {
            if (id != membershipPlan.MembershipPlanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membershipPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipPlanExists(membershipPlan.MembershipPlanId))
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
            return View(membershipPlan);
        }

        // GET: MembershipPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipPlan = await _context.MembershipPlans
                .FirstOrDefaultAsync(m => m.MembershipPlanId == id);
            if (membershipPlan == null)
            {
                return NotFound();
            }

            return View(membershipPlan);
        }

        // POST: MembershipPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membershipPlan = await _context.MembershipPlans.FindAsync(id);
            if (membershipPlan != null)
            {
                _context.MembershipPlans.Remove(membershipPlan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembershipPlanExists(int id)
        {
            return _context.MembershipPlans.Any(e => e.MembershipPlanId == id);
        }
    }
}
