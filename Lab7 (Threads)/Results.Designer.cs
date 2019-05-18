namespace Lab7__Threads_
{
    partial class Results
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
            this.lb_Text = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_Restart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_Text
            // 
            this.lb_Text.AutoSize = true;
            this.lb_Text.Location = new System.Drawing.Point(9, 13);
            this.lb_Text.Name = "lb_Text";
            this.lb_Text.Size = new System.Drawing.Size(85, 13);
            this.lb_Text.TabIndex = 0;
            this.lb_Text.Text = "Игра окончена!";
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(12, 62);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 2;
            this.btn_Close.Text = "Закрыть";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Restart
            // 
            this.btn_Restart.Location = new System.Drawing.Point(259, 62);
            this.btn_Restart.Name = "btn_Restart";
            this.btn_Restart.Size = new System.Drawing.Size(80, 23);
            this.btn_Restart.TabIndex = 1;
            this.btn_Restart.Text = "Перезапуск";
            this.btn_Restart.UseVisualStyleBackColor = true;
            this.btn_Restart.Click += new System.EventHandler(this.btn_Restart_Click);
            // 
            // Results
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 97);
            this.Controls.Add(this.btn_Restart);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.lb_Text);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Results";
            this.Text = "Results";
            this.Load += new System.EventHandler(this.Results_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_Text;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Restart;
    }
}