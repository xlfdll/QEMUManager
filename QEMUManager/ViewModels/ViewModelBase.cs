using Xlfdll;
using Xlfdll.Localization;

namespace QEMUManager
{
    public abstract class ViewModelBase : ObservableObject
    {
        public LanguageDictionary LanguageDictionary
            => App.L10n.CurrentLanguageDictionary;
    }
}