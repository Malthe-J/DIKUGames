using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Physics;

namespace Breakout.PowerUp{
    public class PowerUp : Entity{
        public static EntityContainer<PowerUp> PowerUpContainer = new EntityContainer<PowerUp>();
        private bool shouldShow;
        private DynamicShape dshape;
        public PowerUp(DynamicShape shape, string filepath) : base(shape, new Image(filepath)) {
            shouldShow = false;
            PowerUpContainer.AddEntity(this);
            dshape = shape;
        }
        public void ShouldShow() {
            shouldShow = true;
        }

        public void Render() {
            if (shouldShow) {
                RenderEntity();
            }
        }

        public void Update() {
            if (shouldShow) {
                Shape.Position.Y -= 0.005f;
                Shape.Move();
            }
        }

        public DynamicShape GetShape() {
            return Shape.AsDynamicShape();
        }

        public void Delete(){
            DeleteEntity();
        }

        public virtual void AddEffect() {

        }

        public void CollideWithPlayer(Player player) {
            if (Shape.Position == player.GetShape().Position)
                Console.WriteLine("Hej");
            if (CollisionDetection.Aabb(Shape.AsDynamicShape(), player.GetShape()).Collision)
            {
                Console.WriteLine("Hej");
                AddEffect();
                Delete();
            }
        }
    }
}