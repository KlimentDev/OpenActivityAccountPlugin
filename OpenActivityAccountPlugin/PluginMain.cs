using Microsoft.Xrm.Sdk;
using OpenActivityAccountPlugin.Operations;
using System;
using System.ServiceModel;

namespace OpenActivityAccountPlugin
{
    public class PluginMain : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {  //tracing service
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            //context
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            //check for entity or Pre-Entity Images
            if ((context.InputParameters.Contains("Target") &&
               context.InputParameters["Target"] is Entity) || (context.PreEntityImages.Contains("image")))
            {
                //access service factory
                IOrganizationServiceFactory serviceFactory =
                    (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

                //access service
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                    try
                    {   //configure operation
                        Operation accountOperation = OperationFactory.Operation(service, context);
                        
                        //gets regardingobjectid
                        Guid regardingObjectId = accountOperation.GetRegardingObjectId();

                        //gets activities and owner of activities, configures and sends update request
                        new NextOpenActivity(regardingObjectId, service).Update();
                    }

                    catch (FaultException<OrganizationServiceFault> ex)
                    {
                        throw new InvalidPluginExecutionException("An error occurred in OpenAcitivtyAccountPlugin.", ex);
                    }

                    catch (Exception ex)
                    {
                        tracingService.Trace("PluginMain: {0}", ex.ToString());
                        throw;
                    }
            }
        }
    }
}
