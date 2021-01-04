using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Text;

namespace KeyKeeper.Rules
{
    /// <summary>
    /// Simple validarion rule object which is using rexex matxhing to validate input. Due it's character I have to break my convention here and write error message in polish.
    /// </summary>
    public class FourDigitIdValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex regex = new Regex(@"^[0-9]{4}$");
            Match match = regex.Match(value.ToString());

            if (match == null || match == Match.Empty || value == null || value.ToString() == "") return new ValidationResult(false, "Niewłaściwy format wejścia. Proszę wprowadzić właściwy czterocyfrowy kod."); // it means: "Invalid input format. Please enter a valid four digit code."
            else return ValidationResult.ValidResult;
        }
    }
}
