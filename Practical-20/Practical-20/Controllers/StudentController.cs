using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Practical_20.Interface;
using Practical_20.Models;
using Practical_20.Utility;
using System.Diagnostics;

namespace Practical_20.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentRepo<Student> _repository;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IUnitOfWork unitOfWork, ILogger<StudentController> logger)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetStudentRepo<Student>();
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("List of Students!");
            return _repository.GetAll() != null ?
                        View(_repository.GetAll().ToList()) :
                        Problem("Entity set 'AddDbContext.Students'  is null.");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _repository.GetAll() == null)
            {
                _logger.LogError("Student Data not found");
                return NotFound();
            }

            var student =  _repository.GetById(id ?? 0);
            if (student == null)
            {
                _logger.LogError("Student Data not found");
                return NotFound();
            }
            _logger.LogInformation($"Student Detail Show {student.Id}");
            return View(student);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateEmailDomain(allowDomain: "gmail.com", ErrorMessage = "Email domain must be gmail.com")]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Student Added");
                _repository.Insert(student);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {

            }
            _logger.LogWarning("Student Data is not valid!");
            return View(student);  
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _repository.GetAll() == null)
            {
                return NotFound();
            }

            var student = _repository.GetById(id ?? 0);

            if (student == null)
            {
                _logger.LogCritical("Requested Data is not in database!");

                return NotFound();
            }

            return View(student);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_repository.GetAll() == null)
            {
                return Problem("Entity set 'DatabaseContext.Students'  is null.");
            }
            var student = _repository.GetById(id);
            if (student != null)
            {
                _repository.Delete(student);
                _logger.LogCritical("Requested Data is not in database!");

            }

            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || _repository.GetAll() == null)
            {
                return NotFound();
            }

            var student = _repository.GetById(id ?? 0);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirm(int id,Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _logger.LogInformation($"{student.Name}'s data has been Updated!");
                _repository.Update(student);
                await _unitOfWork.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}