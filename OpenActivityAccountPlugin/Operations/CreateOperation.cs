using Microsoft.Xrm.Sdk;

namespace OpenActivityAccountPlugin.Operations
{
    public class CreateOperation :Operation
    {
        /// <summary>
        /// Create operation constructor
        /// </summary>
        /// <param name="organizationService"></param>
        /// <param name="context"></param>
        public CreateOperation(IOrganizationService organizationService, IPluginExecutionContext context)
            : base(organizationService, context)
        {
            Entity = (Entity)Context.InputParameters["Target"];
        }
    }
}
