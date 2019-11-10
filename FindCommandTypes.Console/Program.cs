using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependenciesFinder;
using FindCommandTypes.Console.Writers;

namespace FindCommandTypes.Console
{
    class Program
    {
        private const string CommandsDLLPath =
            "C:\\projects\\ssc-main\\packages\\ExtendHealth.Core.Commands\\lib\\net40\\ExtendHealth.Core.Commands.dll";

        private static readonly string[] Repositories = {
            "extend-health/CarrierPolicy",
            "extend-health/pega-gateway-service",
            "extend-health/service-bus-commands",
            "extend-health/service-bus-consumers",
            "extend-health/ssc-main",
            "extend-health/ssc-queries",
            "extend-health/ssc-query-handlers"
        };

        private static readonly GitHubService _github;
        private static readonly CoreCommandsService _coreCommandsService;
        private static readonly IWriter _writer;

        static Program()
        {
            _github = new GitHubService();
            _coreCommandsService = new CoreCommandsService(_github);
            
            //_writer = new ConsoleWriter();
            _writer = new DebugWriter();
        }

        static async Task Main(string[] args)
        {
            try
            {
                var seansCommands = await _coreCommandsService.GetAllCommandsUsedBySSC(getSeansSSCExclusiveCommands(), Repositories);
                var dllCommands = await _coreCommandsService.GetAllCommandsUsedBySSC(CommandsDLLPath, 
                                                                                     x => x.Name.EndsWith("Command") || x.Name.EndsWith("Message"),
                                                                                     Repositories);

                await _writer.WriteComparisonAsync(Repositories, seansCommands.Where(x => dllCommands.All(y => y.Name != x.Name)),
                        dllCommands.Where(x => seansCommands.All(y => y.Name != x.Name)));
            
                await _writer.WriteAsync(Repositories, dllCommands);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
                throw;
            }
        }

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
