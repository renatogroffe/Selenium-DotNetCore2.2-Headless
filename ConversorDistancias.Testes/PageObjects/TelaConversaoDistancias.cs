using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ConversorDistancias.Testes.Utils;

namespace ConversorDistancias.Testes.PageObjects
{
    public class TelaConversaoDistancias
    {
        private IConfiguration _configuration;
        private Browser _browser;
        private IWebDriver _driver;

        public TelaConversaoDistancias(
            IConfiguration configuration, Browser browser)
        {
            _configuration = configuration;
            _browser = browser;

            string caminhoDriver = null;
            if (browser == Browser.Firefox)
            {
                caminhoDriver =
                    _configuration.GetSection("Selenium:CaminhoDriverFirefox").Value;
            }
            else if (browser == Browser.Chrome)
            {
                caminhoDriver =
                    _configuration.GetSection("Selenium:CaminhoDriverChrome").Value;
            }

            _driver = WebDriverFactory.CreateWebDriver(
                browser, caminhoDriver, true);
        }
        public void CarregarPagina()
        {
            _driver.LoadPage(
                TimeSpan.FromSeconds(Convert.ToInt32(
                    _configuration.GetSection("Selenium:Timeout").Value)),
                _configuration.GetSection("Selenium:UrlTelaConversaoDistancias").Value);
        }

        public void PreencherDistanciaMilhas(double valor)
        {
            _driver.SetText(
                By.Name("DistanciaMilhas"),
                valor.ToString());
        }

        public void ProcessarConversao()
        {
            _driver.Submit(By.Id("btnConverter"));

            WebDriverWait wait = new WebDriverWait(
                _driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => d.FindElement(By.Id("DistanciaKm")) != null);
        }

        public double ObterDistanciaKm()
        {
            return Convert.ToDouble(
                _driver.GetText(By.Id("DistanciaKm")));
        }

        public void Fechar()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}