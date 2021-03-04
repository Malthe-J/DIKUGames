using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga {
    public class PlayerShot : Entity{
        private static Vec2F PlayerShotExtend = new Vec2F (0.008f, 0.021f);
        private static Vec2F PlayerShotDirection = new Vec2F (0.0f, 0.1f);
        public Vec2F playerShotDirection{get; set;}
        

        public PlayerShot(Vec2F pos, IBaseImage image): base(new DynamicShape(pos, PlayerShotExtend, PlayerShotDirection), image){

        }

    }
}
