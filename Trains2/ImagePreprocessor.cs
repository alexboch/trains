using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;

//using OpenCvSharp;

namespace Trains2
{
    class ImagePreprocessor
    {
        /// <summary>
        /// Матрица для морфологичеких изменений.
        /// </summary>
        private static readonly Mat HighKernel =
            CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(1, 4),
                new Point(-1, -1));

        private static readonly Mat WideKernel =
            CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(4, 1),
                new Point(-1, -1));

        private static Mat CloseKernel = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(1, 2),
            new Point(-1, -1));
            //private static readonly Mat _morphologyMatrix = Cv2.GetStructuringElement(MorphShapes.Cross, new Size(1, 7));


        public Mat Equalized { get; set; }=new Mat();

        public Mat Grayscale { get; set; }=new Mat();
        public Mat Blurred { get; set; }=new Mat();
        public Mat SobelMat { get; set; }=new Mat();

        public double LowThreshold { get; set; }
        public double HighThreshold { get; set; }
        public ImagePreprocessor(double lowThreshold,double highThreshold)
        {
            LowThreshold = lowThreshold;
            HighThreshold = highThreshold;
        }

        public Mat PrepareImage(Mat image)
        {
            Mat edges = new Mat();
            //Cv2.Canny(image,edges,50,120);
            //Cv2.MorphologyEx(edges, edges, MorphTypes.Open, _morphologyMatrix);
            //Cv2.MorphologyEx(edges, edges, MorphTypes.Gradient, _morphologyMatrix);
            CvInvoke.CvtColor(image,Grayscale,ColorConversion.Bgr2Gray);
            CvInvoke.EqualizeHist(Grayscale,Equalized);

            CvInvoke.MedianBlur(Equalized,Blurred,3);
            CvInvoke.Imshow("Blur",Blurred);
            CvInvoke.Canny(Blurred,edges,LowThreshold,HighThreshold);
            CvInvoke.Sobel(edges, SobelMat, DepthType.Default, 1, 0);
            CvInvoke.MorphologyEx(SobelMat, SobelMat, MorphOp.Erode, HighKernel,
                new Point(-1, -1), 1, BorderType.Default, CvInvoke.MorphologyDefaultBorderValue);
            CvInvoke.MorphologyEx(edges, edges, MorphOp.Dilate, WideKernel,
                new Point(-1, -1), 1, BorderType.Default, CvInvoke.MorphologyDefaultBorderValue);
            CvInvoke.MorphologyEx(edges, edges, MorphOp.Close, CloseKernel,
                new Point(-1, -1), 1, BorderType.Default, CvInvoke.MorphologyDefaultBorderValue);

            //CvInvoke.GaussianBlur(edges,Blurred,new Size(3,3),0);
            
            //CvInvoke.Canny(Blurred,edges,LowThreshold,HighThreshold);
            return edges;
        }
    }
}
