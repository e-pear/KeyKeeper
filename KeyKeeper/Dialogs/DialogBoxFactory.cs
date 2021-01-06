using KeyKeeper.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KeyKeeper.Dialogs
{
    public class DialogBoxFactory
    {
        public string HeaderMessage { get; set; }
        public IEnumerable<string> RequestedParameters { get; set; }
        public IEnumerable<string> DefaultValuesForRequestedParameters { get; set; }
        public IEnumerable<ValidationRules> CorrespondingRules { get; set; }

        public static InfoBox GetInfoBox(string info)
        {
            return new InfoBox(info);
        }

        public RequestDialogBox GetRequestDialogBox()
        {
            return new RequestDialogBox(HeaderMessage, RequestedParameters, DefaultValuesForRequestedParameters, GetRules(CorrespondingRules));
        }


        private IEnumerable<ValidationRule> GetRules(IEnumerable<ValidationRules> aliases)
        {

            List<ValidationRule> list = new List<ValidationRule>();

            foreach (ValidationRules alias in aliases)
            {
                if (alias == ValidationRules.StringTyped4DigitCode) list.Add(new FourDigitIdValidationRule());
                else list.Add(null);
            }

            return list;
        }
    }

    public enum ValidationRules {StringTyped4DigitCode, StringTypedIpAdress};
}
