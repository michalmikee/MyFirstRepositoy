using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace StoppingTest.Helpers
{
    public class IISNotifier : IRegisteredObject
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);

        public void Stop(bool immediate)
        {
            // do nothing, I only run tasks if I know that they won't
            // take more than a few seconds.
            
            log.Debug($"Stop requested, immediate: {immediate}, reason: {HostingEnvironment.ShutdownReason}");
        }

        public void Started()
        {
            log.Debug($"Started");
            HostingEnvironment.RegisterObject(this);
            HostingEnvironment.IncrementBusyCount();
        }

        public void Finished()
        {
            log.Debug($"Finished");
            HostingEnvironment.UnregisterObject(this);
            HostingEnvironment.DecrementBusyCount();
        }


        ~IISNotifier()
        {
            log.Debug($"Garbage collection");
        }
    }
}