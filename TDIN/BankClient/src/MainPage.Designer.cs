namespace BankClient
{
    partial class MainPage
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
            this.userName = new System.Windows.Forms.Label();
            this.ProfielPanel = new System.Windows.Forms.Panel();
            this.PasswordBtn = new System.Windows.Forms.Button();
            this.UsernameBtn = new System.Windows.Forms.Button();
            this.NameBtn = new System.Windows.Forms.Button();
            this.lb1 = new System.Windows.Forms.Label();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.ProfielPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // userName
            // 
            this.userName.Dock = System.Windows.Forms.DockStyle.Top;
            this.userName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userName.Location = new System.Drawing.Point(0, 0);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(782, 39);
            this.userName.TabIndex = 0;
            this.userName.Text = "Nome de utilizador";
            this.userName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProfielPanel
            // 
            this.ProfielPanel.Controls.Add(this.PasswordBtn);
            this.ProfielPanel.Controls.Add(this.UsernameBtn);
            this.ProfielPanel.Controls.Add(this.NameBtn);
            this.ProfielPanel.Controls.Add(this.lb1);
            this.ProfielPanel.Controls.Add(this.UsernameLabel);
            this.ProfielPanel.Controls.Add(this.NameLabel);
            this.ProfielPanel.Location = new System.Drawing.Point(7, 42);
            this.ProfielPanel.MinimumSize = new System.Drawing.Size(375, 194);
            this.ProfielPanel.Name = "ProfielPanel";
            this.ProfielPanel.Size = new System.Drawing.Size(375, 194);
            this.ProfielPanel.TabIndex = 1;
            // 
            // PasswordBtn
            // 
            this.PasswordBtn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.PasswordBtn.FlatAppearance.BorderSize = 0;
            this.PasswordBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PasswordBtn.Location = new System.Drawing.Point(71, 141);
            this.PasswordBtn.Name = "PasswordBtn";
            this.PasswordBtn.Size = new System.Drawing.Size(205, 33);
            this.PasswordBtn.TabIndex = 2;
            this.PasswordBtn.Text = "Change Password";
            this.PasswordBtn.UseVisualStyleBackColor = false;
            // 
            // UsernameBtn
            // 
            this.UsernameBtn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.UsernameBtn.FlatAppearance.BorderSize = 0;
            this.UsernameBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UsernameBtn.Location = new System.Drawing.Point(201, 99);
            this.UsernameBtn.Name = "UsernameBtn";
            this.UsernameBtn.Size = new System.Drawing.Size(75, 33);
            this.UsernameBtn.TabIndex = 4;
            this.UsernameBtn.Text = "Change";
            this.UsernameBtn.UseVisualStyleBackColor = false;
            // 
            // NameBtn
            // 
            this.NameBtn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.NameBtn.FlatAppearance.BorderSize = 0;
            this.NameBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NameBtn.Location = new System.Drawing.Point(201, 56);
            this.NameBtn.Name = "NameBtn";
            this.NameBtn.Size = new System.Drawing.Size(75, 33);
            this.NameBtn.TabIndex = 3;
            this.NameBtn.Text = "Change";
            this.NameBtn.UseVisualStyleBackColor = false;
            // 
            // lb1
            // 
            this.lb1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lb1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb1.Location = new System.Drawing.Point(0, 0);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(375, 29);
            this.lb1.TabIndex = 2;
            this.lb1.Text = "Profile";
            this.lb1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.UsernameLabel.Location = new System.Drawing.Point(67, 102);
            this.UsernameLabel.MinimumSize = new System.Drawing.Size(100, 25);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(100, 25);
            this.UsernameLabel.TabIndex = 1;
            this.UsernameLabel.Text = "Username";
            this.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NameLabel.Location = new System.Drawing.Point(67, 59);
            this.NameLabel.MinimumSize = new System.Drawing.Size(100, 25);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(100, 25);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "Name";
            this.NameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.ProfielPanel);
            this.Controls.Add(this.userName);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainPage";
            this.Text = "Bank";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //this.Load += new System.EventHandler(this.MainPage_Load);
            this.ProfielPanel.ResumeLayout(false);
            this.ProfielPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label userName;
        private System.Windows.Forms.Panel ProfielPanel;
        private System.Windows.Forms.Button PasswordBtn;
        private System.Windows.Forms.Button UsernameBtn;
        private System.Windows.Forms.Button NameBtn;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.Label NameLabel;
    }
}