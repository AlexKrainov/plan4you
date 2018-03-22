using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace plan2plan.Common.Image
{
    public static class PreviewSizeType
    {
        public static Size Empty { get { return new Size(0, 0); } }
        public static Size Min { get { return new Size(236, 337); } } //Size(236, 337 // Size(35, 35); 

        public static Size Avg { get { return new Size(379, 540); } } //Size(472, 674) // Size(70, 70);

        public static Size Max { get { return new Size(944, 1348); } }

        public static Size GetSize(string value)
        {
            if (value.ToLower() == "min")
            {
                return Min;
            }
            else if (value.ToLower() == "avg")
            {
                return Avg;
            }
            else if (value.ToLower() == "max")
            {
                return Max;
            }

            return Empty;
        }

    }
}