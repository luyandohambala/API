﻿using SMSAPI.Models;

namespace SMSAPI.Dto.ClassRoomDto
{
    public class ClassRoomWithPupilsDto
    {
        public Guid ClassId { get; set; }

        public string ClassName { get; set; }

        public int PupilTotal { get; set; }

        public ICollection<Pupil> Pupils { get; set; }
    }
}