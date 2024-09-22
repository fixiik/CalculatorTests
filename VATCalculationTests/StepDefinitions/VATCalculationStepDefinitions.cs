using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using TechTalk.SpecFlow;

[Binding]
public class VATCalculationStepDefinitions
{
    private IWebDriver _driver;
    private VATCalculatorPage _vatCalculatorPage;

    [BeforeScenario]
    public void Setup()
    {
        _driver = new ChromeDriver();
        _driver.Manage().Window.Maximize();
        _driver.Navigate().GoToUrl("https://www.calkoo.com/en/vat-calculator");

        _vatCalculatorPage = new VATCalculatorPage(_driver);
        _vatCalculatorPage.AcceptCookies();
    }

    [AfterScenario]
    public void TearDown()
    {
        _driver.Quit();
    }

    [When(@"I select ""(.*)"" from the country drop-down")]
    public void SelectTheCountryFromDropDown(string country)
    {
        _vatCalculatorPage.SelectCountry(country);
    }

    [When(@"I check the ""([^""]*)"" checkbox")]
    public void CheckVATRate(string vatRate)
    {
        _vatCalculatorPage.CheckByValue(vatRate);
    }

    [When(@"I enter ""([^""]*)"" in the ""([^""]*)"" field")]
    public void EnterAmountInTheField(string amount, string inputType)
    {
        _vatCalculatorPage.EnterAmount(amount, inputType);
    }

    [Then(@"the system should calculate the ""([^""]*)"" in the ""([^""]*)"" field")]
    public void AssertResultIsCalculatedAsExpected(string result, string outputType)
    {
        Assert.AreEqual(result, _vatCalculatorPage.GetResult(outputType));
    }
}