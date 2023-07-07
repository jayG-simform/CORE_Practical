using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Practical17.Data;
using Practical17.Interface;
using Practical17.Models;
using System.Diagnostics;

namespace Practical17.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
     
        public IActionResult Index()
        {
            var res = _studentRepository.GetAllStudents();
            return View(res);
        }
        [HttpGet]
        [Authorize(Roles="Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student model)
        {
            if(ModelState.IsValid)
            {
                _studentRepository.Add(model);
                TempData["error"] = "Record Saved!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Empty filed can't Submit";
                return View(model);
            }
        }
        [Authorize(Roles="Admin")]
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || _studentRepository.GetAllStudents() == null)
            {
                return NotFound();
            }

            var student = _studentRepository.GetStudent(id ?? 0);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
            
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            _studentRepository.Delete(id);
            TempData["error"] = "Record Deleted!";
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles="Admin")]
        public IActionResult Edit(int id)
        {
            var st = _studentRepository.GetStudent(id);
            var res = new Student()
            {
                StudentName = st.StudentName,
                Age = st.Age,
                Email = st.Email,
            };
            return View(res);
        }
        [HttpPost]
        public IActionResult Edit(Student model) 
        {
            _studentRepository.Edit(model);
            TempData["error"] = "Record Updated!";
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var res = _studentRepository.Details(id);
            return View(res);
        }
    }
}
