using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

class LoginPage
{
    private readonly IWebDriver _driver;

    public LoginPage(IWebDriver newDriver)
    {
        this._driver = newDriver;
        PageFactory.InitElements(_driver, this);
    }

    [FindsBy(How = How.Id, Using = "user-name")]
    [CacheLookup]
    public IWebElement usernameField;

    [FindsBy(How = How.Id, Using = "password")]
    [CacheLookup]
    public IWebElement passwordField;

    [FindsBy(How = How.Id, Using = "login-button")]
    [CacheLookup]
    public IWebElement loginButton;

    [FindsBy(How = How.ClassName, Using = "error-button")]
    [CacheLookup]
    public IWebElement errorButton;

    public void EnterUsername(string username)
    {
        usernameField.SendKeys(username);       
    }

    public void EnterPassword(string password)
    {
        passwordField.SendKeys(password);
    }

    public void Login()
    {
        loginButton.Click();
    }
}