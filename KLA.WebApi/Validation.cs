namespace KLA.WebApi;
public static class Validation
{
    public static (int?, int?) ValidateCurrency(string[] input)
    {
        if (input.Length == 2)
        {
            return ValidateDollarsAndCents(input);
        }

        else if (input.Length == 1)
        {

            return (ValidateOnlyDollars(input[0]), null);
        }

        return (null, null);

    }

    private static (int?, int?) ValidateDollarsAndCents(string[] input)
    {
        if (!int.TryParse(input[0], out int dollarValue) || !int.TryParse(input[1], out int centValue))
        {
            return (null, null);
        }

        if (dollarValue < 0 || dollarValue > 999999999 || centValue < 0 || centValue > 99)
        {
            return (null, null);
        }

        return (dollarValue, centValue);
    }

    private static int? ValidateOnlyDollars(string input)
    {
        if (!int.TryParse(input, out int dollarValue))
        {
            return null;
        }

        if (dollarValue < 0 || dollarValue > 999999999)
        {
            return null;
        }

        return dollarValue;
    }
}
