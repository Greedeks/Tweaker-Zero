using System.Globalization;
using System.Text.RegularExpressions;

namespace Tweaker.Сlasses
{
    internal sealed class LanguageTranslate
    {
        internal string GetLanguageWindows()
        {
            CultureInfo _cultureInfo = CultureInfo.CurrentCulture;
            string _languageSystem = Regex.Replace(_cultureInfo.ToString(), @"-.+$", "", RegexOptions.Multiline);
            if (_languageSystem.ToString() == "ru")
                return _languageSystem.ToString();
            else return "eng";
        }
    }
}
