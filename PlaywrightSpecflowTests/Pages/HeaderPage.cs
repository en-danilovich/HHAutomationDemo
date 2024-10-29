using Microsoft.Playwright;

namespace PlaywrightSpecflowTests.Pages
{
    public abstract class HeaderPage
    {
        protected readonly IPage _page;

        public HeaderPage(Hooks.Hooks hooks)
        {
            _page = hooks.Page;
        }

        public ILocator ResumesLink => _page.Locator("a[data-qa='mainmenu_myResumes']");
        public ILocator GeoSwitcherButton => _page.Locator("[data-qa='geo-switcher']");
    }
}
