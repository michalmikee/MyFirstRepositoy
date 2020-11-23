using log4net;
using StoppingTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace StoppingTest.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);

        

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            log.Debug("Started");




            var task = Task.Factory.StartNew(() =>
            {

                IISNotifier notif = new IISNotifier();
                notif.Started();
                log.Debug("Task started");

                Thread.Sleep(60000);

                log.Debug("Task ended");



                notif.Finished();
            });

            log.Debug("Task waiting...");
            task.Wait();
            log.Debug("Task finished...");

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}