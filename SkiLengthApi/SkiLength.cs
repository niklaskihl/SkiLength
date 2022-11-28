using FluentValidation;
using SkiLengthUtilities;

namespace SkiLengthApi
{
    public class SkiLength
    {
        public int Age { get; set; }

        public int Length { get; set; }

        public string SkiType { get; set; }
    }

    public class SkiLengthValidator : AbstractValidator<SkiLength>
    {
        public SkiLengthValidator()
        {
            RuleFor(model => model.Age).GreaterThanOrEqualTo(0).WithMessage("Please enter a valid age");

            RuleFor(model => model.Length).GreaterThan(0).WithMessage("Please enter a valid height");

            RuleFor(model => model.SkiType).Must(v => new[] { SkiType.Classic, SkiType.Freestyle }.Contains(v)).WithMessage("Please select a valid ski-type");
        }
    }
}