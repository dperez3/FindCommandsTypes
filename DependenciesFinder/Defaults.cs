using System.Collections.Generic;
using System.Linq;

namespace DependenciesFinder
{
    public static class Defaults
    {
        public const string CommandsDLLPath =
            "C:\\projects\\ssc-main\\packages\\ExtendHealth.Core.Commands\\lib\\net40\\ExtendHealth.Core.Commands.dll";
        public const string CommandsAssemblyName = "ExtendHealth.Core.Commands";

        public const string SSCRepoName = "extend-health/ssc-main";
        public const string CCRepoName = "extend-health/service-bus-consumers";
        public const string CCommandsRepoName = "extend-health/service-bus-commands";


        public static readonly string[] Repositories =
        {
            "extend-health/one-exchange",
            "extend-health/CarrierPolicy",
            "extend-health/pega-gateway-service",
            CCommandsRepoName,
            CCRepoName,
            SSCRepoName,
            "extend-health/ssc-queries",
            "extend-health/ssc-query-handlers"
        };
        //public static readonly string[] Repositories = null;

        public static readonly IEnumerable<string> SeansCommands = getSeansSSCExclusiveCommands();


        private static IEnumerable<string> getSeansSSCExclusiveCommands()
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
    }
}
