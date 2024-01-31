using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using NUnit.Framework;
using System.Linq;
using DAL;
using UI.Models;

namespace InsuranceNunit
{
    [TestFixture]
    public class AdminLoginTests
    {
        [Test]
        public void UserName_Required()
        {
            // Arrange
            var loginView = new LoginView { Password = "pass@123" };

            // Act & Assert
            Assert.Throws<ValidationException>(() => ValidateModel(loginView));
        }

        [Test]
        public void Password_Required()
        {
            // Arrange
            var loginView = new LoginView { UserName = "adminvij" };

            // Act & Assert
            Assert.Throws<ValidationException>(() => ValidateModel(loginView));
        }

        [Test]
        public void Valid_Model_Correct_Credentials()
        {
            // Arrange
            var loginView = new LoginView { UserName = "adminvij", Password = "pass@123" };

            // Act & Assert
            Assert.DoesNotThrow(() => ValidateModel(loginView));
        }

        private void ValidateModel(LoginView model)
        {
            // Perform model validation using DataAnnotations
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = ValidateModel(model, validationContext);

            if (validationResults.Length > 0)
            {
                var errorMessage = FormatValidationResults(validationResults);
                throw new ValidationException(errorMessage);
            }

            // Now check the credentials against the database using your data access logic
            if (!AdminExistsInDatabase(model))
            {
                throw new ValidationException("Admin does not exist in the database");
            }
        }

        private ValidationResult[] ValidateModel(object model, ValidationContext validationContext)
        {
            // Helper method for model validation
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults.ToArray();
        }

        private string FormatValidationResults(IEnumerable<ValidationResult> validationResults)
        {
            // Helper method to format validation results as a string
            var messages = validationResults.Select(result => result.ErrorMessage);
            return string.Join(Environment.NewLine, messages);
        }
        private bool AdminExistsInDatabase(LoginView model)
        {
            // Replace "YourConnectionString" with the actual connection string to your database
            var connectionString = "data source=DESKTOP-VQHITSP;initial catalog=InsuranceDb;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework\" providerName=\"System.Data.SqlClient";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if there is an Admin with the specified credentials in the database
                using (var command = new SqlCommand($"SELECT COUNT(*) FROM Admins WHERE UserName = '{model.UserName}' AND Password = '{model.Password}'", connection))
                {
                    return (int)command.ExecuteScalar() > 0;
                }
            }
        }
    }
}
