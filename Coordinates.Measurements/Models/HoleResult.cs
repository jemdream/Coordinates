using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coordinates.Measurements.Models
{
    public class HoleResult
    {
        private double _x0;
        private double _y0;
        private double _z0;
        private double _R;
        public double R
        {
            get { return _R; }
            set { _R = value; }
        }
        public double Z0
        {
            get { return _z0; }
            set { _z0 = value; }
        }
        public double Y0
        {
            get { return _y0; }
            set { _y0 = value; }
        }
        public double X0
        {
            get { return _x0; }
            set { _x0 = value; }
        }
    }
}
