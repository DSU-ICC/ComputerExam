using Sentry;
using Serilog;

namespace Infrastructure.Common
{
    public class LogHelper
    {
        public static void SendErrorLog(Exception ex)
        {
            Console.WriteLine(ex);
            SentrySdk.CaptureException(ex);
            Log.Write(Serilog.Events.LogEventLevel.Error, ex, "ERR");
        }

        public static void SendInformationLog(string text)
        {
            Console.WriteLine(text);
            SentrySdk.CaptureMessage(text);
            Log.Write(Serilog.Events.LogEventLevel.Information, text, "Info");
        }
    }
}
