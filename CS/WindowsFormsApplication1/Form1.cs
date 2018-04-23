using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;

namespace WindowsFormsApplication1 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            treeList1.ExportToXmlEx("treeListData.xml");
        }

        private void simpleButton2_Click(object sender, EventArgs e) {
            if(System.IO.File.Exists("treeListData.xml")) {
                treeList1.ImportFromXmlEx("treeListData.xml");
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            string[] fields = new string[] { "FirstName", "LastName", "Address", "Certificate" };                
            for (int i = 0; i < fields.Length; i++)
			{
                TreeListColumn treeListColumn = treeList1.Columns.Add();
                treeListColumn.FieldName = fields[i];
                treeListColumn.VisibleIndex = i;			 
			}

            TreeListNode nodeBob = treeList1.AppendNode(new object[] { "Bob", "Brown", new CustomerAddress() { City = "Chicago", Country = "USA" }, new CustomerCertificate() { Series = "AAA", Number = "11-B-0235" } }, null);
            TreeListNode nodeJohn = treeList1.AppendNode(new object[] { "John", "Stone", new CustomerAddress() { City = "Toronto", Country = "Canada" }, new CustomerCertificate() { Series = "BBB", Number = "11-B-0235" } }, nodeBob);
            TreeListNode nodeJames = treeList1.AppendNode(new object[] { "James", "Carrick", new CustomerAddress() { City = "New York", Country = "USA" }, new CustomerCertificate() { Series = "CCC", Number = "11-B-0235" } }, nodeBob);
            TreeListNode nodeSara = treeList1.AppendNode(new object[] { "Sara", "Taylor", new CustomerAddress() { City = "Vancouver", Country = "Canada" }, new CustomerCertificate() { Series = "DDD", Number = "11-B-0235" } }, null);
            TreeListNode nodeMarry = treeList1.AppendNode(new object[] { "Marry", "Smith", new CustomerAddress() { City = "Calgary", Country = "Canada" }, new CustomerCertificate() { Series = "EE", Number = "11-B-0235" } }, nodeSara);
            TreeListNode nodeMonika = treeList1.AppendNode(new object[] { "Monika", "York", new CustomerAddress() { City = "Philadelphia", Country = "USA" }, new CustomerCertificate() { Series = "FFF", Number = "11-B-0235" } }, nodeSara);


        }
    }

    public class CustomerAddress {
        public string Country { get; set; }
        public string City { get; set; }

        public override string ToString() {
            return String.Format("{0} - {1}", Country, City);
        }
    }

    public class CustomerCertificate {
        public string Series { get; set; }
        public string Number { get; set; }

        public override string ToString() {
            return String.Format("{0} - {1}", Series, Number);
        }
    }
}
