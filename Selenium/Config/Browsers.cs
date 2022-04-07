using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.Helpers;
using WebDriverManager.DriverConfigs.Impl;

class Browsers
{
    public static IWebDriver GetDriver(string browserType)
    {
        IWebDriver _driver;
        Settings settings = new();
        bool headlessMode = settings.headlessMode;

        switch (browserType)
        {
            case "Google Chrome":
                new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser, Architecture.Auto);

                if (headlessMode == true)
                {
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("headless");
                    _driver = new ChromeDriver(chromeOptions);
                }
                else
                {
                    _driver = new ChromeDriver();
                }
                break;

            case "Mozilla Firefox":
                new DriverManager().SetUpDriver(new FirefoxConfig(), VersionResolveStrategy.MatchingBrowser, Architecture.Auto);
                var firefoxOptions = new FirefoxOptions() { AcceptInsecureCertificates = true };

                if (headlessMode == true)
                {
                    firefoxOptions.AddArgument("--headless");
                    _driver = new FirefoxDriver(firefoxOptions);
                }
                else
                {
                    _driver = new FirefoxDriver();
                }
                break;

            default:
                _driver = new ChromeDriver();
                break;
        }
        return _driver;
    }
}