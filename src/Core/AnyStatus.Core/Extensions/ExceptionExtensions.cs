using System;

namespace AnyStatus.Core.Extensions
{
    public static class ExceptionExtensions
    {
        public static bool IsTimeout(this Exception @this)
        {
            var e = @this;

            while (e != null)
            {
                if (e.MessageContainsTimeout())
                {
                    return true;
                }

                e = @this.InnerException;
            }

            return false;
        }

        private static bool MessageContainsTimeout(this Exception @this) => @this.Message.Contains("timeout") || @this.Message.Contains("timed out");
    }
}
