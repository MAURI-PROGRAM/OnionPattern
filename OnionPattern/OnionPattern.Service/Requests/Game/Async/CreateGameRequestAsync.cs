﻿using AutoMapper;
using OnionPattern.Domain.Game.Requests;
using OnionPattern.Domain.Game.Responses;
using OnionPattern.Domain.Repository;
using OnionPattern.Domain.Services.Requests.Game.Async;
using Serilog;
using System;
using System.Threading.Tasks;

namespace OnionPattern.Service.Requests.Game.Async
{
    public class CreateGameRequestAsync : BaseServiceRequestAsync<Domain.Game.Entities.Game>, ICreateGameRequestAsync
    {
        public CreateGameRequestAsync(IRepositoryAsync<Domain.Game.Entities.Game> repositoryAsync, IRepositoryAsyncAggregate repositoryAsyncAggregate)
            : base(repositoryAsync, repositoryAsyncAggregate) { }

        #region Implementation of ICreateGameRequestAsync

        public async Task<GameResponse> ExecuteAsync(CreateGameInput input)
        {
            var gameResponse = new GameResponse();
            try
            {
                Log.Information("Creating Game Entry for [{NewName}]...", input?.Name);
                var gameEntity = Mapper.Map<CreateGameInput, Domain.Game.Entities.Game>(input);
                gameResponse.Game = await Repository.CreateAsync(gameEntity);
                gameResponse.StatusCode = 200;
                Log.Information("Created Game Entry for [{NewName}] with Id: [{Id}]", gameResponse.Game.Name, gameResponse.Game.Id);
            }
            catch (Exception exception)
            {
                Log.Error(exception, "Failed to Create Game: [{NewName}].", input?.Name);
                HandleErrors(gameResponse, exception);
            }
            return gameResponse;
        }

        #endregion
    }
}
