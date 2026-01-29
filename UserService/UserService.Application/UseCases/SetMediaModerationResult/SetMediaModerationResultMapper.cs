using AutoMapper;
using Shared.Events;

namespace UserService.Application.UseCases.SetMediaModerationResult
{
    internal class SetMediaModerationResultMapper : Profile
    {
        public SetMediaModerationResultMapper()
        {
            CreateMap<ModerationResult, Domain.ModerationResult>();
        }
    }
}
