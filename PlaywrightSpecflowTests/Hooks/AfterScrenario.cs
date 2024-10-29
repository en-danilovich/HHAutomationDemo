using Microsoft.Playwright;
using ReportPortal.Shared;
using TechTalk.SpecFlow;

namespace PlaywrightSpecflowTests.Hooks
{
    [Binding]
    public class AfterScrenario
    {

        private readonly IPage _page;
        private readonly ScenarioContext _scenarioContext;

        public AfterScrenario(Hooks hooks, ScenarioContext scenarioContext)
        {
            _page = hooks.Page;
            _scenarioContext = scenarioContext;
        }

        [AfterScenario]
        public async Task TakeScreenshotOnFailure()
        {
            if (_scenarioContext.TestError != null)
            {
                var fileName = _scenarioContext.ScenarioInfo.Title;
                var screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), "screenshots", $"{fileName}.png");

                Directory.CreateDirectory(Path.GetDirectoryName(screenshotPath));

                await _page.ScreenshotAsync(new PageScreenshotOptions
                {
                    Path = screenshotPath
                });

                Context.Current.Log.Info(fileName, new FileInfo(screenshotPath));
                TestContext.WriteLine($"Scenario failed. Screenshot saved at {screenshotPath}");
            }
        }
    }
}
