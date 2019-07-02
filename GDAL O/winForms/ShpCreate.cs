using OSGeo.OGR;
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

namespace GDAL_O
{
    public partial class ShpCreate : Form
    {
        public ShpCreate()
        {
            
            InitializeComponent();
            
        }

        private void ShpCreate_Load(object sender, EventArgs e)
        {
            OSGeo.GDAL.Gdal.SetConfigOption("GDAL_FILENAME_IS_UTF8", "YES");
            // 为了使属性表字段支持中文，请添加下面这句
            OSGeo.GDAL.Gdal.SetConfigOption("SHAPE_ENCODING", "");
            Ogr.RegisterAll();
            InitListView(this.listView1);


            //this.listView1.Columns.Add("字段名", 65, HorizontalAlignment.Center);
            //this.listView1.Columns.Add("字段长度", 65, HorizontalAlignment.Center);
            //this.listView1.View = System.Windows.Forms.View.Details;
        }

        private void ShpCreate_Shown(object sender, EventArgs e)
        {
            
        }
        public void InitListView(ListView lv)
        {
            //添加列名
            ColumnHeader c1 = new ColumnHeader();
            c1.Width = 100;
            c1.Text = "字段";
            ColumnHeader c2 = new ColumnHeader();
            c2.Width = 100;
            c2.Text = "长度";
            ColumnHeader c3 = new ColumnHeader();
            c3.Width = 100;
            c3.Text = "x";
            ColumnHeader c4 = new ColumnHeader();
            c4.Width = 100;
            c4.Text = "y";
            ColumnHeader c5 = new ColumnHeader();
            c5.Width = 100;
            c5.Text = "z";

      
            lv.GridLines = true;  //显示网格线
            lv.FullRowSelect = true;  //显示全行
            lv.MultiSelect = false;  //设置只能单选
            lv.View = View.Details;  //设置显示模式为详细
            lv.HoverSelection = true;  //当鼠标停留数秒后自动选择
            //把列名添加到listview中
            lv.Columns.Add(c1);
            lv.Columns.Add(c2);


        }
        public ListView InsertListView(ListView lv)
        {
            //获取文本框中的值
            string name = this.textBox1.Text;
            string sex = this.textBox2.Text;
           
            //创建行对象
            ListViewItem li = new ListViewItem(name);
            //添加同一行的数据
            li.SubItems.Add(sex);
           
            //将行对象绑定在listview对象中
            lv.Items.Add(li);

            MessageBox.Show("新增数据成功！");
            return lv;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("字段名不能为空");
            }
            else {
                ListViewItem ib = new ListViewItem();
                ib.Text = textBox1.Text;
                ib.SubItems.Add(textBox2.Text);


                this.listView1.Items.Add(ib);
                ColumnHeader c1 = new ColumnHeader();
                c1.Width = 100;
                c1.Text = textBox1.Text;

                ColumnHeader c2 = new ColumnHeader();
                c2.Width = 100;
                c2.Text = textBox2.Text;


                textBox1.Clear();
                textBox2.Clear();
            }
           
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
                listView1.Items.Remove(listView1.SelectedItems[0]);
            //string a = listView1.SelectedItems[0].ToString();
            //ColumnHeader c = new ColumnHeader();
            
            //listView2.Columns.Remove(c);

           
            textBox1.Clear();
            textBox2.Clear();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {

                //把选择的信息显示在相应的文本框中
                this.textBox1.Text = this.listView1.SelectedItems[0].SubItems[0].Text;
                this.textBox2.Text = this.listView1.SelectedItems[0].SubItems[1].Text;
                
            }
        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Shpread Create = new Shpread();
            Create.InitinalGdal();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        public Layer oLayer;
      public  string strVectorFile = "";
        public List<string> dabb=new List<string>();
        public string a;
        public path bb;
        Shpread pc = new Shpread();
      
        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "文本文件(*.txt)|*.shp|所有文件|*.*";//设置文件类型
            sfd.FileName = "po";//设置默认文件名
            sfd.DefaultExt = "shp";//设置默认格式（可以不设）
           
            sfd.AddExtension = true;//设置自动在文件名中添加扩展名
            string localFilePat = "";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //获得文件路径 
              string  localFilePath = sfd.FileName.ToString();
                localFilePat = localFilePath;
              


            }
            strVectorFile = localFilePat;
            MessageBox.Show(localFilePat);
            dabb.Add(localFilePat);
          
            pc.path.Add(localFilePat);
            pc.sdsad(localFilePat);
            //bb.pat = localFilePat;
            a = localFilePat;
            string strDriverName = "ESRI Shapefile";
            int count = Ogr.GetDriverCount();
            Driver oDriver = Ogr.GetDriverByName(strDriverName);
            if (oDriver == null)
            {
                Console.WriteLine("%s 驱动不可用！\n", strVectorFile);
                return;
            }

            // 创建数据源
            DataSource oDS = oDriver.CreateDataSource(strVectorFile, null);
            if (oDS == null)
            {
                Console.WriteLine("创建矢量文件【%s】失败！\n", strVectorFile);
                return;
            }
            oLayer = oDS.CreateLayer(textBox3.Text, null, wkbGeometryType.wkbPoint, null);
            if (oLayer == null)
            {

                Console.WriteLine("图层创建失败！\n");
                return;
            }
            // 下面创建属性表
            // 先创建一个叫FieldID的整型属性
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                FieldDefn oFieldI = new FieldDefn(listView1.Items[i].SubItems[0].Text, FieldType.OFTInteger);

                oFieldI.SetWidth(Convert.ToInt32(listView1.Items[i].SubItems[1].Text));
                oLayer.CreateField(oFieldI, 1);

            }
            //Feature poFeature = new Feature(oLayer.GetLayerDefn());
            //Geometry pt = new Geometry(OSGeo.OGR.wkbGeometryType.wkbPoint);
            //for (int i = 100; i < 110; i++)
            //{
            //    //属性一"名称"赋值
            //    poFeature.SetField(0, i);
            //    //属性二"高度"赋值
            //    poFeature.SetField(1, i);
            //    //添加坐标点
            //    pt.AddPoint(i, i, i);
            //    poFeature.SetGeometry(pt);
            //    //将带有坐标及属性的Feature要素点写入Layer中
            //    oLayer.CreateFeature(poFeature);

            //}

            //关闭文件读写
            //poFeature.Dispose();
            oDS.Dispose();
            oDriver.Dispose();
 
            //string information = OGRReadFile.GetVectorInfo(strVectorFile);
            MessageBox.Show("创建成功！");

        }
       
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else
            {
              
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
  //          int iMax = 100;//首先设置上限值
  //if (textBox2.Text != null && textBox2.Text != "")//判断TextBox的内容不为空，如果不判断会导致后面的非数字对比异常
  //              if (int.Parse(textBox2.Text) > iMax)//num就是传进来的值,如果大于上限（输入的值），那就强制为上限-1，或者就是上限值？
  //             {
  //          textBox2.Text = (iMax - 1).ToString();
  //            }
                    }

        private void button3_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "文本文件(*.shp)|*.txt|所有文件|*.*";//设置文件类型
            sfd.FileName = "保存";//设置默认文件名
            sfd.DefaultExt = "txt";//设置默认格式（可以不设）
            sfd.AddExtension = true;//设置自动在文件名中添加扩展名
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //textBox1.Text = "FileName:" + sfd.FileName + "\r\n";
                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                {
                    sw.WriteLineAsync("今天是个好天气");
                }
            }
            MessageBox.Show("ok");

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
         
        }
    }
    
}
