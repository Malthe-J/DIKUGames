using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga{
    public class Enemy : Entity{
        public Enemy(DynamicShape shape, IBaseImage image)
            : base(shape, image) {
                hitpoints=10;
                shape1 = shape;
            }
        public int hitpoints {get;set;}
        
        public DynamicShape shape1;
    }
}