using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace XUnitTests.Tests.DomainTests
{
    public class ProductTests
    {
        /// <summary>
        /// This test case is created for testing the properties where they are marked.
        /// with required which means that they can not be empty otherwise it will give errror.
        /// </summary>
        [Fact]
        public void Product_ValidData_ShouldPassValidation()
        {
            // Arrange
            var product = new Product
            {

                ProductDescription = "ProductDescription",
                ProductCode = "ProductCode"
            };

            // Act
            var validationContext = new ValidationContext(product);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, validationContext, validationResults, true);

            // Assert
            Assert.True(isValid);

        }
        /// <summary>
        /// This test shows that ProductDescription should not be empty or null.
        /// As in this we are testing ProductDescription if it is empty or null then it will give false as we are expecting in assert
        /// and second assert is used for verifying that the give result is missing from the validationResults.
        /// </summary>
        /// <param name="description"></param>
        [Theory]
        [InlineData("")] // Missing ProductDescription
        [InlineData(null)] // Empty ProductDescription
        public void Product_ProductDescription_Validation(string description)
        {
            //Arrange 
            var product = new Product
            {
                ProductCode = "ProductCode"
            };
            product.ProductDescription = description;
            //
            var validationContext = new ValidationContext(product);
            var validationResults= new System.Collections.Generic.List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, validationContext, validationResults, true);
            if(string.IsNullOrEmpty(description))
            {
                Assert.False(isValid);
                Assert.Contains(validationResults, result => result.MemberNames.Contains("ProductDescription"));
            }
        }
        /// <summary>
        /// This test shows that ProductCode should not be empty or null.
        /// As in this we are testing ProductCode if it is empty or null then it will give false as we are expecting in assert
        /// and second assert is used for verifying that the give result is missing from the validationResults.
        /// </summary>
        /// <param name="code"></param>
        [Theory]
        [InlineData("")] //Missing ProductCode
        [InlineData(null)] //Empty ProductCode
        public void Product_ProductCode_Validation(string code)
        {
            //Arrange 
            var product = new Product
            {
                ProductDescription = "ProductDescription"
            };
            product.ProductCode = code;
            //
            var validationContext = new ValidationContext(product);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, validationContext, validationResults, true);
            if (string.IsNullOrEmpty(code))
            {
                Assert.False(isValid);
                Assert.Contains(validationResults, result => result.MemberNames.Contains("ProductCode"));
            }
        }
        /// <summary>
        /// This test shows that Quantity should not be zero, there should always be any value.
        /// As in this we are testing quantity equals to zero then first assert condition should be false
        /// and second assert is used for verifying that the given result is missing from the validationResults.
        /// </summary>
        /// <param name="quantity"></param>

        [Theory]
        //[InlineData(0)]       
        [InlineData(1)]  // Positive Quantity     
        [InlineData(int.MaxValue)]  // Max positive Quantity
        public void Product_ProductId_Validation(int quantity)
        {
            // Arrange
            var product = new Product
            {
                ProductDescription = "ProductDescription",
                ProductCode = "ProductCode"
            };
            product.Qty = quantity;

            // Act
            var validationContext = new ValidationContext(product);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, validationContext, validationResults, true);

            // Assert
            if (quantity == 0)
            {
                Assert.False(isValid);
                Assert.Contains(validationResults, result => result.MemberNames.Contains("Id"));
            }
        }
        /// <summary>
        /// This test shows that instructionId should not be zero, there should always be any value.
        /// As in this we are testing instructionId equals to zero then first assert condition should be false
        /// and second assert is used for verifying that the given result is missing from the validationResults.
        /// </summary>
        /// <param name="instructionId"></param>
        [Theory]
        //[InlineData(0)] // Zero Instructionid
        //[InlineData(-1)] // Negative InstructionId
        [InlineData(1)]  // Positive InstructionId    
        [InlineData(int.MaxValue)] // Maximum InstructionId
        public void Product_InstructionId_Validation(int instructionId)
        {
            // Arrange
            var product = new Product
            {
                ProductDescription = "ProductDescription",
                ProductCode = "ProductCode"
            };
            product.Qty = instructionId;

            // Act
            var validationContext = new ValidationContext(product);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, validationContext, validationResults, true);

            // Assert
            if (instructionId == 0)
            {
                Assert.False(isValid);
                Assert.Contains(validationResults, result => result.MemberNames.Contains("Id"));
            }
        }
    }
}
