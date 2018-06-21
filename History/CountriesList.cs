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
    public partial class CountriesList : Form
    {
        public CountriesList()
        {
            InitializeComponent();
            CountriesBox.TextChanged += new EventHandler(ChooseCountry);
            CountriesBox.Items.Clear();
            CountriesBox.Items.Add("New country");
            foreach (Country c in Form1.Countries)
            {
                if (c.Name != "None")
                    CountriesBox.Items.Add(c.Name);
            }
            
        }

        public static Country ct;
        public int mode = 0;
        public void ChooseCountry(object sender, EventArgs e)
        {
            ComboBox CB = (ComboBox)sender;
            if (CB.Text == "New country")
            {
                NameBox.Text = "Name";
                RulerBox.Text = "Ruler";
                AltRulerBox.Text = "Alternative Ruler";
                SaveButton.Enabled = false;
                CreateButton.Enabled = false;
                mode = 0;
            }
            else
            {
                IEnumerable<Country> query = from c in Form1.Countries
                                             where c.Name == CB.Text
                                             select c;
                ct = query.ElementAt(0);
                NameBox.Text = ct.Name;
                RulerBox.Text = ct.CurrentRuler;
                AltRulerBox.Text = ct.AlternateRuler;
                SaveButton.Enabled = true;
                CreateButton.Enabled = false;
                ColorLabel.BackColor = ct.Color;
                mode = 1;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            ct.Name = NameBox.Text;
            ct.CurrentRuler = RulerBox.Text;
            ct.AlternateRuler = AltRulerBox.Text;
            ct.Color = ColorLabel.BackColor;
            Form1 f1 = new Form1();
            f1.MapUpdate(Form1.mode);
            CountriesBox.Items.Clear();
            CountriesBox.Items.Add("New country");
            foreach (Country p in Form1.Countries)
                CountriesBox.Items.Add(p.Name);
            CountriesBox.SelectedItem = ct.Name;
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            Country c = new Country();
            c.Name = NameBox.Text;
            c.CurrentRuler = RulerBox.Text;
            c.AlternateRuler = AltRulerBox.Text;
            c.Color = ColorLabel.BackColor;
            Form1.Countries.Add(c);
            CountriesBox.Items.Clear();
            CountriesBox.Items.Add("New country");
            foreach (Country p in Form1.Countries)
                CountriesBox.Items.Add(p.Name);
            CountriesBox.SelectedItem = c.Name;

            Form1 f1 = new Form1();
            f1.MapUpdate(Form1.mode);
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            if (mode == 0)
            {
                CreateButton.Enabled = true;

            }
            else
            {
                SaveButton.Enabled = true;
            }
            if (colorDialog1.Color == Color.White || colorDialog1.Color == Color.Blue)
            {
                colorDialog1.Color = Color.Empty;
                SaveButton.Enabled = false;
                CreateButton.Enabled = false;

            }
            foreach (Country c in Form1.Countries)
            {
                if (colorDialog1.Color == c.Color)
                {
                    colorDialog1.Color = Color.Empty;
                    SaveButton.Enabled = false;
                    CreateButton.Enabled = false;
                }

            }
            ColorLabel.BackColor = colorDialog1.Color;

        }
    }
}


