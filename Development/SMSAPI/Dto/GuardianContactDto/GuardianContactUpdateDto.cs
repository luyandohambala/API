﻿namespace SMSAPI.Dto.GuardianContactDto
{
    public class GuardianContactUpdateDto
    {
        public Guid GuardianContactId { get; set; }

        public string GuardianContactOnePhone { get; set; }

        public string GuardianContactOneEmail { get; set; }

        public string GuardianContactTwoPhone { get; set; }

        public string GuardianContactTwoEmail { get; set; }
    }
}
