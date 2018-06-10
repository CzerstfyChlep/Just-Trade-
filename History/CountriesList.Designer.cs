namespace History
{
    partial class CountriesList
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
            this.CountriesBox = new System.Windows.Forms.ComboBox();
            this.CreateButton = new System.Windows.Forms.Button();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.RulerBox = new System.Windows.Forms.TextBox();
            this.AltRulerBox = new System.Windows.Forms.TextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.ColorButton = new System.Windows.Forms.Button();
            this.ColorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CountriesBox
            // 
            this.CountriesBox.FormattingEnabled = true;
            this.CountriesBox.Items.AddRange(new object[] {
            "New Country"});
            this.CountriesBox.Location = new System.Drawing.Point(12, 12);
            this.CountriesBox.Name = "CountriesBox";
            this.CountriesBox.Size = new System.Drawing.Size(260, 21);
            this.CountriesBox.TabIndex = 0;
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(197, 226);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(75, 23);
            this.CreateButton.TabIndex = 1;
            this.CreateButton.Text = "Create";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(12, 39);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(100, 20);
            this.NameBox.TabIndex = 2;
            this.NameBox.Text = "Name";
            // 
            // RulerBox
            // 
            this.RulerBox.Location = new System.Drawing.Point(118, 39);
            this.RulerBox.Name = "RulerBox";
            this.RulerBox.Size = new System.Drawing.Size(154, 20);
            this.RulerBox.TabIndex = 3;
            this.RulerBox.Text = "Ruler";
            // 
            // AltRulerBox
            // 
            this.AltRulerBox.Location = new System.Drawing.Point(118, 65);
            this.AltRulerBox.Name = "AltRulerBox";
            this.AltRulerBox.Size = new System.Drawing.Size(154, 20);
            this.AltRulerBox.TabIndex = 4;
            this.AltRulerBox.Text = "Alternative Ruler";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(116, 226);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 5;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ColorButton
            // 
            this.ColorButton.Location = new System.Drawing.Point(12, 113);
            this.ColorButton.Name = "ColorButton";
            this.ColorButton.Size = new System.Drawing.Size(260, 23);
            this.ColorButton.TabIndex = 6;
            this.ColorButton.Text = "Choose Color";
            this.ColorButton.UseVisualStyleBackColor = true;
            this.ColorButton.Click += new System.EventHandler(this.ColorButton_Click);
            // 
            // ColorLabel
            // 
            this.ColorLabel.Location = new System.Drawing.Point(12, 88);
            this.ColorLabel.Name = "ColorLabel";
            this.ColorLabel.Size = new System.Drawing.Size(260, 22);
            this.ColorLabel.TabIndex = 7;
            this.ColorLabel.Text = "Color";
            this.ColorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CountriesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.ColorLabel);
            this.Controls.Add(this.ColorButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.AltRulerBox);
            this.Controls.Add(this.RulerBox);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.CountriesBox);
            this.Name = "CountriesList";
            this.Text = "CountriesList";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CountriesBox;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.TextBox RulerBox;
        private System.Windows.Forms.TextBox AltRulerBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button ColorButton;
        private System.Windows.Forms.Label ColorLabel;
    }
}