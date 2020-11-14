using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TINH_DTB
{
    

    public partial class frmMain : Form
    {

        public frmMain()
        {
            InitializeComponent();
        }

        monHoc[] mh = new monHoc[1000];
        int count = 0;

        private void frmMain_Load(object sender, EventArgs e)
        {
            btnNhap.Enabled = false;
            btnXoaDL.Enabled = false;
            btnTinh.Enabled = false;
        }

        private void tbNhap_Leave(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            if (ctr.Text.Trim().Length == 0)
            {
                this.errorProvider1.SetError(tbNhap, "Chưa nhập điểm");
                tbNhap.Focus();
            }
            else if (double.TryParse(tbNhap.Text, out double val) == false)
            {
                this.errorProvider1.SetError(tbNhap, "Dữ liệu không hợp lệ");
                tbNhap.Focus();
            }
            else
                this.errorProvider1.Clear();
        }

        private void tbSoTC_Leave(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            if (ctr.Text.Trim().Length == 0)
            {
                this.errorProvider1.SetError(tbSoTC, "Chưa nhập số TC");
                tbSoTC.Focus();
            }
            else if (double.TryParse(tbSoTC.Text, out double val) == false)
            {
                this.errorProvider1.SetError(tbSoTC, "Dữ liệu không hợp lệ");
                tbSoTC.Focus();
            }
            else
                this.errorProvider1.Clear();
        }

        private void tbSoTC_TextChanged(object sender, EventArgs e)
        {
            btnNhap.Enabled = true;
        }

        int ChuyenDoi(double dtbhe10)
        {

            if (dtbhe10 < 4) return 0;
            else if (dtbhe10 >= 4 && dtbhe10 < 5.5) return 1;
            else if (dtbhe10 >= 5.5 && dtbhe10 < 7) return 2;
            else if (dtbhe10 >= 7 && dtbhe10 < 8.5) return 3;
            else return 4;
        }

        double tinhDiemHe4()
        {
            double DTB = 0, TongTC = 0, TuSo = 0;
            for (int i=0; i < count; i++)
            {
                TuSo += ChuyenDoi(mh[i]._diemHe10) * mh[i]._soTC;
                TongTC += mh[i]._soTC;
            }
            DTB = (double)TuSo / TongTC;
            return Math.Round(DTB, 2);
        }

        double tinhDiemHe10()
        {
            double DTB = 0, TongTC = 0, TuSo = 0;
            for (int i = 0; i < count; i++)
            {
                TuSo += mh[i]._diemHe10 * mh[i]._soTC;
                TongTC += mh[i]._soTC;
            }
            DTB = (double)TuSo / TongTC;
            return Math.Round(DTB, 2);
        }

        string xepLoai()
        {
            double Diem = tinhDiemHe4();
            if (Diem >= 0 && Diem < 1) return ("Kém");
            else if (Diem >= 1 && Diem < 2) return ("Yếu");
            else if(Diem >= 2 && Diem < 2.5) return ("Trung Bình");
            else if(Diem >= 2.5 && Diem < 3.2) return ("Khá");
            else if(Diem >= 3.2 && Diem < 3.6) return ("Giỏi");
            else return ("Xuất sắc");
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            mh[count] = new monHoc();
            mh[count]._diemHe10 = double.Parse(tbNhap.Text);
            mh[count]._soTC += int.Parse(tbSoTC.Text);
            count++;
            MessageBox.Show("Đã nhập "+(count).ToString()+" môn học");
            btnXoaDL.Enabled = true;
            btnTinh.Enabled = true;
            tbNhap.Focus();
        }

        private void btnTinh_Click(object sender, EventArgs e)
        {
            tbDTB4.Text = tinhDiemHe4().ToString();
            tbDTB10.Text = tinhDiemHe10().ToString();
            tbLoai.Text = xepLoai();
        }

        private void btnXoaDL_Click(object sender, EventArgs e)
        {
            count = 0;
            tbNhap.Clear();
            tbSoTC.Clear();
            tbDTB10.Clear();
            tbDTB4.Clear();
            tbLoai.Clear();
            tbNhap.Focus();
            btnNhap.Enabled = false;
            btnTinh.Enabled = false;
            btnXoaDL.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to close?", "Exit",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question,
               MessageBoxDefaultButton.Button1);

            if (dialogResult == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }

    class monHoc
    {
        private double diemHe10;
        private int soTC;

        public monHoc()
        {
            diemHe10 = 0;
            soTC = 0;
        }

        public double _diemHe10
        {
            set { diemHe10 = value; }
            get { return diemHe10; }
        }

        public int _soTC
        {
            set { soTC = value; }
            get { return soTC; }
        }
    }
}
