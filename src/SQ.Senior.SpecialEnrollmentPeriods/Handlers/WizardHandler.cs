using SQ.DecisionTreeWorkflow.Services.Handlers;
using SQ.DecisionTreeWorkflow.Services.Models;
using SQ.DecisionTreeWorkflow.Services.Models.Actions;
using SQ.DecisionTreeWorkflow.Services.Models.Responses;
using SQ.Senior.Quoting.Internal.DatabaseAdapter.Models;
using SQ.Senior.Quoting.Internal.DatabaseAdapter.Repositories;
using SQ.Senior.SpecialEnrollmentPeriods.Exceptions;
using SQ.Senior.SpecialEnrollmentPeriods.Extensions;
using SQ.Senior.SpecialEnrollmentPeriods.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQ.Senior.SpecialEnrollmentPeriods.Handlers {
    public class WizardHandler : IWizardHandler {
        private readonly ISeniorInternalQuotingDbRepository _internalQuotingDbRepository;
        private readonly IWorkflowHandler _workflowHandler;
        private readonly IQuestionHandler _questionHandler;

        public WizardHandler(ISeniorInternalQuotingDbRepository internalQuotingDbRepository, 
            IWorkflowHandler workflowHandler, IQuestionHandler questionHandler) {
            _internalQuotingDbRepository = internalQuotingDbRepository ?? throw new ArgumentNullException(nameof(internalQuotingDbRepository));
            _workflowHandler = workflowHandler ?? throw new ArgumentNullException(nameof(workflowHandler));
            _questionHandler = questionHandler ?? throw new ArgumentNullException(nameof(questionHandler));
        }

        public async Task<IEnumerable<WorkflowDisplay>> GetWorkflowDisplaysAsync() {
            var workflows = await GetWorkflowsInOrderAsync(); 
            return workflows.Select(workflow => new WorkflowDisplay {
                WorkflowId = workflow.Id,
                Name = workflow.Name,
                Order = workflow.SortOrder
            });
        }

        public async Task<WizardResponse> GetStepAsync(int stepId, int applicantId, int accountId) {
            var applicant = await GetApplicantAsync(accountId, applicantId);
            var workflowStep = await _workflowHandler.GetWorkflowStepByIdAsync(stepId);
            var questionViews = await GetQuestionViewsAsync(workflowStep.Questions, applicant);

            return new WizardResponse {
                CurrentStep = new StepView {
                    WorkflowId = workflowStep.WorkflowId,
                    StepId = workflowStep.Id,
                    DisplayText = workflowStep.DisplayText,
                    Questions = questionViews
                }
            };
        }

        public async Task<WizardResponse> StartWizardAsync(StartWizardRequest startWizardRequest) {
            var workflows = await GetWorkflowsInOrderAsync();
            var firstWorkflow = workflows.First();
            var firstStep = firstWorkflow.Steps.First(step => step.Id == firstWorkflow.FirstStepId);
            var applicant = await GetApplicantAsync(startWizardRequest.AccountId, startWizardRequest.ApplicantId);
            var questionViews = await GetQuestionViewsAsync(firstStep.Questions, applicant);

            var firstStepResponse = new WizardResponse {
                CurrentStep = new StepView {
                    WorkflowId = firstWorkflow.Id,
                    StepId = firstStep.Id,
                    DisplayText = firstStep.DisplayText,
                    Questions = questionViews
                }
            };

            if (startWizardRequest.AutoEvaluate && firstStepResponse.CurrentStep.AllQuestionsHaveMappedValues()) {
                var processWizardRequest = GenerateProcessWizardRequest(startWizardRequest, firstStepResponse.CurrentStep);
                return await ProcessWizardRequestAsync(processWizardRequest);
            }

            return firstStepResponse;
        }

        public async Task<WizardResponse> ProcessWizardRequestAsync(ProcessWizardRequest processWizardRequest) {
            var workflowStepResponse = GetWorkflowStepResponse(processWizardRequest);
            var evaluationStep = await _workflowHandler.ProcessWorkflowStepResponseAsync(workflowStepResponse);
            var applicant = await GetApplicantAsync(processWizardRequest.AccountId, processWizardRequest.ApplicantId);
            var wizardResponse = await GenerateWizardResponseAsync(evaluationStep, applicant);

            if (processWizardRequest.AutoEvaluate && wizardResponse.CurrentStep.AllQuestionsHaveMappedValues()) {
                var nextProcessWizardRequest = GenerateProcessWizardRequest(processWizardRequest, wizardResponse.CurrentStep);
                return await ProcessWizardRequestAsync(nextProcessWizardRequest);
            }

            return wizardResponse;
        }

        private WorkflowStepResponse GetWorkflowStepResponse(ProcessWizardRequest processWizardRequest) {
            var questionResponses = processWizardRequest.QuestionResponses
                .Select(r => new DecisionTreeWorkflow.Services.Models.Responses.QuestionResponse {
                    QuestionId = r.QuestionId,
                    ValueType = r.ValueType,
                    ResponseValue = r.ResponseValue
                }).ToList();

            return new WorkflowStepResponse {
                AccountId = processWizardRequest.AccountId,
                ApplicantId = processWizardRequest.ApplicantId,
                UserKey = processWizardRequest.UserKey,
                WorkflowId = processWizardRequest.WorkflowId,
                StepId = processWizardRequest.StepId,
                QuestionResponses = questionResponses
            };
        }

        private ProcessWizardRequest GenerateProcessWizardRequest(StartWizardRequest startWizardRequest, StepView stepView) {
            var questionResponses = stepView.Questions.Select(question => new ViewModels.QuestionResponse {
                QuestionId = question.QuestionId,
                ValueType = (int)question.ValueType,
                ResponseValue = question.MappedValue
            });

            return new ProcessWizardRequest {
                AccountId = startWizardRequest.AccountId,
                ApplicantId = startWizardRequest.ApplicantId,
                UserKey = startWizardRequest.UserKey,
                AutoEvaluate = startWizardRequest.AutoEvaluate,
                WorkflowId = stepView.WorkflowId,
                StepId = stepView.StepId,
                QuestionResponses = questionResponses
            };
        }

        private ProcessWizardRequest GenerateProcessWizardRequest(ProcessWizardRequest processWizardRequest, StepView stepView) {
            var questionResponses = stepView.Questions.Select(question => new ViewModels.QuestionResponse {
                QuestionId = question.QuestionId,
                ValueType = (int)question.ValueType,
                ResponseValue = question.MappedValue
            });

            return new ProcessWizardRequest {
                AccountId = processWizardRequest.AccountId,
                ApplicantId = processWizardRequest.ApplicantId,
                UserKey = processWizardRequest.UserKey,
                AutoEvaluate = processWizardRequest.AutoEvaluate,
                WorkflowId = stepView.WorkflowId,
                StepId = stepView.StepId,
                QuestionResponses = questionResponses
            };
        }

        private async Task<WizardResponse> GenerateWizardResponseAsync(EvaluationStep evaluationStep, AgentVersionApplicant applicant) {
            var wizardResponse = new WizardResponse();

            if (evaluationStep.IsPassingWorkflow.HasValue) {
                var isPassingWorkflow = evaluationStep.IsPassingWorkflow.Value;

                if (isPassingWorkflow) {
                    wizardResponse.WorkflowResponse = new WorkflowResponse {
                        PassedWorkflow = evaluationStep.IsPassingWorkflow.Value,
                        DisplayText = evaluationStep.DisplayText
                    };
                } else {
                    var workflows = await GetWorkflowsInOrderAsync();
                    var currentWorkflow = workflows.First(w => w.Id == evaluationStep.WorkflowId);
                    var lastWorkflow = workflows.Last();

                    if (currentWorkflow.Id == lastWorkflow.Id) {
                        wizardResponse.WorkflowResponse = new WorkflowResponse {
                            PassedWorkflow = evaluationStep.IsPassingWorkflow.Value,
                            DisplayText = evaluationStep.DisplayText
                        };
                    } else {
                        var nextWorkflow = workflows.First(w => w.SortOrder == currentWorkflow.SortOrder + 1);
                        var firstStep = nextWorkflow.Steps.First(step => step.Id == nextWorkflow.FirstStepId);
                        var questionViews = await GetQuestionViewsAsync(firstStep.Questions, applicant);

                        wizardResponse.CurrentStep = new StepView {
                            WorkflowId = nextWorkflow.Id,
                            StepId = firstStep.Id,
                            DisplayText = firstStep.DisplayText,
                            Questions = questionViews
                        };
                    }
                }
            }

            var popUpMessageAction = evaluationStep.Actions.OfType<PopUpMessageAction>().FirstOrDefault();
            if (popUpMessageAction != null) {
                wizardResponse.PopUpMessage = new PopUpMessage {
                    Title = popUpMessageAction.Title,
                    Message = popUpMessageAction.Message
                };
            }

            if (evaluationStep.NextWorkflowStepId.HasValue) {
                var workflowStep = await _workflowHandler.GetWorkflowStepByIdAsync(evaluationStep.NextWorkflowStepId.Value);
                var questionViews = await GetQuestionViewsAsync(workflowStep.Questions, applicant);

                wizardResponse.CurrentStep = new StepView {
                    WorkflowId = workflowStep.WorkflowId,
                    StepId = workflowStep.Id,
                    DisplayText = workflowStep.DisplayText,
                    Questions = questionViews
                };
            }

            return wizardResponse;
        }

        private async Task<IEnumerable<Workflow>> GetWorkflowsInOrderAsync() {
            var workflows = await _workflowHandler.GetWorkflowsByWizardNameAsync("SEP Wizard");
            return workflows?.OrderBy(workflow => workflow.SortOrder);
        }

        private async Task<IEnumerable<QuestionView>> GetQuestionViewsAsync(IEnumerable<Question> questions, AgentVersionApplicant applicant) {
            var questionViews = new List<QuestionView>();

            foreach (var question in questions) {
                var mappedValue = await _questionHandler.GetApplicantMappedValueAsync(applicant, question);

                questionViews.Add(new QuestionView {
                    QuestionId = question.Id,
                    LabelText = question.LabelText,
                    ValueType = (ViewModels.ValueType)(int)question.ValueType,
                    MappedValue = mappedValue
                });
            }

            return questionViews;
        }

        private async Task<AgentVersionApplicant> GetApplicantAsync(long accountId, long applicantId) {
            var applicant = await _internalQuotingDbRepository
                .FindSingleByAsync<AgentVersionApplicant>(a => a.SQSAccountId == accountId && a.SQSApplicantId == applicantId);

            if (applicant == null) {
                throw new ApplicantNotFoundException();
            }

            return applicant;
        }
    }
}
