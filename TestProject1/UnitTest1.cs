using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using Xunit;
public class DesktopTest : IDisposable
{
    // В версии 4.3.0 используем <WindowsElement>
    private readonly WindowsDriver<WindowsElement> _driver;
    private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
    private const string AppPath = @"C:\ROBOTA\TestIrovanieProgramModule\8\WpfApp1\WpfApp1\bin\Debug\net10.0-windows\WpfApp1.exe";



    public DesktopTest()
    {
        var options = new AppiumOptions();

        // В версии 4.3.0 этот метод называется AddAdditionalCapability
        // Он отправляет "чистые" ключи (app, deviceName), которые понимает WinAppDriver
        options.AddAdditionalCapability("app", AppPath);
        options.AddAdditionalCapability("deviceName", "WindowsPC");
        options.AddAdditionalCapability("platformName", "Windows");

        _driver = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), options);
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
    }

    [Fact]
    public void Test_LoginProcess()
    {
        // В версии 4.3.0 методы поиска называются по-старому
        var loginInput = _driver.FindElementByAccessibilityId("LoginField");
        loginInput.SendKeys("Student_Admin");

        var submitButton = _driver.FindElementByAccessibilityId("LoginButton");
        submitButton.Click();

        var resultText = _driver.FindElementByAccessibilityId("StatusText").Text;
        Assert.Contains("Welcome", resultText);
    }

    public void Dispose()
    {
        _driver?.Quit();
        _driver?.Dispose();
    }
}