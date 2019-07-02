using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GDAL_O
{
    public partial class ZiDuanChaXun : Form
    {
        public ZiDuanChaXun()
        {
            InitializeComponent();
        }
        ShpCreate a = new ShpCreate();
        string av = "";
        private void ZiDuanChaXun_Load(object sender, EventArgs e)
        {
            //Shpread Sp = new Shpread();
            //Sp.InitinalGdal();
            //Sp.GetShpLayer(a.strVectorFile);
            //Sp.GetFeilds();
            //string ab = Sp.m_FeildList[0].ToString();
            //for (int i = 0; i < Sp.m_FeildList.Count; i++)
            //{
            //    string get = Sp.m_FeildList[i];
            //    //this.comboBox1.Items.Add(get );

            //    this.comboBox1.Items.Add("id:" + (i) + " " + "字段名：" + get);
            //}
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string str= comboBox1.Text;
            char myChar = str[3];
            string result = System.Text.RegularExpressions.Regex.Replace(str, @"[^0-9]+", "");
            string tSt; 
            tSt = str.Substring(str.Length - 2,1);
            string[] sArray1 = result.Split(new char[10] { '0', '1', '2' , '3', '4', '5' , '6', '7', '8', '9' });
            string[] after = result.Split(new char[] { ' ' });
            string str1 = result.Substring(0, 1);
            string piece = result.Substring(0);
            string qw = Convert.ToString(result);
            char[] adv = qw.ToArray();
            int vc= adv[0];
            Shpread ad = new Shpread();
            ad.InitinalGdal();
            ad.GetShpLayer(av);
            ad.GetFeilds();
            List<string> FeildStringList = null;
            ad.GetFeildContent(Convert.ToInt32(str1), out FeildStringList);
            for (int i = 0; i < FeildStringList.Count; i++)
            {
                listBox1.Items.Add(FeildStringList[i]);
            }
        }
 
        List<string> acc = new List<string>();
        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            OpenFileDialog fDilag = new OpenFileDialog();

            fDilag.InitialDirectory = @"H:/";
            fDilag.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fDilag.FilterIndex = 2;
            fDilag.RestoreDirectory = true;

            if (fDilag.ShowDialog() == DialogResult.OK)
            {
                av = fDilag.FileName;
                acc.Add(av);
            }
            Shpread Sp = new Shpread();
            Sp.InitinalGdal();
            Sp.GetShpLayer(av);
            Sp.GetFeilds();
            string ab = Sp.m_FeildList[0].ToString();
            for (int i = 0; i < Sp.m_FeildList.Count; i++)
            {
                string get = Sp.m_FeildList[i];
                //this.comboBox1.Items.Add(get );

                this.comboBox1.Items.Add("id:" + (i) + " " + "字段名：" + get);
            }
        }
    }
}
