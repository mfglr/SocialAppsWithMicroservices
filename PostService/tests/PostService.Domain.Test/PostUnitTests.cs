//using PostService.Domain.Exceptions;
//using Shared.Objects;

//namespace PostService.Domain.Test
//{
//    public class PostUnitTests
//    {

//        private Guid _userId = Guid.NewGuid();
//        private Content _content = new("content");
//        private List<Media> _media =
//            [
//                new(Post.MediaContainerName, "blob0", MediaType.Video),
//                new(Post.MediaContainerName, "blob1", MediaType.Image)
//            ];
//        private readonly DateTime _dateBeforeCreation;
//        private readonly Post _validPost;


//        public PostUnitTests()
//        {
//            _dateBeforeCreation = DateTime.UtcNow;
//            _validPost = new(_userId, _content, _media);
//        }

//        [Fact]
//        public void Constructor_WithValidParameters_ShouldCreatePost()
//        {
//            Assert.NotEqual(default, _validPost.Id);
//            Assert.NotEqual(default, _validPost.CreatedAt);
//            Assert.True(_validPost.CreatedAt > _dateBeforeCreation && _validPost.CreatedAt < DateTime.UtcNow);
//            Assert.Null(_validPost.UpdatedAt);
//            Assert.Equal(1, _validPost.Version);
//            Assert.False(_validPost.IsDeleted);
//            Assert.Equal(_content, _validPost.Content);
//            Assert.Equal(_media.Count, _validPost.Media.Count);
//            for (int i = 0; i < _media.Count; i++)
//            {
//                var itemx = _media[i];
//                var itemy = _validPost.Media[i];
//                Assert.Equal(itemx, itemy);
//            }
//        }


//        [Fact]
//        public void Constructor_WhenMediaIsEmpty_ShouldThrowPostMediaRequiredException()
//        {
//            Assert.Throws<PostMediaRequiredException>(
//                () => new Post(Guid.NewGuid(), null, media: [])
//            );
//        }

//        [Fact]
//        public void Constructor_WhenMediaCountExceedsLimit_ShouldThrowPostMediaCountException()
//        {
//            var media = Enumerable
//                .Range(0, Post.MaxMediaCount + 1)
//                .Select(_ => new Media(Post.MediaContainerName, "test", MediaType.Image))
//                .ToList();

//            Assert.Throws<PostMediaCountException>(
//                () => new Post(Guid.NewGuid(), null, media: media)
//            );
//        }

//        [Fact]
//        public void Constructor_WhenMediaContainerNameNotValid_ShouldThrowInvalidContainerName()
//        {
//            var media = new Media("Invalid Container Name", "test", MediaType.Image);

//            Assert.Throws<InvalidContainerName>(
//                () => new Post(Guid.NewGuid(), null, media: [media])
//            );
//        }
//    }
//}
