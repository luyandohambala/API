using SMSAPI.Models;

namespace SMSAPI.Interfaces
{
    public interface IPupilRepository
    {
        //read methods
        ICollection<Pupil> GetPupils();

        Pupil GetPupil(Guid pupilId);

        ICollection<ReportCard> GetReportCardByPupilId(Guid pupilId);

        Guid GetGuardianIdByPupilId(Guid pupilId);

        Guid GetGradeIdByPupilId(Guid pupilId); 

        Guid GetClassIdByPupilId(Guid pupilId);

        
        //write methods
        bool CreatePupil(Pupil pupil);

        bool UpdatePupil(Pupil pupil);

        bool Save();


        //delete methods
        bool DeletePupil(Pupil pupil);

        bool DeletePupils(List<Pupil> pupils);


        //validation methods
        bool PupilExists(Guid pupilId);
    }
}
