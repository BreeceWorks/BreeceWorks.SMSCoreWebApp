using BreeceWorks.CommunicationHub.Data.Contracts;
using BreeceWorks.Shared.CaseObjects;
using BreeceWorks.Shared.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace BreeceWorks.CommunicationHub.Pages.Case
{
    public partial class NewCase
    {
        private CaseCreateRqst? caseCreateRqst { get; set; }

        private String? ErrorMessage { get; set; }

        private Boolean generalFormValid { get; set; }
        private Boolean caseDataFormValid {  get; set; }
        private Boolean customerFormValid { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }


        [Inject]
        private ICommunicationService? CommunicationService
        {
            get;
            set;
        }

        [Inject]
        private NavigationManager? NavManager { get; set; }

        public String? AssignOperatorID { get; set; }
        public String? PrivacySelection { get; set; }

        public String? LanguagePreferenceSelection { get; set; }


        protected override async Task OnInitializedAsync()
        {
            caseCreateRqst = new CaseCreateRqst()
            {
                CaseData = new CaseDataCreateUpdateRqst(),
                Customer = new CustomerCreateUpdateRqst(),
                CreatedBy = new OperatorBaseRqst(),
            };
        }


        protected void AddLineOfBusiness()
        {
            if (caseCreateRqst != null && caseCreateRqst.CaseData != null)
            {
                caseCreateRqst.CaseData.LineOfBusiness = new LineOfBusinessRqst();
                StateHasChanged();
            }
        }

        protected void RemoveLineOfBusiness()
        {
            if (caseCreateRqst != null && caseCreateRqst.CaseData != null)
            {
                caseCreateRqst.CaseData.LineOfBusiness = null;
                StateHasChanged();
            }
        }

        protected void AddPrimaryContact()
        {
            if (caseCreateRqst != null)
            {
                caseCreateRqst.PrimaryContact = new PrimaryContactCaseCreateUpdateRqst()
                {
                    Email = AssignOperatorID
                };
                StateHasChanged();
            }
        }

        protected void RemovePrimaryContact()
        {
            if (caseCreateRqst != null)
            {
                caseCreateRqst.PrimaryContact = null;
                StateHasChanged();
            }
        }

        protected async void OpenNewCase()
        {
            await JSRuntime.InvokeVoidAsync("validateCustomer", null);

        }



        private async void HandleGeneralValidation(EditContext editContext)
        {
            generalFormValid = editContext.Validate();

            if (caseCreateRqst != null && CommunicationService != null && customerFormValid && caseDataFormValid && generalFormValid)
            {
                ErrorMessage = ValidateRequest();
                if (String.IsNullOrEmpty(ErrorMessage))
                {
                    if (!String.IsNullOrEmpty(LanguagePreferenceSelection))
                    {
                        caseCreateRqst.LanguagePreference = (LanguagePreference)Enum.Parse(typeof(BreeceWorks.Shared.Enums.LanguagePreference), LanguagePreferenceSelection);
                    }
                    if (!String.IsNullOrEmpty(PrivacySelection))
                    {
                        caseCreateRqst.Privacy = (Privacy)Enum.Parse(typeof(BreeceWorks.Shared.Enums.Privacy), PrivacySelection);
                    }
                    if (caseCreateRqst.PrimaryContact != null)
                    {
                        caseCreateRqst.PrimaryContact.Email = AssignOperatorID;
                    }
                    BreeceWorks.Shared.CaseObjects.Case? createdCase = await CommunicationService.OpenNewCase(caseCreateRqst);
                    if (createdCase != null)
                    {
                        if (createdCase.Errors == null || createdCase.Errors.Count == 0)
                        {
                            NavManager.NavigateTo("/casemanagement");
                        }
                        if (createdCase.Errors != null && createdCase.Errors.Count > 0)
                        {
                            ErrorMessage = createdCase.Errors[0].Detail;
                        }
                    }
                }
                StateHasChanged();
            }

        }
        private async void HandleCaseDataValidation(EditContext editContext)
        {
            caseDataFormValid = editContext.Validate(); 
            await JSRuntime.InvokeVoidAsync("validateGeneral", null);
        }
        private async void HandleCustomerdValidation(EditContext editContext)
        {
            customerFormValid = editContext.Validate();
            await JSRuntime.InvokeVoidAsync("validateCaseData", null);
        }
        protected void Cancel()
        {
            NavManager.NavigateTo("/casemanagement");
        }

        private string? ValidateRequest()
        {
            String exceptionMessage = null;
            if (caseCreateRqst == null)
            {
                exceptionMessage = "Case Create Request is null";
            }
            return exceptionMessage;
        }

    }
}
