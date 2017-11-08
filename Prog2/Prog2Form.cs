// Program 2
// CIS 200
// Fall 2016
// Due: 11/1/2016
// By: Andrew L. Wright (Students use Grading ID)

// File: Prog2Form.cs
// This class creates the main GUI for Program 2. It provides a
// File menu with About and Exit items, an Insert menu with Address and
// Letter items, and a Report menu with List Addresses and List Parcels
// items.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace UPVApp
{
    [Serializable]
    public partial class Prog2Form : Form
    {
        private UserParcelView upv; // The UserParcelView

        BinaryFormatter reader = new BinaryFormatter(); //seserializing in binary
        FileStream input; //stream to read from a file

        BinaryFormatter formatter = new BinaryFormatter(); //serialize in binary
        FileStream output; //stream to write to file

        // Precondition:  None
        // Postcondition: The form's GUI is prepared for display.
        public Prog2Form()
        {
            InitializeComponent();

            upv = new UserParcelView();
        }

        // Precondition:  File, About menu item activated
        // Postcondition: Information about author displayed in dialog box
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NL = Environment.NewLine; // Newline shorthand

            MessageBox.Show($"Program 3{NL}By: Rakesh R. Mahbubani{NL}CIS 200{NL}Fall 2016",
                "About Program 3");
        }

        // Precondition:  File, Exit menu item activated
        // Postcondition: The application is exited
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Precondition:  Insert, Address menu item activated
        // Postcondition: The Address dialog box is displayed. If data entered
        //                are OK, an Address is created and added to the list
        //                of addresses
        private void addressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddressForm addressForm = new AddressForm();    // The address dialog box form
            DialogResult result = addressForm.ShowDialog(); // Show form as dialog and store result

            if (result == DialogResult.OK) // Only add if OK
            {
                try
                {
                    upv.AddAddress(addressForm.AddressName, addressForm.Address1,
                        addressForm.Address2, addressForm.City, addressForm.State,
                        int.Parse(addressForm.ZipText)); // Use form's properties to create address
                }
                catch (FormatException) // This should never happen if form validation works!
                {
                    MessageBox.Show("Problem with Address Validation!", "Validation Error");
                }
            }

            addressForm.Dispose(); // Best practice for dialog boxes
        }

        // Precondition:  Report, List Addresses menu item activated
        // Postcondition: The list of addresses is displayed in the addressResultsTxt
        //                text box
        private void listAddressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder(); // Holds text as report being built
                                                        // StringBuilder more efficient than String
            string NL = Environment.NewLine;            // Newline shorthand

            result.Append("Addresses:");
            result.Append(NL); // Remember, \n doesn't always work in GUIs
            result.Append(NL);

            foreach (Address a in upv.AddressList)
            {
                result.Append(a.ToString());
                result.Append(NL);
                result.Append("------------------------------");
                result.Append(NL);
            }

            reportTxt.Text = result.ToString();

            // Put cursor at start of report
            reportTxt.Focus();
            reportTxt.SelectionStart = 0;
            reportTxt.SelectionLength = 0;
        }

        // Precondition:  Insert, Letter menu item activated
        // Postcondition: The Letter dialog box is displayed. If data entered
        //                are OK, a Letter is created and added to the list
        //                of parcels
        private void letterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LetterForm letterForm; // The letter dialog box form
            DialogResult result;   // The result of showing form as dialog

            if (upv.AddressCount < LetterForm.MIN_ADDRESSES) // Make sure we have enough addresses
            {
                MessageBox.Show("Need " + LetterForm.MIN_ADDRESSES + " addresses to create letter!",
                    "Addresses Error");
                return;
            }

            letterForm = new LetterForm(upv.AddressList); // Send list of addresses
            result = letterForm.ShowDialog();

            if (result == DialogResult.OK) // Only add if OK
            {
                try
                {
                    // For this to work, LetterForm's combo boxes need to be in same
                    // order as upv's AddressList
                    upv.AddLetter(upv.AddressAt(letterForm.OriginAddressIndex),
                        upv.AddressAt(letterForm.DestinationAddressIndex),
                        decimal.Parse(letterForm.FixedCostText)); // Letter to be inserted
                }
                catch (FormatException) // This should never happen if form validation works!
                {
                    MessageBox.Show("Problem with Letter Validation!", "Validation Error");
                }
            }

            letterForm.Dispose(); // Best practice for dialog boxes
        }

        // Precondition:  Report, List Parcels menu item activated
        // Postcondition: The list of parcels is displayed in the parcelResultsTxt
        //                text box
        private void listParcelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder(); // Holds text as report being built
                                                        // StringBuilder more efficient than String
            decimal totalCost = 0;                      // Running total of parcel shipping costs
            string NL = Environment.NewLine;            // Newline shorthand

            result.Append("Parcels:");
            result.Append(NL); // Remember, \n doesn't always work in GUIs
            result.Append(NL);

            foreach (Parcel p in upv.ParcelList)
            {
                result.Append(p.ToString());
                result.Append(NL);
                result.Append("------------------------------");
                result.Append(NL);
                totalCost += p.CalcCost();
            }

            result.Append(NL);
            result.Append($"Total Cost: {totalCost:C}");

            reportTxt.Text = result.ToString();

            // Put cursor at start of report
            reportTxt.Focus();
            reportTxt.SelectionStart = 0;
            reportTxt.SelectionLength = 0;
        }

        private void openAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result; //result of dialog box
            string fileName; //file name to save data

            using (OpenFileDialog fileChooser = new OpenFileDialog())
            {
                result = fileChooser.ShowDialog();
                fileName = fileChooser.FileName; //get specified name
            }

            //user clicked OK
            if (result == DialogResult.OK)
                //show error if invalid file
                if (fileName == string.Empty)
                    MessageBox.Show("Invalid File Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    try
                    {
                        //create FileStream to obtain read access to file
                        input = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                        upv = (UserParcelView)reader.Deserialize(input); //upv is set to deserialized data from file
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("Error Reading From File", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (SerializationException)
                    {
                        //notify if error occurs in serialization
                        MessageBox.Show("Error Reading From File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }            
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result; //dialog box result
            string fileName; //name of file to save data

            using (SaveFileDialog fileChooser = new SaveFileDialog())
            {
                fileChooser.CheckFileExists = false; //let user create file
                result = fileChooser.ShowDialog();
                fileName = fileChooser.FileName; //name of file to save data
            }

            //user clicked OK
            if (result == DialogResult.OK)
            {
                //show error if user specified invalid file
                if (fileName == string.Empty)
                    MessageBox.Show("Invalid File Name", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    try
                    {
                        //open file with write access
                        output = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);

                        formatter.Serialize(output, upv); //output is serialized into upv format
                    }
                    //exception if problem opening file
                    catch(IOException)
                    {
                        //notify user if file doesn't exist
                        MessageBox.Show("Error Opening File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch(SerializationException)
                    {
                        //notify if error occurs in serialization
                        MessageBox.Show("Error Writing to File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void editAddressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Address> add = upv.addresses; //address list set to addresses in upv
            if (add.Count() == 0) //addresses must be loaded
                MessageBox.Show("No Addresses Loaded");
            else
            {
                EditAddress ea = new EditAddress(add); //passes add to new EditAddress form
                DialogResult result = ea.ShowDialog(); //show and store dialog result

                if (result == DialogResult.OK) //is result ok?
                    if (ea.CmbBxIndex() != -1) //must select from combo box
                    {
                        Address eadd = add[ea.CmbBxIndex()]; //combo box index is used to identify address being edited
                        AddressForm af = new AddressForm(); //new AddressForm to edit address identified above
                        //each property from identified address is passed to new AddressForm
                        af.AddressName = eadd.Name;
                        af.Address1 = eadd.Address1;
                        af.Address2 = eadd.Address2;
                        af.City = eadd.City;
                        af.State = eadd.State;
                        af.ZipText = eadd.Zip.ToString();

                        DialogResult eres = af.ShowDialog(); //show and store dialog result

                        if (eres == DialogResult.OK) //is result ok?
                        {
                            //user input replaces properties from identified address
                            eadd.Name = af.AddressName;
                            eadd.Address1 = af.Address1;
                            eadd.Address2 = af.Address2;
                            eadd.City = af.City;
                            eadd.State = af.State;
                            eadd.Zip = int.Parse(af.ZipText);
                        }
                    }
            }
        }
    }
}