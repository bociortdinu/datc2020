using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace students_api.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return StudentsRepo.Student;
        }

        [HttpGet("{id}")]
        public Student Get(int id)
        {
            foreach (Student st in StudentsRepo.Student)
            {
                if (st.Id == id)
                {
                    return st;
                }
            }
            return null;
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Student student = StudentsRepo.Student.Find(x => x.Id == id);
            if (student == null)
            {

            }
            else
            {
                StudentsRepo.Student.Remove(student);
            }

            foreach (Student st in StudentsRepo.Student)
            {
                if (st.Id == id)
                {
                    return BadRequest();
                }
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Student stud)
        {
            StudentsRepo.Student.Add(stud);

            foreach (Student st in StudentsRepo.Student)
            {
                if (st.Id == stud.Id)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            return Ok();
        }

        [HttpPut]
        public string Put([FromBody] Student stud)
        {
            try
            {
                foreach (Student st in StudentsRepo.Student)
                {
                    if (st.Id == stud.Id)
                    {
                        st.Nume = stud.Nume;
                        st.Facultate = stud.Facultate;
                        st.AnUniversiatar = stud.AnUniversiatar;

                        return "StudentsRepo.Student";
                    }
                }
                return "null";
            }
            catch (System.Exception e)
            {
                return "Eroare! :" + e.Message;
                throw;
            }

        }


    }
}
