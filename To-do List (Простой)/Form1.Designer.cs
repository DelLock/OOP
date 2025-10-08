namespace To_do_List__Простой_
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
            label1 = new Label();
            Задача = new Label();
            label3 = new Label();
            textNewTask = new TextBox();
            button1 = new Button();
            edit1 = new Button();
            panel1 = new Panel();
            ok1 = new Button();
            done1 = new Button();
            delete1 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            panel0 = new Panel();
            ok2 = new Button();
            done2 = new Button();
            delete2 = new Button();
            label2 = new Label();
            edit2 = new Button();
            panel2 = new Panel();
            label4 = new Label();
            ok3 = new Button();
            edit3 = new Button();
            textBox3 = new TextBox();
            delete3 = new Button();
            done3 = new Button();
            panel3 = new Panel();
            label5 = new Label();
            done5 = new Button();
            ok5 = new Button();
            delete5 = new Button();
            textBox4 = new TextBox();
            edit5 = new Button();
            label6 = new Label();
            done4 = new Button();
            ok4 = new Button();
            delete4 = new Button();
            textBox5 = new TextBox();
            edit4 = new Button();
            panel4 = new Panel();
            panel5 = new Panel();
            timePanel1 = new Panel();
            timeLabel1 = new Label();
            timePanel2 = new Panel();
            timeLabel2 = new Label();
            timePanel3 = new Panel();
            timeLabel3 = new Label();
            timePanel4 = new Panel();
            timeLabel4 = new Label();
            timePanel5 = new Panel();
            timeLabel5 = new Label();
            readLabel1 = new Label();
            readLabel2 = new Label();
            readLabel3 = new Label();
            readLabel4 = new Label();
            readLabel5 = new Label();
            panel1.SuspendLayout();
            panel0.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            timePanel1.SuspendLayout();
            timePanel2.SuspendLayout();
            timePanel3.SuspendLayout();
            timePanel4.SuspendLayout();
            timePanel5.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(291, 32);
            label1.Name = "label1";
            label1.Size = new Size(153, 21);
            label1.TabIndex = 0;
            label1.Text = "To do приложение";
            // 
            // Задача
            // 
            Задача.AutoSize = true;
            Задача.Location = new Point(6, 48);
            Задача.Name = "Задача";
            Задача.Size = new Size(100, 15);
            Задача.TabIndex = 0;
            Задача.Text = "Добавьте задачу:";
            Задача.TextAlign = ContentAlignment.MiddleLeft;
            Задача.UseMnemonic = false;
            // 
            // label3
            // 
            label3.AllowDrop = true;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.Location = new Point(3, 19);
            label3.Name = "label3";
            label3.Size = new Size(20, 20);
            label3.TabIndex = 0;
            label3.Text = "1.";
            // 
            // textNewTask
            // 
            textNewTask.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textNewTask.Location = new Point(108, 46);
            textNewTask.Name = "textNewTask";
            textNewTask.Size = new Size(372, 25);
            textNewTask.TabIndex = 1;
            textNewTask.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(519, 31);
            button1.Name = "button1";
            button1.Size = new Size(127, 55);
            button1.TabIndex = 2;
            button1.Text = "Добавьте Текст";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // edit1
            // 
            edit1.Anchor = AnchorStyles.None;
            edit1.Location = new Point(510, 11);
            edit1.Name = "edit1";
            edit1.Size = new Size(99, 40);
            edit1.TabIndex = 5;
            edit1.Text = "Редактировать";
            edit1.UseVisualStyleBackColor = true;
            edit1.Click += edit1_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(ok1);
            panel1.Controls.Add(done1);
            panel1.Controls.Add(delete1);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(edit1);
            panel1.Location = new Point(12, 200);
            panel1.Name = "panel1";
            panel1.Size = new Size(776, 60);
            panel1.TabIndex = 3;
            panel1.Visible = false;
            // 
            // ok1
            // 
            ok1.Anchor = AnchorStyles.None;
            ok1.Location = new Point(419, 11);
            ok1.Name = "ok1";
            ok1.Size = new Size(85, 40);
            ok1.TabIndex = 3;
            ok1.Text = "Хорошо";
            ok1.UseVisualStyleBackColor = true;
            ok1.Visible = false;
            ok1.Click += ok1_Click;
            // 
            // done1
            // 
            done1.Anchor = AnchorStyles.None;
            done1.Location = new Point(615, 11);
            done1.Name = "done1";
            done1.Size = new Size(75, 40);
            done1.TabIndex = 4;
            done1.Text = "Готово";
            done1.UseVisualStyleBackColor = true;
            done1.Click += done1_Click;
            // 
            // delete1
            // 
            delete1.Anchor = AnchorStyles.None;
            delete1.Location = new Point(696, 11);
            delete1.Name = "delete1";
            delete1.Size = new Size(75, 40);
            delete1.TabIndex = 2;
            delete1.Text = "Удалить";
            delete1.UseVisualStyleBackColor = true;
            delete1.Click += delete1_Click;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.None;
            textBox1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox1.Location = new Point(29, 19);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(384, 25);
            textBox1.TabIndex = 7;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.None;
            textBox2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox2.Location = new Point(29, 14);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(384, 25);
            textBox2.TabIndex = 1;
            // 
            // panel0
            // 
            panel0.Controls.Add(button1);
            panel0.Controls.Add(Задача);
            panel0.Controls.Add(textNewTask);
            panel0.Location = new Point(56, 56);
            panel0.Name = "panel0";
            panel0.Size = new Size(688, 98);
            panel0.TabIndex = 4;
            // 
            // ok2
            // 
            ok2.Anchor = AnchorStyles.None;
            ok2.Location = new Point(419, 7);
            ok2.Name = "ok2";
            ok2.Size = new Size(85, 40);
            ok2.TabIndex = 9;
            ok2.Text = "Хорошо";
            ok2.UseVisualStyleBackColor = true;
            ok2.Visible = false;
            ok2.Click += ok2_Click;
            // 
            // done2
            // 
            done2.Anchor = AnchorStyles.None;
            done2.Location = new Point(615, 7);
            done2.Name = "done2";
            done2.Size = new Size(75, 40);
            done2.TabIndex = 10;
            done2.Text = "Готово";
            done2.UseVisualStyleBackColor = true;
            done2.Click += done2_Click;
            // 
            // delete2
            // 
            delete2.Anchor = AnchorStyles.None;
            delete2.Location = new Point(696, 7);
            delete2.Name = "delete2";
            delete2.Size = new Size(75, 40);
            delete2.TabIndex = 8;
            delete2.Text = "Удалить";
            delete2.UseVisualStyleBackColor = true;
            delete2.Click += delete2_Click;
            // 
            // label2
            // 
            label2.AllowDrop = true;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(3, 15);
            label2.Name = "label2";
            label2.Size = new Size(20, 20);
            label2.TabIndex = 6;
            label2.Text = "2.";
            // 
            // edit2
            // 
            edit2.Anchor = AnchorStyles.None;
            edit2.Location = new Point(510, 7);
            edit2.Name = "edit2";
            edit2.Size = new Size(99, 40);
            edit2.TabIndex = 11;
            edit2.Text = "Редактировать";
            edit2.UseVisualStyleBackColor = true;
            edit2.Click += edit2_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(label2);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(ok2);
            panel2.Controls.Add(edit2);
            panel2.Controls.Add(delete2);
            panel2.Controls.Add(done2);
            panel2.Location = new Point(12, 276);
            panel2.Name = "panel2";
            panel2.Size = new Size(776, 55);
            panel2.TabIndex = 12;
            panel2.Visible = false;
            // 
            // label4
            // 
            label4.AllowDrop = true;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label4.Location = new Point(3, 15);
            label4.Name = "label4";
            label4.Size = new Size(20, 20);
            label4.TabIndex = 12;
            label4.Text = "3.";
            // 
            // ok3
            // 
            ok3.Anchor = AnchorStyles.None;
            ok3.Location = new Point(419, 7);
            ok3.Name = "ok3";
            ok3.Size = new Size(85, 40);
            ok3.TabIndex = 15;
            ok3.Text = "Хорошо";
            ok3.UseVisualStyleBackColor = true;
            ok3.Visible = false;
            ok3.Click += ok3_Click;
            // 
            // edit3
            // 
            edit3.Anchor = AnchorStyles.None;
            edit3.Location = new Point(510, 7);
            edit3.Name = "edit3";
            edit3.Size = new Size(99, 40);
            edit3.TabIndex = 17;
            edit3.Text = "Редактировать";
            edit3.UseVisualStyleBackColor = true;
            edit3.Click += edit3_Click;
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.None;
            textBox3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox3.Location = new Point(29, 16);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(384, 25);
            textBox3.TabIndex = 13;
            // 
            // delete3
            // 
            delete3.Anchor = AnchorStyles.None;
            delete3.Location = new Point(696, 7);
            delete3.Name = "delete3";
            delete3.Size = new Size(75, 40);
            delete3.TabIndex = 14;
            delete3.Text = "Удалить";
            delete3.UseVisualStyleBackColor = true;
            delete3.Click += delete3_Click;
            // 
            // done3
            // 
            done3.Anchor = AnchorStyles.None;
            done3.Location = new Point(615, 7);
            done3.Name = "done3";
            done3.Size = new Size(75, 40);
            done3.TabIndex = 16;
            done3.Text = "Готово";
            done3.UseVisualStyleBackColor = true;
            done3.Click += done3_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(label4);
            panel3.Controls.Add(done3);
            panel3.Controls.Add(ok3);
            panel3.Controls.Add(delete3);
            panel3.Controls.Add(textBox3);
            panel3.Controls.Add(edit3);
            panel3.Location = new Point(12, 347);
            panel3.Name = "panel3";
            panel3.Size = new Size(776, 55);
            panel3.TabIndex = 18;
            panel3.Visible = false;
            // 
            // label5
            // 
            label5.AllowDrop = true;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label5.Location = new Point(5, 14);
            label5.Name = "label5";
            label5.Size = new Size(20, 20);
            label5.TabIndex = 18;
            label5.Text = "5.";
            // 
            // done5
            // 
            done5.Anchor = AnchorStyles.None;
            done5.Location = new Point(617, 6);
            done5.Name = "done5";
            done5.Size = new Size(75, 40);
            done5.TabIndex = 22;
            done5.Text = "Готово";
            done5.UseVisualStyleBackColor = true;
            done5.Click += done5_Click;
            // 
            // ok5
            // 
            ok5.Anchor = AnchorStyles.None;
            ok5.Location = new Point(421, 6);
            ok5.Name = "ok5";
            ok5.Size = new Size(85, 40);
            ok5.TabIndex = 21;
            ok5.Text = "Хорошо";
            ok5.UseVisualStyleBackColor = true;
            ok5.Visible = false;
            ok5.Click += ok5_Click;
            // 
            // delete5
            // 
            delete5.Anchor = AnchorStyles.None;
            delete5.Location = new Point(698, 6);
            delete5.Name = "delete5";
            delete5.Size = new Size(75, 40);
            delete5.TabIndex = 20;
            delete5.Text = "Удалить";
            delete5.UseVisualStyleBackColor = true;
            delete5.Click += delete5_Click;
            // 
            // textBox4
            // 
            textBox4.Anchor = AnchorStyles.None;
            textBox4.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox4.Location = new Point(31, 14);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(384, 25);
            textBox4.TabIndex = 19;
            // 
            // edit5
            // 
            edit5.Anchor = AnchorStyles.None;
            edit5.Location = new Point(512, 6);
            edit5.Name = "edit5";
            edit5.Size = new Size(99, 40);
            edit5.TabIndex = 23;
            edit5.Text = "Редактировать";
            edit5.UseVisualStyleBackColor = true;
            edit5.Click += edit5_Click;
            // 
            // label6
            // 
            label6.AllowDrop = true;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label6.Location = new Point(5, 15);
            label6.Name = "label6";
            label6.Size = new Size(20, 20);
            label6.TabIndex = 24;
            label6.Text = "4.";
            // 
            // done4
            // 
            done4.Anchor = AnchorStyles.None;
            done4.Location = new Point(617, 7);
            done4.Name = "done4";
            done4.Size = new Size(75, 40);
            done4.TabIndex = 28;
            done4.Text = "Готово";
            done4.UseVisualStyleBackColor = true;
            done4.Click += done4_Click;
            // 
            // ok4
            // 
            ok4.Anchor = AnchorStyles.None;
            ok4.Location = new Point(421, 7);
            ok4.Name = "ok4";
            ok4.Size = new Size(85, 40);
            ok4.TabIndex = 27;
            ok4.Text = "Хорошо";
            ok4.UseVisualStyleBackColor = true;
            ok4.Visible = false;
            ok4.Click += ok4_Click;
            // 
            // delete4
            // 
            delete4.Anchor = AnchorStyles.None;
            delete4.Location = new Point(698, 7);
            delete4.Name = "delete4";
            delete4.Size = new Size(75, 40);
            delete4.TabIndex = 26;
            delete4.Text = "Удалить";
            delete4.UseVisualStyleBackColor = true;
            delete4.Click += delete4_Click;
            // 
            // textBox5
            // 
            textBox5.Anchor = AnchorStyles.None;
            textBox5.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox5.Location = new Point(31, 14);
            textBox5.Name = "textBox5";
            textBox5.ReadOnly = true;
            textBox5.Size = new Size(384, 25);
            textBox5.TabIndex = 25;
            // 
            // edit4
            // 
            edit4.Anchor = AnchorStyles.None;
            edit4.Location = new Point(512, 7);
            edit4.Name = "edit4";
            edit4.Size = new Size(99, 40);
            edit4.TabIndex = 29;
            edit4.Text = "Редактировать";
            edit4.UseVisualStyleBackColor = true;
            edit4.Click += edit4_Click;
            // 
            // panel4
            // 
            panel4.Controls.Add(textBox4);
            panel4.Controls.Add(label6);
            panel4.Controls.Add(edit4);
            panel4.Controls.Add(done4);
            panel4.Controls.Add(delete4);
            panel4.Controls.Add(ok4);
            panel4.Location = new Point(10, 415);
            panel4.Name = "panel4";
            panel4.Size = new Size(778, 52);
            panel4.TabIndex = 30;
            panel4.Visible = false;
            // 
            // panel5
            // 
            panel5.Controls.Add(edit5);
            panel5.Controls.Add(textBox5);
            panel5.Controls.Add(label5);
            panel5.Controls.Add(delete5);
            panel5.Controls.Add(done5);
            panel5.Controls.Add(ok5);
            panel5.Location = new Point(10, 482);
            panel5.Name = "panel5";
            panel5.Size = new Size(778, 52);
            panel5.TabIndex = 31;
            panel5.Visible = false;
            // 
            // timePanel1
            // 
            timePanel1.Controls.Add(timeLabel1);
            timePanel1.Location = new Point(794, 217);
            timePanel1.Name = "timePanel1";
            timePanel1.Size = new Size(275, 34);
            timePanel1.TabIndex = 32;
            timePanel1.Visible = false;
            // 
            // timeLabel1
            // 
            timeLabel1.AutoSize = true;
            timeLabel1.Location = new Point(3, 7);
            timeLabel1.Name = "timeLabel1";
            timeLabel1.Size = new Size(117, 15);
            timeLabel1.TabIndex = 0;
            timeLabel1.Text = "В процессе: 00:00:00";
            timeLabel1.Visible = false;
            // 
            // timePanel2
            // 
            timePanel2.Controls.Add(timeLabel2);
            timePanel2.Location = new Point(794, 289);
            timePanel2.Name = "timePanel2";
            timePanel2.Size = new Size(275, 34);
            timePanel2.TabIndex = 33;
            // 
            // timeLabel2
            // 
            timeLabel2.AutoSize = true;
            timeLabel2.Location = new Point(3, 11);
            timeLabel2.Name = "timeLabel2";
            timeLabel2.Size = new Size(117, 15);
            timeLabel2.TabIndex = 34;
            timeLabel2.Text = "В процессе: 00:00:00";
            timeLabel2.Visible = false;
            // 
            // timePanel3
            // 
            timePanel3.Controls.Add(timeLabel3);
            timePanel3.Location = new Point(794, 354);
            timePanel3.Name = "timePanel3";
            timePanel3.Size = new Size(275, 34);
            timePanel3.TabIndex = 33;
            // 
            // timeLabel3
            // 
            timeLabel3.AutoSize = true;
            timeLabel3.Location = new Point(3, 8);
            timeLabel3.Name = "timeLabel3";
            timeLabel3.Size = new Size(117, 15);
            timeLabel3.TabIndex = 35;
            timeLabel3.Text = "В процессе: 00:00:00";
            timeLabel3.Visible = false;
            // 
            // timePanel4
            // 
            timePanel4.Controls.Add(timeLabel4);
            timePanel4.Location = new Point(794, 422);
            timePanel4.Name = "timePanel4";
            timePanel4.Size = new Size(275, 34);
            timePanel4.TabIndex = 33;
            // 
            // timeLabel4
            // 
            timeLabel4.AutoSize = true;
            timeLabel4.Location = new Point(3, 8);
            timeLabel4.Name = "timeLabel4";
            timeLabel4.Size = new Size(117, 15);
            timeLabel4.TabIndex = 36;
            timeLabel4.Text = "В процессе: 00:00:00";
            timeLabel4.Visible = false;
            // 
            // timePanel5
            // 
            timePanel5.Controls.Add(timeLabel5);
            timePanel5.Location = new Point(794, 496);
            timePanel5.Name = "timePanel5";
            timePanel5.Size = new Size(275, 34);
            timePanel5.TabIndex = 33;
            // 
            // timeLabel5
            // 
            timeLabel5.AutoSize = true;
            timeLabel5.Location = new Point(0, 10);
            timeLabel5.Name = "timeLabel5";
            timeLabel5.Size = new Size(117, 15);
            timeLabel5.TabIndex = 37;
            timeLabel5.Text = "В процессе: 00:00:00";
            timeLabel5.Visible = false;
            // 
            // readLabel1
            // 
            readLabel1.AutoSize = true;
            readLabel1.ForeColor = Color.Red;
            readLabel1.Location = new Point(797, 200);
            readLabel1.Name = "readLabel1";
            readLabel1.Size = new Size(101, 15);
            readLabel1.TabIndex = 34;
            readLabel1.Text = "✗ Не выполнено";
            readLabel1.Visible = false;
            // 
            // readLabel2
            // 
            readLabel2.AutoSize = true;
            readLabel2.ForeColor = Color.Red;
            readLabel2.Location = new Point(797, 271);
            readLabel2.Name = "readLabel2";
            readLabel2.Size = new Size(101, 15);
            readLabel2.TabIndex = 35;
            readLabel2.Text = "✗ Не выполнено";
            readLabel2.Visible = false;
            // 
            // readLabel3
            // 
            readLabel3.AutoSize = true;
            readLabel3.ForeColor = Color.Red;
            readLabel3.Location = new Point(797, 336);
            readLabel3.Name = "readLabel3";
            readLabel3.Size = new Size(101, 15);
            readLabel3.TabIndex = 36;
            readLabel3.Text = "✗ Не выполнено";
            readLabel3.Visible = false;
            // 
            // readLabel4
            // 
            readLabel4.AutoSize = true;
            readLabel4.ForeColor = Color.Red;
            readLabel4.Location = new Point(794, 404);
            readLabel4.Name = "readLabel4";
            readLabel4.Size = new Size(101, 15);
            readLabel4.TabIndex = 37;
            readLabel4.Text = "✗ Не выполнено";
            readLabel4.Visible = false;
            // 
            // readLabel5
            // 
            readLabel5.AutoSize = true;
            readLabel5.ForeColor = Color.Red;
            readLabel5.Location = new Point(797, 478);
            readLabel5.Name = "readLabel5";
            readLabel5.Size = new Size(101, 15);
            readLabel5.TabIndex = 38;
            readLabel5.Text = "✗ Не выполнено";
            readLabel5.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1129, 555);
            Controls.Add(readLabel5);
            Controls.Add(readLabel4);
            Controls.Add(readLabel3);
            Controls.Add(readLabel2);
            Controls.Add(readLabel1);
            Controls.Add(timePanel2);
            Controls.Add(timePanel3);
            Controls.Add(timePanel4);
            Controls.Add(timePanel5);
            Controls.Add(timePanel1);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel0);
            Controls.Add(panel1);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel0.ResumeLayout(false);
            panel0.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            timePanel1.ResumeLayout(false);
            timePanel1.PerformLayout();
            timePanel2.ResumeLayout(false);
            timePanel2.PerformLayout();
            timePanel3.ResumeLayout(false);
            timePanel3.PerformLayout();
            timePanel4.ResumeLayout(false);
            timePanel4.PerformLayout();
            timePanel5.ResumeLayout(false);
            timePanel5.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label Задача;
        private TextBox textNewTask;
        private Label label3;
        private Button button1;
        private Button edit1;
        private Panel panel1;
        private Button ok1;
        private TextBox textBox2;
        private Button done1;
        private Button delete1;
        private Panel panel0;
        private Button ok2;
        private TextBox textBox1;
        private Button done2;
        private Button delete2;
        private Label label2;
        private Button edit2;
        private Panel panel2;
        private Label label4;
        private Button ok3;
        private Button edit3;
        private TextBox textBox3;
        private Button delete3;
        private Button done3;
        private Panel panel5;
        private Panel panel3;
        private Label label5;
        private Button done5;
        private Button ok5;
        private Button delete5;
        private TextBox textBox4;
        private Button edit5;
        private Label label6;
        private Button done4;
        private Button ok4;
        private Button delete4;
        private TextBox textBox5;
        private Button edit4;
        private Panel panel4;
        private Panel timePanel1;
        private Panel timePanel2;
        private Panel timePanel3;
        private Panel timePanel4;
        private Panel timePanel5;
        private Label timeLabel1;
        private Label timeLabel3;
        private Label timeLabel2;
        private Label timeLabel4;
        private Label timeLabel5;
        private Label readLabel1;
        private Label readLabel2;
        private Label readLabel3;
        private Label readLabel4;
        private Label readLabel5;
    }
}
