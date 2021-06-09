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
        public PowerUp(DynamicShape shape, string filepath) : base(shape, new Image(filepath)) {
            shouldShow = false;
            dshape = shape;
        }
        
        /// <summary>
        /// This function sets a bool field to true so the PowerUp will be rendered and will change position.
        /// </summary>
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

        /// <summary>
        /// This function is a virtual function so
        /// the subclasses can  override it.
        /// </summary>
        public virtual void AddEffect() {

        }

        public DynamicShape GetDynamicShape() {
            return Shape.AsDynamicShape();
        }
    }
}