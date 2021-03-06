﻿using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace UltimateWorkflowToolkit.CoreOperations.BulkOperations
{
    public class DistributeAction : BulkOperationBase
    {
        #region Inputs

        [Input("Request")]
        public InArgument<string> SerializedObject { get; set; }

        [Input("Action Name")]
        [RequiredArgument]
        public InArgument<string> ActionName { get; set; }

        #endregion Inputs
    
        protected override void PerformOperation(Entity childRecord)
        {
            var request = DeserializeDictionary(SerializedObject.Get(Context.ExecutionContext));

            var organizationRequest = new OrganizationRequest(ActionName.Get(Context.ExecutionContext));

            foreach (var key in request.Keys)
            {
                organizationRequest[key] = request[key];
            }

            organizationRequest["Target"] = childRecord.ToEntityReference();

            Context.UserService.Execute(organizationRequest);
        }
    }
}
