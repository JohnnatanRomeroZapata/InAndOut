using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ExpenseTypeController : Controller
    {
        private readonly MyAppDbContext _myAppDbContext;

        public ExpenseTypeController(MyAppDbContext myAppDbContext)
        {
            _myAppDbContext = myAppDbContext;
        }

        public IActionResult IndexExpenseType()
        {
            IEnumerable<ExpenseType> expensesTypeList = _myAppDbContext.ExpenseTypes; 
            return View(expensesTypeList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseType expenseType)
        {
            if (ModelState.IsValid)
            {
                _myAppDbContext.ExpenseTypes.Add(expenseType);
                await _myAppDbContext.SaveChangesAsync();
                return RedirectToAction("IndexExpenseType");
            }

            return View(expenseType);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int expenseTypeId)
        {
            if(expenseTypeId == 0)
            {
                return NotFound();
            }

            ExpenseType expenseTypeToUpdate = await _myAppDbContext.ExpenseTypes.FindAsync(expenseTypeId);

            if(expenseTypeToUpdate == null)
            {
                return NotFound();
            }

            return View(expenseTypeToUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ExpenseType expenseTypeToUpdate)
        {
            if (ModelState.IsValid)
            {
                _myAppDbContext.ExpenseTypes.Update(expenseTypeToUpdate);
                await _myAppDbContext.SaveChangesAsync();
                return RedirectToAction("IndexExpenseType");
            }

            return View(expenseTypeToUpdate);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int expenseTypeId)
        {
            if (expenseTypeId == 0)
            {
                return NotFound();
            }

            ExpenseType expenseTypeToDelete = await _myAppDbContext.ExpenseTypes.FindAsync(expenseTypeId);

            if (expenseTypeToDelete == null)
            {
                return NotFound();
            }

            return View(expenseTypeToDelete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int expenseTypeId)
        {
            if (expenseTypeId == 0)
            {
                return NotFound();
            }

            ExpenseType expenseTypeToDelete = await _myAppDbContext.ExpenseTypes.FindAsync(expenseTypeId);

            if (expenseTypeToDelete == null)
            {
                return NotFound();
            }

            _myAppDbContext.ExpenseTypes.Remove(expenseTypeToDelete);
            await _myAppDbContext.SaveChangesAsync();

            return RedirectToAction("IndexExpenseType");
        }
    }
}
