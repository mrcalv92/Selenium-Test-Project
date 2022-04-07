using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

class ProductsPage
{
    private readonly IWebDriver _driver;

    public ProductsPage(IWebDriver newDriver)
    {
        this._driver = newDriver;
        PageFactory.InitElements(_driver, this);
    }

    [FindsBy(How = How.ClassName, Using = "bm-burger-button")]
    [CacheLookup]
    public IWebElement hamburgerMenu;

    [FindsBy(How = How.ClassName, Using = "bm-item-list")]
    [CacheLookup]
    public IWebElement hamburgerMenuList;

    public void PressHamburgerMenuButton()
    {
        hamburgerMenu.Click();
    }
}
