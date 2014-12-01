namespace Vue
{
    partial class FrmCreateHeros
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
            this.label3 = new System.Windows.Forms.Label();
            this.cmbClass = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbWorld = new System.Windows.Forms.ComboBox();
            this.btnCreer = new System.Windows.Forms.Button();
            this.grpBoxDescription = new System.Windows.Forms.GroupBox();
            this.txtDescrptionClasse = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDex = new System.Windows.Forms.TextBox();
            this.txtStr = new System.Windows.Forms.TextBox();
            this.txtStamina = new System.Windows.Forms.TextBox();
            this.txtIntelligence = new System.Windows.Forms.TextBox();
            this.btnRegenerateStats = new System.Windows.Forms.Button();
            this.grpBoxDescription.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Mistral", 21.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(12, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(314, 34);
            this.label3.TabIndex = 18;
            this.label3.Text = "CRÉER UN NOUVEAU HÉROS";
            // 
            // cmbClass
            // 
            this.cmbClass.FormattingEnabled = true;
            this.cmbClass.Location = new System.Drawing.Point(127, 69);
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Size = new System.Drawing.Size(208, 21);
            this.cmbClass.Sorted = true;
            this.cmbClass.TabIndex = 17;
            this.cmbClass.SelectedIndexChanged += new System.EventHandler(this.cmbClass_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Classe de votre héros";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Choix du monde";
            // 
            // cmbWorld
            // 
            this.cmbWorld.Location = new System.Drawing.Point(127, 42);
            this.cmbWorld.Name = "cmbWorld";
            this.cmbWorld.Size = new System.Drawing.Size(208, 21);
            this.cmbWorld.Sorted = true;
            this.cmbWorld.TabIndex = 14;
            this.cmbWorld.SelectedIndexChanged += new System.EventHandler(this.cmbWorld_SelectedIndexChanged);
            // 
            // btnCreer
            // 
            this.btnCreer.Location = new System.Drawing.Point(344, 180);
            this.btnCreer.Name = "btnCreer";
            this.btnCreer.Size = new System.Drawing.Size(216, 23);
            this.btnCreer.TabIndex = 13;
            this.btnCreer.Text = "Créer";
            this.btnCreer.UseVisualStyleBackColor = true;
            this.btnCreer.Click += new System.EventHandler(this.btnCreer_Click);
            // 
            // grpBoxDescription
            // 
            this.grpBoxDescription.Controls.Add(this.txtDescrptionClasse);
            this.grpBoxDescription.Location = new System.Drawing.Point(15, 97);
            this.grpBoxDescription.Name = "grpBoxDescription";
            this.grpBoxDescription.Size = new System.Drawing.Size(317, 106);
            this.grpBoxDescription.TabIndex = 19;
            this.grpBoxDescription.TabStop = false;
            this.grpBoxDescription.Text = "Description de la classe";
            // 
            // txtDescrptionClasse
            // 
            this.txtDescrptionClasse.BackColor = System.Drawing.SystemColors.Control;
            this.txtDescrptionClasse.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescrptionClasse.Enabled = false;
            this.txtDescrptionClasse.Location = new System.Drawing.Point(6, 19);
            this.txtDescrptionClasse.Multiline = true;
            this.txtDescrptionClasse.Name = "txtDescrptionClasse";
            this.txtDescrptionClasse.Size = new System.Drawing.Size(305, 72);
            this.txtDescrptionClasse.TabIndex = 0;
            this.txtDescrptionClasse.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(341, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Dextérité";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(341, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Force";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(341, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Stamina";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(341, 123);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Intelligence";
            // 
            // txtDex
            // 
            this.txtDex.BackColor = System.Drawing.SystemColors.Control;
            this.txtDex.Enabled = false;
            this.txtDex.Location = new System.Drawing.Point(430, 42);
            this.txtDex.Name = "txtDex";
            this.txtDex.Size = new System.Drawing.Size(127, 20);
            this.txtDex.TabIndex = 27;
            // 
            // txtStr
            // 
            this.txtStr.BackColor = System.Drawing.SystemColors.Control;
            this.txtStr.Enabled = false;
            this.txtStr.Location = new System.Drawing.Point(430, 68);
            this.txtStr.Name = "txtStr";
            this.txtStr.Size = new System.Drawing.Size(127, 20);
            this.txtStr.TabIndex = 28;
            // 
            // txtStamina
            // 
            this.txtStamina.BackColor = System.Drawing.SystemColors.Control;
            this.txtStamina.Enabled = false;
            this.txtStamina.Location = new System.Drawing.Point(430, 94);
            this.txtStamina.Name = "txtStamina";
            this.txtStamina.Size = new System.Drawing.Size(127, 20);
            this.txtStamina.TabIndex = 29;
            // 
            // txtIntelligence
            // 
            this.txtIntelligence.BackColor = System.Drawing.SystemColors.Control;
            this.txtIntelligence.Enabled = false;
            this.txtIntelligence.Location = new System.Drawing.Point(430, 120);
            this.txtIntelligence.Name = "txtIntelligence";
            this.txtIntelligence.Size = new System.Drawing.Size(127, 20);
            this.txtIntelligence.TabIndex = 30;
            // 
            // btnRegenerateStats
            // 
            this.btnRegenerateStats.Location = new System.Drawing.Point(344, 146);
            this.btnRegenerateStats.Name = "btnRegenerateStats";
            this.btnRegenerateStats.Size = new System.Drawing.Size(216, 28);
            this.btnRegenerateStats.TabIndex = 33;
            this.btnRegenerateStats.Text = "Regenerer les stats";
            this.btnRegenerateStats.UseVisualStyleBackColor = true;
            this.btnRegenerateStats.Click += new System.EventHandler(this.btnRegenerateStats_Click);
            // 
            // FrmCreateHeros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 218);
            this.Controls.Add(this.btnRegenerateStats);
            this.Controls.Add(this.txtIntelligence);
            this.Controls.Add(this.txtStamina);
            this.Controls.Add(this.txtStr);
            this.Controls.Add(this.txtDex);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.grpBoxDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbClass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbWorld);
            this.Controls.Add(this.btnCreer);
            this.Name = "FrmCreateHeros";
            this.Text = "Créer votre héros!";
            this.grpBoxDescription.ResumeLayout(false);
            this.grpBoxDescription.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbClass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbWorld;
        private System.Windows.Forms.Button btnCreer;
        private System.Windows.Forms.GroupBox grpBoxDescription;
        private System.Windows.Forms.TextBox txtDescrptionClasse;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDex;
        private System.Windows.Forms.TextBox txtStr;
        private System.Windows.Forms.TextBox txtStamina;
        private System.Windows.Forms.TextBox txtIntelligence;
        private System.Windows.Forms.Button btnRegenerateStats;
    }
}