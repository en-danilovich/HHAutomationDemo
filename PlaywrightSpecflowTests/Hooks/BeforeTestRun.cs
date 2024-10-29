using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace PlaywrightSpecflowTests.Hooks
{
    [Binding]
    public class BeforeTestRun
    {
        private static readonly bool CreateUserContext = false;

        [BeforeTestRun]
        public static async Task StorageUserLoginState()
        {
            if (!CreateUserContext) { return; }

            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
            var browserContext = await browser.NewContextAsync();

            var page = await browserContext.NewPageAsync();
            
            // login default user
            await page.GotoAsync("https://hh.ru/");
            await page.Locator("a[data-qa='login']").ClickAsync();
            await page.Locator("button[data-qa='expand-login-by-password']").ClickAsync();
            await page.Locator("input[data-qa='login-input-username']").FillAsync("username");
            await page.Locator("input[data-qa='login-input-password']").FillAsync("password");
            await page.Locator("button[data-qa='account-login-submit']").ClickAsync();

            var resumes = page.Locator("a[data-qa='mainmenu_myResumes']");
            await Assertions.Expect(resumes).ToBeVisibleAsync();

            // store storage state
            await browserContext.StorageStateAsync(new()
            {
                Path = "../../../playwright/.auth/state.json"
            });

            await browserContext.CloseAsync();
        }
    }
}