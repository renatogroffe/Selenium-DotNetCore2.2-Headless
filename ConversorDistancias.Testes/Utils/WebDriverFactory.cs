using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;

namespace ConversorDistancias.Testes.Utils
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateWebDriver(
            Browser browser, string pathDriver, bool headless)
        {
            IWebDriver webDriver = null;

            switch (browser)
            {
                case Browser.Firefox:
                    FirefoxOptions optionsFF = new FirefoxOptions();
                    if (headless)
                        optionsFF.AddArgument("--headless");

                    webDriver = new FirefoxDriver(pathDriver, optionsFF);

                    break;
                case Browser.Chrome:
                    ChromeOptions options = new ChromeOptions();
                    if (headless)
                        options.AddArgument("--headless");

                    webDriver = new ChromeDriver(pathDriver, options);

                    break;
            }

            return webDriver;
        }
    }
}