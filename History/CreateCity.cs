using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace History
{
    public partial class CreateCity : Form
    {
        public CreateCity(Province pr)
        {
            InitializeComponent();
            p = pr;
        }
        public static Province p;

        private void PopulationTrackBar_Scroll(object sender, EventArgs e)
        {
            PopulationLabel.Text = "Population: " + PopulationTrackBar.Value;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateCityButton_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            foreach (string a in Form1.ProductName)
            {
                if (a != "tools" && a != "weapons" && a != "ale" && a != "cloth")
                {
                    Control Checked = GetByChecked((Panel)GetByName(a));
                    if (Checked.Name.Contains("Low"))
                    {
                        p.BProduction[a] = rand.Next(0, 8);
                    }
                    else if (Checked.Name.Contains("Normal"))
                    {
                        p.BProduction[a] = rand.Next(8, 14);
                    }
                    else
                    {
                        p.BProduction[a] = rand.Next(14, 21);
                    }
                }

            }
            p.Buildings["tools"] = (int)ToolsNumeric.Value;
            p.Buildings["weapons"] = (int)WeaponsNumeric.Value;
            p.Buildings["ale"] = (int)AleNumeric.Value;
            p.Buildings["cloth"] = (int)ClothNumeric.Value;
            p.ProvinceName = CityNameBox.Text;
            for(int a = PopulationTrackBar.Value; a != 0; a--)
            {
                Human h = new Human();
                h.age = rand.Next(12, 60);
                p.Population.Add(h);
            }
            this.Close();
        }
        private Control GetByName(string name)
        {
            List<Control> a = new List<Control>();
            foreach (Control c in Panel.Controls)
            {
                    a.Add(c);
            }
            IEnumerable<Control> query = from n in a
                                         where n.Name == name
                                         select n;
            return query.ElementAt(0);
        }
        private Control GetByChecked(Panel p)
        {
            List<RadioButton> a = new List<RadioButton>();
            foreach(RadioButton c in p.Controls)
            {
                a.Add(c);
            }
            IEnumerable<Control> query =   from n in a
                                             where n.Checked == true
                                             select n;
            return query.ElementAt(0);
        }
    }
}
