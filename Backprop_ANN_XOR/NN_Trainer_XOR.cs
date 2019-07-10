using System;
using System.Collections.Generic;
using System.Text;

namespace Backprop_ANN_XOR
{
    #region NN_Trainer_XOR CLASS
    /// <summary>
    /// Provides a XOR trainer for a
    /// <see cref="NeuralNetwork">NeuralNetwork</see> class
    /// with 2 inputs, 2 hidden, and 1 output
    /// </summary>
    public class NN_Trainer_XOR
    {

        #region Instance fields
        private NeuralNetwork nn;
        private Random gen = new Random();
        private int training_times = 10000;
        private double[,] train_set =
            {{0, 0},
             {0, 1},
             {1,0},
             {1,1}};
        public delegate void ChangeHandler(Object sender, TrainerEventArgs te);
        public event ChangeHandler Change;

        #endregion
        #region Constructor
        /// <summary>
        /// Constructs a new NN_Trainer_XOR object. 
        /// This object is a specific trainer for training
        /// a NeuralNetwork with 2 inputs, 2 hidden nodes, a 1 output
        /// to try and solve the XOR problem.
        /// If you require the NeuralNetwork to be training for some other
        /// task, simply create a new trainer object for the task and ensure
        /// the nn input parameter has the correct number of inputs, hidden and
        /// outputs for the trainers task
        /// </summary>
        /// <param name="nn">A pre-configured neural network, with the
        /// correct number of inputs, hidden nodes, a outputs
        /// to try and solve the XOR problem</param>
        public NN_Trainer_XOR(ref NeuralNetwork nn)
        {
            this.nn = nn;
            nn.initialiseNetwork();
        }
        #endregion
        #region Events
        /// <summary>
        /// Raises the Change event
        /// </summary>
        /// <param name="te">The TrainerEventArgs</param>
        public virtual void On_Change(TrainerEventArgs te)
        {
            if (Change != null)
            {
                // Invokes the delegates. 
                Change(this, te);
            }
        }
        #endregion
        #region Public methods

        /// <summary>
        /// Is called after the initial training is completed.
        /// Siply presents 1 complete set of the training set to
        /// the trained XOR, which should hopefully get it pretty
        /// correct now its trained
        /// </summary>
        public void doActualRun()
        {
            NeuralNetwork.isInTraining = false;
            //loop through the entire training set
            for (int j = 0; j <= train_set.GetUpperBound(0); j++)
            {
                //and forward them through the network
                double[] targOut = getTargetValues(getTrainSet(j));
                nn.pass_forward(getTrainSet(j), targOut);
            }
        }

        /// <summary>
        /// Presents the exntire training set to the 
        /// <see cref="NeuralNetwork">NeuralNetwork</see>, as many
        /// times as specified by the training_times input parameter
        /// This will call the NeuralNetwork.pass_forward, and also
        /// call the train_network() methods
        /// </summary>
        /// <param name="training_times">the number of times to carry out the
        /// training loop</param>
        public void doTraining(int training_times)
        {
            NeuralNetwork.isInTraining = true;
            this.training_times = training_times;

            //loop for n-many training times
            for (int i = 0; i < this.training_times; i++)
            {

                TrainerEventArgs te = new TrainerEventArgs(i);
                On_Change(te);
                //loop through all training set examples
                for (int j = 0; j <= train_set.GetUpperBound(0); j++)
                {
                    // Train using example set
                    double[] targOut = getTargetValues(getTrainSet(j));
                    //feed forward through network
                    nn.pass_forward(getTrainSet(j), targOut);
                    //do the weight changes (pass back)
                    train_network(targOut);
                }
            }
        }
        #endregion
        #region Private methods

        /// <summary>
        /// Returns the array within the 2D train_set array as the index
        /// specfied by the idx input parameter
        /// </summary>
        /// <param name="idx">The index into the 2d array to get</param>
        /// <returns>The array within the 2D train_set array as the index
        /// specfied by the idx input parameter</returns>
        private double[] getTrainSet(int idx)
        {
            //NOTE :
            //
            //If anyone can tell me how to return an array at index idx from
            //a 2D array, which is holding arrays of arrays I would like that
            //very much.
            //I thought it would be
            //double[] trainValues= (double[])train_set.GetValue(0);
            //but this didn't work, so am doing it like this

            double[] trainValues = { train_set[idx, 0], train_set[idx, 1] };
            return trainValues;
        }

        /// <summary>
        /// The main training. The expected target values are passed in to this
        /// method as paramaters, and the <see cref="NeuralNetwork">NeuralNetwork</see> 
        /// is then updated with small weight changes, for this training iteration
        /// This method also applied momentum, to ensure that the NeuralNetwork is
        /// nurtured into proceeding in the correct direction. We are trying to avoid valleys.
        /// If you dont know what valleys means, read the articles associatd text
        /// </summary>
        /// <param name="target">A double[] array containing the target value(s)</param>
        private void train_network(double[] target)
        {
            //get momentum values (delta values from last pass)
            double[] delta_hidden = new double[nn.NumberOfHidden + 1];
            double[] delta_outputs = new double[nn.NumberOfOutputs];
            
            // Get the delta value for the output layer
            for (int i = 0; i < nn.NumberOfOutputs; i++)
            {
                delta_outputs[i] =
                    nn.Outputs[i] * (1.0 - nn.Outputs[i]) * (target[i] - nn.Outputs[i]);
            }
            // Get the delta value for the hidden layer
            for (int i = 0; i < nn.NumberOfHidden + 1; i++)
            {
                double error = 0.0;
                for (int j = 0; j < nn.NumberOfOutputs; j++)
                {
                    error += nn.HiddenToOutputWeights[i, j] * delta_outputs[j];
                }
                delta_hidden[i] = nn.Hidden[i] * (1.0 - nn.Hidden[i]) * error;
            }
            // Now update the weights between hidden & output layer
            for (int i = 0; i < nn.NumberOfOutputs; i++)
            {
                for (int j = 0; j < nn.NumberOfHidden + 1; j++)
                {
                    //use momentum (delta values from last pass), 
                    //to ensure moved in correct direction
                    nn.HiddenToOutputWeights[j, i] += nn.LearningRate * delta_outputs[i] * nn.Hidden[j];
                }
            }
            // Now update the weights between input & hidden layer
            for (int i = 0; i < nn.NumberOfHidden; i++)
            {
                for (int j = 0; j < nn.NumberOfInputs + 1; j++)
                {
                    //use momentum (delta values from last pass), 
                    //to ensure moved in correct direction
                    nn.InputToHiddenWeights[j, i] += nn.LearningRate * delta_hidden[i] * nn.Inputs[j];
                }
            }
        }

        /// <summary>
        /// Returns a double which represents the output for the
        /// current set of inputs.
        /// In the cases where the summed inputs = 1, then target
        /// should be 1.0, otherwise it should be 0.0. 
        /// This is only for the XOR problem, but this is a trainer
        /// for the XOR problem, so this is fine.
        /// </summary>
        /// <param name="currSet">The current set of inputs</param>
        /// <returns>A double which represents the output for the
        /// current set of inputs</returns>
        private double[] getTargetValues(double[] currSet)
        {
            //the current value of the training set
            double valOfSet = 0;
            double[] targs = new double[1];
            for (int i = 0; i < currSet.Length; i++)
            {
                valOfSet += currSet[i];
            }
            //in the cases where the summed inputs = 1, then target
            //should be 1.0, otherwise it should be 0.0
            targs[0] = valOfSet == 1 ? 1.0 : 0.0;
            return targs;
        }
        #endregion
    }
    #endregion
    #region TrainerEventArgs CLASS
    /// <summary>
    /// Provides the event argumets for the 
    /// <see cref="NN_Trainer_XOR">trainer</see> class
    /// </summary>
    public class TrainerEventArgs : EventArgs
    {
        #region Instance Fields
        //Instance fields
        private int trainLoop = 0;

        #endregion
        #region Public Constructor

        /// <summary>
        /// Constructs a new TrainerEventArgs object using the parameters provided
        /// </summary>
        /// <param name="trainLoop">The current training loop</param>
        public TrainerEventArgs(int trainLoop)
        {
            this.trainLoop = trainLoop;
        }
        #endregion
        #region Public Methods/Properties

        /// <summary>
        /// gets the training loop number
        /// </summary>
        public int TrainingLoop
        {
            get { return trainLoop; }
        }
        #endregion

    }
    #endregion
}
