using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ItemController : Controller
    {
        private readonly MyAppDbContext _myAppDbContext;

        public ItemController(MyAppDbContext myAppDbContext)
        {
            _myAppDbContext = myAppDbContext;
        }

        public IActionResult IndexItem()
        {
            IEnumerable<Item> itemsList = _myAppDbContext.Items;
            
            return View(itemsList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Item newItem)
        {
            if(ModelState.IsValid)
            {
                _myAppDbContext.Items.Add(newItem);
                await _myAppDbContext.SaveChangesAsync();
                return RedirectToAction("IndexItem");
            }
                        
            return View(newItem);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int itemId)
        {
            Item itemToUpdate = await _myAppDbContext.Items.FindAsync(itemId);

            if(itemToUpdate == null)
            {
                return NotFound();
            }

            return View(itemToUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Item itemToUpdate)
        {
            if (ModelState.IsValid)
            {
                _myAppDbContext.Items.Update(itemToUpdate);
                await _myAppDbContext.SaveChangesAsync();
                return RedirectToAction("IndexItem");
            }

            return View(itemToUpdate);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int itemId)
        {
            Item itemToDelete = await _myAppDbContext.Items.FindAsync(itemId);

            if(itemToDelete == null)
            {
                return NotFound();
            }

            return View(itemToDelete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int itemId)
        {
            if(itemId == 0)
            {
                return NotFound();
            }

            Item itemToDelete = await _myAppDbContext.Items.FindAsync(itemId);

            if (itemToDelete != null)
            {
                _myAppDbContext.Items.Remove(itemToDelete);
                await _myAppDbContext.SaveChangesAsync();
                return RedirectToAction("IndexItem");
            }

            return View();
        }
    }
}
