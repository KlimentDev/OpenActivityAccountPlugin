using Microsoft.Xrm.Sdk;
using System;

namespace OpenActivityAccountPlugin.Operations
{
    public abstract class Operation : Base
    {
        /// <summary>
        /// Operation constructor
        /// </summary>
        /// <param name="organizationService"></param>
        /// <param name="context"></param>
        public Operation(IOrganizationService organizationService, IPluginExecutionContext context)
           : base(organizationService, context)
        {
        }

        /// <summary>
        /// Returns regardingobjectid
        /// </summary>
        /// <returns></returns>
        public virtual Guid GetRegardingObjectId()
        {
            return ((EntityReference)Entity.Attributes["regardingobjectid"]).Id;
        }
    }
}
