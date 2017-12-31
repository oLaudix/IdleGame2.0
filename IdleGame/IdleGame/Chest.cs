using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace IdleGame
{
    class Chest : Entity
    {
        public enum Animation
        {
            Open,
            Close,
            Idle
        }
        double prize;
        MainScene scene = (MainScene)MainScene.Instance;
        public Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/chest.png", 31, 28);
        public Chest(float x, float y) : base(x, y)
        {
            SetHitbox(21, 23, ColliderTags.EnemyUnit);
            Hitbox.SetPosition(6, 5);
            this.prize = scene.stage.GetTreasureGold();
            spritemap.Add(Animation.Open, "6-9", 4).NoRepeat();
            spritemap.Add(Animation.Close, "9-6", 4).NoRepeat();
            spritemap.Add(Animation.Idle, "0-5", 4);
            spritemap.Play(Animation.Idle);
            AddGraphic(spritemap);
            scene.Add(this);
        }
        public override void Update()
        {
            if (Overlap(X, Y, ColliderTags.Crosshair) && Input.MouseButtonDown(MouseButton.Left))
            {
                scene.player.gold += this.prize;
                RemoveSelf();
            }
            else if (Overlap(X, Y, ColliderTags.Crosshair) && spritemap.CurrentAnim != Animation.Open)
            {
                spritemap.Play(Animation.Open);
            }
            else if (!Overlap(X, Y, ColliderTags.Crosshair) && spritemap.CurrentAnim == Animation.Open)
            {
                spritemap.Play(Animation.Idle);
            }
            base.Update();
        }
    }
}
