namespace SMSAPI.Models
{
    public class Teacher
    {
        public Guid TeacherId { get; set; }

        public string TeacherTitle { get; set; }

        public string TeacherFirstName { get; set; }

        public string TeacherLastName { get; set; }

        public string TeacherDepartmentId { get; set; }

        public string TeacherGrade { get; set; }


        //Navigation properties
        public ICollection<TeacherSubject> Subjects { get; set; }

        public ICollection<TeacherGrades> Grades { get; set; }

        public TeacherContact TeacherContact { get; set; }

        public TeacherAddress TeacherAddress { get; set; }

        public DepartmentTeacher Department { get; set; }
    }
}
