using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Getting_real;
using System.Timers;

namespace MCMHarvestService
{
    public partial class MCMHarvestService : ServiceBase
    {
        private Timer timer;
        public MCMHarvestService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //runs OnElapsed every 5seconds
            timer = new Timer(5000);
            timer.AutoReset = true;
            timer.Start();
            timer.Elapsed += new ElapsedEventHandler(OnElapsed);
        }

        protected override void OnStop()
        {
            
        }

        private void OnElapsed(object source, ElapsedEventArgs e)
        {
            Controller controller = new Controller();
            controller.Run();
        }
    }
}
