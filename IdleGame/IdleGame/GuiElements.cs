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
        public Unit unit;
        public GuiElement(float x, float y, Unit unit) : base(x, y)
        {
            image = new Image(unit.icon);
            this.unit = unit;
            AddGraphic(image);
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
            CreateText(ref skills, "Skills:", 20, new Vector2(650/2, 60));
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
                "Level: " + element.unit.level + "\n" +
                "Cost: " + FormatNumber(element.unit.nextUpgradeCost) + "\n" +
                "Damage: " + FormatNumber(element.unit.currentDPS) + "\n" +
                "+Damage: " + FormatNumber(element.unit.nextLevelDPSDiff);
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
            if (Input.MouseButtonPressed(MouseButton.Left) && element.MouseHover())
            {
                //permanent = true;
                element.unit.UpgradeHero();
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
}
