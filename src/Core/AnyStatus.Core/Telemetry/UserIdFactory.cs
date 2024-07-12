using System;
using System.Security.Cryptography;
using System.Text;

namespace AnyStatus.Core.Telemetry;

internal class UserIdFactory
{
    internal string Create()
    {
        try
        {
            var bytes = Encoding.UTF8.GetBytes(Environment.UserName + Environment.MachineName);

            using var crypto = MD5.Create();

            var hash = crypto.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
        catch
        {
            return null;
        }
    }
}