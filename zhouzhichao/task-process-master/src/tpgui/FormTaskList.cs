using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace xworks.taskprocess
{
    public partial class FormTaskList : Form
    {
        string xml_FilePath = "";//用来记录当前打开文件的路径的
        public FormTaskList()
        {
            InitializeComponent();
        }
        public static int DateToDate(DateTime dateTime)//计算与当前日期之前相隔天数的函数
        {
            DateTime dateTime1 = DateTime.Now;
            return (dateTime1 - dateTime).Days;

        }

        private static List<Task> tasks = new List<Task>();//task类的list
        private static String file = null;
        private static Task changeTask = new Task();
        internal static List<Task> Tasks { get => tasks; set => tasks = value; }
        public static string File { get => file; set => file = value; }
        internal static Task ChangeTask { get => changeTask; set => changeTask = value; }
        private void _toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();//一个打开文件的对话框            
            openFileDialog1.Filter = "xml文件(*.xml)|*.xml";//设置允许打开的扩展名            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//判断是否选择了文件             
            {                
                xml_FilePath = openFileDialog1.FileName;//记录用户选择的文件路径                
                XmlDocument xmlDocument = new XmlDocument();//新建一个XML“编辑器”                
                xmlDocument.Load(xml_FilePath);//载入路径这个xml                
                try
                {
                    XmlNodeList xmlNodeList = xmlDocument.SelectSingleNode("tasks").ChildNodes;//选择class为根结点并得到旗下所有子节点                    
                    dataGridView1.Rows.Clear();//清空dataGridView1，防止和上次处理的数据混乱                    
                    foreach (XmlNode xmlNode in xmlNodeList)//遍历class的所有节点                    
                    {
                        XmlElement xmlElement = (XmlElement)xmlNode;//对于任何一个元素，其实就是每一个<student>                        
                                                                    //旗下的子节点<name>和<number>分别放入dataGridView1                        
                        int index = dataGridView1.Rows.Add();//在dataGridView1新加一行，并拿到改行的行标                        
                        dataGridView1.Rows[index].Cells[0].Value = xmlElement.ChildNodes.Item(0).InnerText;//各个单元格分别添加                        
                        dataGridView1.Rows[index].Cells[1].Value = xmlElement.ChildNodes.Item(1).InnerText;
                        dataGridView1.Rows[index].Cells[2].Value = xmlElement.ChildNodes.Item(2).InnerText;
                        dataGridView1.Rows[index].Cells[3].Value = xmlElement.ChildNodes.Item(3).InnerText;
                        dataGridView1.Rows[index].Cells[4].Value = xmlElement.ChildNodes.Item(4).InnerText;
                        dataGridView1.Rows[index].Cells[5].Value = xmlElement.ChildNodes.Item(5).InnerText;
                        dataGridView1.Rows[index].Cells[6].Value = xmlElement.ChildNodes.Item(6).InnerText;
                        dataGridView1.Rows[index].Cells[7].Value = xmlElement.ChildNodes.Item(7).InnerText;
                        dataGridView1.Rows[index].Cells[8].Value = xmlElement.ChildNodes.Item(8).InnerText;
                        dataGridView1.Rows[index].Cells[9].Value = xmlElement.ChildNodes.Item(9).InnerText;
                        dataGridView1.Rows[index].Cells[10].Value = xmlElement.ChildNodes.Item(10).InnerText;

                    }
                }
                catch
                {
                    MessageBox.Show("XML格式不对！");
                }
            }
            else
            {
                MessageBox.Show("请打开XML文件");
            }


        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            FormTaskProcess f = new FormTaskProcess();
            f.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FormTaskConfirm f = new FormTaskConfirm();
            f.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            FormLinkFile f = new FormLinkFile();
            f.ShowDialog();
        }


        private void FormTaskList_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

        }

        private void 高ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changePeioritys(0);
        }

        private void changePeioritys(int priority)
        {

        }

        private void toChangePriority(int changePriority)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(File);
            XmlNodeList nodeList = doc.SelectNodes("//task");
        }

        private void 中ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changePeioritys(1);
        }

        private void 一般ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changePeioritys(2);
        }

        private void 低ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changePeioritys(3);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}
