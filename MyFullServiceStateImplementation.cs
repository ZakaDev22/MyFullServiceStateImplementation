using System;
using System.Configuration;
using System.IO;
using System.ServiceProcess;



namespace MyFullServiceStateImplementation
{
    public partial class MyFullServiceStateImplementation : ServiceBase
    {
        private string logDirectory;
        private string logFilePath;

        public MyFullServiceStateImplementation()
        {
            InitializeComponent();

            // Set the CanPauseAndContinue property to true
            CanPauseAndContinue = true; //The service supports pausing and resuming operations.

            // Enable support for OnShutdown
            CanShutdown = true; // The service is notified when the system shuts down.


            // Read log directory path from App.config
            //The service reads the log directory path from an external configuration file (App.config) for flexibility.
            logDirectory = ConfigurationManager.AppSettings["LogDirectory"];


            // Validate and create directory if it doesn't exist
            if (string.IsNullOrWhiteSpace(logDirectory))
            {
                throw new ConfigurationErrorsException("LogDirectory is not specified in the configuration file.");
            }

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            logFilePath = Path.Combine(logDirectory, "ServiceStateLog.txt");
        }



        // Log a message to a file with a timestamp
        //The service logs all its state transitions (Start, Stop, Pause, Continue, Shutdown) to a file named ServiceStateLog.txt in the configured directory.
        // Each log entry includes a timestamp for tracking purposes.
        private void LogServiceEvent(string message)
        {
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}\n";
            File.AppendAllText(logFilePath, logMessage);

            // Write to console if running interactively
            if (Environment.UserInteractive)
            {
                Console.WriteLine(logMessage);
            }
        }

        // OnStart Event
        protected override void OnStart(string[] args)
        {
            LogServiceEvent("Service Started");
            // Add initialization logic here

        }

        // OnStop Event
        protected override void OnStop()
        {
            LogServiceEvent("Service Stopped");
            // Add cleanup logic here
        }

        // OnPause Event
        protected override void OnPause()
        {
            LogServiceEvent("Service Paused");
            // Add pause logic here
        }

        // OnContinue Event
        protected override void OnContinue()
        {
            LogServiceEvent("Service Resumed");
            // Add resume logic here
        }

        // OnShutdown Event
        protected override void OnShutdown()
        {
            LogServiceEvent("Service Shutdown due to system shutdown");
            // Add shutdown cleanup logic here
        }

        // This is added
        // Simulate service behavior in console mode
        public void StartInConsole()
        {
            OnStart(null); // Trigger OnStart logic
            Console.WriteLine("Press Enter to stop the service...");
            Console.ReadLine(); // Wait for user input to simulate service stopping
            OnStop(); // Trigger OnStop logic
            Console.ReadKey();

        }

    }
}
