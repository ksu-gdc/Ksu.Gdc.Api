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

        public List<GameDto> GetGames()
        {
            return GetGamesAsync().Result;
        }

        public async Task<List<GameDto>> GetGamesAsync()
        {
            var dbGames = await _ksuGdcContext.Games.ToListAsync();
            if (dbGames.Count == 0)
            {
                throw new NotFoundException("No games were found.");
            }
            var gameDtos = Mapper.Map<List<GameDto>>(dbGames);
            return gameDtos;
        }

        public GameDto GetGameById(int id)
        {
            return GetGameByIdAsync(id).Result;
        }

        public async Task<GameDto> GetGameByIdAsync(int id)
        {
            var dbGame = await _ksuGdcContext.Games.Where(g => g.Id == id).FirstOrDefaultAsync();
            if (dbGame == null)
            {
                throw new NotFoundException($"No game with Id '{id}' was found.");
            }
            var gameDto = Mapper.Map<GameDto>(dbGame);
            return gameDto;
        }

        public Stream GetGameThumbnailImage(int id)
        {
            return GetGameThumbnailImageAsync(id).Result;
        }

        public async Task<Stream> GetGameThumbnailImageAsync(int id)
        {
            try
            {
                var transferUtility = new TransferUtility(_s3Client);
                var transferRequest = new TransferUtilityOpenStreamRequest()
                {
                    BucketName = AppConfiguration.GetConfig("AWS_S3_BucketName"),
                    Key = $"{GameConfig.DataStoreDirPath}/{id}/thumbnail.jpg"
                };
                var stream = await transferUtility.OpenStreamAsync(transferRequest);
                return stream;
            }
            catch (AmazonS3Exception ex)
            {
                if (ex.Message.Contains("key does not exist"))
                {
                    throw new NotFoundException($"No thumbnail image for game with id '{id}' was found.");
                }
                throw ex;
            }
        }
    }
}
