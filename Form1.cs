using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace BanLinhKien
{
    public partial class BanLinhKien : Form
    {
        public static string SetValueForText1 = "0";
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
        int TogMove;
        int MValX;
        int MValY;
        int a = 0;
        int switches = 0;
        clsBanLinhKien obj = new clsBanLinhKien();
        public BanLinhKien()
        {
            InitializeComponent();
            obj.LayDuLieu("Delete * From Cart");
            Login frm = new Login();
            frm.TopMost = true;
            frm.ShowDialog();
            btn_Log.Text = Login.SetValueForText1;
            tab_BaoCao.SelectedTab = tab2_BaoCao;
            btn_DoanhThu.BackColor = Color.White;
            btn_DoanhThu.ForeColor = Color.Black;
            btn_DoanhThu.FlatAppearance.MouseOverBackColor = Color.White;
            btn_DoanhThu.FlatAppearance.MouseDownBackColor = Color.White;
            Pressed16.Visible = true;
            tab_KhoHang.SelectedTab = tab1_KhoHang;
            btn_NhapKho.BackColor = Color.White;
            btn_NhapKho.ForeColor = Color.Black;
            btn_NhapKho.FlatAppearance.MouseOverBackColor = Color.White;
            btn_NhapKho.FlatAppearance.MouseDownBackColor = Color.White;
            Pressed7.Visible = true;
            tab_BanHang.SelectedTab = tab1_BanHang;
            button11.BackColor = Color.White;
            button11.ForeColor = Color.Black;
            button11.FlatAppearance.MouseOverBackColor = Color.White;
            button11.FlatAppearance.MouseDownBackColor = Color.White;
            Pressed10.Visible = true;
            tab_CongNo.SelectedTab = tab1_CongNo;
            btn_NoPhaiThu.BackColor = Color.White;
            btn_NoPhaiThu.ForeColor = Color.Black;
            btn_NoPhaiThu.FlatAppearance.MouseOverBackColor = Color.White;
            btn_NoPhaiThu.FlatAppearance.MouseDownBackColor = Color.White;
            Pressed13.Visible = true;
            TabControl.SelectedTab = tabPage1;
            btn_TongQuan.BackColor = Color.DimGray;
            btn_TongQuan.FlatAppearance.MouseOverBackColor = Color.DimGray;
            Pressed1.Visible = true;
            tab_SuaThongTin.SelectedTab = tab1_SuaThongTin;
        }
        private void BanLinhKien_Load(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH");
            DataTable tbl1 = new DataTable();
            tbl1 = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH");
            DataTable tbl4 = new DataTable();
            tbl4 = obj.LayDuLieu("Select * From KHACHHANG");
            DataTable tbl5 = new DataTable();
            tbl5 = obj.LayDuLieu("Select TOP 10 TenKH,CHITIETHOADON.MaMH,TenMH,HangSx,SoLuong,SDT,(DonGia*SoLuong) AS THANHTIEN,LoaiHang,NgayLap From KHACHHANG,HOADON,CHITIETHOADON,MATHANG Where HOADON.MaHD=CHITIETHOADON.MaHD AND KHACHHANG.MaKH=HOADON.MaKH AND MATHANG.MaMH=CHITIETHOADON.MaMH AND TinhTrang=1 ORDER BY NgayLap DESC");
            DataTable tbl6 = new DataTable();
            tbl6 = obj.LayDuLieu("Select HOADON.MaHD,TenKH,NgayLap,SUM(DonGia*SoLuong) From KHACHHANG,HOADON,CHITIETHOADON,MATHANG Where HOADON.MaHD=CHITIETHOADON.MaHD AND KHACHHANG.MaKH=HOADON.MaKH AND MATHANG.MaMH=CHITIETHOADON.MaMH Group by TenKH,NgayLap,HOADON.MaHD");
            DataTable tbl7 = new DataTable();
            tbl7 = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,SUM(SoLuong),SUM(DonGia*SoLuong) From MATHANG,CHITIETHOADON,HOADON Where HOADON.MaHD=CHITIETHOADON.MaHD AND MATHANG.MaMH=CHITIETHOADON.MaMH AND TinhTrang=1 Group by MATHANG.MaMH,TenMH");
            DataTable tbl8 = new DataTable();
            tbl8 = obj.LayDuLieu("Select KHACHHANG.TenKH,KHACHHANG.SDT,KHACHHANG.DIACHI,BAOCAOCONGNO.TongNo,BAOCAOCONGNO.DaTra,BAOCAOCONGNO.NgayLap from KHACHHANG inner join BAOCAOCONGNO on KHACHHANG.MaKH=BAOCAOCONGNO.MaKH");
            DataTable tbl9 = new DataTable();
            tbl9 = obj.LayDuLieu("Select TenNCC,SDT,DiaChi,TongNo,DaTra From NHACUNGCAP,BAOCAOCONGNO where NHACUNGCAP.MaNCC=BAOCAOCONGNO.MaNCC");
            if (Login.SetValueForText2 == "1")
            {
                DataTable tbl10 = new DataTable();
                tbl10 = obj.LayDuLieu("Select TAIKHOAN.MaTK,TenNV,User,LoaiTk From TAIKHOAN,NHANVIEN WHERE TAIKHOAN.MaTK=NHANVIEN.MaTK");
                TaoListView(listView2, tbl10);
            }
            TaoListView(lst_view1, tbl);
            TaoListView(lst_view2, tbl);
            TaoListView(lst_View3, tbl1);
            TaoListView(listView9, tbl4);
            TaoListView(lst_View, tbl5);
            TaoListView(listView8, tbl6);
            TaoListView(listView6, tbl7);
            TaoListView(listView3, tbl8);
            TaoListView(listView4, tbl9);
            int n = obj.KiemTra("Select TenKho From KHO");
            DataTable tbl2 = new DataTable();
            tbl2 = obj.LayDuLieu("Select TenKho From KHO");
            cb_TenKho.Items.Clear();
            cb_TenKhoFix.Items.Clear();
            for (int i = 0; i < n; i++)
            {
                cb_TenKho.Items.Add(tbl2.Rows[i][0].ToString());
                cb_TenKhoFix.Items.Add(tbl2.Rows[i][0].ToString());
            }
            int n1 = obj.KiemTra("Select MaHD From HOADON");
            DataTable tbl3 = new DataTable();
            tbl3 = obj.LayDuLieu("Select MaHD From HOADON");
            cb_DonHang.Items.Clear();
            for (int i = 0; i < n1; i++)
            {
                cb_DonHang.Items.Add(tbl3.Rows[i][0].ToString());
            }
            int n3 = obj.KiemTra("Select DISTINCT YEAR(NgayLap) From BAOCAOCONGNO");
            DataTable tbl_nam = new DataTable();
            tbl_nam = obj.LayDuLieu("Select DISTINCT YEAR(NgayLap) From BAOCAOCONGNO");
            cb_ChonNam.Items.Clear();
            for (int i = 0; i < n3; i++)
            {
                cb_ChonNam.Items.Add(tbl_nam.Rows[i][0].ToString());
            }
            int n4 = obj.KiemTra("Select DISTINCT YEAR(NgayLap) From HOADON,CHITIETHOADON Where HOADON.MaHD=CHITIETHOADON.MaHD");
            DataTable tbl_nam1 = new DataTable();
            tbl_nam1 = obj.LayDuLieu("Select DISTINCT YEAR(NgayLap) From HOADON,CHITIETHOADON Where HOADON.MaHD=CHITIETHOADON.MaHD");
            cb_ChonNam1.Items.Clear();
            for (int i = 0; i < n4; i++)
            {
                cb_ChonNam1.Items.Add(tbl_nam1.Rows[i][0].ToString());
            }
            cbo_Nam2.Items.Clear();
            for (int i = 0; i < n3; i++)
            {
                cbo_Nam2.Items.Add(tbl_nam.Rows[i][0].ToString());
            }
            txt_TongKhach.Text = obj.LayDuLieu("Select Count(*) From KHACHHANG").Rows[0][0].ToString();
            txt_SL.Text = obj.LayDuLieu("Select SUM(SoLuong) From CHITIETHOADON,HOADON Where CHITIETHOADON.MaHD=HOADON.MaHD AND TinhTrang=1").Rows[0][0].ToString();
            txt_DonHang.Text = obj.LayDuLieu("Select Count(*) From HOADON").Rows[0][0].ToString();
            txt_DoanhSo.Text = obj.LayDuLieu("Select SUM(DonGia*SoLuong) From CHITIETHOADON,MATHANG,HOADON Where HOADON.MaHD=CHITIETHOADON.MaHD AND CHITIETHOADON.MaMH=MATHANG.MaMH AND TinhTrang=1").Rows[0][0].ToString() + "đ";
            lbl_TongDS.Text = "Tổng tiền: "+obj.LayDuLieu("Select SUM(DonGia * SoLuong) From CHITIETHOADON, MATHANG, HOADON Where HOADON.MaHD = CHITIETHOADON.MaHD AND CHITIETHOADON.MaMH = MATHANG.MaMH AND TinhTrang = 1").Rows[0][0].ToString() +"đ";
        }
        private void btn_Reset_Click(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH");
            DataTable tbl1 = new DataTable();
            tbl1 = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH");
            DataTable tbl4 = new DataTable();
            tbl4 = obj.LayDuLieu("Select * From KHACHHANG");
            DataTable tbl5 = new DataTable();
            tbl5 = obj.LayDuLieu("Select TOP 10 TenKH,CHITIETHOADON.MaMH,TenMH,HangSx,SoLuong,SDT,(DonGia*SoLuong) AS THANHTIEN,LoaiHang,NgayLap From KHACHHANG,HOADON,CHITIETHOADON,MATHANG Where HOADON.MaHD=CHITIETHOADON.MaHD AND KHACHHANG.MaKH=HOADON.MaKH AND MATHANG.MaMH=CHITIETHOADON.MaMH AND TinhTrang=1 ORDER BY NgayLap DESC");
            DataTable tbl6 = new DataTable();
            tbl6 = obj.LayDuLieu("Select HOADON.MaHD,TenKH,NgayLap,SUM(DonGia*SoLuong) From KHACHHANG,HOADON,CHITIETHOADON,MATHANG Where HOADON.MaHD=CHITIETHOADON.MaHD AND KHACHHANG.MaKH=HOADON.MaKH AND MATHANG.MaMH=CHITIETHOADON.MaMH Group by TenKH,NgayLap,HOADON.MaHD");
            DataTable tbl7 = new DataTable();
            tbl7 = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,SUM(SoLuong),SUM(DonGia*SoLuong) From MATHANG,CHITIETHOADON,HOADON Where HOADON.MaHD=CHITIETHOADON.MaHD AND MATHANG.MaMH=CHITIETHOADON.MaMH AND TinhTrang=1 Group by MATHANG.MaMH,TenMH");
            DataTable tbl8 = new DataTable();
            tbl8 = obj.LayDuLieu("Select KHACHHANG.TenKH,KHACHHANG.SDT,KHACHHANG.DIACHI,BAOCAOCONGNO.TongNo,BAOCAOCONGNO.DaTra,BAOCAOCONGNO.NgayLap from KHACHHANG inner join BAOCAOCONGNO on KHACHHANG.MaKH=BAOCAOCONGNO.MaKH");
            DataTable tbl9 = new DataTable();
            tbl9 = obj.LayDuLieu("Select TenNCC,SDT,DiaChi,TongNo,DaTra From NHACUNGCAP,BAOCAOCONGNO where NHACUNGCAP.MaNCC=BAOCAOCONGNO.MaNCC");
            if (Login.SetValueForText2 == "1")
            {
                DataTable tbl11 = new DataTable();
                tbl11 = obj.LayDuLieu("Select TAIKHOAN.MaTK,TenNV,User,LoaiTk From TAIKHOAN,NHANVIEN WHERE TAIKHOAN.MaTK=NHANVIEN.MaTK");
                TaoListView(listView2, tbl11);
            }
            TaoListView(lst_view1, tbl);
            TaoListView(lst_view2, tbl);
            TaoListView(lst_View3, tbl1);
            TaoListView(listView9, tbl4);
            TaoListView(lst_View, tbl5);
            TaoListView(listView8, tbl6);
            TaoListView(listView6, tbl7);
            TaoListView(listView3, tbl8);
            TaoListView(listView4, tbl9);
            int n = obj.KiemTra("Select TenKho From KHO");
            DataTable tbl2 = new DataTable();
            tbl2 = obj.LayDuLieu("Select TenKho From KHO");
            cb_TenKho.Items.Clear();
            cb_TenKhoFix.Items.Clear();
            for (int i = 0; i < n; i++)
            {
                cb_TenKho.Items.Add(tbl2.Rows[i][0].ToString());
                cb_TenKhoFix.Items.Add(tbl2.Rows[i][0].ToString());
            }
            int n1 = obj.KiemTra("Select MaHD From HOADON");
            DataTable tbl3 = new DataTable();
            tbl3 = obj.LayDuLieu("Select MaHD From HOADON");
            cb_DonHang.Items.Clear();
            for (int i = 0; i < n1; i++)
            {
                cb_DonHang.Items.Add(tbl3.Rows[i][0].ToString());
            }
            int n3 = obj.KiemTra("Select DISTINCT YEAR(NgayLap) From BAOCAOCONGNO");
            DataTable tbl_nam = new DataTable();
            tbl_nam = obj.LayDuLieu("Select DISTINCT YEAR(NgayLap) From BAOCAOCONGNO");
            cb_ChonNam.Items.Clear();
            for (int i = 0; i < n3; i++)
            {
                cb_ChonNam.Items.Add(tbl_nam.Rows[i][0].ToString());
            }
            int n4 = obj.KiemTra("Select DISTINCT YEAR(NgayLap) From HOADON,CHITIETHOADON Where HOADON.MaHD=CHITIETHOADON.MaHD");
            DataTable tbl_nam1 = new DataTable();
            tbl_nam1 = obj.LayDuLieu("Select DISTINCT YEAR(NgayLap) From HOADON,CHITIETHOADON Where HOADON.MaHD=CHITIETHOADON.MaHD");
            cb_ChonNam1.Items.Clear();
            for (int i = 0; i < n4; i++)
            {
                cb_ChonNam1.Items.Add(tbl_nam1.Rows[i][0].ToString());
            }
            cbo_Nam2.Items.Clear();
            for (int i = 0; i < n3; i++)
            {
                cbo_Nam2.Items.Add(tbl_nam.Rows[i][0].ToString());
            }
            txt_TongKhach.Text = obj.LayDuLieu("Select Count(*) From KHACHHANG").Rows[0][0].ToString();
            txt_SL.Text = obj.LayDuLieu("Select SUM(SoLuong) From CHITIETHOADON").Rows[0][0].ToString();
            txt_DonHang.Text = obj.LayDuLieu("Select Count(*) From HOADON").Rows[0][0].ToString();
            txt_DoanhSo.Text = obj.LayDuLieu("Select SUM(DonGia*SoLuong) From CHITIETHOADON,MATHANG Where CHITIETHOADON.MaMH=MATHANG.MaMH").Rows[0][0].ToString() + "đ";
            DataTable tbl10 = new DataTable();
            ListViewItem lstItems = new ListViewItem();
            listView1.Items.Clear();
            tbl10 = obj.LayDuLieu("Select * From Cart");
            int n6 = obj.KiemTra("Select * From Cart");
            int l = 1;
            foreach (DataRow drw in tbl10.Rows)
            {
                if (l <= n6)
                {
                    lstItems = listView1.Items.Add(l.ToString());
                    l++;
                    for (int j = 0; j <= tbl10.Columns.Count - 1; j++)
                    {
                        lstItems.SubItems.Add(drw[j].ToString());
                    }
                }
            }
            txt_MaHoaDon.Text = "";
            txt_MaKH.Text = "";
            txt_TenKhachHang.Text = "";
            txt_DiaChi.Text = "";
            txt_SDT.Text = "";
            txt_GhiChu.Text = "";
            obj.LayDuLieu("Delete * From Cart");
            listView1.Items.Clear();
        }

        private void btn_out2_Click(object sender, EventArgs e)
        {
            tab_ChinhSua.SelectedTab = tab1_ChinhSua;
        }
        private void tab4_ChinhSua_Click(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void tab1_BanHang_Click(object sender, EventArgs e)
        {

        }
        private void tabPage4_Click(object sender, EventArgs e)
        {

        }
        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }
        private void TopPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private void tabPage6_Click(object sender, EventArgs e)
        {

        }
        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_TongQuan_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = tabPage1;
            btn_TongQuan.BackColor = Color.DimGray;
            btn_TongQuan.FlatAppearance.MouseOverBackColor = Color.DimGray;
            Pressed1.Visible = true;
            btn_KhoHang.BackColor = Color.Gray;
            btn_ThuChi.BackColor = Color.Gray;
            btn_BaoCao.BackColor = Color.Gray;
            btn_BanHang.BackColor = Color.Gray;
            btn_CaiDat.BackColor = Color.Gray;
            Pressed2.Visible = false;
            Pressed3.Visible = false;
            Pressed4.Visible = false;
            Pressed5.Visible = false;
            Pressed6.Visible = false;
            btn_BaoCao.FlatAppearance.MouseOverBackColor = default(Color);
            btn_BanHang.FlatAppearance.MouseOverBackColor = default(Color);
            btn_CaiDat.FlatAppearance.MouseOverBackColor = default(Color);
            btn_KhoHang.FlatAppearance.MouseOverBackColor = default(Color);
            btn_ThuChi.FlatAppearance.MouseOverBackColor = default(Color);
            btn_XoaROW.BackColor = Color.Gray;
            btn_XoaROW.Enabled = false;
        }
        private void btn_KhoHang_Click(object sender, EventArgs e)
        {
            ;
            TabControl.SelectedTab = tabPage2;
            btn_KhoHang.BackColor = Color.DimGray;
            btn_KhoHang.FlatAppearance.MouseOverBackColor = Color.DimGray;
            Pressed2.Visible = true;
            btn_TongQuan.BackColor = Color.Gray;
            btn_ThuChi.BackColor = Color.Gray;
            btn_BaoCao.BackColor = Color.Gray;
            btn_BanHang.BackColor = Color.Gray;
            btn_CaiDat.BackColor = Color.Gray;
            Pressed1.Visible = false;
            Pressed3.Visible = false;
            Pressed4.Visible = false;
            Pressed5.Visible = false;
            Pressed6.Visible = false;
            btn_BaoCao.FlatAppearance.MouseOverBackColor = default(Color);
            btn_BanHang.FlatAppearance.MouseOverBackColor = default(Color);
            btn_CaiDat.FlatAppearance.MouseOverBackColor = default(Color);
            btn_TongQuan.FlatAppearance.MouseOverBackColor = default(Color);
            btn_ThuChi.FlatAppearance.MouseOverBackColor = default(Color);
            btn_XoaROW.BackColor = Color.Gray;
            btn_XoaROW.Enabled = false;
        }
        private void btn_ThuChi_Click(object sender, EventArgs e)
        {
                TabControl.SelectedTab = tabPage3;
                btn_ThuChi.BackColor = Color.DimGray;
                btn_ThuChi.FlatAppearance.MouseOverBackColor = Color.DimGray;
                Pressed3.Visible = true;
                btn_KhoHang.BackColor = Color.Gray;
                btn_TongQuan.BackColor = Color.Gray;
                btn_BaoCao.BackColor = Color.Gray;
                btn_BanHang.BackColor = Color.Gray;
                btn_CaiDat.BackColor = Color.Gray;
                Pressed1.Visible = false;
                Pressed2.Visible = false;
                Pressed4.Visible = false;
                Pressed5.Visible = false;
                Pressed6.Visible = false;
                btn_KhoHang.FlatAppearance.MouseOverBackColor = default(Color);
                btn_BaoCao.FlatAppearance.MouseOverBackColor = default(Color);
                btn_BanHang.FlatAppearance.MouseOverBackColor = default(Color);
                btn_CaiDat.FlatAppearance.MouseOverBackColor = default(Color);
                btn_TongQuan.FlatAppearance.MouseOverBackColor = default(Color);
                btn_XoaROW.BackColor = Color.Gray;
                btn_XoaROW.Enabled = false;
        }
        private void btn_BaoCao_Click(object sender, EventArgs e)
        {
                TabControl.SelectedTab = tabPage4;
                btn_BaoCao.BackColor = Color.DimGray;
                btn_BaoCao.FlatAppearance.MouseOverBackColor = Color.DimGray;
                Pressed4.Visible = true;
                btn_KhoHang.BackColor = Color.Gray;
                btn_TongQuan.BackColor = Color.Gray;
                btn_ThuChi.BackColor = Color.Gray;
                btn_BanHang.BackColor = Color.Gray;
                btn_CaiDat.BackColor = Color.Gray;
                Pressed1.Visible = false;
                Pressed2.Visible = false;
                Pressed3.Visible = false;
                Pressed5.Visible = false;
                Pressed6.Visible = false;
                btn_KhoHang.FlatAppearance.MouseOverBackColor = default(Color);
                btn_ThuChi.FlatAppearance.MouseOverBackColor = default(Color);
                btn_BanHang.FlatAppearance.MouseOverBackColor = default(Color);
                btn_CaiDat.FlatAppearance.MouseOverBackColor = default(Color);
                btn_TongQuan.FlatAppearance.MouseOverBackColor = default(Color);
                btn_XoaROW.BackColor = Color.Gray;
                btn_XoaROW.Enabled = false;
        }
        private void btn_BanHang_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = tabPage5;
            btn_BanHang.BackColor = Color.DimGray;
            btn_BanHang.FlatAppearance.MouseOverBackColor = Color.DimGray;
            Pressed5.Visible = true;
            btn_KhoHang.BackColor = Color.Gray;
            btn_TongQuan.BackColor = Color.Gray;
            btn_ThuChi.BackColor = Color.Gray;
            btn_BaoCao.BackColor = Color.Gray;
            btn_CaiDat.BackColor = Color.Gray;
            Pressed1.Visible = false;
            Pressed2.Visible = false;
            Pressed3.Visible = false;
            Pressed4.Visible = false;
            Pressed6.Visible = false;
            btn_KhoHang.FlatAppearance.MouseOverBackColor = default(Color);
            btn_ThuChi.FlatAppearance.MouseOverBackColor = default(Color);
            btn_BaoCao.FlatAppearance.MouseOverBackColor = default(Color);
            btn_CaiDat.FlatAppearance.MouseOverBackColor = default(Color);
            btn_TongQuan.FlatAppearance.MouseOverBackColor = default(Color);
            btn_XoaROW.BackColor = Color.Gray;
            btn_XoaROW.Enabled = false;
        }
        private void btn_CaiDat_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = tabPage6;
            btn_CaiDat.BackColor = Color.DimGray;
            btn_CaiDat.FlatAppearance.MouseOverBackColor = Color.DimGray;
            Pressed6.Visible = true;
            btn_KhoHang.BackColor = Color.Gray;
            btn_TongQuan.BackColor = Color.Gray;
            btn_ThuChi.BackColor = Color.Gray;
            btn_BaoCao.BackColor = Color.Gray;
            btn_BanHang.BackColor = Color.Gray;
            Pressed1.Visible = false;
            Pressed2.Visible = false;
            Pressed3.Visible = false;
            Pressed4.Visible = false;
            Pressed5.Visible = false;
            btn_KhoHang.FlatAppearance.MouseOverBackColor = default(Color);
            btn_ThuChi.FlatAppearance.MouseOverBackColor = default(Color);
            btn_BaoCao.FlatAppearance.MouseOverBackColor = default(Color);
            btn_BanHang.FlatAppearance.MouseOverBackColor = default(Color);
            btn_TongQuan.FlatAppearance.MouseOverBackColor = default(Color);
            btn_XoaROW.BackColor = Color.Gray;
            btn_XoaROW.Enabled = false;
        }
        private void btn_NhapKho_Click(object sender, EventArgs e)
        {
            tab_KhoHang.SelectedTab = tab1_KhoHang;
            btn_NhapKho.BackColor = Color.White;
            btn_NhapKho.ForeColor = Color.Black;
            btn_NhapKho.FlatAppearance.MouseOverBackColor = Color.White;
            btn_NhapKho.FlatAppearance.MouseDownBackColor = Color.White;
            Pressed7.Visible = true;
            btn_CapNhat.BackColor = Color.MediumSeaGreen;
            btn_XemKho.BackColor = Color.MediumOrchid;
            Pressed8.Visible = false;
            Pressed9.Visible = false;
            btn_CapNhat.ForeColor = Color.White;
            btn_XemKho.ForeColor = Color.White;
            btn_CapNhat.FlatAppearance.MouseOverBackColor = Color.MediumSeaGreen;
            btn_XemKho.FlatAppearance.MouseOverBackColor = Color.MediumOrchid;
            btn_CapNhat.FlatAppearance.MouseDownBackColor = Color.MediumSeaGreen;
            btn_XemKho.FlatAppearance.MouseDownBackColor = Color.MediumOrchid;
        }
        private void btn_CapNhat_Click(object sender, EventArgs e)
        {
            tab_KhoHang.SelectedTab = tab2_KhoHang;
            btn_CapNhat.BackColor = Color.White;
            btn_CapNhat.ForeColor = Color.Black;
            btn_CapNhat.FlatAppearance.MouseOverBackColor = Color.White;
            btn_CapNhat.FlatAppearance.MouseDownBackColor = Color.White;
            Pressed8.Visible = true;
            btn_NhapKho.BackColor = Color.DodgerBlue;
            btn_XemKho.BackColor = Color.MediumOrchid;
            Pressed7.Visible = false;
            Pressed9.Visible = false;
            btn_NhapKho.ForeColor = Color.White;
            btn_XemKho.ForeColor = Color.White;
            btn_NhapKho.FlatAppearance.MouseOverBackColor = Color.DodgerBlue;
            btn_XemKho.FlatAppearance.MouseOverBackColor = Color.MediumOrchid;
            btn_NhapKho.FlatAppearance.MouseDownBackColor = Color.DodgerBlue;
            btn_XemKho.FlatAppearance.MouseDownBackColor = Color.MediumOrchid;
        }
        private void btn_XemKho_Click(object sender, EventArgs e)
        {
            tab_KhoHang.SelectedTab = tab3_KhoHang;
            btn_XemKho.BackColor = Color.White;
            btn_XemKho.ForeColor = Color.Black;
            btn_XemKho.FlatAppearance.MouseOverBackColor = Color.White;
            btn_XemKho.FlatAppearance.MouseDownBackColor = Color.White;
            Pressed9.Visible = true;
            btn_NhapKho.BackColor = Color.DodgerBlue;
            btn_CapNhat.BackColor = Color.MediumSeaGreen;
            Pressed7.Visible = false;
            Pressed8.Visible = false;
            btn_NhapKho.ForeColor = Color.White;
            btn_CapNhat.ForeColor = Color.White;
            btn_NhapKho.FlatAppearance.MouseOverBackColor = Color.DodgerBlue;
            btn_CapNhat.FlatAppearance.MouseOverBackColor = Color.MediumSeaGreen;
            btn_NhapKho.FlatAppearance.MouseDownBackColor = Color.DodgerBlue;
            btn_CapNhat.FlatAppearance.MouseDownBackColor = Color.MediumSeaGreen;
        }
        private void button11_Click(object sender, EventArgs e)
        {
            tab_BanHang.SelectedTab = tab1_BanHang;
            button11.BackColor = Color.White;
            button11.ForeColor = Color.Black;
            button11.FlatAppearance.MouseOverBackColor = Color.White;
            button11.FlatAppearance.MouseDownBackColor = Color.White;
            Pressed10.Visible = true;
            btn_DonHang.BackColor = Color.Crimson;
            Pressed12.Visible = false;
            btn_DonHang.ForeColor = Color.White;
            btn_DonHang.FlatAppearance.MouseOverBackColor = Color.Crimson;
            btn_DonHang.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btn_XoaROW.BackColor = Color.Gray;
            btn_XoaROW.Enabled = false;
        }
        private void btn_NoPhaiThu_Click(object sender, EventArgs e)
        {
            tab_CongNo.SelectedTab = tab1_CongNo;
            btn_NoPhaiThu.BackColor = Color.White;
            btn_NoPhaiThu.ForeColor = Color.Black;
            btn_NoPhaiThu.FlatAppearance.MouseOverBackColor = Color.White;
            btn_NoPhaiThu.FlatAppearance.MouseDownBackColor = Color.White;
            Pressed13.Visible = true;
            btn_NoPhaiTra.BackColor = Color.DarkTurquoise;
            Pressed14.Visible = false;
            btn_NoPhaiTra.ForeColor = Color.White;
            btn_NoPhaiTra.FlatAppearance.MouseOverBackColor = Color.DarkTurquoise;
            btn_NoPhaiTra.FlatAppearance.MouseDownBackColor = Color.DarkTurquoise;
        }
        private void btn_NoPhaiTra_Click(object sender, EventArgs e)
        {
            tab_CongNo.SelectedTab = tab2_CongNo;
            btn_NoPhaiTra.BackColor = Color.White;
            btn_NoPhaiTra.ForeColor = Color.Black;
            btn_NoPhaiTra.FlatAppearance.MouseOverBackColor = Color.White;
            btn_NoPhaiTra.FlatAppearance.MouseDownBackColor = Color.White;
            Pressed14.Visible = true;
            btn_NoPhaiThu.BackColor = Color.Purple;
            Pressed13.Visible = false;
            btn_NoPhaiThu.ForeColor = Color.White;
            btn_NoPhaiThu.FlatAppearance.MouseOverBackColor = Color.Purple;
            btn_NoPhaiThu.FlatAppearance.MouseDownBackColor = Color.Purple;
        }
        private void btn_LoiNhuan_Click(object sender, EventArgs e)
        {
            btn_DoanhThu.BackColor = Color.DeepPink;
            btn_KhachHang.BackColor = Color.Blue;
            btn_TongDonHang.BackColor = Color.DarkViolet;
            PressedFinal.Visible = false;
            Pressed16.Visible = false;
            Pressed17.Visible = false;
            btn_DoanhThu.ForeColor = Color.White;
            btn_KhachHang.ForeColor = Color.White;
            btn_TongDonHang.ForeColor = Color.White;
            btn_DoanhThu.FlatAppearance.MouseOverBackColor = Color.DeepPink;
            btn_KhachHang.FlatAppearance.MouseOverBackColor = Color.Blue;
            btn_TongDonHang.FlatAppearance.MouseOverBackColor = Color.DarkViolet;
            btn_DoanhThu.FlatAppearance.MouseDownBackColor = Color.DeepPink;
            btn_KhachHang.FlatAppearance.MouseDownBackColor = Color.Blue;
            btn_TongDonHang.FlatAppearance.MouseDownBackColor = Color.DarkViolet;
        }
        private void btn_DoanhThu_Click(object sender, EventArgs e)
        {
            tab_BaoCao.SelectedTab = tab2_BaoCao;
            btn_DoanhThu.BackColor = Color.White;
            btn_DoanhThu.ForeColor = Color.Black;
            btn_DoanhThu.FlatAppearance.MouseOverBackColor = Color.White;
            btn_DoanhThu.FlatAppearance.MouseDownBackColor = Color.White;
            Pressed16.Visible = true;
            btn_KhachHang.BackColor = Color.Blue;
            btn_TongDonHang.BackColor = Color.DarkViolet;
            PressedFinal.Visible = false;
            Pressed17.Visible = false;
            btn_KhachHang.ForeColor = Color.White;
            btn_TongDonHang.ForeColor = Color.White;
            btn_KhachHang.FlatAppearance.MouseOverBackColor = Color.Blue;
            btn_TongDonHang.FlatAppearance.MouseOverBackColor = Color.DarkViolet;
            btn_KhachHang.FlatAppearance.MouseDownBackColor = Color.Blue;
            btn_TongDonHang.FlatAppearance.MouseDownBackColor = Color.DarkViolet;
        }
        private void btn_TongDonHang_Click_1(object sender, EventArgs e)
        {
            tab_BaoCao.SelectedTab = tab3_BaoCao;
            btn_TongDonHang.BackColor = Color.White;
            btn_TongDonHang.ForeColor = Color.Black;
            btn_TongDonHang.FlatAppearance.MouseOverBackColor = Color.White;
            btn_TongDonHang.FlatAppearance.MouseDownBackColor = Color.White;
            Pressed17.Visible = true;
            btn_DoanhThu.BackColor = Color.DeepPink;
            btn_KhachHang.BackColor = Color.Blue;
            Pressed16.Visible = false;
            PressedFinal.Visible = false;
            btn_DoanhThu.ForeColor = Color.White;
            btn_KhachHang.ForeColor = Color.White;
            btn_DoanhThu.FlatAppearance.MouseOverBackColor = Color.DeepPink;
            btn_KhachHang.FlatAppearance.MouseOverBackColor = Color.Blue;
            btn_DoanhThu.FlatAppearance.MouseDownBackColor = Color.DeepPink;
            btn_KhachHang.FlatAppearance.MouseDownBackColor = Color.Blue;
        }
        private void btn_KhachHang_Click(object sender, EventArgs e)
        {
            tab_BaoCao.SelectedTab = tab4_BaoCao;
            btn_KhachHang.BackColor = Color.White;
            btn_KhachHang.ForeColor = Color.Black;
            btn_KhachHang.FlatAppearance.MouseOverBackColor = Color.White;
            btn_KhachHang.FlatAppearance.MouseDownBackColor = Color.White;
            PressedFinal.Visible = true;
            btn_DoanhThu.BackColor = Color.DeepPink;
            btn_TongDonHang.BackColor = Color.DarkViolet;
            Pressed16.Visible = false;
            Pressed17.Visible = false;
            btn_DoanhThu.ForeColor = Color.White;
            btn_TongDonHang.ForeColor = Color.White;
            btn_DoanhThu.FlatAppearance.MouseOverBackColor = Color.DeepPink;
            btn_TongDonHang.FlatAppearance.MouseOverBackColor = Color.DarkViolet;
            btn_DoanhThu.FlatAppearance.MouseDownBackColor = Color.DeepPink;
            btn_TongDonHang.FlatAppearance.MouseDownBackColor = Color.DarkViolet;
        }
        private void btn_LinkTongDonHang_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = tabPage4;
            btn_BaoCao.BackColor = Color.DimGray;
            btn_BaoCao.FlatAppearance.MouseOverBackColor = Color.DimGray;
            Pressed4.Visible = true;
            btn_KhoHang.BackColor = Color.Gray;
            btn_TongQuan.BackColor = Color.Gray;
            btn_ThuChi.BackColor = Color.Gray;
            btn_BanHang.BackColor = Color.Gray;
            btn_CaiDat.BackColor = Color.Gray;
            Pressed1.Visible = false;
            Pressed2.Visible = false;
            Pressed3.Visible = false;
            Pressed5.Visible = false;
            Pressed6.Visible = false;
            btn_KhoHang.FlatAppearance.MouseOverBackColor = default(Color);
            btn_ThuChi.FlatAppearance.MouseOverBackColor = default(Color);
            btn_BanHang.FlatAppearance.MouseOverBackColor = default(Color);
            btn_CaiDat.FlatAppearance.MouseOverBackColor = default(Color);
            btn_TongQuan.FlatAppearance.MouseOverBackColor = default(Color);
            tab_BaoCao.SelectedTab = tab3_BaoCao;
            btn_TongDonHang.BackColor = Color.White;
            btn_TongDonHang.ForeColor = Color.Black;
            btn_TongDonHang.FlatAppearance.MouseOverBackColor = Color.White;
            btn_TongDonHang.FlatAppearance.MouseDownBackColor = Color.White;
            Pressed17.Visible = true;
            btn_DoanhThu.BackColor = Color.DeepPink;
            btn_KhachHang.BackColor = Color.Blue;
            Pressed16.Visible = false;
            PressedFinal.Visible = false;
            btn_DoanhThu.ForeColor = Color.White;
            btn_KhachHang.ForeColor = Color.White;
            btn_DoanhThu.FlatAppearance.MouseOverBackColor = Color.DeepPink;
            btn_KhachHang.FlatAppearance.MouseOverBackColor = Color.Blue;
            btn_DoanhThu.FlatAppearance.MouseDownBackColor = Color.DeepPink;
            btn_KhachHang.FlatAppearance.MouseDownBackColor = Color.Blue;
        }
        private void btn_DonHang_Click(object sender, EventArgs e)
        {
            tab_BanHang.SelectedTab = tab3_BanHang;
            btn_DonHang.BackColor = Color.White;
            btn_DonHang.ForeColor = Color.Black;
            btn_DonHang.FlatAppearance.MouseOverBackColor = Color.White;
            btn_DonHang.FlatAppearance.MouseDownBackColor = Color.White;
            Pressed12.Visible = true;
            button11.BackColor = Color.MediumOrchid;
            Pressed10.Visible = false;
            button11.ForeColor = Color.White;
            button11.FlatAppearance.MouseOverBackColor = Color.MediumOrchid;
            button11.FlatAppearance.MouseDownBackColor = Color.MediumOrchid;
            btn_XoaROW.BackColor = Color.Gray;
            btn_XoaROW.Enabled = false;
        }
        private void btn_DoanhThuTQ_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = tabPage4;
            btn_BaoCao.BackColor = Color.DimGray;
            btn_BaoCao.FlatAppearance.MouseOverBackColor = Color.DimGray;
            Pressed4.Visible = true;
            btn_KhoHang.BackColor = Color.Gray;
            btn_TongQuan.BackColor = Color.Gray;
            btn_ThuChi.BackColor = Color.Gray;
            btn_BanHang.BackColor = Color.Gray;
            btn_CaiDat.BackColor = Color.Gray;
            Pressed1.Visible = false;
            Pressed2.Visible = false;
            Pressed3.Visible = false;
            Pressed5.Visible = false;
            Pressed6.Visible = false;
            btn_KhoHang.FlatAppearance.MouseOverBackColor = default(Color);
            btn_ThuChi.FlatAppearance.MouseOverBackColor = default(Color);
            btn_BanHang.FlatAppearance.MouseOverBackColor = default(Color);
            btn_CaiDat.FlatAppearance.MouseOverBackColor = default(Color);
            btn_TongQuan.FlatAppearance.MouseOverBackColor = default(Color);
            tab_BaoCao.SelectedTab = tab2_BaoCao;
            btn_DoanhThu.BackColor = Color.White;
            btn_DoanhThu.ForeColor = Color.Black;
            btn_DoanhThu.FlatAppearance.MouseOverBackColor = Color.White;
            btn_DoanhThu.FlatAppearance.MouseDownBackColor = Color.White;
            Pressed16.Visible = true;
            btn_KhachHang.BackColor = Color.Blue;
            btn_TongDonHang.BackColor = Color.DarkViolet;
            PressedFinal.Visible = false;
            Pressed17.Visible = false;
            btn_KhachHang.ForeColor = Color.White;
            btn_TongDonHang.ForeColor = Color.White;
            btn_KhachHang.FlatAppearance.MouseOverBackColor = Color.Blue;
            btn_TongDonHang.FlatAppearance.MouseOverBackColor = Color.DarkViolet;
            btn_KhachHang.FlatAppearance.MouseDownBackColor = Color.Blue;
            btn_TongDonHang.FlatAppearance.MouseDownBackColor = Color.DarkViolet;
        }
        private void btn_SoLuongBan_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = tabPage4;
            btn_BaoCao.BackColor = Color.DimGray;
            btn_BaoCao.FlatAppearance.MouseOverBackColor = Color.DimGray;
            Pressed4.Visible = true;
            btn_KhoHang.BackColor = Color.Gray;
            btn_TongQuan.BackColor = Color.Gray;
            btn_ThuChi.BackColor = Color.Gray;
            btn_BanHang.BackColor = Color.Gray;
            btn_CaiDat.BackColor = Color.Gray;
            Pressed1.Visible = false;
            Pressed2.Visible = false;
            Pressed3.Visible = false;
            Pressed5.Visible = false;
            Pressed6.Visible = false;
            btn_KhoHang.FlatAppearance.MouseOverBackColor = default(Color);
            btn_ThuChi.FlatAppearance.MouseOverBackColor = default(Color);
            btn_BanHang.FlatAppearance.MouseOverBackColor = default(Color);
            btn_CaiDat.FlatAppearance.MouseOverBackColor = default(Color);
            btn_TongQuan.FlatAppearance.MouseOverBackColor = default(Color);
            tab_BaoCao.SelectedTab = tab2_BaoCao;
            btn_DoanhThu.BackColor = Color.White;
            btn_DoanhThu.ForeColor = Color.Black;
            btn_DoanhThu.FlatAppearance.MouseOverBackColor = Color.White;
            btn_DoanhThu.FlatAppearance.MouseDownBackColor = Color.White;
            Pressed16.Visible = true;
            btn_KhachHang.BackColor = Color.Blue;
            btn_TongDonHang.BackColor = Color.DarkViolet;
            PressedFinal.Visible = false;
            Pressed17.Visible = false;
            btn_KhachHang.ForeColor = Color.White;
            btn_TongDonHang.ForeColor = Color.White;
            btn_KhachHang.FlatAppearance.MouseOverBackColor = Color.Blue;
            btn_TongDonHang.FlatAppearance.MouseOverBackColor = Color.DarkViolet;
            btn_KhachHang.FlatAppearance.MouseDownBackColor = Color.Blue;
            btn_TongDonHang.FlatAppearance.MouseDownBackColor = Color.DarkViolet;
        }
        private void txt_DoanhSo_TextChanged(object sender, EventArgs e)
        {

        }
        private void txt_DonHang_TextChanged(object sender, EventArgs e)
        {

        }
        private void txt_SL_TextChanged(object sender, EventArgs e)
        {

        }
        private void txt_TongKhach_TextChanged(object sender, EventArgs e)
        {

        }
        private void Disable_MouseClick(object sender, MouseEventArgs e)
        {
            label10.Focus();
        }
        private void BanLinhKien_MouseDown(object sender, MouseEventArgs e)
        {
            TogMove = 1;
            MValX = e.X;
            MValY = e.Y;
        }
        private void BanLinhKien_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }
        private void BanLinhKien_MouseMove(object sender, MouseEventArgs e)
        {
            if (TogMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MValX, MousePosition.Y - MValY);
            }
        }
        private void btn_Minimize_Click(object sender, EventArgs e)
        {
            btn_XoaROW.BackColor = Color.Gray;
            btn_XoaROW.Enabled = false;
            this.WindowState = FormWindowState.Minimized;
        }
        private void Disable_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void Disable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                e.SuppressKeyPress = true;
            }
        }
        private void txt_TimKiem2_TextChanged(object sender, EventArgs e)
        {

        }
        private void button4_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = tabPage4;
            btn_BaoCao.BackColor = Color.DimGray;
            btn_BaoCao.FlatAppearance.MouseOverBackColor = Color.DimGray;
            Pressed4.Visible = true;
            btn_KhoHang.BackColor = Color.Gray;
            btn_TongQuan.BackColor = Color.Gray;
            btn_ThuChi.BackColor = Color.Gray;
            btn_BanHang.BackColor = Color.Gray;
            btn_CaiDat.BackColor = Color.Gray;
            Pressed1.Visible = false;
            Pressed2.Visible = false;
            Pressed3.Visible = false;
            Pressed5.Visible = false;
            Pressed6.Visible = false;
            btn_KhoHang.FlatAppearance.MouseOverBackColor = default(Color);
            btn_ThuChi.FlatAppearance.MouseOverBackColor = default(Color);
            btn_BanHang.FlatAppearance.MouseOverBackColor = default(Color);
            btn_CaiDat.FlatAppearance.MouseOverBackColor = default(Color);
            btn_TongQuan.FlatAppearance.MouseOverBackColor = default(Color);
            tab_BaoCao.SelectedTab = tab4_BaoCao;
            btn_KhachHang.BackColor = Color.White;
            btn_KhachHang.ForeColor = Color.Black;
            btn_KhachHang.FlatAppearance.MouseOverBackColor = Color.White;
            btn_KhachHang.FlatAppearance.MouseDownBackColor = Color.White;
            PressedFinal.Visible = true;
            btn_DoanhThu.BackColor = Color.DeepPink;
            btn_TongDonHang.BackColor = Color.DarkViolet;
            Pressed16.Visible = false;
            Pressed17.Visible = false;
            btn_DoanhThu.ForeColor = Color.White;
            btn_TongDonHang.ForeColor = Color.White;
            btn_DoanhThu.FlatAppearance.MouseOverBackColor = Color.DeepPink;
            btn_TongDonHang.FlatAppearance.MouseOverBackColor = Color.DarkViolet;
            btn_DoanhThu.FlatAppearance.MouseDownBackColor = Color.DeepPink;
            btn_TongDonHang.FlatAppearance.MouseDownBackColor = Color.DarkViolet;

        }
        private void btn_SuaThongTin_Click(object sender, EventArgs e)
        {
            Error2.ForeColor = Color.Red;
            DataTable tbl2 = new DataTable();
            tbl2 = obj.LayDuLieu("Select KHO.TenKho From CHITIETKHO,KHO Where CHITIETKHO.MaKho=KHO.MaKho AND MaMH LIKE '" + txt_TimMaHang.Text + "'");
            DataTable tbl = new DataTable();
            tbl = obj.LayDuLieu("Select TenMH,HangSx,DonGia,LuongTon,LoaiHang,BH From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH AND MATHANG.MaMH LIKE '" + txt_TimMaHang.Text + "'");
            DataTable tbl1 = new DataTable();
            tbl1 = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH AND MATHANG.MaMH LIKE '" + txt_TimMaHang.Text + "'");
            if (a == 0)
            {
                if (obj.KiemTra("Select * from MATHANG,CHITIETKHO " + " where MATHANG.MaMH=CHITIETKHO.MaMH AND MATHANG.MaMH LIKE '" + txt_TimMaHang.Text + "'") != 0)
                {
                    tab_SuaThongTin.SelectedTab = tab2_SuaThongTin;
                    txt_TimMaHang.Enabled = false;
                    txt_TimKiem3.Enabled = false;
                    btn_TimKiem3.Enabled = false;
                    txt_TenHangFix.Text = tbl.Rows[0][0].ToString();
                    txt_DonGiaFix.Text = tbl.Rows[0][2].ToString();
                    txt_LoaiHangFix.Text = tbl.Rows[0][4].ToString();
                    txt_SoLuongFix.Text = tbl.Rows[0][3].ToString();
                    txt_HangFix.Text = tbl.Rows[0][1].ToString();
                    cb_BHFix.Text = tbl.Rows[0][5].ToString();
                    cb_TenKhoFix.Text = tbl2.Rows[0][0].ToString();
                    TaoListView(lst_view2, tbl1);
                    a++;
                }
                else
                    Error2.Text = "Mã hàng không tồn tại";
            }
            else
            {
                tbl1 = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH AND MATHANG.MaMH");
                tab_SuaThongTin.SelectedTab = tab1_SuaThongTin;
                txt_TimMaHang.Enabled = true;
                txt_TimKiem3.Enabled = true;
                btn_TimKiem3.Enabled = true;
                a--;
                Error2.Text = "";
                TaoListView(lst_view2, tbl1);
            }
        }


        private void btn_Log_Click(object sender, EventArgs e)
        {
            btn_XoaROW.BackColor = Color.Gray;
            btn_XoaROW.Enabled = false;
            btn_Log.Text = "None";
            Form5 frm = new Form5();
            frm.ShowDialog();
            btn_Log.Text = Login.SetValueForText1;
        }
        private void phảnHồiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_XoaROW.BackColor = Color.Gray;
            btn_XoaROW.Enabled = false;
            PhanHoi frm = new PhanHoi();
            frm.ShowDialog();
        }
        private void btn_ThanhToan_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            String now = day + "/" + month + "/" + year;
            SetValueForText1 = txt_MaHoaDon.Text;
            btn_XoaROW.BackColor = Color.Gray;
            btn_XoaROW.Enabled = false;
            if (txt_MaHoaDon.Text == "" || txt_MaKH.Text == "" || txt_TenKhachHang.Text == "" || txt_DiaChi.Text == "" || txt_SDT.Text == "")
            {
                Error3.ForeColor = Color.Red;
                Error3.Text = "Không được để trống";
            }
            else if (listView1.Items.Count == 0)
            {
                Error3.ForeColor = Color.Red;
                Error3.Text = "chưa thêm hàng";
            }
            else if (switches == 0)
            {
                if (obj.KiemTra("Select * From HOADON Where MaHD='" + txt_MaHoaDon.Text + "'") == 0)
                {
                    DataTable tbl1 = new DataTable();
                    if (obj.KiemTra("Select * From KHACHHANG Where MaKH='" + txt_MaKH.Text + "' OR TenKH='" + txt_TenKhachHang.Text + "'") == 0)
                    {
                        obj.LayDuLieu("Insert into KHACHHANG(MaKH,TenKH,DiaChi,SDT) Values('" + txt_MaKH.Text + "','" + txt_TenKhachHang.Text + "','" + txt_DiaChi.Text + "','" + txt_SDT.Text + "')");
                    }
                    obj.LayDuLieu("Insert into HOADON(MaHD,NgayLap,MaKH,GhiChu) Values('" + txt_MaHoaDon.Text + "','" + now + "','" + txt_MaKH.Text + "','" + txt_GhiChu.Text + ".')");
                    DataTable tbl2 = new DataTable();
                    tbl2 = obj.LayDuLieu("Select SoLuong,MaMH From Cart");
                    int Countrows = obj.KiemTra("Select SoLuong,MaMH From Cart");
                    for (int s = 0; s < Countrows; s++)
                    {
                        string a, b;
                        a = tbl2.Rows[s][0].ToString();
                        b = tbl2.Rows[s][1].ToString();
                        int n0 = obj.KiemTra("Select * From CHITIETHOADON") + 1;
                        for (int i = 1; i <= n0; i++)
                        {
                            tbl1 = obj.LayDuLieu("Select * From CHITIETHOADON");
                            String check = "CTHD0" + i;
                            int count = 0;
                            for (int j = 0; j < n0 - 1; j++)
                            {
                                if (check == tbl1.Rows[j][0].ToString())
                                { }
                                else if (count < n0 - 2)
                                {
                                    count++;
                                }
                                else
                                {
                                    n0 = i;
                                }
                            }
                        }
                        obj.LayDuLieu("Insert into CHITIETHOADON(MaCTHD,SoLuong,MaHD,MaMH) Values('CTHD0" + n0 + "'," + a + ",'" + txt_MaHoaDon.Text + "','" + b + "')");
                    }
                }
                ThanhToan frm = new ThanhToan();
                frm.ShowDialog();
            }
            else
            {
                ThanhToan frm = new ThanhToan();
                frm.ShowDialog();
            }
            int n = obj.KiemTra("Select MaHD From HOADON");
            DataTable tbl = new DataTable();
            tbl = obj.LayDuLieu("Select MaHD From HOADON");
            cb_DonHang.Items.Clear();
            for (int i = 0; i < n; i++)
            {
                cb_DonHang.Items.Add(tbl.Rows[i][0].ToString());
            }
        }
        private void btn_Them_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            String now = day + "/" + month + "/" + year;
            DataTable tbl2 = new DataTable();
            tbl2 = obj.LayDuLieu("Select MaKho From KHO Where TenKho LIKE '" + cb_TenKho.Text + "'");
            int n0 = obj.KiemTra("Select * From CHITIETKHO") + 1;
            DataTable tbl1 = new DataTable();
            tbl1 = obj.LayDuLieu("Select * From CHITIETKHO");
            DataTable tbl3 = new DataTable();
            for (int i = 1; i <= n0; i++)
            {
                String check = "CTK0" + i;
                int count = 0;
                for (int j = 0; j < n0 - 1; j++)
                {
                    if (check == tbl1.Rows[j][0].ToString())
                    { }
                    else if (count < n0 - 2)
                    {
                        count++;
                    }
                    else
                    {
                        n0 = i;
                    }
                }
            }
            Error.ForeColor = Color.Red;
            DataTable tbl = new DataTable();
            if (txt_NhapMaHang.Text == "" || txt_NhapTenHang.Text == "" || txt_NhapDonGia.Text == "" || txt_NhapSoLuong.Text == "" || txt_Loai.Text == "" || txt_Hang.Text == "" || cb_BH.Text == "" || cb_TenKho.Text == "")
            {
                Error.ForeColor = Color.Red;
                Error.Text = "Không được để trống";
            }
            else if (obj.KiemTra("Select MaMH,TenMH From MATHANG where MaMH LIKE '" + txt_NhapMaHang.Text + "' OR TenMH LIKE '" + txt_NhapTenHang.Text + "'") != 0)
            {
                Error2.ForeColor = Color.Red;
                Error.Text = "Hàng đã tồn tại";
            }
            else
            {
                String sql = "insert into MATHANG(MaMH,TenMH,DonGia,LoaiHang,HangSx,BH) values('";
                sql += txt_NhapMaHang.Text + "','" + txt_NhapTenHang.Text + "'," + txt_NhapDonGia.Text + ",'" + txt_Loai.Text + "','" + txt_Hang.Text + "'," + cb_BH.Text + ")";
                String sql0 = "insert into CHITIETKHO(MaCTKHO,LuongNhap,LuongTon,MaMH,MaKho,NgayNhap) values('CTK0" + n0 + "'," + txt_NhapSoLuong.Text + "," + txt_NhapSoLuong.Text + ",'" + txt_NhapMaHang.Text + "','" + tbl2.Rows[0][0].ToString() + "','" + now + "')";
                if (obj.CapNhatDuLieu(sql) != 0)
                    if (obj.CapNhatDuLieu(sql0) != 0)
                    {
                        tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH");
                        tbl3 = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH");
                        Error.Text = "Thêm thành công";
                        Error.ForeColor = Color.Green;
                        TaoListView(lst_view1, tbl);
                        TaoListView(lst_view2, tbl);
                        TaoListView(lst_View3, tbl3);
                    }
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

        private void btn_ThemHang_Click(object sender, EventArgs e)
        {
            btn_XoaROW.BackColor = Color.Gray;
            btn_XoaROW.Enabled = false;
            Themhang frm = new Themhang();
            frm.ShowDialog();
            DataTable tbl = new DataTable();
            ListViewItem lstItems = new ListViewItem();
            int count = obj.KiemTra("Select * From Cart");
            while (listView1.Items.Count <= count)
            {
                if (Themhang.SetValueForText1 == "1")
                {
                    listView1.Items.Clear();
                    tbl = obj.LayDuLieu("Select * From Cart");
                    int n = obj.KiemTra("Select * From Cart");
                    int i = 1;
                    foreach (DataRow drw in tbl.Rows)
                    {
                        if (i <= n)
                        {
                            lstItems = listView1.Items.Add(i.ToString());
                            i++;
                            for (int j = 0; j <= tbl.Columns.Count - 1; j++)
                            {
                                lstItems.SubItems.Add(drw[j].ToString());
                            }
                        }
                    }
                }
                else
                    break;
                System.Threading.Thread.Sleep(300);
            }
        }

        private void btn_TimKiem2_Click(object sender, EventArgs e)
        {
            int parsedValue;
            DataTable tbl = new DataTable();
            string timkiem;
            timkiem = txt_TimKiem2.Text;
            if (obj.KiemTra("Select * from MATHANG,CHITIETKHO " + " where MATHANG.MaMH=CHITIETKHO.MaMH AND MATHANG.MaMH LIKE '" + timkiem + "%'") == 0)
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH");
            else
            {
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH AND MATHANG.MaMH LIKE '" + timkiem + "%'");
                TaoListView(lst_view1, tbl);
                return;
            }
            if (obj.KiemTra("Select * from MATHANG,CHITIETKHO " + " where MATHANG.MaMH=CHITIETKHO.MaMH AND TenMH LIKE '" + timkiem + "%'") == 0)
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH");
            else
            {
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH AND TenMH LIKE '" + timkiem + "%'");
                TaoListView(lst_view1, tbl);
                return;
            }
            if (!int.TryParse(timkiem, out parsedValue))
            {
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH");
                TaoListView(lst_view1, tbl);
                return;
            }
            else if (obj.KiemTra("Select * from MATHANG,CHITIETKHO " + " where MATHANG.MaMH=CHITIETKHO.MaMH AND DonGia=" + timkiem) == 0)
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH");
            else
            {
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH AND DonGia LIKE " + timkiem);
                TaoListView(lst_view1, tbl);
                return;
            }
            if (!int.TryParse(timkiem, out parsedValue))
            {
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH");
                TaoListView(lst_view1, tbl);
                return;
            }
            else if (obj.KiemTra("Select * from MATHANG,CHITIETKHO " + " where MATHANG.MaMH=CHITIETKHO.MaMH AND LuongTon=" + timkiem) == 0)
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH");
            else
            {
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH AND LuongTon LIKE " + timkiem);
                TaoListView(lst_view1, tbl);
                return;
            }
            TaoListView(lst_view1, tbl);
        }
        private void btn_TimKiem3_Click(object sender, EventArgs e)
        {
            int parsedValue;
            DataTable tbl = new DataTable();
            string timkiem;
            timkiem = txt_TimKiem3.Text;
            if (obj.KiemTra("Select * from MATHANG,CHITIETKHO " + " where MATHANG.MaMH=CHITIETKHO.MaMH AND MATHANG.MaMH LIKE '" + timkiem + "%'") == 0)
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH");
            else
            {
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH AND MATHANG.MaMH LIKE '" + timkiem + "%'");
                TaoListView(lst_view1, tbl);
                return;
            }
            if (obj.KiemTra("Select * from MATHANG,CHITIETKHO " + " where MATHANG.MaMH=CHITIETKHO.MaMH AND TenMH LIKE '" + timkiem + "%'") == 0)
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH");
            else
            {
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH AND TenMH LIKE '" + timkiem + "%'");
                TaoListView(lst_view1, tbl);
                return;
            }
            if (!int.TryParse(timkiem, out parsedValue))
            {
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH");
                TaoListView(lst_view1, tbl);
                return;
            }
            else if (obj.KiemTra("Select * from MATHANG,CHITIETKHO " + " where MATHANG.MaMH=CHITIETKHO.MaMH AND DonGia=" + timkiem) == 0)
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH");
            else
            {
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH AND DonGia LIKE " + timkiem);
                TaoListView(lst_view1, tbl);
                return;
            }
            if (!int.TryParse(timkiem, out parsedValue))
            {
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH");
                TaoListView(lst_view1, tbl);
                return;
            }
            else if (obj.KiemTra("Select * from MATHANG,CHITIETKHO " + " where MATHANG.MaMH=CHITIETKHO.MaMH AND LuongTon=" + timkiem) == 0)
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH");
            else
            {
                tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH AND LuongTon LIKE " + timkiem);
                TaoListView(lst_view1, tbl);
                return;
            }
            TaoListView(lst_view1, tbl);
        }
        private void btn_TimKiem4_Click(object sender, EventArgs e)
        {
            {
                int parsedValue;
                DataTable tbl = new DataTable();
                string timkiem;
                timkiem = txt_TimKiem4.Text;
                if (obj.KiemTra("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH AND MATHANG.MaMH LIKE '" + timkiem + "%'") == 0)
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH");
                else
                {
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH AND MATHANG.MaMH LIKE '" + timkiem + "%'");
                    TaoListView(lst_View3, tbl);
                    return;
                }
                if (obj.KiemTra("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH AND TenMH LIKE '" + timkiem + "%'") == 0)
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH");
                else
                {
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH AND TenMH LIKE '" + timkiem + "%'");
                    TaoListView(lst_View3, tbl);
                    return;
                }
                if (obj.KiemTra("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH AND HangSx LIKE '" + timkiem + "%'") == 0)
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH");
                else
                {
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH AND HangSx LIKE '" + timkiem + "%'");
                    TaoListView(lst_View3, tbl);
                    return;
                }
                if (obj.KiemTra("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH AND LoaiHang LIKE '" + timkiem + "%'") == 0)
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH");
                else
                {
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH AND LoaiHang LIKE '" + timkiem + "%'");
                    TaoListView(lst_View3, tbl);
                    return;
                }
                if (obj.KiemTra("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH AND TenKho LIKE '" + timkiem + "%'") == 0)
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH");
                else
                {
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH AND TenKho LIKE '" + timkiem + "%'");
                    TaoListView(lst_View3, tbl);
                    return;
                }
                if (!int.TryParse(timkiem, out parsedValue))
                {
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH");
                    TaoListView(lst_View3, tbl);
                    return;
                }
                else if (obj.KiemTra("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH AND DonGia=" + timkiem) == 0)
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH");
                else
                {
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH AND DonGia LIKE " + timkiem);
                    TaoListView(lst_View3, tbl);
                    return;
                }
                if (!int.TryParse(timkiem, out parsedValue))
                {
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH");
                    TaoListView(lst_View3, tbl);
                    return;
                }
                else if (obj.KiemTra("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH AND LuongTon=" + timkiem) == 0)
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH");
                else
                {
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH AND LuongTon LIKE " + timkiem);
                    TaoListView(lst_View3, tbl);
                    return;
                }
                if (!int.TryParse(timkiem, out parsedValue))
                {
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH");
                    TaoListView(lst_View3, tbl);
                    return;
                }
                else if (obj.KiemTra("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH AND BH=" + timkiem) == 0)
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH");
                else
                {
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH AND BH LIKE " + timkiem);
                    TaoListView(lst_View3, tbl);
                    return;
                }
                if (!int.TryParse(timkiem, out parsedValue))
                {
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH");
                    TaoListView(lst_View3, tbl);
                    return;
                }
                else if (obj.KiemTra("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH AND YEAR(NgayNhap)=" + timkiem) == 0)
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH");
                else
                {
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH AND YEAR(NgayNhap) LIKE " + timkiem);
                    TaoListView(lst_View3, tbl);
                    return;
                }
                if (!int.TryParse(timkiem, out parsedValue))
                {
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH");
                    TaoListView(lst_View3, tbl);
                    return;
                }
                else if (obj.KiemTra("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH AND DAY(NgayNhap)=" + timkiem) == 0)
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH");
                else
                {
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH AND DAY(NgayNhap) LIKE " + timkiem);
                    TaoListView(lst_View3, tbl);
                    return;
                }
                if (!int.TryParse(timkiem, out parsedValue))
                {
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH");
                    TaoListView(lst_View3, tbl);
                    return;
                }
                else if (obj.KiemTra("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH AND MONTH(NgayNhap)=" + timkiem) == 0)
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH");
                else
                {
                    tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH AND MONTH(NgayNhap) LIKE " + timkiem);
                    TaoListView(lst_View3, tbl);
                    return;
                }
                TaoListView(lst_View3, tbl);
            }
        }



        private void btn_xoa_Click(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            DataTable tbl1 = new DataTable();
            if (obj.KiemTra("Select  MATHANG.MaMH from MATHANG,CHITIETKHO " + " where MATHANG.MaMH=CHITIETKHO.MaMH AND MATHANG.MaMH LIKE '" + txt_TimMaHang.Text + "%'") != 0)
            {
                obj.LayDuLieu("Delete From MATHANG Where MaMH='" + txt_TimMaHang.Text + "'");
                obj.LayDuLieu("Delete From CHITIETKHO Where MaMH='" + txt_TimMaHang.Text + "'");
                Error2.ForeColor = Color.Green;
                Error2.Text = "Xóa hàng thành công";
            }
            else
            {
                Error2.ForeColor = Color.Red;
                Error2.Text = "Mã hàng không tồn tại";
            }
            tbl = obj.LayDuLieu("Select MATHANG.MaMH, TenMH, DonGia, LuongTon From MATHANG, CHITIETKHO Where MATHANG.MaMH = CHITIETKHO.MaMH");
            tbl1 = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH");
            TaoListView(lst_view1, tbl);
            TaoListView(lst_view2, tbl);
            TaoListView(lst_View3, tbl1);
            if (a == 1)
            {
                tab_SuaThongTin.SelectedTab = tab1_SuaThongTin;
                txt_TimMaHang.Enabled = true;
                txt_TimKiem3.Enabled = true;
                btn_TimKiem3.Enabled = true;
                a--;
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            DataTable tbl1 = new DataTable();
            DataTable tbl2 = new DataTable();
            tbl1 = obj.LayDuLieu("Select KHO.MaKho From CHITIETKHO,KHO Where CHITIETKHO.MaKho=KHO.MaKho AND TenKho LIKE '" + cb_TenKhoFix.Text + "'");
            if (txt_TenHangFix.Text == "" || txt_DonGiaFix.Text == "" || txt_LoaiHangFix.Text == "" || txt_HangFix.Text == "" || cb_BHFix.Text == "" || txt_SoLuongFix.Text == "" || txt_TimMaHang.Text == "")
            {
                Error0.ForeColor = Color.Red;
                Error0.Text = "Không được để trống";
            }
            obj.LayDuLieu("Update MATHANG Set TenMH='" + txt_TenHangFix.Text + "', DonGia=" + txt_DonGiaFix.Text + ", LoaiHang='" + txt_LoaiHangFix.Text + "', HangSx='" + txt_HangFix.Text + "', BH=" + cb_BHFix.Text + " Where MaMH='" + txt_TimMaHang.Text + "'");
            obj.LayDuLieu("Update CHITIETKHO Set MaKho='" + tbl1.Rows[0][0].ToString() + "', LuongTon=" + txt_SoLuongFix.Text + " Where MaMH='" + txt_TimMaHang.Text + "'");
            tab_SuaThongTin.SelectedTab = tab1_SuaThongTin;
            txt_TimMaHang.Enabled = true;
            txt_TimKiem3.Enabled = true;
            btn_TimKiem3.Enabled = true;
            tbl = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,DonGia,LuongTon From MATHANG,CHITIETKHO Where MATHANG.MaMH=CHITIETKHO.MaMH");
            tbl2 = obj.LayDuLieu("Select MATHANG.MaMH,TenMH,HangSx,DonGia,LuongTon,LoaiHang,NgayNhap,BH,TenKho From KHO,MATHANG,CHITIETKHO Where CHITIETKHO.MaKho=KHO.MaKho AND MATHANG.MaMH=CHITIETKHO.MaMH");
            TaoListView(lst_view1, tbl);
            TaoListView(lst_view2, tbl);
            TaoListView(lst_View3, tbl2);
            Error2.ForeColor = Color.Green;
            Error2.Text = "Chỉnh sửa thành công";
            a--;
        }

        private void NumberOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btn_ThemKho_Click(object sender, EventArgs e)
        {
            Form7 frm = new Form7();
            frm.ShowDialog();
        }

        private void cb_DonHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (obj.KiemTra("Select * From HOADON Where TinhTrang=1 AND MaHD='" + cb_DonHang.Text + "'") != 0)
                Confirm.Text = "Đã thanh toán";
            else
                Confirm.Text = "";
            DataTable tbl = new DataTable();
            tbl = obj.LayDuLieu("Select MaHD,KHACHHANG.*,NgayLap,GhiChu From HOADON,KHACHHANG Where HOADON.MaKH=KHACHHANG.MaKH AND MaHD='" + cb_DonHang.Text + "'");
            lbl_SoPhieu.Text = "Số phiếu: " + tbl.Rows[0][0].ToString();
            lbl_TenKhach.Text = "Tên khách hàng: " + tbl.Rows[0][2].ToString();
            lbl_DiaChi.Text = "Địa chỉ: " + tbl.Rows[0][3].ToString();
            lbl_SDT.Text = "Số điện thoại: " + tbl.Rows[0][4].ToString();
            lbl_GhiChu.Text = "Ghi chú: " + tbl.Rows[0][6].ToString();
            lbl_NgayLap.Text = "Ngày lập: " + tbl.Rows[0][5].ToString();
            DataTable tbl2 = new DataTable();
            ListViewItem lstItems = new ListViewItem();
            listView5.Items.Clear();
            tbl2 = obj.LayDuLieu("Select CHITIETHOADON.MaMH,TenMH,HangSx,SoLuong,BH,(DonGia*SoLuong) AS THANHTIEN From CHITIETHOADON,MATHANG Where CHITIETHOADON.MaMH=MATHANG.MaMH AND MaHD='" + cb_DonHang.Text + "'");
            int n = obj.KiemTra("Select * From CHITIETHOADON Where MaHD='" + cb_DonHang.Text + "'");
            lbl_TongTien.Text = "Tổng tiền: " + obj.LayDuLieu("Select SUM(DonGia*SoLuong) AS TONG From CHITIETHOADON,MATHANG Where CHITIETHOADON.MaMH=MATHANG.MaMH AND MaHD='" + cb_DonHang.Text + "'").Rows[0][0].ToString() + "đ";
            listView5.Items.Clear();
            int i = 1;
            foreach (DataRow drw in tbl2.Rows)
            {
                if (i <= n)
                {
                    lstItems = listView5.Items.Add(i.ToString());
                    i++;
                    for (int j = 0; j <= tbl2.Columns.Count - 1; j++)
                    {
                        lstItems.SubItems.Add(drw[j].ToString());
                    }
                }
            }
        }

        private void btn_LuuHD_Click(object sender, EventArgs e)
        {
            btn_XoaROW.BackColor = Color.Gray;
            btn_XoaROW.Enabled = false;
            DateTime date = DateTime.Now;
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            String now = day + "/" + month + "/" + year;
            if (switches == 0)
            {
                if (txt_MaHoaDon.Text == "" || txt_MaKH.Text == "" || txt_TenKhachHang.Text == "" || txt_DiaChi.Text == "" || txt_SDT.Text == "")
                {
                    Error3.ForeColor = Color.Red;
                    Error3.Text = "Không được để trống";
                }
                else if (listView1.Items.Count == 0)
                {
                    Error3.ForeColor = Color.Red;
                    Error3.Text = "chưa thêm hàng";
                }
                else if (obj.KiemTra("Select * From HOADON Where MaHD='" + txt_MaHoaDon.Text + "'") != 0)
                {
                    Error3.ForeColor = Color.Red;
                    Error3.Text = "Mã hóa đơn đã tồn tại";
                }
                else
                {
                    DataTable tbl1 = new DataTable();
                    if (obj.KiemTra("Select * From KHACHHANG Where MaKH='" + txt_MaKH.Text + "' OR TenKH='" + txt_TenKhachHang.Text + "'") == 0)
                    {
                        obj.LayDuLieu("Insert into KHACHHANG(MaKH,TenKH,DiaChi,SDT) Values('" + txt_MaKH.Text + "','" + txt_TenKhachHang.Text + "','" + txt_DiaChi.Text + "','" + txt_SDT.Text + "')");
                    }
                    obj.LayDuLieu("Insert into HOADON(MaHD,NgayLap,MaKH,GhiChu) Values('" + txt_MaHoaDon.Text + "','" + now + "','" + txt_MaKH.Text + "','" + txt_GhiChu.Text + ".')");
                    DataTable tbl2 = new DataTable();
                    tbl2 = obj.LayDuLieu("Select SoLuong,MaMH From Cart");
                    int Countrows = obj.KiemTra("Select SoLuong,MaMH From Cart");
                    for (int s = 0; s < Countrows; s++)
                    {
                        string a, b;
                        a = tbl2.Rows[s][0].ToString();
                        b = tbl2.Rows[s][1].ToString();
                        int n0 = obj.KiemTra("Select * From CHITIETHOADON") + 1;
                        for (int i = 1; i <= n0; i++)
                        {
                            tbl1 = obj.LayDuLieu("Select * From CHITIETHOADON");
                            String check = "CTHD0" + i;
                            int count = 0;
                            for (int j = 0; j < n0 - 1; j++)
                            {
                                if (check == tbl1.Rows[j][0].ToString())
                                { }
                                else if (count < n0 - 2)
                                {
                                    count++;
                                }
                                else
                                {
                                    n0 = i;
                                }
                            }
                        }
                        obj.LayDuLieu("Insert into CHITIETHOADON(MaCTHD,SoLuong,MaHD,MaMH) Values('CTHD0" + n0 + "'," + a + ",'" + txt_MaHoaDon.Text + "','" + b + "')");
                    }
                    Error3.ForeColor = Color.Green;
                    Error3.Text = "Lưu Thành công";
                    int n = obj.KiemTra("Select MaHD From HOADON");
                    DataTable tbl = new DataTable();
                    tbl = obj.LayDuLieu("Select MaHD From HOADON");
                    cb_DonHang.Items.Clear();
                    for (int i = 0; i < n; i++)
                    {
                        cb_DonHang.Items.Add(tbl.Rows[i][0].ToString());
                    }
                    txt_MaHoaDon.Text = "";
                    txt_MaKH.Text = "";
                    txt_TenKhachHang.Text = "";
                    txt_DiaChi.Text = "";
                    txt_SDT.Text = "";
                    txt_GhiChu.Text = "";
                    obj.LayDuLieu("Delete * From Cart");
                    listView1.Items.Clear();
                }
            }
            else if (switches == 1)
            {
                obj.LayDuLieu("UPDATE KHACHHANG SET DiaChi='" + txt_DiaChi.Text + "',SDT='" + txt_SDT.Text + "',TenKH='"+txt_TenKhachHang.Text+"' Where MaKH='"+txt_MaKH+"'");
                DataTable tbl1 = new DataTable();
                obj.LayDuLieu("Delete * From HOADON Where MaHD='" + cb_DonHang.Text + "'");
                obj.LayDuLieu("Insert into HOADON(MaHD,NgayLap,MaKH,GhiChu) Values('" + txt_MaHoaDon.Text + "','" + now + "','" + txt_MaKH.Text + "','" + txt_GhiChu.Text + ".')");
                DataTable tbl2 = new DataTable();
                tbl2 = obj.LayDuLieu("Select SoLuong,MaMH From Cart");
                int Countrows = obj.KiemTra("Select SoLuong,MaMH From Cart");
                for (int s = 0; s < Countrows; s++)
                {
                    string a, b;
                    a = tbl2.Rows[s][0].ToString();
                    b = tbl2.Rows[s][1].ToString();
                    int n0 = obj.KiemTra("Select * From CHITIETHOADON") + 1;
                    for (int i = 1; i <= n0; i++)
                    {
                        tbl1 = obj.LayDuLieu("Select * From CHITIETHOADON");
                        String check = "CTHD0" + i;
                        int count = 0;
                        for (int j = 0; j < n0 - 1; j++)
                        {
                            if (check == tbl1.Rows[j][0].ToString())
                            { }
                            else if (count < n0 - 2)
                            {
                                count++;
                            }
                            else
                            {
                                n0 = i;
                            }
                        }
                    }
                    obj.LayDuLieu("Insert into CHITIETHOADON(MaCTHD,SoLuong,MaHD,MaMH) Values('CTHD0" + n0 + "'," + a + ",'" + txt_MaHoaDon.Text + "','" + b + "')");
                }
                Error3.ForeColor = Color.Green;
                Error3.Text = "Lưu Thành công";
                int n = obj.KiemTra("Select MaHD From HOADON");
                DataTable tbl = new DataTable();
                tbl = obj.LayDuLieu("Select MaHD From HOADON");
                cb_DonHang.Items.Clear();
                for (int i = 0; i < n; i++)
                {
                    cb_DonHang.Items.Add(tbl.Rows[i][0].ToString());
                }
                switches = 0;
                txt_MaHoaDon.Text = "";
                txt_TenKhachHang.Text = "";
                txt_MaKH.Text = "";
                txt_DiaChi.Text = "";
                txt_SDT.Text = "";
                txt_GhiChu.Text = "";
                txt_MaHoaDon.Enabled = true;
                txt_TenKhachHang.Enabled = true;
                txt_MaKH.Enabled = true;
                btn_exitupdate.Visible = false;
                obj.LayDuLieu("Delete * From Cart");
                listView1.Items.Clear();
                btn_DonHang.Enabled = true;
                btn_DonHang.BackColor = Color.Crimson;
            }
        }

        private void btn_XoaBH_Click(object sender, EventArgs e)
        {
            if (cb_DonHang.Text != "")
            {
                obj.LayDuLieu("Delete * From HOADON Where MaHD='" + cb_DonHang.Text + "'");
                int n = obj.KiemTra("Select MaHD From HOADON");
                DataTable tbl = new DataTable();
                tbl = obj.LayDuLieu("Select MaHD From HOADON");
                cb_DonHang.Items.Clear();
                for (int i = 0; i < n; i++)
                {
                    cb_DonHang.Items.Add(tbl.Rows[i][0].ToString());
                }
                lbl_SoPhieu.Text = "Số phiếu: None";
                lbl_TenKhach.Text = "Tên khách hàng: None";
                lbl_DiaChi.Text = "Địa chỉ: None";
                lbl_SDT.Text = "Số điện thoại: None";
                lbl_GhiChu.Text = "Ghi chú: None ";
                lbl_NgayLap.Text = "Ngày lập: None";
                ERRORUPDATE.ForeColor = Color.Green;
                ERRORUPDATE.Text = "Xóa thành công";
                listView5.Items.Clear();
            }
            else
            {
                ERRORUPDATE.ForeColor = Color.Red;
                ERRORUPDATE.Text = "Chưa chọn đơn hàng";
            }
        }

        private void cb_ChonNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            if (cb_ChonNam.SelectedIndex != -1)
            {
                tbl = obj.LayDuLieu("Select KHACHHANG.TenKH,KHACHHANG.SDT,KHACHHANG.DIACHI,BAOCAOCONGNO.TongNo,BAOCAOCONGNO.DaTra,BAOCAOCONGNO.NgayLap from KHACHHANG inner join BAOCAOCONGNO on KHACHHANG.MaKH=BAOCAOCONGNO.MaKH where YEAR(NgayLap)=" + cb_ChonNam.Text);
            }
            TaoListView(listView3, tbl);
        }

        private void cb_ChonNam1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            if (cb_ChonNam1.SelectedIndex != -1)
            {
                tbl = obj.LayDuLieu("Select MATHANG.MaMH, TenMH, SUM(SoLuong), SUM(DonGia * SoLuong) From MATHANG, CHITIETHOADON,HOADON Where HOADON.MaHD=CHITIETHOADON.MaHD AND MATHANG.MaMH = CHITIETHOADON.MaMH AND Year(NgayLap)=" + cb_ChonNam1.Text + " Group by MATHANG.MaMH, TenMH,NgayLap");
                lbl_TongDS.Text = "Tổng tiền: "+obj.LayDuLieu("Select SUM(DonGia * SoLuong) From CHITIETHOADON, MATHANG, HOADON Where HOADON.MaHD = CHITIETHOADON.MaHD AND CHITIETHOADON.MaMH = MATHANG.MaMH AND TinhTrang = 1 AND Year(NgayLap)=" + cb_ChonNam1.Text).Rows[0][0].ToString() + "đ";
            }
            TaoListView(listView6, tbl);
        }

        private void btn_XoaCol_Click(object sender, EventArgs e)
        {
            String MaMH = listView1.SelectedItems[0].SubItems[1].Text;
            obj.LayDuLieu("Delete * From Cart Where MaMH='" + MaMH + "'");
            DataTable tbl = new DataTable();
            ListViewItem lstItems = new ListViewItem();
            listView1.Items.Clear();
            tbl = obj.LayDuLieu("Select * From Cart");
            int n = obj.KiemTra("Select * From Cart");
            int i = 1;
            foreach (DataRow drw in tbl.Rows)
            {
                if (i <= n)
                {
                    lstItems = listView1.Items.Add(i.ToString());
                    i++;
                    for (int j = 0; j <= tbl.Columns.Count - 1; j++)
                    {
                        lstItems.SubItems.Add(drw[j].ToString());
                    }
                }
            }
            btn_XoaROW.BackColor = Color.Gray;
            btn_XoaROW.Enabled = false;
        }
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            btn_XoaROW.BackColor = Color.Red;
                btn_XoaROW.Enabled = true;
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_XoaROW.BackColor = Color.Gray;
            btn_XoaROW.Enabled = false;
        }

        private void hệThốngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_XoaROW.BackColor = Color.Gray;
            btn_XoaROW.Enabled = false;
        }

        private void btn_ChinhSuaDonHang_Click(object sender, EventArgs e)
        {
            if (cb_DonHang.Text == "")
            {
                ERRORUPDATE.ForeColor = Color.Red;
                ERRORUPDATE.Text = "Chưa chọn đơn hàng";
            }
            else if (Confirm.Text == "Đã thanh toán")
            {
                ERRORUPDATE.ForeColor = Color.Red;
                ERRORUPDATE.Text = "không được sửa hóa đơn đã thanh toán";
            }
            else
            {
                DataTable tbl = new DataTable();
                tbl = obj.LayDuLieu("Select MaHD,KHACHHANG.*,GhiChu From HOADON,KHACHHANG Where HOADON.MaKH=KHACHHANG.MaKH AND MaHD='" + cb_DonHang.Text + "'");
                txt_MaHoaDon.Text = tbl.Rows[0][0].ToString();
                txt_MaKH.Text = tbl.Rows[0][1].ToString();
                txt_TenKhachHang.Text = tbl.Rows[0][2].ToString();
                txt_DiaChi.Text = tbl.Rows[0][3].ToString();
                txt_SDT.Text = tbl.Rows[0][4].ToString();
                txt_GhiChu.Text = tbl.Rows[0][5].ToString();
                listView1.Items.Clear();
                obj.LayDuLieu("Delete * From Cart");
                obj.LayDuLieu("Insert into Cart(MaMH,TenMH,HangSx,SoLuong,BH,ThanhTien) Select CHITIETHOADON.MaMH,TenMH,HangSx,SoLuong,BH,(DonGia*SoLuong) From CHITIETHOADON,MATHANG Where CHITIETHOADON.MaMH=MATHANG.MaMH AND MaHD='" + cb_DonHang.Text + "'");
                DataTable tbl2 = new DataTable();
                ListViewItem lstItems = new ListViewItem();
                listView1.Items.Clear();
                tbl2 = obj.LayDuLieu("Select * From Cart");
                int n = obj.KiemTra("Select * From Cart");
                int i = 1;
                foreach (DataRow drw in tbl2.Rows)
                {
                    if (i <= n)
                    {
                        lstItems = listView1.Items.Add(i.ToString());
                        i++;
                        for (int j = 0; j <= tbl2.Columns.Count - 1; j++)
                        {
                            lstItems.SubItems.Add(drw[j].ToString());
                        }
                    }
                }
                tab_BanHang.SelectedTab = tab1_BanHang;
                button11.BackColor = Color.White;
                button11.ForeColor = Color.Black;
                button11.FlatAppearance.MouseOverBackColor = Color.White;
                button11.FlatAppearance.MouseDownBackColor = Color.White;
                Pressed10.Visible = true;
                btn_DonHang.BackColor = Color.Crimson;
                Pressed12.Visible = false;
                btn_DonHang.ForeColor = Color.White;
                btn_DonHang.FlatAppearance.MouseOverBackColor = Color.Crimson;
                btn_DonHang.FlatAppearance.MouseDownBackColor = Color.Crimson;
                btn_XoaROW.BackColor = Color.Gray;
                btn_XoaROW.Enabled = false;
                btn_exitupdate.Visible = true;
                txt_MaHoaDon.Enabled = false;
                txt_MaKH.Enabled = false;
                btn_DonHang.Enabled = false;
                btn_DonHang.BackColor = Color.Gray;
                ERRORUPDATE.Text = "";
                switches = 1;
            }
        }
        private void cbo_Nam2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            if (cbo_Nam2.SelectedIndex != -1)
            {
                tbl = obj.LayDuLieu("Select TenNCC,SDT,DiaChi,TongNo,DaTra From NHACUNGCAP,BAOCAOCONGNO where NHACUNGCAP.MaNCC=BAOCAOCONGNO.MaNCC and Year(NgayLap)=" + cbo_Nam2.Text);
            }
            TaoListView(listView4, tbl);
        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            if (Login.SetValueForText2 == "1")
            {
                ERRORSET.Text = "";
                Form8 frm = new Form8();
                frm.ShowDialog();
            }
            else
                ERRORSET.Text = "chỉ Admin mới có quyền";
        }

        private void btn_ChinhSua_Click(object sender, EventArgs e)
        {
            txt_user.Text = "";
            txt_pass.Text = "";
            tab_ChinhSua.SelectedTab = tab2_ChinhSua;
        }

        private void btn2_chinhsua_Click(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            String u = txt_user.Text;
            String p = txt_pass.Text;
            if (obj.KiemTra("Select *From TAIKHOAN where [User]='" + u + "' and [Password]='" + p + "'") != 0)
            {
                tbl = obj.LayDuLieu("Select TenNV,SDT,DiaChi,Email From NHANVIEN,TAIKHOAN where NHANVIEN.MaTK=TAIKHOAN.MATK and [User]='" + u + "' and [Password]='" + p + "'");
                tab_ChinhSua.SelectedTab = tab3_ChinhSua;
                txt_tennv.Text = tbl.Rows[0][0].ToString();
                txt_sdt_cs.Text = tbl.Rows[0][1].ToString();
                txtdiachi.Text = tbl.Rows[0][2].ToString();
                txtemail.Text = tbl.Rows[0][3].ToString();
            }
            else if (txt_user.Text == "" || txt_pass.Text == "")
            {
                lbl_ERROR.ForeColor = Color.Red;
                lbl_ERROR.Text = "Không được để trống";
            }
            else
            {
                lbl_ERROR.ForeColor = Color.Red;
                lbl_ERROR.Text = "Tài khoản hoặc mật khẩu không đúng";
            }
        }

        private void btn_doimk_2_Click(object sender, EventArgs e)
        {
            String u = txt_user.Text;
            String p = txt_pass.Text;
            if (obj.KiemTra("Select *From TAIKHOAN where [User]='" + u + "' and [Password]='" + p + "'") != 0)
            {
                tab_ChinhSua.SelectedTab = tab4_ChinhSua;
            }
            else
            {
                lbl_ERROR.ForeColor = Color.Red;
                lbl_ERROR.Text = "Tài khoản hoặc mật khẩu không đúng";
            }
        }

        private void btncapnhat_Click(object sender, EventArgs e)
        {
            String u = txt_user.Text;
            String p = txt_pass.Text;
            DataTable tbl = new DataTable();
            tbl = obj.LayDuLieu("Select MaNV From NHANVIEN,TAIKHOAN where NHANVIEN.MaTK=TAIKHOAN.MATK and [User]='" + u + "' and [Password]='" + p + "'");
            String manv = tbl.Rows[0][0].ToString();
            String sql = "update NHANVIEN SET TenNV='" + txt_tennv.Text + "', SDT='" + txt_sdt_cs.Text + "', DiaChi='" + txtdiachi.Text + "', Email='" + txtemail.Text + "' where MaNV='" + manv + "' ";
            if (obj.CapNhatDuLieu(sql) != 0)
            {
                lbl_capnhat.ForeColor = Color.Green;
                lbl_capnhat.Text = "Cập nhật thành công";
            }
            else
            {
                lbl_capnhat.ForeColor = Color.Red;
                lbl_capnhat.Text = "Cập nhật không thành công";
            }
        }

        private void btn_out_Click(object sender, EventArgs e)
        {
            tab_ChinhSua.SelectedTab = tab1_ChinhSua;
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            if (Login.SetValueForText2 == "1")
            {
                ERRORSET.Text = "";
                string CurrentDatabasePath = Environment.CurrentDirectory + @"\QLMT.mdb";
                string test = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " ";
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string PathtobackUp = fbd.SelectedPath.ToString();
                    File.Copy(CurrentDatabasePath, PathtobackUp + @"\" + test + "QLMT-Backup.mdb", true);
                    MessageBox.Show("Sao lưu dữ liệu thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
                ERRORSET.Text = "chỉ Admin mới có quyền";

        }

        private void btn_khoiphuc_Click(object sender, EventArgs e)
        {
            if (Login.SetValueForText2 == "1")
            {
                ERRORSET.Text = "";
                string dbFileName = "QLMT.mdb";
                OpenFileDialog open = new OpenFileDialog();
                if (open.ShowDialog() == DialogResult.OK)
                {
                    string pathBackup = open.FileName.ToString();
                    string CurrentDatabasePath = Path.Combine(Environment.CurrentDirectory, dbFileName);
                    File.Copy(pathBackup, CurrentDatabasePath, true);
                    MessageBox.Show("Khôi phục dữ liệu thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
                ERRORSET.Text = "chỉ Admin mới có quyền";
        }

        private void btndoi_Click(object sender, EventArgs e)
        {
            String u = txt_user.Text;
            String p = txt_pass.Text;
            String sql = "Update TAIKHOAN SET [Password]='" + txtmk_moi.Text + "' where [User]='" + txt_user.Text + "'";
            if (txtmk_cu.Text == "" || txtmk_moi.Text == "" || txtremk_moi.Text == "")
            {
                lbl_doimk.ForeColor = Color.Red;
                lbl_doimk.Text = "Không được để trống!";
            }
            else if (txtmk_moi.Text != txtremk_moi.Text)
            {
                lbl_doimk.ForeColor = Color.Red;
                lbl_doimk.Text = "Nhập lại mật khẩu không đúng!";
            }
            else
            {
                if (obj.CapNhatDuLieu(sql) != 0)
                {
                    lbl_doimk.ForeColor = Color.Green;
                    lbl_doimk.Text = "Đổi mật khẩu thành công";
                }
            }
        }

        private void btn_out2_Click_1(object sender, EventArgs e)
        {
            tab_ChinhSua.SelectedTab = tab1_ChinhSua;
        }

        private void btn_Thoat3_Click(object sender, EventArgs e)
        {
            tab_ChinhSua.SelectedTab = tab1_ChinhSua;
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_XoaROW.BackColor = Color.Gray;
            btn_XoaROW.Enabled = false;
            btn_Log.Text = "None";
            Form5 frm = new Form5();
            frm.ShowDialog();
            btn_Log.Text = Login.SetValueForText1;
        }

        private void btn_exitupdate_Click(object sender, EventArgs e)
        {
            switches = 0;
            txt_MaHoaDon.Text = "";
            txt_TenKhachHang.Text = "";
            txt_MaKH.Text = "";
            txt_DiaChi.Text = "";
            txt_SDT.Text = "";
            txt_GhiChu.Text = "";
            txt_MaHoaDon.Enabled = true;
            txt_TenKhachHang.Enabled = true;
            txt_MaKH.Enabled = true;
            btn_exitupdate.Visible = false;
            obj.LayDuLieu("Delete * From Cart");
            listView1.Items.Clear();
            btn_DonHang.Enabled = true;
            btn_DonHang.BackColor = Color.Crimson;
        }

        private void thêmHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = tabPage5;
            btn_BanHang.BackColor = Color.DimGray;
            btn_BanHang.FlatAppearance.MouseOverBackColor = Color.DimGray;
            Pressed5.Visible = true;
            btn_KhoHang.BackColor = Color.Gray;
            btn_TongQuan.BackColor = Color.Gray;
            btn_ThuChi.BackColor = Color.Gray;
            btn_BaoCao.BackColor = Color.Gray;
            btn_CaiDat.BackColor = Color.Gray;
            Pressed1.Visible = false;
            Pressed2.Visible = false;
            Pressed3.Visible = false;
            Pressed4.Visible = false;
            Pressed6.Visible = false;
            btn_KhoHang.FlatAppearance.MouseOverBackColor = default(Color);
            btn_ThuChi.FlatAppearance.MouseOverBackColor = default(Color);
            btn_BaoCao.FlatAppearance.MouseOverBackColor = default(Color);
            btn_CaiDat.FlatAppearance.MouseOverBackColor = default(Color);
            btn_TongQuan.FlatAppearance.MouseOverBackColor = default(Color);
            btn_XoaROW.BackColor = Color.Gray;
            btn_XoaROW.Enabled = false;
            tab_BanHang.SelectedTab = tab1_BanHang;
            button11.BackColor = Color.White;
            button11.ForeColor = Color.Black;
            button11.FlatAppearance.MouseOverBackColor = Color.White;
            button11.FlatAppearance.MouseDownBackColor = Color.White;
            Pressed10.Visible = true;
            btn_DonHang.BackColor = Color.Crimson;
            Pressed12.Visible = false;
            btn_DonHang.ForeColor = Color.White;
            btn_DonHang.FlatAppearance.MouseOverBackColor = Color.Crimson;
            btn_DonHang.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btn_XoaROW.BackColor = Color.Gray;
            btn_XoaROW.Enabled = false;
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = tabPage6;
            btn_CaiDat.BackColor = Color.DimGray;
            btn_CaiDat.FlatAppearance.MouseOverBackColor = Color.DimGray;
            Pressed6.Visible = true;
            btn_KhoHang.BackColor = Color.Gray;
            btn_TongQuan.BackColor = Color.Gray;
            btn_ThuChi.BackColor = Color.Gray;
            btn_BaoCao.BackColor = Color.Gray;
            btn_BanHang.BackColor = Color.Gray;
            Pressed1.Visible = false;
            Pressed2.Visible = false;
            Pressed3.Visible = false;
            Pressed4.Visible = false;
            Pressed5.Visible = false;
            btn_KhoHang.FlatAppearance.MouseOverBackColor = default(Color);
            btn_ThuChi.FlatAppearance.MouseOverBackColor = default(Color);
            btn_BaoCao.FlatAppearance.MouseOverBackColor = default(Color);
            btn_BanHang.FlatAppearance.MouseOverBackColor = default(Color);
            btn_TongQuan.FlatAppearance.MouseOverBackColor = default(Color);
            btn_XoaROW.BackColor = Color.Gray;
            btn_XoaROW.Enabled = false;
        }

        private void saoLưuDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Login.SetValueForText2 == "1")
            {
                ERRORSET.Text = "";
                string CurrentDatabasePath = Environment.CurrentDirectory + @"\QLMT.mdb";
                string test = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " ";
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string PathtobackUp = fbd.SelectedPath.ToString();
                    File.Copy(CurrentDatabasePath, PathtobackUp + @"\" + test + "QLMT-Backup.mdb", true);
                    MessageBox.Show("Sao lưu dữ liệu thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }         
            }
            else
            {
                Form9 frm = new Form9();
                frm.ShowDialog();
            }
        }

        private void phụcHồiDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Login.SetValueForText2 == "1")
            {
                ERRORSET.Text = "";
                string dbFileName = "QLMT.mdb";
                OpenFileDialog open = new OpenFileDialog();
                if (open.ShowDialog() == DialogResult.OK)
                {
                    string pathBackup = open.FileName.ToString();
                    string CurrentDatabasePath = Path.Combine(Environment.CurrentDirectory, dbFileName);
                    File.Copy(pathBackup, CurrentDatabasePath, true);
                    MessageBox.Show("Khôi phục dữ liệu thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                Form9 frm = new Form9();
                frm.ShowDialog();
            }
        }

        private void lst_view2_MouseClick(object sender, MouseEventArgs e)
        {
            txt_TimMaHang.Text = lst_view2.SelectedItems[0].SubItems[0].Text;
        }
    }
}
