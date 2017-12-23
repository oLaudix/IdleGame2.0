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
        public MainScene() : base()
        {
            AddGraphic(background);
            Add(player);







            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 1.0f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 2.0f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.2f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalDamage, 0.2f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 20.0f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.5f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 200.0f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamage, 0.2f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 2.0f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 20.0f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamageDPS, 0.01f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.2f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.2f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 200.0f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 3.0f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.2f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.2f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamageDPS, 0.01f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.ChestGold, 0.4f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalChance, 0.02f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.6f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 1.0f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 8.0f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 6.0f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 5.0f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalDamage, 0.5f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.2f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.ChestGold, 0.2f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 3.0f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.1f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamageDPS, 0.01f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.15f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.ChestGold, 0.2f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamage, 0.05f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 100.0f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 2.0f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 7.0f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.1f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.2f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalDamage, 0.05f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalChance, 0.02f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 100.0f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 2.0f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.05f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.07f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 0.6f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamage, 0.05f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.ChestGold, 0.2f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.3f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 2.0f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.1f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamageDPS, 0.01f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.15f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.ChestGold, 0.2f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.ChestGold, 19.0f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.2f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 1.5f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.05f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.3f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalDamage, 0.05f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 50.0f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.25f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 100.0f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 1.5f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalChance, 0.01f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.05f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.15f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.ChestGold, 0.2f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.ChestGold, 0.25f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 0.15f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 2.0f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 7.5f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamage, 0.05f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamageDPS, 0.01f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.15f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalChance, 0.25f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 3.8f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 2.5f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 13.0f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.05f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalDamage, 0.05f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamageDPS, 0.01f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.1f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.2f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 1.5f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 8.5f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamage, 0.05f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.2f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.3f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalDamage, 0.05f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 15.0f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 2.0f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 8.0f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamageDPS, 0.01f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 4.0f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.1f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalDamage, 0.1f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.1f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 3.0f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.1f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.05f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalChance, 0.02f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalDamage, 0.15f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.ChestGold, 0.2f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 100.0f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 3.5f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.ChestGold, 0.25f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.20f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.05f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.07f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.15f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.2f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 1.5f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 9.0f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.1f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.1f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamage, 0.05f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalDamage, 0.1f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.25f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 4.0f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 5.0f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.05f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 4.5f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamage, 0.05f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.ChestGold, 0.2f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.15f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 2.0f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 10.0f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamageDPS, 0.01f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamage, 0.05f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.1f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.1f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.1f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 2.5f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 6.0f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalDamage, 0.2f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 4.5f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamageDPS, 0.01f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamage, 0.05f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.1f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 2.0f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamage, 0.05f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.1f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalChance, 0.02f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.1f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.ChestGold, 0.2f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 100.0f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 2.5f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 7.5f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.1f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 5.0f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.1f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalDamage, 0.3f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.2f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 3.0f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 8.0f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamageDPS, 0.01f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalDamage, 0.2f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamage, 0.05f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalChance, 0.02f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 100.0f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 2.0f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 5.0f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 12.0f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.15f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.ChestGold, 0.2f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 9.0f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.15f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamage, 0.05f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamage, 0.05f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamageDPS, 0.01f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.1f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.15f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalChance, 0.02f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 150.0f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 3.5f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 6.5f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamageDPS, 0.01f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.05f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.1f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.05f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.15f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 3.0f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 7.0f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.1f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.05f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalChance, 0.02f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalDamage, 0.3f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.ChestGold, 0.2f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 3.5f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.1f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 4.0f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 6.0f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalDamage, 0.2f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalChance, 0.03f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.15f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 3.3f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 5.5f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.1f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamage, 0.1f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.2f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.1f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.3f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 10.0f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamage, 0.2f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamageDPS, 0.05f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.2f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.1f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.2f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.3f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 9.0f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 20.0f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalChance, 0.01f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamage, 0.6f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.ChestGold, 0.25f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.1f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.15f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 0.4f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 0.2f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.25f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamage, 0.6f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.CriticalChance, 0.02f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.3f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.1f, 800, tmp.GetUpgradeCostByLevel(800) * 10));

            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.HeroDamage, 20.0f, 10, tmp.GetUpgradeCostByLevel(10) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamage, 0.2f, 25, tmp.GetUpgradeCostByLevel(25) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.PlayerDamageDPS, 0.01f, 50, tmp.GetUpgradeCostByLevel(50) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllGold, 0.2f, 100, tmp.GetUpgradeCostByLevel(100) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.2f, 200, tmp.GetUpgradeCostByLevel(200) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.3f, 400, tmp.GetUpgradeCostByLevel(400) * 10));
            skillList.Add(new UnitSkill(skillList.Count+1, BonusType.AllDamage, 0.4f, 800, tmp.GetUpgradeCostByLevel(800) * 10));



        }

        public override void Update()
        {
           
            base.Update();
        }
    }
}
