using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Backprop_ANN_XOR
{
    #region Program CLASS
    /// <summary>
    /// This program is now under version control using git!
    /// It is configured to build using TeamCity
    /// A build will automatically take place on new check-ins
    /// This will apply to all check-ins for the whole NN-Demo project.
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