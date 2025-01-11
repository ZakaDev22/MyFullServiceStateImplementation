using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace MyFullServiceStateImplementation
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        private ServiceProcessInstaller processInstaller;
        private ServiceInstaller serviceInstaller;

        public ProjectInstaller()
        {
            InitializeComponent();

            // Initialize ServiceProcessInstaller
            processInstaller = new ServiceProcessInstaller
            {
                // Run the service under the local system account
                Account = ServiceAccount.LocalSystem
            };

            // Initialize ServiceInstaller
            serviceInstaller = new ServiceInstaller
            {
                // Set the name of the service
                ServiceName = "MyFullServiceStateImplementation",
                DisplayName = "My Full Service State Implementation Example",
                Description = "A Windows Service that demonstrates all service states and events.",
                StartType = ServiceStartMode.Automatic // Automatically starts the service on system boot
            };

            // Add both installers to the Installers collection
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);

        }
    }
}
