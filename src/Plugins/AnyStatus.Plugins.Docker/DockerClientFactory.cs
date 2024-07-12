﻿using System;
using Docker.DotNet;

namespace AnyStatus.Plugins.Docker;

internal static class DockerClientFactory
{
    internal static DockerClient GetClient(this DockerEndpoint endpoint)
        => new DockerClientConfiguration(new Uri(endpoint.Address)).CreateClient();
}