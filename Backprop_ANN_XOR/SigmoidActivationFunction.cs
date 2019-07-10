using System;
using System.Collections.Generic;
using System.Text;

namespace Backprop_ANN_XOR
{
    #region SigmoidActivationFunction CLASS
    /// <summary>
    /// Provides a sigmoid activation function
    /// </summary>
    public class SigmoidActivationFunction
    {
        /// <summary>
        /// Takes a value for a current network node, and applies a sigmoid
        /// activation function to it, which is then returned
        /// </summary>
        /// <param name="x">The value to apply the activation to</param>
        /// <returns>The activation value, after a sigmoid function</returns>
        public static double processValue(double x)
        {
            return 1.0 / (1.0 + Math.Pow(Math.E, -x));
        }
    }
    #endregion
}
