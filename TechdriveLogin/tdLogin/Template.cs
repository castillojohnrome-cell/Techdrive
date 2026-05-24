using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechdriveLogin.tdLogin
{
    public partial class Template : Form
    {
        public Template()
        {
            InitializeComponent();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the SETTINGS button", "Settings - TechDrive", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            // Close the current sub-form, which automatically triggers the hidden dashboard's FormClosed handler to show it again.
            this.Close();
        }

        private void btnCntctUs_Click(object sender, EventArgs e)
        {
            string contactInfo = "TechDrive - Support & Localized Presence\n\n" +
                                 "📍 Headquarters (HQ):\n" +
                                 "TechDrive HQ, Angeles City, Pampanga, Philippines\n\n" +
                                 "📞 Primary Contact Hotline:\n" +
                                 "09153442904\n\n" +
                                 "✉️ Corporate Email:\n" +
                                 "support@techdrive.com\n\n" +
                                 "Feel free to reach out to us for any business inquiries or support requests!";
            MessageBox.Show(contactInfo, "Contact Us - TechDrive", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            string aboutInfo = "About TechDrive\n\n" +
                               "TechDrive is a premium software-as-a-service (SaaS) platform built specifically to empower local car rental owners by delivering enterprise-grade digital fleet tracking, automated scheduling, and real-time financial insights.\n\n" +
                               "💡 Our Vision:\n" +
                               "To eliminate manual tracking friction (like paper notebooks and spreadsheets) and prevent double-bookings, all for a hyper-affordable price of just ₱166 per day.\n\n" +
                               "🚀 Project Objectives:\n" +
                               "• Digital Transformation: Integrated real-time tracking.\n" +
                               "• Operational Efficiency: Complete booking overlap prevention.\n" +
                               "• Financial Empowerment: Granular business analytics.\n\n" +
                               "Created by Rei Payumo (Admin) & team.";
            MessageBox.Show(aboutInfo, "About Us - TechDrive", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
