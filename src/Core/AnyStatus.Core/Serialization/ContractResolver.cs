using System;
using Newtonsoft.Json.Serialization;

namespace AnyStatus.Core.Serialization;

public class ContractResolver : DefaultContractResolver
{
    private readonly IServiceProvider _serviceProvider;

    public ContractResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override JsonObjectContract CreateObjectContract(Type type)
    {
        var contract = base.CreateObjectContract(type);

        contract.DefaultCreator = () => _serviceProvider.GetService(type);

        return contract;
    }
}