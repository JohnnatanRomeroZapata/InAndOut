using InAndOut.Data;
using InAndOut.Models;
using InAndOut.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly MyAppDbContext _myAppDbContext;

        public ExpenseController(MyAppDbContext myAppDbContext)
        {
            _myAppDbContext = myAppDbContext;
        }

        public IActionResult IndexExpenses()
        {
            IEnumerable<Expense> expensesList = _myAppDbContext.Expenses;
            //IEnumerable<Expense> expensesList = _myAppDbContext.Expenses.ToList();

            foreach (var exp in expensesList)
            {
                exp.ExpenseType = _myAppDbContext.ExpenseTypes.FirstOrDefault(e => e.ExpenseTypeId == exp.ExpenseTypeId);
            }

            return View(expensesList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //IEnumerable<SelectListItem> ddl = _myAppDbContext.ExpenseTypes.Select(item => new SelectListItem
            //{
            //    Value = item.ExpenseTypeId.ToString(),
            //    Text = item.ExpenseTypeName
            //});

            //ViewBag.Ddl = ddl;

            ExpenseVM expenseVM = new ExpenseVM()
            {
                TheExpense = new Expense(),
                ddl = _myAppDbContext.ExpenseTypes.Select(item => new SelectListItem
                {
                    Text = item.ExpenseTypeName,
                    Value = item.ExpenseTypeId.ToString()
                })
            };

            return View(expenseVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseVM newExpenseVM)
        {
            if (ModelState.IsValid)
            {
                _myAppDbContext.Expenses.Add(newExpenseVM.TheExpense);
                await _myAppDbContext.SaveChangesAsync();
                return RedirectToAction("IndexExpenses");
            }

            newExpenseVM.ddl = _myAppDbContext.ExpenseTypes.Select(item => new SelectListItem
            {
                Text = item.ExpenseTypeName,
                Value = item.ExpenseTypeId.ToString()
            });

            return View(newExpenseVM);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Expense expense = await _myAppDbContext.Expenses.FindAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            ExpenseVM expenseVM = new ExpenseVM()
            {
                TheExpense = expense,
                ddl = _myAppDbContext.ExpenseTypes.Select(item => new SelectListItem
                {
                    Text = item.ExpenseTypeName,
                    Value = item.ExpenseTypeId.ToString()
                })
            };

            return View(expenseVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ExpenseVM theExpenseToUpdate)
        {
            if (ModelState.IsValid)
            {
                var theEx = theExpenseToUpdate.TheExpense;
                _myAppDbContext.Expenses.Update(theEx);
                await _myAppDbContext.SaveChangesAsync();
                return RedirectToAction("IndexExpenses");
            }
            return View(theExpenseToUpdate);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? expenseId)
        {
            if (expenseId == null || expenseId == 0)
            {
                return NotFound();
            }

            Expense expense = await _myAppDbContext.Expenses.FindAsync(expenseId);
            ExpenseType expenseType = await _myAppDbContext.ExpenseTypes.FindAsync(expense.ExpenseTypeId);
            expense.ExpenseType = expenseType;

            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? expenseId)
        {
            if (expenseId == 0)
            {
                return NotFound();
            }

            Expense expense = await _myAppDbContext.Expenses.FindAsync(expenseId);

            if (expense == null)
            {
                return NotFound();
            }

            _myAppDbContext.Expenses.Remove(expense);
            await _myAppDbContext.SaveChangesAsync();

            return RedirectToAction("IndexExpenses");
        }
    }
}
