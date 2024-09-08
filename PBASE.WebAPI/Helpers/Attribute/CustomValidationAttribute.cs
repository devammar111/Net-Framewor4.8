using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace PBASE.WebAPI.Helpers
{
    public class CustomValidationAttribute
    {
        public sealed class checkString : ValidationAttribute
        {
            private const string listOfInvalidWords = "<script>,jquery,javascript,style,<iframe>";
            private const string pattern = @"^((?!(<s|<S)(c|C)(r|R)(i|I)(p|P)(t|T)|(s|S)(t|T)(y|Y)(l|L)(e|E)|(j|J)(a|A)(v|V)(a|A)(s|S)(c|C)(r|R)(i|I)(p|P)(t|T)|(j|J)(q|Q)(u|U)(e|E)(r|R)(y|Y)|(<i|<I)(f|F)(r|R)(a|A)(m|M)(e|E))[\w\W])*$";
            protected override ValidationResult IsValid(object text, ValidationContext validationContext)
            {
                if(text != null)
                {
                    Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
                    string validationMessage = validationContext.DisplayName + " - Invalid data has been entered._<script>, jquery, javascript, style, <iframe>_ ";
                    string[] myarr = listOfInvalidWords.ToString().Split(',');
                    Match value = r.Match(text.ToString());
                    IList<string> invalidWordsOfText = new List<string>();
                    if (!value.Success)
                    {
                        int j = 0;
                        for (var i = 0; i < myarr.Length; i++)
                        {
                            if (text.ToString().ToLower().Contains(myarr[i]))
                            {
                                if (j != 0)
                                {
                                    validationMessage += ", ";
                                }
                                validationMessage += myarr[i];
                                j++;
                            }
                        }
                        return new ValidationResult(validationMessage);
                    }
                }
                return ValidationResult.Success;
            }
        }

        public sealed class checkValidString : ValidationAttribute
        {
            private const string listOfInvalidWords = "<script>,jquery,javascript,<iframe>";
            private const string pattern = @"^((?!(<s|<S)(c|C)(r|R)(i|I)(p|P)(t|T)|(j|J)(a|A)(v|V)(a|A)(s|S)(c|C)(r|R)(i|I)(p|P)(t|T)|(j|J)(q|Q)(u|U)(e|E)(r|R)(y|Y)|(<i|<I)(f|F)(r|R)(a|A)(m|M)(e|E))[\w\W])*$";
            private const string angleCharlist = "<,>";
            protected override ValidationResult IsValid(object text, ValidationContext validationContext)
            {
                if (text != null)
                {
                    Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
                    string validationMessage = validationContext.DisplayName + " - Invalid data has been entered._<script>, jquery, javascript, <iframe>_ ";
                    string[] myarr = listOfInvalidWords.ToString().Split(',');
                    Match value = r.Match(text.ToString());
                    IList<string> invalidWordsOfText = new List<string>();
                    if (!value.Success)
                    {
                        int j = 0;
                        for (var i = 0; i < myarr.Length; i++)
                        {
                            if (text.ToString().ToLower().Contains(myarr[i]))
                            {
                                if (j != 0)
                                {
                                    validationMessage += ", ";
                                }
                                validationMessage += myarr[i];
                                j++;
                            }
                        }
                        return new ValidationResult(validationMessage);
                    }
                    var tagWithoutClosingRegex = new Regex(@"<[^>]+>");
                    var hasTags = tagWithoutClosingRegex.IsMatch(value.ToString());

                    if (hasTags)
                        return new ValidationResult(validationMessage);
                }
                return ValidationResult.Success;
            }
        }
        public sealed class checkReadOnly : ValidationAttribute
        {
            public int _menuId;
            public checkReadOnly(int menuId)
            {
                _menuId = menuId;
            }
            protected override ValidationResult IsValid(object text, ValidationContext validationContext)
            {
                return ValidationResult.Success;
            }
        }

    }
}