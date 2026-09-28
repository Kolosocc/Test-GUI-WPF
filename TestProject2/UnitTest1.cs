using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;
using Xunit;

public class DesktopTest : IDisposable
{
    // Используем базовый класс AppiumDriver для обхода бага парсинга адреса в WindowsDriver v9
    private readonly AppiumDriver _driver;
    private const string AppiumServerUrl = "http://127.0.0";
    private const string AppPath = @"C:\ROBOTA\TestIrovanieProgramModule\8\WpfApp1\WpfApp1\bin\Debug\net10.0-windows\WpfApp1.exe";

    public DesktopTest()
    {
        var options = new AppiumOptions();

        options.PlatformName = "Windows";
        options.AutomationName = "Windows";
        options.App = AppPath;

        // Явно прописываем адрес внешнего WAD, чтобы Appium v3 не падал на проверке статуса
        options.AddAdditionalAppiumOption("appium:wadUrl", "http://127.0.0");
        options.AddAdditionalAppiumOption("ms:experimental-webdriver", true);

        try
        {
            // Инициализируем через универсальный AppiumDriver — он корректно прочитает порт 4723
            _driver = new AppiumDriver(new Uri(AppiumServerUrl), options);

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }
        catch (Exception ex)
        {
            throw new Exception($"ОШИБКА ИНФРАСТРУКТУРЫ: {ex.Message}");
        }
    }

    [Fact]
    public void Test_LoginProcess()
    {
        var loginInput = _driver.FindElement(MobileBy.AccessibilityId("LoginField"));
        loginInput.Clear();
        loginInput.SendKeys("Student_Admin");

        var submitButton = _driver.FindElement(MobileBy.AccessibilityId("LoginButton"));
        submitButton.Click();

        var resultTextElement = _driver.FindElement(MobileBy.AccessibilityId("StatusText"));
        string actualText = resultTextElement.Text;
        Assert.Contains("Welcome", actualText);
    }

    public void Dispose()
    {
        if (_driver != null)
        {
            _driver.Quit();
        }
    }
}
