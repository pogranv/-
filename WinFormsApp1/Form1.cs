using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {

        /// <summary>
        /// Список полных путей до файлов открытого окна.
        /// </summary>
        List<string> mainCurrentFileNames;

        /// <summary>
        /// Количество текущих вкладок.
        /// </summary>
        int mainCountAllTabs = 0;

        /// <summary>
        /// Цвет фона текстового поля.
        /// </summary>
        Color mainRtbColor;
        /// <summary>
        /// Цвет заднего фона окна.
        /// </summary>
        Color mainBackColor;

        /// <summary>
        /// Инициализирует контектсное меню, вызываемое правой кнопкой мыши.
        /// </summary>
        void initContextMenu()
        {
            ToolStripMenuItem selectAllItem = new ToolStripMenuItem("Выбрать все");
            ToolStripMenuItem cutFragmentItem = new ToolStripMenuItem("Вырезать");
            ToolStripMenuItem copyFragmentItem = new ToolStripMenuItem("Копировать");
            ToolStripMenuItem pasteFragmentItem = new ToolStripMenuItem("Вставить");
            ToolStripMenuItem changeFormatItem = new ToolStripMenuItem("Формат");

            contextMenuStrip1.Items.AddRange(new[] { selectAllItem, cutFragmentItem, copyFragmentItem,
                pasteFragmentItem, changeFormatItem });

            selectAllItem.Click += selectAllItem_Click;
            cutFragmentItem.Click += cutFragmentItem_Click;
            copyFragmentItem.Click += copyFragmentItem_Click;
            pasteFragmentItem.Click += pasteFragmentItem_Click;
            changeFormatItem.Click += changeFormatItem_Click;
        }

        /// <summary>
        /// Инициализация таймера из сохраненных настроек.
        /// Настройка отображения галочки в меню настроек автосохранения.
        /// </summary>
        void initTimer()
        {
            timer1.Tick += new EventHandler(Timer1_Tick);

            timer1.Interval = Properties.Settings.Default.timerInterval;
            timer1.Enabled = Properties.Settings.Default.timerEnable;

            settingsSaveFrequencyNo.CheckState = System.Windows.Forms.CheckState.Unchecked;
            settingsSaveFrequency1min.CheckState = System.Windows.Forms.CheckState.Unchecked;
            settingsSaveFrequency5min.CheckState = System.Windows.Forms.CheckState.Unchecked;

            if (timer1.Enabled == false)
            {
                settingsSaveFrequencyNo.CheckState = System.Windows.Forms.CheckState.Checked;
            }
            if (timer1.Interval == 1000 * 60)
            {
                settingsSaveFrequency1min.CheckState = System.Windows.Forms.CheckState.Checked;
            }
            if (timer1.Interval == 1000 * 60 * 5)
            {
                settingsSaveFrequency5min.CheckState = System.Windows.Forms.CheckState.Checked;
            }
        }

        /// <summary>
        /// Открытие файлов, которые были открыты при закрытии приложения.
        /// Пути к файлам берутся из сохраненных настроек.
        /// </summary>
        void openLastFiles()
        {
            // Взятие из настроек пути прошлых файлов.
            var propListOfFiles = Properties.Settings.Default.fileNames;
            // Если файлов не было, то просто открыть пустую вкладку.
            if (propListOfFiles == null || propListOfFiles.Count == 0)
            {
                fileNewTab.PerformClick();
            }
            else
            {
                // Октрытие прошлых вкладок, которые были при закрытии приложения.
                for (int i = 0; i < propListOfFiles.Count; ++i)
                {
                    string needOpenFileName = propListOfFiles[i];
                    try
                    {
                        string readFile = File.ReadAllText(needOpenFileName);
                        // Открытие новой вкладки и в ней уже открытие файла в обоих случаях.
                        if (Path.GetExtension(needOpenFileName) == ".rtf")
                        {
                            fileNewTab.PerformClick();
                            // чтобы не вылетало, если файл пустой
                            if (string.IsNullOrEmpty(readFile))
                                // Обращение к текстовому полю активной вкладки.
                                tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                                    .First().Text = "";
                            else
                                tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                                    .First().LoadFile(needOpenFileName);
                        }
                        else
                        {
                            fileNewTab.PerformClick();
                            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                                .First().Text = readFile;
                        }
                        mainCurrentFileNames[^1] = needOpenFileName;
                        tabControl1.TabPages[tabControl1.SelectedIndex].Text = needOpenFileName.Split('\\')[^1];
                    }
                    // Если файл отсутствует, то ничего не делать.
                    catch (Exception ex) { }
                }
            }
        }

        /// <summary>
        /// Конструктор формы. Инициализирует все необходимые для
        /// дальнейшей работы приложения поля и компоненты.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            this.Text = "Notepad+";
            this.KeyPreview = true;
            this.BackColor = mainBackColor;
            mainRtbColor = Properties.Settings.Default.rtbColor;
            mainBackColor = Properties.Settings.Default.backColor;
            initContextMenu();

            KeyDown += new KeyEventHandler(Form_KeyDown);
            this.FormClosing += new FormClosingEventHandler(Form1_Closing);

            mainCurrentFileNames = new List<string>();

            initTimer();
            openLastFiles();

            // Изменение настроек текущих открытых файлов.
            Properties.Settings.Default.fileNames = new System.Collections.Specialized.StringCollection();
            for (int i = 0; i < mainCurrentFileNames.Count; ++i)
            {
                Properties.Settings.Default.fileNames.Add(mainCurrentFileNames[i]);
            }
        }

        /// <summary>
        /// Обработка события таймера.
        /// После того, как пройдет время, заданное в настройках,
        /// нужно, чтобы все файлы сохранились.
        /// </summary>
        /// <param name="Sender">Отправитель события.</param>
        /// <param name="e">Событие окончания времени таймера.</param>
        private void Timer1_Tick(object Sender, EventArgs e)
        {
            int tabPagesCount = tabControl1.TabPages.Count;
            timer1.Enabled = false;
            for (int i = 0; i < tabPagesCount; ++i)
            {
                try
                {
                    // Страховка на то, что вкладка существует.
                    if (mainCurrentFileNames == null || mainCurrentFileNames.Count <= i)
                        continue;
                    string fullPath = mainCurrentFileNames[i];
                    if (File.Exists(fullPath))
                    {
                        string ext = Path.GetExtension(fullPath);
                        if (ext == ".txt")
                        {
                            // Обращение к текстовому полю активной вкладки.
                            string text = tabControl1.TabPages[tabControl1.SelectedIndex].Controls
                                .OfType<RichTextBox>().First().Text;
                            // Сохранение текстового файла.
                            File.WriteAllText(fullPath, text);
                        }
                        else
                        {
                            // Сохранение файла rtf. 
                            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>().
                                First().SaveFile(fullPath, RichTextBoxStreamType.RichText);
                        }
                        tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                                .First().Modified = false;
                    }
                } 
                // Нет доступа к файлу, но пользователю об этом не сообщается,
                // чтобы каждый раз не вылетали сообщения.
                catch (Exception ex) {}
            }
            timer1.Enabled = true;
        }

        /// <summary>
        /// Обрабатывает событие нажатие на кнопку "Формат" в контекстном меню.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие нажатие на кнопку "Формат" в контекстном меню.</param>
        void changeFormatItem_Click(object sender, EventArgs e)
        {
            format.PerformClick();
        }

        /// <summary>
        /// Метод обрабатывает нажатие горячих клавиш.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие нажатия клавиши.</param>
        void Form_KeyDown(object sender, KeyEventArgs e)
        {
            // Создание новой вкладки.
            if (e.Control && e.KeyCode == Keys.T)
            {
                fileNewTab.PerformClick();
            }
            // Создание нового окна.
            if (e.Control && e.KeyCode == Keys.W)
            {
                Process.Start("WinFormsApp1.exe");
            }
            // Выход из приложения.
            if (e.Control && e.KeyCode == Keys.E)
            {
                this.Close();
            }
            // Проверка на наличие вкладок.
            if (tabControl1.TabPages == null || tabControl1.TabPages.Count == 0)
            {
                return;
            }
            // Сохранение активной вкладки.
            if (e.Control && e.KeyCode == Keys.S && !e.Shift)
            {
                fileSave.PerformClick();
            }
            // Сохранение всех откытых вкладок.
            if (e.Control && e.Shift && e.KeyCode == Keys.S)
            {
                int indexSelectedTab = tabControl1.SelectedIndex;
                int tabPagesCount = tabControl1.TabPages.Count;
                for (int i = 0; i < tabPagesCount; ++i)
                {
                    if (tabControl1.TabPages[i].Controls.OfType<RichTextBox>().
                        First().Modified)
                    {
                        tabControl1.SelectedIndex = i;
                        fileSave.PerformClick();
                    }
                }
                tabControl1.SelectedIndex = indexSelectedTab;
            }

        }

        /// <summary>
        /// Обрабатывает событие нажатие на кнопку "Вставить" в контекстном меню.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие нажатие на кнопку "Вставить" в контекстном меню.</param>
        private void pasteFragmentItem_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                .First().Paste();
        }

        /// <summary>
        /// Обрабатывает событие нажатие на кнопку "Копировать" в контекстном меню.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие нажатие на кнопку "Копировать" в контекстном меню.</param>
        private void copyFragmentItem_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                .First().Copy();
        }

        /// <summary>
        /// Обработка событие нажатие на кнопку "Копировать" в контекстном меню.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие нажатие на кнопку "Копировать" в контекстном меню.</param>
        private void selectAllItem_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                .First().SelectAll();
        }

        /// <summary>
        /// Обработка событие нажатие на кнопку "Вырезать" в контекстном меню.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие нажатие на кнопку "Вырезать" в контекстном меню.</param>
        private void cutFragmentItem_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                .First().Cut();
        }

        /// <summary>
        /// Обрабатывает событие нажатие на кнопку "Вырезать" в контекстном меню.
        /// Создает новую вкладку с текстовым полем.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие нажатие на кнопку "Новая вкладка" в меню.</param>
        private void fileNewTab_Click(object sender, EventArgs e)
        {
            // Инициализация текстового поля.
            int tabPagesCount = tabControl1.TabPages.Count;
            TabPage tPage = new TabPage($"Tab {mainCountAllTabs++}");
            tPage.BackColor = mainBackColor;
            RichTextBox richTextBox = new RichTextBox();
            richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)
                ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            richTextBox.Location = new System.Drawing.Point(10, 10);
            richTextBox.Name = "richTextBox";
            richTextBox.Size = new System.Drawing.Size(180, 80);
            richTextBox.TabIndex = 0;
            richTextBox.Text = "";
            richTextBox.WordWrap = false;
            richTextBox.BackColor = mainRtbColor;
            
            // Привязка контекстного меню.
            richTextBox.ContextMenuStrip = contextMenuStrip1;
            richTextBox.TextChanged += new System.EventHandler(richTextBox_TextChanged);

            // Добавление новой вкладки уже с текстовым полем.
            tPage.Controls.Add(richTextBox);
            tabControl1.TabPages.Add(tPage);
            tabControl1.SelectedIndex = tabPagesCount;
            mainCurrentFileNames.Add(tPage.Text);
        }

        /// <summary>
        /// Обрабатывает событие изменения содержимого текстового 
        /// поля активной вкладки.
        /// </summary>
        /// <param name="sendet">Отправитель.</param>
        /// <param name="e">Событие изменения содержимого текстового 
        /// поля активной вкладки.</param>
        private void richTextBox_TextChanged(object sendet, EventArgs e)
        {
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                .First().Modified = true;
        }


        /// <summary>
        /// Обработка событие нажатия на кнопку "Новая вкладка" в меню.
        /// Создание новой вкладки с текстовым полем.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие нажатия на кнопку "Новая вкладка" в меню.</param>
        private void fileOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                InitialDirectory =
                System.IO.Directory.GetCurrentDirectory()
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string readFile = File.ReadAllText(dialog.FileName);
                    if (Path.GetExtension(dialog.FileName) == ".rtf")
                    {
                        fileNewTab.PerformClick();
                        // Чтобы не вылетало, если файл пустой.
                        if (string.IsNullOrEmpty(readFile))
                            // Обращение к текстовому полю активной вкладки.
                            tabControl1.TabPages[tabControl1.SelectedIndex].Controls
                                .OfType<RichTextBox>().First().Text = "";
                        else
                            tabControl1.TabPages[tabControl1.SelectedIndex].Controls
                                .OfType<RichTextBox>().First().LoadFile(dialog.FileName);
                    }
                    else
                    {
                        fileNewTab.PerformClick();
                        tabControl1.TabPages[tabControl1.SelectedIndex].Controls
                            .OfType<RichTextBox>().First().Text = readFile;
                    }
                    // Добавление полного пути открытого файла.
                    mainCurrentFileNames[^1] = dialog.FileName;
                    tabControl1.TabPages[tabControl1.SelectedIndex].Text = 
                        dialog.FileName.Split('\\')[^1];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка чтения файла");
                }
            }
        }

        /// <summary>
        /// Обработка событие нажание на кнопку "Сделать жирным".
        /// Применения стиля жирности к выделенному тексту.
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="e">Событие нажание на кнопку "Сделать жирным".</param>
        private void editBoldYes_Click(object sender, EventArgs e)
        {
            // Если вкладок нет.
            if (tabControl1.TabPages == null || tabControl1.TabPages.Count == 0)
            {
                return;
            }
            // Формат текущего шрифта.
            var font = tabControl1.TabPages[tabControl1.SelectedIndex].Controls
                .OfType<RichTextBox>().First().SelectionFont;
            // Стили текущего шрифта.
            var styles = font.Style;
            // Добавление жирного стиля.
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                .First().SelectionFont = new Font(font, styles | FontStyle.Bold);
        }

        /// <summary>
        /// Обработка событие нажание на кнопку "Сделать нежирным".
        /// Удаление стиля жирности к выделенному тексту.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие нажание на кнопку "Сделать нежирным".</param>
        private void editBoldNo_Click(object sender, EventArgs e)
        {
            // Если вкладок нет.
            if (tabControl1.TabPages == null || tabControl1.TabPages.Count == 0)
            {
                return;
            }
            // Формат текущего шрифта.
            var font = tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                .First().SelectionFont;
            // Стили текущего шрифта.
            var styles = font.Style;
            // Удаление жирного стиля.
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                .First().SelectionFont = new Font(font, styles ^ FontStyle.Bold);
        }

        /// <summary>
        /// Обработка событие нажание на кнопку "Добавить курсив".
        /// Добавление курсива к выделенному стилю.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие нажание на кнопку "Добавить курсив".</param>
        private void editItalisYes_Click(object sender, EventArgs e)
        {
            // Если вкладок нет.
            if (tabControl1.TabPages == null || tabControl1.TabPages.Count == 0)
            {
                return;
            }
            // // Формат текущего шрифта.
            var font = tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                .First().SelectionFont;
            // Стили текущего шрифта.
            var styles = font.Style;
            // Применение курсива к выделенному тексту.
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                .First().SelectionFont = new Font(font, styles | FontStyle.Italic);
        }

        /// <summary>
        /// Обработка событие нажание на кнопку "Удалить курсив".
        /// Удаление курсива к выделенному стилю.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие нажание на кнопку "Удалить курсив".</param>
        private void editItalicNo_Click(object sender, EventArgs e)
        {
            // Если вкладок нет.
            if (tabControl1.TabPages == null || tabControl1.TabPages.Count == 0)
            {
                return;
            }
            // Стили текущего шрифта.
            var font = tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                .First().SelectionFont;
            // Стили текущего шрифта.
            var styles = font.Style;
            // Удаление курсива к выделенному тексту.
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                .First().SelectionFont = new Font(font, styles ^ FontStyle.Italic);
        }

        /// <summary>
        /// Обработка событие нажание на кнопку "Подчеркнуть".
        /// Применяет подчеркивание к выделенному тексту.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие нажание на кнопку "Подчеркнуть".</param>
        private void editUndelineYes_Click(object sender, EventArgs e)
        {
            // Если вкладок нет.
            if (tabControl1.TabPages == null || tabControl1.TabPages.Count == 0)
            {
                return;
            }
            // Стили текущего шрифта.
            var font = tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                .First().SelectionFont;
            // Стили текущего шрифта.
            var styles = font.Style;
            // Добавить подчеркивание к выделенному тексту.
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                .First().SelectionFont = new Font(font, styles | FontStyle.Underline);
        }

        /// <summary>
        /// Обработка событие нажание на кнопку "Удалить подчеркивание".
        /// Удаляет подчеркивание у выделенного текста.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие нажание на кнопку "Удалить подчеркивание".</param>
        private void editUnderlineNo_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages == null || tabControl1.TabPages.Count == 0)
            {
                return;
            }
            var font = tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                .First().SelectionFont;

            var styles = font.Style;
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                .First().SelectionFont = new Font(font, styles ^ FontStyle.Underline);
        }

        /// <summary>
        /// Обрабатывает событие нажание на кнопку "Зачеркнуть".
        /// Зачеркивает выделенный текст.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие нажание на кнопку "Зачеркнуть".</param>
        private void editStrikeOutYes_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages == null || tabControl1.TabPages.Count == 0)
            {
                return;
            }
            var font = tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                .First().SelectionFont;

            var styles = font.Style;
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                .First().SelectionFont = new Font(font, styles | FontStyle.Strikeout);
        }

        /// <summary>
        /// Обрабатывает событие нажание на кнопку "Удалить зачеркивание".
        /// Удаляет зачеркивание у выделенного текста.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие нажание на кнопку "Удалить зачеркивание".</param>
        private void editStrikeOutNo_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages == null || tabControl1.TabPages.Count == 0)
            {
                return;
            }
            var font = tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                .First().SelectionFont;

            var styles = font.Style;
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                .First().SelectionFont = new Font(font, styles ^ FontStyle.Strikeout);
        }

        /// <summary>
        /// Сохраняет файл, позволяя выбрать дирректорию.
        /// Выводит диалоговое окно сохранения.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие нажание на кнопку "Сохранить как...".</param>
        private void fileSaveAs_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages == null || tabControl1.TabPages.Count == 0)
            {
                return;
            }
            SaveFileDialog dialog = new SaveFileDialog()
            {
                InitialDirectory = System.IO.Directory.GetCurrentDirectory()
            };

            dialog.Filter = "txt files (*.txt)|*.txt|rft files (*.rtf)|*.rtf";
            string tabName = tabControl1.TabPages[tabControl1.SelectedIndex].Text;
            dialog.Title = $"Сохранение файла {tabName}";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string ext = Path.GetExtension(dialog.FileName);
                if (ext == ".txt")
                {
                    // Сохранение текстового файла.
                    string text = tabControl1.TabPages[tabControl1.SelectedIndex].Controls
                        .OfType<RichTextBox>().First().Text;
                    File.WriteAllText(dialog.FileName, text);
                }
                else
                {
                    // Сохранение файла rtf.
                    tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>().
                        First().SaveFile(dialog.FileName, RichTextBoxStreamType.RichText);
                }
                tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                        .First().Modified = false;
                // Название вкладки - название файла.
                tabControl1.TabPages[tabControl1.SelectedIndex].Text = dialog.FileName.Split('\\')[^1];
                // Добавление полного пути ко всем путям.
                mainCurrentFileNames[tabControl1.SelectedIndex] = dialog.FileName;
            }
        }

        /// <summary>
        /// Обрабатывает событие нажатия на кнопку "Сохранить".
        /// Сохраняет файл.
        /// Если файл был сохранен до этого, то просто обновляет.
        /// Иначе предлагает выбрать дирректорию сохранения.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие нажатия на кнопку "Сохранить".</param>
        private void fileSave_Click(object sender, EventArgs e)
        {
            // Если нет вкладок.
            if (tabControl1.TabPages == null || tabControl1.TabPages.Count == 0)
                return;
            int indexCurrentTab = tabControl1.SelectedIndex;
            // Если файл не был сохранен или открыт, то предлагаем выбрать дирректорию сохранения.
            if (mainCurrentFileNames == null || mainCurrentFileNames.Count <= indexCurrentTab)
            {
                fileSaveAs.PerformClick();
                return;
            }
            try
            {
                string fullPath = mainCurrentFileNames[indexCurrentTab];
                if (File.Exists(fullPath))
                {
                    // Обновление сохраненного ранее файла.
                    string ext = Path.GetExtension(fullPath);
                    if (ext == ".txt")
                    {
                        string text = tabControl1.TabPages[tabControl1.SelectedIndex].Controls
                            .OfType<RichTextBox>().First().Text;
                        File.WriteAllText(fullPath, text);
                    }
                    else
                    {
                        tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>().
                            First().SaveFile(fullPath, RichTextBoxStreamType.RichText);
                    }
                    tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                            .First().Modified = false;
                }
                else
                {
                    // Если файл не был сохранен или открыт, то предлагаем выбрать дирректорию сохранения.
                    fileSaveAs.PerformClick();
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка сохранения");
            }
        }

        /// <summary>
        /// Обработка событие выбора пункта "Автосохранения".
        /// Останавливает дейстиве таймера и ставит галочку на 
        /// выбранный пункт.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие выбора пункта "Автосохранения".</param>
        private void settingsSaveFrequencyNo_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            settingsSaveFrequencyNo.CheckState = System.Windows.Forms.CheckState.Checked;
            settingsSaveFrequency1min.CheckState = System.Windows.Forms.CheckState.Unchecked;
            settingsSaveFrequency5min.CheckState = System.Windows.Forms.CheckState.Unchecked;
        }


        /// <summary>
        /// Обработка событие выбора пункта "Автосохранения".
        /// Ставит интервал таймера на 1 минуту и ставит галочку на 
        /// выбранный пункт.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие выбора пункта "Автосохранения".</param>
        private void settingsSaveFrequency1min_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1000 * 60;
            settingsSaveFrequencyNo.CheckState = System.Windows.Forms.CheckState.Unchecked;
            settingsSaveFrequency1min.CheckState = System.Windows.Forms.CheckState.Checked;
            settingsSaveFrequency5min.CheckState = System.Windows.Forms.CheckState.Unchecked;
        }

        /// <summary>
        /// Обработка событие выбора пункта "Автосохранения".
        /// Ставит интервал таймера на 5 минут и ставит галочку на 
        /// выбранный пункт.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие выбора пункта "Автосохранения".</param>
        private void settingsSaveFrequency5min_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1000 * 60 * 5;
            settingsSaveFrequencyNo.CheckState = System.Windows.Forms.CheckState.Unchecked;
            settingsSaveFrequency1min.CheckState = System.Windows.Forms.CheckState.Unchecked;
            settingsSaveFrequency5min.CheckState = System.Windows.Forms.CheckState.Checked;
        }

        /// <summary>
        /// Установка цветовой схемы в бежевый цвет.
        /// Меняет цвета текстового поля и вкладки.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие выбора цветовой схемы.</param>
        private void colorBeige_Click(object sender, EventArgs e)
        {
            mainRtbColor = Color.FromArgb(255, 224, 192);
            mainBackColor = Color.FromArgb(255, 224, 192);
            this.BackColor = mainBackColor;
            for (int i = 0; i < tabControl1.TabPages.Count; ++i)
            {
                // Обращение к текстовому полю активной вкладки.
                tabControl1.TabPages[i].Controls.OfType<RichTextBox>().
                        First().BackColor = mainRtbColor;
                tabControl1.TabPages[i].BackColor = mainBackColor;
            }
        }

        /// <summary>
        /// Установка цветовой схемы в зеленый цвет.
        /// Меняет цвета текстового поля и вкладки.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие выбора цветовой схемы.</param>
        private void colorGreen_Click(object sender, EventArgs e)
        {
            mainRtbColor = Color.FromArgb(192, 255, 192);
            mainBackColor = Color.FromArgb(192, 255, 192);
            this.BackColor = mainBackColor;
            for (int i = 0; i < tabControl1.TabPages.Count; ++i)
            {
                // Обращение к текстовому полю активной вкладки.
                tabControl1.TabPages[i].Controls.OfType<RichTextBox>().
                        First().BackColor = mainRtbColor;
                tabControl1.TabPages[i].BackColor = mainBackColor;
            }
        }

        /// <summary>
        /// Установка цветовой схемы в фиолетовый цвет.
        /// Меняет цвета текстового поля и вкладки.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие выбора цветовой схемы.</param>
        private void colorPurple_Click(object sender, EventArgs e)
        {
            mainRtbColor = Color.FromArgb(192, 192, 255);
            mainBackColor = Color.FromArgb(192, 192, 255);
            this.BackColor = mainBackColor;
            for (int i = 0; i < tabControl1.TabPages.Count; ++i)
            {
                // Обращение к текстовому полю активной вкладки.
                tabControl1.TabPages[i].Controls.OfType<RichTextBox>().
                        First().BackColor = mainRtbColor;
                tabControl1.TabPages[i].BackColor = mainBackColor;
            }
        }

        /// <summary>
        /// Установка цветовой схемы в стандартный цвет.
        /// Меняет цвета текстового поля и вкладки.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие выбора цветовой схемы.</param>
        private void colorStandart_Click(object sender, EventArgs e)
        {
            mainRtbColor = System.Drawing.SystemColors.Control;
            mainBackColor = System.Drawing.SystemColors.Control;
            this.BackColor = mainBackColor;
            for (int i = 0; i < tabControl1.TabPages.Count; ++i)
            {
                // Обращение к текстовому полю активной вкладки.
                tabControl1.TabPages[i].Controls.OfType<RichTextBox>().
                        First().BackColor = mainRtbColor;
                tabControl1.TabPages[i].BackColor = mainBackColor;
            }
        }

        /// <summary>
        /// Обрабатывает событие закрытия формы.
        /// Проверяет все файлы на то, что они сохранены,
        /// и предлагает сохранить несохраненны изменения.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие закрытия формы.</param>
        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            bool needShowSaveWindow = false;
            try
            {
                // Проверка на то, что есть несохраненные файлы.
                for (int i = 0; i < tabControl1.TabPages.Count; ++i)
                    if (tabControl1.TabPages[i].Controls.OfType<RichTextBox>().
                            First().Modified)
                        needShowSaveWindow = true;

                if (needShowSaveWindow)
                {
                    DialogResult dialogResult = MessageBox.Show("Сохранить несохраненные изменения?", "Закрытие приложения", MessageBoxButtons.YesNoCancel);
                    if (dialogResult == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        return;
                    }
                    if (dialogResult == DialogResult.Yes)
                    {
                        // Сохранение всех вкладок.
                        int tabPagesCount = tabControl1.TabPages.Count;
                        for (int i = 0; i < tabPagesCount; ++i)
                        {
                            if (tabControl1.TabPages[i].Controls.OfType<RichTextBox>().
                                First().Modified)
                            {
                                tabControl1.SelectedIndex = i;
                                fileSave.PerformClick();
                            }
                        }
                    }
                }
                e.Cancel = false;
                saveSettings();
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Что-то пошло не так");
            }
        }

        /// <summary>
        /// Записывает все выставленные настройки окна в свойства проекта,
        /// чтобы при следующем открытии формы рнастройки были выставлены
        /// такими же.
        /// </summary>
        void saveSettings()
        {
            Properties.Settings.Default.fileNames = new System.Collections.Specialized.StringCollection();
            for (int i = 0; i < mainCurrentFileNames.Count; ++i)
                Properties.Settings.Default.fileNames.Add(mainCurrentFileNames[i]);

            Properties.Settings.Default.timerInterval = timer1.Interval;
            Properties.Settings.Default.rtbColor = mainRtbColor;
            Properties.Settings.Default.backColor = mainBackColor;
            Properties.Settings.Default.timerEnable = timer1.Enabled;
            Properties.Settings.Default.Save();
        }
        private void format_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages == null || tabControl1.TabPages.Count == 0)
            {
                return;
            }
            FontDialog myFontDialog = new FontDialog();
            if (myFontDialog.ShowDialog() == DialogResult.OK)
            {
                tabControl1.TabPages[tabControl1.SelectedIndex].Controls
                    .OfType<RichTextBox>().First().SelectAll();
                tabControl1.TabPages[tabControl1.SelectedIndex].Controls
                    .OfType<RichTextBox>().First().Font = myFontDialog.Font;
            }
        }

        /// <summary>
        /// Обработка событие нажатия на кнопку "Закрыть вкладку.
        /// Перед закрытием предлагает сохранить несохраненные 
        /// изменения в файле этой вкладке.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие нажатия на кнопку "Закрыть вкладку."</param>
        private void fileCloseTab_Click(object sender, EventArgs e)
        {
            // Если вкладок нет.
            if (tabControl1.TabPages == null || tabControl1.TabPages.Count == 0)
                return;
            int indexCurrentTab = tabControl1.SelectedIndex;
            // Окно спроса на сохранение изменений.
            DialogResult dialogResult = MessageBox.Show("Сохранить несохраненные изменения во вкладке?", "Закрытие вкладки", MessageBoxButtons.YesNoCancel);
            if (dialogResult == DialogResult.Cancel)
                return;
            try
            {
                // Если в файл вносились изменения, сохраняем его.
                if (dialogResult == DialogResult.Yes && tabControl1.TabPages[tabControl1.SelectedIndex]
                    .Controls.OfType<RichTextBox>().First().Modified == true)
                {
                    saveDialogWindow(mainCurrentFileNames[tabControl1.SelectedIndex]);
                }
                mainCurrentFileNames.RemoveAt(indexCurrentTab);
                tabControl1.TabPages.RemoveAt(indexCurrentTab);
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка закрытия вкладки.");
            }
        }

        /// <summary>
        /// Обработка сохранения файла.
        /// Если файл существует, то содержимое просто перезаписывается,
        /// иначе выводится диалоговое окно сохранения в дирректорию.
        /// </summary>
        /// <param name="fullPath">Полный путь до файла.</param>
        void saveDialogWindow(string fullPath)
        {
            if (File.Exists(fullPath))
            {
                // Обновление сохраненного ранее файла.
                string ext = Path.GetExtension(fullPath);
                if (ext == ".txt")
                {
                    string text = tabControl1.TabPages[tabControl1.SelectedIndex].Controls
                        .OfType<RichTextBox>().First().Text;
                    File.WriteAllText(fullPath, text);
                }
                else
                {
                    tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>().
                        First().SaveFile(fullPath, RichTextBoxStreamType.RichText);
                }
                tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                        .First().Modified = false;
            }
            else
            {
                // Если файл не был сохранен или открыт, то предлагаем выбрать дирректорию сохранения.
                SaveFileDialog dialog = new SaveFileDialog()
                {
                    InitialDirectory = System.IO.Directory.GetCurrentDirectory()
                };
                dialog.Filter = "txt files (*.txt)|*.txt|rft files (*.rtf)|*.rtf";
                string tabName = tabControl1.TabPages[tabControl1.SelectedIndex].Text;
                dialog.Title = $"Сохранение файла {tabName}";
                // Диалоговое окно сохранения файла.
                var dialogShow = dialog.ShowDialog();
                if (dialogShow == DialogResult.Cancel)
                    return;
                if (dialogShow == DialogResult.OK)
                    saveText(dialog.FileName);
            }
        }

        /// <summary>
        /// Сохранение файла по переданному полному пути.
        /// </summary>
        /// <param name="fullFileName">Полный путь до файла, который должен быть
        /// сохранен.</param>
        void saveText(string fullFileName)
        {
            string ext = Path.GetExtension(fullFileName);
            if (ext == ".txt")
            {
                // Обращение к текстовому полю активной вкладки.
                string text = tabControl1.TabPages[tabControl1.SelectedIndex].Controls
                    .OfType<RichTextBox>().First().Text;
                // Сохранение текстового файла.
                File.WriteAllText(fullFileName, text);
            }
            else
            {
                // Сохранение файла rtf.
                tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>().
                    First().SaveFile(fullFileName, RichTextBoxStreamType.RichText);
            }

            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.OfType<RichTextBox>()
                    .First().Modified = false;

            tabControl1.TabPages[tabControl1.SelectedIndex].Text = fullFileName.Split('\\')[^1];
            mainCurrentFileNames.Add(fullFileName);
        }

        /// <summary>
        /// Открывает новое окно приложения.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие нажатия на кнопку "Новое окно".</param>
        private void fileNewWindow_Click(object sender, EventArgs e)
        {
            Process.Start("WinFormsApp1.exe");
        }

        /// <summary>
        /// Выводит информацию о горячих клавишах.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Событие нажатия на кнопку "Справка".</param>
        private void information_Click(object sender, EventArgs e)
        {
            string inf = @"Ctrl + T - Создание новой вкладки
Ctrl + W - Создание нового окна
Ctrl + E - Закрытие окна
Ctrl + S - Сохранение текущей вкладки
Ctrl + Shift + S - Сохранение всех вкладок";
            MessageBox.Show(inf, "Горячие клавиши");
        }
    }
}
