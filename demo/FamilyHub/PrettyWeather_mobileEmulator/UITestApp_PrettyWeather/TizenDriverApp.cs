using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Tizen;
using OpenQA.Selenium.Remote;
using System.Drawing;

namespace UITestApp_PrettyWeather
{
    public class TizenDriverApp
    {
        TizenDriver<TizenElement> _driver;
        RemoteTouchScreen _touchScreen;

        string address = "http://127.0.0.1:4723/wd/hub";

        public TizenDriverApp()
        {
            AppiumOptions appiumOptions = new AppiumOptions();

            appiumOptions.AddAdditionalCapability("platformName", "Tizen");
            appiumOptions.AddAdditionalCapability("deviceName", "emulator-26101");

            /// need to install this app on target
            appiumOptions.AddAdditionalCapability("appPackage", "org.tizen.PrettyWeather.Tizen");

            _driver = new TizenDriver<TizenElement>(new Uri(address), appiumOptions);

            _touchScreen = new RemoteTouchScreen(_driver);
        }

        public void Quit()
        {
            _driver.Quit();
        }

        public string GetText(string automationId)
        {
            return _driver.FindElementByAccessibilityId(automationId).Text;
        }

        public void SetText(string automationId, string text)
        {
            _driver.FindElementByAccessibilityId(automationId).SetAttribute("Text", text);
        }

        public void ClearText(string automationId)
        {
            _driver.FindElementByAccessibilityId(automationId).Clear();
        }

        public void ReplaceText(string automationId, string text)
        {
            _driver.FindElementByAccessibilityId(automationId).ReplaceValue(text);
        }

        public string GetAttribute(string automationId, string attribute)
        {
            return _driver.FindElementByAccessibilityId(automationId).GetAttribute(attribute);
        }

        public void SetAttribute(string automationId, string attribute, string value)
        {
            _driver.FindElementByAccessibilityId(automationId).SetAttribute(attribute, value);
        }

        public Size GetSize(string automationId)
        {
            return _driver.FindElementByAccessibilityId(automationId).Size;
        }

        public Point GetLocation(string automationId)
        {
            return _driver.FindElementByAccessibilityId(automationId).Location;
        }

        public bool GetDisplayed(string automationId)
        {
            return _driver.FindElementByAccessibilityId(automationId).Displayed;
        }

        public bool GetEnabled(string automationId)
        {
            return _driver.FindElementByAccessibilityId(automationId).Enabled;
        }

        public void Click(string automationId)
        {
            _driver.FindElementByAccessibilityId(automationId).Click();
        }

        public void Down(int x, int y)
        {
            _touchScreen.Down(x, y);
        }

        public void Up(int x, int y)
        {
            _touchScreen.Up(x, y);
        }

        public void Move(int x, int y)
        {
            _touchScreen.Move(x, y);
        }

        public void Flick(int speedX, int speedY)
        {
            _touchScreen.Flick(speedX, speedY);
        }

        public void ExecuteScript(string script)
        {
            _driver.ExecuteScript(script);
        }
    }
}
