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
        private Detector _detector = new Detector();

        /// <summary>
        /// Горизонт в пикселях снизу
        /// </summary>
        private int _horizont = 400;

        /// <summary>
        /// размер кадра в пикселях, требуемый для обработки
        /// </summary>
        private Size _frameSize = new Size(1280, 720);

        private VideoWriter _videoWriter;
        private string _outputFilePath;

        /// <summary>
        /// Частота кадров по умолчанию
        /// </summary>
        const int DefaultFps = 35;
        public Form1()
        {
            InitializeComponent();
            timer1.Tick += ProcessVideo;//устанавливаем обработчик, который будет вызываться по таймеру
        }


        private void StopVideo()
        {
            stopButton.Enabled = false;
            pauseButton.Enabled = false;
            _videoWriter?.Dispose();
            timer1.Stop();
            OutputFilePanel.Enabled = true;
        }

        /// <summary>
        /// Обработка кадров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessVideo(object sender, EventArgs e)
        {
            if (_capture != null)
            {
                Mat frame = _capture.QueryFrame();

                if (frame != null)
                {
                    pauseButton.Enabled = true;
                    stopButton.Enabled = true;
                    pauseButton.Text = "Пауза";
                    if (frame.Size != _frameSize)
                        CvInvoke.Resize(frame, frame, _frameSize); //изменение размера полученного кадра
                    var info = _detector.GetRails(frame); //получаем информацию о рельсах
                    //CvInvoke.Rectangle(frame, new Rectangle(980, 0, 850, 100), Colors.White);
                    string distanceMsg = $"Distance to object:{info.Meters:N3} ";
                    CvInvoke.PutText(frame, info.IsSafe ? "Safe zone more 100m" : distanceMsg,
                        new Point(950, 40), FontFace.HersheyPlain, 1, info.IsSafe ? Colors.Green : Colors.Red);
                    CvInvoke.PutText(frame, distanceMsg, new Point(950, 80),
                        FontFace.HersheyPlain, 1, Colors.Green);
                    if (info.RightRail != null) //если найдена правая рельса
                    {
                        var rightRail = info.RightRail.Value;
                        CvInvoke.Line(frame, rightRail.P1, rightRail.P2, info.IsSafe ? Colors.Green : Colors.Red, 4);
                    }
                    if (info.LeftRail != null) //если найдена левая рельса
                    {
                        var leftRail = info.LeftRail.Value;
                        CvInvoke.Line(frame, leftRail.P1, leftRail.P2, info.IsSafe ? Colors.Green : Colors.Red, 4);
                    }
                    statusLabel.Text = $"Дистанция:{info.Meters:N3}"; //вывод дистанции в label
                    statusLabel.BackColor = info.IsSafe ? Color.Green : Color.Red;


                    Mat resizedFrame = new Mat();
                    //CvInvoke.Rectangle(frame, _roi, Colors.Green,3);//нарисовать зону поиска
                    CvInvoke.Resize(frame, resizedFrame, new Size(originalImageBox.Width, originalImageBox.Height));

                    originalImageBox.Image = resizedFrame;
                    if (writeOutputCheckBox.Checked) //если отметили, что нужно записывать выходной файл
                    {
                        if (_videoWriter != null)
                        {
                            _videoWriter.Write(frame);//запись кадра в выходной файл
                        }
                    }
                    Mat contoursResized = new Mat();
                    CvInvoke.Resize(_detector.Edges, contoursResized,
                        new Size(contoursImageBox.Width, contoursImageBox.Height));
                    contoursImageBox.Image = contoursResized;

                }
                else
                {
                   StopVideo();
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
                _detector.LowerThreshold = lt;//устанавливаем нижний порог для детектора Кэнни
        }

        private void highThTextBox_TextChanged(object sender, EventArgs e)
        {
            int ht;
            if (int.TryParse((sender as TextBox).Text, out ht))
                _detector.UpperThreshold = ht;//устанавливаем верхний порог для детектора Кэнни
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int ht;
            if (int.TryParse((sender as TextBox).Text, out ht))
                _detector.HoughThreshold = ht;//устанавливаем порог для преобразования Хафа
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
                _videoWriter?.Dispose();
                if (writeOutputCheckBox.Checked)
                {
                    _videoWriter=new VideoWriter(outputFileTextBox.Text,DefaultFps,_frameSize,true);
                }
                OutputFilePanel.Enabled = false;
                
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
                _capture = new Capture(fileNameTextBox.Text);//создаем объект для чтения кадров видеофайла
                double fps = _capture.GetCaptureProperty(CapProp.Fps);//получаем фпс видеофайла
                SetTimerInterval(fps);
                _videoWriter?.Dispose();
                
                if (writeOutputCheckBox.Checked)//если поставили галочку на запись
                {
                    _videoWriter = new VideoWriter(outputFileTextBox.Text, (int)fps, _frameSize, true);
                }
                OutputFilePanel.Enabled = false;//деактивировать панель с настройками выходного файла
                
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

        private void browseSaveButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                outputFileTextBox.Text = saveFileDialog1.FileName;//написать путь выходного файла
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {

            StopVideo();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _videoWriter?.Dispose();//освободить ресурсы записывателя видеофайлов
        }
    }
}
