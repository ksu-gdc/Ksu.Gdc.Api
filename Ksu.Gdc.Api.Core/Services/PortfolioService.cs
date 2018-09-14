using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

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

        public PortfolioService(KsuGdcContext ksuGdcContext)
        {
            _ksuGdcContext = ksuGdcContext;
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
    }
}
