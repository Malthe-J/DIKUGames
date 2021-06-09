using DIKUArcade.Entities;
using System.IO;
using DIKUArcade.Events;
namespace Breakout.PowerUp{
    public class ExtraBall : PowerUp
    {
        public ExtraBall(DynamicShape shape) : base(shape, Path.Combine("Assets", "Images", "ExtraBallPowerUp.png")) {

        }

        public override void AddEffect(){
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{ EventType = GameEventType.GameStateEvent, 
                                                                Message = "ExtraBall", StringArg1 = "EFFECT"});
        }
    }
}