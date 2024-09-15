using SMSAPI.Models;

namespace SMSAPI.Interfaces
{
    public interface IClassRoomRepository
    {
        //read methods
        ICollection<ClassRoom> GetClassRooms(); 

        ICollection<Subject> GetSubjectsByClassRoomId(Guid classRoomId);

        ICollection<Pupil> GetPupilsByClassRoomId(Guid classRoomId);

        ClassRoom GetClassRoom(Guid classRoomId);

        Guid GetGradeByClassRoomId(Guid classRoomId);


        //Write methods
        bool CreateClassRoom(ClassRoom classRoom);

        bool UpdateClassRoom(ClassRoom classRoom);

        bool Save();


        //delete methods
        bool DeleteClassRoom(ClassRoom classRoomId);

        bool DeleteClassRooms(List<ClassRoom> classRooms);


        //validation method
        bool ClassRoomExists(Guid classRoomId);

    }
}
