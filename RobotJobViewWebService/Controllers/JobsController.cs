using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RobotJobViewWebService.Data;
using RobotJobViewWebService.Models;

namespace RobotJobViewWebService.Controllers
{
    public class JobsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Jobs
        [Authorize]
        public async Task<IActionResult> Index()
        {
            string UserName = HttpContext.User.Identity.Name;
            return View(await _context.Job.Where(s => s.User == UserName).ToListAsync());
        }

        // GET: Jobs/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .FirstOrDefaultAsync(m => m.ID == id);
            if (job == null)
            {
                return NotFound();
            }
            ViewData["HasResult"] = "false";
            if (_context.Result.Any(s => s.Job == job))
            {
                ViewData["HasResult"] = "true";
                var result = await _context.Result.FirstOrDefaultAsync(s => s.Job == job);
                ViewData["ResultID"] = result.ID.ToString();
                ViewData["FileName"] = result.filename.ToString();
            }
            return View(job);
        }

        // GET: Jobs/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("JobName,Type,Arg")] Job job)
        {
            if (ModelState.IsValid)
            { Job newJob = new Job{ };
                newJob.JobName = job.JobName;
                newJob.Status = "Added";
                newJob.Type = job.Type;
                newJob.Arg = job.Arg;
                newJob.CreateTime = DateTime.Now;
                newJob.User = HttpContext.User.Identity.Name;
                _context.Add(newJob);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,JobName,Type,Arg,Status,User,CreateTime,EndTime")] Job job)
        {
            if (id != job.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.ID))
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
            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .FirstOrDefaultAsync(m => m.ID == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Job.FindAsync(id);
            var result = await _context.Result.Where(s=>s.Job==job).ToListAsync();
            foreach (var thisresult in result)
            {
                _context.Result.Remove(thisresult);
            }
            _context.Job.Remove(job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return _context.Job.Any(e => e.ID == id);
        }
    }
}
