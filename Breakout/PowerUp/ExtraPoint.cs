using DIKUArcade.Entities;
using System.IO;
using DIKUArcade.Events;
namespace Breakout.PowerUp{
    public class ExtraPoint : PowerUp
    {
        public ExtraPoint(DynamicShape shape) : base(shape, Path.Combine("Assets", "Images", "purple-circle.png")) {

        }

        /// <summary>
        /// This function overrides the AddEffect inherited from the PowerUp class
        /// to add 50 points to the score in ScoreBoard class
        /// </summary>
        public override void AddEffect(){
            ScoreBoard.AddPoint(50);
        }
    }
}