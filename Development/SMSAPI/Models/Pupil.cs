namespace SMSAPI.Models
{
    public class Pupil
    {
        public Guid PupilId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FirstLastName => FirstName + " " + LastName;

        public string Gender { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public Guid PupilClassId { get; set; }

        public Guid PupilGradeId { get; set; }

        public Guid PupilGuardianId { get; set; }


        //Navigation properties
        public ICollection<ReportCard> ReportCards { get; set; }

        public ICollection<MealPaymentHistory> MealPayments { get; set; }

        public MealPayment MealPayment { get; set; }    

        public Guardian Guardian { get; set; }

        public ClassRoom ClassRoom { get; set; } 

    }
}
