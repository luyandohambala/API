using Microsoft.EntityFrameworkCore;
using SMSAPI.Models;

namespace SMSAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }


        //classroom table
        public DbSet<ClassRoom> ClassRooms { get; set; }

        //pupils table
        public DbSet<Pupil> Pupils { get; set; }

        //subjects table
        public DbSet<Subject> Subjects { get; set; }

        //ReportCard table
        public DbSet<ReportCard> ReportCards { get; set; }

        //ReportCardSubjects table
        public DbSet<ReportCardSubjects> ReportCardSubjects { get; set; }

        //Guardians table
        public DbSet<Guardian> Guardians { get; set; }

        //GuardianContact table
        public DbSet<GuardianContact> GuardianContacts { get; set; }

        //Grade table
        public DbSet<Grade> Grades { get; set; }

        //pupil registertable
        public DbSet<Register> Registers { get; set; }

        //classroom register table
        public DbSet<ClassRegister> ClassRegisters { get; set; }

        //MealPayment History table
        public DbSet<MealPaymentHistory> mealPaymentHistories { get; set; }

        //Mealpayment status table
        public DbSet<MealPayment> mealPayments { get; set; }

        //Ingredients table
        public DbSet<Ingredient> Ingredients { get; set; }


        //Join tables
        public DbSet<ClassRoomSubject> ClassRoomSubjects { get; set; }

        public DbSet<ReportCardReportCardSubject> ReportCardReportCardSubjects { get; set; }

        public DbSet<ClassRoomRegister> ClassRoomRegisters { get; set; }


        //model builder section
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ///
            /// many to many joins logic here 
            ///
            //classroom subject logic here 
            modelBuilder.Entity<ClassRoomSubject>()
                .HasKey(crs => new { crs.ClassRoomId, crs.SubjectId });
            modelBuilder.Entity<ClassRoomSubject>()
                .HasOne(cr => cr.ClassRoom).WithMany(crs => crs.Subjects).HasForeignKey(s => s.ClassRoomId);
            modelBuilder.Entity<ClassRoomSubject>()
                .HasOne(s => s.Subject).WithMany(crs => crs.ClassRooms).HasForeignKey(s => s.SubjectId);

            //reportcard join logic here
            modelBuilder.Entity<ReportCardReportCardSubject>()
                .HasKey(rcrcs => new { rcrcs.ReportCardId, rcrcs.ReportCardSubjectId });
            modelBuilder.Entity<ReportCardReportCardSubject>()
                .HasOne(rc => rc.ReportCard).WithMany(rcrcs => rcrcs.ReportCardSubjects).HasForeignKey(rc => rc.ReportCardId);
            modelBuilder.Entity<ReportCardReportCardSubject>()
                .HasOne(rcs => rcs.ReportCardSubject).WithMany(rcrcs => rcrcs.ReportCards).HasForeignKey(rcs => rcs.ReportCardSubjectId);

            //classroom register join logic here 
            modelBuilder.Entity<ClassRoomRegister>()
                .HasKey(crr => new { crr.ClassRoomId, crr.RegisterId });
            modelBuilder.Entity<ClassRoomRegister>()
                .HasOne(cr => cr.ClassRoom).WithMany(crr => crr.Registers).HasForeignKey(cr => cr.ClassRoomId);
            modelBuilder.Entity<ClassRoomRegister>()
                .HasOne(r => r.Register).WithMany(crr => crr.ClassRegisters).HasForeignKey(r => r.RegisterId);




            //one to many mappings
            modelBuilder.Entity<ClassRoom>()
                .HasMany(p => p.Pupils).WithOne(cr => cr.ClassRoom).HasForeignKey(p => p.PupilClassId);
            modelBuilder.Entity<ClassRoom>()
                .HasMany(rc => rc.ReportCards).WithOne(cr => cr.ClassRoom).HasForeignKey(rc => rc.ReportCardClassId);

            modelBuilder.Entity<Pupil>()
                .HasMany(rc => rc.ReportCards).WithOne(p => p.Pupil).HasForeignKey(rc => rc.ReportCardPupilId);
            modelBuilder.Entity<Pupil>()
                .HasMany(mph => mph.MealPayments).WithOne(p => p.Pupil).HasForeignKey(mph => mph.MealPaymentPupilId);

            modelBuilder.Entity<Grade>()
                .HasMany(cr => cr.ClassRooms).WithOne(gr => gr.Grade).HasForeignKey(cr => cr.ClassGradeId);


            //one to one mapping
            modelBuilder.Entity<Guardian>()
                .HasMany(p => p.Pupils).WithOne(g => g.Guardian).HasForeignKey(p => p.PupilGuardianId);
            modelBuilder.Entity<Guardian>()
                .HasOne(gc => gc.Contact).WithOne(g => g.Guardian).HasForeignKey<GuardianContact>(gc => gc.GuardianContactGuardianId);

            modelBuilder.Entity<Pupil>()
                .HasOne(mp => mp.MealPayment).WithOne(p => p.Pupil).HasForeignKey<MealPayment>(mp => mp.MealPaymentPupilId);

        }
    }
}
