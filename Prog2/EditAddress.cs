using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UPVApp
{
    [Serializable]
    public partial class EditAddress : Form
    {
        private List<Address> addresses; //list of address in upv

        //pre: a list of addresses is passed through the constructor
        //post: addresses is assigned to the list passed
        public EditAddress(List<Address> a)
        {
            InitializeComponent();
            addresses = a;
        }

        //pre: none
        //post: the name property of each address is passed to editAddressCmbBx
        private void EditAddress_Load(object sender, EventArgs e)
        {
            foreach (Address a in addresses)
                editAddressCmbBx.Items.Add(a.Name);
        }

        //pre: none
        //post: the selected index of editAddressCmbBx is returned
        public int CmbBxIndex()
        {
            return editAddressCmbBx.SelectedIndex;
        }

        //pre: attempt to shif focus from editAddressCmbBx
        //post: if invalid, focus cannot be changed and error message will be provided
        private void editAddressCmbBx_Validating(object sender, CancelEventArgs e)
        {
            if (editAddressCmbBx.SelectedIndex == -1) //is anything selected?
            {
                e.Cancel = true;
                errorProvider1.SetError(editAddressCmbBx, "Select an address");
            }
        }

        //pre: editAddressCmbBx is valid
        //post: focus can now shift and error message will clear
        private void editAddressCmbBx_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(editAddressCmbBx, "");
        }

        //pre: user clicked okBtn
        //post: if editAddressCmbBx is validated, dialog result will be OK
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
                this.DialogResult = DialogResult.OK;
        }
        //pre: user clicked cancelBtn
        //post: form cancels dialog result and closes
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
