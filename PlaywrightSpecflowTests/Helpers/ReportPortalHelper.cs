using ReportPortal.Shared;

namespace PlaywrightSpecflowTests.Helpers
{
    public class ReportPortalHelper
    {
        public static void LogScreenshot(string screenshotPath)
        {
            var screenshotData = File.ReadAllBytes(screenshotPath);

            Context.Current.Log.Info("my file", new FileInfo(screenshotPath));
            
        }
    }
}
