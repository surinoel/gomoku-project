namespace client
{
    partial class SinglePlay
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
            this.GameStart = new System.Windows.Forms.Button();
            this.ChatLog = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gomoku_area)).BeginInit();
            this.SuspendLayout();
            // 
            // gomoku_area
            // 
            this.gomoku_area.BackColor = System.Drawing.Color.SandyBrown;
            this.gomoku_area.Location = new System.Drawing.Point(23, 25);
            this.gomoku_area.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gomoku_area.Name = "gomoku_area";
            this.gomoku_area.Size = new System.Drawing.Size(571, 625);
            this.gomoku_area.TabIndex = 0;
            this.gomoku_area.TabStop = false;
            this.gomoku_area.Paint += new System.Windows.Forms.PaintEventHandler(this.gomoku_area_Paint);
            this.gomoku_area.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gomoku_area_MouseDown);
            // 
            // GameStart
            // 
            this.GameStart.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.GameStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.GameStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.GameStart.Location = new System.Drawing.Point(691, 25);
            this.GameStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GameStart.Name = "GameStart";
            this.GameStart.Size = new System.Drawing.Size(123, 71);
            this.GameStart.TabIndex = 1;
            this.GameStart.Text = "START";
            this.GameStart.UseVisualStyleBackColor = true;
            this.GameStart.Click += new System.EventHandler(this.GameStart_Click);
            // 
            // ChatLog
            // 
            this.ChatLog.Location = new System.Drawing.Point(616, 118);
            this.ChatLog.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChatLog.Multiline = true;
            this.ChatLog.Name = "ChatLog";
            this.ChatLog.Size = new System.Drawing.Size(284, 71);
            this.ChatLog.TabIndex = 2;
            this.ChatLog.Text = "START 버튼을 눌러서 게임을 시작하세요";
            // 
            // SinglePlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 686);
            this.Controls.Add(this.ChatLog);
            this.Controls.Add(this.GameStart);
            this.Controls.Add(this.gomoku_area);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SinglePlay";
            this.Text = "Gomoku SinglePlay";
            ((System.ComponentModel.ISupportInitialize)(this.gomoku_area)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox gomoku_area;
        private System.Windows.Forms.Button GameStart;
        private System.Windows.Forms.TextBox ChatLog;
    }
}