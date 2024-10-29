using Microsoft.Playwright;

namespace PlaywrightSpecflowTests.Pages
{
    public class ResumesPage : HeaderPage
    {
        public ResumesPage(Hooks.Hooks hooks) : base(hooks)
        {
        }

        public ILocator SuitableVacanciesLink => _page.GetByRole(AriaRole.Link, new() { Name = "Подходящие для отклика вакансии" });

        public async Task ClickOnResumeWithTitle(string title)
        {
            var resumeLink = _page
                .Locator("a[data-qa='resume-title-link']")
                .Filter(new() { Has = _page.Locator($"//span[@data-qa='resume-title'][text()='{title}']") });

            await resumeLink.ClickAsync();
        }
    }
}
