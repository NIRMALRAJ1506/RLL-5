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
    public class QuestionViewTests
    {
        [Test]
        public void QuestionId_NotRequired()
        {
            var questionView = new QuestionView
            {
                Question = "Test question",
                Date = DateTime.Now,
                Answer = "Test answer",
                CustomerId = 123
            };

            Assert.DoesNotThrow(() => ValidateModel(questionView));
        }

        [Test]
        public void Question_LengthWithinLimit()
        {

            var questionView = new QuestionView
            {
                Question = new string('A', 255),
                Date = DateTime.Now,
                Answer = "Test answer",
                CustomerId = 123
            };

            Assert.DoesNotThrow(() => ValidateModel(questionView));
        }

        [Test]
        public void Date_DefaultsToCurrentDate()
        {
            var questionView = new QuestionView
            {
                Question = "Test question",
                Answer = "Test answer",
                CustomerId = 123
            };

            DateTime currentDate = DateTime.Now;

            Assert.That(questionView.Date.Date, Is.EqualTo(currentDate.Date));
        }

        [Test]
        public void Answer_LengthWithinLimit()
        {
            var questionView = new QuestionView
            {
                Question = "Test question",
                Date = DateTime.Now,
                Answer = new string('A', 255),
                CustomerId = 123
            };

            Assert.DoesNotThrow(() => ValidateModel(questionView));
        }

        [Test]
        public void CustomerId_Required()
        {
            var questionView = new QuestionView
            {
                Question = "Test question",
                Date = DateTime.Now,
                Answer = "Test answer",
                CustomerId = 123
            };

            Assert.DoesNotThrow(() => ValidateModel(questionView));
        }


        private void ValidateModel(QuestionView model)
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
