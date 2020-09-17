using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SharePoint;
using Newtonsoft.Json;

namespace AuditingExample
{
    public partial class DemoForm : Form
    {
        public DemoForm()
        {
            InitializeComponent();
        }

        private void GetAuditEntriesButton_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://sp2016");

            SPAudit audit = site.Audit;

            var collection = audit.GetEntries();

            string json = JsonConvert.SerializeObject(collection);
            File.WriteAllText(@"C:\aduit.log", json);

            grid.DataSource = collection.Cast<SPAuditEntry>().ToList();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://sp2016");

            SPAudit audit = site.Audit;

            audit.AuditFlags = SPAuditMaskType.None;

            audit.Update();

            MessageBox.Show("Updated!");
        }

        private void WriteAuditEvent_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://sp2016");

            SPAudit audit = site.Audit;

            audit.WriteAuditEvent(SPAuditEventType.Custom, "MySource", "<xml/>");

            MessageBox.Show("Written!");
        }

        private void QueryButton_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://sp2016");

            SPAuditQuery query = new SPAuditQuery(site);
            query.AddEventRestriction(SPAuditEventType.View);
            query.RestrictToList(site.OpenWeb().Lists[0]);

            var collection = site.Audit.GetEntries(query);

            grid.DataSource = collection.Cast<SPAuditEntry>().ToList();
        }
    }
}
