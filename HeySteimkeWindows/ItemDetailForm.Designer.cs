namespace HeySteimkeWindows
{
    partial class ItemDetailForm
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
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.createdLabel = new System.Windows.Forms.Label();
            this.acceptedLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.itemActionButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(12, 32);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(298, 26);
            this.nameTextBox.TabIndex = 0;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.AcceptsReturn = true;
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 84);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(506, 134);
            this.descriptionTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name";
            // 
            // createdLabel
            // 
            this.createdLabel.AutoSize = true;
            this.createdLabel.Location = new System.Drawing.Point(12, 221);
            this.createdLabel.Name = "createdLabel";
            this.createdLabel.Size = new System.Drawing.Size(84, 20);
            this.createdLabel.TabIndex = 3;
            this.createdLabel.Text = "Erstellt am";
            // 
            // acceptedLabel
            // 
            this.acceptedLabel.AutoSize = true;
            this.acceptedLabel.Location = new System.Drawing.Point(12, 241);
            this.acceptedLabel.Name = "acceptedLabel";
            this.acceptedLabel.Size = new System.Drawing.Size(135, 20);
            this.acceptedLabel.TabIndex = 4;
            this.acceptedLabel.Text = "Angenommen am";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Beschreibung";
            // 
            // itemActionButton
            // 
            this.itemActionButton.Location = new System.Drawing.Point(12, 264);
            this.itemActionButton.Name = "itemActionButton";
            this.itemActionButton.Size = new System.Drawing.Size(506, 50);
            this.itemActionButton.TabIndex = 6;
            this.itemActionButton.Text = "button1";
            this.itemActionButton.UseVisualStyleBackColor = true;
            this.itemActionButton.Click += new System.EventHandler(this.itemActionButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(12, 320);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(506, 50);
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // ItemDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 381);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.itemActionButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.acceptedLabel);
            this.Controls.Add(this.createdLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Name = "ItemDetailForm";
            this.Text = "ItemDetailForm";
            this.Load += new System.EventHandler(this.ItemDetailForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label createdLabel;
        private System.Windows.Forms.Label acceptedLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button itemActionButton;
        private System.Windows.Forms.Button saveButton;
    }
}