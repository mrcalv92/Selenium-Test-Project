using OpenQA.Selenium;
using NUnit.Framework;
class LoginPage
{
    IWebDriver _driver;
    IWebElement usernameField;
    IWebElement passwordField;
    IWebElement loginButton;
    Settings settings = new Settings();

    public LoginPage(IWebDriver newDriver)
    {
        _driver = newDriver;
        MapPage();
    }

    public void MapPage()
    {
        usernameField = _driver.FindElement(By.Id("user-name"));
        passwordField = _driver.FindElement(By.Id("password"));
        loginButton = _driver.FindElement(By.Id("login-button"));
    }

    public void EnterUsername(string username)
    {
        usernameField.SendKeys(username);
        if (usernameField.GetAttribute("value") == "")
        {
            PressLoginButton();
            Assert.IsTrue(_driver.FindElement(By.ClassName("error-button")).Displayed, "Username validation box isn't displayed");
        }
        else
        {
            Assert.AreEqual(usernameField.GetAttribute("value"), username, "Username does not match expected test case");
        }
    }

    public void EnterPassword(string password)
    {
        passwordField.SendKeys(password);
        if (passwordField.GetAttribute("value") == "")
        {
            PressLoginButton();
            Assert.IsTrue(_driver.FindElement(By.ClassName("error-button")).Displayed, "Password validation box isn't displayed");
        }
        else
        {
            Assert.AreEqual(passwordField.GetAttribute("value"), password, "Password does not match expected test case");
        }
    }

    public void PressLoginButton()
    {

        loginButton.Click();
    }

    public void Login()
    {
        string username = usernameField.GetAttribute("value");
        string password = passwordField.GetAttribute("value");
        loginButton.Click();

        if (username != settings.username || password != settings.password)
        {
            Assert.IsTrue(_driver.FindElement(By.ClassName("error-button")).Displayed, "Invalid creds box isn't displayed");
        }
        else
        {
            Assert.AreEqual(_driver.Url, "https://www.saucedemo.com/inventory.html", "Failed auth, not logged in");
        }
    }
}