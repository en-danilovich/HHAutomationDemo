using Microsoft.Playwright;
using PlaywrightSpecflowTests.Pages;
using ReportPortal.Client.Abstractions.Responses;
using TechTalk.SpecFlow;

namespace PlaywrightSpecflowTests.Steps
{
    [Binding]
    public class ResumeSteps
    {
        private readonly IPage _page;
        private readonly ResumesPage _resumesPage;
        private readonly ResumePage _resumePage;

        private readonly ScenarioContext _scenarioContext;

        public ResumeSteps(ScenarioContext scenarioContext, Hooks.Hooks hooks, ResumesPage resumesPage, ResumePage resumePage)
        {
            _page = hooks.Page;                        
            _scenarioContext = scenarioContext;
            _resumesPage = resumesPage;
            _resumePage = resumePage;
        }


        [When("Пользователь открывает резюме с названием '(.*)'")]
        public async Task WhenUserSavesResumeFile(string resumeTitle)
        {
            await _resumesPage.ClickOnResumeWithTitle(resumeTitle);
        }

        
        [When("Пользователь скачивает резюме в формате '(.*)' как '(.*)'")]
        public async Task WhenUserSavesResumeInFormat(string downloadFileFormat, string saveAs)
        {
            var filePath = await _resumePage.DownloadResumeInFormat(downloadFileFormat);
            _scenarioContext.Add(saveAs, filePath);
        }

        [Then("Файл '(.*)' успешно сохранен")]
        public void ThenFileWithKeyExists(string fileKey)
        {
            var filePath = _scenarioContext.Get<string>(fileKey);
            Assert.That(File.Exists(filePath), "File was not downloaded");

            var fileInfo = new FileInfo(filePath);
            Assert.IsTrue(fileInfo.Length > 0, "File is empty.");
        }

        [When("Пользователь открывает список подходящих для отклика вакансий")]
        public async Task WhenUserOpensSuitableVacancies()
        {
            var response = await _page.RunAndWaitForResponseAsync(
                async () => await _resumesPage.SuitableVacanciesLink.ClickAsync(),
                response => response.Url.Contains("/search/vacancy?resume=") && response.Status == 200);

            Assert.That(response.Status, Is.EqualTo(200), "Response status code is not OK");
        }
    }
}
