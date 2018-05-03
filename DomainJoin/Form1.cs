using System;
using System.Windows.Forms;

namespace DomainJoin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Text = System.Windows.Forms.SystemInformation.ComputerName.ToString();
        }

        private void btnChangeName_Click(object sender, EventArgs e)
        {
            Rename rn = new Rename();
            rn.Show();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            functions.SetDomainMembership("SARASOTA\vitiltech", "2018Temp!", "sarasota.k12.fl.us");
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            functions.SetDomainMembership("SARASOTA\vitiltech", "2018Temp!", "student.sarasota.k12.fl.us");
        }
    }
}
