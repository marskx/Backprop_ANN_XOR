using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Backprop_ANN_XOR
{
    #region Program CLASS
    /// <summary>
    /// Simply provides the main entry point for the application.
    /// Creates a new frmMain form
    /// </summary>
    public static class Program
    {
        #region Main entry point
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
        #endregion
    }
    #endregion
}