using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

public class VATCalculatorPage
{
    private readonly IWebDriver _driver;

    public VATCalculatorPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void AcceptCookies()
    {
        try
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement consentButton = wait.Until(d => d.FindElement(By.CssSelector("button[aria-label='Consent']")));
            consentButton.Click();
        }
        catch (NoSuchElementException ex)
        {
            Console.WriteLine("Cookie popup not found or already accepted: " + ex.Message);
        }
        catch (WebDriverTimeoutException ex)
        {
            Console.WriteLine("Cookie popup did not appear within the expected time: " + ex.Message);
        }
    }

    public void SelectCountry(string country)
    {
        var countryDropdown = _driver.FindElement(By.Name("Country"));
        var selectElement = new SelectElement(countryDropdown);
        selectElement.SelectByText(country);
    }

    public void CheckByValue(string value)
    {
        var checkboxes = _driver.FindElements(By.XPath("//input[@type='radio']"));
        foreach (var checkbox in checkboxes)
        {
            var label = checkbox.FindElement(By.XPath("following-sibling::label")).Text;
            if (label.Contains(value))
            {
                var jsExecutor = (IJavaScriptExecutor)_driver;
                jsExecutor.ExecuteScript("arguments[0].click();", checkbox);
                break;
            }
        }
    }

    public void EnterAmount(string amount, string inputType)
    {
        var inputField = _driver.FindElement(By.Name(inputType));
        inputField.Clear();
        inputField.SendKeys(amount);
    }

    public string GetResult(string outputType)
    {
        var resultField = _driver.FindElement(By.Name(outputType));
        return resultField.GetAttribute("value");
    }
}