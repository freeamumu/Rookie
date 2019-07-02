using OSGeo.GDAL;
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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        Shpread a = new Shpread();
        private void Form2_Load(object sender, EventArgs e)
        {
            
            a.InitinalGdal();
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string sShpFileName = @"H:\GDAL\中国省级行政区划_shp";
            //a.GetShpLayer(sShpFileName);
            //a.InitinalGdal();
            ////   
            //a.GetShpLayer(sShpFileName);
            //// 获取所有属性字段名称,存放在m_FeildList中  
            //a.GetFeilds();

            //List<string> FeildStringList = null;
            //a.GetFeildContent(0, out FeildStringList);

            //// 获取某条FID的数据  
            //a.GetGeometry(0);
            //MessageBox.Show(a.sCoordiantes);
            New a = new New();
            Old ac = new Old();
            ac.Sing();

        }
    }
}
