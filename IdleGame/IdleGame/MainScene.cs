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
        public Music Music = new Music("Assets/Sounds/Rolemusic - If Pigs Could Sing.ogg");
        public Dictionary<BonusType, double> Bonuses = new Dictionary<BonusType, double>();
        public Image background = new Image("Assets/Img/background_road2.png");
        public MyPlayer player;
        public MyPlayer player2;
        public List<UnitSkill> skillList = new List<UnitSkill>();
        public List<Unit> unitsList = new List<Unit>();
        public List<Gear> gearList = new List<Gear>();
        public List<Enemy_Units> enemyList;
        public Random random = new Random();
        public Stage stage;
        public int currentStage = 0;
        public Image HPBG = new Image("Assets/Img/HPBarBG.png");
        public Image HPFG = new Image("Assets/Img/HPBarFG.png");
        public Entity stagee;
        public Text staget;
        public Entity stageNumbere;
        public Text stageNumbert;
        public double totaldeeps = 0;
        public PlayerGui menu;
        public int gearOwned = 0;
        public bool isHit = false;
        public bool needUpdate;
        public Crosshair crosshair;
        public double soundVolume;
        public double musicVolume;
        Entity debuge;
        Text debugt;
        Session session;
        DataSaver saveGame = new DataSaver();
        public double totalDPS = 0;
        public MainScene(Session session) : base()
        {
            this.session = session;
        }
        public string GetAnimationString(int a, int b)
        {
            string text = "";
            for (int z = a; z<b; z++)
            {
                text += z + ", ";
            }
            text += b;
            return text;
        }
        public override void Begin()
        {
            enemyList = new List<Enemy_Units>();
            base.Begin();
            crosshair = new Crosshair();
            player = new MyPlayer(941, 575);
            Add(player);
            AddGraphic(background);
            foreach (BonusType bonusType in Enum.GetValues(typeof(BonusType)))
            {
                Bonuses.Add(bonusType, 0);
            }
            CreateGear();
            CreateSkills();
            CreateUnits();
            //gearList[8].level = 10;
            //gearList[8].unlocked = true;
            UpdateBonuses();
            //currentStage = 1000;
            HUD();
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
            player.gold = 2.0e100;
            new Sniper(1112, 610);
            new Soldier(1280, 510);
            new Mortar(1730, 722);
            new Turret(930, 500);
            new Dicokka(1550, 500);
            new FatTonk(1500, 550);
            new Minigun(1320, 700);
            new BigTonk(1460, 480);
            new BiggestTonk(1770, 660);
            new Heli(1080, 280);
            new Hover(1450, 320);
            new Rocket(1700, 550);
            if (session.Data.FileExists())
                LoadGame();
            else
                Console.WriteLine("Ni ma pliku");
            StartStage();
            this.soundVolume = Sound.GlobalVolume;
            Sound.GlobalVolume = 0;
            Music.Play();

            new Garbage(754, 462, "Assets/Img/Decals/des09.png", false);
            new Garbage(831, 533, "Assets/Img/Decals/des16.png", true);
            new Garbage(832, 689, "Assets/Img/Decals/des17.png", true);
            new Garbage(925, 670, "Assets/Img/Decals/des10.png", false);
            new Garbage(936, 531, "Assets/Img/Decals/des01.png", false);
            new Garbage(1004, 575, "Assets/Img/Decals/des19.png", false);
            new Garbage(1322, 497, "Assets/Img/Decals/des19.png", false);
            new Garbage(1082, 612, "Assets/Img/Decals/des14.png", false);
            new Garbage(1115, 704, "Assets/Img/Decals/des18.png", false);
            new Garbage(1206, 665, "Assets/Img/Decals/des08.png", false);
            new Garbage(1161, 537, "Assets/Img/Decals/des08.png", false);
            new Garbage(1214, 484, "Assets/Img/Decals/des15.png", false);
            new Garbage(1032, 378, "Assets/Img/Decals/des13.png", true);
            new Garbage(1276, 555, "Assets/Img/Decals/des11.png", false);
            new Garbage(1248, 597, "Assets/Img/Decals/des24.png", false);
            new Garbage(1377, 595, "Assets/Img/Decals/des25.png", false);
            new Garbage(1372, 659, "Assets/Img/Decals/des20.png", false);
            new Garbage(1476, 648, "Assets/Img/Decals/des12.png", false);
            new Garbage(1381, 709, "Assets/Img/Decals/des22.png", false);
        }

        public void LoadGame()
        {
            session.Data.Import();
            foreach (var unit in unitsList)
            {
                while (unit.level != session.Data.GetInt(unit.name))
                    unit.UpgradeHero();
            }
            foreach (var gear in gearList)
            {
                gear.level = session.Data.GetInt(gear.name + "Level");
                gear.unlocked = session.Data.GetBool(gear.name + "Unlocked");
                if (gear.unlocked)
                    gearOwned++;
            }
            player.honor = session.Data.GetFloat("playerHonor");
            player.gold = session.Data.GetFloat("playerGold");
            player.level = session.Data.GetInt("playerLevel");
            this.currentStage = session.Data.GetInt("stageUnlocked") - 1;
        }

        public void SaveGame()
        {
            session.Data.SetData("playerHonor", player.honor);
            session.Data.SetData("playerGold", player.gold);
            session.Data.SetData("playerLevel", player.level);
            session.Data.SetData("stageUnlocked", currentStage);
            foreach (var unit in unitsList)
                session.Data.SetData(unit.name, unit.level);
            foreach (var gear in gearList)
            {
                session.Data.SetData(gear.name + "Level", gear.level);
                session.Data.SetData(gear.name + "Unlocked", gear.unlocked);
            }
            session.Data.Export();
        }

        public void LayerEnemies()
        {
            int order = -500;
            List<Enemy_Units> SortedList = enemyList.OrderBy(o => (o.Y + o.Hitbox.Height)).ToList();
            foreach(var unit in SortedList)
            {
                order--;
                unit.Layer = order;
            }
        }

        public override void Update()
        {
            SaveGame();
            if (needUpdate)
            {
                UpdateBonuses();
                player.UpdatePlayerStats();
                foreach (var unit in unitsList)
                    unit.UpdateUnitStats();
                needUpdate = false;
            }
            /*if (GetCount<Enemy_Soldier>() < 2)
            {
                new Enemy_Soldier(random.Next(-60, -40), random.Next(511, 700));
                LayerEnemies();
            }
            if (GetCount<Enemy_Bazooka>() < 2)
            {
                new Enemy_Bazooka(random.Next(-60, -40), random.Next(511, 700));
                LayerEnemies();
            }
            if (GetCount<Enemy_Riflemon>() < 2)
            {
                new Enemy_Riflemon(random.Next(-60, -40), random.Next(511, 700));
                LayerEnemies();
            }
            if (GetCount<Enemy_Shield>() < 2)
            {
                new Enemy_Shield(random.Next(-60, -40), random.Next(511, 700));
                LayerEnemies();
            }
            if (GetCount<Enemy_Cokka>() < 2)
            {
                new Enemy_Cokka(random.Next(-60, -40), random.Next(511, 700));
                LayerEnemies();
            }
            if (GetCount<Enemy_Mummy>() < 2)
            {
                new Enemy_Mummy(random.Next(-60, -40), random.Next(511, 700));
                LayerEnemies();
            }*/
            /*if (GetCount<Enemy_Riflemon>() < 20)
            {
                new Enemy_Riflemon(random.Next(-100, -50), random.Next(490, 720));
                LayerEnemies();
            }
            if (GetCount<Enemy_Soldier>() < 0)
            {
                new Enemy_Soldier(random.Next(-100, -50), random.Next(490, 720));
                LayerEnemies();
            }*/
            isHit = false;
            HUD();
            base.Update();
            Attack();
            if (Input.KeyDown(Key.Down))
            {
                Sound.GlobalVolume -= 0.02f;
                Music.GlobalVolume -= 0.02f;
            }
            if (Input.KeyDown(Key.Up))
            {
                Sound.GlobalVolume += 0.02f;
                Music.GlobalVolume += 0.02f;
            }
            if (Input.KeyPressed(Key.Space))
            {
                if (Music.IsPlaying)
                {
                    Music.Pause();
                }
                else
                {
                    Music.Play();
                }
            }
            if (Input.KeyPressed(Key.S))
            {
                if (Sound.GlobalVolume == 0 && this.soundVolume != 0)
                {
                    Sound.GlobalVolume = (float)this.soundVolume;
                }
                else
                {
                    this.soundVolume = Sound.GlobalVolume;
                    Sound.GlobalVolume = 0;
                }
            }
            if (Input.KeyPressed(Key.R))
            {
                session.Data.ClearFile();
                Game.Close();
            }

            //player.spritemap.Angle = (float)((Math.Atan2(Input.MouseX - player.X, Input.MouseY - player.Y) - 1.5) * (180 / Math.PI));
            //Console.WriteLine(isHit);
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
                HPBG.Scale = 1f;
                HPFG.Scale = 1f;
            }
            else
            {
                stageNumbere.Graphic.CenterOrigin();
                stageNumbert.String = stage.stage + "";
                debugt.String = "X: " + Input.MouseX + "\nY: " + Input.MouseY + "\n" + Sound.GlobalVolume + "\n" + Math.Atan2(Input.MouseX - player.X, Input.MouseY - player.Y);
                staget.String = "Stage";
                HPFG.ClippingRegion = new Rectangle(0, 0, (int)Math.Ceiling((HPFG.Width * (stage.CurrentHP / stage.MaxHP))), HPFG.Height);
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
                if (enemyList.Count != 0)
                {
                    enemyList[random.Next(0, enemyList.Count)].GetDamage(Unit.GetDPSByLevel(Unit.level) / 60);
                }
                totalDPS += Unit.GetDPSByLevel(Unit.level);
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
                double chestTest = random.NextDouble();
                if (stage.GetTreasureSpawnChance() >= chestTest)
                {
                    new Chest(random.Next(50, 801), random.Next(520, 750));
                    Console.WriteLine(chestTest + "<" + stage.GetTreasureSpawnChance());
                }
            }
        }

        void CreateGear()
        {
            this.gearList.Add(new Gear("Medal of Honor", 0, BonusType.MonsterGold, 0.1f, 0.5f, 0.25f, "Assets/Img/Gear/icon_moh.png"));
            //this.gearList.Add(new Gear("High Capacity Battery", 0, BonusType.BerserkerRageDuration, 0.1f, 0.7f, 0.35f, "Assets/Img/Gear/icon_battery.png"));
            //this.gearList.Add(new Gear("Fusion Core", 10, BonusType.BerserkerRageCooldown, 0.05f, 0.7f, 0.35f, "Assets/Img/Gear/icon_fusion_core.png"));
            this.gearList.Add(new Gear("Dark Matter", 0, BonusType.AllDamage, 0.02f, 0.9f, 0.9f, "Assets/Img/Gear/icon_dark_matter.png"));
            this.gearList.Add(new Gear("Treasure Chest", 0, BonusType.ChestGold, 0.2f, 0.4f, 0.2f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Philosopher's Stone", 0, BonusType.AllGold, 0.15f, 0.4f, 0.2f, "Assets/Img/Gear/icon_stone.png"));
            this.gearList.Add(new Gear("Illuminati Membership", 0, BonusType.ChestChance, 0.2f, 0.4f, 0.2f, "Assets/Img/Gear/icon_illu.png"));
            this.gearList.Add(new Gear("Ancient Tablet", 25, BonusType.CriticalChance, 0.02f, 0.3f, 0.15f, "Assets/Img/Gear/icon_ancient.png"));
            this.gearList.Add(new Gear("Tyrant Treasure Box", 0, BonusType.ChanceFor10xGold, 0.005f, 0.3f, 0.15f, "Assets/Img/Gear/icon_tyrant.png"));
            this.gearList.Add(new Gear("Unit 2 Vulcan Cannon", 0, BonusType.PlayerDamage, 0.04f, 0.6f, 0.30f, "Assets/Img/Gear/icon_vulcan_cannon.png"));
            this.gearList.Add(new Gear("Poo Covered Ammo", 0, BonusType.CriticalDamage, 0.2f, 0.3f, 0.15f, "Assets/Img/Gear/icon_poo.png"));
            //this.gearList.Add(new Gear("Canned Food", 10, BonusType.WarCryCooldown, 0.05f, 1.2f, 0.6f, "Assets/Img/Gear/icon_canned_food.png"));
            //this.gearList.Add(new Gear("Martian Element X", 10, BonusType.HandOfMidasCooldown, 0.05f, 0.7f, 0.35f, "Assets/Img/Gear/icon_martian_element_x.png"));
            //this.gearList.Add(new Gear("Experimental Core", 0, BonusType.ShadowCloneDuration, 0.1f, 0.7f, 0.35f, "Assets/Img/Gear/icon_unit03_core.png"));
            //this.gearList.Add(new Gear("Scrap", 10, BonusType.ShadowCloneCooldown, 0.05f, 0.7f, 0.35f, "Assets/Img/Gear/icon_scrap.png"));
            //this.gearList.Add(new Gear("Mysterious Liquid 2.0", 0, BonusType.CriticalStrikeDuration, 0.1f, 0.7f, 0.35f, "Assets/Img/Gear/icon_mysterious_liquid_special.png"));
            //this.gearList.Add(new Gear("Martian Energy", 0, BonusType.HandOfMidasDuration, 0.1f, 0.7f, 0.35f, "Assets/Img/Gear/icon_martian_energy.png"));
            this.gearList.Add(new Gear("Spoils of War", 25, BonusType.UpgradeCost, 0.02f, 0.3f, 0.15f, "Assets/Img/Gear/icon_spoils.png"));
            //this.gearList.Add(new Gear("Mysterious Liquid", 10, BonusType.CriticalStrikeCooldown, 0.05f, 0.7f, 0.35f, "Assets/Img/Gear/icon_mysterious_liquid.png"));
            //this.gearList.Add(new Gear("Mobile Artillery Cannons", 10, BonusType.HeavenlyStrikeCooldown, 0.05f, 0.7f, 0.35f, "Assets/Img/Gear/icon_warhead.png"));
            //this.gearList.Add(new Gear("Tincture of the Maker", 0, BonusType.AllDamage, 0.05f, 0.1f, 0.05f, "Assets/Img/Gear/icon_chest.png"));
            this.gearList.Add(new Gear("Lucky Charm", 0, BonusType.BonusRelic, 0.05f, 0.3f, 0.15f, "Assets/Img/Gear/icon_charm.png"));
            //this.gearList.Add(new Gear("Shrooms", 0, BonusType.WarCryDuration, 0.1f, 1.2f, 0.6f, "Assets/Img/Gear/icon_shroom.png"));
        }

        void CreateUnits()
        {
            unitsList.Add(new Unit(1, "Cannon Fodder", 50, "Assets/Img/Gui/icon_private.png"));
            unitsList.Add(new Unit(2, "Master Sergeant Shooter Person", 175, "Assets/Img/Gui/icon_marksman.png"));
            unitsList.Add(new Unit(3, "Captain James Hook", 675, "Assets/Img/Gui/icon_mortar.png"));
            unitsList.Add(new Unit(4, "Scrap Cannon", 2850, "Assets/Img/Gui/icon_scrap_cannon.png"));
            unitsList.Add(new Unit(5, "Small Tonk", 13300, "Assets/Img/Gui/icon_dicokka.png"));
            unitsList.Add(new Unit(6, "Fat Tonk", 68100, "Assets/Img/Gui/icon_ACP.png"));
            unitsList.Add(new Unit(7, "Human Tonk", 384000, "Assets/Img/Gui/icon_minigun.png"));
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
            skillList.Add(new UnitSkill(4, BonusType.AllGold, 0.06f, 50));
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
