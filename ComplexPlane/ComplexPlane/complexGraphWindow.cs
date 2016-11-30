using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

/*----WINDOW----*/ 
namespace ComplexPlane
{
    public partial class complexGrapherWindow : Form
    {
        /*----Members----*/
        private complexLogic cLogic = new complexLogic();

        /*----Functions----*/
        public complexGrapherWindow()
        {
            InitializeComponent();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            //Initializes program logic
            Graphics g = canvas.CreateGraphics();
            cLogic.startGraph(g, new vector(Size.Width, Size.Height));
        }

        private void complexGrapherWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //"Tells" program logic that user is exiting the program
            cLogic.stopGraph();
        }

        private void complexGrapherWindow_Load(object sender, EventArgs e)
        {
            AllocConsole();
        }

        //Allows the command line to be seen during normal execurion
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        static extern bool AllocConsole();

    }
}
