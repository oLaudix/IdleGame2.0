using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace IdleGame
{
    class Crosshair : Entity
    {
        PointCollider pointCollider;
        Image image = new Image("Assets/Img/crosshair2.png");
        public Crosshair()
        {
            pointCollider = new PointCollider(0, 0, ColliderTags.Crosshair);
            AddCollider(pointCollider);
            image.Scale = 0.1f;
            image.CenterOrigin();
            Layer = -1000;
            AddGraphic(image);
            MainScene.Instance.Add(this);
        }
        public override void Update()
        {
            SetPosition(Input.MouseX, Input.MouseY);
            base.Update();
        }
    }

}
