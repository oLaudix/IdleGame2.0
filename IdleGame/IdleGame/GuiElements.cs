using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace IdleGame
{
    class GuiElement : Entity
    {
        Image image;
        public Image can_buy;
        public Unit unit;
        public GuiElement(float x, float y, Unit unit) : base(x, y)
        {
            can_buy = new Image("Assets/Img/Gui/icon_can_buy.png");
            image = new Image(unit.icon);
            this.unit = unit;
            AddGraphic(image);
            AddGraphic(can_buy);
            can_buy.SetPosition(can_buy.X-1, can_buy.Y-1);
            MainScene.Instance.Add(this);
            GuiMenu menu = new GuiMenu(955, 1080 - 261, "Assets/Img/bottom_menu.png", this);
        }

        public bool MouseHover()
        {
            if (X < Input.MouseX && X + image.Width > Input.MouseX)
            {
                if (Y < Input.MouseY && Y + image.Height > Input.MouseY)
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }
    }

    class GuiMenu : Entity
    {
        Text unitName;
        Text unitInfo;
        Text skills;
        Text skill1;
        Text skill2;
        Text skill3;
        Text skill4;
        Text skill5;
        Text skill6;
        Text skill7;
        List<Text> skillsInfo;
        bool permanent = false;
        Image image;
        Image icon;
        public GuiElement element;
        MainScene scene = (MainScene)MainScene.Instance;
        public string font = "Assets/Fonts/trench100free.ttf";
        public GuiMenu(float x, float y, string source, GuiElement entity) : base(x, y)
        {
            skillsInfo = new List<Text>();
            image = new Image(source);
            AddGraphic(image);
            icon = new Image(entity.unit.icon);
            icon.SetPosition(50, 30);
            AddGraphic(icon);
            element = entity;
            Visible = false;
            MainScene.Instance.Add(this);
            CreateText(ref unitName, entity.unit.name, 40, new Vector2(960 / 2, 0));
            CreateText(ref unitInfo, "", 30, new Vector2(50, 100));
            CreateText(ref skills, "Upgrades:", 20, new Vector2(850/2, 60));
            CreateText(ref skill1, entity.unit.unitSkills[0].description, 20, new Vector2(skills.X, skills.Y + (skills.FontSize * 1)));
            CreateText(ref skill2, entity.unit.unitSkills[1].description, 20, new Vector2(skills.X, skills.Y + (skills.FontSize * 2)));
            CreateText(ref skill3, entity.unit.unitSkills[2].description, 20, new Vector2(skills.X, skills.Y + (skills.FontSize * 3)));
            CreateText(ref skill4, entity.unit.unitSkills[3].description, 20, new Vector2(skills.X, skills.Y + (skills.FontSize * 4)));
            CreateText(ref skill5, entity.unit.unitSkills[4].description, 20, new Vector2(skills.X, skills.Y + (skills.FontSize * 5)));
            CreateText(ref skill6, entity.unit.unitSkills[5].description, 20, new Vector2(skills.X, skills.Y + (skills.FontSize * 6)));
            CreateText(ref skill7, entity.unit.unitSkills[6].description, 20, new Vector2(skills.X, skills.Y + (skills.FontSize * 7)));
            skillsInfo.Add(skill1);
            skillsInfo.Add(skill2);
            skillsInfo.Add(skill3);
            skillsInfo.Add(skill4);
            skillsInfo.Add(skill5);
            skillsInfo.Add(skill6);
            skillsInfo.Add(skill7);
            unitName.CenterTextOriginX();
            unitInfo.CenterTextOriginX();
        }

        public string FormatNumber(double number)
        {
            int exponent = 0;
            if (number < 1000)
                return Math.Round(number, 2).ToString("0.00");
            while (true)
            {
                if (number < 10)
                    return Math.Round(number, 2).ToString("0.00") + "e" + exponent;
                else
                {
                    number = number / 10;
                    exponent++;
                }
            }
        }
        public bool MouseHover()
        {
            if (X < Input.MouseX && X + image.Width > Input.MouseX)
            {
                if (Y < Input.MouseY && Y + image.Height > Input.MouseY)
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        public override void Update()
        {
            unitInfo.String = 
                "Level: " + element.unit.level + ((element.unit.GetNumLevelsToUnlockByGivenGoldAmount() > 0) ? " +" + element.unit.GetNumLevelsToUnlockByGivenGoldAmount().ToString() : "") + "\n" +
                "Cost: " + FormatNumber(element.unit.nextUpgradeCost) + "\n" +
                "Power: " + FormatNumber(element.unit.currentDPS) + "\n" +
                "+Power: " + FormatNumber(element.unit.nextLevelDPSDiff);
            for (var a = 0; a < 7; a++)
            {
                if (element.unit.unitSkills[a].isUnlocked)
                    skillsInfo[a].Color = Color.Yellow;
                else
                    skillsInfo[a].Color = Color.Gray;
            }

            if (!permanent)
            {
                if (element.MouseHover())
                    Visible = true;
                else
                    Visible = false;
            }
            if (element.unit.nextUpgradeCost <= scene.player.gold)
                element.can_buy.Visible = true;
            else
                element.can_buy.Visible = false;

            if (element.MouseHover())
            {
                if (Input.MouseButtonPressed(MouseButton.Left))
                {
                    if (element.unit.nextUpgradeCost <= scene.player.gold)
                    {
                        scene.player.gold -= element.unit.nextUpgradeCost;
                        element.unit.UpgradeHero();
                    }
                }
                else if (Input.MouseButtonPressed(MouseButton.Right))
                {
                    while (element.unit.nextUpgradeCost <= scene.player.gold)
                    {
                        if (element.unit.level + 1 == element.unit.nextSkillToUnlock.requiredLevel)
                        {
                            scene.player.gold -= element.unit.nextUpgradeCost;
                            element.unit.UpgradeHero();
                            break;
                        }
                        scene.player.gold -= element.unit.nextUpgradeCost;
                        element.unit.UpgradeHero();
                    }
                }
            }
            if (Input.MouseButtonPressed(MouseButton.Middle) && element.MouseHover())
            {
                permanent = true;
            }
            if (!element.MouseHover() && !MouseHover() && Input.MouseButtonPressed(MouseButton.Middle))
                permanent = false;
            base.Update();
        }

        public void FormatText(Text Text)
        {
            Text.OutlineThickness = 2;
            Text.TextStyle = TextStyle.Bold;
            Text.Color = Color.Yellow;
            Text.OutlineColor = Color.Black;
            //Text.CenterTextOriginX();
        }

        public void CreateText(ref Text t, string text, int size, Vector2 Pos)
        {
            t = new Text(text, this.font, size);
            this.AddGraphic(t);
            //t.CenterOrigin();
            FormatText(t);
            t.SetPosition(Pos.X, Pos.Y);
        }
    }

    class PlayerGui : Entity
    {
        Text unitName;
        Text unitInfo;
        List<Image> gearList;
        List<Text> skillsInfo;
        bool permanent = true;
        Image image;
        Image icon;
        public Image can_buy;
        public MyPlayer element;
        MainScene scene = (MainScene)MainScene.Instance;
        public string font = "Assets/Fonts/trench100free.ttf";
        public PlayerGui(float x, float y, string source, MyPlayer entity) : base(x, y)
        {
            gearList = new List<Image>();
            skillsInfo = new List<Text>();
            image = new Image(source);
            AddGraphic(image);
            can_buy = new Image("Assets/Img/Gui/icon_can_buy.png");
            icon = new Image("Assets/Img/Gui/icon_player.png");
            icon.SetPosition(50, 30);
            can_buy.SetPosition(49, 29);
            AddGraphic(icon);
            AddGraphic(can_buy);
            element = entity;
            Visible = true;
            MainScene.Instance.Add(this);
            CreateText(ref unitName, "Player", 40, new Vector2(960 / 2, 0));
            CreateText(ref unitInfo, "", 25, new Vector2(50, 80));
            unitName.CenterTextOriginX();
            unitInfo.CenterTextOriginX();
            foreach (var gear in scene.gearList)
            {
                //Console.WriteLine(gear.bonusType);
                this.gearList.Add(new Image(gear.icon));
                new GearGui(1920/2, 1080 - 261, gear, this.gearList[this.gearList.Count-1], this);
            }
            Vector2 Pos = new Vector2(850, 50);
            foreach (var gear in this.gearList)
            {
                AddGraphic(gear);
                gear.SetPosition(Pos.X, Pos.Y);
                Pos.X -= 51;
                if (Pos.X <= 450)
                {
                    Pos.Y += 51;
                    Pos.X = 850;
                    if (Pos.Y > 150)
                        Pos.X = 850 - 25;
                }
            }
            new PlayerInfo(1920 / 2, 1080 - 261 - 261, icon, this);
        }

        public string FormatNumber(double number)
        {
            int exponent = 0;
            if (number < 1000)
                return Math.Round(number, 2).ToString("0.00");
            while (true)
            {
                if (number < 10)
                    return Math.Round(number, 2).ToString("0.00") + "e" + exponent;
                else
                {
                    number = number / 10;
                    exponent++;
                }
            }
        }
        public bool MouseHover(Image imagee)
        {
            if (imagee.X + X < Input.MouseX && imagee.X + X + imagee.Width > Input.MouseX)
            {
                if (imagee.Y + Y < Input.MouseY && imagee.Y + Y + imagee.Height > Input.MouseY)
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        public override void Update()
        {
            unitInfo.String =
                "Level: " + element.level + ((element.GetNumLevelsToUnlockByGivenGoldAmount() > 0) ? " +" + element.GetNumLevelsToUnlockByGivenGoldAmount().ToString() : "") + "\n" +
                "Cost: " + FormatNumber(element.upgradeCost) + "\n" +
                "Power: " + FormatNumber(element.currentDamage) + "\n" +
                "+Power: " + FormatNumber(element.nextLevelDamageDiff) + "\n" +
                "Money: " + FormatNumber(element.gold) + "\n" +
                "Total Power: " + FormatNumber(scene.totalDPS);
            unitName.String = "Player - " + element.honor + " Honor";
            if (element.upgradeCost <= scene.player.gold)
                can_buy.Visible = true;
            else
                can_buy.Visible = false;

            if (MouseHover(icon))
            {
                if (Input.MouseButtonPressed(MouseButton.Left))
                {
                    if (element.upgradeCost <= scene.player.gold)
                    {
                        scene.player.gold -= element.upgradeCost;
                        element.UpgradePlayer();
                    }
                }
                else if (Input.MouseButtonPressed(MouseButton.Right))
                {
                    while (element.upgradeCost <= scene.player.gold)
                    {
                        scene.player.gold -= element.upgradeCost;
                        element.UpgradePlayer();
                    }
                }
            }
            base.Update();
        }

        public void FormatText(Text Text)
        {
            Text.OutlineThickness = 2;
            Text.TextStyle = TextStyle.Bold;
            Text.Color = Color.Yellow;
            Text.OutlineColor = Color.Black;
            //Text.CenterTextOriginX();
        }

        public void CreateText(ref Text t, string text, int size, Vector2 Pos)
        {
            t = new Text(text, this.font, size);
            this.AddGraphic(t);
            //t.CenterOrigin();
            FormatText(t);
            t.SetPosition(Pos.X, Pos.Y);
        }
    }

    class GearGui : Entity
    {
        Gear gearPiece;
        public string font = "Assets/Fonts/trench100free.ttf";
        Text gearName;
        Text gearInfo;
        Image parent;
        Image background;
        PlayerGui parentGui;
        MainScene scene = (MainScene)MainScene.Instance;
        public GearGui(float x, float y, Gear gearPiece, Image parent, PlayerGui parentGui) : base(x, y)
        {
            this.background = new Image("Assets/Img/artifact_menu.png");
            AddGraphic(background);
            this.gearPiece = gearPiece;
            this.parent = parent;
            this.parentGui = parentGui;
            Visible = true;
            MainScene.Instance.Add(this);
            CreateText(ref gearName, gearPiece.name, 40, new Vector2(0, 0));
            CreateText(ref gearInfo, "", 20, new Vector2(50, 100));
            gearName.SetPosition(250, 0);
            gearName.CenterTextOriginX();
            gearInfo.CenterTextOriginX();
        }

        public void CreateText(ref Text t, string text, int size, Vector2 Pos)
        {
            t = new Text(text, this.font, size);
            this.AddGraphic(t);
            //t.CenterOrigin();
            FormatText(t);
            t.SetPosition(Pos.X, Pos.Y);
        }

        public void FormatText(Text Text)
        {
            Text.OutlineThickness = 2;
            Text.TextStyle = TextStyle.Bold;
            Text.Color = Color.Yellow;
            Text.OutlineColor = Color.Black;
            //Text.CenterTextOriginX();
        }

        public override void Update()
        {
            gearInfo.String =
                "Cost: " + gearPiece.GetUpgradeCost() +
                "\nBonus to all power: " + Math.Round(gearPiece.GetDamageBonus() * 100) + "%" +
                "\n" + gearPiece.GetDescritopion() + "\n" + ((gearPiece.unlocked) ? "Level: " + gearPiece.level : "Locked") + 
                "\nBonus Damage Per Level: " + Math.Round(gearPiece.bonusPerLevel * 100) + "%" +
                "\nBonus Per Level: " + Math.Round(gearPiece.bonusPerLevel2 * 100) + "%";

            if (parentGui.MouseHover(parent))
                Visible = true;
            else
                Visible = false;

            if (parentGui.MouseHover(parent))
            {
                if (Input.MouseButtonPressed(MouseButton.Left))
                {
                    if (gearPiece.GetUpgradeCost() <= scene.player.honor)
                    {
                        scene.player.honor -= gearPiece.GetUpgradeCost();
                        gearPiece.UpgradeGear();
                    }
                }
            }
            base.Update();
        }
    }


    class PlayerInfo : Entity
    {
        public string font = "Assets/Fonts/trench100free.ttf";
        Text InfoText;
        Image parent;
        Image background;
        PlayerGui parentGui;
        MainScene scene = (MainScene)MainScene.Instance;
        public PlayerInfo(float x, float y, Image parent, PlayerGui parentGui) : base(x, y)
        {
            this.background = new Image("Assets/Img/artifact_menu.png");
            AddGraphic(background);
            this.parent = parent;
            this.parentGui = parentGui;
            Visible = true;
            MainScene.Instance.Add(this);
            //CreateText(ref gearName, gearPiece.name, 40, new Vector2(0, 0));
            CreateText(ref InfoText, "", 20, new Vector2(50, 25));
            //gearName.SetPosition(250, 0);
            //gearName.CenterTextOriginX();
            InfoText.CenterTextOriginX();
        }

        public void CreateText(ref Text t, string text, int size, Vector2 Pos)
        {
            t = new Text(text, this.font, size);
            this.AddGraphic(t);
            //t.CenterOrigin();
            FormatText(t);
            t.SetPosition(Pos.X, Pos.Y);
        }

        public void FormatText(Text Text)
        {
            Text.OutlineThickness = 2;
            Text.TextStyle = TextStyle.Bold;
            Text.Color = Color.Yellow;
            Text.OutlineColor = Color.Black;
            //Text.CenterTextOriginX();
        }

        public override void Update()
        {
            var num = 0;
            foreach (var unit in scene.unitsList)
            {
                num += unit.level;
            }
            InfoText.String =
                "Player Info:" +
                "\nTotal Hero Levels: " + num +
                "\nCritical Strike Chance: " + Math.Round((scene.Bonuses[BonusType.CriticalChance] + scene.player.critChance) * 1000) / 10 + "%" +
                "\nCritical Strike Damage Multiplier: " + Math.Round(((1 + scene.Bonuses[BonusType.CriticalDamage]) * scene.player.critMagnitude)) +
                "\nAll Money Bonus: " + Math.Round((scene.Bonuses[BonusType.AllGold]) * 1000) / 10 + "%" +
                "\nMoney for Kills Bonus: " + Math.Round((scene.Bonuses[BonusType.MonsterGold]) * 1000) / 10 + "%" +
                "\nMoney From Chests Bonus: " + Math.Round((scene.Bonuses[BonusType.ChestGold]) * 1000) / 10 + "%" +
                "\nChance for Chest: " + Math.Round((0.02 * (1f + scene.Bonuses[BonusType.ChestChance])) * 1000) / 10 + "%" +
                "\n\nHonor on Prestige: " + scene.player.GetPrestigeRelicCount() + " (Middle Mouse Button)";
            if (parentGui.MouseHover(parent))
                Visible = true;
            else
                Visible = false;

            if (parentGui.MouseHover(parent))
            {
                if (Input.MouseButtonPressed(MouseButton.Middle))
                {
                    scene.player.Prestige();
                }
            }
            base.Update();
        }
    }
}
