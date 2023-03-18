using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Chart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            fillChart();
        }

        //fillChart method  
        private void fillChart()
        {
            SqlConnection con = new SqlConnection("Server=.\\SQLEXPRESS;Database=WindowsBackgroundServices;Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=True;");
            DataSet ds = new DataSet();
            con.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("Select Country,SalePrice from FileData", con);
            adapt.Fill(ds);
            chart1.DataSource = ds;
            //set the member of the chart data source used to data bind to the X-values of the series  
            chart1.Series["Salary"].XValueMember = "Country";
            //set the member columns of the chart data source used to data bind to the X-values of the series  
            chart1.Series["Salary"].YValueMembers = "SalePrice";
            chart1.Titles.Add("Salary Chart");
            con.Close();
        }
    }
}
