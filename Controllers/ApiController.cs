using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using RestApi.Models;
using RestApi.Data;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Api
        [HttpGet]
        public ActionResult<IEnumerable<StudentModel>> GetAll()
        {
            return _context.Students.ToList();
        }

        // GET: api/Api/5
        [HttpGet("{id}")]
        public ActionResult<StudentModel> GetById(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }

        // POST: api/Api
        [HttpPost]
        public ActionResult<StudentModel> Create(StudentModel student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }

        // PUT: api/Api/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, StudentModel student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        // DELETE: api/Api/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            _context.SaveChanges();
            return Ok();
        }
    }
}