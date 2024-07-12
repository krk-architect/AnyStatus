using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using AnyStatus.API.Attributes;

namespace AnyStatus.Plugins.SystemInformation.Network;

public class NetworkInterfacesSource : IItemsSource
{
    public IEnumerable<NameValueItem> GetItems(object source)
        => NetworkInterface.GetAllNetworkInterfaces()
                           .Select(i => new NameValueItem(i.Name, i.Id));
}