using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.S3.Model;

using Ksu.Gdc.Api.Core.Configurations;
using Ksu.Gdc.Api.Core.Exceptions;
using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Core.Models;
using Ksu.Gdc.Api.Data.DbContexts;
using Ksu.Gdc.Api.Data.Entities;
namespace Ksu.Gdc.Api.Core.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly KsuGdcContext _ksuGdcContext;
        private readonly IAmazonS3 _s3Client;

        public PortfolioService(KsuGdcContext ksuGdcContext, IAmazonS3 s3Client)
        {
            _ksuGdcContext = ksuGdcContext;
            _s3Client = s3Client;
        }

        public List<Dto_Game> GetGames()
        {
            return GetGamesAsync().Result;
        }

        public async Task<List<Dto_Game>> GetGamesAsync()
        {
            var dbGames = await _ksuGdcContext.Games
                                              .ToListAsync();
            var dtoGames = Mapper.Map<List<Dto_Game>>(dbGames);
            return dtoGames;
        }

        public Dto_Game GetGameById(int gameId)
        {
            return GetGameByIdAsync(gameId).Result;
        }

        public async Task<Dto_Game> GetGameByIdAsync(int gameId)
        {
            var dbGame = await _ksuGdcContext.Games
                                             .Where(g => g.GameId == gameId)
                                             .FirstOrDefaultAsync();
            if (dbGame == null)
            {
                throw new NotFoundException($"No game with Id '{gameId}' was found.");
            }
            var dtoGames = Mapper.Map<Dto_Game>(dbGame);
            return dtoGames;
        }

        public bool UpdateGameThumbnailImage(int gameId, Stream imageStream)
        {
            return UpdateGameThumbnailImageAsync(gameId, imageStream).Result;
        }

        public async Task<bool> UpdateGameThumbnailImageAsync(int gameId, Stream imageStream)
        {
            var transferUtility = new TransferUtility(_s3Client);
            var transferRequest = new TransferUtilityUploadRequest()
            {
                BucketName = AppConfiguration.GetConfig("AWS_S3_BucketName"),
                Key = $"{GameConfig.DataStoreDirPath}/{gameId}/thumbnail.jpg",
                InputStream = imageStream,
                StorageClass = S3StorageClass.Standard,
                CannedACL = S3CannedACL.PublicRead
            };
            await transferUtility.UploadAsync(transferRequest);
            return true;
        }

        public Stream GetGameThumbnailImage(int gameId)
        {
            return GetGameThumbnailImageAsync(gameId).Result;
        }

        public async Task<Stream> GetGameThumbnailImageAsync(int gameId)
        {
            try
            {
                var transferUtility = new TransferUtility(_s3Client);
                var transferRequest = new TransferUtilityOpenStreamRequest()
                {
                    BucketName = AppConfiguration.GetConfig("AWS_S3_BucketName"),
                    Key = $"{GameConfig.DataStoreDirPath}/{gameId}/thumbnail.jpg"
                };
                var stream = await transferUtility.OpenStreamAsync(transferRequest);
                return stream;
            }
            catch (AmazonS3Exception ex)
            {
                if (ex.Message.Contains("key does not exist"))
                {
                    throw new NotFoundException($"No thumbnail image for game with id '{gameId}' was found.");
                }
                throw ex;
            }
        }
    }
}
