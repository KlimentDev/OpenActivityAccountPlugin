using Microsoft.Xrm.Sdk;

namespace OpenActivityAccountPlugin.Operations
{
    public static class OperationFactory
    {
        /// <summary>
        /// Operation switch
        /// </summary>
        /// <param name="organizationService"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Operation Operation(IOrganizationService organizationService, IPluginExecutionContext context)
        {
            switch (context.MessageName)
            {
                case "Create":
                    return new CreateOperation(organizationService, context);
                case "Update":
                    return new UpdateOperation(organizationService, context);
                case "Delete":
                    return new DeleteOperation(organizationService, context);
                default:
                    return new CreateOperation(organizationService, context);
            }
        }
    }
}
