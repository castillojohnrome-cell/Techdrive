using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TechdriveLogin
{
    public partial class Vehicles : TechdriveLogin.tdLogin.Template
    {
        private List<VehicleInfo> _loadedVehicles = new List<VehicleInfo>();

        public Vehicles()
        {
            InitializeComponent();
            this.Load += (s, e) => LoadVehiclesData();
        }

        private void LoadVehiclesData()
        {
            try
            {
                Panel scrollPanel = panel7.Controls["dynamicScrollPanel"] as Panel;
                if (scrollPanel == null)
                {
                    scrollPanel = new Panel
                    {
                        Name = "dynamicScrollPanel",
                        Location = new Point(0, 90),
                        Size = new Size(panel7.Width - 30, panel7.Height - 90), // Width is 966
                        AutoScroll = true,
                        BackColor = Color.Transparent
                    };
                    panel7.Controls.Add(scrollPanel);

                    // Add Guna custom scrollbar
                    var scrollbar = new Guna.UI2.WinForms.Guna2VScrollBar
                    {
                        Name = "customScrollbar",
                        BindingContainer = scrollPanel,
                        Location = new Point(panel7.Width - 15, 90),
                        Size = new Size(10, panel7.Height - 90),
                        FillColor = Color.FromArgb(2, 36, 78), // Matches panel7's dark blue
                        ThumbColor = Color.FromArgb(135, 226, 98), // Matches brand green
                        BorderRadius = 4
                    };
                    panel7.Controls.Add(scrollbar);

                    // Hide original designer row controls
                    Label[] vmLabels = { lblVm1, lblVm2, lblVm3, lblVm4, lblVm5, lblVm6, lblVm7 };
                    Label[] pnLabels = { lblPn1, lblPn2, lblPn3, lblPn4, lblPn5, lblPn6, lblPn7 };
                    Label[] remarksLabels = { lblRemarks1, lblRemarks2, lblRemarks3, lblRemarks4, lblRemarks5, lblRemarks6, lblRemarks7 };
                    Label[] statusLabels = { lblStatus1, lblStatus2, lblStatus3, lblStatus4, lblStatus5, lblStatus6, lblStatus7 };
                    Button[] statusButtons = { btnStm1, btnStm2, btnStm3, btnStm4, btnStm5, btnStm6, btnStm7 };
                    
                    // Hide original designer grid lines (both horizontal and vertical)
                    Label[] gridLines = { label4, label5, label6, label7, label8, label9, label10, label15, label16, label18 };
                    foreach (var line in gridLines) { if (line != null) line.Visible = false; }

                    for (int i = 0; i < 7; i++)
                    {
                        if (vmLabels[i] != null) vmLabels[i].Visible = false;
                        if (pnLabels[i] != null) pnLabels[i].Visible = false;
                        if (remarksLabels[i] != null) remarksLabels[i].Visible = false;
                        if (statusLabels[i] != null) statusLabels[i].Visible = false;
                        if (statusButtons[i] != null) statusButtons[i].Visible = false;
                    }
                }

                scrollPanel.Controls.Clear();
                _loadedVehicles = DatabaseHelper.GetVehicles(100);

                int rowHeight = 58;
                for (int i = 0; i < _loadedVehicles.Count; i++)
                {
                    var vehicle = _loadedVehicles[i];
                    int yPos = i * rowHeight;

                    // 1. Vehicle Model Label
                    Label lblVm = new Label
                    {
                        Text = $"{vehicle.Make} {vehicle.Model}",
                        Location = new Point(16, yPos + 6),
                        Size = new Size(164, 47),
                        Font = new Font("Century Gothic", 12F, FontStyle.Bold),
                        ForeColor = Color.White,
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    // 2. Plate Number Label
                    Label lblPn = new Label
                    {
                        Text = vehicle.PlateNumber,
                        Location = new Point(199, yPos + 6),
                        Size = new Size(140, 47),
                        Font = new Font("Century Gothic", 12F, FontStyle.Bold),
                        ForeColor = Color.White,
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    // 3. Remarks Label
                    Label lblRemarks = new Label
                    {
                        Text = vehicle.Remarks,
                        Location = new Point(365, yPos + 6),
                        Size = new Size(380, 47),
                        Font = new Font("Century Gothic", 11.25F, FontStyle.Bold),
                        ForeColor = Color.White,
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    // 4. Status Label
                    Label lblStatus = new Label
                    {
                        Text = vehicle.Status,
                        Location = new Point(775, yPos + 6),
                        Size = new Size(135, 47),
                        Font = new Font("Century Gothic", 12F, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    // 5. Action Button - shifted slightly left to fit inside the panel width (ends at 966)
                    Button btnStm = new Button
                    {
                        Location = new Point(915, yPos + 16),
                        Size = new Size(46, 27),
                        FlatStyle = FlatStyle.Flat,
                        Tag = i
                    };

                    if (vehicle.Status == "Available")
                    {
                        lblStatus.ForeColor = Color.FromArgb(135, 226, 98);
                        btnStm.Text = "Maint";
                        btnStm.BackColor = Color.Red;
                        btnStm.ForeColor = Color.White;
                        btnStm.Font = new Font("Century Gothic", 8F, FontStyle.Bold);
                        btnStm.Enabled = true;
                    }
                    else if (vehicle.Status == "In Maintenance")
                    {
                        lblStatus.ForeColor = Color.FromArgb(255, 222, 89);
                        btnStm.Text = "Avail";
                        btnStm.BackColor = Color.FromArgb(29, 59, 172);
                        btnStm.ForeColor = Color.White;
                        btnStm.Font = new Font("Century Gothic", 8F, FontStyle.Bold);
                        btnStm.Enabled = true;
                    }
                    else
                    {
                        lblStatus.ForeColor = Color.FromArgb(255, 49, 49);
                        btnStm.Text = "Rented";
                        btnStm.BackColor = Color.Gray;
                        btnStm.ForeColor = Color.White;
                        btnStm.Font = new Font("Century Gothic", 7.5F, FontStyle.Bold);
                        btnStm.Enabled = false;
                    }

                    btnStm.Click += StatusButton_Click;

                    scrollPanel.Controls.Add(lblVm);
                    scrollPanel.Controls.Add(lblPn);
                    scrollPanel.Controls.Add(lblRemarks);
                    scrollPanel.Controls.Add(lblStatus);
                    scrollPanel.Controls.Add(btnStm);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading vehicle data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StatusButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is int index)
            {
                if (index >= 0 && index < _loadedVehicles.Count)
                {
                    var vehicle = _loadedVehicles[index];
                    bool success = DatabaseHelper.ToggleVehicleStatus(vehicle.VehicleId, vehicle.Status);
                    if (success)
                    {
                        LoadVehiclesData(); // Refresh list on UI
                    }
                }
            }
        }
    }
}
