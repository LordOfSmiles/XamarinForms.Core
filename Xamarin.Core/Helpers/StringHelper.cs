namespace Xamarin.Core.Helpers;

public static class StringHelper
{
    public static string GetFullNameAbbreviation(string input)
    {
        var result = string.Empty;

        if (!string.IsNullOrWhiteSpace(input))
        {
            var words = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 1)
            {
                result = GetFirstLetter(words[0]);
            }
            else if (words.Length > 1)
            {
                result = $"{GetFirstLetter(words[0])}{GetFirstLetter(words[1])}";
            }

            result = result.ToUpper();
        }

        return result;
    }
    
    #region Private Methods

    public static string GetFirstLetter(string input)
    {
        var result = string.Empty;

        for (var i = 0; i < input.Length; i++)
        {
            var c = input[i];
            if (char.IsLetter(c))
            {
                result = c.ToString();
                break;
            }
        }

        return result;
    }

    #endregion
}