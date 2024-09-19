﻿using SMSAPI.Models;

namespace SMSAPI.Dto.StaffAddressDto
{
    public class StaffAddressUpdateDto
    {
        public Guid StaffAddressId { get; set; }

        public string Area { get; set; }

        public string Street { get; set; }

        public string HouseNo { get; set; }
    }
}