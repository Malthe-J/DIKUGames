using DIKUArcade;
using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Physics;

namespace Breakout{
    public class Ball : Entity{

        private DynamicShape shape;
        public Ball(DynamicShape shape, IBaseImage image) : base(shape, image)
        {
            this.shape = shape;
        }

        public void Start()
        {
            shape.Direction.X = -0.005f;
            shape.Direction.Y = 0.005f;
        }

        public void Move(){
            if (shape.Position.X + shape.Extent.X > 1.0f)
            {
                shape.Direction.X *= -1.0f;
            }
            if (shape.Position.X + shape.Extent.X < 0.0f + shape.Extent.X)
            {
                shape.Direction.X *= -1.0f;
            }
            if (shape.Position.Y + shape.Extent.Y > 1.0f)
            {
                shape.Direction.Y *= -1.0f;
            }

            if (shape.Position.Y <= 0.0f)
                DeleteEntity();
            shape.Move();
        }

        public void Render()
        {
            RenderEntity();
        }

        public void CollideWithPlayer(Player player)
        {
            if (CollisionDetection.Aabb(shape, player.GetShape()).Collision)
            {
                shape.Direction.Y *= -1.0f;
            }
        }

        public DynamicShape GetShape() {
            return shape;
        }
    }
}