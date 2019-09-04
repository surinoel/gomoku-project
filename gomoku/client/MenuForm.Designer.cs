namespace client
{
    partial class MenuForm
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
            this.Single_Button = new System.Windows.Forms.Button();
            this.Exit_Button = new System.Windows.Forms.Button();
            this.Multi_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Single_Button
            // 
            this.Single_Button.Location = new System.Drawing.Point(319, 82);
            this.Single_Button.Name = "Single_Button";
            this.Single_Button.Size = new System.Drawing.Size(100, 40);
            this.Single_Button.TabIndex = 0;
            this.Single_Button.Text = "AI";
            this.Single_Button.UseVisualStyleBackColor = true;
            this.Single_Button.Click += new System.EventHandler(this.Single_Button_Click);
            // 
            // Exit_Button
            // 
            this.Exit_Button.Location = new System.Drawing.Point(319, 286);
            this.Exit_Button.Name = "Exit_Button";
            this.Exit_Button.Size = new System.Drawing.Size(100, 40);
            this.Exit_Button.TabIndex = 1;
            this.Exit_Button.Text = "EXIT";
            this.Exit_Button.UseVisualStyleBackColor = true;
            this.Exit_Button.Click += new System.EventHandler(this.Exit_Button_Click);
            // 
            // Multi_Button
            // 
            this.Multi_Button.Location = new System.Drawing.Point(319, 185);
            this.Multi_Button.Name = "Multi_Button";
            this.Multi_Button.Size = new System.Drawing.Size(100, 40);
            this.Multi_Button.TabIndex = 2;
            this.Multi_Button.Text = "MULTI";
            this.Multi_Button.UseVisualStyleBackColor = true;
            this.Multi_Button.Click += new System.EventHandler(this.Multi_Button_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Multi_Button);
            this.Controls.Add(this.Exit_Button);
            this.Controls.Add(this.Single_Button);
            this.Name = "MenuForm";
            this.Text = "GomokuMenu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Single_Button;
        private System.Windows.Forms.Button Exit_Button;
        private System.Windows.Forms.Button Multi_Button;
    }
}