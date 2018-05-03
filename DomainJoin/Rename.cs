using System;
using System.Windows.Forms;

namespace DomainJoin
{
    public partial class Rename : Form
    {
        public Rename()
        {
            InitializeComponent();
        }

        private void Rename_Load(object sender, EventArgs e)
        {
          
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            functions.SetMachineName(textBox1.Text);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
