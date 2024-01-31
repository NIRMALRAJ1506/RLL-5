using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using NUnit.Framework;
using System.Linq;
using DAL;
using UI.Models;

namespace InsuranceNunit
{
    [TestFixture]
    public class CustomerLoginTests
    {
        [Test]
        public void UserName_Required()
        {
            // Arrange
            var customerView = new CustomerView { Password = "Ajaykumar@1" };

            // Act & Assert
            Assert.Throws<ValidationException>(() => ValidateModel(customerView));
        }

        [Test]
        public void Password_Required()
        {
            // Arrange
            var customerView = new CustomerView { UserName = "Ajay15" };

            // Act & Assert
            Assert.Throws<ValidationException>(() => ValidateModel(customerView));
        }

        [Test]
        public void Valid_Model_Correct_Credentials()
        {
            // Arrange
            var customerView = new CustomerView { UserName = "Ajay15", Password = "Ajaykumar@1" };

            // Act & Assert
            Assert.DoesNotThrow(() => ValidateModel(customerView));
        }

        private void ValidateModel(CustomerView model)
        {
            // Perform model validation using DataAnnotations for UserName and Password
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = ValidateModelForUserNameAndPassword(model, validationContext);

            if (validationResults.Length > 0)
            {
                var errorMessage = FormatValidationResults(validationResults);
                throw new ValidationException(errorMessage);
            }

            // Now check the credentials against the database using your data access logic
            if (!CustomerExistsInDatabase(model))
            {
                throw new ValidationException("Customer does not exist in the database");
            }
        }

        private ValidationResult[] ValidateModelForUserNameAndPassword(object model, ValidationContext validationContext)
        {
            // Helper method for model validation, checking only UserName and Password
            var validationResults = new List<ValidationResult>();
            var propertiesToCheck = new[] { "UserName", "Password" };

            Validator.TryValidateObject(model, validationContext, validationResults, true);

            // Filter validation results to include only UserName and Password errors
            validationResults = validationResults
                .Where(result => propertiesToCheck.Contains(result.MemberNames.FirstOrDefault()))
                .ToList();

            return validationResults.ToArray();
        }

        private string FormatValidationResults(IEnumerable<ValidationResult> validationResults)
        {
            // Helper method to format validation results as a string
            var messages = validationResults.Select(result => result.ErrorMessage);
            return string.Join(Environment.NewLine, messages);
        }

        private bool CustomerExistsInDatabase(CustomerView model)
        {
            // Replace "YourConnectionString" with the actual connection string to your database
            var connectionString = "data source=DESKTOP-VQHITSP;initial catalog=InsuranceDb;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework\" providerName=\"System.Data.SqlClient";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if there is a Customer with the specified credentials in the database
                using (var command = new SqlCommand($"SELECT COUNT(*) FROM Customers WHERE UserName = '{model.UserName}' AND Password = '{model.Password}'", connection))
                {
                    return (int)command.ExecuteScalar() > 0;
                }
            }
        }
    }
}
