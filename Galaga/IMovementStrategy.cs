using DIKUArcade.Entities;
using DIKUArcade.Math;
namespace Galaga.MovementStrategy {
    public interface IMovementStrategy
    {
        void MoveEnemy(Enemy enemy);
        void MoveEnemies(EntityContainer<Enemy> enemies);
    }

    public class NoMove : IMovementStrategy {
        void IMovementStrategy.MoveEnemy(Enemy enemy)
        {
            
        }

        void IMovementStrategy.MoveEnemies(EntityContainer<Enemy> enemies)
        {

        }
    }

    public class MoveDown : IMovementStrategy {
       void IMovementStrategy.MoveEnemy(Enemy enemy)
        {
            enemy.Shape.MoveY(-0.001f);
        }

        void IMovementStrategy.MoveEnemies(EntityContainer<Enemy> enemies)
        {
            foreach (Enemy e in enemies) {
                e.Shape.AsDynamicShape().Direction = new Vec2F(0.0f, -0.0003f);
                e.Shape.Move();
            }
        } 
    }

    public class ZigZagMove : IMovementStrategy {
        void IMovementStrategy.MoveEnemy(Enemy enemy)
        {
            float y = enemy.Shape.Position.Y - 0.0003f;
            float x = 0.0f + 0.00005f * (float)System.Math.Sin((2 * System.Math.PI * (0.9f - y)) / 0.045f);
            enemy.Shape.Move(new Vec2F(x, -0.0003f));
        }

        void IMovementStrategy.MoveEnemies(EntityContainer<Enemy> enemies)
        {
            enemies.Iterate(enemy => {
                float y = enemy.Shape.Position.Y - 0.0003f;
                float x = 0.0f + 0.005f * (float)System.Math.Sin((2 * System.Math.PI * (0.05f - y)) / 0.045f);
                enemy.Shape.Move(new Vec2F( x, -0.0003f));
            });
        }
    }
}