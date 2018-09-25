using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace xworks.taskprocess
{
    class TaskFile
    {
        private static String GetThing(XmlNode xmlNode, String nodeName)//获取节点的值的函数
        {
            XmlNodeList xmlNodelist1 = xmlNode.SelectNodes(nodeName);
            return xmlNodelist1.Item(0).InnerText;
        }
        private static DateTime StringToDate(String DateString)//将String转换为Date的函数
        {

            return DateTime.ParseExact(DateString, "yyyyMMddHHmmss", null);
        }
        public List<Task> LoadTasks(String filePath)
        {
            List<Task> Tasks = new List<Task>();
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNodeList nodeList = doc.SelectNodes("//task");
            foreach (XmlNode xmlNode in nodeList)
            {
                Task task = new Task();
                String Id = xmlNode.Attributes["id"].Value;//获取Xml文件的节点的属性值
                Guid Idguid = new Guid(Id);
                String Author = GetThing(xmlNode, "Author");
                String StringSubmitTime = GetThing(xmlNode, "SubmitTime");
                DateTime SubmitTime = StringToDate(StringSubmitTime);
                String StirngPriority = GetThing(xmlNode, "Priority");
                TaskPriority Priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), StirngPriority);
                String StirngDueTime = GetThing(xmlNode, "DueTime");
                DateTime DueTime = StringToDate(StirngDueTime);
                String Assignee = GetThing(xmlNode, "Assignee");
                String Content = GetThing(xmlNode, "Content");
                String HandlingNote = GetThing(xmlNode, "HandlingNote");
                String StringStatus = GetThing(xmlNode, "Status");
                TaskStatus Status = (TaskStatus)Enum.Parse(typeof(TaskStatus), StringStatus);//String向枚举转换
                String Checker = GetThing(xmlNode, "Checker");
                String StringCheckTime = GetThing(xmlNode, "CheckTime");
                DateTime CheckTime = StringToDate(StringCheckTime);
                String StringFinallyTime = GetThing(xmlNode, "FinallyTime");
                DateTime FinallyTime = StringToDate(StringFinallyTime);
                task.Id = Idguid;
                task.Author = Author;
                task.SubmitTime = SubmitTime;
                task.Priority = Priority;
                task.DueTime = DueTime;
                task.Assignee = Assignee;
                task.Content = Content;
                task.FinallyTime = FinallyTime;
                task.HandlingNote = HandlingNote;
                task.Status = Status;
                task.Checker = Checker;
                task.CheckTime = CheckTime;
                Tasks.Add(task);
            }
            return Tasks;
        }
    }
}
