
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace RailsDetector.RailsDetector
{
    public class Detector
    {
        /// <summary>
        /// Сплайн для сопоставления пикселей к метрам.
        /// </summary>
        private CubicSpline spline;

        /// <summary>
        /// True - стабильно больше 100 метров.
        /// </summary>
        private bool _isSafe = false;

        /// <summary>
        /// Количество кадров, при которых не нашлось ни одной линии слева.
        /// </summary>
        private int _lineLeftNotExistsFrames;

        /// <summary>
        /// Количество кадров, при которых не нашлось ни одной линии справа.
        /// </summary>
        private int _lineRightNotExistsFrames;

        /// <summary>
        /// Горизонт в пикселях снизу.
        /// </summary>
        private int _horizont = 400;

        /// <summary>
        /// Смещение по X для области поиска линий
        /// </summary>
        private int _cannyX = 500;

        /// <summary>
        /// Средний коэффициент k рельсы слева.
        /// </summary>
        private double _averageAngleLeft = -1.40564765;

        /// <summary>
        /// Средний коэффициент k рельсы справа.
        /// </summary>
        private double _averageAngleRight = 1.40564765;

        /// <summary>
        /// Кол-во найденных углов слева. Используется для подсчета среднего.
        /// </summary>
        private int _angleLeftCount = 1;

        /// <summary>
        /// Кол-во найденных углов справа. Используется для подсчета среднего.
        /// </summary>
        private int _angleRightCount = 1;

        /// <summary>
        /// Кол-во безопасных расстояний на некотором кол-ве кадров.
        /// </summary>
        private int _safeCount;

        /// <summary>
        /// 
        /// </summary>
        private int _safe100, _safe150, _safe200, _safe250;

        /// <summary>
        /// Номер текущего кадра.
        /// </summary>
        private int _treatment;

        /// <summary>
        /// Номер кадра, до которого safe-зона должна подтвердиться.
        /// </summary>
        private int _treatmentСheck;

        /// <summary>
        /// Последнее значение правой рельсы.
        /// </summary>
        private LineSegment2D? _lastRightRail;

        /// <summary>
        /// Последнее значение правой рельсы.
        /// </summary>
        private LineSegment2D? _lastLeftRail;


        /// <summary>
        /// Нижний порог детектора границ Кэнни
        /// </summary>
        public int LowerThreshold { get; set; } = 50;
        public int UpperThreshold { get; set; } = 120;


        public int HoughThreshold { get; set; } = 30;

        public Mat Edges { get; set; }=new Mat();

        /// <summary>
        /// Матрица для морфологичеких изменений.
        /// </summary>
        //private Mat _morphologyMatrix = CvInvoke.GetStructuringElement(ElementShape.Cross,new Size(1, 7),new Point(-1,-1));
        private Mat _morphologyMatrix = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(1, 4),new Point(-1,-1));
        /// <summary>
        /// Последние найденные рельсы.
        /// </summary>
        private List<int> _lastRails = new List<int>();

        /// <summary>
        /// Количество последних высших точек рельс.
        /// </summary>
        private int _lastRailsCount = 6;

        /// <summary>
        /// Количество кадров при котором линии перестают рисоваться.
        /// </summary>
        private int _lineNotExistsFramesBad = 20;

        /// <summary>
        /// Отношение угла нового к среднему, при котором детектится поворот. Значение найдено на видео 24.
        /// </summary>
        private double _differenceTurnDetectionMax = 0.976;

        /// <summary>
        /// Отношение угла нового к среднему, при котором детектится поворот. Значение найдено на видео 24.
        /// </summary>
        private double _differenceTurnDetectionMin = 0.976;

        private int _safeCountMax = 5;

        private const int FrameHeight = 720;

        public Detector()
        {
            spline = new CubicSpline();
            spline.BuildSpline((new double[] { 76, 210, 268, 307, 334, 350, 395 }),
                                new double[] { 12.7, 25.4, 38.1, 50.8, 63.5, 76.2, 355.6 });//12.7--длина грузового полувагона от стенки до стенки
        }

        /// <summary>
        /// Получить информацию для построения.
        /// </summary>
        /// <param name="inputImage">Входное изображение.</param>
        /// <returns>Информация для построения рельс.</returns>
        public DetectedRails GetRails(Mat inputImage)
        {
            List<LineSegment2D> filteredLeftLines = new List<LineSegment2D>();
            List<LineSegment2D> filteredRightLines = new List<LineSegment2D>();
            List<double> angleDifferences = new List<double>();
            _treatment += 1;
            var lines = GetLines(inputImage);
            FilterLines(filteredLeftLines, filteredRightLines, lines, angleDifferences);



            
            SetLastRail(filteredLeftLines, _averageAngleLeft, ref _lineLeftNotExistsFrames, ref _lastLeftRail);
            SetLastRail(filteredRightLines, _averageAngleRight, ref _lineRightNotExistsFrames, ref _lastRightRail);
            //Метод показал свою недееспособность, он просто обнаруживал стрелки, либо крутые углы, работал нестабильно.
            //CheckAngle(angleDifferences);
            
            DrewRails();
            DeleteIfRailsNotDetected();

            if (_lastRails.Count != 0)
            {
                double meters=0;
                meters = spline.Interpolate(GetNormalizedAveragePixels());
               
                //if (_lastLeftRail != null && _lastRightRail != null)
                //{
                //    //double dX = _lastLeftRail.Value.P2.X - _lastRightRail.Value.P2.X;
                //    double focalLength = 10;//Фокусное расстояние в миллиметрах
                //    double sensorWidth = 8.8;//ширина сенсора
                //    double railsWidthInPixels = Math.Abs(_lastLeftRail.Value.P1.X-_lastRightRail.Value.P1.X);
                //    double imgWidthInPixels = inputImage.Width;
                //    double railsWidth = 1520;//ширина колеи в мм
                //    //double focalLength = Math.Abs(_lastLeftRail.Value.P2.X-_lastRightRail.Value.P2.X)*15 / 1.52;
                //    //meters = widthInPixels;
                    
                //    meters = focalLength*railsWidth*imgWidthInPixels / (railsWidthInPixels*sensorWidth)/1000;

                //    double splineMeters = spline.Interpolate(_lastLeftRail.Value.P1.Y);
                //    Cv2.PutText(inputImage, "Spline distance: " + splineMeters, new Point(950, 120),
                //        HersheyFonts.HersheySimplex, 0.5, Scalar.Pink);
                //}
                DetectSafeZone(meters);
                return new DetectedRails(_lastLeftRail, _lastRightRail, _isSafe, meters);
            }
            return new DetectedRails(null, null, false, 0);
        }

        private void DetectSafeZone(double meters)
        {
            if (!_isSafe && _treatment > _treatmentСheck)
            {
                _treatmentСheck = _treatment;
            }

            if (_treatmentСheck == _treatment)
            {
                if (_safeCount >= _safeCountMax)
                {
                    _treatmentСheck += ((_safe250 * 10) + (_safe200 * 8) + (_safe150 * 6) + (_safe100 * 5));
                    _isSafe = true;
                    _safeCount = 0;
                    _safe100 = 0;
                    _safe150 = 0;
                    _safe200 = 0;
                    _safe250 = 0;
                }
                else
                {
                    _isSafe = false;
                }
            }

            if (meters > 100 && _safeCount < _safeCountMax * 2)
            {
                _safeCount += 1;
                if (meters > 100 && meters <= 150)
                {
                    _safe100++;
                }
                if (meters > 150 && meters <= 200)
                {
                    _safe150++;
                }
                if (meters > 200 && meters <= 250)
                {
                    _safe200++;
                }
                if (meters > 250 && meters <= 350)
                {
                    _safe250++;
                }
            }
        }

        /// <summary>
        /// Метод возвращает нормализованное среднее значение пикселей рельс без двух самых меньших.
        /// </summary>
        /// <returns>Нормализованное под сплайн среднее значение высоты пикселей рельс.</returns>
        private double GetNormalizedAveragePixels()
        {
            var rails = _lastRails.OrderByDescending(x => x).ToList();
            
            if (rails.Count == _lastRailsCount)
            {
                rails.RemoveAt(0);
                rails.RemoveAt(0);
            }
            var average = rails.Average();
            var max = rails.Max();
            _lastRails.Remove(max);
            _lastRails.Add(((int)average + max) / 2);
            // Здесь происходит нормализация среднего значения под горизонт и сплайн.
            return (_horizont - average) * (_horizont / 400f);
        }


        /// <summary>
        /// Метод удаляет значения из последних результатов, если долго не находятся рельсы.
        /// </summary>
        private void DeleteIfRailsNotDetected()
        {
            if (_lastRails.Count != 0 &&
                _lineLeftNotExistsFrames >= _lineNotExistsFramesBad &&
                _lineRightNotExistsFrames >= _lineNotExistsFramesBad)
            {
                if (Math.Min(_lineLeftNotExistsFrames, _lineRightNotExistsFrames) == 30)
                {
                    _treatmentСheck = _treatment;
                    _safeCount = 0;
                    _safe100 = 0;
                    _safe150 = 0;
                    _safe200 = 0;
                    _safe250 = 0;
                    _lastRails.Clear();
                    _isSafe = false;
                }
            }
        }

        /// <summary>
        /// Метод дорисовывает меньшую рельсу до большей от начала по среднему углу.
        /// </summary>
        private void DrewRails()
        {
            if (_lastRightRail != null && _lastLeftRail != null)
            {
                var lastRightRail = _lastRightRail.Value;
                var lastLeftRail = _lastLeftRail.Value;
                if (lastRightRail.P1.Y > lastLeftRail.P1.Y)
                {
                    _lastRightRail = DrewLineY(lastRightRail, lastLeftRail.P1.Y, _averageAngleRight);
                }
                else
                {
                    _lastLeftRail = DrewLineY(lastLeftRail, lastRightRail.P1.Y, _averageAngleLeft);
                }
            }
        }

        /// <summary>
        /// Метод в зависимости от среднего отношений углов линий к среднему углу рельс отнимает заработанные безопасные кадры.
        /// </summary>
        /// <param name="tanDifferences">Отношения углов новых к углу среднему.</param>
        private void CheckAngle(List<double> tanDifferences)
        {
            if (tanDifferences.Count != 0)
            {
                double averageDifferecne = tanDifferences.Average();
                if (averageDifferecne < _differenceTurnDetectionMax && 
                    averageDifferecne > _differenceTurnDetectionMin)
                {
                    int angleRevenge = (int)((58.8 - 60 * averageDifferecne) / 0.01581775892747639);
                    if ((_treatmentСheck - angleRevenge) <= _treatment)
                    {
                        _treatmentСheck = _treatment;
                        _safeCount = 0;
                        _safe100 = 0;
                        _safe150 = 0;
                        _safe200 = 0;
                        _safe250 = 0;
                    }
                    else
                    {
                        _treatmentСheck -= angleRevenge;
                    }
                }
            }
        }

        /// <summary>
        /// Дорисовывает линию до значения снизу вверх по заданому углу.
        /// </summary>
        /// <param name="sourceLine">Исходная линия.</param>
        /// <param name="y">Новое значение P1.Y.</param>
        /// <param name="angle">Угол, под которым нужно достроить линию.</param>
        /// <returns></returns>
        private LineSegment2D DrewLineY(LineSegment2D sourceLine, int y, double angle)
        {
            LineSegment2D line = new LineSegment2D(sourceLine.P1, sourceLine.P2);
            int x= (int)((y - line.P2.Y + Math.Tan(angle) * line.P2.X) / Math.Tan(angle));
            line.P1=new Point(x,y);
            //line.P1.Y = y;
            //line.P1.X = (int)((line.P1.Y - line.P2.Y + Math.Tan(angle) * line.P2.X) / Math.Tan(angle));
            return line;
        }

        /// <summary>
        /// Метод фильтрует найденные линии по углу.
        /// </summary>
        /// <param name="filteredLeftLines">Коллекция, куда складываются левые отфильтрованные линии.</param>
        /// <param name="filteredRightLines">Коллекция, куда складываются правые отфильтрованные линии.</param>
        /// <param name="detectedLines">Линии, подлежащие фильтрации.</param>
        private void FilterLines(List<LineSegment2D> filteredLeftLines, List<LineSegment2D> filteredRightLines, LineSegment2D[] detectedLines, List<double> angleDifferences)
        {
            foreach (LineSegment2D s in detectedLines)
            {
                if (s.P1.Y == s.P2.Y)
                    continue;
                var tan = (float)(s.P1.Y - s.P2.Y) / (float)(s.P1.X - s.P2.X);

                if (Math.Abs(tan) > 4.5 && Math.Abs(tan) < 7.5)
                {
                    if (tan > 0)
                    {
                        filteredRightLines.Add(s);
                        angleDifferences.Add(ModifyAverageAngle(Math.Atan(tan), ref _averageAngleRight, ref _angleRightCount));
                    }
                    else
                    {
                        filteredLeftLines.Add(s);
                        angleDifferences.Add(ModifyAverageAngle(Math.Atan(tan), ref _averageAngleLeft, ref _angleLeftCount));
                    }
                }
            }
        }

        /// <summary>
        /// Метод модифицирует текущий средний угол/
        /// </summary>
        /// <param name="angle">Новый угол.</param>
        /// <param name="averageAngle">Средний угол.</param>
        /// <param name="countAngle">Количество значений в среднем угле.</param>
        /// <returns>Отношение нового угла к среднему.</returns>
        private double ModifyAverageAngle(double angle, ref double averageAngle, ref int countAngle)
        {
            double difference = Math.Min(Math.Abs(averageAngle), Math.Abs(angle)) / Math.Max(Math.Abs(averageAngle), Math.Abs(angle));
            if (difference > _differenceTurnDetectionMax)// && countAngle != 30)
            {
                // Формула из мат моделей для поиска среднего.
                averageAngle = (averageAngle * countAngle + angle) / (countAngle + 1);
                countAngle++;
                countAngle = countAngle >= 100 ? 100 : countAngle;
            }
            return difference;
        }

        /// <summary>
        /// Метод возращает выделенные линии из изображения.
        /// </summary>
        /// <param name="inputImage">Входное изображение.</param>
        /// <returns>Набор линий.</returns>
        private LineSegment2D[] GetLines(Mat inputImage)
        {
            Mat edges = new Mat();
            // Рельсы обычно смещены влево, а не по центру.
            Mat edges_tmp = new Mat(inputImage, new Rectangle(_cannyX, FrameHeight - _horizont, 210, _horizont));
            
            //CvInvoke.EqualizeHist(edges_tmp,edges_tmp);
            CvInvoke.Canny(edges_tmp, edges, LowerThreshold, UpperThreshold);
            //CvInvoke.MorphologyEx(edges, edges, MorphOp.Erode, _morphologyMatrix,new Point(-1,-1),1,BorderType.Constant,CvInvoke.MorphologyDefaultBorderValue);
            //CvInvoke.MorphologyEx(edges, edges, MorphOp.Dilate, _morphologyMatrix, new Point(-1, -1), 1, BorderType.Constant, CvInvoke.MorphologyDefaultBorderValue);

            CvInvoke.MorphologyEx(edges, edges, MorphOp.Open, _morphologyMatrix, new Point(-1, -1), 1, BorderType.Constant, CvInvoke.MorphologyDefaultBorderValue);
            //CvInvoke.MorphologyEx(edges, edges, MorphOp.Gradient, _morphologyMatrix, new Point(-1, -1), 1, BorderType.Constant, CvInvoke.MorphologyDefaultBorderValue);

            Edges = edges;
            //Cv2.MorphologyEx(edges, edges, MorphTypes.ERODE, _morphologyMatrix);
            //Cv2.MorphologyEx(edges, edges, MorphTypes.DILATE, _morphologyMatrix);
            //Cv2.MorphologyEx(edges, edges, MorphTypes.Close, Cv2.GetStructuringElement(MorphShapes.Rect, new Size(1,1)));
            return CvInvoke.HoughLinesP(edges, 9, Math.PI / 180, HoughThreshold, 180, 30);
        }

        /// <summary>
        /// Метод выделяет рельсу из набора линий.
        /// </summary>
        /// <param name="filteredLines">Набор линий, из которых надо создать рельсу.</param>
        /// <param name="angle">Угол под которым следует строить рельсу.</param>
        /// <returns>Выделенная из набора линий рельса.</returns>
        private LineSegment2D? GetRail(List<LineSegment2D> filteredLines, double angle)//todo:Проверить на возможные ошибки
        {
            Point? topPoint = GetMaxPoint(filteredLines);
            if (topPoint != null)
            {
                var lastRail = new LineSegment2D(topPoint.Value, new Point());
                _lastRails.Add(lastRail.P1.Y);
                if (_lastRails.Count > _lastRailsCount)
                {
                    _lastRails.RemoveAt(0);
                }
                //Смешение из за обрезания по Canny
                int newX = lastRail.P1.X+ _cannyX;
                int newY = lastRail.P1.Y + FrameHeight - _horizont;
                lastRail.P1=new Point(newX,newY);

                //Формула для нахождения координаты X по углу, известной точки P1 и Y
                int newX2= (int)Math.Round((FrameHeight- lastRail.P1.Y + Math.Tan(angle) * lastRail.P1.X) / Math.Tan(angle));
                lastRail.P2=new Point(newX2,FrameHeight);
               
                return lastRail;
            }
            return null;
        }

        /// <summary>
        /// Метод устанавливает рельсу.
        /// </summary>
        /// <param name="filteredLines">Набор линий, из которых надо создать рельсу.</param>
        /// <param name="lineNotExistsFrames">Количество кадров, на которых не было найдено рельсы.</param>
        /// <param name="lastRail">Значение последней установленной рельсы.</param>
        private void SetLastRail(List<LineSegment2D> filteredLines, double angle, ref int lineNotExistsFrames, ref LineSegment2D? lastRail)
        {
            LineSegment2D? rail = GetRail(filteredLines, angle);
            if (rail == null)
            {
                lineNotExistsFrames++;
                if (lineNotExistsFrames > _lineNotExistsFramesBad)
                {
                    lastRail = null;
                }
            }
            else
            {
                lineNotExistsFrames = 0;
                lastRail = rail;
            }
        }

        /// <summary>
        /// Этот метод возвращает линию, которая формируется из самой максимальной по высоте и самой минимальной по высоте точек
        /// </summary>
        /// <param name="lines">Набор правых или левых линий, после разложения Хафа, из которых ищется самая min и самая max по высоте точки</param>
        /// <param name="draw">Линия, которая формируется из самой минимальной и максимальной точек</param>
        /// <returns>Возвращает true, если нашлась такая draw. Иначе draw не меняется</returns>
        private static Point? GetMaxPoint(List<LineSegment2D> lines)
        {
            LineSegment2D top = new LineSegment2D();
            if (lines.Count != 0)
            {
                top.P1=new Point(top.P1.X,2000);
                foreach (var item in lines)
                {
                    if (item.P1.Y < top.P1.Y)
                    {
                        top.P1 = item.P1;
                    }
                    if (item.P2.Y < top.P1.Y)
                    {
                        top.P1 = item.P2;
                    }
                }
                foreach (var item in lines)
                {
                    if (item.P1.Y > top.P2.Y)
                    {
                        top.P2 = item.P1;
                    }
                    if (item.P2.Y > top.P2.Y)
                    {
                        top.P2 = item.P2;
                    }
                }
                return top.P1;
            }
            else
            {
                return null;
            }

        }
    }
}
