using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using DependenciesFinder;
using Octokit;

namespace FindCommandsTypes
{
    class Program
    {
        private const string _messageRegex = "[a-zA-Z]+Command";

        static void Main(string[] args)
        {
            foreach (var sscExclusiveCommand in getSSCExclusiveCommands())
            {
                Debug.WriteLine(sscExclusiveCommand);
                
                Debug.Indent();
                foreach (var consumer in GitHubSearcher.GetConsumersForCommand(sscExclusiveCommand).Result)
                {
                    Debug.WriteLine(consumer, sscExclusiveCommand);
                }
                Debug.Unindent();
            }
        }

        private static void doOtherTHing()
        {
            foreach (var coreCommandsType in CoreCommands.GetAllPublicTypesThatSSCDependsOn().Result)
            {
                Debug.WriteLine(coreCommandsType.FullName);

                Debug.Indent();
                foreach (var repository in coreCommandsType.GetDependentRepositories().Result)
                {
                    Debug.WriteLine(repository.FullName, coreCommandsType.Name);
                }

                Debug.Unindent();
            }
        }

        private static IEnumerable<string> getSSCExclusiveCommands()
        {
            yield return "AccountStatusFlagsSaveForPersonMessage";
            yield return "AccountStatusFlagsSaveForPersonResponseMessage";
            yield return "AddApplicationAnnotationCommand";
            yield return "AddPolicyAnnotationCommand";
            yield return "AddressDeleteForPersonMessage";
            yield return "AddressesVerifiedForPersonMessage";
            yield return "AddressSaveForPersonMessage";
            yield return "AddressSaveForPersonResponseMessage";
            yield return "ApplicantForManuallyEnteredApplicationMessage";
            yield return "ApplicationDemoteRequestMessage";
            yield return "ApplicationExternalLoginMessage";
            yield return "ApplicationLogAuditCommand";
            yield return "ApplicationMessage";
            yield return "ApplicationPromotedToPendingScrubbingMessage";
            yield return "ApplicationPromoteRequestMessage";
            yield return "ApplicationSaveMessage";
            yield return "ApplicationsSaveTypeRequestMessage";
            yield return "ApplicationsSaveTypeResponseMessage";
            yield return "ApplicationStaleErrorResponse";
            yield return "AppointmentReserveResponseMessage";
            yield return "BenefitGroupingMessage";
            yield return "CallDirectionMessage";
            yield return "CampaignDeleteForPersonMessage";
            yield return "CampaignMessage";
            yield return "CampaignSaveForPersonMessage";
            yield return "CampaignsVerifiedForPersonMessage";
            yield return "CarrierMessage";
            yield return "CreateCustomerRequestMessage";
            yield return "CreateCustomerResponseMessage";
            yield return "CreateCustomTaskForCustomerCommand";
            yield return "CreateManuallyEnteredApplicationCommand";
            yield return "CreateManuallyEnteredApplicationResponseMessage";
            yield return "CredentialMessage";
            yield return "CurrentCoveragesRemoveForPersonMessage";
            yield return "CurrentCoveragesSaveForPersonMessage";
            yield return "CustomerLogAuditCommand";
            yield return "CustomerPreferenceRemovedMessage";
            yield return "CustomerPreferenceRemoveMessage";
            yield return "CustomerPreferenceUpdatedMessage";
            yield return "CustomerPreferenceUpdateMessage";
            yield return "DrugInformationVerifiedForPersonMessage";
            yield return "DrugPackageConfirmedCommand";
            yield return "DrugPackageRemoveMessage";
            yield return "DrugPackageRemoveResponseMessage";
            yield return "DrugPackageReplaceWithGenericMessage";
            yield return "DrugPackageReplaceWithGenericResponseMessage";
            yield return "DrugPackageSaveResponseMessage";
            yield return "EmailAddressDispositionMessage";
            yield return "HipaaRepresentativeCreateCommand";
            yield return "HipaaRepresentativeEditCommand";
            yield return "HipaaRepresentativeRemoveCommand";
            yield return "IChangeAutoReimbursementPreferenceCommand";
            yield return "ICreateTaskCommand";
            yield return "LinkCustomerCommand";
            yield return "LogAuditCommand";
            yield return "MedicareDataVerifiedForPersonMessage";
            yield return "MedicareEligibilityInformationVerifiedForPersonMessage";
            yield return "NoteAddCommand";
            yield return "NoteMessage";
            yield return "NoteRemoveCommand";
            yield return "NoteTypeMessage";
            yield return "NoteUpdateCommand";
            yield return "NoteUpdateStickyStatusCommand";
            yield return "PersonalInformationVerifiedForPersonMessage";
            yield return "PersonSaveMedicareDataRequestMessage";
            yield return "PersonSavePersonalInformationResponseMessage";
            yield return "PersonSaveRequestMessage";
            yield return "PhoneMessage";
            yield return "PhoneSaveForPersonRequestMessage";
            yield return "PhoneSaveForPersonResponseMessage";
            yield return "PhonesVerifiedForPersonMessage";
            yield return "PlanDrugMessage";
            yield return "PlanTypeGroupMessage";
            yield return "PowerOfAttorneyVerifiedForPersonMessage";
            yield return "PrioritizedMessage";
            yield return "QuoteConfigurationMessage";
            yield return "QuoteMessage";
            yield return "QuoteSaveMedigapQualifyingQuestionsMessage";
            yield return "QuoteSaveMedigapQualifyingQuestionsResponse";
            yield return "RelationshipDeleteForPersonMessage";
            yield return "RelationshipSaveForPersonRequestMessage";
            yield return "RelationshipSaveForPersonResponseMessage";
            yield return "SaveOnlineApplicationsRequestMessage";
            yield return "SaveOnlineApplicationsResponseMessage";
            yield return "ScheduleCallbackWithCustomerCommand";
            yield return "SearchLogAuditCommand";
            yield return "SituationAnalysisSaveForPersonMessage";
            yield return "SituationAnalysisSaveForPersonResponseMessage";
            yield return "SSTFlagsSaveForPersonResponseMessage";
            yield return "StatusInstanceMessage";
            yield return "SubmittedApplicationRequestMessage";
            yield return "SubmittedApplicationResponseMessage";
            yield return "TaskDelayDueDateMessage";
            yield return "TaskSaveRequestMessage";
            yield return "TaskSaveResponseMessage";
            yield return "TasksMarkAsCompletedRequestMessage";
            yield return "TasksMarkAsCompletedResponseMessage";
            yield return "TasksReAssignRequestMessage";
            yield return "TasksResolvedMessage";
            yield return "TaskUnMarkAsCompletedMessage";
            yield return "UpsertVoluntaryBenefitsCommand";
            yield return "VerifiedForPersonMessage";
            yield return "VoluntaryBenefitsEnrollmentResponseMessage";
        }

        private static IEnumerable<string> getAlreadyImportedTypes()
        {
            yield return "AarpStateSpecificAttachmentsMessage";
            yield return "AccountStatusFlagsSaveForPersonMessage";
            yield return "AccountStatusFlagsSaveForPersonResponseMessage";
            yield return "AddressChangeMessage";
            yield return "AddressDeleteForPersonMessage";
            yield return "AddressesVerifiedForPersonMessage";
            yield return "AddressMessage";
            yield return "AddressSaveForPersonMessage";
            yield return "AddressSaveForPersonResponseMessage";
            yield return "ApplicantForManuallyEnteredApplicationMessage";
            yield return "ApplicationDemoteRequestMessage";
            yield return "ApplicationDiscardRequestMessage";
            yield return "ApplicationDisclaimerMessage";
            yield return "ApplicationExternalLoginMessage";
            yield return "ApplicationPromoteRequestMessage";
            yield return "ApplicationSaveMessage";
            yield return "ApplicationSaveStatusResponseMessage";
            yield return "ApplicationsSaveTypeRequestMessage";
            yield return "ApplicationsSaveTypeResponseMessage";
            yield return "AuthoriedRepSendDocsInstructionMessage";
            yield return "BenefitGroupingMessage";
            yield return "CampaignDeleteForPersonMessage";
            yield return "CampaignMessage";
            yield return "CampaignSaveForPersonMessage";
            yield return "CampaignsVerifiedForPersonMessage";
            yield return "CarrierMessage";
            yield return "CreateCustomerRequestMessage";
            yield return "CreateCustomerResponseMessage";
            yield return "CreateManuallyEnteredApplicationResponseMessage";
            yield return "CredentialMessage";
            yield return "CurrentCoveragesRemoveForPersonMessage";
            yield return "CurrentCoveragesSaveForPersonMessage";
            yield return "CustomerPreferenceRemovedMessage";
            yield return "CustomerPreferenceRemoveMessage";
            yield return "CustomerPreferenceUpdatedMessage";
            yield return "CustomerPreferenceUpdateMessage";
            yield return "DrugInformationVerifiedForPersonMessage";
            yield return "DrugPackageMessage";
            yield return "DrugPackageReplaceWithGenericMessage";
            yield return "DrugPackageReplaceWithGenericResponseMessage";
            yield return "DrugPackageSaveResponseMessage";
            yield return "EmailAddressDispositionMessage";
            yield return "EmployeeMessage";
            yield return "EmployeePresenceChangedToAvailableMessage";
            yield return "EmployeeStillSignedInConfirmationMessage";
            yield return "ForgotPasswordMessage";
            yield return "InitiateResetPasswordMessage";
            yield return "LoginSuccessAcknowledgedMessage";
            yield return "LogMessage";
            yield return "MedicareDataVerifiedForPersonMessage";
            yield return "MedicareEligibilityInformationVerifiedForPersonMessage";
            yield return "MedicationPackageSaveMessage";
            yield return "NeedsAnalysisMessage";
            yield return "NoteMessage";
            yield return "OtherCoverageMessage";
            yield return "PersonalInformationVerifiedForPersonMessage";
            yield return "PersonMessage";
            yield return "PersonSaveMedicareDataRequestMessage";
            yield return "PersonSavePersonalInformationMessage";
            yield return "PersonSavePersonalInformationResponseMessage";
            yield return "PersonSaveRequestMessage";
            yield return "PersonSaveShopperResponseMessage";
            yield return "PhoneMessage";
            yield return "PhoneSaveForPersonRequestMessage";
            yield return "PhoneSaveForPersonResponseMessage";
            yield return "PhonesVerifiedForPersonMessage";
            yield return "PingMessage";
            yield return "PlanDrugMessage";
            yield return "PlanMessage";
            yield return "PlanTypeMessage";
            yield return "PlanTypeYearKeyMessage";
            yield return "PongMessage";
            yield return "PowerOfAttorneyDeleteForPersonMessage";
            yield return "PowerOfAttorneySaveForPersonMessage";
            yield return "PowerOfAttorneyVerifiedForPersonMessage";
            yield return "PrioritizedMessage";
            yield return "QuoteConfigurationMessage";
            yield return "QuoteMessage";
            yield return "QuoteSaveMedigapQualifyingQuestionsMessage";
            yield return "RelationshipDeleteForPersonMessage";
            yield return "RelationshipSaveForPersonRequestMessage";
            yield return "RelationshipSaveForPersonResponseMessage";
            yield return "RelationshipTypeMessage";
            yield return "ResetPasswordInitiatedMessage";
            yield return "SaveOnlineApplicationsRequestMessage";
            yield return "SaveOnlineApplicationsResponseMessage";
            yield return "SaveOtherCoverageMessage";
            yield return "SaveOtherCoverageResponseMessage";
            yield return "SecureCallForCustomersResponseMessage";
            yield return "SendAvailabilityMessage";
            yield return "SituationAnalysisSaveForPersonMessage";
            yield return "SituationAnalysisSaveForPersonResponseMessage";
            yield return "SSTFlagsSaveForPersonResponseMessage";
            yield return "StatusInstanceMessage";
            yield return "SubmittedApplicationRequestMessage";
            yield return "SubmittedApplicationResponseMessage";
            yield return "TaskDelayDueDateMessage";
            yield return "TaskSaveRequestMessage";
            yield return "TaskSaveResponseMessage";
            yield return "TasksMarkAsCompletedRequestMessage";
            yield return "TasksMarkAsCompletedResponseMessage";
            yield return "TasksReAssignRequestMessage";
            yield return "TasksResolvedMessage";
            yield return "TaskUnMarkAsCompletedMessage";
            yield return "VoluntaryBenefitsEnrollmentResponseMessage";
        }
    }
}
