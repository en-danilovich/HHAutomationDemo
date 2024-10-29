using FluentAssertions;
using Microsoft.Playwright;

namespace PlaywrightSpecflowTests.Pages
{
    public class ResumePage : HeaderPage
    {
        public ResumePage(Hooks.Hooks hooks) : base(hooks)
        {
        }

        public ILocator DownloadResumeButton => _page.Locator("button[data-qa='resume-download-button']");

        public async Task<string> DownloadResumeInFormat(string fileFormat)
        {
            var downloadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "downloads");

            await DownloadResumeButton.ClickAsync();
            // Start the task of waiting for the download before clicking
            var waitForDownloadTask = _page.WaitForDownloadAsync();
            await _page.Locator($"//a[contains(.,'{fileFormat}')]").ClickAsync();
            var download = await waitForDownloadTask;

            // Wait for the download process to complete and save the downloaded file somewhere
            var filePath = Path.Combine(downloadFolderPath, download.SuggestedFilename);
            await download.SaveAsAsync(filePath);

            return filePath;
        }
    }
}
