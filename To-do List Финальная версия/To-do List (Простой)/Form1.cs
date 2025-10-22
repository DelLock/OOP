using System.Text.Json;
using System.IO;
using System.Drawing;

namespace To_do_List__Простой_
{
    public partial class Form1 : Form
    {
        private List<TaskItem> tasks = new List<TaskItem>();
        private System.Windows.Forms.Timer timer;

        private bool[] taskCompleted = new bool[5] { false, false, false, false, false };
        private bool[] taskRead = new bool[5] { false, false, false, false, false };
        private DateTime[] taskCreated = new DateTime[5];
        private DateTime?[] taskCompletedDate = new DateTime?[5];

        private Button[] okButtons;
        private Button[] editButtons;
        private TextBox[] taskTextBoxes;
        private Button[] doneButtons;
        private Button[] deleteButtons;
        private Panel[] taskPanels;
        private Panel[] timePanels;
        private Label[] timeLabels;
        private Label[] readLabels;

        private string saveFilePath = "tasks.json";

        private Color primaryColor = Color.FromArgb(41, 128, 185);
        private Color secondaryColor = Color.FromArgb(52, 152, 219);
        private Color accentColor = Color.FromArgb(46, 204, 113);
        private Color dangerColor = Color.FromArgb(231, 76, 60);
        private Color backgroundColor = Color.FromArgb(236, 240, 241);

        private TableLayoutPanel mainLayout;

        public Form1()
        {
            InitializeComponent();
            InitializeArrays();
            SetupMainLayout();
            ApplyStyling();
            LoadTasks();
            InitializeTimer();
            InitializeEventHandlers();
            InitializeButtonsVisibility();
        }

        private void SetupMainLayout()
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoSize = false;
            this.MinimumSize = new Size(900, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "To-Do List";
            this.Padding = new Padding(20);

            mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.ColumnCount = 2;
            mainLayout.RowCount = 7;
            mainLayout.Padding = new Padding(10);

            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
            for (int i = 0; i < 5; i++)
            {
                mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }

            this.Controls.Add(mainLayout);

            label1.TextAlign = ContentAlignment.MiddleCenter;
            mainLayout.Controls.Add(label1, 0, 0);
            mainLayout.SetColumnSpan(label1, 2);

            mainLayout.Controls.Add(panel0, 0, 1);
            mainLayout.SetColumnSpan(panel0, 2);

            for (int i = 0; i < 5; i++)
            {
                mainLayout.Controls.Add(taskPanels[i], 0, i + 2);

                Panel sidePanel = new Panel();
                sidePanel.Dock = DockStyle.Fill;
                sidePanel.Controls.Add(readLabels[i]);
                sidePanel.Controls.Add(timePanels[i]);

                readLabels[i].Dock = DockStyle.Top;
                readLabels[i].Height = 25;
                timePanels[i].Dock = DockStyle.Bottom;
                timePanels[i].Height = 25;

                mainLayout.Controls.Add(sidePanel, 1, i + 2);
            }
        }

        private void ApplyStyling()
        {
            this.BackColor = backgroundColor;

            label1.ForeColor = primaryColor;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);

            panel0.BackColor = Color.White;
            panel0.BorderStyle = BorderStyle.FixedSingle;

            button1.BackColor = accentColor;
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            textNewTask.BorderStyle = BorderStyle.FixedSingle;
            textNewTask.BackColor = Color.White;

            foreach (var panel in taskPanels)
            {
                if (panel != null)
                {
                    panel.BackColor = Color.White;
                    panel.BorderStyle = BorderStyle.FixedSingle;
                }
            }

            StyleButtons();
        }

        private void StyleButtons()
        {
            foreach (var button in editButtons)
            {
                button.BackColor = secondaryColor;
                button.ForeColor = Color.White;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.Font = new Font("Segoe UI", 9F);
            }

            foreach (var button in okButtons)
            {
                button.BackColor = accentColor;
                button.ForeColor = Color.White;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.Font = new Font("Segoe UI", 9F);
            }

            foreach (var button in doneButtons)
            {
                button.BackColor = accentColor;
                button.ForeColor = Color.White;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.Font = new Font("Segoe UI", 9F);
            }

            foreach (var button in deleteButtons)
            {
                button.BackColor = dangerColor;
                button.ForeColor = Color.White;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.Font = new Font("Segoe UI", 9F);
            }
        }

        private void InitializeArrays()
        {
            okButtons = new Button[] { ok1, ok2, ok3, ok4, ok5 };
            editButtons = new Button[] { edit1, edit2, edit3, edit4, edit5 };
            taskTextBoxes = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5 };
            doneButtons = new Button[] { done1, done2, done3, done4, done5 };
            deleteButtons = new Button[] { delete1, delete2, delete3, delete4, delete5 };

            taskPanels = new Panel[] { panel1, panel2, panel3, panel4, panel5 };
            timePanels = new Panel[] { timePanel1, timePanel2, timePanel3, timePanel4, timePanel5 };
            timeLabels = new Label[] { timeLabel1, timeLabel2, timeLabel3, timeLabel4, timeLabel5 };
            readLabels = new Label[] { readLabel1, readLabel2, readLabel3, readLabel4, readLabel5 };
        }

        private TextBox GetTaskTextBox(int taskIndex)
        {
            if (taskIndex >= 0 && taskIndex < taskTextBoxes.Length)
            {
                return taskTextBoxes[taskIndex];
            }
            return null;
        }

        private Panel GetTimePanel(int taskNumber)
        {
            int index = taskNumber - 1;
            if (index >= 0 && index < timePanels.Length)
            {
                return timePanels[index];
            }
            return null;
        }

        private Label GetTimeLabel(int taskNumber)
        {
            int index = taskNumber - 1;
            if (index >= 0 && index < timeLabels.Length)
            {
                return timeLabels[index];
            }
            return null;
        }

        private Label GetReadLabel(int taskNumber)
        {
            int index = taskNumber - 1;
            if (index >= 0 && index < readLabels.Length)
            {
                return readLabels[index];
            }
            return null;
        }

        private Panel GetTaskPanel(int taskNumber)
        {
            int index = taskNumber - 1;
            if (index >= 0 && index < taskPanels.Length)
            {
                return taskPanels[index];
            }
            return null;
        }

        private void InitializeTasks()
        {
            for (int i = 0; i < 5; i++)
            {
                taskCreated[i] = DateTime.Now;
            }
        }

        private void InitializeTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void InitializeButtonsVisibility()
        {
            for (int i = 0; i < 5; i++)
            {
                okButtons[i].Visible = false;
                editButtons[i].Visible = !string.IsNullOrEmpty(taskTextBoxes[i].Text) && !taskCompleted[i];
                doneButtons[i].Visible = !string.IsNullOrEmpty(taskTextBoxes[i].Text) && !taskCompleted[i];
            }
        }

        public class SavedTask
        {
            public string Text { get; set; } = string.Empty;
            public bool Completed { get; set; }
            public bool Read { get; set; }
            public DateTime Created { get; set; }
            public DateTime? CompletedDate { get; set; }
            public bool OkButtonVisible { get; set; }
            public bool EditButtonVisible { get; set; } = true;
            public bool DoneButtonVisible { get; set; } = true;
        }

        private void SaveTasks()
        {
            try
            {
                var tasksToSave = new List<SavedTask>();

                for (int i = 0; i < 5; i++)
                {
                    tasksToSave.Add(new SavedTask
                    {
                        Text = taskTextBoxes[i].Text,
                        Completed = taskCompleted[i],
                        Read = taskRead[i],
                        Created = taskCreated[i],
                        CompletedDate = taskCompletedDate[i],
                        OkButtonVisible = okButtons[i].Visible,
                        EditButtonVisible = editButtons[i].Visible,
                        DoneButtonVisible = doneButtons[i].Visible
                    });
                }

                string json = JsonSerializer.Serialize(tasksToSave, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText(saveFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении задач: {ex.Message}");
            }
        }

        private void LoadTasks()
        {
            try
            {
                if (File.Exists(saveFilePath))
                {
                    string json = File.ReadAllText(saveFilePath);
                    var loadedTasks = JsonSerializer.Deserialize<List<SavedTask>>(json);

                    if (loadedTasks != null && loadedTasks.Count == 5)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            var task = loadedTasks[i];
                            taskTextBoxes[i].Text = task.Text;
                            taskCompleted[i] = task.Completed;
                            taskRead[i] = task.Read;
                            taskCreated[i] = task.Created;
                            taskCompletedDate[i] = task.CompletedDate;
                            okButtons[i].Visible = task.OkButtonVisible;
                            editButtons[i].Visible = task.EditButtonVisible && !task.Completed;
                            doneButtons[i].Visible = task.DoneButtonVisible && !task.Completed;

                            if (!string.IsNullOrEmpty(task.Text))
                            {
                                GetTaskPanel(i + 1).Visible = true;
                                GetTimeLabel(i + 1).Visible = true;
                                GetReadLabel(i + 1).Visible = true;
                                UpdateTimeDisplay(i + 1);
                                UpdateReadStatus(i + 1, task.Read);

                                if (task.Completed)
                                {
                                    UpdateCompleteDisplay(i + 1);
                                }
                            }
                        }
                        return;
                    }
                }

                InitializeTasks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке задач: {ex.Message}");
                InitializeTasks();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            SaveTasks();
        }

        private void InitializeEventHandlers()
        {
            done1.Click += (s, e) => CompleteTask(0);
            done2.Click += (s, e) => CompleteTask(1);
            done3.Click += (s, e) => CompleteTask(2);
            done4.Click += (s, e) => CompleteTask(3);
            done5.Click += (s, e) => CompleteTask(4);

            delete1.Click += (s, e) => DeleteTask(0);
            delete2.Click += (s, e) => DeleteTask(1);
            delete3.Click += (s, e) => DeleteTask(2);
            delete4.Click += (s, e) => DeleteTask(3);
            delete5.Click += (s, e) => DeleteTask(4);

            textBox1.Click += (s, e) => MarkAsRead(0);
            textBox2.Click += (s, e) => MarkAsRead(1);
            textBox3.Click += (s, e) => MarkAsRead(2);
            textBox4.Click += (s, e) => MarkAsRead(3);
            textBox5.Click += (s, e) => MarkAsRead(4);

            edit1.Click += (s, e) => StartEditing(0);
            edit2.Click += (s, e) => StartEditing(1);
            edit3.Click += (s, e) => StartEditing(2);
            edit4.Click += (s, e) => StartEditing(3);
            edit5.Click += (s, e) => StartEditing(4);

            ok1.Click += (s, e) => FinishEditing(0);
            ok2.Click += (s, e) => FinishEditing(1);
            ok3.Click += (s, e) => FinishEditing(2);
            ok4.Click += (s, e) => FinishEditing(3);
            ok5.Click += (s, e) => FinishEditing(4);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                if (!string.IsNullOrEmpty(taskTextBoxes[i].Text) && !taskCompleted[i])
                {
                    UpdateTimeDisplay(i + 1);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textNewTask.Text))
            {
                MessageBox.Show("Введите текст задачи!");
                return;
            }

            for (int i = 0; i < 5; i++)
            {
                TextBox currentTextBox = taskTextBoxes[i];

                if (string.IsNullOrEmpty(currentTextBox.Text) || taskCompleted[i])
                {
                    currentTextBox.Text = textNewTask.Text;
                    GetTaskPanel(i + 1).Visible = true;
                    GetTimeLabel(i + 1).Visible = true;
                    GetReadLabel(i + 1).Visible = true;

                    taskCompleted[i] = false;
                    taskRead[i] = false;
                    taskCreated[i] = DateTime.Now;
                    taskCompletedDate[i] = null;

                    UpdateTimeDisplay(i + 1);
                    UpdateReadStatus(i + 1, false);

                    editButtons[i].Visible = true;
                    okButtons[i].Visible = false;
                    doneButtons[i].Visible = true;

                    currentTextBox.BackColor = Color.White;
                    currentTextBox.ForeColor = Color.Black;
                    currentTextBox.Font = new Font(currentTextBox.Font, FontStyle.Regular);

                    textNewTask.Text = "";
                    SaveTasks();
                    return;
                }
            }

            MessageBox.Show("Все задачи заняты! Завершите или удалите некоторые задачи.");
        }

        private void UpdateTimeDisplay(int taskNumber)
        {
            int index = taskNumber - 1;
            Panel timePanel = GetTimePanel(taskNumber);
            Label timeLabel = GetTimeLabel(taskNumber);

            if (timePanel != null && timeLabel != null)
            {
                if (taskCompleted[index] && taskCompletedDate[index].HasValue)
                {
                    TimeSpan timeSpent = taskCompletedDate[index].Value - taskCreated[index];
                    timeLabel.Text = $"Выполнено за: {timeSpent:hh\\:mm\\:ss}";
                    timeLabel.ForeColor = Color.Green;
                }
                else if (!string.IsNullOrEmpty(taskTextBoxes[index].Text))
                {
                    TimeSpan timeElapsed = DateTime.Now - taskCreated[index];
                    timeLabel.Text = $"В процессе: {timeElapsed:hh\\:mm\\:ss}";
                    timeLabel.ForeColor = Color.Blue;
                }

                timePanel.Visible = !string.IsNullOrEmpty(taskTextBoxes[index].Text);
            }
        }

        private void UpdateReadStatus(int taskNumber, bool isRead)
        {
            int index = taskNumber - 1;
            Label readLabel = GetReadLabel(taskNumber);

            if (readLabel != null)
            {
                taskRead[index] = isRead;
                readLabel.Text = isRead ? "✓ Прочитано" : "✗ Не прочитано";
                readLabel.ForeColor = isRead ? Color.Green : Color.Red;
            }
        }

        private void UpdateCompleteDisplay(int taskNumber)
        {
            int index = taskNumber - 1;
            Label readLabel = GetReadLabel(taskNumber);
            TextBox taskTextBox = GetTaskTextBox(index);

            if (readLabel != null)
            {
                readLabel.Text = "✅ Выполнено";
                readLabel.ForeColor = Color.Green;
            }

            if (taskTextBox != null)
            {
                taskTextBox.BackColor = Color.FromArgb(240, 255, 240);
                taskTextBox.ForeColor = Color.DarkGreen;
                taskTextBox.Font = new Font(taskTextBox.Font, FontStyle.Strikeout);
            }
        }

        private void CompleteTask(int taskIndex)
        {
            taskCompleted[taskIndex] = true;
            taskCompletedDate[taskIndex] = DateTime.Now;
            UpdateTimeDisplay(taskIndex + 1);
            UpdateCompleteDisplay(taskIndex + 1);

            editButtons[taskIndex].Visible = false;
            okButtons[taskIndex].Visible = false;
            doneButtons[taskIndex].Visible = false;

            SaveTasks();
        }

        private void DeleteTask(int taskIndex)
        {
            bool[] tempCompleted = new bool[5];
            bool[] tempRead = new bool[5];
            DateTime[] tempCreated = new DateTime[5];
            DateTime?[] tempCompletedDate = new DateTime?[5];
            bool[] tempOkVisible = new bool[5];
            bool[] tempEditVisible = new bool[5];
            bool[] tempDoneVisible = new bool[5];

            for (int i = 0; i < 5; i++)
            {
                tempCompleted[i] = taskCompleted[i];
                tempRead[i] = taskRead[i];
                tempCreated[i] = taskCreated[i];
                tempCompletedDate[i] = taskCompletedDate[i];
                tempOkVisible[i] = okButtons[i].Visible;
                tempEditVisible[i] = editButtons[i].Visible;
                tempDoneVisible[i] = doneButtons[i].Visible;
            }

            for (int i = taskIndex; i < 4; i++)
            {
                taskTextBoxes[i].Text = taskTextBoxes[i + 1].Text;
                taskCompleted[i] = tempCompleted[i + 1];
                taskRead[i] = tempRead[i + 1];
                taskCreated[i] = tempCreated[i + 1];
                taskCompletedDate[i] = tempCompletedDate[i + 1];
                okButtons[i].Visible = tempOkVisible[i + 1];
                editButtons[i].Visible = tempEditVisible[i + 1] && !tempCompleted[i + 1];
                doneButtons[i].Visible = tempDoneVisible[i + 1] && !tempCompleted[i + 1];
            }

            taskTextBoxes[4].Text = "";
            taskCompleted[4] = false;
            taskRead[4] = false;
            taskCreated[4] = DateTime.Now;
            taskCompletedDate[4] = null;
            okButtons[4].Visible = false;
            editButtons[4].Visible = false;
            doneButtons[4].Visible = false;

            taskTextBoxes[4].BackColor = Color.White;
            taskTextBoxes[4].ForeColor = Color.Black;
            taskTextBoxes[4].Font = new Font(taskTextBoxes[4].Font, FontStyle.Regular);

            for (int i = 1; i <= 5; i++)
            {
                UpdateTimeDisplay(i);
                UpdateReadStatus(i, taskRead[i - 1]);

                if (taskCompleted[i - 1])
                {
                    UpdateCompleteDisplay(i);
                }
            }

            SaveTasks();
        }

        private void MarkAsRead(int taskIndex)
        {
            if (!string.IsNullOrEmpty(taskTextBoxes[taskIndex].Text))
            {
                UpdateReadStatus(taskIndex + 1, true);
                SaveTasks();
            }
        }

        private void StartEditing(int taskIndex)
        {
            editButtons[taskIndex].Visible = false;
            okButtons[taskIndex].Visible = true;
            taskTextBoxes[taskIndex].ReadOnly = false;

            taskTextBoxes[taskIndex].BackColor = Color.White;
            taskTextBoxes[taskIndex].ForeColor = Color.Black;
            taskTextBoxes[taskIndex].Font = new Font(taskTextBoxes[taskIndex].Font, FontStyle.Regular);
        }

        private void FinishEditing(int taskIndex)
        {
            okButtons[taskIndex].Visible = false;
            editButtons[taskIndex].Visible = !taskCompleted[taskIndex];
            taskTextBoxes[taskIndex].ReadOnly = true;

            if (taskCompleted[taskIndex])
            {
                UpdateCompleteDisplay(taskIndex + 1);
            }
            SaveTasks();
        }
    }
}