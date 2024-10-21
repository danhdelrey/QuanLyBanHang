using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHang
{
    public partial class Form2 : Form
    {
        public static string SharedData;
        public static string SharedData_tdn;

        string strConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=SSPI";
        
        SqlConnection conn = null;
        SqlDataAdapter daTenDangNhap = null;

        public Form2()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            try
            {
                conn = new SqlConnection(strConnectionString);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table. Lỗi rồi!!!");
            }
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            daTenDangNhap = new SqlDataAdapter(
                "SELECT * FROM TaiKhoan WHERE tendangnhap = '" + this.txtUser.Text +
                "' AND matkhau = '" + this.txtPass.Text + "'", conn);
            //daTenDangNhap = new SqlDataAdapter("SELECT * FROM TaiKhoan WHERE tendangnhap = "+ this.txtUser.Text +" AND matkhau = " + this.txtPass.Text, conn);
            DataTable dtTenDangNhap = new DataTable();
            dtTenDangNhap.Clear();
            daTenDangNhap.Fill(dtTenDangNhap);
            if (dtTenDangNhap.Rows.Count > 0)
            {
                SharedData = txtPass.Text;
                SharedData_tdn = txtUser.Text;
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Không đúng tên người dùng / mật khẩu!!!", "Thông báo");
                this.txtUser.Focus();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult traloi = MessageBox.Show("Chắc không?", "Trả lời", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (traloi == DialogResult.OK)
                Application.Exit();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
