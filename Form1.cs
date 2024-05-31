using System;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Windows.Forms;
using Newtonsoft.Json;
using Microsoft.Win32; // For registry operations

namespace ServiceMonitor
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer monitoringTimer = new System.Windows.Forms.Timer();
        private readonly System.Collections.Generic.List<string> servicesToMonitor = new System.Collections.Generic.List<string>();

        public Form1()
        {
            InitializeComponent();
            InitializeTimer();
            LoadServicesList();  // Load the monitored services at startup
        }

        private void InitializeTimer()
        {
            monitoringTimer.Tick += OnTimedEvent;
            monitoringTimer.Interval = 5000;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateServiceList();
        }

        private void SaveServicesList()
        {
            var key = Registry.CurrentUser.CreateSubKey(@"Software\ServiceMonitor");
            if (key != null)
            {
                string json = JsonConvert.SerializeObject(servicesToMonitor);
                key.SetValue("MonitoredServices", json);
                key.Close();
            }
        }

        private void LoadServicesList()
        {
            var key = Registry.CurrentUser.OpenSubKey(@"Software\ServiceMonitor");
            if (key != null)
            {
                string json = key.GetValue("MonitoredServices") as string;
                if (!string.IsNullOrEmpty(json))
                {
                    servicesToMonitor.Clear();
                    servicesToMonitor.AddRange(JsonConvert.DeserializeObject<System.Collections.Generic.List<string>>(json));
                    listBox1.Items.Clear();
                    listBox1.Items.AddRange(servicesToMonitor.ToArray());
                }
                key.Close();
            }
        }

        private void UpdateServiceList()
        {
            listBox1.Items.Clear();
            var services = ServiceController.GetServices();
            listBox1.Items.AddRange(services.Select(s => s.DisplayName).OrderBy(name => name).ToArray());
            servicesToMonitor.Clear();
            servicesToMonitor.AddRange(services.Select(s => s.ServiceName));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (monitoringTimer.Enabled)
            {
                monitoringTimer.Stop();
                button2.BackColor = Color.Red;
                button2.Text = "Start Monitoring";
                label1.Text = "Monitoring stopped.";
            }
            else
            {
                monitoringTimer.Start();
                button2.BackColor = Color.Green;
                button2.Text = "Stop Monitoring";
                label1.Text = "Monitoring started.";
                SaveServicesList();  // Save on starting to persist the latest state
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var serviceSelector = new ServiceSelectorForm())
            {
                if (serviceSelector.ShowDialog() == DialogResult.OK)
                {
                    foreach (string service in serviceSelector.SelectedServices)
                    {
                        if (!listBox1.Items.Contains(service))
                        {
                            listBox1.Items.Add(service);
                            servicesToMonitor.Add(service);
                        }
                    }
                    SaveServicesList();  // Save whenever the list changes
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string selectedServiceName = listBox1.SelectedItem.ToString();
                listBox1.Items.Remove(listBox1.SelectedItem);
                servicesToMonitor.Remove(selectedServiceName);
                SaveServicesList();  // Save whenever the list changes
                label1.Text = "Service removed.";
                if (servicesToMonitor.Count == 0)
                {
                    monitoringTimer.Stop();
                    button2.BackColor = Color.Red;
                    button2.Text = "Start Monitoring";
                    label1.Text = "Monitoring stopped.";
                }
            }
            else
            {
                MessageBox.Show("Please select a service to remove.");
            }
        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            foreach (string serviceName in servicesToMonitor)
            {
                ServiceController service = new ServiceController(serviceName);
                try
                {
                    service.Refresh();
                    if (service.Status != ServiceControllerStatus.Running)
                    {
                        service.Start();
                        service.WaitForStatus(ServiceControllerStatus.Running);
                        Invoke(new Action(() =>
                        {
                            label1.Text = $"{service.DisplayName} restarted by monitor.";
                        }));
                    }
                }
                catch (Exception ex)
                {
                    Invoke(new Action(() =>
                    {
                        MessageBox.Show($"Error monitoring service {service.DisplayName}: {ex.Message}");
                        label1.Text = "Error monitoring service.";
                    }));
                }
            }
        }
    }
}
