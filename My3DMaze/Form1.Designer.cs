namespace My3DMaze
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._hp = new System.Windows.Forms.Label();
            this._energy = new System.Windows.Forms.Label();
            this._power = new System.Windows.Forms.Label();
            this._plane = new System.Windows.Forms.Label();
            this._score = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this._monsters = new System.Windows.Forms.Label();
            this._wave = new System.Windows.Forms.Timer(this.components);
            this._colorDown = new System.Windows.Forms.PictureBox();
            this._colorRight = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.攻擊震動ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上下地圖ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._smallMapUp = new System.Windows.Forms.PictureBox();
            this._smallMapDown = new System.Windows.Forms.PictureBox();
            this._position3d = new System.Windows.Forms.Label();
            this._status = new System.Windows.Forms.Label();
            this._smallPlaneUp = new System.Windows.Forms.Label();
            this._smallPlaneDown = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._colorDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._colorRight)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._smallMapUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._smallMapDown)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 512);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // _hp
            // 
            this._hp.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._hp.AutoSize = true;
            this._hp.BackColor = System.Drawing.SystemColors.Control;
            this._hp.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)), true);
            this._hp.ForeColor = System.Drawing.Color.White;
            this._hp.Location = new System.Drawing.Point(229, 233);
            this._hp.Name = "_hp";
            this._hp.Size = new System.Drawing.Size(33, 21);
            this._hp.TabIndex = 9;
            this._hp.Text = "HP";
            this._hp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _energy
            // 
            this._energy.AutoSize = true;
            this._energy.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._energy.ForeColor = System.Drawing.SystemColors.ControlText;
            this._energy.Location = new System.Drawing.Point(538, 40);
            this._energy.Name = "_energy";
            this._energy.Size = new System.Drawing.Size(42, 21);
            this._energy.TabIndex = 11;
            this._energy.Text = "能量";
            // 
            // _power
            // 
            this._power.AutoSize = true;
            this._power.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._power.ForeColor = System.Drawing.SystemColors.ControlText;
            this._power.Location = new System.Drawing.Point(538, 62);
            this._power.Name = "_power";
            this._power.Size = new System.Drawing.Size(42, 21);
            this._power.TabIndex = 12;
            this._power.Text = "力量";
            // 
            // _plane
            // 
            this._plane.AutoSize = true;
            this._plane.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._plane.Location = new System.Drawing.Point(538, 105);
            this._plane.Name = "_plane";
            this._plane.Size = new System.Drawing.Size(51, 20);
            this._plane.TabIndex = 15;
            this._plane.Text = "Plane";
            // 
            // _score
            // 
            this._score.AutoSize = true;
            this._score.Font = new System.Drawing.Font("微軟正黑體", 15F, System.Drawing.FontStyle.Bold);
            this._score.ForeColor = System.Drawing.SystemColors.Highlight;
            this._score.Location = new System.Drawing.Point(376, 0);
            this._score.Name = "_score";
            this._score.Size = new System.Drawing.Size(66, 25);
            this._score.TabIndex = 16;
            this._score.Text = "Score";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // _monsters
            // 
            this._monsters.AutoSize = true;
            this._monsters.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._monsters.ForeColor = System.Drawing.SystemColors.ControlText;
            this._monsters.Location = new System.Drawing.Point(538, 84);
            this._monsters.Name = "_monsters";
            this._monsters.Size = new System.Drawing.Size(74, 21);
            this._monsters.TabIndex = 17;
            this._monsters.Text = "剩餘怪物";
            // 
            // _wave
            // 
            this._wave.Interval = 50;
            this._wave.Tick += new System.EventHandler(this._wave_Tick);
            // 
            // _colorDown
            // 
            this._colorDown.Location = new System.Drawing.Point(12, 552);
            this._colorDown.Name = "_colorDown";
            this._colorDown.Size = new System.Drawing.Size(512, 6);
            this._colorDown.TabIndex = 19;
            this._colorDown.TabStop = false;
            // 
            // _colorRight
            // 
            this._colorRight.Location = new System.Drawing.Point(524, 40);
            this._colorRight.Name = "_colorRight";
            this._colorRight.Size = new System.Drawing.Size(6, 512);
            this._colorRight.TabIndex = 21;
            this._colorRight.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.攻擊震動ToolStripMenuItem,
            this.上下地圖ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(687, 24);
            this.menuStrip1.TabIndex = 22;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 攻擊震動ToolStripMenuItem
            // 
            this.攻擊震動ToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.攻擊震動ToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.攻擊震動ToolStripMenuItem.Name = "攻擊震動ToolStripMenuItem";
            this.攻擊震動ToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.攻擊震動ToolStripMenuItem.Text = "攻擊震動 (開)";
            this.攻擊震動ToolStripMenuItem.Click += new System.EventHandler(this.攻擊震動ToolStripMenuItem_Click);
            // 
            // 上下地圖ToolStripMenuItem
            // 
            this.上下地圖ToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.上下地圖ToolStripMenuItem.Name = "上下地圖ToolStripMenuItem";
            this.上下地圖ToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.上下地圖ToolStripMenuItem.Text = "上下地圖 (開)";
            this.上下地圖ToolStripMenuItem.Click += new System.EventHandler(this.上下地圖ToolStripMenuItem_Click);
            // 
            // _smallMapUp
            // 
            this._smallMapUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._smallMapUp.Location = new System.Drawing.Point(540, 281);
            this._smallMapUp.Name = "_smallMapUp";
            this._smallMapUp.Size = new System.Drawing.Size(100, 100);
            this._smallMapUp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._smallMapUp.TabIndex = 23;
            this._smallMapUp.TabStop = false;
            // 
            // _smallMapDown
            // 
            this._smallMapDown.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._smallMapDown.Location = new System.Drawing.Point(540, 452);
            this._smallMapDown.Name = "_smallMapDown";
            this._smallMapDown.Size = new System.Drawing.Size(100, 100);
            this._smallMapDown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._smallMapDown.TabIndex = 24;
            this._smallMapDown.TabStop = false;
            // 
            // _position3d
            // 
            this._position3d.AutoSize = true;
            this._position3d.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._position3d.ForeColor = System.Drawing.Color.White;
            this._position3d.Location = new System.Drawing.Point(221, 290);
            this._position3d.Name = "_position3d";
            this._position3d.Size = new System.Drawing.Size(92, 21);
            this._position3d.TabIndex = 13;
            this._position3d.Text = "position3d";
            // 
            // _status
            // 
            this._status.AutoSize = true;
            this._status.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._status.ForeColor = System.Drawing.Color.White;
            this._status.Location = new System.Drawing.Point(30, 50);
            this._status.Margin = new System.Windows.Forms.Padding(3);
            this._status.Name = "_status";
            this._status.Size = new System.Drawing.Size(0, 26);
            this._status.TabIndex = 5;
            // 
            // _smallPlaneUp
            // 
            this._smallPlaneUp.AutoSize = true;
            this._smallPlaneUp.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._smallPlaneUp.ForeColor = System.Drawing.SystemColors.ControlText;
            this._smallPlaneUp.Location = new System.Drawing.Point(538, 384);
            this._smallPlaneUp.Name = "_smallPlaneUp";
            this._smallPlaneUp.Size = new System.Drawing.Size(51, 20);
            this._smallPlaneUp.TabIndex = 25;
            this._smallPlaneUp.Text = "plane";
            // 
            // _smallPlaneDown
            // 
            this._smallPlaneDown.AutoSize = true;
            this._smallPlaneDown.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._smallPlaneDown.ForeColor = System.Drawing.SystemColors.ControlText;
            this._smallPlaneDown.Location = new System.Drawing.Point(538, 429);
            this._smallPlaneDown.Name = "_smallPlaneDown";
            this._smallPlaneDown.Size = new System.Drawing.Size(51, 20);
            this._smallPlaneDown.TabIndex = 26;
            this._smallPlaneDown.Text = "plane";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 567);
            this.Controls.Add(this._smallPlaneDown);
            this.Controls.Add(this._smallPlaneUp);
            this.Controls.Add(this._smallMapDown);
            this.Controls.Add(this._smallMapUp);
            this.Controls.Add(this._colorRight);
            this.Controls.Add(this._colorDown);
            this.Controls.Add(this._monsters);
            this.Controls.Add(this._score);
            this.Controls.Add(this._plane);
            this.Controls.Add(this._position3d);
            this.Controls.Add(this._power);
            this.Controls.Add(this._energy);
            this.Controls.Add(this._hp);
            this.Controls.Add(this._status);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "3D迷宮";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._colorDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._colorRight)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._smallMapUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._smallMapDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label _hp;
        private System.Windows.Forms.Label _energy;
        private System.Windows.Forms.Label _power;
        private System.Windows.Forms.Label _plane;
        private System.Windows.Forms.Label _score;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label _monsters;
        private System.Windows.Forms.Timer _wave;
        private System.Windows.Forms.PictureBox _colorDown;
        private System.Windows.Forms.PictureBox _colorRight;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 攻擊震動ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上下地圖ToolStripMenuItem;
        private System.Windows.Forms.PictureBox _smallMapUp;
        private System.Windows.Forms.PictureBox _smallMapDown;
        private System.Windows.Forms.Label _position3d;
        private System.Windows.Forms.Label _status;
        private System.Windows.Forms.Label _smallPlaneUp;
        private System.Windows.Forms.Label _smallPlaneDown;
    }
}

