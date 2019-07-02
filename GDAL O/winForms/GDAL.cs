using System;
using System.Collections;
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
    public partial class GDAL : Form
    {
        public GDAL()
        {
            InitializeComponent();
        }

        //private void shp文件创立ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    SaveFileDialog sfd = new SaveFileDialog();
        //    sfd.Filter = "文本文件(*.txt)|*.shp|所有文件|*.*";//设置文件类型
        //    sfd.FileName = "po";//设置默认文件名
        //    sfd.DefaultExt = "shp";//设置默认格式（可以不设）
        //    sfd.AddExtension = true;//设置自动在文件名中添加扩展名
        //    if (sfd.ShowDialog() == DialogResult.OK)
        //    {
        //        //txtPath.Text = "FileName:" + sfd.FileName + "\r\n";
        //        using (StreamWriter sw = new StreamWriter(sfd.FileName))
        //        {
        //            sw.WriteLineAsync("今天是个好天气");
        //        }
        //    }
        //   string localFilePath = sfd.FileName.ToString();
        //    MessageBox.Show("ok");
        //}

        private void 创建字段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShpCreate SP = new ShpCreate();
            SP.Show();
        }

        private void 创建点要素ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point a = new Point();
            a.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
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
        ArrayList polygons = new ArrayList();
        ArrayList polylines = new ArrayList();
        ArrayList points = new ArrayList();
        Pen pen = new Pen(Color.Blue, 2);
        int ShapeType;
        int count;
        double xmin, ymin, xmax, ymax;

        private void 更换颜色ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void 字段管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZiDuanGuanLi ad = new ZiDuanGuanLi();
            ad.Show();
        }

        private void 字段查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZiDuanChaXun A = new ZiDuanChaXun();
            A.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void GDAL_Load(object sender, EventArgs e)
        {
            pictureBox1.MouseWheel += new MouseEventHandler(pictureBox1_MouseWheel);
        }
        void pictureBox1_MouseWheel(object sender, MouseEventArgs e)

        {

            this.pictureBox1.Width += e.Delta;

            this.pictureBox1.Height += e.Delta;
            this.pictureBox1.Refresh();


        }
        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double width = xmax - xmin;
            double height = ymax - ymin;
            n1 = (float)(this.pictureBox1.Width * 0.9 / width);//x轴放大倍数
            n2 = (float)(this.pictureBox1.Height * 0.9 / height);//y轴放大倍数
            this.pictureBox1.Refresh();
        }

        double n1, n2;
        private void 打开shp文件ToolStripMenuItem_Click(object sender, EventArgs e)
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
            this.pictureBox1.Refresh();
        }
    }
}
