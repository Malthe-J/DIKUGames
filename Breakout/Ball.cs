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

        /// <summary>
        /// Sets the start direction
        /// </summary>
        public void Start()
        {
            shape.Direction.X = -0.01f;
            shape.Direction.Y = 0.01f;
        }

        /// <summary>
        /// This function checks whether the ball is hitting
        /// the window and if it hits we flipped the axis according 
        ///to the side of the window
        /// </summary>
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

        /// <summary>
        /// This function checks whether the ball has collide
        /// with the player and if it has the Y direction is flipped
        /// </summary>
        /// <param name="player"></param>
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