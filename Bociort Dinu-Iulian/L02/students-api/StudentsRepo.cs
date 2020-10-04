using System.Collections.Generic;

namespace students_api
{
    public static class StudentsRepo
    {
        public static List<Student> Student = new List<Student>(){
            new Student() { Id = 1, Nume = "Dinu", Facultate = "AC", AnUniversiatar = "4"},
            new Student() { Id = 2, Nume = "Florin", Facultate = "AC", AnUniversiatar = "2"},
            new Student() { Id = 3, Nume = "Madalin", Facultate = "AC", AnUniversiatar = "2"},
            new Student() { Id = 4, Nume = "Flavius", Facultate = "AC", AnUniversiatar = "3"}
        };
    }
}