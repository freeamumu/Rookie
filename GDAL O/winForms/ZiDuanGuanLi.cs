using OSGeo.OGR;
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
    public partial class ZiDuanGuanLi : Form
    {
        public ZiDuanGuanLi()
        {
            InitializeComponent();
        }
        string av = "";
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        ShpCreate a = new ShpCreate();
        public List<string> daaa = new List<string>();
        private void ZiDuanGuanLi_Load(object sender, EventArgs e)
        {
            //Shpread Sp = new Shpread();
            //Sp.InitinalGdal();
            //Sp.GetShpLayer(a.strVectorFile);
            //Sp.GetFeilds();
            //string ab = Sp.m_FeildList[0].ToString();
            //for (int i = 0; i < Sp.m_FeildList.Count; i++)
            //{
            //    string a = Sp.m_FeildList[i];
            //    //this.comboBox1.Items.Add("id:"+(i)+" " +"字段名："+a);
            

            //}
            //ListView aq = new ListView();
            //for (int i = 0; i < Sp.m_FeildList.Count; i++)
            //{
            //    string a = Sp.m_FeildList[i];
            //    this.listBox1.Items.Add("id:" + (i) + " " + "字段名：" + a);
               


            //}
    
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        Shpread da = new Shpread();
        private void button1_Click(object sender, EventArgs e)
        {
         
           
            da.InitinalGdal();
            da.GetShpLayer(av);
            FieldDefn oFieldI = new FieldDefn(textBox1.Text, FieldType.OFTInteger);
            oFieldI.SetWidth(Convert.ToInt32(textBox2.Text));
            da.oLayer.CreateField(oFieldI, 1);
            SX();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            da.InitinalGdal();
            da.GetShpLayer(av);
            da.del(Convert.ToInt32(textBox3.Text));
            SX();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        public void SX()
        {
            listBox1.Items.Clear();
            Shpread Sp = new Shpread();
            Sp.InitinalGdal();
            Sp.GetShpLayer(av);
            Sp.GetFeilds();
            string ab = Sp.m_FeildList[0].ToString();
            for (int i = 0; i < Sp.m_FeildList.Count; i++)
            {
                string a = Sp.m_FeildList[i];
                //this.comboBox1.Items.Add("id:" + (i) + " " + "字段名：" + a);


            }
            ListView aq = new ListView();
            for (int i = 0; i < Sp.m_FeildList.Count; i++)
            {
                string a = Sp.m_FeildList[i];
                this.listBox1.Items.Add("id:" + (i) + " " + "字段名：" + a);



            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            da.InitinalGdal();
            da.GetShpLayer(av);
            da.recat(Convert.ToInt32(textBox4.Text), Convert.ToInt32(textBox5.Text));
            SX();
        }
        
       
        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            OpenFileDialog fDilag = new OpenFileDialog();

            fDilag.InitialDirectory = @"H:/";
            fDilag.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fDilag.FilterIndex = 2;
            fDilag.RestoreDirectory = true;
            
            if (fDilag.ShowDialog() == DialogResult.OK)
            {
                av = fDilag.FileName;
                daaa.Add(av);
            }
            Shpread Sp = new Shpread();
            Sp.InitinalGdal();
            Sp.GetShpLayer(av);
            Sp.GetFeilds();
            string ab = Sp.m_FeildList[0].ToString();
            for (int i = 0; i < Sp.m_FeildList.Count; i++)
            {
                string a = Sp.m_FeildList[i];
                //this.comboBox1.Items.Add("id:"+(i)+" " +"字段名："+a);


            }
            ListView aq = new ListView();
            for (int i = 0; i < Sp.m_FeildList.Count; i++)
            {
                string a = Sp.m_FeildList[i];
                this.listBox1.Items.Add("id:" + (i) + " " + "字段名：" + a);



            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
