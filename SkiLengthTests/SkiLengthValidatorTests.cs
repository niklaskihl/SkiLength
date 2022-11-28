using FluentValidation.TestHelper;
using SkiLengthApi;
using SkiLengthUtilities;

namespace SkiLengthTests
{
    public class SkiLengthValidatorTests
    {
        private readonly SkiLengthValidator _validator = new SkiLengthValidator();

        [Theory]
        [InlineData(22, 170, SkiType.Classic)]
        public void ValidModels_Error(int age, int length, string skiType)
        {
            var model = new SkiLength { Age = age, Length = length, SkiType = skiType };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(model => model.Age);
        }

        [Theory]
        [InlineData(-1, 170, SkiType.Classic)]
        public void InvalidAge_Error(int age, int length, string skiType)
        {
            var model = new SkiLength { Age = age, Length = length, SkiType = skiType };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Age);
        }

        [Theory]
        [InlineData(22, -1, SkiType.Freestyle)]
        public void InvalidLength_Error(int age, int length, string skiType)
        {
            var model = new SkiLength { Age = age, Length = length, SkiType = skiType };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Length);
        }

        [Theory]
        [InlineData(22, 170, "Snowboard")]
        public void InvalidSkiType_Error(int age, int length, string skiType)
        {
            var model = new SkiLength { Age = age, Length = length, SkiType = skiType };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.SkiType);
        }
    }
}