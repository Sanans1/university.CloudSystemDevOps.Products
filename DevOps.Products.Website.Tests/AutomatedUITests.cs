using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DevOps.Products.Website.Tests
{
    public class AutomatedUITests : IDisposable
    {
        private readonly IWebDriver _driver;
        public AutomatedUITests()
        {
            _driver = new ChromeDriver();
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Test]
        public async Task ProductListPage_WhenExecuted_ReturnsProductListPage()
        {
            //_driver.Navigate().GoToUrl("localhost:63651");

            //IWebElement header = _driver.FindElement(By.Id("TitleHeader"));
            //Assert.IsTrue(header.Displayed);
            //Assert.AreEqual("Products", header.Text);
        }
    }
}
