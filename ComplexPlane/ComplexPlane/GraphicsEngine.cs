using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace ComplexPlane
{
    class GraphicsEngine
    {
        /*---------Members------------*/
        private Graphics drawHandle;
        private Thread renderThread;
        private Color backgroundColor;
        private vector canvasSize;
        private List<graphicsSet> gSets = new List<graphicsSet>();
        private List<int> setsToRemove = new List<int>();


        /*--------Functions-----------*/

        /*-Graphics Sets Management-*/

        public int addGSet(graphicsSet gSet) {
            gSets.Add(gSet);
            return gSets.Count - 1;
        }

        public void removeGSetAtIndex(int index) {
            if (index > gSets.Count - 1 || index <= 0)
            {
                Console.WriteLine("GEngine: Can't remove set #'" + index + "'. set not found");
                return;
            }
            else
                setsToRemove.Add(index);
        }

        /* -Render-Related Functions-*/

        public GraphicsEngine(Graphics g, Color bg, vector size)
        {
            canvasSize = size;
            backgroundColor = bg;
            drawHandle = g;
        }
        /*----Makes sure all threads are closed---*/
        public void stop() {

            renderThread.Abort();
        }
        /*----Init all essecial threads---*/
        public void init()
        {
            renderThread = new Thread(new ThreadStart(render));
            renderThread.Start();
        }
        /*----Runs render routine---*/
        private void render()
        {
            //Frame counting
            int framesRendered = 0;
            long startTime = Environment.TickCount;
            //frame buffer
            Bitmap frameBuffer = new Bitmap((int)canvasSize.x, (int)canvasSize.y);
            Graphics graphicsBuffer = Graphics.FromImage(frameBuffer);
            //main loop
            while (true)
            {
                //draws to frame buffer
                graphicsBuffer.FillRectangle(new SolidBrush(backgroundColor), 0, 0, (int)canvasSize.x, (int)canvasSize.y);
                for (int i = 0; i < gSets.Count; i++)
                    gSets[i].renderSet(graphicsBuffer);
                //sends frame to windows
                drawHandle.DrawImage(frameBuffer, 0, 0);
                //frames tracking 
                framesRendered++;
                if (Environment.TickCount >= startTime + 1000)
                {
                    if (framesRendered < 20) {
                        Console.WriteLine("WARNING! YOU ARE RUNNING WITH LESS THAN 20 FRAMES PER SECOND!");
                        Console.WriteLine("GEngine: " + framesRendered + " fps");
                    }
                    framesRendered = 0;
                    startTime = Environment.TickCount;
                }
                for (int i = 0; i <= setsToRemove.Count-1; i++)
                {
                    gSets.RemoveAt(setsToRemove[i]);
                    setsToRemove.Clear();
                }
            }
        }
    }

    class graphicsObject
    {
        /*---------Members------------*/
        public int objectType;
        public int brushOrPenIndex;
        public Rectangle rect;
        /*--------Functions-----------*/
        public graphicsObject(int ObjTy, int brushOrPenI, Rectangle posAndBb) {
            objectType = ObjTy; rect = posAndBb; brushOrPenIndex = brushOrPenI;
        }

        public void drawObject(Graphics g) {
            if (objectType == 0)
                artist.drawLine(brushOrPenIndex, rect, g);
            if (objectType == 1)
                artist.point(brushOrPenIndex,rect,g);
        }
    }

    class graphicsSet
    {
        /*---------Members------------*/
        String setName = "untitled";
        Rectangle setRect;
        private List<graphicsObject> objs = new List<graphicsObject>();
        private List<int> objsToRemove = new List<int>();

        /*--------Functions-----------*/

        public graphicsSet(String name, Rectangle rect) {
            setRect = rect;
            setName = name;
        }

        public int addObjectToSet(graphicsObject obj) {
            objs.Add(new graphicsObject(obj.objectType,
                obj.brushOrPenIndex,
                    new Rectangle(new Point(setRect.X + obj.rect.X, setRect.Y+obj.rect.Y),
                    new Size(obj.rect.Width*setRect.Width, obj.rect.Height * setRect.Height)
                    )
                ));
            return objs.Count - 1;
        }

        public void removeObjectToSet(int index)
        {
            if (index > objs.Count - 1 || index <= 0)
            {
                Console.WriteLine("GSet: Can't remove object in set '" + setName + "'. Object #" + index + " not found");
                return;
            }
            else
                objsToRemove.Add(index);
        }

        public void renderSet(Graphics g) {
            for (int i = 0; i <= objsToRemove.Count - 1; i++)
            {
                objs.RemoveAt(objsToRemove[i]);
                objsToRemove.Clear();
            }
            for (int i = 0; i <= objs.Count - 1; i++)
                objs[i].drawObject(g);
            
        }
    }

    class artist {
        const int pointSize = 1;
        static Pen[] pens = {
            new Pen(Color.Black,1),
            new Pen(Color.Black, 4),
            new Pen(Color.LightGray, 1),
            new Pen(Color.LightGray, 1),
            new Pen(Color.Gray, 1),
            new Pen(Color.Gray, 4),
            new Pen(Color.DarkMagenta,1),
            new Pen(Color.DarkMagenta,4),
            new Pen(Color.Magenta,1),
            new Pen(Color.Magenta,4),
    };

        public static void drawLine(int penIndex, Rectangle rect, Graphics g) {
            g.DrawLine(pens[penIndex], new Point(rect.X - rect.Size.Width, rect.Y - rect.Size.Height),
                    new Point(rect.X + rect.Size.Width, rect.Y + rect.Size.Height));
        }

        public static void point (int penIndex, Rectangle rect, Graphics g) {
            g.DrawEllipse(pens[penIndex], rect);
        }
    }
}