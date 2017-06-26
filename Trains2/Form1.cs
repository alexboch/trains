using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using OpenTK.Graphics.OpenGL;
using RailsDetector.RailsDetector;

//using OpenCvSharp;

namespace Trains2
{
    public partial class Form1 : Form
    {
        private Capture _capture;
        private Detector _detector=new Detector();
        private ImagePreprocessor _imp;
        /// <summary>
        /// Зона, в которой ищем рельсы
        /// </summary>
        private Rectangle _roi;
        /// <summary>
        /// Горизонт в пикселях снизу
        /// </summary>
        private int _horizont = 400;

        /// <summary>
        /// размер кадра в пикселях, требуемый для обработки
        /// </summary>
        private Size _frameSize=new Size(1280,720);



        /// <summary>
        /// Координата левого верхнего угла прямоугольника поиска
        /// </summary>
        private int _leftX = 500, _topY = 320;

        private int _roiWidth = 210, _roiHeight = 400;
        
        const int Fps = 35;
        public Form1()
        {
            InitializeComponent();
            timer1.Interval =(int)Math.Round(1.0 / Fps*1000);
            timer1.Tick += ProcessVideo;
            //_imp=new ImagePreprocessor(20,120);
            _roi=new Rectangle(_leftX,_topY,_roiWidth,_roiHeight);
            
            //backgroundWorker1.DoWork += ProcessVideo;
        }


        
        /// <summary>
        /// Обработка кадров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessVideo(object sender,EventArgs e)
        {
            if (_capture != null)
            {
                Mat frame = _capture.QueryFrame();

                if (frame != null)
                {
                    button3.Text = "Пауза";
                    if(frame.Size!=_frameSize)
                    CvInvoke.Resize(frame, frame, _frameSize);
                    var info = _detector.GetRails(frame);
                    //contoursImageBox.Image = _detector.ContoursMat;
                    //contoursImageBox.Image = _detector.Image;

                    //CvInvoke.Line(frame,new Point(0,0),new Point(500,15),Colors.Red,2);
                    CvInvoke.Rectangle(frame, new Rectangle(980, 0, 850, 60), Colors.White);
                    string distanceMsg = $"Distance to object:{info.Meters:N3} ";
                    CvInvoke.PutText(frame, info.IsSafe ? "Safe zone more 100m" : distanceMsg,
                        new Point(950, 40), FontFace.HersheyPlain, 1, info.IsSafe ? Colors.Green : Colors.Red);
                    CvInvoke.PutText(frame, distanceMsg, new Point(950, 80),
                        FontFace.HersheyPlain, 1, Colors.Green);
                    if (info.RightRail != null)
                    {
                        var rightRail = info.RightRail.Value;
                        CvInvoke.Line(frame, rightRail.P1, rightRail.P2, info.IsSafe ? Colors.Green : Colors.Red, 4);
                        //inputImage.Line(rightRail.P1, rightRail.P2, info.IsSafe ? Scalar.Green : Scalar.Red, 4, LineTypes.AntiAlias, 0);

                    }
                    if (info.LeftRail != null)
                    {
                        var leftRail = info.LeftRail.Value;
                        //inputImage.Line(leftRail.P1, leftRail.P2, info.IsSafe ? Scalar.Green : Scalar.Red, 4, LineTypes.AntiAlias, 0);
                        CvInvoke.Line(frame, leftRail.P1, leftRail.P2, info.IsSafe ? Colors.Green : Colors.Red, 4);
                    }
                    statusLabel.Text = $"Дистанция:{info.Meters:N3}";
                    statusLabel.BackColor = info.IsSafe?Color.Green:Color.Red;

                    //string lt = lowThTextBox.Text.Replace(".",",");

                    //double lowThreshold;
                    //if(double.TryParse(lt,out lowThreshold))
                    //    _imp.LowThreshold = lowThreshold;

                    //string ht = highThTextBox.Text.Replace(".", ",");

                    //double highThreshold;
                    //if (double.TryParse(ht, out highThreshold))
                    //    _imp.HighThreshold = highThreshold;

                    //_imp.HighThreshold = Double.Parse(lowThTextBox.Text.Replace(".",","));
                    Mat resizedFrame = new Mat();
                    //CvInvoke.Rectangle(frame, _roi, Colors.Green,3);//нарисовать зону поиска
                    CvInvoke.Resize(frame, resizedFrame, new Size(originalImageBox.Width, originalImageBox.Height));

                    originalImageBox.Image = resizedFrame;


                    Mat contoursResized = new Mat();
                    CvInvoke.Resize(_detector.Edges, contoursResized,
                        new Size(contoursImageBox.Width, contoursImageBox.Height));
                    contoursImageBox.Image = contoursResized;
                    //originalImageBox.Image = frame;
                    //Mat frameToProcess=new Mat(frame,_roi);
                    //contoursImageBox.Image = _imp.PrepareImage(frameToProcess);
                    //equalizedHistImageBox.Image = _imp.Equalized;
                    //imageBox2.Image = _imp.SobelMat;

                    //Mat resizedContoursFrame = new Mat();
                    //CvInvoke.Resize();
                }
            }
        }


        /// <summary>
        /// Обрабатывает видео в отдельном потоке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void ProcessVideo(object sender, DoWorkEventArgs e)
        //{
        //    //var sourceVideo = e.Argument as FrameSource;
        //    //var sizes = new[] {originalPictureBox.Width, originalPictureBox.Height};
        //    //Mat originalFrame=new Mat(sizes,MatType.CV_16S);
        //    //while (true)
        //    //{
        //    //    sourceVideo.NextFrame(originalFrame);
        //    //    originalFrame.Resize(new OpenCvSharp.Size(originalPictureBox.Width, originalPictureBox.Height));

        //    //    var ms = originalFrame.ToMemoryStream();
        //    //    var bitmap=new Bitmap(ms,false);
                
        //    //    originalPictureBox.Invoke((Action)(() =>originalPictureBox.Image=bitmap ));
        //    //}
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            fileNameTextBox.Text = openFileDialog1.FileName;
        }

        private void lowThTextBox_TextChanged(object sender, EventArgs e)
        {
            int lt;
            if (int.TryParse((sender as TextBox).Text, out lt))
                _detector.LowerThreshold = lt;
        }

        private void highThTextBox_TextChanged(object sender, EventArgs e)
        {
            int ht;
            if (int.TryParse((sender as TextBox).Text, out ht))
                _detector.UpperThreshold = ht;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int ht;
            if (int.TryParse((sender as TextBox).Text, out ht))
                _detector.HoughThreshold = ht;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                _capture = new Capture(0);//захват с самой первой камеры
                timer1.Start();

            }
            catch (Exception exc)
            {
                MessageBox.Show(($@"Error
 Message:{exc.Message} Source:{exc.Source}
 StackTrace:{exc.StackTrace}"));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (!timer1.Enabled)
            {
                timer1.Enabled = true;
                btn.Text = "Пауза";
            }
            else
            {
                timer1.Enabled = false;
                btn.Text = "Продолжить";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //var sourceVideo = Cv2.CreateFrameSource_Video(fileNameTextBox.Text);
                //backgroundWorker1.RunWorkerAsync(sourceVideo);
                _capture=new Capture(fileNameTextBox.Text);
                timer1.Start();
                
            }
            catch (Exception exc)
            {
                MessageBox.Show(($@"Error
 Message:{exc.Message} Source:{exc.Source}
 StackTrace:{exc.StackTrace}"));
            }
        }

        private void fileNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
