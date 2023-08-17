using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Security.AccessControl;
using System.IO;
using System.Timers;

namespace CST_Service
{
    public partial class CstService : ServiceBase
    {
        Timer timer = new Timer();
        private Process batchProcess;

        public CstService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteDoc("The service has been started ---- " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 300000;
            timer.Enabled = true;

            string batchFilePath = Environment.GetEnvironmentVariable("CST_Service_PATH");

            if (string.IsNullOrEmpty(batchFilePath))
            {
                EventLog.WriteEntry("CstService", "Environment variable CST_Service_PATH not set or empty.");
                // You might want to handle this situation accordingly
                return;
            }
            // Set the working directory to the batch file's directory
            string workingDirectory = System.IO.Path.GetDirectoryName(@batchFilePath);

            // Start the process
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = batchFilePath,
                WorkingDirectory = workingDirectory,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            Process process = new Process { StartInfo = psi };
            process.Start();        }

        protected override void OnStop()
        {
            WriteDoc("The service has been stopped ---- " + DateTime.Now);
            if (batchProcess != null && !batchProcess.HasExited)
            {
                batchProcess.Kill();
                batchProcess.Dispose();
            }

        
        }
        private void OnElapsedTime(object sender, ElapsedEventArgs e)
        {
            WriteDoc("The service working well ---- " + DateTime.Now);
        }
        public void WriteDoc(string message)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "/logs";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string textPath = AppDomain.CurrentDomain.BaseDirectory + "/logs/service_log.txt";

            if (!File.Exists(textPath))
            {
                using (StreamWriter writer = File.CreateText(textPath))
                {
                    writer.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter writer = File.AppendText(textPath))
                {
                    writer.WriteLine(message);
                }
            }


        }
    }
}
