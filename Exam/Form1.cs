using System;
using System.Net.Http;
using System.Windows.Forms;

namespace Exam
{
    public partial class Form1 : Form
    {
        private string ApiKey = "[yourApiKey]";

        public Form1()
        {
            InitializeComponent();
        }

        private async void buttonSearch_ClickAsync(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxSearch.Text))
            {
                string title = textBoxSearch.Text;

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        string link = $"http://www.omdbapi.com/?t={title}&apikey={ApiKey}";
                        HttpResponseMessage response = await client.GetAsync(link);

                        Data data = await response.Content.ReadAsAsync<Data>();

                        textBoxYear.Text = data.Year;
                        textBoxGenre.Text = data.Genre;
                        textBoxCountry.Text = data.Country;
                        textBoxMetascore.Text = data.Metascore;
                        pictureBox1.ImageLocation = data.Poster;

                        textBoxDescription.Text = $"Director: {data.Director}\t\t\tRuntime: {data.Runtime}\t\t\tActors: {data.Actors}\t\t\tPlot: {data.Plot}";
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Something went wrong\n\nError:{ex.Message}", "Unknown error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
                MessageBox.Show("Enter title of the film", "Search field", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}