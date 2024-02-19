using BreeceWorks.CommunicationHub.Data;
using Microsoft.AspNetCore.Components;

namespace BreeceWorks.CommunicationHub.Pages.Components
{
    public partial class LanguagePreferenceSelection
    {
        private String? languagePreferenceValue { get; set; }
        public List<String> LanguagePreferencemaster
        {
            get;
            set;
        }
        public String? ErrorMessage { get; set; }

        [Inject]
        public CommunicationService CommunicationService
        {
            get;
            set;
        }
        [Parameter]
        public String? Value
        {
            get { return languagePreferenceValue; }
            set
            {
                if (languagePreferenceValue != value)
                {
                    languagePreferenceValue = value;
                    if (ValueChanged.HasDelegate)
                    {
                        ValueChanged.InvokeAsync(value);
                    }
                }
            }
        }
        [Parameter]
        public EventCallback<String?> ValueChanged
        {
            get;
            set;
        }


        protected async override Task OnInitializedAsync()
        {
            LanguagePreferencemaster = new List<String>();
            Array privacyValues = Enum.GetValues(typeof(BreeceWorks.Shared.Enums.LanguagePreference));
            Int32 numOfOptions = privacyValues.Length;
            for (int i = 0; i < numOfOptions; i++)
            {
                LanguagePreferencemaster.Add(privacyValues.GetValue(i).ToString());
            }
            ErrorMessage = null;
        }
    }
}
