using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practical_18.DataContext;
using Practical_18.Repository;
using DataAccessLayer.Model;
using DataAccessLayer.Interface;
using DataAccessLayer.ViewModel;

namespace Practical_18.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _context;

        public StudentsController(IStudentRepository context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetStudents()
        {
            try
            {
                return Ok(await _context.GetAllStudents()); 
            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retriving data from database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetStudent(int id)
        {
            try
            {
                return Ok(await _context.GetStudent(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retriving data from database");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutStudent(int id, StudentView student)
        {
            try
            {
                if (id != student.Id)
                {
                    return BadRequest("Id Mismatched");
                }
                var StUpdate = await _context.GetStudent(id);
                if(StUpdate == null) 
                {
                    return NotFound($"Student id={id} not found");
                }
                return Ok(await _context.Edit(student));


            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retriving data from database");
            }            
        }

        [HttpPost]
        public async Task<ActionResult> PostStudent(StudentView student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (student == null) return BadRequest(ModelState);
                    int studentId = await _context.Add(student);
                    student.Id = studentId;
                    return CreatedAtAction("GetStudents", new { id = studentId }, student);
                }
                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retriving data from database");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var empId = await _context.GetStudent(id);
                if (empId == null)
                {
                    return NotFound($"Student id={id} not found");
                }
                return Ok(await _context.Delete(id));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retriving data from database");
            }
        }

    }
}
