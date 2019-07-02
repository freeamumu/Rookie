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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = @"所有文件|*.*";
            ofd.Title = "打开图层";
            ofd.ShowDialog();
            string filename = ofd.FileName;
            if (filename == null)
            {
                MessageBox.Show("路径不能为空");
                return;
            }
            string path = System.IO.Path.GetDirectoryName(ofd.FileName);
            shp a = new shp();
            string sShpFileName = @"H:\GDAL\中国省级行政区划_shp";
            a.GetShpLayer(sShpFileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OSGeo.GDAL.Gdal.SetConfigOption("GDAL_FILENAME_IS_UTF8", "YES");
            // 为了使属性表字段支持中文，请添加下面这句
            OSGeo.GDAL.Gdal.SetConfigOption("SHAPE_ENCODING", "");

            string strVectorFile = "H:\\GDAL\\中国省级行政区划_shp";

            // 注册所有的驱动
            Ogr.RegisterAll();

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

            // 创建图层，创建一个多边形图层，这里没有指定空间参考，如果需要的话，需要在这里进行指定
            Layer oLayer = oDS.CreateLayer("TestPolygon", null, wkbGeometryType.wkbPolygon, null);
            if (oLayer == null)
            {
                Console.WriteLine("图层创建失败！\n");
                return;
            }

            // 下面创建属性表
            // 先创建一个叫FieldID的整型属性
            FieldDefn oFieldID = new FieldDefn("FieldID", FieldType.OFTInteger);
            oLayer.CreateField(oFieldID, 1);

            // 再创建一个叫FeatureName的字符型属性，字符长度为50
            FieldDefn oFieldName = new FieldDefn("FieldName", FieldType.OFTString);
            oFieldName.SetWidth(100);
            oLayer.CreateField(oFieldName, 1);

            FeatureDefn oDefn = oLayer.GetLayerDefn();

            // 创建三角形要素
            Feature oFeatureTriangle = new Feature(oDefn);
            oFeatureTriangle.SetField(0, 0);
            oFeatureTriangle.SetField(1, "三角形");
            Geometry geomTriangle = Geometry.CreateFromWkt("POLYGON ((0 0,20 0,10 15,0 0))");
            oFeatureTriangle.SetGeometry(geomTriangle);

            oLayer.CreateFeature(oFeatureTriangle);

            // 创建矩形要素
            Feature oFeatureRectangle = new Feature(oDefn);
            oFeatureRectangle.SetField(0, 1);
            oFeatureRectangle.SetField(1, "矩形");
            Geometry geomRectangle = Geometry.CreateFromWkt("POLYGON ((30 0,60 0,60 30,30 30,30 0))");
            oFeatureRectangle.SetGeometry(geomRectangle);

            oLayer.CreateFeature(oFeatureRectangle);

            // 创建岛要素
            Feature oFeatureHole = new Feature(oDefn);
            oFeatureHole.SetField(0, 1);
            oFeatureHole.SetField(1, "环岛测试");
            //Geometry geomWYX = Geometry.CreateFromWkt("POLYGON ((30 0,60 0,60 30,30 30,30 0))");
            OSGeo.OGR.Geometry outGeo = new OSGeo.OGR.Geometry(OSGeo.OGR.wkbGeometryType.wkbLinearRing);
            outGeo.AddPoint(40, -30, 0);
            outGeo.AddPoint(60, -30, 0);
            outGeo.AddPoint(60, -10, 0);
            outGeo.AddPoint(40, -10, 0);

            OSGeo.OGR.Geometry inGeo = new OSGeo.OGR.Geometry(OSGeo.OGR.wkbGeometryType.wkbLinearRing);
            inGeo.AddPoint(45, -25, 0);
            inGeo.AddPoint(55, -25, 0);
            inGeo.AddPoint(55, -15, 0);
            inGeo.AddPoint(45, -15, 0);

            OSGeo.OGR.Geometry geo = new OSGeo.OGR.Geometry(OSGeo.OGR.wkbGeometryType.wkbPolygon);
            geo.AddGeometryDirectly(outGeo);
            geo.AddGeometryDirectly(inGeo);
            oFeatureHole.SetGeometry(geo);
            oLayer.CreateFeature(oFeatureHole);

            // 创建Multi要素
            Feature oFeatureMulty = new Feature(oDefn);
            oFeatureMulty.SetField(0, 1);
            oFeatureMulty.SetField(1, "MultyPart测试");
            OSGeo.OGR.Geometry geo1 = new OSGeo.OGR.Geometry(OSGeo.OGR.wkbGeometryType.wkbLinearRing);
            geo1.AddPoint(25, -10, 0);
            geo1.AddPoint(5, -10, 0);
            geo1.AddPoint(5, -30, 0);
            geo1.AddPoint(25, -30, 0);
            OSGeo.OGR.Geometry poly1 = new Geometry(wkbGeometryType.wkbPolygon);
            poly1.AddGeometryDirectly(geo1);

            OSGeo.OGR.Geometry geo2 = new OSGeo.OGR.Geometry(OSGeo.OGR.wkbGeometryType.wkbLinearRing);
            geo2.AddPoint(0, -15, 0);
            geo2.AddPoint(-5, -15, 0);
            geo2.AddPoint(-5, -20, 0);
            geo2.AddPoint(0, -20, 0);

            OSGeo.OGR.Geometry poly2 = new Geometry(wkbGeometryType.wkbPolygon);
            poly2.AddGeometryDirectly(geo2);

            OSGeo.OGR.Geometry geoMulty = new OSGeo.OGR.Geometry(OSGeo.OGR.wkbGeometryType.wkbMultiPolygon);
            geoMulty.AddGeometryDirectly(poly1);
            geoMulty.AddGeometryDirectly(poly2);
            oFeatureMulty.SetGeometry(geoMulty);

            oLayer.CreateFeature(oFeatureMulty);




            Console.WriteLine("\n数据集创建完成！\n");

        }

        private void button3_Click(object sender, EventArgs e)
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
        }
    }

