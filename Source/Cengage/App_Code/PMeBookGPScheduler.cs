using System;
using DotNetNuke.Common.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Services.Scheduling;
using DotNetNuke.Modules.eCollection_Books.Components.Controllers;
namespace Cengage.eCollection.ScheduleTask
{

    /// <summary>
    /// Summary description for PMeBookGPScheduler
    /// </summary>
    public class PMeBookGPScheduler : SchedulerClient
    {
        public PMeBookGPScheduler(ScheduleHistoryItem oItem)
            : base()
        {
            this.ScheduleHistoryItem = oItem;
        }

        public override void DoWork()
        {
            this.Progressing();
            try
            {
                BooksController booksController = BooksController.Instance;
                booksController.RunScheduleForGracePeriodNotification(DateTime.Now); 
            }
            catch(Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
            }
            this.ScheduleHistoryItem.Succeeded = true;
        }
    }
}