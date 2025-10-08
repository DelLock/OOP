namespace To_do_List__Простой_
{
    public partial class Form1 : Form
    {
        private List<TaskItem> tasks = new List<TaskItem>();
        private System.Windows.Forms.Timer timer;

        // Переменные для отслеживания состояния задач
        private bool[] taskCompleted = new bool[5] { false, false, false, false, false };
        private bool[] taskRead = new bool[5] { false, false, false, false, false };
        private DateTime[] taskCreated = new DateTime[5];
        private DateTime?[] taskCompletedDate = new DateTime?[5];

        public Form1()
        {
            InitializeComponent();
            InitializeTasks();
            InitializeTimer();
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

        private void Timer_Tick(object sender, EventArgs e)
        {

            for (int i = 0; i < 5; i++)
            {
                if (!string.IsNullOrEmpty(GetTaskTextBox(i).Text) && !taskCompleted[i])
                {
                    UpdateTimeDisplay(i + 1);
                }
            }
        }


        private TextBox? GetTaskTextBox(int taskIndex)
        {
            return Controls.Find($"textBox{taskIndex + 1}", true).FirstOrDefault() as TextBox;
        }

        private Panel? GetTimePanel(int taskNumber)
        {
            return Controls.Find($"timePanel{taskNumber}", true).FirstOrDefault() as Panel;
        }

        private Label? GetTimeLabel(int taskNumber)
        {
            return Controls.Find($"timeLabel{taskNumber}", true).FirstOrDefault() as Label;
        }

        private Label? GetReadLabel(int taskNumber)
        {
            return Controls.Find($"readLabel{taskNumber}", true).FirstOrDefault() as Label;
        }

        private Panel? GetTaskPanel(int taskNumber)
        {
            return Controls.Find($"panel{taskNumber}", true).FirstOrDefault() as Panel;
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
                TextBox currentTextBox = GetTaskTextBox(i);

                if (string.IsNullOrEmpty(currentTextBox.Text) || taskCompleted[i])
                {

                    currentTextBox.Text = textNewTask.Text;
                    GetTaskPanel(i + 1).Visible = true;
                    GetTimeLabel(i + 1).Visible = true;


                    taskCompleted[i] = false;
                    taskRead[i] = false;
                    taskCreated[i] = DateTime.Now;
                    taskCompletedDate[i] = null;


                    UpdateTimeDisplay(i + 1);
                    UpdateReadStatus(i + 1, false);

                    textNewTask.Text = "";
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
                else if (!string.IsNullOrEmpty(GetTaskTextBox(index).Text))
                {

                    TimeSpan timeElapsed = DateTime.Now - taskCreated[index];
                    timeLabel.Text = $"В процессе: {timeElapsed:hh\\:mm\\:ss}";
                    timeLabel.ForeColor = Color.Blue;
                }

                timePanel.Visible = !string.IsNullOrEmpty(GetTaskTextBox(index).Text);
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


        private void done1_Click(object sender, EventArgs e)
        {
            taskCompleted[0] = true;
            taskCompletedDate[0] = DateTime.Now;
            UpdateTimeDisplay(1);
        }

        private void done2_Click(object sender, EventArgs e)
        {
            taskCompleted[1] = true;
            taskCompletedDate[1] = DateTime.Now;
            UpdateTimeDisplay(2);
        }

        private void done3_Click(object sender, EventArgs e)
        {
            taskCompleted[2] = true;
            taskCompletedDate[2] = DateTime.Now;
            UpdateTimeDisplay(3);
        }

        private void done4_Click(object sender, EventArgs e)
        {
            taskCompleted[3] = true;
            taskCompletedDate[3] = DateTime.Now;
            UpdateTimeDisplay(4);
        }

        private void done5_Click(object sender, EventArgs e)
        {
            taskCompleted[4] = true;
            taskCompletedDate[4] = DateTime.Now;
            UpdateTimeDisplay(5);
        }


        private void delete1_Click(object sender, EventArgs e)
        {
            taskCompleted[0] = false;
            taskRead[0] = false;
            textBox1.Text = textBox2.Text;
            textBox2.Text = textBox3.Text;
            textBox3.Text = textBox4.Text;
            textBox4.Text = textBox5.Text;
            textBox5.Text = "";


            for (int i = 1; i <= 5; i++)
            {
                UpdateTimeDisplay(i);
                UpdateReadStatus(i, taskRead[i - 1]);
            }

        }

        private void delete2_Click(object sender, EventArgs e)
        {
            taskCompleted[1] = false;
            taskRead[1] = false;
            textBox2.Text = textBox3.Text;
            textBox3.Text = textBox4.Text;
            textBox4.Text = textBox5.Text;
            textBox5.Text = "";

            for (int i = 1; i <= 5; i++)
            {
                UpdateTimeDisplay(i);
                UpdateReadStatus(i, taskRead[i - 1]);
            }
        }

        private void delete3_Click(object sender, EventArgs e)
        {
            taskCompleted[2] = false;
            taskRead[2] = false;
            textBox3.Text = textBox4.Text;
            textBox4.Text = textBox5.Text;
            textBox5.Text = "";

            for (int i = 1; i <= 5; i++)
            {
                UpdateTimeDisplay(i);
                UpdateReadStatus(i, taskRead[i - 1]);
            }
        }
        private void delete4_Click(object sender, EventArgs e)
        {
            taskCompleted[3] = false;
            taskRead[3] = false;
            textBox4.Text = textBox5.Text;
            textBox5.Text = "";

            for (int i = 1; i <= 5; i++)
            {
                UpdateTimeDisplay(i);
                UpdateReadStatus(i, taskRead[i - 1]);
            }
        }
        private void delete5_Click(object sender, EventArgs e)
        {
            taskCompleted[4] = false;
            taskRead[4] = false;
            textBox5.Text = "";

            for (int i = 1; i <= 5; i++)
            {
                UpdateTimeDisplay(i);
                UpdateReadStatus(i, taskRead[i - 1]);
            }
        }


        private void textBox1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                UpdateReadStatus(1, true);
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                UpdateReadStatus(2, true);
            }
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                UpdateReadStatus(3, true);
            }
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox4.Text))
            {
                UpdateReadStatus(4, true);
            }
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox5.Text))
            {
                UpdateReadStatus(5, true);
            }
        }

        private void edit1_Click(object sender, EventArgs e)
        {
            ok1.Visible = true;
            textBox1.ReadOnly = false;
        }

        private void edit2_Click(object sender, EventArgs e)
        {
            ok2.Visible = true;
            textBox2.ReadOnly = false;
        }

        private void edit3_Click(object sender, EventArgs e)
        {
            ok3.Visible = true;
            textBox3.ReadOnly = false;
        }

        private void edit4_Click(object sender, EventArgs e)
        {
            ok4.Visible = true;
            textBox4.ReadOnly = false;
        }

        private void edit5_Click(object sender, EventArgs e)
        {
            ok5.Visible = true;
            textBox5.ReadOnly = false;
        }

        private void ok1_Click(object sender, EventArgs e)
        {
            ok1.Visible = false;
            textBox1.ReadOnly = true;
        }

        private void ok2_Click(object sender, EventArgs e)
        {
            ok2.Visible = false;
            textBox2.ReadOnly = true;
        }

        private void ok3_Click(object sender, EventArgs e)
        {
            ok3.Visible = false;
            textBox3.ReadOnly = true;
        }

        private void ok4_Click(object sender, EventArgs e)
        {
            ok4.Visible = false;
            textBox4.ReadOnly = true;
        }

        private void ok5_Click(object sender, EventArgs e)
        {
            ok5.Visible = false;
            textBox5.ReadOnly = true;
        }
    }
}