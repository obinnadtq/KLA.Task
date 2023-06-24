namespace KLA.WebApi.Services;
public class CurrencyConverterService : ICurrencyConverterService
{
    public string ConvertDollarToWords(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new BadHttpRequestException("No valid input entered");
        }

        var splittedString = input.Split(",");

        var (dollarValue, centValue) = Validation.ValidateCurrency(splittedString);

        if (dollarValue is null && centValue is null)
        {
            throw new BadHttpRequestException("The values entered are invalid currency values and cannot be converted to words");
        }

        var resultingDollarValueInWords = ConvertDollarValue(dollarValue!.Value);

        if (dollarValue == 1)
        {
            return $"{resultingDollarValueInWords} dollar";
        }

        return $"{resultingDollarValueInWords} dollars";
    }

    private string ConvertDollarValue(int dollarValue)
    {
        if (dollarValue == 0)
        {
            return Currency.currencyValueWordsMapping[dollarValue];
        }

        string result = "";

        if ((dollarValue / 1000000) > 0)
        {
            result += $"{ConvertDollarValue(dollarValue / 1000000)} million ";
            dollarValue %= 1000000;
        }

        if ((dollarValue / 1000) > 0)
        {
            result += $"{ConvertDollarValue(dollarValue / 1000)} thousand ";
            dollarValue %= 1000;
        }

        if ((dollarValue / 100) > 0)
        {
            result += $"{ConvertDollarValue(dollarValue / 100)} hundred ";
            dollarValue %= 100;
        }

        if ((dollarValue / 10) > 0)
        {
            var wholeNumber = (dollarValue / 10) * 10;
            result += Currency.currencyValueWordsMapping[wholeNumber];
            dollarValue %= 10;
        }

        if (dollarValue > 0)
        {
            if (result != "")
            {
                result += "-";
            }
            if ((dollarValue % 10) > 0)
            {
                result += Currency.currencyValueWordsMapping[dollarValue];
            }
        }
        return result;
    }
}
