using AutoMapper;
using SMSAPI.Dto.ClassRoomDto;
using SMSAPI.Dto.GuardianContactDto;
using SMSAPI.Dto.GuardianDto;
using SMSAPI.Dto.IngredientDto;
using SMSAPI.Dto.PupilDto;
using SMSAPI.Dto.Register;
using SMSAPI.Dto.ReportCardDto;
using SMSAPI.Dto.ReportCardSubjectDto;
using SMSAPI.Dto.SubjectDto;
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
        }
    }
}
