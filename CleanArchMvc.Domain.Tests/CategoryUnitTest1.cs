using CleanArchMvc.Domain.Entities;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests
{
    public class CategoryUnitTest1
    {
        [Fact(DisplayName = "Create Category With Valid State")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Category(1, "Category Name ");
            action.Should()
                  .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Category With id negative")]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Category(-1, "Category Name ");
            action.Should()
                  .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                  .WithMessage("Invalid id.");
        }

        [Fact(DisplayName = "Create Category With name less than 3")]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Category(1, "Ca");
            action.Should()
                  .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                  .WithMessage("Invalid name, too short, minimun 3 charecters.");
        }

        [Fact(DisplayName = "Create Category With name Empty")]
        public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Category(1, "");
            action.Should()
                  .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                  .WithMessage("Invalid name. Name is required.");
        }

        [Fact(DisplayName = "Create Category With name null")]
        public void CreateCategory_WithNullNameValue_DomainExceptionInvalidName()
        {
            Action action = () => new Category(1, null);
            action.Should()
                  .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }
    }
}
