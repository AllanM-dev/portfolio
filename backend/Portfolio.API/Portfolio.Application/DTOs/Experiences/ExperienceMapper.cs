using Portfolio.Domain.Entities;

namespace Portfolio.Application.DTOs.Experiences
{
    public static class ExperienceMapper
    {
        public static ExperienceDto ToDto(this Experience exp)
        {
            return new ExperienceDto
            {
                Id = exp.Id,
                Title = exp.Title,
                Company = exp.Company,
                StartDate = exp.StartDate,
                EndDate = exp.EndDate,
                Description = exp.Description,
                Skills = exp.Skills
            };
        }

        public static Experience ToEntity(this CreateExperienceDto dto)
        {
            return new Experience
            {
                Title = dto.Title,
                Company = dto.Company,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Description = dto.Description,
                Skills = dto.Skills
            };
        }

        public static Experience ToEntity(this UpdateExperienceDto dto)
        {
            return new Experience
            {
                Id = dto.Id,
                Title = dto.Title,
                Company = dto.Company,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Description = dto.Description,
                Skills = dto.Skills
            };
        }
    }
}
