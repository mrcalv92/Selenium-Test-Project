using OpenQA.Selenium;
using NUnit.Framework;

[TestFixture("Google Chrome")]
class LoginTests
{
    IWebDriver _driver;
    Settings settings = new Settings();
    string browser;

    public LoginTests(string browserType)
    {
        browser = browserType;
    }

    [SetUp]
    public void Setup()
    {
        _driver = Browsers.GetDriver(browser);
        _driver.Manage().Window.Maximize();
        _driver.Navigate().GoToUrl(settings.url);
        Assert.AreEqual(_driver.Title, "Swag Labs", "Page title is incorrect, check URL");
    }

    [Test]
    [TestCase("bob")]
    public void EnterUsername(string username)
    {
        LoginPage loginPage = new LoginPage(_driver);
        loginPage.EnterUsername(username);
    }

    [Test]
    [TestCase("123456")]
    public void EnterPassword(string password)
    {
        LoginPage loginPage = new LoginPage(_driver);
        loginPage.EnterPassword(password);
    }

    [Test]
    public void MissingUsername()
    {
        LoginPage loginPage = new LoginPage(_driver);
        loginPage.EnterPassword("123456");
        loginPage.EnterUsername("");
    }

    [Test]
    public void MissingPassword()
    {
        LoginPage loginPage = new LoginPage(_driver);
        loginPage.EnterUsername("bob");
        loginPage.EnterPassword("");
    }

    [Test]
    public void MissingLoginCreds()
    {
        LoginPage loginPage = new LoginPage(_driver);
        loginPage.EnterUsername("");
        loginPage.EnterPassword("");
    }

    [Test]
    public void Login()
    {
        LoginPage loginPage = new LoginPage(_driver);
        loginPage.EnterUsername(settings.username);
        loginPage.EnterPassword(settings.password);
        loginPage.Login();
    }

    [Test]
    [TestCase("bob", "123456")]
    public void InvalidLoginCreds(string username, string password)
    {
        LoginPage loginPage = new LoginPage(_driver);
        loginPage.EnterUsername(username);
        loginPage.EnterPassword(password);
        loginPage.Login();
    }

    [TearDown]
    public void TearDown()
    {
        _driver.Quit();
    }
}