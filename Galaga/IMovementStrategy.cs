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
            float x = enemy.Shape.Position.X + 0.05f + (((float)System.Math.PI * (y - enemy.Shape.Position.Y)) / 0.045f);
            enemy.Shape.Move(new Vec2F(x, y));
        }

        void IMovementStrategy.MoveEnemies(EntityContainer<Enemy> enemies)
        {
            foreach (Enemy enemy in enemies) {
                float y = enemy.Shape.Position.Y - 0.0003f;
                float x = enemy.Shape.Position.X + 0.05f + (float)System.Math.Sin((((float)System.Math.PI * (enemy.Shape.Position.Y - y)) / 0.045f));
                enemy.Shape.Move(new Vec2F(x, y));
            }
        }
    }
}