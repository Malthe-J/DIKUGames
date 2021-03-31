using Galaga.Squadron;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
namespace Galaga {
    public class VFormation : ISquadron{
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

        public VFormation()
        {
            _enemies = new EntityContainer<Enemy>(_MaxEnemies);
            
        }

        void ISquadron.CreateEnemies(System.Collections.Generic.List<DIKUArcade.Graphics.Image> enemyStrides, System.Collections.Generic.List<DIKUArcade.Graphics.Image> alternativeEnemyStrides)
        {
            _enemies.AddEntity(new Enemy(new DynamicShape(new Vec2F(0.05f, 0.9f), new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
            _enemies.AddEntity(new Enemy(new DynamicShape(new Vec2F(0.85f, 0.9f), new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
            _enemies.AddEntity(new Enemy(new DynamicShape(new Vec2F(0.25f, 0.85f), new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
            _enemies.AddEntity(new Enemy(new DynamicShape(new Vec2F(0.65f, 0.85f), new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
            _enemies.AddEntity(new Enemy(new DynamicShape(new Vec2F(0.45f, 0.8f), new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
            
        }

        public void Render() {
            _enemies.RenderEntities();
        }
    }
}