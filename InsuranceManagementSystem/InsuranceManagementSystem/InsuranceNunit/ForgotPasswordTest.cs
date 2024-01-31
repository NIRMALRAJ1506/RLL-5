using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Models;

namespace InsuranceNunit
{
    [TestFixture]
    public class ForgetPasswordViewModelTests
    {
        [Test]
        public void Username_Required()
        {
            // Arrange
            var viewModel = new ForgetPasswordViewModel
            {
                Email = "test@example.com",
                NewPassword = "NewPass123",
                ConfirmPassword = "NewPass123"
            };

            // Act & Assert
            Assert.Throws<ValidationException>(() => ValidateModel(viewModel));
        }

        [Test]
        public void Email_Required()
        {
            // Arrange
            var viewModel = new ForgetPasswordViewModel
            {
                Username = "testuser",
                NewPassword = "NewPass123",
                ConfirmPassword = "NewPass123"
            };

            // Act & Assert
            Assert.Throws<ValidationException>(() => ValidateModel(viewModel));
        }

        [Test]
        public void Email_InvalidFormat()
        {
            // Arrange
            var viewModel = new ForgetPasswordViewModel
            {
                Username = "testuser",
                Email = "invalid-email",
                NewPassword = "NewPass123",
                ConfirmPassword = "NewPass123"
            };

            // Act & Assert
            Assert.Throws<ValidationException>(() => ValidateModel(viewModel));
        }

        [Test]
        public void NewPassword_Required()
        {
            // Arrange
            var viewModel = new ForgetPasswordViewModel
            {
                Username = "testuser",
                Email = "test@example.com",
                ConfirmPassword = "NewPass123"
            };

            // Act & Assert
            Assert.Throws<ValidationException>(() => ValidateModel(viewModel));
        }

        [Test]
        public void ConfirmPassword_Required()
        {
            // Arrange
            var viewModel = new ForgetPasswordViewModel
            {
                Username = "testuser",
                Email = "test@example.com",
                NewPassword = "NewPass123"
            };

            // Act & Assert
            Assert.Throws<ValidationException>(() => ValidateModel(viewModel));
        }

        [Test]
        public void ConfirmPassword_DoesNotMatchNewPassword()
        {
            // Arrange
            var viewModel = new ForgetPasswordViewModel
            {
                Username = "testuser",
                Email = "test@example.com",
                NewPassword = "NewPass123",
                ConfirmPassword = "MismatchedPass"
            };

            // Act & Assert
            Assert.Throws<ValidationException>(() => ValidateModel(viewModel));
        }

        [Test]
        public void Valid_Model()
        {
            // Arrange
            var viewModel = new ForgetPasswordViewModel
            {
                Username = "testuser",
                Email = "test@example.com",
                NewPassword = "NewPass123",
                ConfirmPassword = "NewPass123"
            };

            // Act & Assert
            Assert.DoesNotThrow(() => ValidateModel(viewModel));
        }

        private void ValidateModel(ForgetPasswordViewModel model)
        {
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);

            if (validationResults.Any())
            {
                var errorMessage = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException(errorMessage);
            }
        }
    }
}