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
    public partial class Point : Form
    {
    public ShpCreate returnForm1 = null;
        public Point( )
        {
            InitializeComponent();
         
        }
        ShpCreate a = new ShpCreate();

        string av = "";
        private void Point_Load(object sender, EventArgs e)
        {
            //path aaa = new path();
            //Shpread Sp = new Shpread();
            //Sp.InitinalGdal();
            //string gh = "dasd";
            //Sp.GetShpLayer(gh);
            //Sp.GetFeilds();
            //string ab=Sp.m_FeildList[0].ToString();
            //for (int i = 0; i < Sp.m_FeildList.Count; i++)
            //{
            //    DataGridViewTextBoxColumn acCode0 = new DataGridViewTextBoxColumn();
            //    acCode0.Name = Sp.m_FeildList[i];
            //    acCode0.DataPropertyName = Sp.m_FeildList[i];
            //    acCode0.HeaderText = Sp.m_FeildList[i];
            //    dataGridView1.Columns.Add(acCode0);

            //}
            //DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
            //acCode.Name = "x";
            //acCode.DataPropertyName = "x";
            //acCode.HeaderText = "x";
            //dataGridView1.Columns.Add(acCode);
            //DataGridViewTextBoxColumn acCode1 = new DataGridViewTextBoxColumn();
            //acCode1.Name = "y";
            //acCode1.DataPropertyName = "y";
            //acCode1.HeaderText = "y";
            //dataGridView1.Columns.Add(acCode1);
            //DataGridViewTextBoxColumn acCode2 = new DataGridViewTextBoxColumn();
            //acCode2.Name = "z";
            //acCode2.DataPropertyName = "z";
            //acCode2.HeaderText = "z";
            //dataGridView1.Columns.Add(acCode2);
            //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            //btn.Name = "btnModify";
            //btn.HeaderText = "删除";
            //btn.DefaultCellStyle.NullValue = "删除";
            //dataGridView1.Columns.Add(btn);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {

            int ac = dataGridView1.ColumnCount-4;
            Shpread ad = new Shpread();
           
       
            Shpread Sp = new Shpread();
            Sp.InitinalGdal();
            Sp.GetShpLayer(av);
            Sp.GetFeilds();
            Feature poFeature = new Feature(Sp.oLayer.GetLayerDefn());
            Geometry pt = new Geometry(wkbGeometryType.wkbPoint);
            DataSource oDS = Sp.oDerive.Open(av, 1);
           
            int ab = Sp.m_FeildList.Count - 1;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                //object vc = dataGridView1.Rows[i].Cells[dataGridView1.ColumnCount - 5].Value;
                //object ap = dataGridView1.Rows[i].Cells[dataGridView1.ColumnCount - 6].Value;

                //poFeature.SetField(0,Convert.ToString(vc));
              
                //poFeature.SetField(1,Convert.ToString(ap));
               
                //添加坐标点
                pt.AddPoint(Convert.ToDouble(dataGridView1.Rows[i].Cells[(dataGridView1.ColumnCount - 4)].Value), Convert.ToDouble(dataGridView1.Rows[i].Cells[(dataGridView1.ColumnCount - 4) + 1].Value), Convert.ToDouble(dataGridView1.Rows[i].Cells[(dataGridView1.ColumnCount - 4) + 2].Value));
                poFeature.SetGeometry(pt);
                //将带有坐标及属性的Feature要素点写入Layer中
                Sp.oLayer.CreateFeature(poFeature);

            }
            //poFeature.SetGeometry(pt);
            //oLayer.CreateFeature(poFeature);
            //关闭文件读写
            poFeature.Dispose();
      
            oDS.Dispose();
           
            Sp.oDerive.Dispose();
            MessageBox.Show("创建成功");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
          
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                if (column is DataGridViewButtonColumn)
                {
                    //这里可以编写你需要的任意关于按钮事件的操作~
                    //MessageBox.Show("按钮被点击");
                    //this.dataGridView1.CurrentRow.Visible=false;//隐藏当前行
                    this.dataGridView1.Rows.RemoveAt(e.RowIndex);//删除当前行
                }
            }
        }
        public DataGridViewTextBoxEditingControl CellEdit = null;
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dataGridView1.CurrentCellAddress.X == dataGridView1.ColumnCount - 4)//获取当前处于活动状态的单元格索引
            {
                CellEdit = (DataGridViewTextBoxEditingControl)e.Control;
                CellEdit.SelectAll();
                CellEdit.KeyPress += Cells_KeyPress; //绑定事件
            }
        }
        private void Cells_KeyPress(object sender, KeyPressEventArgs e) //自定义事件
        {
            int a = dataGridView1.ColumnCount - 4;

            if ((this.dataGridView1.CurrentCellAddress.X == a) || (this.dataGridView1.CurrentCellAddress.X == a+1) || (this.dataGridView1.CurrentCellAddress.X == a+2))//获取当前处于活动状态的单元格索引
            {
                if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
                if (e.KeyChar == '\b') e.Handled = false;
            }
          
        }
       
        private void button1_Click_2(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            OpenFileDialog fDilag = new OpenFileDialog();
         
            fDilag.InitialDirectory = @"H:/";
            fDilag.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fDilag.FilterIndex = 2;
            fDilag.RestoreDirectory = true;
           
            if (fDilag.ShowDialog() == DialogResult.OK)
            {
                 av = fDilag.FileName;
            }

          
            Shpread Sp = new Shpread();
            Sp.InitinalGdal();
          
            Sp.GetShpLayer(av);
            Sp.GetFeilds();
            string ab = Sp.m_FeildList[0].ToString();
            for (int i = 0; i < Sp.m_FeildList.Count; i++)
            {
                DataGridViewTextBoxColumn acCode0 = new DataGridViewTextBoxColumn();
                acCode0.Name = Sp.m_FeildList[i];
                acCode0.DataPropertyName = Sp.m_FeildList[i];
                acCode0.HeaderText = Sp.m_FeildList[i];
                dataGridView1.Columns.Add(acCode0);

            }
            DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
            acCode.Name = "x";
            acCode.DataPropertyName = "x";
            acCode.HeaderText = "x";
            dataGridView1.Columns.Add(acCode);
            DataGridViewTextBoxColumn acCode1 = new DataGridViewTextBoxColumn();
            acCode1.Name = "y";
            acCode1.DataPropertyName = "y";
            acCode1.HeaderText = "y";
            dataGridView1.Columns.Add(acCode1);
            DataGridViewTextBoxColumn acCode2 = new DataGridViewTextBoxColumn();
            acCode2.Name = "z";
            acCode2.DataPropertyName = "z";
            acCode2.HeaderText = "z";
            dataGridView1.Columns.Add(acCode2);
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = "btnModify";
            btn.HeaderText = "删除";
            btn.DefaultCellStyle.NullValue = "删除";
            dataGridView1.Columns.Add(btn);
        }
    }
}
