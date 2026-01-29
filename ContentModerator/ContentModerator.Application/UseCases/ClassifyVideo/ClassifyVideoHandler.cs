using MediatR;
using Shared.Events;

namespace ContentModerator.Application.UseCases.ClassifyVideo
{
    internal class ClassifyVideoHandler(TempDirectoryManager tempDirectoryManager, IVideoFrameExtractor videoFrameExtractor, IModerator moderator) : IRequestHandler<ClassifyVideoRequest, ModerationResult>
    {
        private readonly IModerator _moderator = moderator;
        private readonly TempDirectoryManager _tempDirectoryManager = tempDirectoryManager;
        private readonly IVideoFrameExtractor _videoFrameExtractor = videoFrameExtractor;

        public async Task<ModerationResult> Handle(ClassifyVideoRequest request, CancellationToken cancellationToken)
        {
            double resolution = 720;
            double fps = 1;
            try
            {
                _tempDirectoryManager.Create();

                var inputStream = request.File.OpenReadStream();
                var inputPath = await _tempDirectoryManager.AddAsync(inputStream, cancellationToken);
                inputStream.Close();
                inputStream.Dispose();

                var tempPath = _tempDirectoryManager.GenerateUniqPath();
                IEnumerable<string> outputPaths = await _videoFrameExtractor.ExtractAsync(inputPath, tempPath, resolution, fps, cancellationToken);
                var tasks = outputPaths.Select(path => _moderator.ClassifyImageAsync(path, cancellationToken));
                var moderationResult = ModerationResult.Max(await Task.WhenAll(tasks));

                _tempDirectoryManager.Delete();

                return moderationResult;
            }
            catch (Exception)
            {
                _tempDirectoryManager.Delete();
                throw;
            }
        }
    }
}
