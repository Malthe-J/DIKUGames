using DIKUArcade.Entities;
using System.IO;
using DIKUArcade.Events;
namespace Breakout.PowerUp{
    public class ExtraBall : PowerUp
    {
        public ExtraBall(DynamicShape shape) : base(shape, Path.Combine("Assets", "Images", "LifePickUp.png")) {

        }

        public override void AddEffect(){
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{ EventType = GameEventType.StatusEvent, 
                                                                Message = "ExtraBall", StringArg1 = "EFFECT"});
        }
    }
}