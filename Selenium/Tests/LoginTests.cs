using OpenQA.Selenium;
using NUnit.Framework;

[TestFixture("Google Chrome")]
class LoginTests
{
    private IWebDriver _driver;
    private readonly Settings settings = new();
    private readonly string browser;

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
        LoginPage loginPage = new(_driver);
        loginPage.EnterUsername(username);
        Assert.AreEqual(loginPage.usernameField.GetAttribute("value"), username, "Username does not match expected test case");
    }

    [Test]
    [TestCase("123456")]
    public void EnterPassword(string password)
    {
        LoginPage loginPage = new(_driver);
        loginPage.EnterPassword(password);
        Assert.AreEqual(loginPage.passwordField.GetAttribute("value"), password, "Password does not match expected test case");
    }

    [Test]
    public void MissingUsername()
    {
        LoginPage loginPage = new(_driver);
        loginPage.EnterPassword("123456");
        loginPage.EnterUsername("");
        loginPage.Login();
        Assert.IsTrue(loginPage.errorButton.Displayed, "Validation box isn't displayed");
    }

    [Test]
    public void MissingPassword()
    {
        LoginPage loginPage = new(_driver);
        loginPage.EnterUsername("bob");
        loginPage.EnterPassword("");
        loginPage.Login();
        Assert.IsTrue(loginPage.errorButton.Displayed, "Validation box isn't displayed");
    }

    [Test]
    public void MissingLoginCreds()
    {
        LoginPage loginPage = new(_driver);
        loginPage.EnterUsername("");
        loginPage.EnterPassword("");
        loginPage.Login();
        Assert.IsTrue(loginPage.errorButton.Displayed, "Validation box isn't displayed");
    }

    [Test]
    public void Login()
    {
        LoginPage loginPage = new(_driver);
        loginPage.EnterUsername(settings.username);
        loginPage.EnterPassword(settings.password);
        loginPage.Login();
        Assert.AreEqual(_driver.Url, "https://www.saucedemo.com/inventory.html", "Failed auth, not logged in");
    }

    [Test]
    [TestCase("bob", "123456")]
    public void InvalidLoginCreds(string username, string password)
    {
        LoginPage loginPage = new(_driver);
        loginPage.EnterUsername(username);
        loginPage.EnterPassword(password);
        loginPage.Login();
        Assert.IsTrue(loginPage.errorButton.Displayed, "Invalid creds box isn't displayed");
    }

    [TearDown]
    public void TearDown()
    {
        _driver.Quit();
    }
}