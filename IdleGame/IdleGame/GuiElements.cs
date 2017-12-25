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
        public GuiElement(float x, float y, string source) : base(x, y)
        {
            image = new Image(source);
            AddGraphic(image);
            MainScene.Instance.Add(this);
            HiddenGui menu = new HiddenGui(965, 800 - 261, "Assets/Img/bottom_menu.png", this);
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

    class HiddenGui : Entity
    {
        bool permanent = false;
        Image image;
        GuiElement element;
        public HiddenGui(float x, float y, string source, GuiElement entity) : base(x, y)
        {
            image = new Image(source);
            AddGraphic(image);
            element = entity;
            Visible = false;
            MainScene.Instance.Add(this);
            BuyButton button = new BuyButton(this.X + 50, this.Y + 50, this);
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
            if (!permanent)
            {
                if (element.MouseHover())
                    Visible = true;
                else
                    Visible = false;
            }
            if (Input.MouseButtonPressed(MouseButton.Left) && element.MouseHover())
            {
                permanent = true;
            }
            if (!element.MouseHover() && !MouseHover() && Input.MouseButtonPressed(MouseButton.Left))
                permanent = false;
            base.Update();
        }

    }

    class BuyButton : Entity
    {
        HiddenGui parent;
        MainScene scene = (MainScene)MainScene.Instance;
        Image active = new Image("Assets/Img/Gui/buy_button.png");
        Image inactive = new Image("Assets/Img/Gui/buy_button_inactive.png");
        public BuyButton(float x, float y, HiddenGui entity) : base(x, y)
        {
            parent = entity;
            AddGraphic(active);
            AddGraphic(inactive);
            //inactive.Visible = false;
            MainScene.Instance.Add(this);
        }
        public bool MouseHover()
        {
            if (X < Input.MouseX && X + active.Width > Input.MouseX)
            {
                if (Y < Input.MouseY && Y + active.Height > Input.MouseY)
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        private void OnClick()
        {
            if (Visible && MouseHover() && Input.MouseButtonPressed(MouseButton.Left))
            {
                scene.unitsList[0].level++;
            }
        }
        public override void Update()
        {

            if (scene.unitsList[0].GetUpgradeCostByLevel(scene.unitsList[0].level, scene.Bonuses[BonusType.UpgradeCost]) > scene.player.gold)
            {
                active.Visible = false;
                inactive.Visible = true;
            }
            else
            {
                active.Visible = true;
                inactive.Visible = false;
            }
            scene.player.gold = 999999999;
            Visible = parent.Visible;
            OnClick();
            base.Update();
        }
    }
}
