namespace client
{
    partial class MultiPlay
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
            this.gomoku_area = new System.Windows.Forms.PictureBox();
            this.Enter_Button = new System.Windows.Forms.Button();
            this.GameStart = new System.Windows.Forms.Button();
            this.Room = new System.Windows.Forms.TextBox();
            this.Status = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gomoku_area)).BeginInit();
            this.SuspendLayout();
            // 
            // gomoku_area
            // 
            this.gomoku_area.BackColor = System.Drawing.Color.SandyBrown;
            this.gomoku_area.Location = new System.Drawing.Point(20, 20);
            this.gomoku_area.Name = "gomoku_area";
            this.gomoku_area.Size = new System.Drawing.Size(500, 500);
            this.gomoku_area.TabIndex = 0;
            this.gomoku_area.TabStop = false;
            this.gomoku_area.Paint += new System.Windows.Forms.PaintEventHandler(this.gomoku_area_Paint);
            this.gomoku_area.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gomoku_area_MouseDown);
            // 
            // Enter_Button
            // 
            this.Enter_Button.Location = new System.Drawing.Point(687, 29);
            this.Enter_Button.Name = "Enter_Button";
            this.Enter_Button.Size = new System.Drawing.Size(100, 40);
            this.Enter_Button.TabIndex = 1;
            this.Enter_Button.Text = "ENTER";
            this.Enter_Button.UseVisualStyleBackColor = true;
            this.Enter_Button.Click += new System.EventHandler(this.Enter_Button_Click);
            // 
            // GameStart
            // 
            this.GameStart.Location = new System.Drawing.Point(612, 93);
            this.GameStart.Name = "GameStart";
            this.GameStart.Size = new System.Drawing.Size(100, 40);
            this.GameStart.TabIndex = 2;
            this.GameStart.Text = "START";
            this.GameStart.UseVisualStyleBackColor = true;
            this.GameStart.Click += new System.EventHandler(this.GameStart_Click);
            // 
            // Room
            // 
            this.Room.Location = new System.Drawing.Point(540, 40);
            this.Room.Name = "Room";
            this.Room.Size = new System.Drawing.Size(131, 21);
            this.Room.TabIndex = 3;
            // 
            // Status
            // 
            this.Status.Location = new System.Drawing.Point(559, 149);
            this.Status.Multiline = true;
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(203, 125);
            this.Status.TabIndex = 4;
            // 
            // MultiPlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 549);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.Room);
            this.Controls.Add(this.GameStart);
            this.Controls.Add(this.Enter_Button);
            this.Controls.Add(this.gomoku_area);
            this.Name = "MultiPlay";
            this.Text = "MultiPlay";
            ((System.ComponentModel.ISupportInitialize)(this.gomoku_area)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox gomoku_area;
        private System.Windows.Forms.Button Enter_Button;
        private System.Windows.Forms.Button GameStart;
        private System.Windows.Forms.TextBox Room;
        private System.Windows.Forms.TextBox Status;
    }
}