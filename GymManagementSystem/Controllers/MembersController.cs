using GymManagementSystem.Data;
using GymManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Controllers
{
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            var members = _context.Members
                .Include(m => m.Trainer)
                .Include(m => m.MembershipPlan);

            return View(await members.ToListAsync());
        }


        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.MembershipPlan)
                .Include(m => m.Trainer)
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            ViewData["MembershipPlanId"] =
                new SelectList(_context.MembershipPlans,"MembershipPlanId","PlanName");

            ViewData["TrainerId"] =
                new SelectList(_context.Trainers, "TrainerId","FullName");

            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,FullName,PhoneNumber,Email,DateOfBirth,JoinDate,IsActive,MembershipPlanId,TrainerId")] Member member)
        {
            if (ModelState.IsValid)
            {
                var plan = await _context.MembershipPlans
                    .FindAsync(member.MembershipPlanId);

                member.ExpiryDate =
                    member.JoinDate.AddMonths(plan.DurationInMonths);

                _context.Add(member);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["MembershipPlanId"] =
                new SelectList(_context.MembershipPlans, "MembershipPlanId", "PlanName", member.MembershipPlanId);

            ViewData["TrainerId"] =
                new SelectList(_context.Trainers, "TrainerId", "FullName", member.TrainerId);

            return View(member);
        }


        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var member = await _context.Members.FindAsync(id);

            ViewData["MembershipPlanId"] =
                new SelectList(_context.MembershipPlans,"MembershipPlanId","PlanName", member.MembershipPlanId);
            ViewData["TrainerId"] =
                new SelectList(_context.Trainers,"TrainerId","FullName",member.TrainerId);

            return View(member);
        }


        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,FullName,PhoneNumber,Email,DateOfBirth,JoinDate,IsActive,MembershipPlanId,TrainerId")] Member member)
        {
            if (id != member.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberId))
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
            ViewData["MembershipPlanId"] = new SelectList(_context.MembershipPlans, "MembershipPlanId", "PlanName", member.MembershipPlanId);
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "TrainerId", "FullName", member.TrainerId);
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.MembershipPlan)
                .Include(m => m.Trainer)
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.MemberId == id);
        }
    }
}
