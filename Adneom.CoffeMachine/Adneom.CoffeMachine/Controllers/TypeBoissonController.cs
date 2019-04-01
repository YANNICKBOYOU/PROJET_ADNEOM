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
    public class TypeBoissonController : Controller
    {
        //private readonly DbEntityContext _context;
        private readonly IGenericRepository<TypeBoisson> _repository;

        public TypeBoissonController(IGenericRepository<TypeBoisson> repository)
        {
            _repository = repository;
        }

        // GET: TypeBoisson
        public async Task<IActionResult> Index()
        {
           // return View(await _repository.TypeBoisson.ToListAsync());
            return View(await _repository.GetAllAsync());

        }

        // GET: TypeBoisson/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var typeBoisson = await _repository.GetByIdAsync(id.Value);

            //var typeBoisson = await _context.TypeBoisson
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (typeBoisson == null)
            {
                return NotFound();
            }

            return View(typeBoisson);
        }

        // GET: TypeBoisson/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeBoisson/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeBoisson1")] TypeBoisson typeBoisson)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(typeBoisson);
                //await _context.SaveChangesAsync();

                await _repository.AddAsync(typeBoisson);


                return RedirectToAction(nameof(Index));
            }
            return View(typeBoisson);
        }

        // GET: TypeBoisson/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

          //  var typeBoisson = await _context.TypeBoisson.FindAsync(id);

          var typeBoisson = await _repository.FindAsync(a => a.Id == id.Value);
            if (typeBoisson == null)
            {
                return NotFound();
            }
            return View(typeBoisson);
        }

        // POST: TypeBoisson/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeBoisson1")] TypeBoisson typeBoisson)
        {
            if (id != typeBoisson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync(typeBoisson);
                    //_context.Update(typeBoisson);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeBoissonExists(typeBoisson.Id))
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
            return View(typeBoisson);
        }

        // GET: TypeBoisson/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeBoisson = await _repository.FindAsync(a => a.Id == id.Value);

            //var typeBoisson = await _context.TypeBoisson
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (typeBoisson == null)
            {
                return NotFound();
            }

            return View(typeBoisson);
        }

        // POST: TypeBoisson/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var typeBoisson = await _repository.FindAsync(a => a.Id == id);
            await _repository.DeleteAsync(typeBoisson);
          

            return RedirectToAction(nameof(Index));
        }

        private bool TypeBoissonExists(int id)
        {
            return _repository.Exist(e => e.Id == id);
            //return _context.TypeBoisson.Any(e => e.Id == id);
        }
    }
}


//private readonly IGenericRepository〈User〉 _userRepository;
//public AccountController(
//IGenericRepository〈User〉 userRepository
//)
//{
//_userRepository = userRepository;
//}

//public async Task GetAllUser()
//{
//var data = await _userRepository.GetAllAsync();
//// reset of code
//}