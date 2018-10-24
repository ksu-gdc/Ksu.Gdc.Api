using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.S3.Model;
using AutoMapper;

using Ksu.Gdc.Api.Core.Configurations;
using Ksu.Gdc.Api.Core.Exceptions;
using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Core.Models;
using Ksu.Gdc.Api.Data.DbContexts;
using Ksu.Gdc.Api.Data.Entities;
namespace Ksu.Gdc.Api.Core.Services
{
    public class GameService : IGameService
    {
        private readonly KsuGdcContext _ksuGdcContext;
        private readonly IAmazonS3 _s3Client;

        public GameService(KsuGdcContext ksuGdcContext, IAmazonS3 s3Client)
        {
            _ksuGdcContext = ksuGdcContext;
            _s3Client = s3Client;
        }

        #region CREATE

        public async Task<ModelEntity_Game> CreateGameAsync(CreateDto_Game newGame)
        {
            var newDbGame = Mapper.Map<ModelEntity_Game>(newGame);
            await _ksuGdcContext.Games.AddAsync(newDbGame);
            await _ksuGdcContext.SaveChangesAsync();
            return newDbGame;
        }

        #endregion CREATE

        #region GET

        public async Task<List<ModelEntity_Game>> GetGamesAsync()
        {
            var games = await _ksuGdcContext.Games
                                              .ToListAsync();
            return games;
        }

        public async Task<ModelEntity_Game> GetGameByIdAsync(int gameId)
        {
            var game = await _ksuGdcContext.Games
                                             .Where(g => g.GameId == gameId)
                                             .FirstOrDefaultAsync();
            if (game == null)
            {
                throw new NotFoundException($"No game with Id '{gameId}' was found.");
            }
            return game;
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

        #endregion GET

        #region UPDATE

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

        public async Task<bool> UpdateGameAsync(ModelEntity_Game dbGame, UpdateDto_Game updateGame)
        {
            Mapper.Map(updateGame, dbGame);
            dbGame.UpdatedAt = DateTimeOffset.Now;
            _ksuGdcContext.Update(dbGame);
            await _ksuGdcContext.SaveChangesAsync();
            return true;
        }

        #endregion UPDATE

        #region DELETE

        public async Task<bool> DeleteGameByIdAsync(int gameId)
        {
            var dbGame = await GetGameByIdAsync(gameId);
            _ksuGdcContext.Games.Remove(dbGame);
            await _ksuGdcContext.SaveChangesAsync();
            return true;
        }

        #endregion DELETE
    }
}
