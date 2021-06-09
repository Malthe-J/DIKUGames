using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Physics;

namespace Breakout.PowerUp{
    public class PowerUp : Entity{
        private bool shouldShow;
        private DynamicShape dshape;
        public PowerUp(DynamicShape shape, string filepath) : base(shape, new Image(filepath)) {
            shouldShow = false;
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
                if (Shape.Position.Y < 0.0f)
                {
                    DeleteEntity();
                    AddEffect();
                }
                Shape.Move();
            }
        }

        public Shape GetShape() {
            return Shape;
        }

        public void Delete(){
            DeleteEntity();
        }

        public virtual void AddEffect() {

        }

        public DynamicShape GetDynamicShape() {
            return Shape.AsDynamicShape();
        }
    }
}