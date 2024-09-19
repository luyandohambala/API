using AutoMapper;
using SMSAPI.Dto.ClassRoomDto;
using SMSAPI.Dto.DepartmentDto;
using SMSAPI.Dto.GuardianContactDto;
using SMSAPI.Dto.GuardianDto;
using SMSAPI.Dto.IngredientDto;
using SMSAPI.Dto.MealPaymentDto;
using SMSAPI.Dto.MealPaymentHistoryDto;
using SMSAPI.Dto.MealPreparedDto;
using SMSAPI.Dto.PupilDto;
using SMSAPI.Dto.Register;
using SMSAPI.Dto.ReportCardDto;
using SMSAPI.Dto.ReportCardSubjectDto;
using SMSAPI.Dto.StaffAddressDto;
using SMSAPI.Dto.StaffContactDto;
using SMSAPI.Dto.StaffDto;
using SMSAPI.Dto.SubjectDto;
using SMSAPI.Dto.TeacherAddressDto;
using SMSAPI.Dto.TeacherContactDto;
using SMSAPI.Dto.TeacherDto;
using SMSAPI.Models;

namespace SMSAPI.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //To Dto Mapping
            CreateMap<ClassRoom, ClassRoomReadDto>();
            CreateMap<ClassRoom, ClassRoomWithPupilsDto>(); 
            CreateMap<ClassRoom, ClassRoomWithSubjectsDto>();
            
            CreateMap<Subject, SubjectReadDto>();

            CreateMap<Pupil, PupilReadDto>();
            CreateMap<Pupil, PupilWithReportCardsDto>();

            CreateMap<ReportCard, ReportCardReadDto>();
            CreateMap<ReportCard, ReportCardWithReportCardSubjectsDto>();

            CreateMap<ReportCardSubjects, ReportCardSubjectReadDto>();

            CreateMap<Guardian, GuardianReadDto>();

            CreateMap<GuardianContact, GuardianContactReadDto>();

            CreateMap<Register, RegisterReadDto>();

            CreateMap<Ingredient, IngredientReadDto>();

            CreateMap<MealPayment, MealPaymentReadDto>();

            CreateMap<MealPaymentHistory, MealPaymentHistoryReadDto>();

            CreateMap<MealPrepared, MealPreparedReadDto>();

            CreateMap<TeacherContact, TeacherContactReadDto>();

            CreateMap<TeacherAddress, TeacherAddressReadDto>();

            CreateMap<Teacher, StaffReadDto>();

            CreateMap<Department, DepartmentReadDto>();

            CreateMap<Staff, StaffReadDto>();

            CreateMap<StaffContact, StaffContactReadDto>();

            CreateMap<StaffAddress, StaffAddressReadDto>();

            //From Dto Mapping
            CreateMap<ClassRoomCreateDto, ClassRoom>();
            CreateMap<ClassRoomUpdateDto, ClassRoom>();

            CreateMap<PupilCreateDto, Pupil>();
            CreateMap<PupilUpdateDto, Pupil>();

            CreateMap<ReportCardCreateDto, ReportCard>();
            CreateMap<ReportCardUpdateDto, ReportCard>();

            CreateMap<ReportCardSubjectCreateDto, ReportCardSubjects>();
            CreateMap<ReportCardSubjectUpdateDto, ReportCardSubjects>();

            CreateMap<SubjectReadDto, Subject>();
            CreateMap<SubjectUpdateDto, Subject>();

            CreateMap<GuardianCreateDto, Guardian>();
            CreateMap<GuardianUpdateDto, Guardian>();

            CreateMap<GuardianContactReadDto, GuardianContact>();
            CreateMap<GuardianContactUpdateDto, GuardianContact>();

            CreateMap<RegisterCreateDto, Register>();
            CreateMap<RegisterUpdateDto, Register>();

            CreateMap<IngredientCreateDto, Ingredient>();
            CreateMap<IngredientUpdateDto, Ingredient>();
            CreateMap<IngredientCalculateUpdateDto, Ingredient>();

            CreateMap<MealPaymentUpdateDto, MealPayment>();
            CreateMap<MealPaymentStatusUpdateDto, MealPayment>();
            CreateMap<MealPaymentCreateDto, MealPayment>();

            CreateMap<MealPaymentHistoryCreateDto, MealPaymentHistory>();
            CreateMap<MealPaymentHistoryUpdateDto, MealPaymentHistory>();

            CreateMap<MealPreparedCreateDto, MealPrepared>();
            CreateMap<MealPreparedUpdateDto, MealPrepared>();

            CreateMap<TeacherContactCreateDto, TeacherContact>();
            CreateMap<TeacherContactUpdateDto, TeacherContact>();

            CreateMap<TeacherAddressCreateDto, TeacherAddress>();
            CreateMap<TeacherAddressUpdateDto, TeacherAddress>();

            CreateMap<TeacherCreateDto, Teacher>();
            CreateMap<TeacherUpdateDto, Teacher>();

            CreateMap<DepartmentCreateDto, Department>();
            
            CreateMap<StaffContactCreateDto, StaffContact>();
            CreateMap<StaffContactUpdateDto, StaffContact>();

            CreateMap<StaffAddressCreateDto, StaffAddress>();
            CreateMap<StaffAddressUpdateDto, StaffAddress>();
            
            CreateMap<StaffCreateDto, Staff>();
            CreateMap<StaffUpdateDto, Staff>();
        }
    }
}
