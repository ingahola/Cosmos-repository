// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.Azure.CosmosRepository.ChangeFeed.Providers;

internal class DefaultChangeFeedContainerProcessorProvider : IChangeFeedContainerProcessorProvider
{
    private readonly IOptionsMonitor<RepositoryOptions> _optionsMonitor;
    private readonly ICosmosContainerService _containerService;
    private readonly ILeaseContainerProvider _leaseContainerProvider;
    private readonly ILoggerFactory _loggerFactory;
    private readonly IServiceProvider _serviceProvider;
    private readonly IChangeFeedOptionsProvider _changeFeedOptionsProvider;

    public DefaultChangeFeedContainerProcessorProvider(
        IOptionsMonitor<RepositoryOptions> optionsMonitor,
        ICosmosContainerService containerService,
        ILeaseContainerProvider leaseContainerProvider,
        ILoggerFactory loggerFactory,
        IServiceProvider serviceProvider,
        IChangeFeedOptionsProvider changeFeedOptionsProvider)
    {
        _optionsMonitor = optionsMonitor;
        _containerService = containerService;
        _leaseContainerProvider = leaseContainerProvider;
        _loggerFactory = loggerFactory;
        _serviceProvider = serviceProvider;
        _changeFeedOptionsProvider = changeFeedOptionsProvider;
    }

    public IEnumerable<IContainerChangeFeedProcessor> GetProcessors()
    {
        RepositoryOptions repositoryOptions = _optionsMonitor.CurrentValue;

        IEnumerable<IGrouping<string, ContainerOptionsBuilder>> containers = repositoryOptions
            .ContainerBuilder
            .Options
            .Where(x =>
                x.ChangeFeedOptions != null && x.Name is not null)
            .GroupBy(x => x.Name!);

        foreach (IGrouping<string, ContainerOptionsBuilder> container in containers)
        {
            var itemTypes = container.Select(x => x.Type).ToList();

            yield return new DefaultContainerChangeFeedProcessor(
                _containerService,
                itemTypes,
                _leaseContainerProvider,
                _changeFeedOptionsProvider.GetOptionsForItems(itemTypes),
                _loggerFactory.CreateLogger<DefaultContainerChangeFeedProcessor>(),
                _serviceProvider);
        }
    }
}