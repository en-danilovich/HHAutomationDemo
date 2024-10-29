using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace PlaywrightSpecflowTests.Hooks
{
    [Binding]
    public class Hooks
    {
        public IPage Page { get; private set; } = null!;
        public IBrowserContext BrowserContext { get; private set; } = null!;
        public IBrowser Browser { get; private set; } = null!;

        public readonly ScenarioContext ScenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            ScenarioContext = scenarioContext;
        }

        [BeforeScenario(Order = 1)]
        public async Task RegisterSingleInstancePractitioner()
        {
            var playwright = await Playwright.CreateAsync();
            //Initialise a browser - 'Chromium' can be changed to 'Firefox' or 'Webkit'
            Browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                Args = new List<string>() { "--start-maximized" }
            });

            var browserContextOptions = new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport // This allows the window to take full size
            };

            if (ScenarioContext.ScenarioInfo.Tags.Contains("UserLogin"))
            {
                browserContextOptions.StorageStatePath = "../../../playwright/.auth/state.json";
            }
            BrowserContext = await Browser.NewContextAsync(browserContextOptions);


            Page = await BrowserContext.NewPageAsync();
            await Page.GotoAsync("https://hh.ru/");
        }


        [BeforeScenario(Order = 2)]
        public async Task StartTracing()
        {
            await BrowserContext.Tracing.StartAsync(new TracingStartOptions
            {
                Title = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}",
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
        }

        [AfterScenario(Order = 1)]
        public async Task StopTracing()
        {
            var traceFilePath = Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                "playwright-traces",
                $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
            );

            // Create directory if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(traceFilePath)!);

            // Stop tracing and save the trace to the desired path
            await BrowserContext.Tracing.StopAsync(new TracingStopOptions
            {
                Path = traceFilePath
            });
        }

        [AfterScenario(Order = 2)]
        public async Task AfterScenario()
        {
            await Browser.CloseAsync();
        }
    }
}
