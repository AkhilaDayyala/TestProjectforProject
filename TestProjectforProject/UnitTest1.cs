using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace TravelDeskSeleniumTests.Pages
{
    public class AdminLogin
    {
        IWebDriver driver;
        public AdminLogin()
        {

            driver = new ChromeDriver(@"C:\Users\91789\Downloads\chromedriver-win64\chromedriver-win64");
            driver.Navigate().GoToUrl("http://localhost:3000/");
            Thread.Sleep(6000);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Fact]
        public void AdminLoginAndAddUser()
        {
            driver.Navigate().GoToUrl("http://localhost:3000/login");
            Thread.Sleep(6000);
            driver.FindElement(By.Name("Email")).SendKeys("akhila@gmail.com");
            Thread.Sleep(2000);
            driver.FindElement(By.Name("Password")).SendKeys("akki@123");
            Thread.Sleep(2000);
            driver.FindElement(By.ClassName("btn1")).Click();
            Thread.Sleep(2000);

            Assert.True(driver.FindElement(By.TagName("h1")).Displayed);
            Thread.Sleep(2000);
            driver.FindElement(By.ClassName("add-button")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Name("Email")).SendKeys("priya@gmail.com");
            Thread.Sleep(2000);
            driver.FindElement(By.Name("FirstName")).SendKeys("Priya");
            Thread.Sleep(2000);
            driver.FindElement(By.Name("LastName")).SendKeys("Madhuri");
            Thread.Sleep(2000);
            driver.FindElement(By.Name("Address")).SendKeys("Hyderabad");
            Thread.Sleep(2000);

            driver.FindElement(By.Name("Password")).SendKeys("priya@123");
            Thread.Sleep(2000);
            driver.FindElement(By.Name("MobileNum")).SendKeys("9618765432");
            Thread.Sleep(2000);
            // Select Department
            var departmentSelect = new SelectElement(driver.FindElement(By.Name("Select Department")));
            departmentSelect.SelectByText("IT");
            Thread.Sleep(2000);

            // Select Role
            var roleSelect = new SelectElement(driver.FindElement(By.Name("Select Role")));
            roleSelect.SelectByText("Employee");
            Thread.Sleep(2000);

            // Select Manager (only if role is not 'Manager')
            if (driver.FindElements(By.Name("Select Manager")).Count > 0)
            {
                var managerSelect = new SelectElement(driver.FindElement(By.Name("Select Manager")));
                managerSelect.SelectByText("Rashmi Mechineni");
                Thread.Sleep(2000);
            }

            // Submit the form
            driver.FindElement(By.ClassName("submit-button")).Click();
            Thread.Sleep(2000);

            Assert.True(driver.FindElement(By.XPath("//td[contains(text(),'Priya')]")).Displayed);
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//td[contains(text(),'Priya')]/following-sibling::td/button[contains(@class, 'edit-button')]")).Click();
            Thread.Sleep(1000);

            driver.FindElement(By.Name("LastName")).SendKeys("Madhu");
            Thread.Sleep(2000);
            driver.FindElement(By.ClassName("submit-button")).Click();
            Thread.Sleep(2000);


            // Assert that the user is present in the UserTable
            Assert.True(driver.FindElement(By.XPath("//td[contains(text(),'Priya')]")).Displayed);
            Thread.Sleep(1000);

            // Delete the user
            driver.FindElement(By.XPath("//td[contains(text(),'Priya')]/following-sibling::td/button[contains(@class, 'delete-button')]")).Click();
            Thread.Sleep(1000);

            // Confirm deletion in alert box
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(2000);
            Assert.True(driver.FindElement(By.Name("logout")).Displayed);
            Thread.Sleep(1000);
            driver.FindElement(By.Name("logout")).Click();
            Thread.Sleep(2000);

            Assert.Equal("http://localhost:3000/login", driver.Url);
            Thread.Sleep(6000);
            driver.FindElement(By.Name("Email")).SendKeys("shubham@gmail.com");
            Thread.Sleep(2000);
            driver.FindElement(By.Name("Password")).SendKeys("shubham");
            Thread.Sleep(2000);
            driver.FindElement(By.ClassName("btn1")).Click();
            Thread.Sleep(3000);

            Assert.True(driver.FindElement(By.ClassName("createBtn")).Displayed);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement createButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("createBtn")));
            Assert.True(createButton.Displayed);

            // Now attempt to click it
            createButton.Click();

            driver.FindElement(By.Name("projectId")).SendKeys("DWF");
            Thread.Sleep(2000);
            driver.FindElement(By.Name("reasonForTravel")).SendKeys("Project Presentation");
            Thread.Sleep(2000);
            driver.FindElement(By.Name("fromDate")).SendKeys("24-12-2024");
            Thread.Sleep(1000);
            driver.FindElement(By.Name("toDate")).SendKeys("02-01-2025");
            Thread.Sleep(2000);
            driver.FindElement(By.Name("fromLocation")).SendKeys("India");
            Thread.Sleep(2000);
            driver.FindElement(By.Name("toLocation")).SendKeys("UK");
            Thread.Sleep(2000);


            driver.FindElement(By.ClassName("btns")).Click();
            Thread.Sleep(1000);

            var alert = driver.SwitchTo().Alert();
            Assert.Equal("Travel Request submitted successfully!", alert.Text);
            alert.Accept();
            Thread.Sleep(1000);

            Assert.True(driver.FindElement(By.Name("logout")).Displayed);
            Thread.Sleep(1000);
            driver.FindElement(By.Name("logout")).Click();
            Thread.Sleep(2000);
            Assert.Equal("http://localhost:3000/login", driver.Url);
            Thread.Sleep(6000);

            driver.FindElement(By.Name("Email")).SendKeys("rashmi@gmail.com");
            Thread.Sleep(2000);
            driver.FindElement(By.Name("Password")).SendKeys("rashmi@123");
            Thread.Sleep(2000);
            driver.FindElement(By.ClassName("btn1")).Click();
            Thread.Sleep(2000);

            //Assert.True(driver.FindElement(By.ClassName("reject-button")).Displayed);
            //Thread.Sleep(2000);

            //driver.FindElement(By.ClassName("reject-button")).Click();
            //Thread.Sleep(2000);
            //IAlert commentAlert = driver.SwitchTo().Alert();
            //commentAlert.SendKeys("Give the proper reason for travel");
            //commentAlert.Accept();
            //Thread.Sleep(2000);
            //driver.SwitchTo().Alert().Accept();
            //Thread.Sleep(2000);

            Assert.True(driver.FindElement(By.ClassName("approve-button")).Displayed);
            Thread.Sleep(2000);
            driver.FindElement(By.ClassName("approve-button")).Click();
            Thread.Sleep(2000);

            IAlert cmtAlert = driver.SwitchTo().Alert();
            cmtAlert.SendKeys("Approved");
            cmtAlert.Accept();
            Thread.Sleep(2000);
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(2000);

            Assert.True(driver.FindElement(By.Name("logout")).Displayed);
            Thread.Sleep(1000);
            driver.FindElement(By.Name("logout")).Click();
            Thread.Sleep(2000);


            Assert.Equal("http://localhost:3000/login", driver.Url);
            Thread.Sleep(4000);

            driver.FindElement(By.Name("Email")).SendKeys("sushan@gmail.com");
            Thread.Sleep(2000);
            driver.FindElement(By.Name("Password")).SendKeys("sushan@123");
            Thread.Sleep(2000);
            driver.FindElement(By.ClassName("btn1")).Click();
            Thread.Sleep(2000);

            Assert.True(driver.FindElement(By.ClassName("ReturnToManager")).Displayed);
            Thread.Sleep(2000);
            driver.FindElement(By.ClassName("ReturnToManager")).Click();
            Thread.Sleep(1000);
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(2000);
            Assert.True(driver.FindElement(By.ClassName("ReturnToEmployee")).Displayed);
            Thread.Sleep(1000);

            driver.FindElement(By.ClassName("ReturnToEmployee")).Click();
            Thread.Sleep(1000);
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(2000);


            Assert.True(driver.FindElement(By.ClassName("BookTicket")).Displayed);
            Thread.Sleep(2000);
            driver.FindElement(By.ClassName("BookTicket")).Click();
            Thread.Sleep(7000);

            alert = driver.SwitchTo().Alert();
            Assert.Equal("Ticket booked successfully!", alert.Text);
            alert.Accept();
            Thread.Sleep(2000);






            Assert.True(driver.FindElement(By.ClassName("closeRequest")).Displayed);
            Thread.Sleep(2000);

            driver.FindElement(By.ClassName("closeRequest")).Click();
            Thread.Sleep(1000);
            driver.SwitchTo().Alert().Accept();
            //Thread.Sleep(2000);
            Thread.Sleep(2000);
            Assert.True(driver.FindElement(By.ClassName("downloadTicket")).Displayed);
            Thread.Sleep(1000);
            driver.FindElement(By.ClassName("downloadTicket")).Click();
            Thread.Sleep(3000);

           
            Assert.True(driver.FindElement(By.ClassName("login-button")).Displayed);
            Thread.Sleep(1000);
            driver.FindElement(By.ClassName("login-button")).Click();
            Thread.Sleep(2000);
            Assert.Equal("http://localhost:3000/login", driver.Url);
            driver.Close();


        }
    }
}