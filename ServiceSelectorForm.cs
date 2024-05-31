using System;
using System.Linq;
using System.ServiceProcess;
using System.Windows.Forms;

namespace ServiceMonitor
{
    public partial class ServiceSelectorForm : Form
    {
        public ServiceSelectorForm()
        {
            InitializeComponent();
        }

        private void ServiceSelectorForm_Load(object sender, EventArgs e)
        {
            PopulateServicesList();
        }

        private void PopulateServicesList()
        {
            listBoxServices.Items.Clear();
            ServiceController[] services = ServiceController.GetServices();

            // Sort services alphabetically by DisplayName
            var sortedServices = services.OrderBy(service => service.DisplayName).ToArray();

            foreach (var service in sortedServices)
            {
                listBoxServices.Items.Add(service.DisplayName);
            }
        }


        public string[] SelectedServices
        {
            get { return listBoxServices.SelectedItems.Cast<string>().ToArray(); }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            // Set the dialog result to OK and close the form
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            // Set the dialog result to Cancel and close the form
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
