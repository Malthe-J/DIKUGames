using DIKUArcade;
using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Physics;

namespace Breakout{
    public class Ball{

        private DynamicShape shape;
        private Entity entity;
        public Ball(DynamicShape shape, IBaseImage image)
        {
            entity = new Entity(shape, image);
            this.shape = shape;
        }

        public void Start()
        {
            shape.Direction.X = -0.01f;
            shape.Direction.Y = 0.01f;
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
                entity.DeleteEntity();
            shape.Move();
        }

        public void Render()
        {
            entity.RenderEntity();
        }

        public void CollideWithPlayer(Player player)
        {
            if (CollisionDetection.Aabb(shape, player.GetShape()).Collision)
            {
                shape.Direction.Y *= -1.0f;
            }
        }

        public void CollideWithBlock(EntityContainer<Block> blocks)
        {
            blocks.Iterate(block => {
                if (CollisionDetection.Aabb(shape, block.GetShape()).Collision)
                {
                    switch (CollisionDetection.Aabb(shape, block.GetShape()).CollisionDir)
                    {
                        case CollisionDirection.CollisionDirLeft:
                        {
                            shape.Direction.X *= -1.0f;
                            break;
                        }
                        case CollisionDirection.CollisionDirRight:
                        {
                            shape.Direction.X *= -1.0f;
                            break;
                        }
                        case CollisionDirection.CollisionDirUp:
                        {
                            shape.Direction.Y *= -1.0f;
                            break;
                        }
                        case CollisionDirection.CollisionDirDown:
                        {
                            shape.Direction.Y *= -1.0f;
                            break;
                        }
                    }
                    block.HealthDown();
                }
                });
        }
    }
}