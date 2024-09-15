namespace SMSAPI.Models
{
    public class ClassRoom
    {
        public Guid ClassId { get; set; }

        public string ClassName { get; set; }

        public Guid ClassGradeId { get; set; }

        public int PupilTotal { get; set; }

        
        //navigation properties  
        public Grade Grade { get; set; }

        public ICollection<Pupil> Pupils { get; set; }

        public ICollection<ClassRoomSubject> Subjects { get; set; }

        public ICollection<ReportCard> ReportCards { get; set; }    
    }
}
