using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xworks.taskprocess
{
    public partial class FormTaskEdit : Form
    {

        public FormTaskEdit()
        {
            InitializeComponent();
        }
        ListViewItem oldItem = new ListViewItem();
        private static Task changeTask = new Task();
        internal static Task ChangeTask { get => changeTask; set => changeTask = value; }
        public static int ThinkClose { get => thinkClose; set => thinkClose = value; }
        private static int thinkClose = 0;//判断用户是否取消的代码,0为不取消
        private int addOrChange = 0;//判断是否为修改操作的代码，1为进行修改操作

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认取消吗", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                thinkClose = 1;
                this.Close();
            }
            else
            {
                return;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int Priority = 0;
            if (radioButton1.Checked)
            {
                Priority = 0;
            }
            else if (radioButton2.Checked)
            {
                Priority = 1;
            }
            else if (radioButton3.Checked)
            {
                Priority = 2;
            }
            else if (radioButton4.Checked)
            {
                Priority = 3;
            }
            String DueTime = dateTimePicker1.Text;//预订日
            DateTime DateDueTime = Convert.ToDateTime(DueTime);//String转换Date
            int days = FormTaskList.DateToDate(DateDueTime);
            String Assignee = textBox2.Text;//作业者
            String Content = textBox1.Text;//详细
            if (Assignee == "" || Content == "")
            {
                MessageBox.Show("作业者和详细必须全部填写！");
                return;
            }
            else if (days < 0)
            {
                MessageBox.Show("预订日期不得超过今日！");
            }
            else
            {
                if (addOrChange == 0)//addOrChange的值为0，说明并没有选择修改项，就是添加
                {
                    DialogResult dr = MessageBox.Show("确认保存吗", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        String nullDate = "1830/01/01 21:25";
                        DateTime dateTime = Convert.ToDateTime(nullDate);
                        thinkClose = 0;
                        changeTask.Id = System.Guid.NewGuid();
                        changeTask.Priority = (TaskPriority)Priority;
                        changeTask.CheckTime = dateTime;
                        changeTask.FinallyTime = dateTime;
                        changeTask.SubmitTime = dateTime;
                        changeTask.DueTime = DateDueTime;
                        changeTask.Assignee = Assignee;
                        changeTask.Content = Content;
                        this.Close();
                    }
                    else
                    {
                        thinkClose = 1;
                        return;
                    }
                }
                else//不是添加，是修改
                {
                    DialogResult dr = MessageBox.Show("确认保存吗", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        thinkClose = 0;
                        changeTask.Priority = (TaskPriority)Priority;
                        changeTask.DueTime = DateDueTime;
                        changeTask.Assignee = Assignee;
                        changeTask.Content = Content;
                        this.Close();
                    }
                    else
                    {
                        thinkClose = 1;
                        return;
                    }
                }
            }
        }

        private void FormTaskEdit_Load(object sender, EventArgs e)
        {
            Guid guid = new Guid();
            if (FormTaskList.ChangeTask.Id != guid)//changtask的id的值不为00000，说明选择修改项
            {
                addOrChange = 1;//代码为1，做修改操作
                if (FormTaskList.ChangeTask.Priority == (TaskPriority)0)
                {
                    radioButton1.Checked = true;
                }
                else if (FormTaskList.ChangeTask.Priority == (TaskPriority)1)
                {
                    radioButton2.Checked = true;
                }
                else if (FormTaskList.ChangeTask.Priority == (TaskPriority)2)
                {
                    radioButton3.Checked = true;
                }
                else if (FormTaskList.ChangeTask.Priority == (TaskPriority)3)
                {
                    radioButton4.Checked = true;
                }
                dateTimePicker1.Text = FormTaskList.ChangeTask.DueTime.ToString("yyyy-MM-dd");
                textBox2.Text = FormTaskList.ChangeTask.Assignee;
                textBox1.Text = FormTaskList.ChangeTask.Content;
            }
            else
            {
            }

        }

        private void FormTaskEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认取消吗", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                thinkClose = 1;
            }
            else
            {
                return;
            }
        }
    }
}
