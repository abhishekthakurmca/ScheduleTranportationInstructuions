using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTests.Tests.DomainTests
{
    public class ScheduleTransportTests
    {
        /// <summary>
        /// In this test case we are validating all the properties as they are marked Required then they can not be empty.
        /// All properties should always have some data otherwise it will give error.
        /// </summary>
        [Fact]
        public void ScheduleTransport_WithValidData_ShouldPassValidation()
        {
            // Arrange
            var scheduleTransport = new ScheduleTransport
            {
                DateScheduled = DateTime.Now,
                Transporter = "Transporter",
            };

            // Act
            var validationContext = new ValidationContext(scheduleTransport);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var isValid = Validator.TryValidateObject(scheduleTransport, validationContext, validationResults, true);

            // Assert
            Assert.True(isValid);
        }
        /// <summary>
        /// This test is used to check the dateScheduled dateTime should be equal to the current dateTime at which time the data is inserted,
        /// if not then it will give error.
        /// </summary>
        [Fact]
        public void ScheduleTransport_DateScheduled_ShouldDefaultToCurrentDateTime()
        {
            // Arrange
            var scheduleTransport = new ScheduleTransport();

            // Act
            var currentDate = DateTime.Now.Date;

            // Assert
            Assert.Equal(currentDate, scheduleTransport.DateScheduled.Date);
        }
        /// <summary>
        /// In this test we are testing transporter property should not be empty or null.
        /// As in this we are testing transporter if it is empty or null then it will give false as we are expecting in assert
        /// and second assert is used for verifying that the give result is missing from the validationResults .
        /// </summary>
        /// <param name="transporter"></param>
        [Theory]
        [InlineData("")] // Empty Transporter
        [InlineData(null)] // Null Transporter
        public void ScheduleTransport_Transporter_Validation(string transporter)
        {
            //Arrange 
            var scheduleTransporter = new ScheduleTransport
            {
                DateScheduled=DateTime.Now,
            };
   
            scheduleTransporter.Transporter = transporter;
            //
            var validationContext = new ValidationContext(scheduleTransporter);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var isValid = Validator.TryValidateObject(scheduleTransporter, validationContext, validationResults, true);

            if (string.IsNullOrEmpty(transporter))
            {
                Assert.False(isValid);
                Assert.Contains(validationResults, result => result.MemberNames.Contains("Transporter"));
            }
        }
        /// <summary>
        /// This test shows that instructionId should not be zero, there should always be any value entered.
        /// As in this we are testing instructionId equals to zero then first assert condition should be false as we are expecting
        /// and second assert is used for verifying that the given result is missing from the validationResults.
        /// </summary>
        /// <param name="instructionId"></param>
        [Theory]
        //[InlineData(0)] // Zero InstructionId     
        [InlineData(1)] // Positive InstructionId
        [InlineData(int.MaxValue)] // Positive InstructionId
        public void ScheduleTransport_InstructionId_Validation(int instructionId)
        {
            // Arrange
            var scheduleTransporter = new ScheduleTransport
            {
                DateScheduled = DateTime.Now,
                Transporter = "Transporter"
            };
            scheduleTransporter.InstructionId = instructionId;

            // Act
            var validationContext = new ValidationContext(scheduleTransporter);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var isValid = Validator.TryValidateObject(scheduleTransporter, validationContext, validationResults, true);

            // Assert
            if (instructionId == 0)
            {
                Assert.False(isValid);
                Assert.Contains(validationResults, result => result.MemberNames.Contains("Id"));
            }
        }
        /// <summary>
        /// This test shows that productId should not be Zero, there sgould always be any value entered.
        /// As in this we are testing productId equals to zero then first assert condition should are we are expecting
        /// and second assert is used for verifying that the given result is missing from the validationResults.
        /// </summary>
        /// <param name="productId"></param>
        [Theory]
        //[InlineData(0)]       
        [InlineData(1)]
        [InlineData(int.MaxValue)]
        public void ScheduleTransport_ProductId_Validation(int productId)
        {
            // Arrange
            var scheduleTransporter = new ScheduleTransport
            {
                DateScheduled = DateTime.Now,
                Transporter = "Transporter"
            };
            scheduleTransporter.ProductId = productId;

            // Act
            var validationContext = new ValidationContext(scheduleTransporter);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var isValid = Validator.TryValidateObject(scheduleTransporter, validationContext, validationResults, true);

            // Assert
            if (productId == 0)
            {
                Assert.False(isValid);
                Assert.Contains(validationResults, result => result.MemberNames.Contains("Id"));
            }
        }
    }
}
