namespace KLA.WebApi.Services;
public class CurrencyConverterService : ICurrencyConverterService
{
    public string ConvertDollarToWords(string input)
    {
        var (dollarValue, centValue) = Validation.ValidateInput(input);

        if (dollarValue is null && centValue is null)
        {
            throw new BadHttpRequestException("The values entered are invalid currency values and cannot be converted to words");
        }

        return FormatResult(dollarValue, centValue);
    }

    private string FormatResult(int? dollarValue, int? centValue)
    {
        string resultingDollarValueInWords;
        string resultingCentsValueInWords;
        string result = "";

        if (dollarValue != null)
        {
            resultingDollarValueInWords = ConvertNumberToWords(dollarValue.Value);

            if (dollarValue == 1)
            {
                result = $"{resultingDollarValueInWords} dollar";
            }
            else
            {
                result = $"{resultingDollarValueInWords} dollars";
            }
        }

        if (centValue != null)
        {
            resultingCentsValueInWords = ConvertNumberToWords(centValue.Value);

            if (centValue == 1)
            {
                result += $" and {resultingCentsValueInWords} cent";
            }
            else
            {
                result += $" and {resultingCentsValueInWords} cents";
            }
        }

        return result;
    }

    private string ConvertNumberToWords(int number)
    {
        if (number == 0)
        {
            return Currency.currencyValueWordsMapping[number];
        }

        string result = "";

        if ((number / 1000000) > 0)
        {
            result += $"{ConvertNumberToWords(number / 1000000)} million ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            result += $"{ConvertNumberToWords(number / 1000)} thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            result += $"{ConvertNumberToWords(number / 100)} hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (number < 20)
            {
                result += Currency.currencyValueWordsMapping[number];
            }
            else
            {
                result += Currency.currencyValueWordsMapping[number - (number % 10)];

                if ((number % 10) > 0)
                {
                    result += "-" + Currency.currencyValueWordsMapping[number % 10];
                }
            }
        }
        return result;
    }
}
