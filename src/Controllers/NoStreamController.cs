using log4net;
using StoppingTest.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;

namespace StoppingTest.Controllers
{
    public class NoStreamController : ApiController
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);


        // http://localhost:12084/api/file
        public HttpResponseMessage Get()
        {
            //var man = ApplicationManager.GetApplicationManager();
            //var notif = (IISNotifier)man.CreateObject(HostingEnvironment.ApplicationHost, typeof(IISNotifier));
            //var notif = new IISNotifier();
            //notif.Started();

            //IISNotifier Notifier = new IISNotifier();
            //Notifier.Started();

            log.Debug("---------------------------------------------");

            var notif = new IISNotifier();
            notif.Started();

            log.Debug("Task started");

            int maxCount = 60;
            for (int i = 0; i < maxCount; i++)
            {
                string message = $", {i}";

                //outputStream.Write(uniEncoding.GetBytes(message), 0, uniEncoding.GetBytes(message).Length);
                log.Debug(i.ToString());
                Thread.Sleep(1000);
            }

            log.Debug("Finish");
            notif.Finished();

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = null
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            result.Content.Headers.ContentEncoding.Add("utf-8");
            return result;
        }
    }
}
