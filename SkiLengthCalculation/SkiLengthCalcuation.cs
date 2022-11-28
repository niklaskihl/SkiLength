namespace SkiLengthUtilities;

public static class SkiLengthCalcuation
{
    private const int MaxLengthFreestyle = 192;
    private const int MaxLengthClassic = 207;

    public static List<int> CalculateSkiLength(int age, int length, string skiType)
    {
        List<int> suggestedLengths = new List<int> { length };

        switch (skiType)
        {
            case SkiType.Classic:
                if (age >= 5 && age <= 8)
                {
                    suggestedLengths = new List<int> {
                        Math.Min(length + 10, MaxLengthClassic),
                        Math.Min(length + 20, MaxLengthClassic)
                    };
                }
                else if (age >= 9)
                {
                    suggestedLengths = new List<int> {
                        Math.Min(length + 20, MaxLengthClassic)
                    };
                }
                break;

            case SkiType.Freestyle:
                if (age >= 5)
                {
                    var minLength = Math.Min(length + 10, MaxLengthFreestyle);
                    var maxLength = Math.Min(length + 15, MaxLengthFreestyle);

                    suggestedLengths = minLength == maxLength ?
                        new List<int> { minLength } :
                        new List<int> { minLength, maxLength };

                }
                break;
        }

        return suggestedLengths;
    }
}