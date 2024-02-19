using BreeceWorks.CommunicationHub.Data;
using Microsoft.AspNetCore.Components;

namespace BreeceWorks.CommunicationHub.Pages.Components
{
    public partial class PrivacySelection
    {
        private String? privacyValue { get; set; }
        public List<String> Privacymaster
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
            get { return privacyValue; }
            set
            {
                if (privacyValue != value)
                {
                    privacyValue = value;
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
            Privacymaster = new List<String>();
            Array privacyValues = Enum.GetValues(typeof(BreeceWorks.Shared.Enums.Privacy));
            Int32 numOfOptions = privacyValues.Length;
            for (int i = 0; i < numOfOptions; i++)
            {
                Privacymaster.Add(privacyValues.GetValue(i).ToString());
            }
            ErrorMessage = null;
        }
    }
}
