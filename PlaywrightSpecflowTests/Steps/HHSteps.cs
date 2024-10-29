using Microsoft.Playwright;
using PlaywrightSpecflowTests.Pages;
using ReportPortal.Shared;
using TechTalk.SpecFlow;

namespace PlaywrightSpecflowTests.Steps
{
    [Binding]
    public class HHSteps
    {
        private readonly IPage _page;
        private readonly HomePage _homePage;
        private readonly LoginPage _loginPage;

        private readonly ScenarioContext _scenarioContext;

        public HHSteps(ScenarioContext scenarioContext, Hooks.Hooks hooks, HomePage homePage, LoginPage loginPage)
        {
            _page = hooks.Page;
            _homePage = homePage;
            _loginPage = loginPage;
            _scenarioContext = scenarioContext;
        }

        [Given("Пользователь вошел в приложение c помощью почты '(.*)'")]
        public async Task GivenUserIsLoggedInWithEmail(string email)
        {            
            await _page.GotoAsync("https://hh.ru/");
            await _homePage.AssertPageContent();
            await _homePage.LoginButton.ClickAsync();

            await _loginPage.LoginWithPassword("ktaghFayudip");
        }

        [Given("Пользователь вошел в приложение hh.ru")]
        public async Task GivenUserIsLoggedIn()
        {
            await Assertions.Expect(_homePage.ResumesLink).ToBeVisibleAsync();

            // screenshots
            var filePath = "screenshot.png";
            await _homePage.ResumesLink.ScreenshotAsync(new() { Path = filePath });
            Context.Current.Log.Info("my file", new FileInfo(filePath));
        }

        [Given("Пользователь вошел в приложение hh.ru как админ")]
        public async Task GivenScenarioFail()
        {
            Assert.Fail();
        }

        [When("Пользователь открывает 'Мои Резюме'")]
        public async Task WhenUserOpensMyResumes()
        {
            await _homePage.ResumesLink.ClickAsync();
        }

        [Then("Отображается список доступных вакансий")]
        public async Task ThenVacanciesListDisplayed()
        {
            var count = await _homePage.VacancyCard.CountAsync();
            Assert.That(count, Is.GreaterThanOrEqualTo(1), "Нет доступных вакансий");
        }

        [When("Пользователь открывает страницу текущей локации")]
        public async Task UserOpensLocations()
        {
            await _homePage.GeoSwitcherButton.ClickAsync();
        }

        [Then("Следующие страны доступны для выбора:")]
        public void ThenCountiesAvailableToSelect(Table table)
        {
            Assert.Pass();
        }
    }
}
