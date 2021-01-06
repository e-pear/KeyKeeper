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
    /// <summary>
    /// Simple factory for dialogboxes. Object creates instances simple information dialogs in static manner. Must be instantiated and properly set in order to create more sophisticated RequestDialogBox object.
    /// </summary>
    public class DialogBoxFactory
    {
        /// <summary>
        /// A message which will be displayed in header part of RequestDialogBox window object. It's optional.
        /// </summary>
        public string HeaderMessage { get; set; }
        /// <summary>
        /// A collection of parameters names which will be displayed as labels along with assigned text box object in dialog in collection's order. Setting property is mandatory. 
        /// </summary>
        public IEnumerable<string> RequestedParameters { get; set; }
        /// <summary>
        /// A collection of default parameters which will fill displayed text box objects in dialog in collection's order. Setting property isn't mandatory but at least empty collection is required.
        /// </summary>
        public IEnumerable<string> DefaultValuesForRequestedParameters { get; set; }
        /// <summary>
        /// A collection of rules which will be assigned to text box objects in dialog in collection's order. Setting property isn't mandatory but at least empty collection is required.
        /// </summary>
        public IEnumerable<ValidationRules> CorrespondingRules { get; set; }
        /// <summary>
        /// Static factory method providing an instance of information dialog box object.
        /// </summary>
        /// <param name="info">Message displayed in info dialog.</param>
        /// <returns></returns>
        public static InfoBox GetInfoBox(string info)
        {
            return new InfoBox(info);
        }
        /// <summary>
        /// Factory method providing an instance of RequestDialogBox object. Required Factory's properties must be set before colling.
        /// </summary>
        /// <returns></returns>
        public RequestDialogBox GetRequestDialogBox()
        {
            return new RequestDialogBox(HeaderMessage, RequestedParameters, DefaultValuesForRequestedParameters, GetRules(CorrespondingRules));
        }
        // Internal method which translates collection of rule describing enums to proper validation rules.
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
    // Validation rules aliases. Only one is used now.
    public enum ValidationRules {StringTyped4DigitCode, StringTypedIpAdress};
}
