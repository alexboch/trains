using Emgu.CV.Structure;

namespace RailsDetector.RailsDetector
{
    /// <summary>
    /// В классе содержатся результаты поиска рельс и находимся ли мы в безопасной зоне.
    /// </summary>
    public class DetectedRails
    {
        /// <summary>
        /// Левая рельса.
        /// </summary>
        public LineSegment2D? LeftRail { get; private set; }

        /// <summary>
        /// Правая рельса.
        /// </summary>
        public LineSegment2D? RightRail { get; private set; }

        /// <summary>
        /// True - стабильно больше 100 метров.
        /// </summary>
        public bool IsSafe { get; private set; }

        /// <summary>
        /// Метры, определенные в этом кадре.
        /// </summary>
        public double Meters { get; private set; }

        /// <summary>
        /// Конструктор объекта, содержащего результат.
        /// </summary>
        /// <param name="leftRail">Левая рельса.</param>
        /// <param name="rightRail">Правая рельса.</param>
        /// <param name="isSafe">True - больше 100 метров.</param>
        /// <param name="meters">Метры, определенные в этом кадре.</param>
        public DetectedRails(LineSegment2D? leftRail, LineSegment2D? rightRail, bool isSafe, double meters)
        {
            LeftRail = leftRail;
            RightRail = rightRail;
            IsSafe = isSafe;
            Meters = meters;
        }
    }
}
