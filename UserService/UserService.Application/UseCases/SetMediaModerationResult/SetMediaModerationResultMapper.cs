using AutoMapper;

namespace UserService.Application.UseCases.SetMediaModerationResult
{
    internal class SetMediaModerationResultMapper : Profile
    {
        public SetMediaModerationResultMapper()
        {
            CreateMap<Shared.Objects.ModerationResult, Domain.ModerationResult>();
        }
    }
}
