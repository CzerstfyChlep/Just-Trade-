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
            PlayerInventory.Clear();
            foreach (string s in ProductName)
            {
                PlayerInventory.Add(s, 0);
            }


            if (!Countries.Any())
            {
                Country Gaia = new Country();
                Gaia.Name = "None";
                Countries.Add(Gaia);
                Gaia.Color = Color.Gray;

            }
            if (!Provinces.Any())
            {
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
                    GetByName("None").Land.Add(prv);
                    prv.Occ = null;
                }
            }
            TerrainBox.TextChanged += new EventHandler(TerrainChange);
            IEnumerable<Province> query = Provinces.OrderBy(n => n.ctrl.Name);
            Provinces = query.ToList();
            PlayerCurrent = Provinces[150];
            Provinces[150].Terrain = "grassland";
            Tabs.SelectedIndexChanged += Tabs_TabIndexChanged;
            foreach (Province pr in GetSurrounding(Provinces[150]))
            {
                pr.Terrain = "grassland";
            }
            Prices.Add("wood", 50);
            Prices.Add("bricks", 75);
            Prices.Add("wheat", 40);
            Prices.Add("metals", 200);
            Prices.Add("tools", 350);
            Prices.Add("weapons", 500);
            Prices.Add("wool", 100);
            Prices.Add("cloth", 400);
            Prices.Add("furs", 300);
            Prices.Add("ale", 175);
            Prices.Add("wine", 300);
            Prices.Add("spices", 700);
            Prices.Add("salt", 150);
            Prices.Add("meat", 150);
            Prices.Add("hemps", 100);
        }

        private void Tabs_TabIndexChanged(object sender, EventArgs e)
        {
            if (Tabs.SelectedTab == PlayerPage)
            {
                InventroyLabel.Text = "Wood: " + PlayerInventory["wood"] + "\nBricks: " + PlayerInventory["bricks"] + "\nWheat: " + PlayerInventory["wheat"] + "\nMeat: " + PlayerInventory["meat"] + "\nMetals: " + PlayerInventory["metals"] + "\nTools: " + PlayerInventory["tools"] + "\nWeapons: " + PlayerInventory["weapons"] + "\nWool: " + PlayerInventory["wool"] + "\nCloth: " + PlayerInventory["cloth"] + "\nFurs: " + PlayerInventory["furs"] + "\nAle: " + PlayerInventory["ale"] + "\nWine: " + PlayerInventory["wine"] + "\nSpices: " + PlayerInventory["spices"] + "\nSalt: " + PlayerInventory["salt"] + "\nHemps: " + PlayerInventory["hemps"];
                #region woodUpdateBuySell                
                WoodStorageLabel.Text = "City storage: " + Clicked.Storage["wood"];
                WoodTrackBar.Maximum = Clicked.Development * 20;
                WoodTrackBar.Value = Clicked.Storage["wood"];
                #endregion
            }
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
        public Dictionary<string, int> PlayerInventory = new Dictionary<string, int>();
        public Dictionary<string, int> Prices = new Dictionary<string, int>();
        public static string[] ProductName = new string[] {"wood", "bricks", "wheat", "metals", "tools",
            "weapons","wool", "cloth", "furs", "ale", "wine", "spices", "salt","meat", "hemps"};
        public static int Gold;
        public static int idstore = 0;
        public static int MovePoints = 3;
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
            ProductionBox.Text = "Production in " + Clicked.ProvinceName;
            if (Clicked.Production["wood"] >= 0)
                WoodLabel.Text = "Wood: " + Clicked.Storage["wood"] + "/+" + Clicked.Production["wood"];
            else
                WoodLabel.Text = "Wood: " + Clicked.Storage["wood"] + "/" + Clicked.Production["wood"];

            if (Clicked.Production["bricks"] >= 0)
                BricksLabel.Text = "Bricks: " + Clicked.Storage["bricks"] + "/+" + Clicked.Production["bricks"];
            else
                BricksLabel.Text = "Bricks: " + Clicked.Storage["bricks"] + "/" + Clicked.Production["bricks"];

            if (Clicked.Production["wheat"] >= 0)
                WheatLabel.Text = "Wheat: " + Clicked.Storage["wheat"] + "/+" + Clicked.Production["wheat"];
            else
                WheatLabel.Text = "Wheat: " + Clicked.Storage["wheat"] + "/" + Clicked.Production["wheat"];

            if (Clicked.Production["metals"] >= 0)
                MetalsLabel.Text = "Metals: " + Clicked.Storage["metals"] + "/+" + Clicked.Production["metals"];
            else
                MetalsLabel.Text = "Metals: " + Clicked.Storage["metals"] + "/" + Clicked.Production["metals"];

            if (Clicked.Production["tools"] >= 0)
                ToolsLabel.Text = "Tools: " + Clicked.Storage["tools"] + "/+" + Clicked.Production["tools"];
            else
                ToolsLabel.Text = "Tools: " + Clicked.Storage["tools"] + "/" + Clicked.Production["tools"];

            if (Clicked.Production["weapons"] >= 0)
                WeaponsLabel.Text = "Weapons: " + Clicked.Storage["weapons"] + "/+" + Clicked.Production["weapons"];
            else
                WeaponsLabel.Text = "Weapons: " + Clicked.Storage["weapons"] + "/" + Clicked.Production["weapons"];

            if (Clicked.Production["wool"] >= 0)
                WoolLabel.Text = "Wool: " + Clicked.Storage["wool"] + "/+" + Clicked.Production["wool"];
            else
                WoolLabel.Text = "Wool: " + Clicked.Storage["Wool"] + "/" + Clicked.Production["wool"];

            if (Clicked.Production["cloth"] >= 0)
                ClothLabel.Text = "Cloth: " + Clicked.Storage["cloth"] + "/+" + Clicked.Production["cloth"];
            else
                ClothLabel.Text = "Cloth: " + Clicked.Storage["cloth"] + "/" + Clicked.Production["cloth"];

            if (Clicked.Production["furs"] >= 0)
                FursLabel.Text = "Furs: " + Clicked.Storage["furs"] + "/+" + Clicked.Production["furs"];
            else
                FursLabel.Text = "Furs: " + Clicked.Storage["furs"] + "/" + Clicked.Production["furs"];

            if (Clicked.Production["ale"] >= 0)
                BeerLabel.Text = "Ale: " + Clicked.Storage["ale"] + "/+" + Clicked.Production["ale"];
            else
                BeerLabel.Text = "Ale: " + Clicked.Storage["ale"] + "/" + Clicked.Production["ale"];

            if (Clicked.Production["wine"] >= 0)
                WineLabel.Text = "Wine: " + Clicked.Storage["wine"] + "/+" + Clicked.Production["wine"];
            else
                WineLabel.Text = "Wine: " + Clicked.Storage["wine"] + "/" + Clicked.Production["wine"];

            if (Clicked.Production["spices"] >= 0)
                SpicesLabel.Text = "Spices: " + Clicked.Storage["spices"] + "/+" + Clicked.Production["spices"];
            else
                SpicesLabel.Text = "Spices: " + Clicked.Storage["spices"] + "/" + Clicked.Production["spices"];

            if (Clicked.Production["salt"] >= 0)
                SaltLabel.Text = "Salt: " + Clicked.Storage["salt"] + "/+" + Clicked.Production["salt"];
            else
                SaltLabel.Text = "Salt: " + Clicked.Storage["salt"] + "/" + Clicked.Production["salt"];

            if (Clicked.Production["hemps"] >= 0)
                HempsLabel.Text = "Hemps: " + Clicked.Storage["hemps"] + "/+" + Clicked.Production["hemps"];
            else
                HempsLabel.Text = "Hemps: " + Clicked.Storage["hemps"] + "/" + Clicked.Production["hemps"];
            if (Clicked.Production["meat"] >= 0)
                MeatLabel.Text = "Meat: " + Clicked.Storage["meat"] + "/+" + Clicked.Production["meat"];
            else
                MeatLabel.Text = "Meat: " + Clicked.Storage["meat"] + "/" + Clicked.Production["meat"];

        }
        private void ProvinceClick(object sender, EventArgs e)
        {
            PrevClicked = Clicked;
            MapUpdate(mode, PrevClicked);
            Current = (Label)sender;
            Clicked = GetByObject(Current);

            if (Clicked.Owner.Name == "None")
                CapitalBox.Enabled = false;
            else
                CapitalBox.Enabled = true;
            if (Clicked.Owner.Capital == Clicked)
                CapitalBox.Checked = true;
            else
                CapitalBox.Checked = false;
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
                UpdateProductionStorage();
                if (GetSurrounding(PlayerCurrent).Contains(Clicked) && Clicked.Terrain != "water" && MovePoints > 0)
                {
                    MoveButton.Enabled = true;
                }
                else
                {
                    MoveButton.Enabled = false;
                }
            }
            else
            {
                Clicked.Terrain = BrushTerrain.Text;
                MapUpdate(0, Clicked);
            }


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
            IEnumerable<Country> query = from n in Countries
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
            OwnerBox.TextChanged -= OwnerBox_TextChanged;
            TerrainBox.TextChanged -= TerrainChange;
            DevelopmentBox.ValueChanged -= DevelopmentBox_ValueChanged;
            FortificationsBox.ValueChanged -= FortificationsBox_ValueChanged;
            CapitalBox.CheckedChanged -= CapitalBox_CheckedChanged;
            CapitalBox.Enabled = true;
            CapitalBox.Checked = false;
            CapitalBox.Enabled = false;
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
                            if(pr.ProvinceName != "None")
                            {
                                pr.ctrl.Text = pr.ProvinceName;
                            }
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
                        if(!PlayerCurrent.ctrl.Text.Contains("#"))
                            PlayerCurrent.ctrl.Text += "\n*";
                        else
                        {
                            PlayerCurrent.ctrl.Text = "*";
                        }
                    }
                    else
                    {
                        pro.ctrl.Text = "#";
                        if (pro.ProvinceName != "None")
                        {
                            pro.ctrl.Text = pro.ProvinceName;
                        }
                        if (PlayerCurrent.ctrl.Text.Contains("#"))                        
                            PlayerCurrent.ctrl.Text = "*";
                        
                        else if (!PlayerCurrent.ctrl.Text.Contains("*"))
                            PlayerCurrent.ctrl.Text += "\n*";

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
                            {
                                pr.ctrl.Text = "#";
                                pr.ctrl.BackColor = pr.Owner.Color;
                                if (pr.Occ != null)
                                {
                                    pr.ctrl.Text = "@";
                                    pr.ctrl.ForeColor = pr.Occ.Color;
                                }
                            }
                        }
                        foreach (Country c in Countries)
                        {
                            try
                            {
                                if (c.Name != "None" && c.Land.Count != 0)
                                    c.Capital.ctrl.Text = c.Name;
                            }
                            catch
                            {
                                MessageBox.Show("Country of " + c.Name + " doesn't have a capital!");
                            }
                        }
                    }
                    else
                    {
                        if (pro.Terrain == "water")
                            pro.ctrl.BackColor = Color.Blue;
                        else
                        {
                            pro.ctrl.Text = "#";
                            pro.ctrl.BackColor = pro.Owner.Color;
                            if (pro.Occ != null)
                            {
                                pro.ctrl.Text = "@";
                                pro.ctrl.ForeColor = pro.Occ.Color;
                            }
                        }

                    }
                    break;
            }
            MovePointsLabel.Text = "Move Points: " + MovePoints;
            OwnerBox.TextChanged += OwnerBox_TextChanged;
            TerrainBox.TextChanged += TerrainChange;
            DevelopmentBox.ValueChanged += DevelopmentBox_ValueChanged;
            FortificationsBox.ValueChanged += FortificationsBox_ValueChanged;
            CapitalBox.CheckedChanged += CapitalBox_CheckedChanged;
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
                foreach (Country c in Countries)
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
            if (Clicked.Owner.Land.Count == 0)
                Clicked.Owner.Capital = Clicked;
            if (OwnerBox.Text != "")
            {
                Clicked.Owner = GetByName(OwnerBox.Text);
                GetByName(OwnerBox.Text).Land.Add(Clicked);
            }

        }

        private void OccupantBox_SelectedTextChanged(object sender, EventArgs e)
        {
            if (OccupantBox.Text != "" && OccupantBox.Text != "Occupant" && OccupantBox.Text != "None")
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

        private List<Province> GetSurrounding(Province pr, string Border = "all")
        {
            int index = Provinces.IndexOf(pr);
            List<Province> ls = new List<Province>();
            switch (Border)
            {
                case "all":
                    ls.Add(GetByObject(GetByLocation(pr.ctrl.Location.X, pr.ctrl.Location.Y - 50)));
                    ls.Add(GetByObject(GetByLocation(pr.ctrl.Location.X + 38, pr.ctrl.Location.Y - 25)));
                    ls.Add(GetByObject(GetByLocation(pr.ctrl.Location.X + 38, pr.ctrl.Location.Y + 25)));
                    ls.Add(GetByObject(GetByLocation(pr.ctrl.Location.X, pr.ctrl.Location.Y + 50)));
                    ls.Add(GetByObject(GetByLocation(pr.ctrl.Location.X - 38, pr.ctrl.Location.Y + 25)));
                    ls.Add(GetByObject(GetByLocation(pr.ctrl.Location.X - 38, pr.ctrl.Location.Y - 25)));
                    break;
                case "n":
                    ls.Add(GetByObject(GetByLocation(pr.ctrl.Location.X, pr.ctrl.Location.Y - 50)));
                    break;
                case "ne":
                    ls.Add(GetByObject(GetByLocation(pr.ctrl.Location.X + 38, pr.ctrl.Location.Y - 25)));
                    break;
                case "se":
                    ls.Add(GetByObject(GetByLocation(pr.ctrl.Location.X + 38, pr.ctrl.Location.Y + 25)));
                    break;
                case "s":
                    ls.Add(GetByObject(GetByLocation(pr.ctrl.Location.X, pr.ctrl.Location.Y + 50)));
                    break;
                case "sw":
                    ls.Add(GetByObject(GetByLocation(pr.ctrl.Location.X - 38, pr.ctrl.Location.Y + 25)));
                    break;
                case "sn":
                    ls.Add(GetByObject(GetByLocation(pr.ctrl.Location.X - 38, pr.ctrl.Location.Y - 25)));
                    break;
            }
            return ls;
        }
        private Label GetByLocation(int x, int y)
        {
            List<Label> lb = new List<Label>();
            foreach (Label lab in Map.Controls)
            {
                lb.Add(lab);
            }
            IEnumerable<Label> query = from n in lb
                                       where n.Location.X == x
                                       where n.Location.Y == y
                                       select n;
            return query.ElementAt(0);
        }
        private void CapitalBox_CheckedChanged(object sender, EventArgs e)
        {
            Clicked.Owner.Capital = Clicked;
            CapitalBox.Enabled = false;
        }

        private void MoveButton_Click(object sender, EventArgs e)
        {
            PlayerCurrent = Clicked;
            MovePoints -= 1;
            MapUpdate(mode);
            MoveButton.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                /*
                if (Clicked.Development >= 5)
                    Clicked.CreateCity();
                else
                    MessageBox.Show("Cities need at least 5 development!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateProductionStorage();
                */
                CreateCity cc = new CreateCity(Clicked);
                cc.ShowDialog();
                UpdateProductionStorage();
                
            }
            catch
            {
                MessageBox.Show("No province has been selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
            
        }

       
    }
    public class Country
        {
            public List<Province> Land { get; set; } = new List<Province>();
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
    public class Human
    {
        public double age { get; set; }
        public string Job { get; set; }
        public int hunger { get; set; }
        public Human()
        {
            age = 0;
            hunger = 0;
            Job = "none";
        }
        public void nextTurn()
        {
            age += 0.25;
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
            public double Hungry { get; set; }
            public bool WarMode { get; set; }
            public int Treseoury { get; set; }
            public List<Human> Population { get; set; } = new List<Human> { };
            public Dictionary<string, int> Shortage { get; set; } = new Dictionary<string, int>();
            public Dictionary<string, int> Buildings { get; set; } = new Dictionary<string, int>();
            public Country Occ { get; set; }
            public Dictionary<string, int> Production { get; set; } = new Dictionary<string, int>();
            public Dictionary<string, int> Storage { get; set; } = new Dictionary<string, int>();
            public Dictionary<string, int> BProduction { get; set; } = new Dictionary<string, int>();
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
                Production.Add("metals", 0);
                Storage.Add("metals", 0);
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
                Production.Add("spices", 0);
                Storage.Add("spices", 0);
                Production.Add("meat", 0);
                Storage.Add("meat", 0);
                Production.Add("salt", 0);
                Storage.Add("salt", 0);
                Production.Add("hemps", 0);
                Storage.Add("hemps", 0);
                BProduction = Production;
                Buildings = Production;
                Shortage = Production;
                WarMode = false;

            }
            public void CreateCity()
            {
                Random rand = new Random();
                foreach (string s in Form1.ProductName)
                {
                    if (s != "tools" && s != "weapons" && s != "cloth" && s != "ale")
                        BProduction[s] = rand.Next(0, 21);

                }
                for (int a = 0; a < 5; a++)
                {
                    start:
                    int random = rand.Next(0, Form1.ProductName.Length);
                    if (Form1.ProductName[random] != "tools" && Form1.ProductName[random] != "weapons" && Form1.ProductName[random] != "cloth" && Form1.ProductName[random] != "ale")
                        BProduction[Form1.ProductName[random]] += 5;
                    else
                        goto start;

                }
            }
            public void CityTurn()
            {
                if (Development > 5)
                {
                    foreach (string key in Buildings.Keys)
                    {
                        
                            switch (key)
                            {
                                case "tools":
                                    if (WarMode == false)
                                    {
                                        int MetalsNeeded = Buildings["tools"] * 5;
                                        if (Math.Floor(Storage["metals"] * 0.75) >= MetalsNeeded)
                                        {
                                            Storage["metals"] -= MetalsNeeded;
                                            Storage["tools"] += Buildings["tools"];
                                            Production["tools"] = Buildings["tools"];
                                        }
                                        else if (Math.Floor(Storage["metals"] * 0.75) <= MetalsNeeded)
                                        {
                                            int number = (int)Math.Floor((double)Math.Floor(Storage["metals"] * 0.75) / 5);
                                            Storage["metals"] -= 5 * number;
                                            Storage["tools"] += number;
                                            Production["tools"] = number;
                                        }
                                    }
                                    else
                                    {
                                        int MetalsNeeded = Buildings["tools"] * 5;
                                        if (Math.Floor(Storage["metals"] * 0.25) >= MetalsNeeded)
                                        {
                                            Storage["metals"] -= MetalsNeeded;
                                            Storage["tools"] += Buildings["tools"];
                                            Production["tools"] = Buildings["tools"];
                                        }
                                        else if (Math.Floor(Storage["metals"] * 0.25) <= MetalsNeeded)
                                        {
                                            int number = (int)Math.Floor((double)Math.Floor(Storage["metals"] * 0.25) / 5);
                                            Storage["metals"] -= 5 * number;
                                            Storage["tools"] += number;
                                            Production["tools"] = number;
                                        }
                                    }
                                    break;
                                case "weapons":
                                    if (WarMode == true)
                                    {
                                        int MetalsNeeded = Buildings["weapons"] * 5;
                                        if (Math.Floor(Storage["metals"] * 0.25) >= MetalsNeeded)
                                        {
                                            Storage["metals"] -= MetalsNeeded;
                                            Storage["weapons"] += Buildings["weapons"];
                                            Production["weapons"] = Buildings["weapons"];
                                        }
                                        else if (Math.Floor(Storage["metals"] * 0.25) <= MetalsNeeded)
                                        {
                                            int number = (int)Math.Floor((double)Math.Floor(Storage["metals"] * 0.25) / 5);
                                            Storage["metals"] -= 5 * number;
                                            Storage["weapons"] += number;
                                            Production["weapons"] = number;
                                        }
                                    }
                                    else
                                    {
                                        int MetalsNeeded = Buildings["weapons"] * 5;
                                        if (Math.Floor(Storage["metals"] * 0.75) >= MetalsNeeded)
                                        {
                                            Storage["metals"] -= MetalsNeeded;
                                            Storage["weapons"] += Buildings["weapons"];
                                            Production["weapons"] = Buildings["weapons"];
                                        }
                                        else if (Math.Floor(Storage["metals"] * 0.75) <= MetalsNeeded)
                                        {
                                            int number = (int)Math.Floor((double)Math.Floor(Storage["metals"] * 0.75) / 5);
                                            Storage["metals"] -= 5 * number;
                                            Storage["weapons"] += number;
                                            Production["weapons"] = number;
                                        }

                                    }
                                    break;
                                case "cloth":
                                    int WoolNeeded = Buildings["cloth"] * 10;
                                    if (Storage["wool"] >= WoolNeeded)
                                    {
                                        Storage["wool"] -= WoolNeeded;
                                        Storage["cloth"] += Buildings["cloth"];
                                        Production["cloth"] = Buildings["cloth"];
                                    }
                                    else if (Storage["wool"] <= WoolNeeded)
                                    {
                                        int number = (int)Math.Floor((double)Storage["wool"] / 10);
                                        Storage["wool"] -= 10 * number;
                                        Storage["cloth"] += number;
                                        Production["cloth"] = number;
                                    }
                                    break;
                                case "ale":
                                    goto Food;
                                    int WheatNeeded = Buildings["ale"] * 15;
                                    if (Storage["wheat"] >= WheatNeeded)
                                    {
                                        Storage["wheat"] -= WheatNeeded;
                                        Storage["ale"] += Buildings["ale"];
                                        Production["ale"] = Buildings["ale"];
                                    }
                                    else if (Storage["wheat"] <= WheatNeeded)
                                    {
                                        int number = (int)Math.Floor((double)Storage["wheat"] / 15);
                                        Storage["wheat"] -= 15 * number;
                                        Storage["ale"] += number;
                                        Production["ale"] = number;
                                    }
                                    break;
                                default:
                                    Production[key] = BProduction[key] + (int)Math.Floor(BProduction[key] * (Buildings[key] * 0.1));
                                    break;
                            
                        }
                    }
                    Production["wood"] -= (int)Math.Round(Population.Count * 0.005);
                    Production["bricks"] -= (int)Math.Round(Population.Count * 0.004);
                    Production["tools"] -= (int)Math.Round(Population.Count * 0.001);
                    Production["cloth"] -= (int)Math.Round(Population.Count * 0.001);
                    Production["furs"] -= (int)Math.Round(Population.Count * 0.003);
                    Production["ale"] -= (int)Math.Round(Population.Count * 0.002);
                    Production["wine"] -= (int)Math.Round(Population.Count * 0.001);
                    Production["spices"] -= (int)Math.Round(Population.Count * 0.001);
                    Production["salt"] -= (int)Math.Round(Population.Count * 0.003);
                    Production["hemps"] -= (int)Math.Round(Population.Count * 0.005);
                    Production["metals"] -= Production["tools"] * 5 + Production["weapons"] * 5;
                    Production["wool"] -= Production["cloth"] * 10;
                    Production["wheat"] -= Production["ale"] * 15;
                    foreach (string s in Form1.ProductName)
                    {
                        Storage[s] += Production[s];
                        if (Storage[s] < 0)
                        {
                            Shortage[s] -= (int)Math.Ceiling((double)Storage[s] / 2);
                            Storage[s] = 0;
                        }
                    }
                    #region Buildings

                    #endregion
                    #region Food
                    Food:
                    int FoodNeeded = Development * 5;
                    int MeatFood = Storage["meat"] * 2;
                    int WheatFood = Storage["wheat"];
                    if (FoodNeeded <= MeatFood)
                    {
                        FoodNeeded = 0;
                        MeatFood -= FoodNeeded;
                        Storage["meat"] = (int)Math.Floor((double)MeatFood / 2);
                    }
                
                    else if (FoodNeeded > MeatFood && FoodNeeded <= (MeatFood + WheatFood))
                    {
                        FoodNeeded -= MeatFood;
                        Storage["meat"] = 0;
                        WheatFood -= FoodNeeded;
                        Storage["wheat"] = WheatFood;

                    }
                    else if (FoodNeeded > (MeatFood + WheatFood))
                    {
                        int AF = FoodNeeded;
                        FoodNeeded -= MeatFood + WheatFood;
                        Storage["meat"] = 0;
                        Storage["wheat"] = 0;
                        Hungry = FoodNeeded / AF;
                        if (Hungry > 0.33)
                        {
                            Development -= 1;
                        }
                    }
                    #endregion


                }
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

