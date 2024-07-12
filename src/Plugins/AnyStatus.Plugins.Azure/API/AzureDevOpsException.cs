using System;

namespace AnyStatus.Plugins.Azure.API;

[Serializable]
internal class AzureDevOpsException : Exception
{
    public AzureDevOpsException() { }

    public AzureDevOpsException(string message) : base(message) { }

    public AzureDevOpsException(string message, Exception innerException) : base(message, innerException) { }
}