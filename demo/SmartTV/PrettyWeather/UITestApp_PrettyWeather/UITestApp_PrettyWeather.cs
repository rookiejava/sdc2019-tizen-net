using NUnit.Framework;
using System.Threading;

namespace UITestApp_PrettyWeather
{
    [TestFixture]
    public class Tests
    {
        TizenDriverApp Driver;

        [OneTimeSetUp]
        public void Setup()
        {
            Driver = AppInitializer.StartApp();
            Thread.Sleep(2000);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Driver.Quit();
        }

        [Test]
        /// <summary>
        /// Test to element click
        /// </summary>
        public void ClickSanJose()
        {
            Driver.Click("San Jose");
            Thread.Sleep(900);
            string text = Driver.GetText("cityName");
            Assert.True(text == "San Jose");
        }

        [Test]
        /// <summary>
        /// Test to get text of element
        /// </summary>
        public void ClickSanFrancisco()
        {
            Driver.Click("San Francisco");
            Thread.Sleep(900);
            string text = Driver.GetText("cityName");
            Assert.True(text == "San Francisco");
        }

        [Test]
        /// <summary>
        /// Test to set text of element
        /// </summary>
        public void ClickSeattle()
        {
            Driver.Click("Seattle");
            Thread.Sleep(900);
            Driver.Click("San Jose");
            Thread.Sleep(900);
            Driver.Click("San Francisco");
            Thread.Sleep(900);
            Driver.Click("Seoul");
            Thread.Sleep(900);
            Driver.Click("San Jose");
            Thread.Sleep(900);
            Driver.Click("San Francisco");
            Thread.Sleep(900);
            Driver.Click("Seattle");
            Thread.Sleep(900);
            Driver.Click("San Jose");
            Thread.Sleep(900);
            Driver.Click("San Francisco");
            Thread.Sleep(900);
            Driver.Click("Seoul");
            Thread.Sleep(900);
        }
    }
}
