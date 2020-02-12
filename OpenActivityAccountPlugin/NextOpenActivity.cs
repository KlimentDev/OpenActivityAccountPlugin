using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;

namespace OpenActivityAccountPlugin
{
    public class NextOpenActivity
    {
        /// <summary>
        /// Regarding object id
        /// </summary>
        private Guid regardingObjectId;

        /// <summary>
        /// Access service
        /// </summary>
        private IOrganizationService service;

        /// <summary>
        /// NextOpenActivity constructor
        /// </summary>
        /// <param name="regardingObjectId"></param>
        /// <param name="service"></param>
        public NextOpenActivity(Guid regardingObjectId, IOrganizationService service)
        {
            this.regardingObjectId = regardingObjectId;
            this.service = service;
        }

        /// <summary>
        /// Set activites to the associated account
        /// </summary>
        /// <param name="associateAccount"></param>
        /// <param name="associateActivities"></param>
        public void SetNextActivity(Entity associateAccount, EntityCollection associateActivities)
        {
            if (associateActivities.Entities.Any())
            {
                var nextActivity = associateActivities.Entities.First();

                associateAccount.Attributes["new_nextopenactivitysubject"] = associateAccount.Attributes["new_nextopenactivity"] = nextActivity.Attributes["subject"];

                if (nextActivity.Attributes.Contains("scheduledstart"))
                {
                    associateAccount.Attributes["new_nextopenactivitydate"] = nextActivity.Attributes["scheduledstart"];
                    associateAccount.Attributes["new_nextopenactivity"] += " : date(" + ((DateTime)nextActivity.Attributes["scheduledstart"]).ToString() + ")";
                }
            }
            else
            {
                associateAccount.Attributes["new_nextopenactivitydate"] = null;
                associateAccount.Attributes["new_nextopenactivitysubject"] = associateAccount.Attributes["new_nextopenactivity"] = string.Empty;
            }
        }

        /// <summary>
        /// Query to get activities and sort them.
        /// </summary>
        /// <returns></returns>
        public QueryExpression BuildQuery()
        {
            var activitiesQuery = new QueryExpression
            {
                EntityName = "activitypointer",
                ColumnSet = new ColumnSet(true),
                TopCount = 1
            };
            activitiesQuery.Orders.Add(new OrderExpression("scheduledstart", OrderType.Ascending));

            activitiesQuery.Criteria = new FilterExpression();
            activitiesQuery.Criteria.FilterOperator = LogicalOperator.And;

            FilterExpression filter1 = activitiesQuery.Criteria.AddFilter(LogicalOperator.And);
            filter1.Conditions.Add(new ConditionExpression("isregularactivity", ConditionOperator.Equal, true));
            filter1.Conditions.Add(new ConditionExpression("regardingobjectid", ConditionOperator.Equal, regardingObjectId));

            FilterExpression filter2 = activitiesQuery.Criteria.AddFilter(LogicalOperator.Or);
            filter2.Conditions.Add(new ConditionExpression("statecode", ConditionOperator.Equal, 0));
            filter2.Conditions.Add(new ConditionExpression("statecode", ConditionOperator.Equal, 3));

            return activitiesQuery;
        }

        /// <summary>
        /// Configuration and sending update request
        /// </summary>
        public void Update()
        {
            //get activities, sorted
            var associateActivities = service.RetrieveMultiple(BuildQuery());

            //get associated account
            var associateAccount = service.Retrieve("account", regardingObjectId, new ColumnSet(true));

            ///setting the next open activity to the associated account
            SetNextActivity(associateAccount, associateActivities);

            //send update request
            service.Update(associateAccount);
        }
    }
}
