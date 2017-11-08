namespace UPVApp
{
    partial class EditAddress
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.editAddressCmbBx = new System.Windows.Forms.ComboBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.okBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // editAddressCmbBx
            // 
            this.editAddressCmbBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.editAddressCmbBx.FormattingEnabled = true;
            this.editAddressCmbBx.Location = new System.Drawing.Point(55, 77);
            this.editAddressCmbBx.Name = "editAddressCmbBx";
            this.editAddressCmbBx.Size = new System.Drawing.Size(270, 28);
            this.editAddressCmbBx.TabIndex = 1;
            this.editAddressCmbBx.Validating += new System.ComponentModel.CancelEventHandler(this.editAddressCmbBx_Validating);
            this.editAddressCmbBx.Validated += new System.EventHandler(this.editAddressCmbBx_Validated);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(55, 111);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(130, 36);
            this.okBtn.TabIndex = 2;
            this.okBtn.Text = "Ok";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(100, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select an address to edit.";
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(195, 111);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(130, 36);
            this.cancelBtn.TabIndex = 0;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // EditAddress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 200);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.editAddressCmbBx);
            this.Name = "EditAddress";
            this.Text = "Edit Address";
            this.Load += new System.EventHandler(this.EditAddress_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox editAddressCmbBx;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelBtn;
    }
}