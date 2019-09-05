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
            this.comment = new System.Windows.Forms.TextBox();
            this.send = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.blackTimer_label = new System.Windows.Forms.Label();
            this.whiteTimer_label = new System.Windows.Forms.Label();
            this.blackTimer = new System.Timers.Timer();
            this.whiteTimer = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.gomoku_area)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blackTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.whiteTimer)).BeginInit();
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
            this.Enter_Button.Location = new System.Drawing.Point(687, 9);
            this.Enter_Button.Name = "Enter_Button";
            this.Enter_Button.Size = new System.Drawing.Size(100, 40);
            this.Enter_Button.TabIndex = 1;
            this.Enter_Button.Text = "ENTER";
            this.Enter_Button.UseVisualStyleBackColor = true;
            this.Enter_Button.Click += new System.EventHandler(this.Enter_Button_Click);
            // 
            // GameStart
            // 
            this.GameStart.Location = new System.Drawing.Point(614, 64);
            this.GameStart.Name = "GameStart";
            this.GameStart.Size = new System.Drawing.Size(100, 40);
            this.GameStart.TabIndex = 2;
            this.GameStart.Text = "START";
            this.GameStart.UseVisualStyleBackColor = true;
            this.GameStart.Click += new System.EventHandler(this.GameStart_Click);
            // 
            // Room
            // 
            this.Room.Location = new System.Drawing.Point(538, 20);
            this.Room.Name = "Room";
            this.Room.Size = new System.Drawing.Size(131, 21);
            this.Room.TabIndex = 3;
            // 
            // Status
            // 
            this.Status.Location = new System.Drawing.Point(559, 228);
            this.Status.Multiline = true;
            this.Status.Name = "Status";
            this.Status.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Status.Size = new System.Drawing.Size(203, 241);
            this.Status.TabIndex = 4;
            // 
            // comment
            // 
            this.comment.Location = new System.Drawing.Point(559, 490);
            this.comment.Name = "comment";
            this.comment.Size = new System.Drawing.Size(144, 21);
            this.comment.TabIndex = 5;
            this.comment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comment_KeyDown);
            // 
            // send
            // 
            this.send.Location = new System.Drawing.Point(709, 488);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(53, 23);
            this.send.TabIndex = 6;
            this.send.Text = "send";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.send_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(555, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 22);
            this.label1.TabIndex = 7;
            this.label1.Text = "BLACK";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(704, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 22);
            this.label2.TabIndex = 8;
            this.label2.Text = "WHITE";
            // 
            // blackTimer_label
            // 
            this.blackTimer_label.AutoSize = true;
            this.blackTimer_label.Font = new System.Drawing.Font("Trebuchet MS", 20F, System.Drawing.FontStyle.Bold);
            this.blackTimer_label.Location = new System.Drawing.Point(553, 156);
            this.blackTimer_label.Name = "blackTimer_label";
            this.blackTimer_label.Size = new System.Drawing.Size(47, 35);
            this.blackTimer_label.TabIndex = 9;
            this.blackTimer_label.Text = "00";
            // 
            // whiteTimer_label
            // 
            this.whiteTimer_label.AutoSize = true;
            this.whiteTimer_label.Font = new System.Drawing.Font("Trebuchet MS", 20F, System.Drawing.FontStyle.Bold);
            this.whiteTimer_label.Location = new System.Drawing.Point(715, 156);
            this.whiteTimer_label.Name = "whiteTimer_label";
            this.whiteTimer_label.Size = new System.Drawing.Size(47, 35);
            this.whiteTimer_label.TabIndex = 10;
            this.whiteTimer_label.Text = "00";
            // 
            // blackTimer
            // 
            this.blackTimer.Interval = 1000D;
            this.blackTimer.SynchronizingObject = this;
            this.blackTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.blackTimer_Elapsed);
            // 
            // whiteTimer
            // 
            this.whiteTimer.Interval = 1000D;
            this.whiteTimer.SynchronizingObject = this;
            this.whiteTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.whiteTimer_Elapsed);
            // 
            // MultiPlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 549);
            this.Controls.Add(this.whiteTimer_label);
            this.Controls.Add(this.blackTimer_label);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.send);
            this.Controls.Add(this.comment);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.Room);
            this.Controls.Add(this.GameStart);
            this.Controls.Add(this.Enter_Button);
            this.Controls.Add(this.gomoku_area);
            this.Name = "MultiPlay";
            this.Text = "MultiPlay";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MultiPlay_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.gomoku_area)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blackTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.whiteTimer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox gomoku_area;
        private System.Windows.Forms.Button Enter_Button;
        private System.Windows.Forms.Button GameStart;
        private System.Windows.Forms.TextBox Room;
        private System.Windows.Forms.TextBox Status;
        private System.Windows.Forms.TextBox comment;
        private System.Windows.Forms.Button send;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label blackTimer_label;
        private System.Windows.Forms.Label whiteTimer_label;
        private System.Timers.Timer blackTimer;
        private System.Timers.Timer whiteTimer;
    }
}