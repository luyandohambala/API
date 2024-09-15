namespace SMSAPI.Dto.GuardianDto
{
    public class GuardianCreateDto
    {
        public Guid GuardianId { get; set; }

        public string TitleOne { get; set; }

        public string GuardianOneName { get; set; }
        virtual
        public string TitleTwo
        { get; set; }

        public string GuardianTwoName { get; set; }
    }
}
