using AutoMapper;
using QueryService.Domain.CommentDomain;

namespace QueryService.Application.UseCases.CommentUseCases.UpdateComent
{
    internal class UpdateCommentMapper : Profile
    {
        public UpdateCommentMapper()
        {
            CreateMap<UpdateCommentRequest_Content, CommentContent>();
            CreateMap<UpdateCommentRequest, Comment>();
        }
    }
}
