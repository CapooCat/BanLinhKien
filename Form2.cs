using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace BanLinhKien
{
    public partial class Login : Form
    {
        int TogMove;
        int MValX;
        int MValY;
        public static string SetValueForText1 = "0";
        public static string SetValueForText2 = "0";
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
        public Login()
        {
            InitializeComponent();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
        private void btn_Login_Click(object sender, EventArgs e)
        {
            DataTable Login = new DataTable();
            if (obj.KiemTra("Select * from TAIKHOAN where User='" + txt_user.Text + "' and Password='" + txt_password.Text + "' and LoaiTk='Admin'") == 1)
            {
                Login = obj.LayDuLieu("Select TenNV From NHANVIEN,TAIKHOAN where NHANVIEN.MaTK=TAIKHOAN.MaTK  AND LoaiTk='Admin'");
                SetValueForText1 = Login.Rows[0][0].ToString();
                SetValueForText2 = "1";
                this.Close();
            }
            else if (obj.KiemTra("Select * from TAIKHOAN where User='" + txt_user.Text + "' and Password='" + txt_password.Text + "'") == 1)
            {
                Login = obj.LayDuLieu("Select TenNV From NHANVIEN,TAIKHOAN where NHANVIEN.MaTK=TAIKHOAN.MaTK AND User Like '" + txt_user.Text + "' and Password Like '" + txt_password.Text + "'");
                SetValueForText1 = Login.Rows[0][0].ToString();
                SetValueForText2 = "0";
                this.Close();
            }
            else
                label.ForeColor = Color.Red;
                label.Text = "Nhập sai user hoặc mật khẩu";
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void txt_user_TextChanged(object sender, EventArgs e)
        {

        }
        private void DisableEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btn_Login.PerformClick();
            }
        }

        private void btn_Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btn_Login.PerformClick();
        }
    }
}
