using BreeceWorks.CommunicationHub.Data.Contracts;
using BreeceWorks.Shared;
using Microsoft.AspNetCore.Components;

namespace BreeceWorks.CommunicationHub.Pages.Components
{
    public partial class OperatorSelection : ComponentBase
    {
        private String? operatorValue { get; set; }
        public IEnumerable<BreeceWorks.Shared.CaseObjects.Operator> Operatormaster
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
            get { return operatorValue; }
            set
            {
                if (operatorValue != value)
                {
                    operatorValue = value;
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


        private BreeceWorks.Shared.CaseObjects.Operators? operators { get; set; }
        protected async override Task OnInitializedAsync()
        {
            ErrorMessage = null;
            operators = await CommunicationService.GetAllOperators();
            if (operators != null)
            {
                if (operators.Operators1 != null && operators.Operators1.Any())
                {
                    Operatormaster = operators.Operators1.ToList();
                }
                else
                {
                    if (operators != null && operators.Errors != null && operators.Errors.Any() && !String.IsNullOrEmpty(operators.Errors[0].Detail))
                    {
                        ErrorMessage = operators.Errors[0].Detail;
                    }
                }
            }
            else
            {
                ErrorMessage = Constants.ErrorMessages.OperatorsNotFound;
            }
        }
    }
}
