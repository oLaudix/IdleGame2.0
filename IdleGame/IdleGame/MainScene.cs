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
        Image background = new Image("Assets/Img/background.png");
        MyPlayer player = new MyPlayer(1000, 800);
        List<UnitSkill> skillList = new List<UnitSkill>();
        List<Unit> unitsList = new List<Unit>();
        List<Gear> gearList = new List<Gear>();
        public MainScene() : base()
        {
            AddGraphic(background);
            Add(player);

            this.gearList.Add(new Gear("Amulet of the Valrunes", 0, BonusType.MonsterGold, 0.1f, 0.5f, 0.25f));
            this.gearList.Add(new Gear("Axe of Resolution", 0, BonusType.BerserkerRageDuration, 0.1f, 0.7f, 0.35f));
            this.gearList.Add(new Gear("Barbarian's Mettle", 10, BonusType.BerserkerRageCooldown, 0.05f, 0.7f, 0.35f));
            this.gearList.Add(new Gear("Brew of Absorbtion", 0, BonusType.AllDamage, 0.02f, 0.9f, 0.9f));
            this.gearList.Add(new Gear("Chest of Contentment", 0, BonusType.GoldFromChests, 0.2f, 0.4f, 0.2f));
            this.gearList.Add(new Gear("Crafter's Elixir", 0, BonusType.AllGold, 0.15f, 0.4f, 0.2f));
            this.gearList.Add(new Gear("Crown Egg", 0, BonusType.ChestChance, 0.2f, 0.4f, 0.2f));
            this.gearList.Add(new Gear("Death Seeker", 25, BonusType.CriticalChance, 0.02f, 0.3f, 0.15f));
            this.gearList.Add(new Gear("Divine Chalice", 0, BonusType.ChanceFor10xGold, 0.005f, 0.3f, 0.15f));
            this.gearList.Add(new Gear("Drunken Hammer", 0, BonusType.PlayerDamage, 0.04f, 0.6f, 0.30f));
            this.gearList.Add(new Gear("Hero's Thrust", 0, BonusType.CriticalDamage, 0.2f, 0.3f, 0.15f));
            this.gearList.Add(new Gear("Hunter's Ointment", 10, BonusType.WarCryCooldown, 0.05f, 1.2f, 0.6f));
            this.gearList.Add(new Gear("Laborer's Pendant", 10, BonusType.HandOfMidasCooldown, 0.05f, 0.7f, 0.35f));
            this.gearList.Add(new Gear("Ogre's Gauntlet", 0, BonusType.ShadowCloneDuration, 0.1f, 0.7f, 0.35f));
            this.gearList.Add(new Gear("Overseer's Lotion", 10, BonusType.ShadowCloneCooldown, 0.05f, 0.7f, 0.35f));
            this.gearList.Add(new Gear("Parchment of Importance", 0, BonusType.CriticalStrikeDuration, 0.1f, 0.7f, 0.35f));
            this.gearList.Add(new Gear("Ring of Opulence", 0, BonusType.HandOfMidasDuration, 0.1f, 0.7f, 0.35f));
            this.gearList.Add(new Gear("Ring of Wonderous Charm", 25, BonusType.UpgradeCost, 0.02f, 0.3f, 0.15f));
            this.gearList.Add(new Gear("Sacred Scroll", 10, BonusType.CriticalStrikeCooldown, 0.05f, 0.7f, 0.35f));
            this.gearList.Add(new Gear("Saintly Shield", 10, BonusType.HeavenlyStrikeCooldown, 0.05f, 0.7f, 0.35f));
            this.gearList.Add(new Gear("Tincture of the Maker", 0, BonusType.AllDamage, 0.05f, 0.1f, 0.05f));
            this.gearList.Add(new Gear("Undead Aura", 0, BonusType.BonusRelic, 0.05f, 0.3f, 0.15f));
            this.gearList.Add(new Gear("Universal Fissure", 0, BonusType.WarCryDuration, 0.1f, 1.2f, 0.6f));




            unitsList.Add(new Unit(1, "Takeda", 50));
            unitsList.Add(new Unit(2, "Contessa", 175));
            unitsList.Add(new Unit(3, "Hornetta", 675));
            unitsList.Add(new Unit(4, "Mila", 2850));
            unitsList.Add(new Unit(5, "Terra", 13300));
            unitsList.Add(new Unit(6, "Inquisireaux", 68100));
            unitsList.Add(new Unit(7, "Charlotte ", 384000));
            unitsList.Add(new Unit(8, "Jordaan", 2800000));
            unitsList.Add(new Unit(9, "Jukka", 23800000));
            unitsList.Add(new Unit(10, "Milo", 143000000));
            unitsList.Add(new Unit(11, "Macelord ", 943000000));
            unitsList.Add(new Unit(12, "Gertrude ", 84E+09f));
            unitsList.Add(new Unit(13, "Twitterella  ", 5.47E+10f));
            unitsList.Add(new Unit(14, "Master   ", 8.20E+11f));
            unitsList.Add(new Unit(15, "Elpha   ", 8.20E+12f));
            unitsList.Add(new Unit(16, "Poppy", 1.64E+14f));
            unitsList.Add(new Unit(17, "Skulptor", 1.64E+15f));
            unitsList.Add(new Unit(18, "Sterling", 4.92E+16f));
            unitsList.Add(new Unit(19, "Orba", 2.46E+18f));
            unitsList.Add(new Unit(20, "Remus", 7.38E+19f));
            unitsList.Add(new Unit(21, "Mikey", 2.44E+21f));
            unitsList.Add(new Unit(22, "Peter", 2.44E+23f));
            unitsList.Add(new Unit(23, "Teeny ", 4.87E+25f));
            unitsList.Add(new Unit(24, "Deznis", 1.95E+28f));
            unitsList.Add(new Unit(25, "Hamlette ", 2.14E+31f));
            unitsList.Add(new Unit(26, "Eistor", 2.36E+36f));
            unitsList.Add(new Unit(27, "Flavius", 2.59E+46));
            unitsList.Add(new Unit(28, "Chester", 2.85E+61));
            unitsList.Add(new Unit(29, "Mohacas", 3.14E+81));
            unitsList.Add(new Unit(30, "Jaqulin", 3.14E+96));
            unitsList.Add(new Unit(31, "Pixie", 3.76E+101));
            unitsList.Add(new Unit(32, "Jackalope", 4.14E+136));
            unitsList.Add(new Unit(33, "Dark Lord", 4.56E+141));
            //unitsList[0].
            //System.Console.WriteLine(unitsList[0].GetUpgradeCostByLevel(1));




            
            skillList.Add(new UnitSkill(unitsList[0].heroID, BonusType.HeroDamage, 1.0f, 10, unitsList[0].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[0].heroID, BonusType.HeroDamage, 2.0f, 25, unitsList[0].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[0].heroID, BonusType.AllDamage, 0.2f, 50, unitsList[0].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[0].heroID, BonusType.CriticalDamage, 0.2f, 100, unitsList[0].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[0].heroID, BonusType.HeroDamage, 20.0f, 200, unitsList[0].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[0].heroID, BonusType.AllDamage, 0.5f, 400, unitsList[0].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[0].heroID, BonusType.HeroDamage, 200.0f, 800, unitsList[0].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[1].heroID, BonusType.PlayerDamage, 0.2f, 10, unitsList[1].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[1].heroID, BonusType.HeroDamage, 2.0f, 25, unitsList[1].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[1].heroID, BonusType.HeroDamage, 20.0f, 50, unitsList[1].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[1].heroID, BonusType.PlayerDamageDPS, 0.01f, 100, unitsList[1].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[1].heroID, BonusType.AllDamage, 0.2f, 200, unitsList[1].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[1].heroID, BonusType.AllGold, 0.2f, 400, unitsList[1].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[1].heroID, BonusType.HeroDamage, 200.0f, 800, unitsList[1].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[2].heroID, BonusType.HeroDamage, 3.0f, 10, unitsList[2].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[2].heroID, BonusType.AllGold, 0.2f, 25, unitsList[2].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[2].heroID, BonusType.AllDamage, 0.2f, 50, unitsList[2].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[2].heroID, BonusType.PlayerDamageDPS, 0.01f, 100, unitsList[2].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[2].heroID, BonusType.ChestGold, 0.4f, 200, unitsList[2].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[2].heroID, BonusType.CriticalChance, 0.02f, 400, unitsList[2].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[2].heroID, BonusType.AllDamage, 0.6f, 800, unitsList[2].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[3].heroID, BonusType.HeroDamage, 1.0f, 10, unitsList[3].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[3].heroID, BonusType.HeroDamage, 8.0f, 25, unitsList[3].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[3].heroID, BonusType.AllGold, 6.0f, 50, unitsList[3].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[3].heroID, BonusType.HeroDamage, 5.0f, 100, unitsList[3].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[3].heroID, BonusType.CriticalDamage, 0.5f, 200, unitsList[3].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[3].heroID, BonusType.AllDamage, 0.2f, 400, unitsList[3].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[3].heroID, BonusType.ChestGold, 0.2f, 800, unitsList[3].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[4].heroID, BonusType.HeroDamage, 3.0f, 10, unitsList[4].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[4].heroID, BonusType.AllGold, 0.1f, 25, unitsList[4].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[4].heroID, BonusType.PlayerDamageDPS, 0.01f, 50, unitsList[4].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[4].heroID, BonusType.AllGold, 0.15f, 100, unitsList[4].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[4].heroID, BonusType.ChestGold, 0.2f, 200, unitsList[4].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[4].heroID, BonusType.PlayerDamage, 0.05f, 400, unitsList[4].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[4].heroID, BonusType.HeroDamage, 100.0f, 800, unitsList[4].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[5].heroID, BonusType.HeroDamage, 2.0f, 10, unitsList[5].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[5].heroID, BonusType.HeroDamage, 7.0f, 25, unitsList[5].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[5].heroID, BonusType.AllDamage, 0.1f, 50, unitsList[5].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[5].heroID, BonusType.AllDamage, 0.2f, 100, unitsList[5].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[5].heroID, BonusType.CriticalDamage, 0.05f, 200, unitsList[5].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[5].heroID, BonusType.CriticalChance, 0.02f, 400, unitsList[5].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[5].heroID, BonusType.HeroDamage, 100.0f, 800, unitsList[5].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[6].heroID, BonusType.HeroDamage, 2.0f, 10, unitsList[6].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[6].heroID, BonusType.AllDamage, 0.05f, 25, unitsList[6].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[6].heroID, BonusType.AllDamage, 0.07f, 50, unitsList[6].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[6].heroID, BonusType.HeroDamage, 0.6f, 100, unitsList[6].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[6].heroID, BonusType.PlayerDamage, 0.05f, 200, unitsList[6].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[6].heroID, BonusType.ChestGold, 0.2f, 400, unitsList[6].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[6].heroID, BonusType.AllDamage, 0.3f, 800, unitsList[6].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[7].heroID, BonusType.HeroDamage, 2.0f, 10, unitsList[7].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[7].heroID, BonusType.AllDamage, 0.1f, 25, unitsList[7].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[7].heroID, BonusType.PlayerDamageDPS, 0.01f, 50, unitsList[7].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[7].heroID, BonusType.AllGold, 0.15f, 100, unitsList[7].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[7].heroID, BonusType.ChestGold, 0.2f, 200, unitsList[7].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[7].heroID, BonusType.ChestGold, 19.0f, 400, unitsList[7].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[7].heroID, BonusType.AllDamage, 0.2f, 800, unitsList[7].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[8].heroID, BonusType.HeroDamage, 1.5f, 10, unitsList[8].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[8].heroID, BonusType.AllDamage, 0.05f, 25, unitsList[8].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[8].heroID, BonusType.AllDamage, 0.3f, 50, unitsList[8].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[8].heroID, BonusType.CriticalDamage, 0.05f, 100, unitsList[8].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[8].heroID, BonusType.HeroDamage, 50.0f, 200, unitsList[8].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[8].heroID, BonusType.AllDamage, 0.25f, 400, unitsList[8].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[8].heroID, BonusType.HeroDamage, 100.0f, 800, unitsList[8].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[9].heroID, BonusType.HeroDamage, 1.5f, 10, unitsList[9].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[9].heroID, BonusType.CriticalChance, 0.01f, 25, unitsList[9].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[9].heroID, BonusType.AllDamage, 0.05f, 50, unitsList[9].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[9].heroID, BonusType.AllGold, 0.15f, 100, unitsList[9].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[9].heroID, BonusType.ChestGold, 0.2f, 200, unitsList[9].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[9].heroID, BonusType.ChestGold, 0.25f, 400, unitsList[9].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[9].heroID, BonusType.HeroDamage, 0.15f, 800, unitsList[9].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[10].heroID, BonusType.HeroDamage, 2.0f, 10, unitsList[10].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[10].heroID, BonusType.HeroDamage, 7.5f, 25, unitsList[10].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[10].heroID, BonusType.PlayerDamage, 0.05f, 50, unitsList[10].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[10].heroID, BonusType.PlayerDamageDPS, 0.01f, 100, unitsList[10].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[10].heroID, BonusType.AllGold, 0.15f, 200, unitsList[10].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[10].heroID, BonusType.CriticalChance, 0.25f, 400, unitsList[10].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[10].heroID, BonusType.HeroDamage, 3.8f, 800, unitsList[10].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[11].heroID, BonusType.HeroDamage, 2.5f, 10, unitsList[11].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[11].heroID, BonusType.HeroDamage, 13.0f, 25, unitsList[11].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[11].heroID, BonusType.AllDamage, 0.05f, 50, unitsList[11].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[11].heroID, BonusType.CriticalDamage, 0.05f, 100, unitsList[11].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[11].heroID, BonusType.PlayerDamageDPS, 0.01f, 200, unitsList[11].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[11].heroID, BonusType.AllDamage, 0.1f, 400, unitsList[11].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[11].heroID, BonusType.AllGold, 0.2f, 800, unitsList[11].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[12].heroID, BonusType.HeroDamage, 1.5f, 10, unitsList[12].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[12].heroID, BonusType.HeroDamage, 8.5f, 25, unitsList[12].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[12].heroID, BonusType.PlayerDamage, 0.05f, 50, unitsList[12].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[12].heroID, BonusType.AllDamage, 0.2f, 100, unitsList[12].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[12].heroID, BonusType.AllDamage, 0.3f, 200, unitsList[12].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[12].heroID, BonusType.CriticalDamage, 0.05f, 400, unitsList[12].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[12].heroID, BonusType.HeroDamage, 15.0f, 800, unitsList[12].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[13].heroID, BonusType.HeroDamage, 2.0f, 10, unitsList[13].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[13].heroID, BonusType.HeroDamage, 8.0f, 25, unitsList[13].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[13].heroID, BonusType.PlayerDamageDPS, 0.01f, 50, unitsList[13].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[13].heroID, BonusType.HeroDamage, 4.0f, 100, unitsList[13].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[13].heroID, BonusType.AllGold, 0.1f, 200, unitsList[13].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[13].heroID, BonusType.CriticalDamage, 0.1f, 400, unitsList[13].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[13].heroID, BonusType.AllGold, 0.1f, 800, unitsList[13].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[14].heroID, BonusType.HeroDamage, 3.0f, 10, unitsList[14].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[14].heroID, BonusType.AllDamage, 0.1f, 25, unitsList[14].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[14].heroID, BonusType.AllDamage, 0.05f, 50, unitsList[14].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[14].heroID, BonusType.CriticalChance, 0.02f, 100, unitsList[14].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[14].heroID, BonusType.CriticalDamage, 0.15f, 200, unitsList[14].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[14].heroID, BonusType.ChestGold, 0.2f, 400, unitsList[14].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[14].heroID, BonusType.HeroDamage, 100.0f, 800, unitsList[14].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[15].heroID, BonusType.HeroDamage, 3.5f, 10, unitsList[15].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[15].heroID, BonusType.ChestGold, 0.25f, 25, unitsList[15].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[15].heroID, BonusType.AllGold, 0.20f, 50, unitsList[15].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[15].heroID, BonusType.AllDamage, 0.05f, 100, unitsList[15].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[15].heroID, BonusType.AllDamage, 0.07f, 200, unitsList[15].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[15].heroID, BonusType.AllDamage, 0.15f, 400, unitsList[15].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[15].heroID, BonusType.AllDamage, 0.2f, 800, unitsList[15].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[16].heroID, BonusType.HeroDamage, 1.5f, 10, unitsList[16].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[16].heroID, BonusType.HeroDamage, 9.0f, 25, unitsList[16].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[16].heroID, BonusType.AllGold, 0.1f, 50, unitsList[16].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[16].heroID, BonusType.AllGold, 0.1f, 100, unitsList[16].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[16].heroID, BonusType.PlayerDamage, 0.05f, 200, unitsList[16].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[16].heroID, BonusType.CriticalDamage, 0.1f, 400, unitsList[16].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[16].heroID, BonusType.AllGold, 0.25f, 800, unitsList[16].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[17].heroID, BonusType.HeroDamage, 4.0f, 10, unitsList[17].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[17].heroID, BonusType.HeroDamage, 5.0f, 25, unitsList[17].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[17].heroID, BonusType.AllDamage, 0.05f, 50, unitsList[17].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[17].heroID, BonusType.HeroDamage, 4.5f, 100, unitsList[17].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[17].heroID, BonusType.PlayerDamage, 0.05f, 200, unitsList[17].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[17].heroID, BonusType.ChestGold, 0.2f, 400, unitsList[17].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[17].heroID, BonusType.AllDamage, 0.15f, 800, unitsList[17].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[18].heroID, BonusType.HeroDamage, 2.0f, 10, unitsList[18].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[18].heroID, BonusType.HeroDamage, 10.0f, 25, unitsList[18].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[18].heroID, BonusType.PlayerDamageDPS, 0.01f, 50, unitsList[18].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[18].heroID, BonusType.PlayerDamage, 0.05f, 100, unitsList[18].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[18].heroID, BonusType.AllDamage, 0.1f, 200, unitsList[18].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[18].heroID, BonusType.AllGold, 0.1f, 400, unitsList[18].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[18].heroID, BonusType.AllDamage, 0.1f, 800, unitsList[18].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[19].heroID, BonusType.HeroDamage, 2.5f, 10, unitsList[19].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[19].heroID, BonusType.HeroDamage, 6.0f, 25, unitsList[19].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[19].heroID, BonusType.CriticalDamage, 0.2f, 50, unitsList[19].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[19].heroID, BonusType.HeroDamage, 4.5f, 100, unitsList[19].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[19].heroID, BonusType.PlayerDamageDPS, 0.01f, 200, unitsList[19].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[19].heroID, BonusType.PlayerDamage, 0.05f, 400, unitsList[19].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[19].heroID, BonusType.AllGold, 0.1f, 800, unitsList[19].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[20].heroID, BonusType.HeroDamage, 2.0f, 10, unitsList[20].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[20].heroID, BonusType.PlayerDamage, 0.05f, 25, unitsList[20].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[20].heroID, BonusType.AllDamage, 0.1f, 50, unitsList[20].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[20].heroID, BonusType.CriticalChance, 0.02f, 100, unitsList[20].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[20].heroID, BonusType.AllDamage, 0.1f, 200, unitsList[20].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[20].heroID, BonusType.ChestGold, 0.2f, 400, unitsList[20].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[20].heroID, BonusType.HeroDamage, 100.0f, 800, unitsList[20].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[21].heroID, BonusType.HeroDamage, 2.5f, 10, unitsList[21].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[21].heroID, BonusType.HeroDamage, 7.5f, 25, unitsList[21].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[21].heroID, BonusType.AllDamage, 0.1f, 50, unitsList[21].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[21].heroID, BonusType.HeroDamage, 5.0f, 100, unitsList[21].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[21].heroID, BonusType.AllDamage, 0.1f, 200, unitsList[21].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[21].heroID, BonusType.CriticalDamage, 0.3f, 400, unitsList[21].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[21].heroID, BonusType.AllDamage, 0.2f, 800, unitsList[21].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[22].heroID, BonusType.HeroDamage, 3.0f, 10, unitsList[22].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[22].heroID, BonusType.HeroDamage, 8.0f, 25, unitsList[22].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[22].heroID, BonusType.PlayerDamageDPS, 0.01f, 50, unitsList[22].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[22].heroID, BonusType.CriticalDamage, 0.2f, 100, unitsList[22].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[22].heroID, BonusType.PlayerDamage, 0.05f, 200, unitsList[22].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[22].heroID, BonusType.CriticalChance, 0.02f, 400, unitsList[22].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[22].heroID, BonusType.HeroDamage, 100.0f, 800, unitsList[22].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[23].heroID, BonusType.HeroDamage, 2.0f, 10, unitsList[23].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[23].heroID, BonusType.HeroDamage, 5.0f, 25, unitsList[23].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[23].heroID, BonusType.HeroDamage, 12.0f, 50, unitsList[23].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[23].heroID, BonusType.AllGold, 0.15f, 100, unitsList[23].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[23].heroID, BonusType.ChestGold, 0.2f, 200, unitsList[23].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[23].heroID, BonusType.HeroDamage, 9.0f, 400, unitsList[23].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[23].heroID, BonusType.AllDamage, 0.15f, 800, unitsList[23].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[24].heroID, BonusType.PlayerDamage, 0.05f, 10, unitsList[24].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[24].heroID, BonusType.PlayerDamage, 0.05f, 25, unitsList[24].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[24].heroID, BonusType.PlayerDamageDPS, 0.01f, 50, unitsList[24].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[24].heroID, BonusType.AllDamage, 0.1f, 100, unitsList[24].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[24].heroID, BonusType.AllGold, 0.15f, 200, unitsList[24].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[24].heroID, BonusType.CriticalChance, 0.02f, 400, unitsList[24].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[24].heroID, BonusType.HeroDamage, 150.0f, 800, unitsList[24].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[25].heroID, BonusType.HeroDamage, 3.5f, 10, unitsList[25].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[25].heroID, BonusType.HeroDamage, 6.5f, 25, unitsList[25].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[25].heroID, BonusType.PlayerDamageDPS, 0.01f, 50, unitsList[25].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[25].heroID, BonusType.AllDamage, 0.05f, 100, unitsList[25].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[25].heroID, BonusType.AllDamage, 0.1f, 200, unitsList[25].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[25].heroID, BonusType.AllDamage, 0.05f, 400, unitsList[25].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[25].heroID, BonusType.AllGold, 0.15f, 800, unitsList[25].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[26].heroID, BonusType.HeroDamage, 3.0f, 10, unitsList[26].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[26].heroID, BonusType.HeroDamage, 7.0f, 25, unitsList[26].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[26].heroID, BonusType.AllDamage, 0.1f, 50, unitsList[26].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[26].heroID, BonusType.AllDamage, 0.05f, 100, unitsList[26].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[26].heroID, BonusType.CriticalChance, 0.02f, 200, unitsList[26].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[26].heroID, BonusType.CriticalDamage, 0.3f, 400, unitsList[26].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[26].heroID, BonusType.ChestGold, 0.2f, 800, unitsList[26].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[27].heroID, BonusType.HeroDamage, 3.5f, 10, unitsList[27].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[27].heroID, BonusType.AllDamage, 0.1f, 25, unitsList[27].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[27].heroID, BonusType.HeroDamage, 4.0f, 50, unitsList[27].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[27].heroID, BonusType.HeroDamage, 6.0f, 100, unitsList[27].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[27].heroID, BonusType.CriticalDamage, 0.2f, 200, unitsList[27].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[27].heroID, BonusType.CriticalChance, 0.03f, 400, unitsList[27].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[27].heroID, BonusType.AllDamage, 0.15f, 800, unitsList[27].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[28].heroID, BonusType.HeroDamage, 3.3f, 10, unitsList[28].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[28].heroID, BonusType.HeroDamage, 5.5f, 25, unitsList[28].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[28].heroID, BonusType.AllGold, 0.1f, 50, unitsList[28].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[28].heroID, BonusType.PlayerDamage, 0.1f, 100, unitsList[28].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[28].heroID, BonusType.AllGold, 0.2f, 200, unitsList[28].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[28].heroID, BonusType.AllDamage, 0.1f, 400, unitsList[28].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[28].heroID, BonusType.AllGold, 0.3f, 800, unitsList[28].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[29].heroID, BonusType.HeroDamage, 10.0f, 10, unitsList[29].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[29].heroID, BonusType.PlayerDamage, 0.2f, 25, unitsList[29].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[29].heroID, BonusType.PlayerDamageDPS, 0.05f, 50, unitsList[29].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[29].heroID, BonusType.AllGold, 0.2f, 100, unitsList[29].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[29].heroID, BonusType.AllDamage, 0.1f, 200, unitsList[29].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[29].heroID, BonusType.AllDamage, 0.2f, 400, unitsList[29].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[29].heroID, BonusType.AllDamage, 0.3f, 800, unitsList[29].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[30].heroID, BonusType.HeroDamage, 9.0f, 10, unitsList[30].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[30].heroID, BonusType.HeroDamage, 20.0f, 25, unitsList[30].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[30].heroID, BonusType.CriticalChance, 0.01f, 50, unitsList[30].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[30].heroID, BonusType.PlayerDamage, 0.6f, 100, unitsList[30].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[30].heroID, BonusType.ChestGold, 0.25f, 200, unitsList[30].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[30].heroID, BonusType.AllDamage, 0.1f, 400, unitsList[30].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[30].heroID, BonusType.AllGold, 0.15f, 800, unitsList[30].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[31].heroID, BonusType.HeroDamage, 0.4f, 10, unitsList[31].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[31].heroID, BonusType.HeroDamage, 0.2f, 25, unitsList[31].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[31].heroID, BonusType.AllGold, 0.25f, 50, unitsList[31].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[31].heroID, BonusType.PlayerDamage, 0.6f, 100, unitsList[31].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[31].heroID, BonusType.CriticalChance, 0.02f, 200, unitsList[31].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[31].heroID, BonusType.AllDamage, 0.3f, 400, unitsList[31].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[31].heroID, BonusType.AllDamage, 0.1f, 800, unitsList[31].GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(unitsList[32].heroID, BonusType.HeroDamage, 20.0f, 10, unitsList[32].GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(unitsList[32].heroID, BonusType.PlayerDamage, 0.2f, 25, unitsList[32].GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(unitsList[32].heroID, BonusType.PlayerDamageDPS, 0.01f, 50, unitsList[32].GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(unitsList[32].heroID, BonusType.AllGold, 0.2f, 100, unitsList[32].GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(unitsList[32].heroID, BonusType.AllDamage, 0.2f, 200, unitsList[32].GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(unitsList[32].heroID, BonusType.AllDamage, 0.3f, 400, unitsList[32].GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(unitsList[32].heroID, BonusType.AllDamage, 0.4f, 800, unitsList[32].GetUpgradeCostByLevel(800) * 10));

            skillList[0].isUnlocked = true;
            skillList[2].isUnlocked = true;
            System.Console.WriteLine(GetBonusArtifactDamage());

        }

        public void UpdateBonuses()
        {

        }

        

        public override void Update()
        {
           
            base.Update();
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
    }
}
