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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        List<ZiDuan> ad = new List<ZiDuan>();
        private void button1_Click(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
            acCode.Name = "acCode";
            acCode.DataPropertyName = "acCode";
            acCode.HeaderText = textBox1.Text;
            dataGridView1.Columns.Add(acCode);
           
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
            acCode.Name = "x";
            acCode.DataPropertyName = "acCode";
            acCode.HeaderText = "x";
            dataGridView1.Columns.Add(acCode);
            DataGridViewTextBoxColumn acCode1 = new DataGridViewTextBoxColumn();
            acCode1.Name = "y";
            acCode1.DataPropertyName = "acCode";
            acCode1.HeaderText = "y";
            dataGridView1.Columns.Add(acCode1);
            DataGridViewTextBoxColumn acCode2 = new DataGridViewTextBoxColumn();
            acCode2.Name = "z";
            acCode2.DataPropertyName = "acCode";
            acCode2.HeaderText = "z";
            dataGridView1.Columns.Add(acCode2);
           

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

        private void button2_Click(object sender, EventArgs e)
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = "btnModify";
            btn.HeaderText = "修改";
            btn.DefaultCellStyle.NullValue = "修改";
            dataGridView1.Columns.Add(btn);
            dataGridView1.Columns.Remove("acCode");


        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow item = dataGridView1.SelectedRows[0];
                DataRowView rowview = item.DataBoundItem as DataRowView;
                rowview.Row.Delete();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedColumns.Count > 0)
            {
                DataGridViewColumn dat = dataGridView1.SelectedColumns[0];
              dataGridView1.Columns.Remove("x");
                //DataRowView rowview = ite.DataBoundItem as DataRowView;
                //rowview.Row.Delete();

            }
        }
    }
}
