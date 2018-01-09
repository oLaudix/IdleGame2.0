using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace IdleGame
{
    class ActiveSkill : Entity
    {
        public bool activated = false;
        public int lvlreq;
        public int spacing;
        public string font = "Assets/Fonts/trench100free.ttf";
        public Image active_image;
        public Image inactive_image;
        public Image can_buy;
        public Image infoBackground;
        public Text InfoText;
        public Text skillName;
        public Text durationText;
        public string name;
        public int level;
        public int magnitude;
        public double cost;
        public int TotalCooldown;
        public int cooldown;
        public int duration;
        public bool active = false;
        public MainScene scene = (MainScene)MainScene.Instance;
        public ActiveSkill()
        {
            infoBackground = new Image("Assets/Img/artifact_menu.png");
            infoBackground.SetPosition(-5, 50);
            infoBackground.Visible = false;
            AddGraphic(infoBackground);
            CreateText(ref InfoText, "", 20, new Vector2(-5 + 50, 50 + 50));
            CreateText(ref skillName, this.name, 40, new Vector2(-5 + 50, 50));
            can_buy = new Image("Assets/Img/Gui/icon_can_buy.png");
            this.level = 0;
        }
        public double GetNextUpgradeCost(int req_lvl, int spcing)
        {
            double num = (25 * Math.Pow((double)1.075f, req_lvl + (this.level * spcing))) * 3;
            return (num * (1.0 - scene.Bonuses[BonusType.UpgradeCost]));
        }

        public void UpdateStats()
        {
            this.magnitude = GetMagnitude();
            this.cost = GetNextUpgradeCost(lvlreq, spacing);
            this.TotalCooldown = GetTotalCooldown();
            this.duration = GetDuration();
        }

        public override void Update()
        {
            if (this.Timer % 20 == 0)
            {
                if (activated)
                {
                    durationText.String = "" + duration / 60;
                    durationText.CenterTextOrigin();
                }
                else
                    durationText.String = "";
                if (scene.player.gold < this.cost)
                    can_buy.Visible = false;
                else
                    can_buy.Visible = true;
                if (this.cooldown > 0)
                {
                    inactive_image.Visible = true;
                    inactive_image.ClippingRegion = new Rectangle(0, inactive_image.Height - (int)Math.Ceiling((inactive_image.Height * ((double)cooldown / TotalCooldown))), inactive_image.Width, inactive_image.Height);
                }
                else if (level == 0)
                    inactive_image.Visible = true;
                else if (this.cooldown <= 0)
                {
                    inactive_image.Visible = false;
                }
            }
            if (MouseHover())
            {
                if (Input.MouseButtonPressed(MouseButton.Left))
                {
                    if (scene.player.gold >= this.cost)
                    {
                        scene.player.gold -= this.cost;
                        UpgradeSkill();
                    }
                }
                infoBackground.Visible = true;
                skillName.Visible = true;
                InfoText.Visible = true;
            }
            else
            {
                infoBackground.Visible = false;
                skillName.Visible = false;
                InfoText.Visible = false;
            }
            base.Update();
        }

        public virtual int GetTotalCooldown()
        {
            return 0;
        }

        public virtual int GetMagnitude()
        {
            return 0;
        }

        public virtual int GetDuration()
        {
            return 0;
        }

        public void UpgradeSkill()
        {
            this.level++;
            UpdateStats();
            scene.needUpdate = true;
        }

        public bool MouseHover()
        {
            if (X < Input.MouseX && X + active_image.Width > Input.MouseX)
            {
                if (Y < Input.MouseY && Y + active_image.Height > Input.MouseY)
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        public void CreateText(ref Text t, string text, int size, Vector2 Pos)
        {
            t = new Text(text, this.font, size);
            this.AddGraphic(t);
            //t.CenterOrigin();
            FormatText(t);
            t.SetPosition(Pos.X, Pos.Y);
            t.Visible = false;
        }

        public void FormatText(Text Text)
        {
            Text.OutlineThickness = 2;
            Text.TextStyle = TextStyle.Bold;
            Text.Color = Color.Yellow;
            Text.OutlineColor = Color.Black;
            //Text.CenterTextOriginX();
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
    }

    class Barrage : ActiveSkill
    {
        int waiting_for_rockets = 207;
        int number_of_rockets = 100;
        int interval_between_rockets = 2;
        int stage = 0;
        int counter = 0;
        int hits = 0;
        public Barrage()
        {
            this.active_image = new Image("Assets/Img/Gui/icon_barrage_active.png");
            this.inactive_image = new Image("Assets/Img/Gui/icon_barrage_inactive.png");
            AddGraphic(can_buy);
            can_buy.SetPosition(X - 1, Y - 1);
            AddGraphic(active_image);
            AddGraphic(inactive_image);
            SetPosition(1920/2, 1080 - 261 - 50);
            this.name = "Barrage";
            this.magnitude = 70 * (1 + this.level);
            this.cost = GetNextUpgradeCost(50, 160);
            this.cooldown = 0;
            this.TotalCooldown = 10 * 60 * 60;
            this.duration = 0;
            this.lvlreq = 50;
            this.spacing = 160;
            scene.Add(this);
            skillName.String = this.name;
            CreateText(ref durationText, "", 20, new Vector2(0, 0));
        }

        public override int GetTotalCooldown()
        {
            return (int)(this.TotalCooldown * (1 - scene.Bonuses[BonusType.HeavenlyStrikeCooldown]));
        }

        public override int GetDuration()
        {
            return 0;
        }

        public override int GetMagnitude()
        {
            return 70 * (1 + this.level);
        }
        public override void Update()
        {
            if (this.Timer % 20 == 0)
            {
                InfoText.String = "Cost: " + FormatNumber(this.cost) +
                    "\nDeals Player damage x " + this.magnitude +
                    "\nLevel: " + (level > 0 ? "" + level : "locked") +
                    "\nDuration: " + this.duration / 60 +
                    "\nCooldown: " + ((cooldown > 0) ? (cooldown / 60 / 60 + "m") + (cooldown / 60 % 60) + "s" : "0") +
                    "\nTotal Cooldown: " + (TotalCooldown / 60 / 60 + "m") + (TotalCooldown / 60 % 60) +
                    "\n\nTo use press 1.";
                //Console.WriteLine("CD: " + this.cooldown);
            }
            if (cooldown > 0)
            {
                cooldown--;
            }
            if (Input.KeyPressed(Key.Num1))
            {
                if (cooldown == 0 && stage == 0 && level > 0)
                {
                    stage = 1;
                    cooldown = TotalCooldown;
                }
            }
            if (stage == 1)
            {
                for (int a = 0; a < number_of_rockets; a++)
                    new Bradley_rocket(-200, 500, interval_between_rockets * a, false);
                stage = 2;
            }
            else if (stage == 2)
            {
                waiting_for_rockets--;
                if (waiting_for_rockets == 0)
                {
                    stage = 3;
                }
            }
            else if (stage == 3)
            {
                if (counter % interval_between_rockets == 0)
                {
                    if (scene.enemyList.Count != 0)
                    {
                        scene.enemyList[scene.random.Next(0, scene.enemyList.Count)].GetDamage(scene.player.currentDamage * this.magnitude / number_of_rockets);
                        hits++;
                    }
                    if (hits == number_of_rockets)
                    {
                        stage = 0;
                        counter = -1;
                        hits = 0;
                    }
                }
                counter++;
            }
            base.Update();
        }
    }

    class Clone : ActiveSkill
    {
        public Sound Shooting = new Sound("Assets/Sounds/minigun2.ogg") { Loop = true };
        public Sound dying = new Sound("Assets/Sounds/clone_dying.ogg") { Loop = false };
        public int runtime = 0;
        public enum Animation
        {
            Idle,
            IdleToShooting,
            ShootingToIdle,
            Dead,
            Deactivating,
            Activating,
            Shoot
        }
        public Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/clone.png", 145, 61);
        public Clone()
        {
            this.lvlreq = 100;
            this.spacing = 100;
            spritemap.Add(Animation.Idle, "0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15", 4);
            spritemap.Add(Animation.Activating, "16-31", 4).NoRepeat();
            spritemap.Add(Animation.Deactivating, "31-16", 4).NoRepeat();
            spritemap.Add(Animation.Dead, "16", 4).NoRepeat();
            spritemap.Add(Animation.IdleToShooting, "32-35", 2).NoRepeat();
            spritemap.Add(Animation.ShootingToIdle, "35-32", 2).NoRepeat();
            spritemap.Add(Animation.Shoot, "36-40", 2);
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Dead);
            AddGraphic(spritemap);
            //spritemap.SetPosition(spritemap.X + 920, spritemap.Y + 660);
            spritemap.SetPosition(0, -100);
            this.active_image = new Image("Assets/Img/Gui/icon_clone_active.png");
            this.inactive_image = new Image("Assets/Img/Gui/icon_clone_inactive.png");
            AddGraphic(can_buy);
            infoBackground.SetPosition(infoBackground.X - 51, infoBackground.Y);
            InfoText.SetPosition(InfoText.X - 51, InfoText.Y);
            skillName.SetPosition(skillName.X - 51, skillName.Y);
            can_buy.SetPosition(X - 1, Y - 1);
            AddGraphic(active_image);
            AddGraphic(inactive_image);
            SetPosition(1920 / 2 + 51, 1080 - 261 - 50);
            this.name = "Unit 02";
            this.magnitude = (3 * this.level) + 4;
            this.cost = GetNextUpgradeCost(100, 100);
            this.cooldown = 0;
            this.TotalCooldown = 10 * 60 * 60;
            this.duration = 30 * 60;
            scene.Add(this);
            skillName.String = this.name;
            CreateText(ref durationText, "test", 20, new Vector2(0, 0));
            durationText.Visible = true;
            durationText.CenterTextOrigin();
            durationText.SetPosition(25, 25);
        }

        public override int GetTotalCooldown()
        {
            return (int)(this.TotalCooldown * (1 - scene.Bonuses[BonusType.ShadowCloneCooldown]));
        }

        public override int GetMagnitude()
        {
            return (3 * this.level) + 4;
        }

        public override int GetDuration()
        {
            return (int)(this.duration * (1 + scene.Bonuses[BonusType.ShadowCloneDuration]));
        }

        public override void Update()
        {
            if (this.Timer % 20 == 0)
            {
                InfoText.String = "Cost: " + FormatNumber(this.cost) +
                    "\nDeals Player damage " + this.magnitude + " times per second" +
                    "\nLevel: " + (level > 0 ? "" + level : "locked") +
                    "\nDuration: " + this.duration / 60 +
                    "\nCooldown: " + ((cooldown > 0) ? (cooldown / 60 / 60 + "m") + (cooldown / 60 % 60) + "s" : "0") +
                    "\nTotal Cooldown: " + (TotalCooldown / 60 / 60 + "m") + (TotalCooldown / 60 % 60) +
                    "\n\nTo use press 2.";
                //Console.WriteLine("CD: " + this.cooldown);
            }
            if (cooldown > 0)
            {
                cooldown--;
            }
            if (Input.KeyPressed(Key.Num2))
            {
                if (cooldown == 0 && level > 0)
                {
                    //cooldown = TotalCooldown;
                    activated = true;

                }
            }
            if (cooldown == 0)
            {
                runtime--;
                if (spritemap.CurrentAnim == Animation.Dead && level > 0)
                {
                    spritemap.Play(Animation.Activating);
                    runtime = (int)spritemap.Anim(Animation.Activating).TotalDuration;
                }
                if (spritemap.CurrentAnim == Animation.Activating && runtime == 0)
                {
                    spritemap.Play(Animation.Idle);
                }
                if (spritemap.CurrentAnim == Animation.Idle && activated)
                {
                    Shooting.Play();
                    spritemap.Play(Animation.Shoot);
                    cooldown = TotalCooldown;
                }
            }
            else
            {
                if (activated)
                {
                    if (scene.enemyList.Count != 0)
                        scene.enemyList[scene.random.Next(0, scene.enemyList.Count)].GetDamage(scene.player.GetPlayerAttackDamageByLevel(scene.player.level) * magnitude / 60);
                    duration--;
                }
                if (duration <= 0)
                {
                    Shooting.Stop();
                    dying.Play();
                    spritemap.Play(Animation.Deactivating);
                    activated = false;
                    this.duration = 30 * 60;
                }
            }
            base.Update();
        }
    }

    class CriticalStrike : ActiveSkill
    {
        public CriticalStrike()
        {
            this.lvlreq = 200;
            this.spacing = 140;
            this.active_image = new Image("Assets/Img/Gui/icon_perfectaim_active.png");
            this.inactive_image = new Image("Assets/Img/Gui/icon_perfectaim_inactive.png");
            AddGraphic(can_buy);
            can_buy.SetPosition(X - 1, Y - 1);
            AddGraphic(active_image);
            AddGraphic(inactive_image);
            SetPosition(1920 / 2 + 102, 1080 - 261 - 50);
            infoBackground.SetPosition(infoBackground.X - 51 * 2, infoBackground.Y);
            InfoText.SetPosition(InfoText.X - 51 * 2, InfoText.Y);
            skillName.SetPosition(skillName.X - 51 * 2, skillName.Y);
            this.name = "Perfect Aim";
            this.magnitude = (3 * this.level) + 14;
            this.cost = GetNextUpgradeCost(this.lvlreq, this.spacing);
            this.cooldown = 0;
            this.TotalCooldown = 30 * 60 * 60;
            this.duration = 30 * 60;
            scene.Add(this);
            skillName.String = this.name;
            CreateText(ref durationText, "test", 20, new Vector2(0, 0));
            durationText.Visible = true;
            durationText.CenterTextOrigin();
            durationText.SetPosition(25, 25);
        }

        public override int GetTotalCooldown()
        {
            return (int)(this.TotalCooldown * (1 - scene.Bonuses[BonusType.CriticalStrikeCooldown]));
        }

        public override int GetDuration()
        {
            return (int)(this.duration * (1 + scene.Bonuses[BonusType.CriticalStrikeDuration]));
        }

        public override int GetMagnitude()
        {
            return (3 * this.level) + 14;
        }
        public override void Update()
        {
            if (this.Timer % 20 == 0)
            {
                InfoText.String = "Cost: " + FormatNumber(this.cost) +
                    "\nIncrease Critical Strike Chance by " + this.magnitude + "%" +
                    "\nLevel: " + (level > 0 ? "" + level : "locked") +
                    "\nDuration: " + this.duration / 60 +
                    "\nCooldown: " + ((cooldown > 0) ? (cooldown / 60 / 60 + "m") + (cooldown / 60 % 60) + "s" : "0") +
                    "\nTotal Cooldown: " + (TotalCooldown / 60 / 60 + "m") + (TotalCooldown / 60 % 60) + 
                    "\n\nTo use press 3.";
                //Console.WriteLine("CD: " + this.cooldown);
            }
            if (cooldown > 0)
            {
                cooldown--;
            }
            if (activated)
            {
                duration--;
                scene.player.critChance = 0.01 + ((float)magnitude / 100);
            }
            if (duration <= 0)
            {
                scene.player.critChance = 0.01;
                activated = false;
                this.duration = 30 * 60;
            }
            if (Input.KeyPressed(Key.Num3))
            {
                if (cooldown == 0 && level > 0)
                {
                    cooldown = TotalCooldown;
                    activated = true;
                    if (scene.player.Shooting.IsPlaying)
                    {
                        scene.player.Shooting.Stop();
                        scene.player.Shooting.Pitch += 2;
                        scene.player.Shooting.Play();
                    }
                    else
                        scene.player.Shooting.Pitch += 2;

                }
            }
            base.Update();
        }
    }

    class Speach : ActiveSkill
    {
        public Speach()
        {
            this.lvlreq = 300;
            this.spacing = 110;
            this.active_image = new Image("Assets/Img/Gui/icon_highmorale_active.png");
            this.inactive_image = new Image("Assets/Img/Gui/icon_highmorale_inactive.png");
            AddGraphic(can_buy);
            can_buy.SetPosition(X - 1, Y - 1);
            AddGraphic(active_image);
            AddGraphic(inactive_image);
            SetPosition(1920 / 2 + 51*3, 1080 - 261 - 50);
            infoBackground.SetPosition(infoBackground.X - 51 * 3, infoBackground.Y);
            InfoText.SetPosition(InfoText.X - 51 * 3, InfoText.Y);
            skillName.SetPosition(skillName.X - 51 * 3, skillName.Y);
            this.name = "Increase Morale";
            this.magnitude = (50 * this.level) + 100;
            this.cost = GetNextUpgradeCost(this.lvlreq, this.spacing);
            this.cooldown = 0;
            this.TotalCooldown = 30 * 60 * 60;
            this.duration = 30 * 60;
            scene.Add(this);
            skillName.String = this.name;
            CreateText(ref durationText, "test", 20, new Vector2(0, 0));
            durationText.Visible = true;
            durationText.CenterTextOrigin();
            durationText.SetPosition(25, 25);
        }

        public override int GetTotalCooldown()
        {
            return (int)(this.TotalCooldown * (1 - scene.Bonuses[BonusType.WarCryCooldown]));
        }

        public override int GetDuration()
        {
            return (int)(this.duration * (1 + scene.Bonuses[BonusType.WarCryDuration]));
        }

        public override int GetMagnitude()
        {
            return (50 * this.level) + 100;
        }
        public override void Update()
        {
            if (this.Timer % 20 == 0)
            {
                InfoText.String = "Cost: " + FormatNumber(this.cost) +
                    "\nIncrease Damage by " + this.magnitude + "%" +
                    "\nLevel: " + (level > 0 ? "" + level : "locked") +
                    "\nDuration: " + this.duration / 60 +
                    "\nCooldown: " + ((cooldown > 0) ? (cooldown / 60 / 60 + "m") + (cooldown / 60 % 60) + "s" : "0") +
                    "\nTotal Cooldown: " + (TotalCooldown / 60 / 60 + "m") + (TotalCooldown / 60 % 60) +
                    "\n\nTo use press 4.";
                //Console.WriteLine("CD: " + this.cooldown);
            }
            if (cooldown > 0)
            {
                cooldown--;
            }
            if (activated)
            {
                duration--;
                foreach (var Unit in scene.unitsList)
                {
                    if (scene.enemyList.Count != 0)
                    {
                        scene.enemyList[scene.random.Next(0, scene.enemyList.Count)].GetDamage((Unit.GetDPSByLevel(Unit.level) / 60) * ((float)magnitude/100));
                    }
                }
            }
            if (duration <= 0)
            {
                activated = false;
                this.duration = 30 * 60;
            }
            if (Input.KeyPressed(Key.Num4))
            {
                if (cooldown == 0 && level > 0)
                {
                    cooldown = TotalCooldown;
                    activated = true;
                }
            }
            base.Update();
        }
    }

    class Overdrive : ActiveSkill
    {
        public Overdrive()
        {
            this.lvlreq = 400;
            this.spacing = 130;
            this.active_image = new Image("Assets/Img/Gui/icon_overdrive_active.png");
            this.inactive_image = new Image("Assets/Img/Gui/icon_overdrive_inactive.png");
            AddGraphic(can_buy);
            can_buy.SetPosition(X - 1, Y - 1);
            AddGraphic(active_image);
            AddGraphic(inactive_image);
            SetPosition(1920 / 2 + 51 * 4, 1080 - 261 - 50);
            infoBackground.SetPosition(infoBackground.X - 51 * 4, infoBackground.Y);
            InfoText.SetPosition(InfoText.X - 51 * 4, InfoText.Y);
            skillName.SetPosition(skillName.X - 51 * 4, skillName.Y);
            this.name = "Overdrive";
            this.magnitude = (30 * this.level) + 40;
            this.cost = GetNextUpgradeCost(this.lvlreq, this.spacing);
            this.cooldown = 0;
            this.TotalCooldown = 60 * 60 * 60;
            this.duration = 30 * 60;
            scene.Add(this);
            skillName.String = this.name;
            CreateText(ref durationText, "test", 20, new Vector2(0, 0));
            durationText.Visible = true;
            durationText.CenterTextOrigin();
            durationText.SetPosition(25, 25);
        }

        public override int GetTotalCooldown()
        {
            return (int)(this.TotalCooldown * (1 - scene.Bonuses[BonusType.BerserkerRageCooldown]));
        }

        public override int GetDuration()
        {
            return (int)(this.duration * (1 + scene.Bonuses[BonusType.BerserkerRageDuration]));
        }

        public override int GetMagnitude()
        {
            return (30 * this.level) + 40;
        }
        public override void Update()
        {
            if (this.Timer % 20 == 0)
            {
                InfoText.String = "Cost: " + FormatNumber(this.cost) +
                    "\nIncrease Player Damage by " + this.magnitude + "%" +
                    "\nLevel: " + (level > 0 ? "" + level : "locked") +
                    "\nDuration: " + this.duration / 60 +
                    "\nCooldown: " + ((cooldown > 0) ? (cooldown / 60 / 60 + "m") + (cooldown / 60 % 60) + "s" : "0") +
                    "\nTotal Cooldown: " + (TotalCooldown / 60 / 60 + "m") + (TotalCooldown / 60 % 60) +
                    "\n\nTo use press 5.";
                //Console.WriteLine("CD: " + this.cooldown);
            }
            if (cooldown > 0)
            {
                cooldown--;
            }
            if (activated)
            {
                duration--;
            }
            if (duration <= 0)
            {
                activated = false;
                scene.player.UpdatePlayerStats();
                this.duration = 30 * 60;
            }
            if (Input.KeyPressed(Key.Num5))
            {
                if (cooldown == 0 && level > 0)
                {
                    activated = true;
                    scene.player.UpdatePlayerStats();
                    cooldown = TotalCooldown;
                }
            }
            base.Update();
        }
    }

    class MoneyShot : ActiveSkill
    {
        public MoneyShot()
        {
            this.lvlreq = 500;
            this.spacing = 130;
            this.active_image = new Image("Assets/Img/Gui/icon_transmute_active.png");
            this.inactive_image = new Image("Assets/Img/Gui/icon_transmute_inactive.png");
            AddGraphic(can_buy);
            can_buy.SetPosition(X - 1, Y - 1);
            AddGraphic(active_image);
            AddGraphic(inactive_image);
            SetPosition(1920 / 2 + 51 * 5, 1080 - 261 - 50);
            infoBackground.SetPosition(infoBackground.X - 51 * 5, infoBackground.Y);
            InfoText.SetPosition(InfoText.X - 51 * 5, InfoText.Y);
            skillName.SetPosition(skillName.X - 51 * 5, skillName.Y);
            this.name = "Transmute";
            this.magnitude = (5 * this.level) + 10;
            this.cost = GetNextUpgradeCost(this.lvlreq, this.spacing);
            this.cooldown = 0;
            this.TotalCooldown = 60 * 60 * 60;
            this.duration = 30 * 60;
            scene.Add(this);
            skillName.String = this.name;
            CreateText(ref durationText, "test", 20, new Vector2(0, 0));
            durationText.Visible = true;
            durationText.CenterTextOrigin();
            durationText.SetPosition(25, 25);
        }

        public override int GetTotalCooldown()
        {
            return (int)(this.TotalCooldown * (1 - scene.Bonuses[BonusType.HandOfMidasCooldown]));
        }

        public override int GetMagnitude()
        {
            return (5 * this.level) + 10;
        }

        public override int GetDuration()
        {
            return (int)(this.duration * (1 + scene.Bonuses[BonusType.HandOfMidasDuration]));
        }

        public override void Update()
        {
            if (this.Timer % 20 == 0)
            {
                InfoText.String = "Cost: " + FormatNumber(this.cost) +
                    "\nGet " + this.magnitude + "% of money for each succesful hit" +
                    "\nLevel: " + (level > 0 ? "" + level : "locked") +
                    "\nDuration: " + this.duration / 60 + 
                    "\nCooldown: " + ((cooldown > 0) ? (cooldown / 60 / 60 + "m") + (cooldown / 60 % 60) + "s" : "0") +
                    "\nTotal Cooldown: " + (TotalCooldown / 60 / 60 + "m") + (TotalCooldown / 60 % 60) +
                    "\n\nTo use press 6.";
                //Console.WriteLine("CD: " + this.cooldown);
            }
            if (cooldown > 0)
            {
                cooldown--;
            }
            if (activated)
            {
                duration--;
            }
            if (duration <= 0)
            {
                activated = false;
                this.duration = 30 * 60;
            }
            if (Input.KeyPressed(Key.Num6))
            {
                if (cooldown == 0 && level > 0)
                {
                    activated = true;
                    cooldown = TotalCooldown;
                }
            }
            base.Update();
        }
    }
}
