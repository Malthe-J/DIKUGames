using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.IO;
namespace Breakout.PowerUp{
    public class PowerUpBlock : Block{
        private PowerUp powerUp;
        public PowerUpBlock(StationaryShape shape, string filepath) : base(shape, filepath){
           RandomEffect(shape); 
        }

        public override void HealthDown()
        {
            base.HealthDown();
            if (Health <= 0)
                powerUp.ShouldShow();
        }

        public PowerUp GetPowerUp(){
            return powerUp;
        }

        private void RandomEffect(Shape shape){
            var rand = new System.Random();
            int f = rand.Next(1, 4); // random number for formation
            switch(f)
            {
                case 1:
                    powerUp = new ExtraLife(new DynamicShape(shape.Position, new Vec2F(0.06f, 0.06f)));
                    return;
                case 2:
                    powerUp = new ExtraBall(new DynamicShape(shape.Position, new Vec2F(0.06f, 0.06f)));
                    return;
                case 3:
                    powerUp = new ExtraPoint(new DynamicShape(shape.Position, new Vec2F(0.06f, 0.06f)));
                    return;
            }
        }
    }
}