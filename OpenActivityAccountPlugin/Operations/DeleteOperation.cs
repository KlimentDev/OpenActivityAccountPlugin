using Microsoft.Xrm.Sdk;

namespace OpenActivityAccountPlugin.Operations
{
    public class DeleteOperation:Operation
    {
        /// <summary>
        /// Delete operation constructor
        /// </summary>
        /// <param name="organizationService"></param>
        /// <param name="context"></param>
        public DeleteOperation(IOrganizationService organizationService, IPluginExecutionContext context)
           : base(organizationService, context)
        {
            Entity = Context.PreEntityImages["image"];
        }
    }
}
