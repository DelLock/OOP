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

        // Массивы для хранения ссылок на кнопки
        private Button[] okButtons;
        private Button[] editButtons;
        private TextBox[] taskTextBoxes;
        private Button[] doneButtons;
        private Button[] deleteButtons;
        private Panel[] taskPanels;
        private Panel[] timePanels;
        private Label[] timeLabels;
        private Label[] readLabels;

        // Путь к файлу сохранения
        private string saveFilePath = "tasks.json";

        // Разрешение окна
        private Size formSize = new Size(1080, 720);

        public Form1()
        {
            InitializeComponent();
            InitializeArrays();
            SetFormResolution();
            LoadTasks();
            InitializeTimer();
            InitializeEventHandlers();
            InitializeButtonsVisibility();
            SetupAutoScale();
        }

        // Установка разрешения формы
        private void SetFormResolution()
        {
            this.Size = formSize;
            this.MinimumSize = formSize;
            this.MaximumSize = formSize;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "To-Do List - " + formSize.Width + "x" + formSize.Height;
        }

        // Настройка автоматического масштабирования
        private void SetupAutoScale()
        {
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = false;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            // Устанавливаем якоря для основных элементов
            SetAnchorsForControls();
        }

        // Установка якорей для элементов управления
        private void SetAnchorsForControls()
        {
            // Основные элементы - привязываем к верхнему левому углу
            textNewTask.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // Панели задач - привязываем к верхнему левому углу и растягиваем по ширине
            for (int i = 0; i < 5; i++)
            {
                if (taskPanels[i] != null)
                {
                    taskPanels[i].Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                }

                if (taskTextBoxes[i] != null)
                {
                    taskTextBoxes[i].Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                }

                if (timePanels[i] != null)
                {
                    timePanels[i].Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                }
            }

            // Устанавливаем фиксированные размеры для кнопок
            foreach (var button in okButtons)
            {
                button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                button.Size = new Size(80, 25);
            }

            foreach (var button in editButtons)
            {
                button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                button.Size = new Size(80, 25);
            }

            foreach (var button in doneButtons)
            {
                button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                button.Size = new Size(80, 25);
            }

            foreach (var button in deleteButtons)
            {
                button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                button.Size = new Size(80, 25);
            }

            // Устанавливаем фиксированные размеры для текстовых полей
            foreach (var textBox in taskTextBoxes)
            {
                textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                textBox.Size = new Size(400, 20);
            }

            // Устанавливаем фиксированные размеры для меток
            for (int i = 0; i < 5; i++)
            {
                if (readLabels[i] != null)
                {
                    readLabels[i].Anchor = AnchorStyles.Top | AnchorStyles.Right;
                    readLabels[i].Size = new Size(100, 20);
                }

                if (timeLabels[i] != null)
                {
                    timeLabels[i].Anchor = AnchorStyles.Top | AnchorStyles.Right;
                    timeLabels[i].Size = new Size(150, 20);
                }
            }
        }

        private void InitializeArrays()
        {
            // Инициализируем массивы с ссылками на элементы управления
            okButtons = new Button[] { ok1, ok2, ok3, ok4, ok5 };
            editButtons = new Button[] { edit1, edit2, edit3, edit4, edit5 };
            taskTextBoxes = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5 };
            doneButtons = new Button[] { done1, done2, done3, done4, done5 };
            deleteButtons = new Button[] { delete1, delete2, delete3, delete4, delete5 };

            // Инициализируем массивы для панелей и меток
            taskPanels = new Panel[] { panel1, panel2, panel3, panel4, panel5 };
            timePanels = new Panel[] { timePanel1, timePanel2, timePanel3, timePanel4, timePanel5 };
            timeLabels = new Label[] { timeLabel1, timeLabel2, timeLabel3, timeLabel4, timeLabel5 };
            readLabels = new Label[] { readLabel1, readLabel2, readLabel3, readLabel4, readLabel5 };
        }

        // Метод для получения текстового поля задачи по индексу
        private TextBox GetTaskTextBox(int taskIndex)
        {
            if (taskIndex >= 0 && taskIndex < taskTextBoxes.Length)
            {
                return taskTextBoxes[taskIndex];
            }
            return null;
        }

        // Метод для получения панели времени по номеру задачи
        private Panel GetTimePanel(int taskNumber)
        {
            int index = taskNumber - 1;
            if (index >= 0 && index < timePanels.Length)
            {
                return timePanels[index];
            }
            return null;
        }

        // Метод для получения метки времени по номеру задачи
        private Label GetTimeLabel(int taskNumber)
        {
            int index = taskNumber - 1;
            if (index >= 0 && index < timeLabels.Length)
            {
                return timeLabels[index];
            }
            return null;
        }

        // Метод для получения метки статуса прочтения по номеру задачи
        private Label GetReadLabel(int taskNumber)
        {
            int index = taskNumber - 1;
            if (index >= 0 && index < readLabels.Length)
            {
                return readLabels[index];
            }
            return null;
        }

        // Метод для получения панели задачи по номеру задачи
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
                // Кнопка "Готово" всегда скрыта изначально
                okButtons[i].Visible = false;

                // Кнопка "Редактировать" видна только если есть текст и задача не завершена
                editButtons[i].Visible = !string.IsNullOrEmpty(taskTextBoxes[i].Text) && !taskCompleted[i];
            }
        }

        // Класс для сохранения данных задачи
        public class SavedTask
        {
            public string Text { get; set; } = string.Empty;
            public bool Completed { get; set; }
            public bool Read { get; set; }
            public DateTime Created { get; set; }
            public DateTime? CompletedDate { get; set; }
            public bool OkButtonVisible { get; set; }
            public bool EditButtonVisible { get; set; } = true;
        }

        // Сохранение задач в файл
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
                        EditButtonVisible = editButtons[i].Visible
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

        // Загрузка задач из файла
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

                            // Обновляем отображение для загруженных задач
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

        // Сохранение при закрытии формы
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
                readLabel.Text = isRead ? "✓ Прочитано" : "✗ Не выполнено";
                readLabel.ForeColor = isRead ? Color.Green : Color.Red;
            }
        }

        private void UpdateCompleteDisplay(int taskNumber)
        {
            int index = taskNumber - 1;
            Label readLabel = GetReadLabel(taskNumber);

            if (readLabel != null)
            {
                readLabel.Text = "✅ Выполнено";
                readLabel.ForeColor = Color.Green;
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

            for (int i = 0; i < 5; i++)
            {
                tempCompleted[i] = taskCompleted[i];
                tempRead[i] = taskRead[i];
                tempCreated[i] = taskCreated[i];
                tempCompletedDate[i] = taskCompletedDate[i];
                tempOkVisible[i] = okButtons[i].Visible;
                tempEditVisible[i] = editButtons[i].Visible;
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
            }

            taskTextBoxes[4].Text = "";
            taskCompleted[4] = false;
            taskRead[4] = false;
            taskCreated[4] = DateTime.Now;
            taskCompletedDate[4] = null;
            okButtons[4].Visible = false;
            editButtons[4].Visible = false;

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
        }

        private void FinishEditing(int taskIndex)
        {
            okButtons[taskIndex].Visible = false;
            editButtons[taskIndex].Visible = !taskCompleted[taskIndex];
            taskTextBoxes[taskIndex].ReadOnly = true;
            SaveTasks();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void delete3_Click(object sender, EventArgs e)
        {

        }

        private void done2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}