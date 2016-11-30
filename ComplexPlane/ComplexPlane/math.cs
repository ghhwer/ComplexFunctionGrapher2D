using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexPlane
{
      class vector {
            public float x, y, z, w;
            public vector(float X, float Y) {
                x = X; y = Y; z = 0; w = 0;
            }
            public vector(float X, float Y, float Z)
            {
                x = X; y = Y; z = Z; w = 0;
            }
            public vector(float X, float Y, float Z, float W)
            {
                x = X; y = Y; z = Z; w = W;
            }
        }
}
