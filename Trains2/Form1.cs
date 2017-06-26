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
        /// <summary>
        /// Объект для захвата видео
        /// </summary>
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
        
        const int DefaultFps = 35;
        public Form1()
        {
            InitializeComponent();
            timer1.Tick += ProcessVideo;
            _roi=new Rectangle(_leftX,_topY,_roiWidth,_roiHeight);
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
                    CvInvoke.Resize(frame, frame, _frameSize);//изменение размера полученного кадра
                    var info = _detector.GetRails(frame);
                    CvInvoke.Rectangle(frame, new Rectangle(980, 0, 850, 100), Colors.White);
                    string distanceMsg = $"Distance to object:{info.Meters:N3} ";
                    CvInvoke.PutText(frame, info.IsSafe ? "Safe zone more 100m" : distanceMsg,
                        new Point(950, 40), FontFace.HersheyPlain, 1, info.IsSafe ? Colors.Green : Colors.Red);
                    CvInvoke.PutText(frame, distanceMsg, new Point(950, 80),
                        FontFace.HersheyPlain, 1, Colors.Green);
                    if (info.RightRail != null)//если найдена правая рельса
                    {
                        var rightRail = info.RightRail.Value;
                        CvInvoke.Line(frame, rightRail.P1, rightRail.P2, info.IsSafe ? Colors.Green : Colors.Red, 4);
                    }
                    if (info.LeftRail != null)//если найдена левая рельса
                    {
                        var leftRail = info.LeftRail.Value;
                        CvInvoke.Line(frame, leftRail.P1, leftRail.P2, info.IsSafe ? Colors.Green : Colors.Red, 4);
                    }
                    statusLabel.Text = $"Дистанция:{info.Meters:N3}";//вывод дистанции в label
                    statusLabel.BackColor = info.IsSafe?Color.Green:Color.Red;

                 
                    Mat resizedFrame = new Mat();
                    //CvInvoke.Rectangle(frame, _roi, Colors.Green,3);//нарисовать зону поиска
                    CvInvoke.Resize(frame, resizedFrame, new Size(originalImageBox.Width, originalImageBox.Height));

                    originalImageBox.Image = resizedFrame;


                    Mat contoursResized = new Mat();
                    CvInvoke.Resize(_detector.Edges, contoursResized,
                        new Size(contoursImageBox.Width, contoursImageBox.Height));
                    contoursImageBox.Image = contoursResized;
                   
                }
            }
        }



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

        /// <summary>
        /// Устанавливает интервал в зависимости от частоты кадров
        /// </summary>
        private void SetTimerInterval(double fps)
        {
            timer1.Interval = (int)Math.Round(1.0 / fps * 1000);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                _capture = new Capture(0);//захват с самой первой камеры
                SetTimerInterval(DefaultFps);//устанавливаю фпс по умолчанию, не получилось получить фпс камеры
                timer1.Start();

            }
            catch (Exception exc)
            {
                MessageBox.Show(($@"Error
 Message:{exc.Message} Source:{exc.Source}
 StackTrace:{exc.StackTrace}"));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                _capture=new Capture(fileNameTextBox.Text);
                SetTimerInterval(_capture.GetCaptureProperty(CapProp.Fps));
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
