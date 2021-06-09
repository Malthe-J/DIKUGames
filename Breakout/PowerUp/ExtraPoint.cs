using DIKUArcade.Entities;
using System.IO;
using DIKUArcade.Events;
namespace Breakout.PowerUp{
    public class ExtraPoint : PowerUp
    {
        public ExtraPoint(DynamicShape shape) : base(shape, Path.Combine("Assets", "Images", "purple-circle.png")) {

        }

        public override void AddEffect(){
            ScoreBoard.AddPoint(50);
        }
    }
}