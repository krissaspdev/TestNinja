using System;
using System.Collections.Generic;
using Moq;
using TestNinja.Mocking;
using Xunit;

namespace TestNinja.UnitTests.Mocking
{
    public class VideoServiceTests
    {
        private readonly VideoService _videoService;
        private readonly Mock<IFileReader> _fileReader;
        private readonly Mock<IVideoRepository> _videoRepository;

        public VideoServiceTests()
        {
            _fileReader = new Mock<IFileReader>();
            _videoRepository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_fileReader.Object, _videoRepository.Object);
        }

        [Fact]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle();

            Assert.Contains("error", result, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void GetUnprocessedVideosAsCsv_AllVideoAreProcessed_ReturnEmptyString()
        {
            _videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void GetUnprocessedVideosAsCsv_AFewUnprocessdeVideos_ReturnAStringWithIdOfUnprocessedVideos()
        {
            _videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(
                new List<Video>
                {
                    new Video { Id = 1},
                    new Video { Id = 2},
                    new Video { Id = 3}
                }
            );

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.Equal("1,2,3", result);
        }
    }
}