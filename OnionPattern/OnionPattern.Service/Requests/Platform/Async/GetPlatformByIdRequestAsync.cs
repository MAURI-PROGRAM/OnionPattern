﻿using OnionPattern.Domain.Platform.Responses;
using OnionPattern.Domain.Repository;
using OnionPattern.Domain.Services.Requests.Platform.Async;
using Serilog;
using System;
using System.Threading.Tasks;

namespace OnionPattern.Service.Requests.Platform.Async
{
    public class GetPlatformByIdRequestAsync : BaseServiceRequestAsync<Domain.Platform.Entities.Platform>, IGetPlatformByIdRequestAsync
    {
        /// <inheritdoc />
        /// <summary>
        ///     Request to get a Platform by it's Id asynchronously.
        /// </summary>
        /// <exception cref="T:System.ArgumentNullException">Condition.</exception>
        public GetPlatformByIdRequestAsync(IRepositoryAsync<Domain.Platform.Entities.Platform> repositoryAsync, IRepositoryAsyncAggregate repositoryAsyncAggregate)
            : base(repositoryAsync, repositoryAsyncAggregate) { }

        #region Implementation of IGetPlatformByIdRequestAsync

        /// <summary>
        /// Executes the request asynchronously.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PlatformResponse> ExecuteAsync(int id)
        {
            Log.Information("Retrieving Platform by Id: [{Id}]...", id);

            CheckInputValidity(id);

            var platformResponse = new PlatformResponse();
            try
            {
                var platform = await Repository.SingleOrDefaultAsync(p => p.Id == id);
                if (platform == null)
                {
                    var exception = new Exception($"Could not find Platform with Id: [{id}].");
                    Log.Error(exception, EXCEPTION_MESSAGE_TEMPLATE, exception.Message);
                    HandleErrors(platformResponse, exception, 404);
                }
                else
                {
                    platformResponse.Platform = platform;
                    platformResponse.StatusCode = 200;
                    Log.Information("Retrieved [{NewName}] for Id: [{Id}].", platformResponse.Platform.Name, id);
                }
            }
            catch (Exception exception)
            {
                Log.Error(exception, "Failed to get Platform with Id: [{Id}].", id);
                HandleErrors(platformResponse, exception);
            }
            return platformResponse;
        }

        #endregion

        private void CheckInputValidity(int id)
        {
            if (id <= 0) { throw new ArgumentException($"{nameof(id)} must be 1 or greater."); }
        }
    }
}
