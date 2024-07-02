﻿namespace WinFormsApp1
{
    partial class DeathCounter
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tb_AreaName = new TextBox();
            btn_cgAreaName = new Button();
            ShortcutPoolingTimer = new System.Windows.Forms.Timer(components);
            lbl_AreaDeaths = new Label();
            button1 = new Button();
            button2 = new Button();
            label2 = new Label();
            label3 = new Label();
            lbl_totalDeaths = new Label();
            SuspendLayout();
            // 
            // tb_AreaName
            // 
            tb_AreaName.Enabled = false;
            tb_AreaName.Font = new Font("Segoe UI", 12F);
            tb_AreaName.Location = new Point(7, 41);
            tb_AreaName.Name = "tb_AreaName";
            tb_AreaName.Size = new Size(350, 29);
            tb_AreaName.TabIndex = 2;
            tb_AreaName.TextAlign = HorizontalAlignment.Center;
            tb_AreaName.KeyPress += onTextBoxKeyPress;
            // 
            // btn_cgAreaName
            // 
            btn_cgAreaName.Location = new Point(7, 12);
            btn_cgAreaName.Name = "btn_cgAreaName";
            btn_cgAreaName.Size = new Size(350, 23);
            btn_cgAreaName.TabIndex = 1;
            btn_cgAreaName.Text = "Change Area Name";
            btn_cgAreaName.UseVisualStyleBackColor = true;
            btn_cgAreaName.Click += setAreaName;
            // 
            // ShortcutPoolingTimer
            // 
            ShortcutPoolingTimer.Enabled = true;
            ShortcutPoolingTimer.Interval = 16;
            ShortcutPoolingTimer.Tick += ShortcutPoolingTimerTick;
            // 
            // lbl_AreaDeaths
            // 
            lbl_AreaDeaths.Font = new Font("Segoe UI", 16F);
            lbl_AreaDeaths.Location = new Point(154, 114);
            lbl_AreaDeaths.Name = "lbl_AreaDeaths";
            lbl_AreaDeaths.Size = new Size(198, 35);
            lbl_AreaDeaths.TabIndex = 3;
            lbl_AreaDeaths.Text = "0";
            lbl_AreaDeaths.TextAlign = ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 12F);
            button1.Location = new Point(185, 76);
            button1.Name = "button1";
            button1.Size = new Size(172, 35);
            button1.TabIndex = 4;
            button1.Text = "+1 Death";
            button1.UseVisualStyleBackColor = true;
            button1.Click += AddDeath;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 12F);
            button2.Location = new Point(7, 76);
            button2.Name = "button2";
            button2.Size = new Size(172, 35);
            button2.TabIndex = 5;
            button2.Text = "-1 Death";
            button2.UseVisualStyleBackColor = true;
            button2.Click += RemoveDeath;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 16F);
            label2.Location = new Point(7, 114);
            label2.Name = "label2";
            label2.Size = new Size(141, 35);
            label2.TabIndex = 6;
            label2.Text = "Area Deaths:";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 16F);
            label3.Location = new Point(7, 149);
            label3.Name = "label3";
            label3.Size = new Size(141, 35);
            label3.TabIndex = 7;
            label3.Text = "Total Deaths:";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lbl_totalDeaths
            // 
            lbl_totalDeaths.Font = new Font("Segoe UI", 16F);
            lbl_totalDeaths.Location = new Point(154, 149);
            lbl_totalDeaths.Name = "lbl_totalDeaths";
            lbl_totalDeaths.Size = new Size(198, 35);
            lbl_totalDeaths.TabIndex = 8;
            lbl_totalDeaths.Text = "0";
            lbl_totalDeaths.TextAlign = ContentAlignment.MiddleRight;
            // 
            // DeathCounter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(364, 188);
            Controls.Add(lbl_totalDeaths);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(lbl_AreaDeaths);
            Controls.Add(tb_AreaName);
            Controls.Add(btn_cgAreaName);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DeathCounter";
            ShowIcon = false;
            StartPosition = FormStartPosition.Manual;
            Text = "Death Tracker";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblAreaName;
        private TextBox tb_AreaName;
        private Button btn_cgAreaName;
        private System.Windows.Forms.Timer ShortcutPoolingTimer;
        private Panel panel1;
        private Label lbl_AreaDeaths;
        private Button button1;
        private Button button2;
        private Label label2;
        private Label label3;
        private Label lbl_totalDeaths;
    }
}
