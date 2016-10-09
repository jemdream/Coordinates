using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coordinates.Measurements.Models
{
    class SurfaceResult
    {
        private double _a1;
        private double _a2;
        private double _a3;
        public double A1
        {
            get { return _a1; }
            set { _a1 = value; }
        }
        public double A2
        {
            get { return _a2; }
            set { _a2 = value; }
        }
        public double A3
        {
            get { return _a3; }
            set { _a3 = value; }
        }
    }
}
