using SMSAPI.Data;
using SMSAPI.Interfaces;
using SMSAPI.Models;
using System.Runtime.Intrinsics.Arm;

namespace SMSAPI.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _context;

        public DepartmentRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateDepartment(Department department)
        {
            _context.Add(department);

            return Save();
        }

        public bool DeleteDepartment(Department department)
        {
            _context.Remove(department);

            return Save();
        }

        public bool DepartmentExists(Guid departmentId)
        {
            return _context.Departments.Any(d => d.DepartmentId == departmentId);
        }

        public Department GetDepartment(Guid departmentId)
        {
            return _context.Departments.Where(d => d.DepartmentId == departmentId).FirstOrDefault();
        }

        public ICollection<Department> GetDepartments()
        {
            return [.. _context.Departments.OrderBy(d => d.DepartmentId)];
        }

        public ICollection<Teacher> GetTeachersByDepartmentId(Guid departmentId)
        {
            return [.. _context.DepartmentTeachers.Where(dt => dt.DepartmentId == departmentId).Select(t => t.Teacher)];
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool UpdateDepartment(Department department)
        {
            _context.Update(department);

            return Save();
        }
    }
}
