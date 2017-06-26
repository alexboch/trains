using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;

namespace Trains2
{
    class Helpers
    {
        /// <summary>
        /// Считает сходство между двумя изображениями с контурами
        /// </summary>
        /// <param name="template"></param>
        /// <param name="region"></param>
        /// <param name="diff"></param>
        /// <returns></returns>
        public double ChamferDistance(Mat template,Mat region,out Mat diff)
        {
            diff=new Mat();
            CvInvoke.AbsDiff(template, region,diff);
            double sumDist = CvInvoke.Sum(diff).V0;
            //double distance = sumDist / CvInvoke.CountNonZero(diff);
            double distance = sumDist / template.Cols*template.Rows;
            return distance;
        }


    }
}
