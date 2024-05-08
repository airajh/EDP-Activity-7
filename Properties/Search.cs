using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Excel = Microsoft.Office.Interop.Excel;

namespace skincare.Properties
{
    public partial class Search : Form
    {
        MySqlConnection conn;
        string myConnectionString;

        public Search()
        {
            InitializeComponent();
            SetupAutoComplete();
            LoadDataIntoDataGridView();

            myConnectionString = "server=127.0.0.1;uid=root;" +
            "pwd=airajhean;database=skincare";

            // Initialize MySqlConnection
            conn = new MySqlConnection(myConnectionString);
        }

        private void srchBtn_Click(object sender, EventArgs e)
        {
            string searchQuery = searchTxt.Text.Trim();

            // Ensure search query is not empty
            if (string.IsNullOrEmpty(searchQuery))
            {
                MessageBox.Show("Please enter a search query");
                return;
            }

            try
            {
                // Open connection
                conn.Open();

                // Prepare SQL query
                string query = "SELECT * FROM employee WHERE empname LIKE @searchQuery OR empsalary LIKE @searchQuery";

                // Create MySqlCommand
                MySqlCommand cmd = new MySqlCommand(query, conn);

                // Add parameter to the command
                cmd.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%");

                // Create a DataTable to store the search results
                DataTable dataTable = new DataTable();

                // Create a MySqlDataAdapter to fill the DataTable
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                // Fill the DataTable with the search results
                adapter.Fill(dataTable);

                // Bind the DataTable to the DataGridView to display the search results
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close connection
                conn.Close();
            }
        }

        private void searchTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void SetupAutoComplete()
        {
            // Create a list to store suggestions
            List<string> suggestions = new List<string>();

            // Populate the list with unique values from the DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    string value = cell.Value?.ToString();
                    if (!string.IsNullOrEmpty(value) && !suggestions.Contains(value))
                    {
                        suggestions.Add(value);
                    }
                }
            }

            // Configure AutoComplete mode and add suggestions to the TextBox
            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
            autoComplete.AddRange(suggestions.ToArray());
            searchTxt.AutoCompleteMode = AutoCompleteMode.Suggest;
            searchTxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
            searchTxt.AutoCompleteCustomSource = autoComplete;

        }
        private void LoadDataIntoDataGridView()
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;" +
            "pwd=airajhean;database=skincare";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT *  FROM products";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            catch (MySqlException ex)//
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
               
            }
        }

        private void Search_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
                try
                {
                    // Create a new Excel application
                    Excel.Application excelApp = new Excel.Application();
                    excelApp.Visible = true;

                    // Open the Excel template file (replace Template.xlsx with your template's file name)
                    Excel.Workbook workbook = excelApp.Workbooks.Open(@"C:\Users\Aira\Documents\Templates.xlsx");
                    Excel.Worksheet dataSheet = workbook.Sheets[1]; // Assuming Sheet1 for data
                    Excel.Worksheet graphSheet = workbook.Sheets[2]; // Assuming Sheet2 for graph

                    int startRow = 19; // Adjust this according to your template
                    int startColumn = 3; // Adjust this according to your template


                    // Get the DataGridView data
                    DataGridViewRowCollection rows = dataGridView1.Rows;
                    DataGridViewColumnCollection columns = dataGridView1.Columns;

                    // Write DataGridView data to Excel dataSheet
                    for (int i = 0; i < rows.Count; i++)
                    {
                        for (int j = 0; j < columns.Count; j++)
                        {
                            dataSheet.Cells[startRow + i, startColumn + j] = rows[i].Cells[j].Value?.ToString();
                        }
                    }




                    // Define the range for the data
                    Excel.Range dataRange = dataSheet.Range[dataSheet.Cells[startRow, startColumn], dataSheet.Cells[startRow + rows.Count - 1, startColumn + columns.Count - 1]];



                    // Define the range for the cell where you want to anchor the chart
                    Excel.Range anchorRange = graphSheet.Cells[25, 25];


                    // Add a chart
                    Excel.ChartObjects chartObjects = (Excel.ChartObjects)graphSheet.ChartObjects();
                    Excel.ChartObject chartObject = chartObjects.Add(100, 100, 300, 200); // Adjust position and size as needed
                    Excel.Chart chart = chartObject.Chart;

                    // Set chart type
                    chart.ChartType = Excel.XlChartType.xlColumnClustered; // Change this to the desired chart type

                    // Set chart data
                    chart.SetSourceData(dataRange);

                    // Set chart title
                    chart.HasTitle = true;
                    chart.ChartTitle.Text = "Skin Care Chart";

                    // Save the Excel file with a new name
                    workbook.SaveAs(@"C:\Users\Aira\Documents\Reports.xlsx");



                    // Release COM objects
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(chart);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(chartObject);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(chartObjects);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(dataRange);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(graphSheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(dataSheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                    MessageBox.Show("Excel file has been created successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        


        private void comboBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }






        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                string selectedTable = comboBox1.SelectedItem.ToString();

                switch (selectedTable)
                {
                    case "Customer":
                        DisplayCustomerTable();
                        break;
                    case "Transaction":
                        DisplayTransactionTable();
                        break;
                    case "Supplier":
                        DisplaySupplierTable();
                        break;
                    default:
                        break;
                }
            }

            // Method to display data from the User table
            private void DisplayCustomerTable()
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM customer";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    DataTable dataTable = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

            // Similar methods for displaying data from Event and Budget tables
            private void DisplayTransactionTable()
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM transaction";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    DataTable dataTable = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

            private void DisplaySupplierTable()
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM supplier";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    DataTable dataTable = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }


    
