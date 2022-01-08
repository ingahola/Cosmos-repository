// Copyright (c) IEvangelist. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.CosmosRepository.Options;
using Microsoft.Azure.CosmosRepository.Providers;
using Microsoft.Extensions.Options;

namespace Microsoft.Azure.CosmosRepository.ChangeFeed.Providers
{
    class DefaultLeastContainerProvider : ILeaseContainerProvider
    {
        private readonly ICosmosClientProvider _cosmosClientProvider;
        private readonly Lazy<Task<Container>> _lazyContainer;
        private readonly RepositoryOptions _repositoryOptions;
        private const string LeaseContainerName = "lease";
        private const string LeastContainerPartitionKey = "/id";

        public DefaultLeastContainerProvider(ICosmosClientProvider cosmosClientProvider,
            IOptionsMonitor<RepositoryOptions> optionsMonitor)
        {
            _repositoryOptions = optionsMonitor.CurrentValue;
            _cosmosClientProvider = cosmosClientProvider;
            _lazyContainer = new Lazy<Task<Container>>(BuildLeastContainer);
        }

        public Task<Container> GetLeaseContainerAsync() =>
            _lazyContainer.Value;

        private async Task<Container> BuildLeastContainer()
        {
            Database database =
                await _cosmosClientProvider.UseClientAsync(
                        client => client.CreateDatabaseIfNotExistsAsync(_repositoryOptions.DatabaseId))
                    .ConfigureAwait(false);


            Container container =
                await database.CreateContainerIfNotExistsAsync(LeaseContainerName, LeastContainerPartitionKey).ConfigureAwait(false);

            return container;
        }
    }
}