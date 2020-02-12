using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace OpenActivityAccountPlugin.Operations
{
    public class UpdateOperation:Operation
    {
        /// <summary>
        /// Update operation constructor
        /// </summary>
        /// <param name="organizationService"></param>
        /// <param name="context"></param>
        public UpdateOperation(IOrganizationService organizationService, IPluginExecutionContext context)
            : base(organizationService, context)
        {
            Entity = (Entity)Context.InputParameters["Target"];
        }

        /// <summary>
        /// Securing regardingobjectid
        /// </summary>
        /// <returns></returns>
        public override Guid GetRegardingObjectId()
        {
            var activity = OrganizationService.Retrieve("activitypointer",
                               new Guid(Entity.Attributes["activityid"].ToString()),
                               new ColumnSet(new[] { "regardingobjectid" }));

            return ((EntityReference)activity.Attributes["regardingobjectid"]).Id;
        }
    }
}
