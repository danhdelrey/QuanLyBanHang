using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHang
{
    public partial class frmDoiMatKhau : Form
    {
        public static string currentTaiKhoan = null;
        string strConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=SSPI";
        //Đối tượng kết nối
        SqlConnection conn = null;
        SqlDataAdapter daTenDangNhap = null;
        SqlDataAdapter daMatKhau = null;
        //Đối tượng hiển thị dữ liệu lên Form
        //DataTable dtKhachHang = null;

        void LoadData()
        {
            try
            {
                conn = new SqlConnection(strConnectionString);
                conn.Open();
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table. Lỗi rồi!!!");
            }
        }

        public frmDoiMatKhau()
        {
            InitializeComponent();
            this.textBox1.PasswordChar = '*'; // Dùng ký tự tùy chỉnh
            this.textBox1.UseSystemPasswordChar = false; // Đảm bảo không dùng ký tự hệ thống

            this.textBox2.PasswordChar = '*'; // Dùng ký tự tùy chỉnh
            this.textBox2.UseSystemPasswordChar = false; // Đảm bảo không dùng ký tự hệ thống
        }

        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {
            LoadData();
            currentTaiKhoan = Form2.SharedData;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string oldPassword = this.textBox1.Text;
            string newPassword = this.textBox2.Text;
            if(string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("Không được để trống ô nhập liệu!");
            }
            else if(oldPassword != currentTaiKhoan)
            {
                MessageBox.Show("Mật khẩu cũ không đúng!");
            }
            // Điều kiện 1: Độ dài mật khẩu phải từ 8 đến 12 ký tự
            else if (newPassword.Length < 8 || newPassword.Length > 12)
            {
                MessageBox.Show("Mật khẩu mới phải có độ dài từ 8 đến 12 ký tự.");
            }
            // Điều kiện 2: Phải có ít nhất 1 ký tự hoa, 1 ký tự thường, 1 chữ số và 1 ký tự đặc biệt
            else if (!Regex.IsMatch(newPassword, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).+$"))
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 1 ký tự hoa, 1 ký tự thường, 1 chữ số và 1 ký tự đặc biệt.");
            }
            // Điều kiện 3: Mật khẩu mới trùng với mật khẩu cũ
            else if (newPassword == oldPassword)
            {
                MessageBox.Show("Mật khẩu mới không được trùng với mật khẩu cũ.");
            }
            // Điều kiện 4: Mật khẩu mới không được chứa ký tự khoảng trắng
            else if (newPassword.Contains(" "))
            {
                MessageBox.Show("Mật khẩu mới không được chứa ký tự khoảng trắng.");
            }
            // Điều kiện 5: Mật khẩu mới không được có ký tự lặp lại liên tiếp 3 lần
            else if (Regex.IsMatch(newPassword, @"(.)\1\1"))
            {
                MessageBox.Show("Mật khẩu mới không được có ký tự lặp lại liên tiếp 3 lần.");
            }
            // Điều kiện 6: Không sử dụng mẫu ký tự đơn giản hoặc dãy số tuần tự
            else if (Regex.IsMatch(newPassword, @"(abc|123|password|qwerty|abcdef|123456|qwert|98765)"))
            {
                MessageBox.Show("Mật khẩu mới không được sử dụng mẫu ký tự đơn giản hoặc dãy số tuần tự.");
            }
            else
            {
                string sql = "UPDATE TaiKhoan SET matkhau = @newPassword WHERE tendangnhap = @username";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@newPassword", newPassword);
                    cmd.Parameters.AddWithValue("@username", Form2.SharedData_tdn);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Đổi mật khẩu thành công!.");
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
