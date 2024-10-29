using FluentAssertions;
using Microsoft.Playwright;

namespace PlaywrightSpecflowTests.Pages
{
    public class HomePage : HeaderPage
    {
        public HomePage(Hooks.Hooks hooks) : base(hooks)
        {
        }

        public ILocator LoginButton => _page.Locator("a[data-qa='login']");

        public ILocator VacancyCard => _page.Locator("//div[contains(@data-qa,'vacancy-serp__vacancy')]");

        public async Task AssertPageContent()
        {            
            _page.Url.Should().Be("https://hh.ru/");
            await Assertions.Expect(LoginButton).ToBeVisibleAsync();
        }
    }
}
