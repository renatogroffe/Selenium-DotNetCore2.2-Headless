using System;
using System.IO;
using System.Globalization;
using Xunit;
using Microsoft.Extensions.Configuration;
using ConversorDistancias.Testes.PageObjects;
using ConversorDistancias.Testes.Utils;

namespace ConversorDistancias.Testes
{
    public class TestesConversaoDistancias
    {
        private IConfiguration _configuration;

        public TestesConversaoDistancias()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json");
            _configuration = builder.Build();

            var padroesBR = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = padroesBR;
            CultureInfo.DefaultThreadCurrentUICulture = padroesBR;
        }

        [Theory]
        [InlineData(Browser.Chrome, 100, 160.9)]
        [InlineData(Browser.Chrome, 230.05, 370.1505)]
        [InlineData(Browser.Chrome, 250.5, 403.10545)]
        public void TestarConversaoDistancia(
            Browser browser, double valorMilhas, double valorKm)
        {
            TelaConversaoDistancias tela =
                new TelaConversaoDistancias(_configuration, browser);

            tela.CarregarPagina();
            tela.PreencherDistanciaMilhas(valorMilhas);
            tela.ProcessarConversao();
            double resultado = tela.ObterDistanciaKm();
            tela.Fechar();

            Assert.Equal(valorKm, resultado);
        }
    }
}