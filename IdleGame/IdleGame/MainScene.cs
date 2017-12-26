using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace IdleGame
{
    class MainScene : Scene
    {
        public Dictionary<BonusType, double> Bonuses = new Dictionary<BonusType, double>();
        public Image background = new Image("Assets/Img/background_road2.png");
        public MyPlayer player;
        public List<UnitSkill> skillList = new List<UnitSkill>();
        public List<Unit> unitsList = new List<Unit>();
        public List<Gear> gearList = new List<Gear>();
        public Random random = new Random();
        public Stage stage;
        int currentStage = 0;
        public Image HPBG = new Image("Assets/Img/HPBarBG.png");
        public Image HPFG = new Image("Assets/Img/HPBarFG.png");
        public Entity stagee;
        public Text staget;
        public Entity stageNumbere;
        public Text stageNumbert;
        public double totaldeeps = 0;
        public PlayerGui menu;
        public int gearOwned = 0;
        Entity debuge;
        Text debugt;

        Image Crosshair = new Image("Assets/Img/crosshair.png");
        Entity Crosshair_e = new Entity(0, 0);

        public double totalDPS = 0;
        public MainScene() : base()
        {
            
        }
        public override void Render()
        {
            base.Render();
        }

        public override void Begin()
        {
            base.Begin();
            player = new MyPlayer(1000, 600);
            Add(player);
            AddGraphic(background);
            foreach (BonusType bonusType in Enum.GetValues(typeof(BonusType)))
            {
                Bonuses.Add(bonusType, 0);
            }
            CreateGear();
            CreateSkills();
            CreateUnits();
            UpdateBonuses();
            StartStage();
            HUD();
            unitsList[0].level = 0;
            Vector2 Pos = new Vector2(47, 854);
            int counter = 1;
            foreach(var Unit in unitsList)
            {
                new GuiElement(Pos.X, Pos.Y, Unit);
                Pos.X += 82;
                if (counter == 11 || counter == 22)
                {
                    Pos.Y += 70;
                    Pos.X = 47;
                }
                counter++;
            }
            player.UpgradePlayer();
            menu = new PlayerGui(955, 1080 - 261, "Assets/Img/bottom_menu.png", player)
            {
                Layer = 1000
            };
            new Dicokka(1500, 500);
            new BiggestTonk(1500, 600);
            new FatTonk(1500, 700);

        }
        public override void Update()
        {
            Attack();
            HUD();
            base.Update();
        }

        public void CreateTextEntity(ref Entity e, ref Text t, int x, int y, int size)
        {
            e = new Entity(x, y);
            Add(e);
            t = new Text("", "Assets/Fonts/Reality Hyper Regular.ttf", size);
            e.AddGraphic(t);
            //t.CenterOrigin();
            FormatText(t);
        }

        public void HUD()
        {
            if (stageNumbere == null)
            {
                CreateTextEntity(ref stagee, ref staget, 590, 19, 30);
                CreateTextEntity(ref stageNumbere, ref stageNumbert, 1920 / 2, 28, 30);
                CreateTextEntity(ref debuge, ref debugt, 20, 20, 30);
                AddGraphic(HPBG, (1920 - 800) / 2, 2);
                AddGraphic(HPFG, (1920 - 800) / 2, 2);
                //AddGraphic(Crosshair, -100, -100);
                Crosshair.Scale = 0.1f;
                Crosshair.CenterOrigin();
                Crosshair_e.Layer = -1000;
                HPBG.Scale = 1f;
                HPFG.Scale = 1f;
                Crosshair_e.AddGraphic(Crosshair);
                Add(Crosshair_e);
            }
            else
            {
                stageNumbere.Graphic.CenterOrigin();
                stageNumbert.String = stage.stage + "";
                debugt.String = "X: " + Input.MouseX + "\nY: " + Input.MouseY;
                staget.String = "Stage";
                HPFG.ClippingRegion = new Rectangle(0, 0, (int)Math.Ceiling((HPFG.Width * (stage.CurrentHP / stage.MaxHP))), HPFG.Height);
                Crosshair_e.SetPosition(Input.MouseX, Input.MouseY);
            }
        }
        public void FormatText(Text Text)
        {
            Text.OutlineThickness = 2;
            Text.TextStyle = TextStyle.Bold;
            Text.SetPosition(0, 0);
            Text.Color = Color.Yellow;
            Text.OutlineColor = Color.Black;
        }

        public double GetHeroAdditionlDamage(int ID)
        {
            double num = 0;
            foreach (var skill in this.skillList)
            {
                if (skill.isUnlocked && skill.ownerID == ID)
                    num += skill.magnitude;
            }
            return num;
        }
        public double GetBonusArtifactDamage()
        {
            double num = 0;
            foreach (var gear in this.gearList)
            {
                if (gear.unlocked)
                    num += gear.GetDamageBonus();
            }
            return num;
        }
        public double GetBonusTypeMagnitude(BonusType bonusTypee)
        {
            double num = 0f;
            foreach (var item in this.gearList)
            {
                if (item.unlocked)
                {
                    if (item.GetbonusType() == bonusTypee)
                    {
                        num += item.GetBonusMagnitude();
                    }
                }
            }
            double num2 = 0f;
            foreach (var skill in skillList)
            {
                if (skill.isUnlocked)
                {
                    if (skill.bonusType == bonusTypee)
                    {
                        num2 += skill.magnitude;
                    }
                }
            }
            return num + num2;
        }

        public void UpdateBonuses()
        {
            foreach (var bonus in Bonuses.Keys.ToArray())
            {
                Bonuses[bonus] = GetBonusTypeMagnitude(bonus);
            }
        }

        public void StartStage()
        {
            this.currentStage++;
            this.stage = new Stage(currentStage, Bonuses, random);
        }

        public void Attack()
        {
            //this.stage.CurrentHP -= (this.GetTotalDps() / timesPerSecond);
            totalDPS = 0;
            foreach (var Unit in unitsList)
            {
                this.stage.CurrentHP -= Unit.GetDPSByLevel(Unit.level)/60;
                totalDPS += Unit.GetDPSByLevel(Unit.level);
            }
            if (Input.MouseButtonDown(MouseButton.Left) && Input.MouseY < 1080 - 261)
            {
                //Console.WriteLine(player.GetPlayerAttackDamageByLevel(player.level, Bonuses[BonusType.AllDamage], Bonuses[BonusType.PlayerDamage], Bonuses[BonusType.PlayerDamageDPS], totaldeeps, GetBonusArtifactDamage(), Bonuses[BonusType.CriticalChance], Bonuses[BonusType.CriticalDamage], random)/60);
                double hit = player.GetPlayerAttackDamageByLevel(player.level) * 15 / 60;
                this.stage.CurrentHP -= hit;
                //totalDPS += player.GetPlayerAttackDamageByLevel(player.level, Bonuses[BonusType.AllDamage], Bonuses[BonusType.PlayerDamage], Bonuses[BonusType.PlayerDamageDPS], totaldeeps, GetBonusArtifactDamage(), Bonuses[BonusType.CriticalChance], Bonuses[BonusType.CriticalDamage], random) * 15;
                Console.WriteLine(hit);

            }
            //totalDPS += totaldeeps;
            //totalDPS
            if (this.stage.CurrentHP <= 0)
            {
                if (this.stage.Wave < 10)
                {
                    this.stage.CurrentHP = stage.MaxHP;
                    this.stage.Wave++;
                    player.gold += this.stage.Prize;
                }
                else
                {
                    player.gold += this.stage.Prize;
                    StartStage();
                }
            }
        }

        void CreateGear()
        {
            this.gearList.Add(new Gear("Amulet of the Valrunes", 0, BonusType.MonsterGold, 0.1f, 0.5f, 0.25f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Axe of Resolution", 0, BonusType.BerserkerRageDuration, 0.1f, 0.7f, 0.35f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Barbarian's Mettle", 10, BonusType.BerserkerRageCooldown, 0.05f, 0.7f, 0.35f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Brew of Absorbtion", 0, BonusType.AllDamage, 0.02f, 0.9f, 0.9f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Chest of Contentment", 0, BonusType.ChestGold, 0.2f, 0.4f, 0.2f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Crafter's Elixir", 0, BonusType.AllGold, 0.15f, 0.4f, 0.2f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Crown Egg", 0, BonusType.ChestChance, 0.2f, 0.4f, 0.2f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Death Seeker", 25, BonusType.CriticalChance, 0.02f, 0.3f, 0.15f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Divine Chalice", 0, BonusType.ChanceFor10xGold, 0.005f, 0.3f, 0.15f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Drunken Hammer", 0, BonusType.PlayerDamage, 0.04f, 0.6f, 0.30f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Hero's Thrust", 0, BonusType.CriticalDamage, 0.2f, 0.3f, 0.15f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Hunter's Ointment", 10, BonusType.WarCryCooldown, 0.05f, 1.2f, 0.6f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Laborer's Pendant", 10, BonusType.HandOfMidasCooldown, 0.05f, 0.7f, 0.35f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Ogre's Gauntlet", 0, BonusType.ShadowCloneDuration, 0.1f, 0.7f, 0.35f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Overseer's Lotion", 10, BonusType.ShadowCloneCooldown, 0.05f, 0.7f, 0.35f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Parchment of Importance", 0, BonusType.CriticalStrikeDuration, 0.1f, 0.7f, 0.35f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Ring of Opulence", 0, BonusType.HandOfMidasDuration, 0.1f, 0.7f, 0.35f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Ring of Wonderous Charm", 25, BonusType.UpgradeCost, 0.02f, 0.3f, 0.15f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Sacred Scroll", 10, BonusType.CriticalStrikeCooldown, 0.05f, 0.7f, 0.35f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Saintly Shield", 10, BonusType.HeavenlyStrikeCooldown, 0.05f, 0.7f, 0.35f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Tincture of the Maker", 0, BonusType.AllDamage, 0.05f, 0.1f, 0.05f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Undead Aura", 0, BonusType.BonusRelic, 0.05f, 0.3f, 0.15f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Universal Fissure", 0, BonusType.WarCryDuration, 0.1f, 1.2f, 0.6f, "Assets/Img/Gear/icon_chest.png"));
        }

        void CreateUnits()
        {
            unitsList.Add(new Unit(1, "Cannon Fodder", 50, "Assets/Img/Gui/icon_private.png"));
            unitsList.Add(new Unit(2, "Master Sergeant Shooter Person", 175, "Assets/Img/Gui/icon_marksman.png"));
            unitsList.Add(new Unit(3, "Richard Jordan Gatling", 675, "Assets/Img/Gui/icon_minigun.png"));
            unitsList.Add(new Unit(4, "Captain James Hook", 2850, "Assets/Img/Gui/icon_mortar.png"));
            unitsList.Add(new Unit(5, "Captain Ethan Obvious", 13300, "Assets/Img/Gui/icon_turret.png"));
            unitsList.Add(new Unit(6, "Small Tonk", 68100, "Assets/Img/Gui/icon_dicokka.png"));
            unitsList.Add(new Unit(7, "Fat Tonk", 384000, "Assets/Img/Gui/icon_ACP.png"));
            unitsList.Add(new Unit(8, "Big Tonk", 2800000, "Assets/Img/Gui/icon_ironiso.png"));
            unitsList.Add(new Unit(9, "Biggest Tonk", 23800000, "Assets/Img/Gui/icon_biggest_tank.png"));
            unitsList.Add(new Unit(10, "Sergeant Sergeant Master Sergeant", 143000000, "Assets/Img/Gui/icon_hover.png"));
            unitsList.Add(new Unit(11, "Senior Airman Wi Tu Lo", 943000000, "Assets/Img/Gui/icon_heli.png"));
            unitsList.Add(new Unit(12, "Team Rocket", 84E+09, "Assets/Img/Gui/icon_rocket.png"));
            unitsList.Add(new Unit(13, "Bigger Guns!", 5.47E+10, "Assets/Img/Gui/icon_infantry_anti-tank.png"));
            unitsList.Add(new Unit(14, "Better Stuff!", 8.20E+11, "Assets/Img/Gui/icon_infantry_eq.png"));
            unitsList.Add(new Unit(15, "Everything penetrating bullets!", 8.20E+12, "Assets/Img/Gui/icon_ap_ammo.png"));
            unitsList.Add(new Unit(16, "Drugs!!", 1.64E+14, "Assets/Img/Gui/icon_field_hospitals.png"));
            unitsList.Add(new Unit(17, "Crates of bullets!", 1.64E+15, "Assets/Img/Gui/icon_ammo_supply.png"));
            unitsList.Add(new Unit(18, "Biker Gangs", 4.92E+16, "Assets/Img/Gui/icon_motorized_infantry.png"));
            unitsList.Add(new Unit(19, "Bigger Cannons!", 2.46E+18, "Assets/Img/Gui/icon_tank_armament.png"));
            unitsList.Add(new Unit(20, "Crates of Stuff!", 7.38E+19, "Assets/Img/Gui/icon_supply_drops.png"));
            unitsList.Add(new Unit(21, "Rockets!", 2.44E+21, "Assets/Img/Gui/icon_rockets.png"));
            unitsList.Add(new Unit(22, "Even Bigger Cannons!!", 2.44E+23, "Assets/Img/Gui/icon_artillery_support.png"));
            unitsList.Add(new Unit(23, "Huge Crates of Stuff!!", 4.87E+25, "Assets/Img/Gui/icon_bigger_crate.png"));
            unitsList.Add(new Unit(24, "Bigger Rockets!!", 1.95E+28, "Assets/Img/Gui/icon_bigger_rockets.png"));
            unitsList.Add(new Unit(25, "Biggest Cannons!!!", 2.14E+31, "Assets/Img/Gui/icon_biggest_guns.png"));
            unitsList.Add(new Unit(26, "Truckloads of Stuff!!!", 2.36E+36, "Assets/Img/Gui/icon_supply_lines.png"));
            unitsList.Add(new Unit(27, "Biggest Rockets!!!", 2.59E+46, "Assets/Img/Gui/icon_rocket_guidance_system.png"));
            unitsList.Add(new Unit(28, "Killstreaks!", 2.85E+61, "Assets/Img/Gui/icon_paratroopers.png"));
            unitsList.Add(new Unit(29, "Moar Barrels!", 3.14E+81, "Assets/Img/Gui/icon_gatling.png"));
            unitsList.Add(new Unit(30, "Moar Cannons!!", 3.14E+96, "Assets/Img/Gui/icon_more_guns.png"));
            unitsList.Add(new Unit(31, "Fat Man!", 3.76E+101, "Assets/Img/Gui/icon_bombing.png"));
            unitsList.Add(new Unit(32, "Space Lasers!!", 4.14E+136, "Assets/Img/Gui/icon_gps.png"));
            unitsList.Add(new Unit(33, "MEEEEEEE BIL", 4.56E+141, "Assets/Img/Gui/icon_bil.png"));
        }

        void CreateSkills()
        {
            skillList.Add(new UnitSkill(1, BonusType.HeroDamage, 1.0f, 10));
            skillList.Add(new UnitSkill(1, BonusType.HeroDamage, 2.0f, 25));
            skillList.Add(new UnitSkill(1, BonusType.AllDamage, 0.2f, 50));
            skillList.Add(new UnitSkill(1, BonusType.CriticalDamage, 0.2f, 100));
            skillList.Add(new UnitSkill(1, BonusType.HeroDamage, 20.0f, 200));
            skillList.Add(new UnitSkill(1, BonusType.AllDamage, 0.5f, 400));
            skillList.Add(new UnitSkill(1, BonusType.HeroDamage, 200.0f, 800));

            skillList.Add(new UnitSkill(2, BonusType.PlayerDamage, 0.1f, 10));
            skillList.Add(new UnitSkill(2, BonusType.HeroDamage, 2.0f, 25));
            skillList.Add(new UnitSkill(2, BonusType.HeroDamage, 20.0f, 50));
            skillList.Add(new UnitSkill(2, BonusType.PlayerDamageDPS, 0.01f, 100));
            skillList.Add(new UnitSkill(2, BonusType.AllDamage, 0.2f, 200));
            skillList.Add(new UnitSkill(2, BonusType.AllGold, 0.2f, 400));
            skillList.Add(new UnitSkill(2, BonusType.HeroDamage, 200.0f, 800));

            skillList.Add(new UnitSkill(3, BonusType.HeroDamage, 3.0f, 10));
            skillList.Add(new UnitSkill(3, BonusType.AllGold, 0.2f, 25));
            skillList.Add(new UnitSkill(3, BonusType.AllDamage, 0.2f, 50));
            skillList.Add(new UnitSkill(3, BonusType.PlayerDamageDPS, 0.01f, 100));
            skillList.Add(new UnitSkill(3, BonusType.ChestGold, 0.4f, 200));
            skillList.Add(new UnitSkill(3, BonusType.CriticalChance, 0.02f, 400));
            skillList.Add(new UnitSkill(3, BonusType.AllDamage, 0.6f, 800));

            skillList.Add(new UnitSkill(4, BonusType.HeroDamage, 1.0f, 10));
            skillList.Add(new UnitSkill(4, BonusType.HeroDamage, 8.0f, 25));
            skillList.Add(new UnitSkill(4, BonusType.AllGold, 6.0f, 50));
            skillList.Add(new UnitSkill(4, BonusType.HeroDamage, 5.0f, 100));
            skillList.Add(new UnitSkill(4, BonusType.CriticalDamage, 0.5f, 200));
            skillList.Add(new UnitSkill(4, BonusType.AllDamage, 0.2f, 400));
            skillList.Add(new UnitSkill(4, BonusType.ChestGold, 0.2f, 800));

            skillList.Add(new UnitSkill(5, BonusType.HeroDamage, 3.0f, 10));
            skillList.Add(new UnitSkill(5, BonusType.AllGold, 0.1f, 25));
            skillList.Add(new UnitSkill(5, BonusType.PlayerDamageDPS, 0.01f, 50));
            skillList.Add(new UnitSkill(5, BonusType.AllGold, 0.15f, 100));
            skillList.Add(new UnitSkill(5, BonusType.ChestGold, 0.2f, 200));
            skillList.Add(new UnitSkill(5, BonusType.PlayerDamage, 0.05f, 400));
            skillList.Add(new UnitSkill(5, BonusType.HeroDamage, 100.0f, 800));

            skillList.Add(new UnitSkill(6, BonusType.HeroDamage, 2.0f, 10));
            skillList.Add(new UnitSkill(6, BonusType.HeroDamage, 7.0f, 25));
            skillList.Add(new UnitSkill(6, BonusType.AllDamage, 0.1f, 50));
            skillList.Add(new UnitSkill(6, BonusType.AllDamage, 0.2f, 100));
            skillList.Add(new UnitSkill(6, BonusType.CriticalDamage, 0.05f, 200));
            skillList.Add(new UnitSkill(6, BonusType.CriticalChance, 0.02f, 400));
            skillList.Add(new UnitSkill(6, BonusType.HeroDamage, 100.0f, 800));

            skillList.Add(new UnitSkill(7, BonusType.HeroDamage, 2.0f, 10));
            skillList.Add(new UnitSkill(7, BonusType.AllDamage, 0.05f, 25));
            skillList.Add(new UnitSkill(7, BonusType.AllDamage, 0.07f, 50));
            skillList.Add(new UnitSkill(7, BonusType.HeroDamage, 0.6f, 100));
            skillList.Add(new UnitSkill(7, BonusType.PlayerDamage, 0.05f, 200));
            skillList.Add(new UnitSkill(7, BonusType.ChestGold, 0.2f, 400));
            skillList.Add(new UnitSkill(7, BonusType.AllDamage, 0.3f, 800));

            skillList.Add(new UnitSkill(8, BonusType.HeroDamage, 2.0f, 10));
            skillList.Add(new UnitSkill(8, BonusType.AllDamage, 0.1f, 25));
            skillList.Add(new UnitSkill(8, BonusType.PlayerDamageDPS, 0.01f, 50));
            skillList.Add(new UnitSkill(8, BonusType.AllGold, 0.15f, 100));
            skillList.Add(new UnitSkill(8, BonusType.ChestGold, 0.2f, 200));
            skillList.Add(new UnitSkill(8, BonusType.HeroDamage, 19.0f, 400));
            skillList.Add(new UnitSkill(8, BonusType.AllDamage, 0.2f, 800));

            skillList.Add(new UnitSkill(9, BonusType.HeroDamage, 1.5f, 10));
            skillList.Add(new UnitSkill(9, BonusType.AllDamage, 0.05f, 25));
            skillList.Add(new UnitSkill(9, BonusType.AllDamage, 0.3f, 50));
            skillList.Add(new UnitSkill(9, BonusType.CriticalDamage, 0.05f, 100));
            skillList.Add(new UnitSkill(9, BonusType.HeroDamage, 50.0f, 200));
            skillList.Add(new UnitSkill(9, BonusType.AllDamage, 0.25f, 400));
            skillList.Add(new UnitSkill(9, BonusType.HeroDamage, 100.0f, 800));

            skillList.Add(new UnitSkill(10, BonusType.HeroDamage, 1.5f, 10));
            skillList.Add(new UnitSkill(10, BonusType.CriticalChance, 0.01f, 25));
            skillList.Add(new UnitSkill(10, BonusType.AllDamage, 0.05f, 50));
            skillList.Add(new UnitSkill(10, BonusType.AllGold, 0.15f, 100));
            skillList.Add(new UnitSkill(10, BonusType.ChestGold, 0.2f, 200));
            skillList.Add(new UnitSkill(10, BonusType.ChestGold, 0.25f, 400));
            skillList.Add(new UnitSkill(10, BonusType.AllDamage, 0.15f, 800));

            skillList.Add(new UnitSkill(11, BonusType.HeroDamage, 2.0f, 10));
            skillList.Add(new UnitSkill(11, BonusType.HeroDamage, 7.5f, 25));
            skillList.Add(new UnitSkill(11, BonusType.PlayerDamage, 0.05f, 50));
            skillList.Add(new UnitSkill(11, BonusType.PlayerDamageDPS, 0.01f, 100));
            skillList.Add(new UnitSkill(11, BonusType.AllGold, 0.15f, 200));
            skillList.Add(new UnitSkill(11, BonusType.CriticalChance, 0.25f, 400));
            skillList.Add(new UnitSkill(11, BonusType.HeroDamage, 3.8f, 800));

            skillList.Add(new UnitSkill(12, BonusType.HeroDamage, 2.5f, 10));
            skillList.Add(new UnitSkill(12, BonusType.HeroDamage, 13.0f, 25));
            skillList.Add(new UnitSkill(12, BonusType.AllDamage, 0.05f, 50));
            skillList.Add(new UnitSkill(12, BonusType.CriticalDamage, 0.05f, 100));
            skillList.Add(new UnitSkill(12, BonusType.PlayerDamageDPS, 0.01f, 200));
            skillList.Add(new UnitSkill(12, BonusType.AllDamage, 0.1f, 400));
            skillList.Add(new UnitSkill(12, BonusType.AllGold, 0.2f, 800));

            skillList.Add(new UnitSkill(13, BonusType.HeroDamage, 1.5f, 10));
            skillList.Add(new UnitSkill(13, BonusType.HeroDamage, 8.5f, 25));
            skillList.Add(new UnitSkill(13, BonusType.PlayerDamage, 0.05f, 50));
            skillList.Add(new UnitSkill(13, BonusType.AllDamage, 0.2f, 100));
            skillList.Add(new UnitSkill(13, BonusType.AllDamage, 0.3f, 200));
            skillList.Add(new UnitSkill(13, BonusType.CriticalDamage, 0.05f, 400));
            skillList.Add(new UnitSkill(13, BonusType.HeroDamage, 15.0f, 800));

            skillList.Add(new UnitSkill(14, BonusType.HeroDamage, 2.0f, 10));
            skillList.Add(new UnitSkill(14, BonusType.HeroDamage, 8.0f, 25));
            skillList.Add(new UnitSkill(14, BonusType.PlayerDamageDPS, 0.01f, 50));
            skillList.Add(new UnitSkill(14, BonusType.HeroDamage, 4.0f, 100));
            skillList.Add(new UnitSkill(14, BonusType.AllGold, 0.1f, 200));
            skillList.Add(new UnitSkill(14, BonusType.CriticalDamage, 0.1f, 400));
            skillList.Add(new UnitSkill(14, BonusType.AllGold, 0.1f, 800));

            skillList.Add(new UnitSkill(15, BonusType.HeroDamage, 3.0f, 10));
            skillList.Add(new UnitSkill(15, BonusType.AllDamage, 0.1f, 25));
            skillList.Add(new UnitSkill(15, BonusType.AllDamage, 0.05f, 50));
            skillList.Add(new UnitSkill(15, BonusType.CriticalChance, 0.02f, 100));
            skillList.Add(new UnitSkill(15, BonusType.CriticalDamage, 0.15f, 200));
            skillList.Add(new UnitSkill(15, BonusType.ChestGold, 0.2f, 400));
            skillList.Add(new UnitSkill(15, BonusType.HeroDamage, 100.0f, 800));

            skillList.Add(new UnitSkill(16, BonusType.HeroDamage, 3.5f, 10));
            skillList.Add(new UnitSkill(16, BonusType.ChestGold, 0.25f, 25));
            skillList.Add(new UnitSkill(16, BonusType.AllGold, 0.20f, 50));
            skillList.Add(new UnitSkill(16, BonusType.AllDamage, 0.05f, 100));
            skillList.Add(new UnitSkill(16, BonusType.AllDamage, 0.07f, 200));
            skillList.Add(new UnitSkill(16, BonusType.AllDamage, 0.15f, 400));
            skillList.Add(new UnitSkill(16, BonusType.AllDamage, 0.2f, 800));

            skillList.Add(new UnitSkill(17, BonusType.HeroDamage, 1.5f, 10));
            skillList.Add(new UnitSkill(17, BonusType.HeroDamage, 9.0f, 25));
            skillList.Add(new UnitSkill(17, BonusType.AllGold, 0.1f, 50));
            skillList.Add(new UnitSkill(17, BonusType.AllGold, 0.1f, 100));
            skillList.Add(new UnitSkill(17, BonusType.PlayerDamage, 0.05f, 200));
            skillList.Add(new UnitSkill(17, BonusType.CriticalDamage, 0.1f, 400));
            skillList.Add(new UnitSkill(17, BonusType.AllGold, 0.25f, 800));

            skillList.Add(new UnitSkill(18, BonusType.HeroDamage, 4.0f, 10));
            skillList.Add(new UnitSkill(18, BonusType.HeroDamage, 5.0f, 25));
            skillList.Add(new UnitSkill(18, BonusType.AllDamage, 0.05f, 50));
            skillList.Add(new UnitSkill(18, BonusType.HeroDamage, 4.5f, 100));
            skillList.Add(new UnitSkill(18, BonusType.PlayerDamage, 0.05f, 200));
            skillList.Add(new UnitSkill(18, BonusType.ChestGold, 0.2f, 400));
            skillList.Add(new UnitSkill(18, BonusType.AllDamage, 0.15f, 800));

            skillList.Add(new UnitSkill(19, BonusType.HeroDamage, 2.0f, 10));
            skillList.Add(new UnitSkill(19, BonusType.HeroDamage, 10.0f, 25));
            skillList.Add(new UnitSkill(19, BonusType.PlayerDamageDPS, 0.01f, 50));
            skillList.Add(new UnitSkill(19, BonusType.PlayerDamage, 0.05f, 100));
            skillList.Add(new UnitSkill(19, BonusType.AllDamage, 0.1f, 200));
            skillList.Add(new UnitSkill(19, BonusType.AllGold, 0.1f, 400));
            skillList.Add(new UnitSkill(19, BonusType.AllDamage, 0.1f, 800));

            skillList.Add(new UnitSkill(20, BonusType.HeroDamage, 2.5f, 10));
            skillList.Add(new UnitSkill(20, BonusType.HeroDamage, 6.0f, 25));
            skillList.Add(new UnitSkill(20, BonusType.CriticalDamage, 0.2f, 50));
            skillList.Add(new UnitSkill(20, BonusType.HeroDamage, 4.5f, 100));
            skillList.Add(new UnitSkill(20, BonusType.PlayerDamageDPS, 0.01f, 200));
            skillList.Add(new UnitSkill(20, BonusType.PlayerDamage, 0.05f, 400));
            skillList.Add(new UnitSkill(20, BonusType.AllGold, 0.1f, 800));

            skillList.Add(new UnitSkill(21, BonusType.HeroDamage, 2.0f, 10));
            skillList.Add(new UnitSkill(21, BonusType.PlayerDamage, 0.05f, 25));
            skillList.Add(new UnitSkill(21, BonusType.AllDamage, 0.1f, 50));
            skillList.Add(new UnitSkill(21, BonusType.CriticalChance, 0.02f, 100));
            skillList.Add(new UnitSkill(21, BonusType.AllDamage, 0.1f, 200));
            skillList.Add(new UnitSkill(21, BonusType.ChestGold, 0.2f, 400));
            skillList.Add(new UnitSkill(21, BonusType.HeroDamage, 100.0f, 800));

            skillList.Add(new UnitSkill(22, BonusType.HeroDamage, 2.5f, 10));
            skillList.Add(new UnitSkill(22, BonusType.HeroDamage, 7.5f, 25));
            skillList.Add(new UnitSkill(22, BonusType.AllDamage, 0.1f, 50));
            skillList.Add(new UnitSkill(22, BonusType.HeroDamage, 5.0f, 100));
            skillList.Add(new UnitSkill(22, BonusType.AllDamage, 0.1f, 200));
            skillList.Add(new UnitSkill(22, BonusType.CriticalDamage, 0.3f, 400));
            skillList.Add(new UnitSkill(22, BonusType.AllDamage, 0.2f, 800));

            skillList.Add(new UnitSkill(23, BonusType.HeroDamage, 3.0f, 10));
            skillList.Add(new UnitSkill(23, BonusType.HeroDamage, 8.0f, 25));
            skillList.Add(new UnitSkill(23, BonusType.PlayerDamageDPS, 0.01f, 50));
            skillList.Add(new UnitSkill(23, BonusType.CriticalDamage, 0.2f, 100));
            skillList.Add(new UnitSkill(23, BonusType.PlayerDamage, 0.05f, 200));
            skillList.Add(new UnitSkill(23, BonusType.CriticalChance, 0.02f, 400));
            skillList.Add(new UnitSkill(23, BonusType.HeroDamage, 100.0f, 800));

            skillList.Add(new UnitSkill(24, BonusType.HeroDamage, 2.0f, 10));
            skillList.Add(new UnitSkill(24, BonusType.HeroDamage, 5.0f, 25));
            skillList.Add(new UnitSkill(24, BonusType.HeroDamage, 12.0f, 50));
            skillList.Add(new UnitSkill(24, BonusType.AllGold, 0.15f, 100));
            skillList.Add(new UnitSkill(24, BonusType.ChestGold, 0.2f, 200));
            skillList.Add(new UnitSkill(24, BonusType.HeroDamage, 9.0f, 400));
            skillList.Add(new UnitSkill(24, BonusType.AllDamage, 0.15f, 800));

            skillList.Add(new UnitSkill(25, BonusType.PlayerDamage, 0.05f, 10));
            skillList.Add(new UnitSkill(25, BonusType.PlayerDamage, 0.05f, 25));
            skillList.Add(new UnitSkill(25, BonusType.PlayerDamageDPS, 0.01f, 50));
            skillList.Add(new UnitSkill(25, BonusType.AllDamage, 0.1f, 100));
            skillList.Add(new UnitSkill(25, BonusType.AllGold, 0.15f, 200));
            skillList.Add(new UnitSkill(25, BonusType.CriticalChance, 0.02f, 400));
            skillList.Add(new UnitSkill(25, BonusType.HeroDamage, 150.0f, 800));

            skillList.Add(new UnitSkill(26, BonusType.HeroDamage, 3.5f, 10));
            skillList.Add(new UnitSkill(26, BonusType.HeroDamage, 6.5f, 25));
            skillList.Add(new UnitSkill(26, BonusType.PlayerDamageDPS, 0.01f, 50));
            skillList.Add(new UnitSkill(26, BonusType.AllDamage, 0.05f, 100));
            skillList.Add(new UnitSkill(26, BonusType.AllDamage, 0.1f, 200));
            skillList.Add(new UnitSkill(26, BonusType.AllDamage, 0.05f, 400));
            skillList.Add(new UnitSkill(26, BonusType.AllGold, 0.15f, 800));

            skillList.Add(new UnitSkill(27, BonusType.HeroDamage, 3.0f, 10));
            skillList.Add(new UnitSkill(27, BonusType.HeroDamage, 7.0f, 25));
            skillList.Add(new UnitSkill(27, BonusType.AllDamage, 0.1f, 50));
            skillList.Add(new UnitSkill(27, BonusType.AllDamage, 0.05f, 100));
            skillList.Add(new UnitSkill(27, BonusType.CriticalChance, 0.02f, 200));
            skillList.Add(new UnitSkill(27, BonusType.CriticalDamage, 0.3f, 400));
            skillList.Add(new UnitSkill(27, BonusType.ChestGold, 0.2f, 800));

            skillList.Add(new UnitSkill(28, BonusType.HeroDamage, 3.5f, 10));
            skillList.Add(new UnitSkill(28, BonusType.AllDamage, 0.1f, 25));
            skillList.Add(new UnitSkill(28, BonusType.HeroDamage, 4.0f, 50));
            skillList.Add(new UnitSkill(28, BonusType.HeroDamage, 6.0f, 100));
            skillList.Add(new UnitSkill(28, BonusType.CriticalDamage, 0.2f, 200));
            skillList.Add(new UnitSkill(28, BonusType.CriticalChance, 0.03f, 400));
            skillList.Add(new UnitSkill(28, BonusType.AllDamage, 0.15f, 800));

            skillList.Add(new UnitSkill(29, BonusType.HeroDamage, 3.3f, 10));
            skillList.Add(new UnitSkill(29, BonusType.HeroDamage, 5.5f, 25));
            skillList.Add(new UnitSkill(29, BonusType.AllGold, 0.1f, 50));
            skillList.Add(new UnitSkill(29, BonusType.PlayerDamage, 0.1f, 100));
            skillList.Add(new UnitSkill(29, BonusType.AllGold, 0.2f, 200));
            skillList.Add(new UnitSkill(29, BonusType.AllDamage, 0.1f, 400));
            skillList.Add(new UnitSkill(29, BonusType.AllGold, 0.3f, 800));

            skillList.Add(new UnitSkill(30, BonusType.HeroDamage, 10.0f, 10));
            skillList.Add(new UnitSkill(30, BonusType.PlayerDamage, 0.2f, 25));
            skillList.Add(new UnitSkill(30, BonusType.PlayerDamageDPS, 0.05f, 50));
            skillList.Add(new UnitSkill(30, BonusType.AllGold, 0.2f, 100));
            skillList.Add(new UnitSkill(30, BonusType.AllDamage, 0.1f, 200));
            skillList.Add(new UnitSkill(30, BonusType.AllDamage, 0.2f, 400));
            skillList.Add(new UnitSkill(30, BonusType.AllDamage, 0.3f, 800));

            skillList.Add(new UnitSkill(31, BonusType.HeroDamage, 9.0f, 10));
            skillList.Add(new UnitSkill(31, BonusType.HeroDamage, 20.0f, 25));
            skillList.Add(new UnitSkill(31, BonusType.CriticalChance, 0.01f, 50));
            skillList.Add(new UnitSkill(31, BonusType.PlayerDamage, 0.6f, 100));
            skillList.Add(new UnitSkill(31, BonusType.ChestGold, 0.25f, 200));
            skillList.Add(new UnitSkill(31, BonusType.AllDamage, 0.1f, 400));
            skillList.Add(new UnitSkill(31, BonusType.AllGold, 0.15f, 800));

            
            skillList.Add(new UnitSkill(32, BonusType.HeroDamage, 0.4f, 10));
            skillList.Add(new UnitSkill(32, BonusType.HeroDamage, 0.2f, 25));
            skillList.Add(new UnitSkill(32, BonusType.AllGold, 0.25f, 50));
            skillList.Add(new UnitSkill(32, BonusType.PlayerDamage, 0.6f, 100));
            skillList.Add(new UnitSkill(32, BonusType.CriticalChance, 0.02f, 200));
            skillList.Add(new UnitSkill(32, BonusType.AllDamage, 0.3f, 400));
            skillList.Add(new UnitSkill(32, BonusType.AllDamage, 0.1f, 800));

            skillList.Add(new UnitSkill(33, BonusType.HeroDamage, 20.0f, 10));
            skillList.Add(new UnitSkill(33, BonusType.PlayerDamage, 0.2f, 25));
            skillList.Add(new UnitSkill(33, BonusType.PlayerDamageDPS, 0.01f, 50));
            skillList.Add(new UnitSkill(33, BonusType.AllGold, 0.2f, 100));
            skillList.Add(new UnitSkill(33, BonusType.AllDamage, 0.2f, 200));
            skillList.Add(new UnitSkill(33, BonusType.AllDamage, 0.3f, 400));
            skillList.Add(new UnitSkill(33, BonusType.AllDamage, 0.4f, 800));

        }
    }
}
