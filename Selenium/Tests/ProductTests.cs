using OpenQA.Selenium;
using NUnit.Framework;

[TestFixture("Google Chrome")]
class ProductTests
{
    private IWebDriver _driver;
    private readonly Settings settings = new();
    private readonly string browser;

    public ProductTests(string browserType)
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
        LoginPage loginPage = new(_driver);
        loginPage.EnterUsername(settings.username);
        loginPage.EnterPassword(settings.password);
        loginPage.Login();
        Assert.AreEqual(_driver.Url, "https://www.saucedemo.com/inventory.html", "Failed auth, not logged in");
    }

    [Test]
    public void PressHamburgerMenu()
    {
        ProductsPage products = new(_driver);
        products.PressHamburgerMenuButton();
        Assert.IsTrue(products.hamburgerMenuList.Enabled, "Hamburger menu not visible");
    }

    [TearDown]
    public void TearDown()
    {
        _driver.Quit();
    }
}
