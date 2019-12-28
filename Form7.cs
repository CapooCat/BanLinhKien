using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BanLinhKien
{
    public partial class Form7 : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        clsBanLinhKien obj = new clsBanLinhKien();
        public Form7()
        {
            InitializeComponent();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void TaoListView(ListView lst, DataTable tbl)
        {
            ListViewItem lstItems = new ListViewItem();
            int i;
            lst.Items.Clear();
            foreach (DataRow drw in tbl.Rows)
            {
                {
                    lstItems = lst.Items.Add(drw[0].ToString());
                }
                for (i = 1; i <= tbl.Columns.Count - 1; i++)
                {
                    lstItems.SubItems.Add(drw[i].ToString());
                }
            }
        }
        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (txt_TenKho.Text == "" || txt_MaKho.Text =="")
            {
                Error.Text = "Không được để trống";
            }
            else  if (obj.KiemTra("Select * From KHO where TenKho LIKE '" + txt_TenKho.Text + "' OR MaKho LIKE '" + txt_MaKho.Text + "'") == 0)
            {
                obj.LayDuLieu("Insert into KHO(MaKho,TenKho) Values('" + txt_MaKho.Text + "','" + txt_TenKho.Text + "')");
                DataTable tbl = new DataTable();
                tbl = obj.LayDuLieu("Select MaKho,TenKho From Kho");
                TaoListView(listView1, tbl);
                txt_MaKho.Text = "";
                txt_TenKho.Text = "";
            }
            else
                Error.Text = "Kho này đã tồn tại";
        }
        private void Form7_Load(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            tbl = obj.LayDuLieu("Select MaKho,TenKho From Kho");
            TaoListView(listView1, tbl);
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (obj.KiemTra("Select * From KHO") == 1)
            {
                Error.Text = "Không thể xóa toàn bộ";
            }
            else if (obj.KiemTra("Select * From KHO where TenKho LIKE '" + txt_TenKho.Text + "' OR MaKho LIKE '" + txt_MaKho.Text + "'") != 0)
            {
                obj.LayDuLieu("Delete * From KHO where TenKho LIKE '" + txt_TenKho.Text + "' OR MaKho LIKE '" + txt_MaKho.Text + "'");
                obj.LayDuLieu("Delete * From MATHANG Where NOT EXISTS (Select * from CHITIETKHO where CHITIETKHO.MaMH=MATHANG.MaMH)");
                DataTable tbl = new DataTable();
                tbl = obj.LayDuLieu("Select MaKho,TenKho From Kho");
                TaoListView(listView1, tbl);
                txt_MaKho.Text = "";
                txt_TenKho.Text = "";
            }
            else
                Error.Text = "Kho không tồn tại";
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            txt_MaKho.Text = listView1.SelectedItems[0].SubItems[0].Text;
            txt_TenKho.Text = listView1.SelectedItems[0].SubItems[1].Text;
        }
    }
}
