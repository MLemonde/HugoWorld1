namespace Vue
{
    partial class frmManage
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
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dtgridViewHeros = new System.Windows.Forms.DataGridView();
            this.Classe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Monde = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Niveau = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Experience = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heroBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnCreate = new System.Windows.Forms.Button();
            this.txtMoney = new System.Windows.Forms.TextBox();
            this.txtIntelligence = new System.Windows.Forms.TextBox();
            this.txtStamina = new System.Windows.Forms.TextBox();
            this.txtStr = new System.Windows.Forms.TextBox();
            this.txtDex = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgridViewHeros)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heroBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(12, 241);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(545, 28);
            this.btnPlay.TabIndex = 39;
            this.btnPlay.Text = "Jouer!";
            this.btnPlay.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(344, 173);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(213, 28);
            this.btnDelete.TabIndex = 38;
            this.btnDelete.Text = "Supprimer Heros";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Mistral", 21.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(6, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(210, 34);
            this.label4.TabIndex = 37;
            this.label4.Text = "GÉRER VOS HÉROS";
            // 
            // dtgridViewHeros
            // 
            this.dtgridViewHeros.AllowUserToAddRows = false;
            this.dtgridViewHeros.AllowUserToDeleteRows = false;
            this.dtgridViewHeros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgridViewHeros.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Classe,
            this.Monde,
            this.Niveau,
            this.Experience});
            this.dtgridViewHeros.Location = new System.Drawing.Point(12, 46);
            this.dtgridViewHeros.Name = "dtgridViewHeros";
            this.dtgridViewHeros.ReadOnly = true;
            this.dtgridViewHeros.Size = new System.Drawing.Size(323, 189);
            this.dtgridViewHeros.TabIndex = 36;
            // 
            // Classe
            // 
            this.Classe.HeaderText = "Classe";
            this.Classe.Name = "Classe";
            this.Classe.ReadOnly = true;
            this.Classe.Width = 75;
            // 
            // Monde
            // 
            this.Monde.HeaderText = "Monde";
            this.Monde.Name = "Monde";
            this.Monde.ReadOnly = true;
            this.Monde.Width = 75;
            // 
            // Niveau
            // 
            this.Niveau.HeaderText = "Niveau";
            this.Niveau.Name = "Niveau";
            this.Niveau.ReadOnly = true;
            this.Niveau.Width = 65;
            // 
            // Experience
            // 
            this.Experience.HeaderText = "Experience";
            this.Experience.Name = "Experience";
            this.Experience.ReadOnly = true;
            this.Experience.Width = 65;
            // 
            // heroBindingSource
            // 
            this.heroBindingSource.DataSource = typeof(HugoWorldServiceRef.Hero);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(344, 207);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(213, 28);
            this.btnCreate.TabIndex = 40;
            this.btnCreate.Text = "Créer un héros";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // txtMoney
            // 
            this.txtMoney.BackColor = System.Drawing.SystemColors.Control;
            this.txtMoney.Enabled = false;
            this.txtMoney.Location = new System.Drawing.Point(430, 147);
            this.txtMoney.Name = "txtMoney";
            this.txtMoney.Size = new System.Drawing.Size(127, 20);
            this.txtMoney.TabIndex = 50;
            // 
            // txtIntelligence
            // 
            this.txtIntelligence.BackColor = System.Drawing.SystemColors.Control;
            this.txtIntelligence.Enabled = false;
            this.txtIntelligence.Location = new System.Drawing.Point(430, 121);
            this.txtIntelligence.Name = "txtIntelligence";
            this.txtIntelligence.Size = new System.Drawing.Size(127, 20);
            this.txtIntelligence.TabIndex = 49;
            // 
            // txtStamina
            // 
            this.txtStamina.BackColor = System.Drawing.SystemColors.Control;
            this.txtStamina.Enabled = false;
            this.txtStamina.Location = new System.Drawing.Point(430, 95);
            this.txtStamina.Name = "txtStamina";
            this.txtStamina.Size = new System.Drawing.Size(127, 20);
            this.txtStamina.TabIndex = 48;
            // 
            // txtStr
            // 
            this.txtStr.BackColor = System.Drawing.SystemColors.Control;
            this.txtStr.Enabled = false;
            this.txtStr.Location = new System.Drawing.Point(430, 69);
            this.txtStr.Name = "txtStr";
            this.txtStr.Size = new System.Drawing.Size(127, 20);
            this.txtStr.TabIndex = 47;
            // 
            // txtDex
            // 
            this.txtDex.BackColor = System.Drawing.SystemColors.Control;
            this.txtDex.Enabled = false;
            this.txtDex.Location = new System.Drawing.Point(430, 43);
            this.txtDex.Name = "txtDex";
            this.txtDex.Size = new System.Drawing.Size(127, 20);
            this.txtDex.TabIndex = 46;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(341, 150);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 45;
            this.label10.Text = "Argent";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(341, 124);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 44;
            this.label9.Text = "Intelligence";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(341, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 43;
            this.label8.Text = "Stamina";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(341, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 42;
            this.label7.Text = "Force";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(341, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 41;
            this.label6.Text = "Dextérité";
            // 
            // frmManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 280);
            this.Controls.Add(this.txtMoney);
            this.Controls.Add(this.txtIntelligence);
            this.Controls.Add(this.txtStamina);
            this.Controls.Add(this.txtStr);
            this.Controls.Add(this.txtDex);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtgridViewHeros);
            this.Name = "frmManage";
            this.Text = "fmrManage";
            ((System.ComponentModel.ISupportInitialize)(this.dtgridViewHeros)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heroBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dtgridViewHeros;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TextBox txtMoney;
        private System.Windows.Forms.TextBox txtIntelligence;
        private System.Windows.Forms.TextBox txtStamina;
        private System.Windows.Forms.TextBox txtStr;
        private System.Windows.Forms.TextBox txtDex;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.BindingSource heroBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn Classe;
        private System.Windows.Forms.DataGridViewTextBoxColumn Monde;
        private System.Windows.Forms.DataGridViewTextBoxColumn Niveau;
        private System.Windows.Forms.DataGridViewTextBoxColumn Experience;
    }
}