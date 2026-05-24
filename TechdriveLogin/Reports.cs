using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TechdriveLogin
{
    public partial class Reports : TechdriveLogin.tdLogin.Template
    {
        public Reports()
        {
            InitializeComponent();
            this.Load += (s, e) => LoadReportsData();
        }

        private void LoadReportsData()
        {
            try
            {
                Panel scrollPanel = panel7.Controls["dynamicScrollPanel"] as Panel;
                if (scrollPanel == null)
                {
                    scrollPanel = new Panel
                    {
                        Name = "dynamicScrollPanel",
                        Location = new Point(0, 44),
                        Size = new Size(panel7.Width - 30, panel7.Height - 44), // Width is 966
                        AutoScroll = true,
                        BackColor = Color.Transparent
                    };
                    panel7.Controls.Add(scrollPanel);

                    // Add Guna custom scrollbar
                    var scrollbar = new Guna.UI2.WinForms.Guna2VScrollBar
                    {
                        Name = "customScrollbar",
                        BindingContainer = scrollPanel,
                        Location = new Point(panel7.Width - 15, 44),
                        Size = new Size(10, panel7.Height - 44),
                        FillColor = Color.FromArgb(2, 36, 78), // Matches panel7's dark blue
                        ThumbColor = Color.FromArgb(135, 226, 98), // Matches brand green
                        BorderRadius = 4
                    };
                    panel7.Controls.Add(scrollbar);

                    // Hide original designer row controls
                    Label[] vmLabels = { reportVMLbl1, reportVMLbl2, reportVMLbl3, reportVMLbl4, reportVMLbl5, reportVMLbl6, reportVMLbl7, reportVMLbl8 };
                    Label[] pnLabels = { reportPNLbl1, reportPNLbl2, reportPNLbl3, reportPNLbl4, reportPNLbl5, reportPNLbl6, reportPNLbl7, reportPNLbl8 };
                    Label[] lmdLabels = { lblLmd1, lblLmd2, lblLmd3, lblLmd4, lblLmd5, lblLmd6, lblLmd7, lblLmd8 };
                    Label[] tbtmLabels = { lblTBTM1, lblTBTM2, lblTBTM3, lblTBTM4, lblTBTM5, lblTBTM6, lblTBTM7, lblTBTM8 };
                    Label[] netLabels = { lblNetEarnings1, lblNetEarnings2, lblNetEarnings3, lblNetEarnings4, lblNetEarnings5, lblNetEarnings6, lblNetEarnings7, lblNetEarnings8 };
                    
                    // Hide original designer grid lines (both horizontal and vertical)
                    Label[] gridLines = { label7, label8, label9, label10, label11, label12, label13, label14, label15 };
                    foreach (var line in gridLines) { if (line != null) line.Visible = false; }

                    for (int i = 0; i < 8; i++)
                    {
                        if (vmLabels[i] != null) vmLabels[i].Visible = false;
                        if (pnLabels[i] != null) pnLabels[i].Visible = false;
                        if (lmdLabels[i] != null) lmdLabels[i].Visible = false;
                        if (tbtmLabels[i] != null) tbtmLabels[i].Visible = false;
                        if (netLabels[i] != null) netLabels[i].Visible = false;
                    }
                }

                scrollPanel.Controls.Clear();
                var reports = DatabaseHelper.GetVehicleReports(100); // Load up to 100 report entries

                int rowHeight = 50;
                for (int i = 0; i < reports.Count; i++)
                {
                    var rep = reports[i];
                    int yPos = i * rowHeight;

                    // 1. Vehicle Model Label
                    Label lblVm = new Label
                    {
                        Text = rep.VehicleModel,
                        Location = new Point(5, yPos + 6),
                        Size = new Size(142, 36),
                        Font = new Font("Century Gothic", 12F, FontStyle.Bold),
                        ForeColor = Color.White,
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    // 2. Plate Number Label
                    Label lblPn = new Label
                    {
                        Text = rep.PlateNumber,
                        Location = new Point(156, yPos + 6),
                        Size = new Size(142, 36),
                        Font = new Font("Century Gothic", 12F, FontStyle.Bold),
                        ForeColor = Color.White,
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    // 3. Last Maintenance Label
                    Label lblLmd = new Label
                    {
                        Text = rep.LastMaintenance,
                        Location = new Point(305, yPos + 6),
                        Size = new Size(410, 36),
                        Font = new Font("Century Gothic", 12F, FontStyle.Bold),
                        ForeColor = Color.White,
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    // 4. Total Trips Label
                    Label lblTbtm = new Label
                    {
                        Text = rep.TotalTrips.ToString(),
                        Location = new Point(726, yPos + 6),
                        Size = new Size(138, 36),
                        Font = new Font("Century Gothic", 12F, FontStyle.Bold),
                        ForeColor = Color.White,
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    // 5. Net Earnings Label - shifted slightly left and narrowed to fit inside panel width (ends at 960)
                    Label lblNet = new Label
                    {
                        Text = $"₱{rep.NetEarnings:N2}",
                        Location = new Point(860, yPos + 6),
                        Size = new Size(100, 36),
                        Font = new Font("Century Gothic", 12F, FontStyle.Bold),
                        ForeColor = Color.White,
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    scrollPanel.Controls.Add(lblVm);
                    scrollPanel.Controls.Add(lblPn);
                    scrollPanel.Controls.Add(lblLmd);
                    scrollPanel.Controls.Add(lblTbtm);
                    scrollPanel.Controls.Add(lblNet);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
