using CRUDaspNetCoreWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDaspNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsApiController : ControllerBase
    {
        private readonly MyDbContext _context;

        //Getting object of MyDbContext class in constructor
        public StudentsApiController(MyDbContext context)
        {
            _context = context;    
        }

        //Creating all data fetch (READ) api functionality
        //GET: localhost:44338/api/StudentsApi => GetStudents()


        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            var data = await _context.Students.ToListAsync();

            //return 200 status code OK Http response with specified data
            return Ok(data);
        }

        //creating data fetch(READ) api functionality based on particular id
        //GET: localhost:44338/api/StudentsApi/1 => GetStudentById(int id)

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            //FindAsync(id) => check whether the id is present in Students table or not
            //then return the whole row to student variable, if present
            var student = await _context.Students.FindAsync(id);

            //checking if value received in student variable is null,
            //if null => return 404 notfound
            //else => return student (complete row for particular id)
            if(student == null)
            {
                return NotFound();
            }

            return student;
        }

        //Creating/Inserting data (CREATE) api functionality
        //POST: localhost:44338/api/StudentsApi
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student std)
        {
            //adding the new student in Students table
            await _context.Students.AddAsync(std);

            //Saving the changes in database table
            await _context.SaveChangesAsync();

            return Ok(std);
        }

        //Updating data api functionality
        //PUT: localhost:44338/api/StudentApi
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id, Student std)
        {
            //checking if entered id parameter is not equal to the id from std object
            //then return BadRequest 400
            if (id != std.Id)
            {
                return BadRequest();
            }

            //_context.Entry(std).State = EntityState.Modified; => marks the student object as modified, allowing Entity Framework to
            //know that it needs to update the record in the database when changes are saved.
            _context.Entry(std).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(std);
        }

        //Delete data api functionality
        //DELETE: localhost:44338/api/StudentApi
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            //Checking if id is present in Students table,
            //then pass the row of that id from table to std variable
            var std = await _context.Students.FindAsync(id);

            //check the value in std is null
            //then return 404 NotFound
            if (std == null)
            {
                return NotFound();
            }

            //if we find student, then Remove
            _context.Students.Remove(std);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
