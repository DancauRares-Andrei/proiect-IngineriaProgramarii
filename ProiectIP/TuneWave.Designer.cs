
namespace ProiectIP
{
    partial class TuneWave
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.operatiiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deschidereFisierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deschiderePlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ascultarePostRadioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crearePlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarePlaylistExistentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iesireToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajutorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.operatiiToolStripMenuItem,
            this.ajutorToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(834, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // operatiiToolStripMenuItem
            // 
            this.operatiiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deschidereFisierToolStripMenuItem,
            this.deschiderePlaylistToolStripMenuItem,
            this.ascultarePostRadioToolStripMenuItem,
            this.crearePlaylistToolStripMenuItem,
            this.modificarePlaylistExistentToolStripMenuItem,
            this.iesireToolStripMenuItem});
            this.operatiiToolStripMenuItem.Name = "operatiiToolStripMenuItem";
            this.operatiiToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.operatiiToolStripMenuItem.Text = "Meniu";
            // 
            // deschidereFisierToolStripMenuItem
            // 
            this.deschidereFisierToolStripMenuItem.Name = "deschidereFisierToolStripMenuItem";
            this.deschidereFisierToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.deschidereFisierToolStripMenuItem.Text = "Deschidere fisier";
            this.deschidereFisierToolStripMenuItem.Click += new System.EventHandler(this.DeschidereFisierToolStripMenuItem_Click);
            // 
            // deschiderePlaylistToolStripMenuItem
            // 
            this.deschiderePlaylistToolStripMenuItem.Name = "deschiderePlaylistToolStripMenuItem";
            this.deschiderePlaylistToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.deschiderePlaylistToolStripMenuItem.Text = "Deschidere playlist";
            this.deschiderePlaylistToolStripMenuItem.Click += new System.EventHandler(this.DeschiderePlaylistToolStripMenuItem_Click);
            // 
            // ascultarePostRadioToolStripMenuItem
            // 
            this.ascultarePostRadioToolStripMenuItem.Name = "ascultarePostRadioToolStripMenuItem";
            this.ascultarePostRadioToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.ascultarePostRadioToolStripMenuItem.Text = "Ascultare post radio";
            this.ascultarePostRadioToolStripMenuItem.Click += new System.EventHandler(this.AscultarePostRadioToolStripMenuItem_Click);
            // 
            // crearePlaylistToolStripMenuItem
            // 
            this.crearePlaylistToolStripMenuItem.Name = "crearePlaylistToolStripMenuItem";
            this.crearePlaylistToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.crearePlaylistToolStripMenuItem.Text = "Creare playlist";
            this.crearePlaylistToolStripMenuItem.Click += new System.EventHandler(this.CrearePlaylistToolStripMenuItem_Click);
            // 
            // modificarePlaylistExistentToolStripMenuItem
            // 
            this.modificarePlaylistExistentToolStripMenuItem.Name = "modificarePlaylistExistentToolStripMenuItem";
            this.modificarePlaylistExistentToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.modificarePlaylistExistentToolStripMenuItem.Text = "Modificare playlist existent";
            this.modificarePlaylistExistentToolStripMenuItem.Click += new System.EventHandler(this.ModificarePlaylistExistentToolStripMenuItem_Click);
            // 
            // iesireToolStripMenuItem
            // 
            this.iesireToolStripMenuItem.Name = "iesireToolStripMenuItem";
            this.iesireToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.iesireToolStripMenuItem.Text = "Iesire";
            this.iesireToolStripMenuItem.Click += new System.EventHandler(this.IesireToolStripMenuItem_Click);
            // 
            // ajutorToolStripMenuItem
            // 
            this.ajutorToolStripMenuItem.Name = "ajutorToolStripMenuItem";
            this.ajutorToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.ajutorToolStripMenuItem.Text = "Ajutor";
            this.ajutorToolStripMenuItem.Click += new System.EventHandler(this.AjutorToolStripMenuItem_Click);
            // 
            // groupBox
            // 
            this.groupBox.Location = new System.Drawing.Point(13, 28);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(809, 410);
            this.groupBox.TabIndex = 2;
            this.groupBox.TabStop = false;
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // TuneWave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 450);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.menuStrip);
            this.HelpButton = true;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "TuneWave";
            this.Text = "TuneWave";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem operatiiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deschidereFisierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deschiderePlaylistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iesireToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem crearePlaylistToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem modificarePlaylistExistentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ascultarePostRadioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ajutorToolStripMenuItem;
    }
}

