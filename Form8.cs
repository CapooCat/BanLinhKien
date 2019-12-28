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
    public partial class Form8 : Form
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
        public Form8()
        {
            InitializeComponent();
            txthoten.ForeColor = Color.LightGray;
            txthoten.Text = "Tran Huy Duc";
            this.txthoten.Leave += new System.EventHandler(this.txthoten_Leave);
            this.txthoten.Enter += new System.EventHandler(this.txthoten_Enter);

            txtsdt.ForeColor = Color.Gray;
            txtsdt.Text = "-0909012212";

            txtdiachi.ForeColor = Color.Gray;
            txtdiachi.Text = "233 Dong Da,Ha Noi";

            txtemail.ForeColor = Color.Gray;
            txtemail.Text = "huyduc@gmail.com";

            txt_user.ForeColor = Color.Gray;
            txt_user.Text = "nhoxking500";
        }

        private void txthoten_Leave(object sender, EventArgs e)
        {
            if (txthoten.Text == "")
            {
                txthoten.Text = "Tran Huy Duc";
                txthoten.ForeColor = Color.Gray;
            }
        }

        private void txthoten_Enter(object sender, EventArgs e)
        {
            if (txthoten.Text == "Tran Huy Duc")
            {
                txthoten.Text = "";
                txthoten.ForeColor = Color.Black;
            }
        }
        private void txtsdt_Leave(object sender, EventArgs e)
        {
            if (txtsdt.Text == "")
            {
                txtsdt.Text = "-0909012212";
                txtsdt.ForeColor = Color.Gray;
            }
        }

        private void txtsdt_Enter(object sender, EventArgs e)
        {
            if (txtsdt.Text == "-0909012212")
            {
                txtsdt.Text = "";
                txtsdt.ForeColor = Color.Black;
            }
        }
        private void txtdiachi_Leave(object sender, EventArgs e)
        {
            if (txtdiachi.Text == "")
            {
                txtdiachi.Text = "233 Dong Da,Ha Noi";
                txtdiachi.ForeColor = Color.Gray;
            }
        }

        private void txtdiachi_Enter(object sender, EventArgs e)
        {
            if (txtdiachi.Text == "233 Dong Da,Ha Noi")
            {
                txtdiachi.Text = "";
                txtdiachi.ForeColor = Color.Black;
            }
        }
        private void txtemail_Leave(object sender, EventArgs e)
        {
            if (txtemail.Text == "")
            {
                txtemail.Text = "huyduc@gmail.com";
                txtemail.ForeColor = Color.Gray;
            }
        }

        private void txtemail_Enter(object sender, EventArgs e)
        {
            if (txtemail.Text == "huyduc@gmail.com")
            {
                txtemail.Text = "";
                txtemail.ForeColor = Color.Black;
            }
        }
        private void txt_user_Leave(object sender, EventArgs e)
        {
            if (txt_user.Text == "")
            {
                txt_user.Text = "nhoxking500";
                txt_user.ForeColor = Color.Gray;
            }
        }

        private void txt_user_Enter(object sender, EventArgs e)
        {
            if (txt_user.Text == "nhoxking500")
            {
                txt_user.Text = "";
                txt_user.ForeColor = Color.Black;
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            if (txthoten.Text == "Tran Huy Duc" || txtsdt.Text == "-0909012212" || txtdiachi.Text == "233 Dong Da,Ha Noi" || txtemail.Text == "huyduc@gmail.com" || txt_user.Text == "nhoxking500" || txt_pass.Text == "" || txt_repass.Text == "")
            {
                lbl_error.ForeColor = Color.Red;
                lbl_error.Text = "Không được để trống!";
            }
            else if (txt_pass.Text != txt_repass.Text)
            {
                lbl_error.ForeColor = Color.Red;
                lbl_error.Text = "Mật khẩu nhập lại không đúng!";
            }
            else
            {
                int n = obj.KiemTra("Select * From NHANVIEN") + 1;
                int n2 = obj.KiemTra("Select * From TAIKHOAN") + 1;
                DataTable tbl = new DataTable();
                DataTable tbl1 = new DataTable();
                tbl = obj.LayDuLieu("Select * From NHANVIEN");
                tbl1 = obj.LayDuLieu("Select * From TAIKHOAN");
                for (int i = 1; i <= n2; i++)
                {
                    String check = "TK0" + i;
                    int count = 0;
                    for (int j = 0; j < n2 - 1; j++)
                    {
                        if (check == tbl1.Rows[j][0].ToString())
                        { }
                        else if (count < n2 - 2)
                        {
                            count++;
                        }
                        else
                        {
                            n2 = i;
                        }
                    }
                }
                for (int i = 1; i <= n; i++)
                {
                    String check = "00" + i;
                    int count = 0;
                    for (int j = 0; j < n - 1; j++)
                    {
                        if (check == tbl.Rows[j][0].ToString())
                        { }
                        else if (count < n - 2)
                        {
                            count++;
                        }
                        else
                        {
                            n = i;
                        }
                    }
                }
                String matk = "TK0" + n2;
                String sql_tk = "insert into TAIKHOAN(MaTK,[User],[Password],LoaiTk)";
                sql_tk += " values('TK0" + n2 + "','" + txt_user.Text + "','" + txt_pass.Text + "','NV')";
                String sql = "insert into NHANVIEN(MaNV,TenNV,SDT,DiaChi,Email,MaTK)";
                sql += " values('00" + n + "','" + txthoten.Text + "','" + txtsdt.Text + "','" + txtdiachi.Text + "','" + txtemail.Text + "','" + matk + "')";
                if (obj.CapNhatDuLieu(sql_tk) != 0)
                {
                    if (obj.CapNhatDuLieu(sql) != 0)
                    {
                        lbl_error.ForeColor = Color.Green;
                        lbl_error.Text = "Thêm nhân viên thành công";
                    }
                }
            }
        }
    }
}
