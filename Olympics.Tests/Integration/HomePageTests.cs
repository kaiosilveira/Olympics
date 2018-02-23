using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Olympics.Tests.Integration
{
    public class HomePageTests
    {
        [Fact]
        public void ICanManipulateCountriesMedals()
        {
            IWebDriver driver = new ChromeDriver(@"C:\Users\Kaio Silveira\documents\visual studio 2015\Projects\Olympics\Olympics.Tests\");
            
            driver.Navigate().GoToUrl("http://localhost:55604/");
            
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElement(By.ClassName("btn-add")));
            
            driver.FindElement(By.ClassName("btn-add")).Click();

            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElement(By.Id("country-name")));

            driver.FindElement(By.Id("country-name")).SendKeys("Teste");
            driver.FindElement(By.Id("gold-medals")).SendKeys("10");
            driver.FindElement(By.Id("silver-medals")).SendKeys("10");
            driver.FindElement(By.Id("bronze-medals")).SendKeys("10");
            
            driver.FindElement(By.ClassName("btn-success")).Click();

            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElement(By.ClassName("country-box")));

            var countryBoxes = driver.FindElements(By.ClassName("country-box"));
            var countryBox = countryBoxes.Last();

            Assert.Equal("Teste", countryBox.FindElement(By.ClassName("country-name")).Text);
            Assert.Equal("10", countryBox.FindElement(By.ClassName("gold-amount")).Text);
            Assert.Equal("10", countryBox.FindElement(By.ClassName("silver-amount")).Text);
            Assert.Equal("10", countryBox.FindElement(By.ClassName("bronze-amount")).Text);

            countryBox.FindElement(By.ClassName("btn-danger")).Click();

            System.Threading.Thread.Sleep(200);

            Assert.Equal(countryBoxes.Count - 1, driver.FindElements(By.ClassName("country-box")).Count);
        }
    }
}
