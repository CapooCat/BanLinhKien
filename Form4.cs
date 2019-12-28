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
    public partial class ThanhToan : Form
    {
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
        public ThanhToan()
        {
            InitializeComponent();
        }
        private void NumberOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void ThanhToan_Load(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            tbl = obj.LayDuLieu("Select SUM(ThanhTien) From Cart");
            lbl_TongTien.Text = "Tổng tiền: " + tbl.Rows[0][0].ToString()+"đ";
        }

        private void BanLinhKien_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_ThanhToan_Click(object sender, EventArgs e)
        {
            if (txt_Money.Text == "")
            ERROR.Text = "vui lòng nhập số tiền";
            else if ((Convert.ToInt32(txt_Money.Text) - Convert.ToInt32(obj.LayDuLieu("Select SUM(ThanhTien) From Cart").Rows[0][0].ToString())) < 0)
            {
                ERROR.Text = "Chưa đủ tiền";
            }
            else
            {
                obj.LayDuLieu("UPDATE HOADON SET TinhTrang=1 Where MaHD='" + BanLinhKien.SetValueForText1 + "'");
                DataTable tbl = new DataTable();
                tbl = obj.LayDuLieu("Select SoLuong,MaMH From Cart");
                int Countrows = obj.KiemTra("Select SoLuong,MaMH From Cart");
                for (int s = 0; s < Countrows; s++)
                {
                    string a, b;
                    a = tbl.Rows[s][0].ToString();
                    b = tbl.Rows[s][1].ToString();
                    obj.LayDuLieu("UPDATE CHITIETKHO SET LuongTon=(LuongTon-" + a + ") Where MaMH='" + b + "'");
                }
                this.Close();
            }
        }

        private void txt_Money_TextChanged(object sender, EventArgs e)
        {
            if (txt_Money.Text == "")
                lbl_TienThoi.Text = "Số tiền cần thối: 0đ";
            else if (obj.KiemTra("Select * From Cart") != 0)
                lbl_TienThoi.Text = "Số tiền cần thối: " + (Convert.ToInt32(txt_Money.Text)- Convert.ToInt32(obj.LayDuLieu("Select SUM(ThanhTien) From Cart").Rows[0][0].ToString())).ToString() + "đ";
        }
    }
}
