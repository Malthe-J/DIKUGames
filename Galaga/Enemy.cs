using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga{
    public class Enemy : Entity{

        public int hitpoints {get;set;}

        public float speed {
            get {
                return _speed;
            }
        }

        private float _speed;
        public Enemy(DynamicShape shape, IBaseImage image)
            : base(shape, image) {
                hitpoints=10;
                _speed = -0.0009f;
            }

        public void EnragedSpeed()
        {
            _speed = -0.0020f;
        }
    }
}