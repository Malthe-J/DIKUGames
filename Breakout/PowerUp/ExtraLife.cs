using DIKUArcade.Entities;
using System.IO;
using DIKUArcade.Events;
namespace Breakout.PowerUp{
    public class ExtraLife : PowerUp
    {
        public ExtraLife(DynamicShape shape) : base(shape, Path.Combine("Assets", "Images", "LifePickUp.png")) {

        }

        public override void AddEffect(){
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{ EventType = GameEventType.GameStateEvent, 
                                                                Message = "ExtraLife", StringArg1 = "EFFECT"});
        }
    }
}