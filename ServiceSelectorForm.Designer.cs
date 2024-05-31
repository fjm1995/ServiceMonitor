namespace ServiceMonitor
{
    partial class ServiceSelectorForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox listBoxServices;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            listBoxServices = new ListBox();
            btnOK = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // listBoxServices
            // 
            listBoxServices.ItemHeight = 15;
            listBoxServices.Location = new Point(12, 12);
            listBoxServices.Name = "listBoxServices";
            listBoxServices.SelectionMode = SelectionMode.MultiExtended;
            listBoxServices.Size = new Size(290, 319);
            listBoxServices.TabIndex = 0;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(308, 12);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(90, 29);
            btnOK.TabIndex = 1;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += BtnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(308, 47);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 29);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;
            // 
            // ServiceSelectorForm
            // 
            AutoScaleBaseSize = new Size(6, 16);
            ClientSize = new Size(408, 341);
            Controls.Add(listBoxServices);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Name = "ServiceSelectorForm";
            Text = "Select Services";
            Load += ServiceSelectorForm_Load;
            ResumeLayout(false);
        }
    }
}