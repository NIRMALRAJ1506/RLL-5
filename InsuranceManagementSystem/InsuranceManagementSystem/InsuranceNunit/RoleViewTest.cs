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
    public class RoleViewTests
    {
        [Test]
        public void Valid_RoleView()
        {
            var roleView = new RoleView
            {
                RoleId = 1,
                Name = "User"
            };
            Assert.DoesNotThrow(() => ValidateModel(roleView));
        }

        [Test]
        public void Name_Required()
        {
            var roleView = new RoleView
            {
                RoleId = 1
            };
            Assert.Throws<ValidationException>(() => ValidateModel(roleView));
        }

        [Test]
        public void Name_MaxLength()
        {
            var roleView = new RoleView
            {
                RoleId = 1,
                Name = new string('A', 50)
            };
            Assert.DoesNotThrow(() => ValidateModel(roleView));
        }

        [Test]
        public void Name_ExceedsMaxLength()
        {
            var roleView = new RoleView
            {
                RoleId = 1,
                Name = new string('A', 101)
            };
            Assert.Throws<ValidationException>(() => ValidateModel(roleView));
        }

        private void ValidateModel(RoleView model)
        {
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            if (validationResults.Any())
            {
                var errorMessage = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException(errorMessage);
            }
        }
    }
}
