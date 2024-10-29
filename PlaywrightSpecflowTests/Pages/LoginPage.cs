using Microsoft.Playwright;

namespace PlaywrightSpecflowTests.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;

        public LoginPage(Hooks.Hooks hooks)
        {
            _page = hooks.Page;
        }

        public ILocator UsernameInput => _page.Locator("input[data-qa='login-input-username']");
        public ILocator PasswordInput => _page.Locator("input[data-qa='login-input-password']");
        public ILocator LoginWithPasswordButton => _page.Locator("button[data-qa='expand-login-by-password']");
        public ILocator LoginButton => _page.Locator("button[data-qa='account-login-submit']");

        public async Task LoginWithPassword(string username)
        {
            await this.LoginWithPasswordButton.ClickAsync();
            await this.UsernameInput.FillAsync(username);
            await this.PasswordInput.FillAsync("pssword");
            await this.LoginButton.ClickAsync();
        }
    }
}
