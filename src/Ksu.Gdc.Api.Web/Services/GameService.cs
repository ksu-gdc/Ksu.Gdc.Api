using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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
        private readonly IUserService _userService;

        public GameService(KsuGdcContext ksuGdcContext, IUserService userService)
        {
            _ksuGdcContext = ksuGdcContext;
            _userService = userService;
        }

        #region CREATE

        public async Task<bool> CreateAsync(DbEntity_Game createdGame)
        {
            await _ksuGdcContext.Games.AddAsync(createdGame);
            return true;
        }
        public async Task<DbEntity_Game> CreateAsync(CreateDto_Game newGame)
        {
            var createdGame = Mapper.Map<DbEntity_Game>(newGame);
            var success = await CreateAsync(createdGame);
            return createdGame;
        }

        #endregion CREATE

        #region GET

        public async Task<List<DbEntity_Game>> GetAllAsync()
        {
            var games = await _ksuGdcContext.Games
                .ToListAsync();
            return games;
        }

        public async Task<List<DbEntity_Game>> GetFeaturedAsync()
        {
            var games = await _ksuGdcContext.Games
                .Where(g => g.IsFeatured == true)
                .ToListAsync();
            return games;
        }

        public async Task<DbEntity_Game> GetByIdAsync(int gameId)
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

        public async Task<DbEntity_Image> GetImageAsync(DbEntity_Game game)
        {
            var image = await _ksuGdcContext.GameImages
                .Where(gi => gi.GameId == game.GameId)
                .Select(gi => gi.Image)
                .FirstOrDefaultAsync();
            if (image == null)
            {
                throw new NotFoundException($"No image with game id '{game.GameId}' was found.");
            }
            return image;
        }
        public async Task<DbEntity_Image> GetImageAsync(int gameId)
        {
            var game = await GetByIdAsync(gameId);
            var image = await GetImageAsync(game);
            return image;
        }

        public async Task<List<DbEntity_User>> GetCollaboratorsAsync(DbEntity_Game game)
        {
            var users = await _ksuGdcContext.GameUsers
                .Where(gu => gu.GameId == game.GameId)
                .Include(gu => gu.User)
                .Select(gu => gu.User)
                .ToListAsync();
            return users;
        }
        public async Task<List<DbEntity_User>> GetCollaboratorsAsync(int gameId)
        {
            var game = await GetByIdAsync(gameId);
            var collaborators = await GetCollaboratorsAsync(game);
            return collaborators;
        }

        public async Task<List<DbEntity_User>> GetNonCollaboratorsAsync(DbEntity_Game game)
        {
            var collaborators = _ksuGdcContext.GameUsers
                .Where(gu => gu.GameId == game.GameId)
                .Select(gu => gu.UserId);
            var nonCollaborators = await _ksuGdcContext.Users
                .Where(u => !(collaborators.Contains(u.UserId)))
                .ToListAsync();
            return nonCollaborators;
        }
        public async Task<List<DbEntity_User>> GetNonCollaboratorsAsync(int gameId)
        {
            var game = await GetByIdAsync(gameId);
            var nonCollaborators = await GetNonCollaboratorsAsync(game);
            return nonCollaborators;
        }

        public async Task<DbEntity_GameUser> GetCollaboratorAsync(int gameId, int userId)
        {
            var user = await _ksuGdcContext.GameUsers
                .Where(gu => gu.GameId == gameId && gu.UserId == userId)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new NotFoundException($"User with id, '{userId}', is not a collaborator for game with id, '{gameId}'.");
            }
            return user;
        }

        #endregion GET

        #region UPDATE

        public async Task<bool> UpdateAsync(DbEntity_Game updatedGame)
        {
            updatedGame.UpdatedOn = DateTimeOffset.Now;
            _ksuGdcContext.Update(updatedGame);
            return true;
        }

        public async Task<bool> UpdateImageAsync(int gameId, UpdateDto_Image imageUpdate)
        {
            var game = await GetByIdAsync(gameId);
            DbEntity_Image image;
            try
            {
                image = await GetImageAsync(game);
            }
            catch (NotFoundException)
            {
                var newImage = Mapper.Map<DbEntity_Image>(imageUpdate);
                await _ksuGdcContext.Images.AddAsync(newImage);
                var gameImage = new DbEntity_GameImage()
                {
                    GameId = game.GameId,
                    ImageId = newImage.ImageId
                };
                await _ksuGdcContext.GameImages.AddAsync(gameImage);
                return true;
            }
            Mapper.Map(imageUpdate, image);
            _ksuGdcContext.Images.Update(image);
            return true;
        }

        public async Task<bool> AddCollaboratorAsync(DbEntity_GameUser collaborator)
        {
            await _ksuGdcContext.GameUsers.AddAsync(collaborator);
            return true;
        }
        public async Task<bool> AddCollaboratorAsync(DbEntity_Game game, DbEntity_User user)
        {
            var collaborator = new DbEntity_GameUser()
            {
                GameId = game.GameId,
                UserId = user.UserId
            };
            var success = await AddCollaboratorAsync(collaborator);
            return success;
        }
        public async Task<bool> AddCollaboratorAsync(DbEntity_Game game, int userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            var success = await AddCollaboratorAsync(game, user);
            return success;
        }
        public async Task<bool> AddCollaboratorAsync(int gameId, DbEntity_User user)
        {
            var game = await GetByIdAsync(gameId);
            var success = await AddCollaboratorAsync(game, user);
            return success;
        }
        public async Task<bool> AddCollaboratorAsync(int gameId, int userId)
        {
            var game = await GetByIdAsync(gameId);
            var user = await _userService.GetByIdAsync(userId);
            var success = await AddCollaboratorAsync(game, user);
            return success;
        }

        #endregion UPDATE

        #region DELETE

        public async Task<bool> DeleteAsync(DbEntity_Game game)
        {
            _ksuGdcContext.Games.Remove(game);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(int gameId)
        {
            DbEntity_Game game = await GetByIdAsync(gameId);
            var success = await DeleteAsync(game);
            return success;
        }

        public async Task<bool> RemoveCollaboratorAsync(DbEntity_GameUser collaborator)
        {
            _ksuGdcContext.GameUsers.Remove(collaborator);
            return true;
        }
        public async Task<bool> RemoveCollaboratorAsync(DbEntity_Game game, DbEntity_User user)
        {
            var collaborator = await GetCollaboratorAsync(game.GameId, user.UserId);
            var success = await RemoveCollaboratorAsync(collaborator);
            return success;
        }
        public async Task<bool> RemoveCollaboratorAsync(DbEntity_Game game, int userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            var success = await RemoveCollaboratorAsync(game, user);
            return success;
        }
        public async Task<bool> RemoveCollaboratorAsync(int gameId, DbEntity_User user)
        {
            var game = await GetByIdAsync(gameId);
            var success = await RemoveCollaboratorAsync(gameId, user);
            return success;
        }
        public async Task<bool> RemoveCollaboratorAsync(int gameId, int userId)
        {
            var game = await GetByIdAsync(gameId);
            var user = await _userService.GetByIdAsync(userId);
            var success = await RemoveCollaboratorAsync(game, user);
            return success;
        }

        #endregion DELETE

        public async Task<int> SaveChangesAsync()
        {
            return await _ksuGdcContext.SaveChangesAsync();
        }
    }
}
