using DIKUArcade.Entities;
using System.IO;
using DIKUArcade.Events;
namespace Breakout.PowerUp{
    public class ExtraLife : PowerUp
    {
        public ExtraLife(DynamicShape shape) : base(shape, Path.Combine("Assets", "Images", "LifePickUp.png")) {

        }

        /// <summary>
        /// This function overrides the AddEffect inherited from the PowerUp class
        /// to send a message through the event system from DIKUArcade
        /// </summary>
        public override void AddEffect(){
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{ EventType = GameEventType.GameStateEvent, 
                                                                Message = "ExtraLife", StringArg1 = "EFFECT"});
        }
    }
}