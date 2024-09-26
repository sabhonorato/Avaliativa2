using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustoFuncionarioApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using CustoFuncionarioApp.Models;
using System.Threading.Tasks;

namespace CustoFuncionarioApp.Controllers.Tests
{
    [TestClass()]
    public class CalculoControllerTests
    {
        private CalculoController controller;

        [TestInitialize]
        public void inicializar()
        {
            controller = new CalculoController();
        }

        [TestMethod]
        [DataRow(5423.17, 837.56, 654.32, 123.45)]
        [DataRow(9067.43, 0.00, 0.00, 0.00)]
        [DataRow(1478.29, 256.34, 78.90, 102.34)]
        [DataRow(3956.78, 1200.00, 500.00, 800.00)]
        [DataRow(2345.00, 0.00, 0.00, 0.00)]
        [DataRow(3789.11, 450.25, 250.00, 300.00)]
        [DataRow(876.54, 320.00, 90.25, 50.00)]
        [DataRow(5600.88, 650.45, 150.00, 380.00)]
        public void TestarCusto(double salarioBruto, double planoSaude, double seguroVida, double outrosBeneficios)
        {
            var custo = new Custo
            {
                SalarioBruto = Convert.ToDecimal(salarioBruto),
                PlanoSaude = Convert.ToDecimal(planoSaude),
                SeguroVida = Convert.ToDecimal(seguroVida),
                OutrosBeneficios = Convert.ToDecimal(outrosBeneficios)
            };

            Assert.AreEqual(Convert.ToDecimal(salarioBruto), custo.SalarioBruto);
        }

        [TestMethod]
        [DataRow(2675.45, 512.78, 180.34, 289.67)]
        [DataRow(1345.12, 295.40, 125.90, 98.75)]
        [DataRow(4890.26, 675.20, 230.10, 412.85)]
        public void Relatorio_EntradaValida_RetornaViewComModelo(double salarioBruto, double planoSaude, double seguroVida, double outrosBeneficios)
        {
            // Arrange
            var custo = new Custo
            {
                SalarioBruto = Convert.ToDecimal(salarioBruto),
                PlanoSaude = Convert.ToDecimal(planoSaude),
                SeguroVida = Convert.ToDecimal(seguroVida),
                OutrosBeneficios = Convert.ToDecimal(outrosBeneficios)
            };

            // Act
            var resultado = controller.Relatorio(custo) as ViewResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(custo, resultado.Model);
        }


        [TestMethod]
        public void requisicao_DeveRetornarTipoProdutos()
        {
            // Arrange
            var esperado = typeof(Custo);
            var custo = new Custo
            {
                SalarioBruto = 3000M,
                PlanoSaude = 500M,
                SeguroVida = 100M,
                OutrosBeneficios = 200M
            };

            // Act
            var resultado = controller.Relatorio(custo);
            var viewResult = resultado as ViewResult;
            var obtido = viewResult?.Model;

            // Assert
            Assert.IsNotNull(viewResult);
            Assert.AreEqual(esperado, obtido?.GetType());
        }





        [TestClass]
        public class CustoTests
        {
            [TestMethod]
            public void Testar_Calculo_CustoTotal()
            {
                // Arrange
                var custo = new Custo
                {
                    SalarioBruto = 3000M,
                    PlanoSaude = 500M,
                    SeguroVida = 100M,
                    OutrosBeneficios = 200M
                };

                // Act
                var custoTotal = custo.getCustoTotal();

                // Assert
                decimal inssEsperado = custo.getINSS_Valor();
                decimal fgtsEsperado = custo.getFGTS();
                decimal decimoTerceiroEsperado = custo.get13oSalario();
                decimal feriasEsperado = custo.getFerias();
                decimal custoTotalEsperado = custo.SalarioBruto + inssEsperado + fgtsEsperado + decimoTerceiroEsperado + feriasEsperado + custo.PlanoSaude + custo.SeguroVida + custo.OutrosBeneficios;

                Assert.AreEqual(custoTotalEsperado, custoTotal);
            }
        }





    }
}