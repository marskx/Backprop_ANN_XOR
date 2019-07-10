using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Backprop_ANN_XOR
{
    #region frmNeuralNetworkConfig CLASS
    /// <summary>
    /// Simply shows a <see cref="NeuralNetwork">NeuralNetwork</see>
    /// within a <see cref="System.Windows.Forms.PropertyGrid">PropertyGrid</see>
    /// </summary>
    public partial class frmNeuralNetworkConfig : Form
    {
        #region Instance Fields
        private NeuralNetwork nn;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructs a new frmNeuralNetworkConfig object
        /// </summary>
        public frmNeuralNetworkConfig()
        {
            InitializeComponent();
        }
        #endregion
        #region Public properties

        /// <summary>
        /// gets/sets the <see cref="NeuralNetwork">NeuralNetwork</see> is use
        /// </summary>
        public NeuralNetwork NeuralNetworkCfg
        {
            get { return nn; }
            set
            {
                nn = value;
                this.pGrid.SelectedObject = value;
            }
        }
        #endregion
    }
    #endregion
}