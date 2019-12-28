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
    public partial class Themhang : Form
    {
        public static string SetValueForText1 = "";
        String MaMH, TenMH, HangSx, Soluong, BH,DonGia;
        int ThanhTien;
        clsBanLinhKien obj = new clsBanLinhKien();
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
        public Themhang()
        {
            InitializeComponent();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            SetValueForText1 = "0";
            this.Close();
        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            string timkiem;
            timkiem = txt_Search.Text;
            if (obj.KiemTra("Select MATHANG.MaMH,TenMH,LuongTon,LoaiHang,DonGia From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH AND MATHANG.MaMH LIKE '" + timkiem + "%'") == 0)
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,LuongTon,LoaiHang,DonGia From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH");
            else
            {
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,LuongTon,LoaiHang,DonGia From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH AND MATHANG.MaMH LIKE '" + timkiem + "%'");
                TaoListView(listView1, tbl);
                return;
            }
            if (obj.KiemTra("Select MATHANG.MaMH,TenMH,LuongTon,LoaiHang,DonGia From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH AND TenMH LIKE '" + timkiem + "%'") == 0)
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,LuongTon,LoaiHang,DonGia From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH");
            else
            {
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,LuongTon,LoaiHang,DonGia From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH AND TenMH LIKE '" + timkiem + "%'");
                TaoListView(listView1, tbl);
                return;
            }
            TaoListView(listView1, tbl);
        }

        private void btn_ThemHang_Click(object sender, EventArgs e)
        {
            if (txt_SL.Text == "")
                ERROR.Text = "số lượng không được để trống";
            else if (Selected.Text == "None")
                ERROR.Text = "chưa chọn hàng";
            else if (Convert.ToInt32(obj.LayDuLieu("Select LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH AND TenMH='" + Selected.Text + "'").Rows[0][0].ToString()) < Convert.ToInt32(txt_SL.Text))
                ERROR.Text = "số lượng quá lớn";
            else if (obj.KiemTra("Select * From Cart Where MaMH='" + MaMH + "'") != 0)
                ERROR.Text = "sản phẩm đã tồn tại";
            else
            {
                Soluong = txt_SL.Text;
                ThanhTien = Convert.ToInt32(Soluong) * Convert.ToInt32(DonGia);
                ERROR.Text = "";
                obj.LayDuLieu("Insert into Cart (MaMH,TenMH,HangSx,SoLuong,BH,ThanhTien) Values('" + MaMH + "','" + TenMH + "','" + HangSx + "'," + Soluong + "," + BH + "," + ThanhTien + ")");
                SetValueForText1 = "1";
                this.Close();
            }

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

        private void Themhang_Load(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,LuongTon,LoaiHang,DonGia From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH");
            TaoListView(listView1, tbl);
            int n = obj.KiemTra("Select DISTINCT LoaiHang From MATHANG");
            DataTable tbl2 = new DataTable();
            tbl2 = obj.LayDuLieu("Select DISTINCT LoaiHang From MATHANG");
            cb_Sort.Items.Clear();
            for (int i = 0; i < n; i++)
            {
                cb_Sort.Items.Add(tbl2.Rows[i][0].ToString());
            }
        }

        private void cb_Sort_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            if (cb_Sort.Text == "All")
            {
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,LuongTon,LoaiHang,DonGia From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH");
                TaoListView(listView1, tbl);
            }
            else
            {
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,LuongTon,LoaiHang,DonGia From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH AND LoaiHang='" + cb_Sort.Text + "'");
                TaoListView(listView1, tbl);
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            Selected.Text = listView1.SelectedItems[0].SubItems[1].Text;
            MaMH = listView1.SelectedItems[0].SubItems[0].Text;
            TenMH = listView1.SelectedItems[0].SubItems[1].Text;
            DonGia = obj.LayDuLieu("Select DonGia From MATHANG Where MaMH='" + MaMH + "'").Rows[0][0].ToString();
            HangSx = obj.LayDuLieu("Select HangSx From MATHANG Where MaMH='" + MaMH + "'").Rows[0][0].ToString();
            BH = obj.LayDuLieu("Select BH From MATHANG Where MaMH='" + MaMH + "'").Rows[0][0].ToString();
        }
    }
}
