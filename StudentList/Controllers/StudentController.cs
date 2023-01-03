using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StudentList.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Student student { get; set; }
        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public Student GetStudentById(int id)
        {
            return _db.Students.FirstOrDefault(u => u.Id == id); ;
        }

        public Student GetStudentByName(string name)
        {
            return _db.Students.FirstOrDefault(u => u.Name.Equals(name));
        }

        public Student GetStudentByFaculty(string faculty)
        {
            return _db.Students.FirstOrDefault(u => u.Faculty.Equals(faculty));
        }

        public IActionResult Upsert(int? id)
        {
            student = new Student();
            if (id == null)
            {
                //create
                return View(student);
            }
            //update
            student = _db.Students.FirstOrDefault(u => u.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (student.Id == 0)
                {
                    //create
                    _db.Students.Add(student);
                }
                else
                {
                    _db.Students.Update(student);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Students.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _db.Students.FirstOrDefaultAsync(u => u.Id == id);
            if (student == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Students.Remove(student);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}