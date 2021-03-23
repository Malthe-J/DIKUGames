using Galaga.Squadron;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
namespace Galaga {
    public class LineFormation : ISquadron{
        EntityContainer<Enemy> _enemies;

        private int _MaxEnemies = 5;

        EntityContainer<Enemy> ISquadron.Enemies {
            get {
                return _enemies;
            }
        }

        int ISquadron.MaxEnemies {
            get {
                return _MaxEnemies;
            }
        }

        public LineFormation()
        {
            _enemies = new EntityContainer<Enemy>(_MaxEnemies);
            
        }

        void ISquadron.CreateEnemies(System.Collections.Generic.List<DIKUArcade.Graphics.Image> enemyStrides, System.Collections.Generic.List<DIKUArcade.Graphics.Image> alternativeEnemyStrides)
        {
            
            for (int i = 0; i < _MaxEnemies; i++){
                _enemies.AddEntity(new Enemy(new DynamicShape(new Vec2F(0.0f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
            }
        }
    }
}