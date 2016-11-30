using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
/*----Graph Manager----
    This is where the logic happens.
     */
namespace ComplexPlane
{
    class complexLogic
    {
        /*----Members----*/
        private GraphicsEngine gEngine;

        /*----Functions----*/
        public void startGraph(Graphics g, vector size) {
            //startsEngine
            gEngine = new GraphicsEngine(g, Color.White,size);
            gEngine.addGSet(createGridSet(new Point(0,-200), new Rectangle(new Point((int) size.x/2,(int) (size.y/2)), new Size(1,1)),4,0,5,4,2));
            gEngine.addGSet(createGridSet(new Point(0, 100), new Rectangle(new Point((int)size.x / 2, (int)(size.y / 2)), new Size(1, 1)), 2, 0, 10, 6, 8));
            gEngine.init();
        }

        public graphicsSet createGridSet(Point move,Rectangle locAndSize, int scaler,int gN, int step, int pen1, int pen2) {
            graphicsSet myGSet = new graphicsSet("grid "+ gN, new Rectangle(new Point(locAndSize.X + move.X, locAndSize.Y + move.Y),locAndSize.Size));

            for (int i = step; i < (locAndSize.Y / scaler); i = i + step)
            {
                myGSet.addObjectToSet(new graphicsObject(0, pen2, new Rectangle(new Point(0, i), new Size(locAndSize.Y / scaler, 0))));
                myGSet.addObjectToSet(new graphicsObject(0, pen2, new Rectangle(new Point(i, 0), new Size(0, locAndSize.X / scaler))));
                myGSet.addObjectToSet(new graphicsObject(0, pen2, new Rectangle(new Point(0, -i), new Size(locAndSize.Y / scaler, 0))));
                myGSet.addObjectToSet(new graphicsObject(0, pen2, new Rectangle(new Point(-i, 0), new Size(0, locAndSize.X / scaler))));
            }
            myGSet.addObjectToSet(new graphicsObject(0, pen1, new Rectangle(new Point(0, 0), new Size(locAndSize.Y / scaler, 0))));
            myGSet.addObjectToSet(new graphicsObject(0, pen1, new Rectangle(new Point(0, 0), new Size(0, locAndSize.X / scaler))));

            myGSet.addObjectToSet(new graphicsObject(0, pen1, new Rectangle(new Point(0, locAndSize.Y / scaler), new Size(locAndSize.Y / scaler, 0))));
            myGSet.addObjectToSet(new graphicsObject(0, pen1, new Rectangle(new Point(0, -locAndSize.Y / scaler), new Size(locAndSize.Y / scaler, 0))));
            myGSet.addObjectToSet(new graphicsObject(0, pen1, new Rectangle(new Point(locAndSize.X / scaler, 0), new Size(0, locAndSize.X / scaler))));
            myGSet.addObjectToSet(new graphicsObject(0, pen1, new Rectangle(new Point(-locAndSize.X / scaler, 0), new Size(0, locAndSize.X / scaler))));

            return myGSet;
        }

        public void stopGraph() {
            gEngine.stop();
        }
    }
}
