using System;

namespace AnyStatus.Core.Extensions;

public static class ExceptionExtensions
{
    public static bool IsTimeout(this Exception @this)
    {
        var e = @this;
        if (e is null)
        {
            return false;
        }

        var message = "";

        while (e != null)
        {
            message = e.Message;

            if (e.IsTheOperationHasTimedOut())
            {
                return true;
            }

            e = @this.InnerException;
        }

        return false;
    }

    private static bool IsTheOperationHasTimedOut(this Exception @this)
    {
        const string theOperationHasTimedOut = "The operation has timed out.";
        var          message                 = @this.Message;
        return string.Compare(message, theOperationHasTimedOut, StringComparison.InvariantCultureIgnoreCase) != 0;
    }
}