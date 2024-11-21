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
    public partial class FrmProduct : Form
    {
        public FrmProduct()
        {
            InitializeComponent();
        }
        DBCustomerDBFirstEntities db = new DBCustomerDBFirstEntities();
        private void FrmProduct_Load(object sender, EventArgs e)
        {
            ProductList();
            var categories=db.TblCategory.ToList();
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryId";
            cmbCategory.DataSource = categories;
        }

        private void ProductList()
        {
            //dataGridView1.DataSource = db.TblProduct.ToList();
            var products = (from p in db.TblProduct
                            join c in db.TblCategory
                            on p.CategoryId equals c.CategoryId
                            select new
                            {
                                ProductId=p.ProductId,
                                ProductName=p.ProductName,
                                ProductStock=p.ProductStock,
                                ProductPrice=p.ProductPrice,
                                CategoryId=c.CategoryId,
                                CategoryName=c.CategoryName
                            }).ToList();
            dataGridView1.DataSource = products;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            TblProduct product = new TblProduct();
            product.ProductName= txtProductName.Text;   
            product.ProductStock=int.Parse(txtProductStock.Text);
            product.ProductPrice=decimal.Parse(txtProductPrice.Text);
            product.CategoryId=int.Parse(cmbCategory.SelectedValue.ToString());
            db.TblProduct.Add(product);
            db.SaveChanges();
            ProductList();  

        }
    }
}
