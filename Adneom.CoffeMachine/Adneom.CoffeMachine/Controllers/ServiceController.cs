using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Adneom.CoffeMachine.Domain.Entities;
using Adneom.CoffeMachine.Domain.Repository;

namespace Adneom.CoffeMachine.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IGenericRepository<ServiceMachine> _context;
        private readonly DbEntityContext _entityContext;
        public ServiceController(IGenericRepository<ServiceMachine> context, DbEntityContext entityContext)
        {
            _context = context;
            _entityContext = entityContext;
        }

        // GET: Service
        public async Task<IActionResult> Index()
        {
           // var dbEntityContext = _context.ServiceMachine.Include(s => s.Machine).Include(s => s.Operateur).Include(s => s.TypeBoisson);
            var dbEntityContext = _context.Query().Include(s => s.Machine)
                .Include(s => s.Operateur)
                .Include(s => s.TypeBoisson);

            return View(await dbEntityContext.ToListAsync());
        }

        // GET: Service/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceMachine = await _context.Query()
                .Include(s => s.Machine)
                .Include(s => s.Operateur)
                .Include(s => s.TypeBoisson)
                .FirstOrDefaultAsync(m => m.Id == id);

            //var serviceMachine = await _context.ServiceMachine
            //    .Include(s => s.Machine)
            //    .Include(s => s.Operateur)
            //    .Include(s => s.TypeBoisson)
            //    .FirstOrDefaultAsync(m => m.Id == id);

            if (serviceMachine == null)
            {
                return NotFound();
            }

            return View(serviceMachine);
        }

        // GET: Service/Create
        public IActionResult Create()
        {
            ViewData["MachineId"] = new SelectList(_entityContext.Machine, "Id", "Nom");
            ViewData["OperateurId"] = new SelectList(_entityContext.OperateurService, "Id", "Operateur");
            ViewData["TypeBoissonId"] = new SelectList(_entityContext.TypeBoisson, "Id", "TypeBoisson1");
            return View();
        }

        // POST: Service/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MachineId,TypeBoissonId,QuantiteSucre,OperateurId,AvecMug")] ServiceMachine serviceMachine)
        {
            if (ModelState.IsValid)
            {
                //_context.AddAsync(serviceMachine);
                await _context.AddAsync(serviceMachine);
                return RedirectToAction(nameof(Index));
            }
            ViewData["MachineId"] = new SelectList(_entityContext.Machine, "Id", "Nom", serviceMachine.MachineId);
            ViewData["OperateurId"] = new SelectList(_entityContext.OperateurService, "Id", "Operateur", serviceMachine.OperateurId);
            ViewData["TypeBoissonId"] = new SelectList(_entityContext.TypeBoisson, "Id", "TypeBoisson1", serviceMachine.TypeBoissonId);
            return View(serviceMachine);
        }

        // GET: Service/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceMachine = await _context.FindAsync(a => a.Id == id.Value);
            if (serviceMachine == null)
            {
                return NotFound();
            }
            ViewData["MachineId"] = new SelectList(_entityContext.Machine, "Id", "Nom", serviceMachine.MachineId);
            ViewData["OperateurId"] = new SelectList(_entityContext.OperateurService, "Id", "Operateur", serviceMachine.OperateurId);
            ViewData["TypeBoissonId"] = new SelectList(_entityContext.TypeBoisson, "Id", "TypeBoisson1", serviceMachine.TypeBoissonId);
            return View(serviceMachine);
        }

        // POST: Service/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MachineId,TypeBoissonId,QuantiteSucre,OperateurId,AvecMug")] ServiceMachine serviceMachine)
        {
            if (id != serviceMachine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.UpdateAsync(serviceMachine);
                   // await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceMachineExists(serviceMachine.Id))
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
            ViewData["MachineId"] = new SelectList(_entityContext.Machine, "Id", "Nom", serviceMachine.MachineId);
            ViewData["OperateurId"] = new SelectList(_entityContext.OperateurService, "Id", "Operateur", serviceMachine.OperateurId);
            ViewData["TypeBoissonId"] = new SelectList(_entityContext.TypeBoisson, "Id", "TypeBoisson1", serviceMachine.TypeBoissonId);
            return View(serviceMachine);
        }

        // GET: Service/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceMachine = await _context.Query()
                .Include(s => s.Machine)
                .Include(s => s.Operateur)
                .Include(s => s.TypeBoisson)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceMachine == null)
            {
                return NotFound();
            }

            return View(serviceMachine);
        }

        // POST: Service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceMachine = await _context.GetByIdAsync(id);

            //_context.ServiceMachine.Remove(serviceMachine);
            //await _context.SaveChangesAsync();
           
            await _context.DeleteAsync(serviceMachine);

            return RedirectToAction(nameof(Index));
        }

        private bool ServiceMachineExists(int id)
        {
            return _context.Exist(e => e.Id == id);
        }
    }
}
