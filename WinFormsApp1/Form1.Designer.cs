
namespace WinFormsApp1
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.fileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.fileNewTab = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.fileCloseTab = new System.Windows.Forms.ToolStripMenuItem();
            this.fileNewWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.edit = new System.Windows.Forms.ToolStripMenuItem();
            this.editBoldYes = new System.Windows.Forms.ToolStripMenuItem();
            this.editBoldNo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editItalisYes = new System.Windows.Forms.ToolStripMenuItem();
            this.editItalicNo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.editUndelineYes = new System.Windows.Forms.ToolStripMenuItem();
            this.editUnderlineNo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.editStrikeOutYes = new System.Windows.Forms.ToolStripMenuItem();
            this.editStrikeOutNo = new System.Windows.Forms.ToolStripMenuItem();
            this.format = new System.Windows.Forms.ToolStripMenuItem();
            this.settings = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsSaveFrequency = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsSaveFrequencyNo = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsSaveFrequency1min = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsSaveFrequency5min = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsColor = new System.Windows.Forms.ToolStripMenuItem();
            this.colorBeige = new System.Windows.Forms.ToolStripMenuItem();
            this.colorGreen = new System.Windows.Forms.ToolStripMenuItem();
            this.colorPurple = new System.Windows.Forms.ToolStripMenuItem();
            this.colorStandart = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.information = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 422);
            this.tabControl1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Location = new System.Drawing.Point(0, 28);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menu
            // 
            this.menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.edit,
            this.format,
            this.settings,
            this.information});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(800, 28);
            this.menu.TabIndex = 3;
            this.menu.Text = "menuStrip2";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileOpen,
            this.fileSaveAs,
            this.fileNewTab,
            this.fileSave,
            this.fileCloseTab,
            this.fileNewWindow});
            this.menuFile.MergeIndex = 10;
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(59, 24);
            this.menuFile.Text = "Файл";
            // 
            // fileOpen
            // 
            this.fileOpen.Name = "fileOpen";
            this.fileOpen.Size = new System.Drawing.Size(206, 26);
            this.fileOpen.Text = "Открыть файл";
            this.fileOpen.Click += new System.EventHandler(this.fileOpen_Click);
            // 
            // fileSaveAs
            // 
            this.fileSaveAs.Name = "fileSaveAs";
            this.fileSaveAs.Size = new System.Drawing.Size(206, 26);
            this.fileSaveAs.Text = "Сохранить как...";
            this.fileSaveAs.Click += new System.EventHandler(this.fileSaveAs_Click);
            // 
            // fileNewTab
            // 
            this.fileNewTab.Name = "fileNewTab";
            this.fileNewTab.Size = new System.Drawing.Size(206, 26);
            this.fileNewTab.Text = "Новая вкладка";
            this.fileNewTab.Click += new System.EventHandler(this.fileNewTab_Click);
            // 
            // fileSave
            // 
            this.fileSave.Name = "fileSave";
            this.fileSave.Size = new System.Drawing.Size(206, 26);
            this.fileSave.Text = "Сохранить";
            this.fileSave.Click += new System.EventHandler(this.fileSave_Click);
            // 
            // fileCloseTab
            // 
            this.fileCloseTab.Name = "fileCloseTab";
            this.fileCloseTab.Size = new System.Drawing.Size(206, 26);
            this.fileCloseTab.Text = "Закрыть вкладку";
            this.fileCloseTab.Click += new System.EventHandler(this.fileCloseTab_Click);
            // 
            // fileNewWindow
            // 
            this.fileNewWindow.Name = "fileNewWindow";
            this.fileNewWindow.Size = new System.Drawing.Size(206, 26);
            this.fileNewWindow.Text = "Новое окно";
            this.fileNewWindow.Click += new System.EventHandler(this.fileNewWindow_Click);
            // 
            // edit
            // 
            this.edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editBoldYes,
            this.editBoldNo,
            this.toolStripSeparator1,
            this.editItalisYes,
            this.editItalicNo,
            this.toolStripSeparator2,
            this.editUndelineYes,
            this.editUnderlineNo,
            this.toolStripSeparator3,
            this.editStrikeOutYes,
            this.editStrikeOutNo});
            this.edit.Name = "edit";
            this.edit.Size = new System.Drawing.Size(74, 24);
            this.edit.Text = "Правка";
            // 
            // editBoldYes
            // 
            this.editBoldYes.Name = "editBoldYes";
            this.editBoldYes.Size = new System.Drawing.Size(254, 26);
            this.editBoldYes.Text = "Сделать жирным";
            this.editBoldYes.Click += new System.EventHandler(this.editBoldYes_Click);
            // 
            // editBoldNo
            // 
            this.editBoldNo.Name = "editBoldNo";
            this.editBoldNo.Size = new System.Drawing.Size(254, 26);
            this.editBoldNo.Text = "Сделать нежирным";
            this.editBoldNo.Click += new System.EventHandler(this.editBoldNo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(251, 6);
            // 
            // editItalisYes
            // 
            this.editItalisYes.Name = "editItalisYes";
            this.editItalisYes.Size = new System.Drawing.Size(254, 26);
            this.editItalisYes.Text = "Курсив";
            this.editItalisYes.Click += new System.EventHandler(this.editItalisYes_Click);
            // 
            // editItalicNo
            // 
            this.editItalicNo.Name = "editItalicNo";
            this.editItalicNo.Size = new System.Drawing.Size(254, 26);
            this.editItalicNo.Text = "Без курсива";
            this.editItalicNo.Click += new System.EventHandler(this.editItalicNo_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(251, 6);
            // 
            // editUndelineYes
            // 
            this.editUndelineYes.Name = "editUndelineYes";
            this.editUndelineYes.Size = new System.Drawing.Size(254, 26);
            this.editUndelineYes.Text = "Подчеркнуть";
            this.editUndelineYes.Click += new System.EventHandler(this.editUndelineYes_Click);
            // 
            // editUnderlineNo
            // 
            this.editUnderlineNo.Name = "editUnderlineNo";
            this.editUnderlineNo.Size = new System.Drawing.Size(254, 26);
            this.editUnderlineNo.Text = "Убрать подчеркивание";
            this.editUnderlineNo.Click += new System.EventHandler(this.editUnderlineNo_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(251, 6);
            // 
            // editStrikeOutYes
            // 
            this.editStrikeOutYes.Name = "editStrikeOutYes";
            this.editStrikeOutYes.Size = new System.Drawing.Size(254, 26);
            this.editStrikeOutYes.Text = "Зачеркнуть";
            this.editStrikeOutYes.Click += new System.EventHandler(this.editStrikeOutYes_Click);
            // 
            // editStrikeOutNo
            // 
            this.editStrikeOutNo.Name = "editStrikeOutNo";
            this.editStrikeOutNo.Size = new System.Drawing.Size(254, 26);
            this.editStrikeOutNo.Text = "Убрать зачеркивание";
            this.editStrikeOutNo.Click += new System.EventHandler(this.editStrikeOutNo_Click);
            // 
            // format
            // 
            this.format.Name = "format";
            this.format.Size = new System.Drawing.Size(77, 24);
            this.format.Text = "Формат";
            this.format.Click += new System.EventHandler(this.format_Click);
            // 
            // settings
            // 
            this.settings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsSaveFrequency,
            this.settingsColor});
            this.settings.Name = "settings";
            this.settings.Size = new System.Drawing.Size(98, 24);
            this.settings.Text = "Настройки";
            // 
            // settingsSaveFrequency
            // 
            this.settingsSaveFrequency.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsSaveFrequencyNo,
            this.settingsSaveFrequency1min,
            this.settingsSaveFrequency5min});
            this.settingsSaveFrequency.Name = "settingsSaveFrequency";
            this.settingsSaveFrequency.Size = new System.Drawing.Size(264, 26);
            this.settingsSaveFrequency.Text = "Частота автосохранения";
            // 
            // settingsSaveFrequencyNo
            // 
            this.settingsSaveFrequencyNo.Checked = true;
            this.settingsSaveFrequencyNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.settingsSaveFrequencyNo.Name = "settingsSaveFrequencyNo";
            this.settingsSaveFrequencyNo.Size = new System.Drawing.Size(234, 26);
            this.settingsSaveFrequencyNo.Text = "Без автосохранения";
            this.settingsSaveFrequencyNo.Click += new System.EventHandler(this.settingsSaveFrequencyNo_Click);
            // 
            // settingsSaveFrequency1min
            // 
            this.settingsSaveFrequency1min.Name = "settingsSaveFrequency1min";
            this.settingsSaveFrequency1min.Size = new System.Drawing.Size(234, 26);
            this.settingsSaveFrequency1min.Text = "1 минута";
            this.settingsSaveFrequency1min.Click += new System.EventHandler(this.settingsSaveFrequency1min_Click);
            // 
            // settingsSaveFrequency5min
            // 
            this.settingsSaveFrequency5min.Name = "settingsSaveFrequency5min";
            this.settingsSaveFrequency5min.Size = new System.Drawing.Size(234, 26);
            this.settingsSaveFrequency5min.Text = "5 минут";
            this.settingsSaveFrequency5min.Click += new System.EventHandler(this.settingsSaveFrequency5min_Click);
            // 
            // settingsColor
            // 
            this.settingsColor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorBeige,
            this.colorGreen,
            this.colorPurple,
            this.colorStandart});
            this.settingsColor.Name = "settingsColor";
            this.settingsColor.Size = new System.Drawing.Size(264, 26);
            this.settingsColor.Text = "Цветовая схема";
            // 
            // colorBeige
            // 
            this.colorBeige.Name = "colorBeige";
            this.colorBeige.Size = new System.Drawing.Size(180, 26);
            this.colorBeige.Text = "Бежевая";
            this.colorBeige.Click += new System.EventHandler(this.colorBeige_Click);
            // 
            // colorGreen
            // 
            this.colorGreen.Name = "colorGreen";
            this.colorGreen.Size = new System.Drawing.Size(180, 26);
            this.colorGreen.Text = "Зеленая";
            this.colorGreen.Click += new System.EventHandler(this.colorGreen_Click);
            // 
            // colorPurple
            // 
            this.colorPurple.Name = "colorPurple";
            this.colorPurple.Size = new System.Drawing.Size(180, 26);
            this.colorPurple.Text = "Фиолетовая";
            this.colorPurple.Click += new System.EventHandler(this.colorPurple_Click);
            // 
            // colorStandart
            // 
            this.colorStandart.Name = "colorStandart";
            this.colorStandart.Size = new System.Drawing.Size(180, 26);
            this.colorStandart.Text = "Стандартная";
            this.colorStandart.Click += new System.EventHandler(this.colorStandart_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // information
            // 
            this.information.Name = "information";
            this.information.Size = new System.Drawing.Size(81, 24);
            this.information.Text = "Справка";
            this.information.Click += new System.EventHandler(this.information_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem fileOpen;
        private System.Windows.Forms.ToolStripMenuItem fileSaveAs;
        private System.Windows.Forms.ToolStripMenuItem fileNewTab;
        private System.Windows.Forms.ToolStripMenuItem edit;
        private System.Windows.Forms.ToolStripMenuItem editBoldYes;
        private System.Windows.Forms.ToolStripMenuItem editBoldNo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem editItalisYes;
        private System.Windows.Forms.ToolStripMenuItem editItalicNo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem editUndelineYes;
        private System.Windows.Forms.ToolStripMenuItem editUnderlineNo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem editStrikeOutYes;
        private System.Windows.Forms.ToolStripMenuItem editStrikeOutNo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem format;
        private System.Windows.Forms.ToolStripMenuItem fileSave;
        private System.Windows.Forms.ToolStripMenuItem settings;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem settingsSaveFrequency;
        private System.Windows.Forms.ToolStripMenuItem settingsSaveFrequencyNo;
        private System.Windows.Forms.ToolStripMenuItem settingsSaveFrequency1min;
        private System.Windows.Forms.ToolStripMenuItem settingsSaveFrequency5min;
        private System.Windows.Forms.ToolStripMenuItem settingsColor;
        private System.Windows.Forms.ToolStripMenuItem colorBeige;
        private System.Windows.Forms.ToolStripMenuItem colorGreen;
        private System.Windows.Forms.ToolStripMenuItem colorPurple;
        private System.Windows.Forms.ToolStripMenuItem colorStandart;
        private System.Windows.Forms.ToolStripMenuItem fileCloseTab;
        private System.Windows.Forms.ToolStripMenuItem fileNewWindow;
        private System.Windows.Forms.ToolStripMenuItem information;
    }

    
}

