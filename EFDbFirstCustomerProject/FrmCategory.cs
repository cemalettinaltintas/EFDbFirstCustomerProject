using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFDbFirstCustomerProject
{
    public partial class FrmCategory : Form
    {
        public FrmCategory()
        {
            InitializeComponent();
        }
        
        DBCustomerDBFirstEntities db=new DBCustomerDBFirstEntities();
        private void FrmCategory_Load(object sender, EventArgs e)
        {
            CategoryList();
        }

        private void CategoryList()
        {
            //dataGridView1.DataSource = db.TblCategory.Where(x=>x.Status==true).ToList();
            var values=(from p in db.TblCategory select new
            {
                KategoriID=p.CategoryId,
                KategoriAdı=p.CategoryName
            }).ToList();
            dataGridView1.DataSource = values;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            TblCategory category = new TblCategory();
            category.CategoryName=txtCategoryName.Text;
            category.Status = true;
            db.TblCategory.Add(category);
            db.SaveChanges();

            CategoryList();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCategoryId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtCategoryName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id=Convert.ToInt32(txtCategoryId.Text);
            var category= db.TblCategory.Find(id);
            category.CategoryName = txtCategoryName.Text;
            db.SaveChanges();
            CategoryList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtCategoryId.Text);
            var category = db.TblCategory.Find(id);
            //db.TblCategory.Remove(category);
            category.Status = false;
            db.SaveChanges();
            CategoryList();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var values=db.TblCategory.Where(x=>x.CategoryName.Contains(txtCategoryName.Text)).ToList();  
            dataGridView1.DataSource = values;
            if (values.Count==0) { 
                MessageBox.Show("Bulanamadı!");
            }
            
        }
    }
}