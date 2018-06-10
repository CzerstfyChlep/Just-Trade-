using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace History
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            if (!Countries.Any())
            {
                Country Gaia = new Country();
                Gaia.Name = "None";
                Countries.Add(Gaia);
                Gaia.Color = Color.Gray;
            }
            foreach (Label ctrl in Map.Controls)
            {
                MakeHex(ctrl);
                Province prv = new Province(ctrl);
                Provinces.Add(prv);
                ctrl.Click += new EventHandler(ProvinceClick);
                prv.Terrain = "water";
                prv.Development = 1;
                prv.Fortifications = 0;
                ctrl.Text = "#";
                GetByObject(ctrl).Owner = GetByName("None");              
            }          
            TerrainBox.TextChanged += new EventHandler(TerrainChange);
            IEnumerable<Province> query = Provinces.OrderBy(n => n.ctrl.Name);
            Provinces = query.ToList();
            PlayerCurrent = Provinces[150];

        }
        public static List<Province> Provinces = new List<Province> { };
      
        private void MakeHex(Control Button)
        {
            Button.SetBounds(Button.Location.X, Button.Location.Y, 50, 50);
            GraphicsPath polygon_path = new GraphicsPath(FillMode.Winding);
            polygon_path.AddPolygon(GetPoints(Button.ClientRectangle));
            Region polygon_region = new Region(polygon_path);
            Button.Region = polygon_region;
        }
        public Point[] GetPoints(Rectangle container)
        {
            Point[] points = new Point[6];
            int half = container.Height / 2;
            int quart = container.Width / 4;
            points[0] = new Point(container.Left + quart, container.Top);
            points[1] = new Point(container.Right - quart, container.Top);
            points[2] = new Point(container.Right, container.Top + half);
            points[3] = new Point(container.Right - quart, container.Bottom);
            points[4] = new Point(container.Left + quart, container.Bottom);
            points[5] = new Point(container.Left, container.Top + half);
            return points;
        }
        
        public static int idstore = 0;
        public static bool BrushFlag = false;
        public static Label Current;
        public static Province Clicked;
        public static Province PrevClicked;
        public static List<Country> Countries = new List<Country>();
        public static int mode = 0;
        public int Year;
        public static Province PlayerCurrent;
        public void UpdateProductionStorage()
        {
            if(Clicked.Production["wood"] >= 0)
                WoodLabel.Text = "Wood: " + Clicked.Storage["wood"] + "/+" + Clicked.Production["wood"];
            else
                WoodLabel.Text = "Wood: " + Clicked.Storage["wood"] + "/" + Clicked.Production["wood"];

            if (Clicked.Production["bricks"] >= 0)
                WoodLabel.Text = "Bricks: " + Clicked.Storage["bricks"] + "/+" + Clicked.Production["bricks"];
            else
                WoodLabel.Text = "Bricks: " + Clicked.Storage["bricks"] + "/" + Clicked.Production["bricks"];

            if (Clicked.Production["wheat"] >= 0)
                WoodLabel.Text = "Wheat: " + Clicked.Storage["wheat"] + "/+" + Clicked.Production["wheat"];
            else
                WoodLabel.Text = "Wheat: " + Clicked.Storage["wheat"] + "/" + Clicked.Production["wheat"];

            if (Clicked.Production["metals"] >= 0)
                WoodLabel.Text = "Metals: " + Clicked.Storage["metals"] + "/+" + Clicked.Production["metals"];
            else
                WoodLabel.Text = "Metals: " + Clicked.Storage["metals"] + "/" + Clicked.Production["metals"];

            if (Clicked.Production["tools"] >= 0)
                WoodLabel.Text = "Tools: " + Clicked.Storage["tools"] + "/+" + Clicked.Production["tools"];
            else
                WoodLabel.Text = "Tools: " + Clicked.Storage["tools"] + "/" + Clicked.Production["tools"];

            if (Clicked.Production["tools"] >= 0)
                WoodLabel.Text = "Tools: " + Clicked.Storage["tools"] + "/+" + Clicked.Production["tools"];
            else
                WoodLabel.Text = "Tools: " + Clicked.Storage["tools"] + "/" + Clicked.Production["tools"];

            if (Clicked.Production["weapons"] >= 0)
                WoodLabel.Text = "Weapons: " + Clicked.Storage["weapons"] + "/+" + Clicked.Production["weapons"];
            else
                WoodLabel.Text = "Weapons: " + Clicked.Storage["weapons"] + "/" + Clicked.Production["weapons"];

            if (Clicked.Production["wool"] >= 0)
                WoodLabel.Text = "Wool: " + Clicked.Storage["wool"] + "/+" + Clicked.Production["wool"];
            else
                WoodLabel.Text = "Wool: " + Clicked.Storage["Wool"] + "/" + Clicked.Production["wool"];

            if (Clicked.Production["cloth"] >= 0)
                WoodLabel.Text = "Cloth: " + Clicked.Storage["cloth"] + "/+" + Clicked.Production["cloth"];
            else
                WoodLabel.Text = "Cloth: " + Clicked.Storage["cloth"] + "/" + Clicked.Production["cloth"];

            if (Clicked.Production["furs"] >= 0)
                WoodLabel.Text = "Furs: " + Clicked.Storage["furs"] + "/+" + Clicked.Production["furs"];
            else
                WoodLabel.Text = "Furs: " + Clicked.Storage["furs"] + "/" + Clicked.Production["furs"];

            if (Clicked.Production["ale"] >= 0)
                WoodLabel.Text = "Ale: " + Clicked.Storage["ale"] + "/+" + Clicked.Production["ale"];
            else
                WoodLabel.Text = "Ale: " + Clicked.Storage["ale"] + "/" + Clicked.Production["ale"];

            if (Clicked.Production["wine"] >= 0)
                WoodLabel.Text = "Wine: " + Clicked.Storage["wine"] + "/+" + Clicked.Production["wine"];
            else
                WoodLabel.Text = "Wine: " + Clicked.Storage["wine"] + "/" + Clicked.Production["wine"];

            if (Clicked.Production["spices"] >= 0)
                WoodLabel.Text = "Spices: " + Clicked.Storage["spices"] + "/+" + Clicked.Production["spices"];
            else
                WoodLabel.Text = "Spices: " + Clicked.Storage["spices"] + "/" + Clicked.Production["spices"];

            if (Clicked.Production["salt"] >= 0)
                WoodLabel.Text = "Salt: " + Clicked.Storage["salt"] + "/+" + Clicked.Production["salt"];
            else
                WoodLabel.Text = "Salt: " + Clicked.Storage["salt"] + "/" + Clicked.Production["salt"];

            if (Clicked.Production["hemps"] >= 0)
                WoodLabel.Text = "Hemps: " + Clicked.Storage["hemps"] + "/+" + Clicked.Production["hemps"];
            else
                WoodLabel.Text = "Hemps: " + Clicked.Storage["hemps"] + "/" + Clicked.Production["hemps"];
        }
        private void ProvinceClick(object sender, EventArgs e)
        {                       
            PrevClicked = Clicked;
            MapUpdate(mode, PrevClicked);
            Current = (Label)sender;
            OwnerBox.TextChanged -= OwnerBox_TextChanged;
            TerrainBox.TextChanged -= TerrainChange;
            DevelopmentBox.ValueChanged -= DevelopmentBox_ValueChanged;
            FortificationsBox.ValueChanged -= FortificationsBox_ValueChanged;
            Clicked = GetByObject(Current);
            CapitalBox.Checked = false;
            if (Clicked.Owner.Name == "None")           
                CapitalBox.Enabled = false;           
            else
                CapitalBox.Enabled = true;
            if (Clicked.Occ == null)
            {
                OccupiedCheckBox.Checked = false;
            }
            else
            {
                OccupiedCheckBox.Checked = true;
            }
            
            if (BrushFlag == false)
            {              
                OwnerBox.SelectedText = Clicked.Owner.Name;
                TerrainBox.SelectedItem = Clicked.Terrain;
                Current.BackColor = Color.Aquamarine;
                id.Text = "Id:" + Provinces.IndexOf(Clicked);
                DevelopmentBox.Value = Clicked.Development;
                FortificationsBox.Value = Clicked.Fortifications;              
            }
            else
            {
                Clicked.Terrain = BrushTerrain.Text;
                MapUpdate(0, Clicked);
            }
            OwnerBox.TextChanged += OwnerBox_TextChanged;
            TerrainBox.TextChanged += TerrainChange;
            DevelopmentBox.ValueChanged += DevelopmentBox_ValueChanged;
            FortificationsBox.ValueChanged += FortificationsBox_ValueChanged;

        }
        public Province GetByObject(Label obj)
        {
            IEnumerable<Province> query = from n in Provinces
                                          where n.ctrl == obj
                                          select n;
            return query.ElementAt(0);
        }
        public Country GetByName(string name)
        {
            IEnumerable<Country> query =  from n in Countries
                                          where n.Name == name
                                          select n;

            return query.ElementAt(0);
        }
        private void TerrainChange(object sender, EventArgs e)
        {
            Clicked.Terrain = TerrainBox.Text;
        }
        public void MapUpdate(int mode, Province pro = null)
        {
            OwnerBox.Items.Clear();
            OwnerBox.Text = "";
            OccupantBox.Items.Clear();
            OccupantBox.Text = "";           
            foreach (Country country in Countries)
            {
                OwnerBox.Items.Add(country.Name);
                OccupantBox.Items.Add(country.Name);
                Label label = new Label();
                label.Text = country.Name;
                label.ForeColor = country.Color;                
            }

            switch (mode)
            {
                case 0:
                    if (pro == null)
                    {
                        foreach (Province pr in Provinces)
                        {
                            pr.ctrl.Text = "#";                            
                            switch (pr.Terrain)
                            {
                                case "grassland":
                                    pr.ctrl.BackColor = Color.Lime;
                                    break;
                                case "hills":
                                    pr.ctrl.BackColor = Color.Orange;
                                    break;
                                case "mountains":
                                    pr.ctrl.BackColor = Color.Brown;
                                    break;
                                case "desert":
                                    pr.ctrl.BackColor = Color.Gold;
                                    break;
                                case "forest":
                                    pr.ctrl.BackColor = Color.DarkGreen;
                                    break;
                                case "swamp":
                                    pr.ctrl.BackColor = Color.LightSeaGreen;
                                    break;
                                case "water":
                                    pr.ctrl.BackColor = Color.Blue;
                                    break;
                            }
                        }
                        PlayerCurrent.ctrl.Text = "*";
                    }
                    else
                    {
                        pro.ctrl.Text = "#";
                        PlayerCurrent.ctrl.Text = "*";
                        switch (pro.Terrain)
                        {
                            case "grassland":
                                pro.ctrl.BackColor = Color.Lime;
                                break;
                            case "hills":
                                pro.ctrl.BackColor = Color.Orange;
                                break;
                            case "mountains":
                                pro.ctrl.BackColor = Color.Brown;
                                break;
                            case "desert":
                                pro.ctrl.BackColor = Color.Gold;
                                break;
                            case "forest":
                                pro.ctrl.BackColor = Color.DarkGreen;
                                break;
                            case "swamp":
                                pro.ctrl.BackColor = Color.LightSeaGreen;
                                break;
                            case "water":
                                pro.ctrl.BackColor = Color.Blue;
                                break;
                        }
                    }
                    break;
                case 1:
                    if (pro == null)
                    {
                        foreach (Province pr in Provinces)
                        {
                            if (pr.Terrain == "water")
                                pr.ctrl.BackColor = Color.Blue;
                            else
                                pr.ctrl.BackColor = pr.Owner.Color;
                            pr.ctrl.Text = "#";
                        }
                        foreach(Country c in Countries)
                        {
                            c.Capital.ctrl.Text = c.Name;
                        }
                    }
                    else
                    {
                        if (pro.Terrain == "water")
                            pro.ctrl.BackColor = Color.Blue;
                        else
                            pro.ctrl.BackColor = pro.Owner.Color;

                    }
                    break;
            }
        }
        private void TerrainButton_Click(object sender, EventArgs e)
        {
            MapUpdate(0);
            mode = 0;
            BrushButton.Enabled = true;
        }

        private void BrushButton_Click(object sender, EventArgs e)
        {
            if (BrushFlag == false)
            {
                BrushButton.Text = "Brush on";
                BrushFlag = true;
            }
            else
            {
                BrushButton.Text = "Brush off";
                BrushFlag = false;
            }
        }

        private void CountriesListButton_Click(object sender, EventArgs e)
        {
            CountriesList CL = new CountriesList();
            CL.ShowDialog();
        }

        private void OccupiedBox_CheckedChanged(object sender, EventArgs e)
        {
            if (OccupiedCheckBox.Checked)
            {
                OccupantBox.Items.Clear();
                OccupantBox.Enabled = true;
                foreach(Country c in Countries)
                    OccupantBox.Items.Add(c.Name);
            }
            else
            {
                OccupantBox.SelectedText = "";
                OccupantBox.Enabled = false;          
                Clicked.Occ = null;
            }
        }
        private void OwnerBox_TextChanged(object sender, EventArgs e)
        {           
            if(OwnerBox.Text != "")
            Clicked.Owner = GetByName(OwnerBox.Text);
        }

        private void OccupantBox_SelectedTextChanged(object sender, EventArgs e)
        {
            if(OccupantBox.Text != "" && OccupantBox.Text != "Occupant" && OccupantBox.Text != "None")
                Clicked.Occ = GetByName(OccupantBox.Text);
        }

        private void BordersButton_Click(object sender, EventArgs e)
        {
            MapUpdate(1);
            BrushFlag = false;
            BrushButton.Text = "Brush off";
            BrushButton.Enabled = false;
            mode = 1;
        }

        private void DevelopmentBox_ValueChanged(object sender, EventArgs e)
        {
            Clicked.Development = (int)DevelopmentBox.Value;
        }

        private void FortificationsBox_ValueChanged(object sender, EventArgs e)
        {
            Clicked.Fortifications = (int)FortificationsBox.Value;
        }

        private List<Province> GetSurrounding(Province pr,string Border = "all")
        {
            int index = Provinces.IndexOf(pr);
            List<Province> ls = new List<Province>();
            switch (Border)
            {
                case "all":
                    ls.Add(Provinces[index - 24]);
                    ls.Add(Provinces[index - 11]);
                    ls.Add(Provinces[index + 13]);
                    ls.Add(Provinces[index + 24]);
                    ls.Add(Provinces[index + 12]);
                    ls.Add(Provinces[index - 12]);
                    break;
                case "n":
                    ls.Add(Provinces[index - 24]);
                    break;
                case "ne":
                    ls.Add(Provinces[index - 11]);
                    break;
                case "se":
                    ls.Add(Provinces[index + 13]);
                    break;
                case "s":
                    ls.Add(Provinces[index + 24]);
                    break;
                case "sw":
                    ls.Add(Provinces[index + 12]);
                    break;
                case "nw":
                    ls.Add(Provinces[index - 12]);
                    break;
            }
            return ls;
        }
    }
    public class Country
    {
        public List<Province> Land { get; set; }
        public string Name { get; set; }
        public string CurrentRuler { get; set; }
        public string AlternateRuler { get; set; }
        public List<string> PreviousRulers { get; set; }
        public int Stability { get; set; }
        public List<Country> WarEnemies { get; set; }
        public List<Country> Allies { get; set; }
        public string TypeOfGovernoment { get; set; }
        public Province Capital { get; set; }
        public Color Color { get; set; }
        public void NewRuler(string Name)
        {
            PreviousRulers.Add(CurrentRuler);
            CurrentRuler = Name;
        }

    }
    public class Province
    {
        public Label ctrl { get; }
        public int ID { get; }
        public string Terrain { get; set; }
        public string ProvinceName { get; set; }
        public int Fortifications { get; set; }
        public int Development { get; set; }
        public Country Owner { get; set; }
        public Country Occ { get; set; }
        public Dictionary<string, int> Production { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> Storage { get; set; } = new Dictionary<string, int>();
        public Province(Label Lb)
        {
            ctrl = Lb;
            
            ID = Form1.idstore;
            Form1.idstore++;
            ProvinceName = "None";
            Production.Add("wood", 0);
            Storage.Add("wood", 0);
            Production.Add("bricks", 0);
            Storage.Add("bricks", 0);
            Production.Add("wheat", 0);
            Storage.Add("wheat", 0);
            Production.Add("iron", 0);
            Storage.Add("iron", 0);
            Production.Add("tools", 0);
            Storage.Add("tools", 0);
            Production.Add("weapons", 0);
            Storage.Add("weapons", 0);
            Production.Add("wool", 0);
            Storage.Add("wool", 0);
            Production.Add("cloth", 0);
            Storage.Add("cloth", 0);
            Production.Add("furs", 0);
            Storage.Add("furs", 0);
            Production.Add("ale", 0);
            Storage.Add("ale", 0);
            Production.Add("wine", 0);
            Storage.Add("wine", 0);
            Production.Add("spieces", 0);
            Storage.Add("spieces", 0);
            Production.Add("meat", 0);
            Storage.Add("meat", 0);
            Production.Add("salt", 0);
            Storage.Add("salt", 0);
            Production.Add("hemps", 0);
            Storage.Add("hemps", 0);

        }
    }
}
/*Production list
 1: Wood
 3: Bricks
 4: Wheat
 5: Iron / Metals
 6: Tools
 7: Weapons
 8: Wool
 9: Cloth
 10: Furs
 11: Ale / Beer
 12: Wine
 13: Spices
 14: Meat 
 15: Salt
 16: Hemps
 */

