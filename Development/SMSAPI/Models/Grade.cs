namespace SMSAPI.Models
{
    public class Grade
    {
        public Guid GradeId { get; set; }

        public string GradeName { get; set; }


        //Navigation properties
        public ICollection<ClassRoom> ClassRooms { get; set; }

        public ICollection<TeacherGrades> TeacherGrades { get; set; }   
    }

}