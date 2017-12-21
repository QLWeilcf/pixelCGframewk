using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
/*本程序用于练习xml文件的读取和写入、删除；
 * 数据为LFim的数据，基本格式不在此描述
 * 顺便复习正则
 * */
namespace XMLrwConsole {
    class Program {
        static void Main (string[] args) {
            Program prog = new Program();
            //Console.WriteLine(DateTime.Now.ToString());
            DateTime dt = new DateTime(2017,12,21,20,30,56);
            prog.reTest();
            //prog.readXML();
            //prog.readAndDelXML();


            Console.ReadKey();
        }

        public void wriFile () {
            string sfpath = "E:/UnityProj/AlftoIM/LF及时通讯v1215/LF及时通讯/LF_Profile/chatLog1222.txt";
            FileStream wrfstm = new FileStream(sfpath, FileMode.Append, FileAccess.Write);
            StreamWriter swri = new StreamWriter(wrfstm);
            DateTime dtime = DateTime.Now;
            swri.Write(dtime.ToString() + "\n");
            swri.Close();
        }
        public void readXML () {
            //检查用户是否存在，账号密码是否对应
            //生成 list<string> exist_ID_lst
            //把密码暴露出来很危险，所以List<string[]> idlst 是函数内的临时变量；展示出来的只有exist那个
            List<string[]> id_sList = new List<string[]>();
            List<string> exist_ID_lst = new List<string>();
            string pathXML = "E:/UnityProj/AlftoIM/LF及时通讯v1215/LF及时通讯/LF_Profile/lfIDlist1.xml";

            XmlDocument xmlID = new XmlDocument();
            xmlID.Load(pathXML);
            XmlElement rootElem = xmlID.DocumentElement;   //获取根节点  
            XmlNodeList personNodes = rootElem.GetElementsByTagName("person"); //获取person子节点集合  

            


            foreach (XmlNode node in personNodes) {
                string[] idTwo = new string[2];
                string strName = ((XmlElement)node).GetAttribute("xmlns");   //获取属性值  
                idTwo[0] = strName;
                idTwo[1] = node.ChildNodes[0].InnerText;
                id_sList.Add(idTwo);
                exist_ID_lst.Add(strName);
            }

            string nuID = "libai";
            string nPsw = "lb123456";

            XmlElement xelnewp = xmlID.CreateElement("person",nuID);
            XmlElement xelnewid = xmlID.CreateElement("id");
            XmlElement xelnewpsw = xmlID.CreateElement("pwd");
            xelnewid.InnerText = nuID;
            xelnewpsw.InnerText = nPsw;
            xelnewp.AppendChild(xelnewid);
            xelnewp.AppendChild(xelnewpsw);

            rootElem.AppendChild(xelnewp);
            xmlID.Save(pathXML);

            var a = exist_ID_lst;
            //当用户注册时，检查exist_ID_lst，用户发消息到离线时，检查exist_ID_lst
            //return id_sList;



        }
        public void readAndDelXML () {
            //读取离线消息，打包发送；删除xml里的消息；最好在chatlog里有记录，难写就算了；
            string snedID="lyndon";//要发给的ID
            //content="sender:time:cont;sender2:time:cont"
            List<string[]> a_msg = new List<string[]>();
            List<string> msg_lst = new List<string>();
            string pathXML = "E:/UnityProj/AlftoIM/LF及时通讯v1215/LF及时通讯/LF_Profile/messageUnsendTest.xml";

            XmlDocument xmlID = new XmlDocument();
            xmlID.Load(pathXML);
            XmlElement rootElem = xmlID.DocumentElement;   //获取根节点  
            //另外一种获取根节点的方式：
            //XmlNode xn = xmlID.SelectSingleNode("root");
            XmlNodeList personNodes = rootElem.GetElementsByTagName("msg"); //获取person子节点集合
            //XmlNodeList xnl = rootElem.ChildNodes;



            for (int i = personNodes.Count - 1; i >= 0; i--) {
                XmlNode nodep = personNodes[i];
                string strName = ((XmlElement)nodep).GetAttribute("xmlns");   //获取属性值  
                if (strName == snedID) {
                    string[] idTwo = new string[3];
                    idTwo[0] = nodep.ChildNodes[0].InnerText;//sender
                    idTwo[1] = nodep.ChildNodes[1].InnerText;//time
                    idTwo[2] = nodep.ChildNodes[3].InnerText;

                    a_msg.Add(idTwo);

                    //删除节点
                    nodep.ParentNode.RemoveChild(nodep);

                }
            }
            

            xmlID.Save(pathXML);
            var a = a_msg;


        }
        public void wriXML () {
            //写入离线消息
            string sender = "liuxl";
            string ctime = DateTime.Now.ToLongTimeString();
            string reciver = "lyndon";
            string content = "a note to right";

            string pathXML = "E:/UnityProj/AlftoIM/LF及时通讯v1215/LF及时通讯/LF_Profile/messageUnsendTest.xml";

            XmlDocument xmlID = new XmlDocument();
            xmlID.Load(pathXML);
            XmlElement rootElem = xmlID.DocumentElement;   //获取根节点  
            //另外一种获取根节点的方式：
            //XmlNode xn = xmlID.SelectSingleNode("root");
            XmlNodeList personNodes = rootElem.GetElementsByTagName("msg"); //获取person子节点集合

            XmlElement xelnewp = xmlID.CreateElement("msg",reciver);
            XmlElement xnsend = xmlID.CreateElement("sender");
            XmlElement xntm = xmlID.CreateElement("time");
            XmlElement xnrec = xmlID.CreateElement("reciver");
            XmlElement xncnt = xmlID.CreateElement("content");
            XmlElement xncnty = xmlID.CreateElement("time");
            xnsend.InnerText = sender;
            xntm.InnerText = ctime;
            xnrec.InnerText = reciver;
            xncnt.InnerText = content;
            xncnty.InnerText = content;
            xelnewp.AppendChild(xnsend);
            xelnewp.AppendChild(xntm);
            xelnewp.AppendChild(xnrec);
            xelnewp.AppendChild(xncnt);
            xelnewp.AppendChild(xncnty);


            rootElem.AppendChild(xelnewp);
            xmlID.Save(pathXML);


        }
        public void addIDtoXML () {
            //加入新注册的id；
            string pathXML = "E:/UnityProj/AlftoIM/LF及时通讯v1215/LF及时通讯/LF_Profile/lfIDlist1.xml";

            XmlDocument xmlID = new XmlDocument();
            xmlID.Load(pathXML);
            XmlElement rootElem = xmlID.DocumentElement;   //获取根节点  
            XmlNodeList personNodes = rootElem.GetElementsByTagName("person"); //获取person子节点集合  

            string nuID = "libai";
            string nPsw = "lb123456";

            XmlElement xelnewp = xmlID.CreateElement("person", nuID);
            XmlElement xelnewid = xmlID.CreateElement("id");
            XmlElement xelnewpsw = xmlID.CreateElement("pwd");
            xelnewid.InnerText = nuID;
            xelnewpsw.InnerText = nPsw;
            xelnewp.AppendChild(xelnewid);
            xelnewp.AppendChild(xelnewpsw);

            rootElem.AppendChild(xelnewp);
            xmlID.Save(pathXML);


        }
        private void reTest () {//用于公告简单解析
            string conc = "lyndon;liuyf;lingf;libai;youyu";
            string[] stList = conc.Split(';');
            string anncOne = "lyndon=:2017/12/21 20:13:56=:time to eat=;=ly=:2017/12/21 20:15:26=:homecoming";//=:  =;=
            Regex rgxe = new Regex(@"=;=");
            Regex rgx = new Regex(@"=:");
            string[] anncT = rgxe.Split(anncOne);

            string[] annc_three = rgx.Split(anncT[0]);

            var a = stList;

        }

    }
}
