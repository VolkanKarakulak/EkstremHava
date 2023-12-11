using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EkstremHava.Data;
using EkstremHava.Models;
using NuGet.Packaging;

namespace EkstremHava.Controllers
{
    public class GalleryPhotoAdminController : AdminController
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public GalleryPhotoAdminController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: GalleryPhotoAdmin
        public async Task<IActionResult> Index()
        {
              return _context.GalleryPhotos != null ? 
                          View(await _context.GalleryPhotos.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.GalleryPhotos'  is null.");
        }

        // GET: GalleryPhotoAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GalleryPhotos == null)
            {
                return NotFound();
            }

            var galleryPhoto = await _context.GalleryPhotos
                .FirstOrDefaultAsync(m => m.GalleryPhotoId == id);
            if (galleryPhoto == null)
            {
                return NotFound();
            }

            return View(galleryPhoto);
        }

        // GET: GalleryPhotoAdmin/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: GalleryPhotoAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("GalleryPhotoId,PhotoName,Description,Date,LocationName")] GalleryPhoto galleryPhoto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(galleryPhoto);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(galleryPhoto);
        //}

        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GalleryPhotoId,PhotoName,Description,Date,LocationName")] GalleryPhoto photo, IFormFile PhotoName)
        {

            if (ModelState.IsValid)
            {

                string uploadedFileName = PhotoName.FileName;

                string uploadedFileExtension = System.IO.Path.GetExtension(uploadedFileName);

                string fileNameToUse = Guid.NewGuid().ToString() + uploadedFileExtension;

                string savePath = Path.Combine(_environment.WebRootPath, "img/", fileNameToUse);

                FileStream stream = new FileStream(savePath, FileMode.Create);

                PhotoName.CopyTo(stream);

                photo.PhotoName = fileNameToUse;


                _context.Add(photo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
          
            return View(photo);
        }


        // GET: GalleryPhotoAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GalleryPhotos == null)
            {
                return NotFound();
            }

            var galleryPhoto = await _context.GalleryPhotos.FindAsync(id);

            if (galleryPhoto == null)
            {
                return NotFound();
            }

            return View(galleryPhoto);
        }

        // POST: GalleryPhotoAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GalleryPhotoId,PhotoName,Description,Date,LocationName")] GalleryPhoto galleryPhoto)
        {
            if (id != galleryPhoto.GalleryPhotoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(galleryPhoto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalleryPhotoExists(galleryPhoto.GalleryPhotoId))
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
            return View(galleryPhoto);
        }

        // GET: GalleryPhotoAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GalleryPhotos == null)
            {
                return NotFound();
            }

            var galleryPhoto = await _context.GalleryPhotos
                .FirstOrDefaultAsync(m => m.GalleryPhotoId == id);
            if (galleryPhoto == null)
            {
                return NotFound();
            }

            return View(galleryPhoto);
        }

        // POST: GalleryPhotoAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GalleryPhotos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GalleryPhotos'  is null.");
            }
            var galleryPhoto = await _context.GalleryPhotos.FindAsync(id);
            if (galleryPhoto != null)
            {
                _context.GalleryPhotos.Remove(galleryPhoto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GalleryPhotoExists(int id)
        {
          return (_context.GalleryPhotos?.Any(e => e.GalleryPhotoId == id)).GetValueOrDefault();
        }
    }
}
