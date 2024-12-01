using SMSAPI.Models;

namespace SMSAPI.Interfaces
{
    public interface IDepartmentRepository
    {
        //read methods
        ICollection<Department> GetDepartments();

        Department GetDepartment(Guid departmentId);

        ICollection<Teacher> GetTeachersByDepartmentId(Guid departmentId);

        ICollection<Staff> GetStaffByDepartmentId(Guid departmentId);   


        //write methods
        bool CreateDepartment(Department department);

        bool UpdateDepartment(Department department);

        bool Save();


        //delete methods
        bool DeleteDepartment(Department department);


        //validation methods
        bool DepartmentExists(Guid departmentId);
    }
}
