using AutoMapper;

namespace PostMedia.Application.UseCases.SetPostMediaModerationResult
{
    internal class SetPostMediaModerationResultMapper : Profile
    {
        public SetPostMediaModerationResultMapper()
        {
            CreateMap<Shared.Events.ModerationResult, Domain.ModerationResult>();
        }
    }
}
