
namespace LabProj
{
    /// <summary>
    /// Class Racers Motion, is in charge for moving 
    /// picture box objects from start line on the left,
    /// to finish line on the right.
    /// </summary>
    public class RacersMotion
    {
        // this variable keeps whole distance of
        // whole distance that racer needs to run
        // through in order to reach finish.
        public int RacetrackLength;
        // A picturebox object.
        public PictureBox? PersonPicBox = null;
        //orig location.
        public int Location;
        // this is used to randomize speed.
        public Random? Rand;
        // this is used when need to go back to start line.
        public int StartingPosition;
        public bool MoveToReachFinish()
        {
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            var motion = Rand.Next(1, 10); // will move randomly from 1 to 20 to right to reach finish line.
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            var reachFinish = PersonPicBox.Location; // reach finish point is location of pic box.
            // take Picture boxes horizontal position and + it to the motion.
            reachFinish.X += motion;
            // location of pic box is location of picture box.
            PersonPicBox.Location = reachFinish;
            // return horizontal location of picture box have reached finish or over finish.
            return reachFinish.X >= RacetrackLength;
        }
    }
}