using BreeceWorks.CommunicationHub.Data;
using BreeceWorks.Shared.CaseObjects;
using BreeceWorks.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace BreeceWorks.CommunicationHub.Pages.Case
{
    public partial class NewCase
    {
        private CaseCreateRqst? caseCreateRqst { get; set; }

        private String? ErrorMessage { get; set; }

        [Inject]
        private CommunicationService? CommunicationService
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

            if (caseCreateRqst != null && CommunicationService != null)
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

        private string? ValidateRequest()
        {
            String exceptionMessage = null;
            if (caseCreateRqst != null)
            {
                if (!String.IsNullOrEmpty(caseCreateRqst.CaseType))
                {
                    if (caseCreateRqst.CaseData != null)
                    {
                        if (String.IsNullOrEmpty(caseCreateRqst.CaseData.ClaimNumber))
                        {
                            exceptionMessage = "The claimNumber field is required.";
                        }
                        if (String.IsNullOrEmpty(caseCreateRqst.CaseData.PolicyNumber))
                        {
                            exceptionMessage = "The policyNumber field is required.";
                        }
                    }
                    else
                    {
                        exceptionMessage = "Case Data is required";
                    }
                    if (caseCreateRqst.Customer != null)
                    {
                        if (String.IsNullOrEmpty(caseCreateRqst.Customer.First))
                        {
                            exceptionMessage = "Customer First Name field is required.";
                        }
                        if (String.IsNullOrEmpty(caseCreateRqst.Customer.Last))
                        {
                            exceptionMessage = "Cuatomer Last Name field is required.";
                        }
                        if (String.IsNullOrEmpty(caseCreateRqst.Customer.Email))
                        {
                            exceptionMessage = "Customer Email field is required.";
                        }
                        if (String.IsNullOrEmpty(caseCreateRqst.Customer.Mobile))
                        {
                            exceptionMessage = "Customer Mobile field is required.";
                        }
                    }
                    else
                    {
                        exceptionMessage = "Customer is required";
                    }
                }
                else
                {
                    exceptionMessage = "Case Type is required";
                }
            }
            else
            {
                exceptionMessage = "Case Create Request is null";
            }
            return exceptionMessage;
        }

        protected void Cancel()
        {
            NavManager.NavigateTo("/casemanagement");
        }
    }
}
