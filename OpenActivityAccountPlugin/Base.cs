using Microsoft.Xrm.Sdk;

namespace OpenActivityAccountPlugin
{
    public abstract class Base
    {
        /// <summary>
        /// Access service
        /// </summary>
        protected IOrganizationService OrganizationService { get; set; }

        /// <summary>
        /// Context service
        /// </summary>
        protected IPluginExecutionContext Context { get; set; }

        /// <summary>
        /// Entity
        /// </summary>
        protected Entity Entity { get; set; }

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="organizationService"></param>
        /// <param name="context"></param>
        public Base(IOrganizationService organizationService, IPluginExecutionContext context)
        {
            OrganizationService = organizationService;
            Context = context;
        }
    }
}
