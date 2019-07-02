using OSGeo.OGR;
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GDAL_O
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public Layer oLayer;
        private void button1_Click(object sender, EventArgs e)
        {
        
            OSGeo.GDAL.Gdal.SetConfigOption("GDAL_FILENAME_IS_UTF8", "YES");
            // 为了使属性表字段支持中文，请添加下面这句
            OSGeo.GDAL.Gdal.SetConfigOption("SHAPE_ENCODING", "");
            Ogr.RegisterAll();
            
            string strVectorFile = "H:\\GDAL\\po.shp";
            //创建数据，这里以创建ESRI的shp文件为例
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
            oLayer = oDS.CreateLayer("TestPolygon", null, wkbGeometryType.wkbPoint, null);
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
            
            // FieldDefn oFieldID = new FieldDefn(textBox1.Text, FieldType.OFTInteger);

            // oFieldID.SetWidth(Convert.ToInt32(textBox2.Text));
            // oLayer.CreateField(oFieldID, 1);


            //FieldDefn oFieldName = new FieldDefn("FieldName", FieldType.OFTString);
            // oFieldName.SetWidth(20);
            // oLayer.CreateField(oFieldName, 0);
            Feature poFeature = new Feature(oLayer.GetLayerDefn());
            Geometry pt = new Geometry(OSGeo.OGR.wkbGeometryType.wkbPoint);
            for (int i = 100; i < 110; i++)
            {
                //属性一"名称"赋值
                poFeature.SetField(0, i);
                //属性二"高度"赋值
                poFeature.SetField(1, i);
                //添加坐标点
                pt.AddPoint(i, i, i);
                poFeature.SetGeometry(pt);
                //将带有坐标及属性的Feature要素点写入Layer中
                oLayer.CreateFeature(poFeature);

            }
            //poFeature.SetGeometry(pt);
            //oLayer.CreateFeature(poFeature);
            //关闭文件读写
            poFeature.Dispose();
            oDS.Dispose();
            string information = OGRReadFile.GetVectorInfo(strVectorFile);
            MessageBox.Show(information);
            // string sShpFileName = @"H:\GDAL\po.shp";
           // Shpread a = new Shpread();
           // a.GetShpLayer(sShpFileName);
           // a.InitinalGdal();
           // a.GetFeilds();
           // List<string> m_FeildList = new List<string>();
           // m_FeildList.Clear();
           //// wkbGeometryType oTempGeometryType = oLayer.GetGeomType();
           // List<string> TempstringList = new List<string>();

           // //
           // FeatureDefn oDefn = oLayer.GetLayerDefn();
           // int iFieldCount = oDefn.GetFieldCount();
           // for (int iAttr = 0; iAttr < iFieldCount; iAttr++)
           // {
           //     FieldDefn oField = oDefn.GetFieldDefn(iAttr);
           //     if (null != oField)
           //     {
           //         m_FeildList.Add(oField.GetNameRef());
           //     }


           // }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "打开ShapeFile数据";
            dlg.Filter = "ShapeFile数据(*.shp)|*.shp";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Ogr.RegisterAll();

                string strVectorFile = dlg.FileName;
                textBox1.Text = strVectorFile;
                //打开数据  
                DataSource ds = Ogr.Open(strVectorFile, 0);
                if (ds == null)
                {
                    listBox1.Items.Add(string.Format("打开文件【{0}】失败！", strVectorFile));
                    return;
                }
                listBox1.Items.Add(string.Format("打开文件【{0}】成功！", strVectorFile));

                // 获取该数据源中的图层个数，一般shp数据图层只有一个，如果是mdb、dxf等图层就会有多个  
                int iLayerCount = ds.GetLayerCount();

                // 获取第一个图层  
                Layer oLayer = ds.GetLayerByIndex(0);
                if (oLayer == null)
                {
                    listBox1.Items.Add(string.Format("获取第{0}个图层失败！\n", 0));
                    return;
                }

                // 对图层进行初始化，如果对图层进行了过滤操作，执行这句后，之前的过滤全部清空  
                oLayer.ResetReading();

                // 通过属性表的SQL语句对图层中的要素进行筛选，这部分详细参考SQL查询章节内容  
                //oLayer.SetAttributeFilter("\"NAME99\"LIKE \"北京市市辖区\"");

                // 通过指定的几何对象对图层中的要素进行筛选  
                //oLayer.SetSpatialFilter();  

                // 通过指定的四至范围对图层中的要素进行筛选  
                //oLayer.SetSpatialFilterRect();  

                // 获取图层中的属性表表头并输出  
                listBox1.Items.Add("属性表结构信息：");
                FeatureDefn oDefn = oLayer.GetLayerDefn();
                int iFieldCount = oDefn.GetFieldCount();
                for (int iAttr = 0; iAttr < iFieldCount; iAttr++)
                {
                    FieldDefn oField = oDefn.GetFieldDefn(iAttr);

                    listBox1.Items.Add(string.Format("{0}:{1} ({2}.{3})", oField.GetNameRef(),
                             oField.GetFieldTypeName(oField.GetFieldType()),
                             oField.GetWidth(), oField.GetPrecision()));
                }
                // 输出图层中的要素个数  
                listBox1.Items.Add(string.Format("要素个数 = {0}", oLayer.GetFeatureCount(0)));
                Feature oFeature = null;
                // 下面开始遍历图层中的要素  
                while ((oFeature = oLayer.GetNextFeature()) != null)
                {
                    Geometry geo = oFeature.GetGeometryRef();
                    wkbGeometryType wkb = geo.GetGeometryType();
                    listBox1.Items.Add(string.Format("当前处理第要素值：{0}", wkb.ToString()));
                    string strGml = geo.ExportToGML();
                    listBox1.Items.Add(strGml);
                    listBox1.Items.Add(string.Format("当前处理第{0}个: \n属性值：", oFeature.GetFID()));

                    // 获取要素中的属性表内容  
                    for (int iField = 0; iField < iFieldCount; iField++)
                    {
                        FieldDefn oFieldDefn = oDefn.GetFieldDefn(iField);
                        FieldType type = oFieldDefn.GetFieldType();
                        switch (type)
                        {
                            case FieldType.OFTString:
                                listBox1.Items.Add(string.Format("{0}\t", oFeature.GetFieldAsString(iField)));
                                break;
                            case FieldType.OFTReal:
                                listBox1.Items.Add(string.Format("{0}\t", oFeature.GetFieldAsDouble(iField)));
                                break;
                            case FieldType.OFTInteger:
                                listBox1.Items.Add(string.Format("{0}\t", oFeature.GetFieldAsInteger(iField)));
                                break;
                            default:
                                listBox1.Items.Add(string.Format("{0}\t", oFeature.GetFieldAsString(iField)));
                                break;
                        }
                    }
                    // 获取要素中的几何体  
                    Geometry oGeometry = oFeature.GetGeometryRef();
                    // 为了演示，只输出一个要素信息  
                    break;
                }
                listBox1.Items.Add("数据集关闭！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListViewItem ib = new ListViewItem();
            ib.Text = textBox1.Text;
            ib.SubItems.Add(textBox2.Text);

            ib.SubItems.Add(comboBox1.Text);
            this.listView1.Items.Add(ib);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.listView1.Columns.Add("字段名", 65, HorizontalAlignment.Center);
            this.listView1.Columns.Add("字段长度", 65, HorizontalAlignment.Center);
            this.listView1.Columns.Add("字段类型", 150, HorizontalAlignment.Center);
            this.listView1.View = System.Windows.Forms.View.Details;
            FieldDefn oFieldID = new FieldDefn(textBox1.Text, FieldType.OFTInteger);
            Array role = Enum.GetValues(typeof(FieldType));
            string[] rolee = Enum.GetNames(typeof(FieldType));
           foreach(string a in rolee)
            {
                comboBox1.Items.Add(a);
            }
           
           
        }
        private string pathname = string.Empty;
        private void button4_Click(object sender, EventArgs e)
        {
            string sShpFileName = @"H:\GDAL\po.shp";
            Shpread a = new Shpread();
            a.InitinalGdal();
            a.GetShpLayer(sShpFileName);
            //bool c=  a.GetGeometry(Convert.ToInt32(textBox3.Text));
            a.GetGeometry(int.Parse(textBox4.Text));
            textBox3.Text= a.a;
           
            
         
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        ArrayList polygons = new ArrayList();
        ArrayList polylines = new ArrayList();
        ArrayList points = new ArrayList();
        Pen pen = new Pen(Color.Blue, 2);
        int ShapeType;
        int count;
        double xmin, ymin, xmax, ymax;
        double n1, n2;

        private void button6_Click(object sender, EventArgs e)
        {
            double width = xmax - xmin;
            double height = ymax - ymin;
            n1 = (float)(this.pictureBox1.Width * 0.9 / width);//x轴放大倍数
            n2 = (float)(this.pictureBox1.Height * 0.9 / height);//y轴放大倍数
            this.pictureBox1.Refresh();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.colorDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                pen.Color = this.colorDialog1.Color;
                //this.button3.BackColor = pen.Color;
            }
            this.pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            PointF[] point;

            switch (ShapeType)
            {
                case 1:
                    foreach (Point_shape p in points)
                    {
                        PointF pp = new PointF();
                        pp.X = (float)(10 + (p.X - xmin) * n1);
                        pp.Y = (float)(10 + (p.Y - ymin) * n2);
                        e.Graphics.DrawEllipse(pen, pp.X, pp.Y, 1.5f, 1.5f);
                    }
                    break;
                case 3:
                    foreach (PolyLine_shape p in polylines)
                    {

                        for (int i = 0; i < p.NumParts; i++)
                        {
                            int startpoint;
                            int endpoint;
                            point = null;
                            if (i == p.NumParts - 1)
                            {
                                startpoint = (int)p.Parts[i];
                                endpoint = p.NumPoints;
                            }
                            else
                            {
                                startpoint = (int)p.Parts[i];
                                endpoint = (int)p.Parts[i + 1];
                            }
                            point = new PointF[endpoint - startpoint];
                            for (int k = 0, j = startpoint; j < endpoint; j++, k++)
                            {
                                Point_shape ps = (Point_shape)p.Points[j];
                                point[k].X = (float)(10 + (ps.X - xmin) * n1);
                                point[k].Y = (float)(10 + (ps.Y - ymin) * n2);
                            }
                            e.Graphics.DrawLines(pen, point);
                        }
                    }
                    break;
                case 5:
                    foreach (Polygon_shape p in polygons)
                    {
                        for (int i = 0; i < p.NumParts; i++)
                        {
                            int startpoint;
                            int endpoint;
                            point = null;
                            if (i == p.NumParts - 1)
                            {
                                startpoint = (int)p.Parts[i];
                                endpoint = p.NumPoints;
                            }
                            else
                            {
                                startpoint = (int)p.Parts[i];
                                endpoint = (int)p.Parts[i + 1];
                            }
                            point = new PointF[endpoint - startpoint];
                            for (int k = 0, j = startpoint; j < endpoint; j++, k++)
                            {
                                Point_shape ps = (Point_shape)p.Points[j];
                                point[k].X = (float)(10 + (ps.X - xmin) * n1);
                                point[k].Y = (float)(10 + (ps.Y - ymin) * n2);
                            }
                            e.Graphics.DrawPolygon(pen, point);
                        }
                    }
                    break;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
          
            string sShpFileName = @"H:\GDAL\po.shp";
            Shpread a = new Shpread();
            a.InitinalGdal();
            a.GetShpLayer(sShpFileName);
            a.del(int.Parse(textBox5.Text));
            
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            string sShpFileName = @"H:\GDAL\po.shp";
            Shpread a = new Shpread();
            a.InitinalGdal();
            a.GetShpLayer(sShpFileName);
            a.recat(int.Parse(textBox7.Text),int.Parse(textBox6.Text));
            
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void 新建shp文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 a = new Form1();
            ShpCreate ab = new ShpCreate();
            ab.Show();
        }

        private void 打开shp文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        struct Polygon_shape
        {
            public double Box1; //边界盒
            public double Box2; //边界盒
            public double Box3; //边界盒
            public double Box4; //边界盒
            public int NumParts; //部分的数目
            public int NumPoints; //点的总数目
            public ArrayList Parts; //在部分中第一个点的索引
            public ArrayList Points; //所有部分的点
        }
        struct Point_shape
        {
            public double X;
            public double Y;
        }
        struct PolyLine_shape
        {
            public double[] Box; //边界盒
            public int NumParts; //部分的数目
            public int NumPoints; //点的总数目
            public ArrayList Parts; //在部分中第一个点的索引
            public ArrayList Points; //所有部分的点
        }
        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                BinaryReader br = new BinaryReader(openFileDialog1.OpenFile());
                //读取文件过程

                br.ReadBytes(24);
                int FileLength = br.ReadInt32();//<0代表数据长度未知
                int FileBanben = br.ReadInt32();
                ShapeType = br.ReadInt32();
                xmin = br.ReadDouble();
                ymax = -1 * br.ReadDouble();
                xmax = br.ReadDouble();
                ymin = -1 * br.ReadDouble();
                double width = xmax - xmin;
                double height = ymax - ymin;
                n1 = (float)(this.pictureBox1.Width * 0.9 / width);//x轴放大倍数
                n2 = (float)(this.pictureBox1.Height * 0.9 / height);//y轴放大倍数
                br.ReadBytes(32);

                switch (ShapeType)
                {
                    case 1:
                        points.Clear();
                        while (br.PeekChar() != -1)
                        {
                            Point_shape point = new Point_shape();

                            uint RecordNum = br.ReadUInt32();
                            int DataLength = br.ReadInt32();


                            //读取第i个记录
                            br.ReadInt32();
                            point.X = br.ReadDouble();
                            point.Y = -1 * br.ReadDouble();
                            points.Add(point);
                        }
                        StreamWriter sw = new StreamWriter("point.txt");

                        foreach (Point_shape p in points)
                        {
                            sw.WriteLine("{0},{1},{2} ", p.X, -1 * p.Y, 0);
                        }
                        sw.Close();
                        break;
                    case 3:
                        polylines.Clear();
                        while (br.PeekChar() != -1)
                        {
                            PolyLine_shape polyline = new PolyLine_shape();
                            polyline.Box = new double[4];
                            polyline.Parts = new ArrayList();
                            polyline.Points = new ArrayList();

                            uint RecordNum = br.ReadUInt32();
                            int DataLength = br.ReadInt32();

                            //读取第i个记录
                            br.ReadInt32();
                            polyline.Box[0] = br.ReadDouble();
                            polyline.Box[1] = br.ReadDouble();
                            polyline.Box[2] = br.ReadDouble();
                            polyline.Box[3] = br.ReadDouble();
                            polyline.NumParts = br.ReadInt32();
                            polyline.NumPoints = br.ReadInt32();
                            for (int i = 0; i < polyline.NumParts; i++)
                            {
                                int parts = new int();
                                parts = br.ReadInt32();
                                polyline.Parts.Add(parts);
                            }
                            for (int j = 0; j < polyline.NumPoints; j++)
                            {

                                Point_shape pointtemp = new Point_shape();
                                pointtemp.X = br.ReadDouble();
                                pointtemp.Y = -1 * br.ReadDouble();
                                polyline.Points.Add(pointtemp);
                            }
                            polylines.Add(polyline);
                        }
                        StreamWriter sw2 = new StreamWriter("line.txt");
                        count = 1;
                        foreach (PolyLine_shape p in polylines)
                        {

                            for (int i = 0; i < p.NumParts; i++)
                            {
                                int startpoint;
                                int endpoint;
                                if (i == p.NumParts - 1)
                                {
                                    startpoint = (int)p.Parts[i];
                                    endpoint = p.NumPoints;
                                }
                                else
                                {
                                    startpoint = (int)p.Parts[i];
                                    endpoint = (int)p.Parts[i + 1];
                                }
                                sw2.WriteLine("线" + count.ToString() + ":");
                                for (int k = 0, j = startpoint; j < endpoint; j++, k++)
                                {
                                    Point_shape ps = (Point_shape)p.Points[j];
                                    sw2.WriteLine("    {0},{1},{2} ", ps.X, -1 * ps.Y, 0);
                                }
                                count++;
                            }
                        }
                        sw2.Close();
                        break;
                    case 5:
                        polygons.Clear();
                        while (br.PeekChar() != -1)
                        {
                            Polygon_shape polygon = new Polygon_shape();
                            polygon.Parts = new ArrayList();
                            polygon.Points = new ArrayList();

                            uint RecordNum = br.ReadUInt32();
                            int DataLength = br.ReadInt32();

                            //读取第i个记录
                            int m = br.ReadInt32();
                            polygon.Box1 = br.ReadDouble();
                            polygon.Box2 = br.ReadDouble();
                            polygon.Box3 = br.ReadDouble();
                            polygon.Box4 = br.ReadDouble();
                            polygon.NumParts = br.ReadInt32();
                            polygon.NumPoints = br.ReadInt32();
                            for (int j = 0; j < polygon.NumParts; j++)
                            {
                                int parts = new int();
                                parts = br.ReadInt32();
                                polygon.Parts.Add(parts);
                            }
                            for (int j = 0; j < polygon.NumPoints; j++)
                            {
                                Point_shape pointtemp = new Point_shape();
                                pointtemp.X = br.ReadDouble();
                                pointtemp.Y = -1 * br.ReadDouble();
                                polygon.Points.Add(pointtemp);
                            }
                            polygons.Add(polygon);
                        }
                        StreamWriter sw1 = new StreamWriter("polygon.txt");
                        count = 1;
                        foreach (Polygon_shape p in polygons)
                        {
                            for (int i = 0; i < p.NumParts; i++)
                            {
                                int startpoint;
                                int endpoint;
                                if (i == p.NumParts - 1)
                                {
                                    startpoint = (int)p.Parts[i];
                                    endpoint = p.NumPoints;
                                }
                                else
                                {
                                    startpoint = (int)p.Parts[i];
                                    endpoint = (int)p.Parts[i + 1];
                                }
                                sw1.WriteLine("多边形" + count.ToString() + ":");
                                for (int k = 0, j = startpoint; j < endpoint; j++, k++)
                                {
                                    Point_shape ps = (Point_shape)p.Points[j];
                                    sw1.WriteLine("    {0},{1},{2} ", ps.X, -1 * ps.Y, 0);
                                }
                                count++;
                            }
                        }
                        sw1.Close();
                        break;
                }
                br.Close();
            }
        }

    }
}
