using BreeceWorks.CommunicationHub.Data.Contracts;
using Microsoft.AspNetCore.Components;


namespace BreeceWorks.CommunicationHub.Pages.Components
{
    public partial class CaseTypeSelection
    {
        private String? caseTypeValue { get; set; }
        public List<String> CaseTypeMaster
        {
            get;
            set;
        }
        public String? ErrorMessage { get; set; }

        [Inject]
        public ICommunicationService CommunicationService
        {
            get;
            set;
        }
        [Parameter]
        public String? Value
        {
            get { return caseTypeValue; }
            set
            {
                if (caseTypeValue != value)
                {
                    caseTypeValue = value;
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
            CaseTypeMaster = new List<String>();
            Array caseTypeValues = Enum.GetValues(typeof(BreeceWorks.Shared.Enums.CaseType));
            Int32 numOfOptions = caseTypeValues.Length;
            for (int i = 0; i < numOfOptions; i++)
            {
                CaseTypeMaster.Add(caseTypeValues.GetValue(i).ToString());
            }
            ErrorMessage = null;
        }

    }
}
