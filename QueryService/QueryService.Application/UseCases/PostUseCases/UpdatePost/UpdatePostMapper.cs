using AutoMapper;
using QueryService.Domain.PostDomain;

namespace QueryService.Application.UseCases.PostUseCases.UpdatePost
{
    internal class UpdatePostMapper : Profile
    {
        public UpdatePostMapper()
        {
            CreateMap<UpdatePostRequest_Content, PostContent>();
            CreateMap<UpdatePostRequest_Media, Media>();
            CreateMap<UpdatePostRequest, Post>()
                .BeforeMap((request,post) => request.Media.RemoveAll(x => x.IsDeleted));
        }
    }
}
