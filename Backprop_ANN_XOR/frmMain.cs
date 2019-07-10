using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using dotnetCHARTING.WinForms;

namespace Backprop_ANN_XOR
{
    #region frmMain CLASS
    /// <summary>
    /// Provides a GUI to show the results of this article which is
    /// to use a <see cref="NN_Trainer_XOR">trainer</see> to train a 
    /// <see cref="NeuralNetwork">NeuralNetwork </see>to solve the XOR
    /// logic problem. 
    /// </summary>
    public partial class frmMain : Form
    {
        #region Instance Fields
        private int trainingLoop = 0;
        private int trainingNo = 0;
        private NeuralNetwork nn;
        private Series[] series_Training;
        private SeriesCollection SC_Training;
        private Series[] series_Trained;
        private SeriesCollection SC_Trained;
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new frmMain, and ensures its as flicker free as it can be
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw |
                     ControlStyles.DoubleBuffer | ControlStyles.UserPaint, true);
        }
        #endregion
        #region private methods
        /// <summary>
        /// Carries out the main function of this article which is
        /// to use a <see cref="NN_Trainer_XOR">trainer</see> to train a 
        /// <see cref="NeuralNetwork">NeuralNetwork </see>to solve the XOR
        /// logic problem. 
        /// The results are also used to show within this example GUI form
        /// </summary>
        /// <param name="sender">btnDoXOR</param>
        /// <param name="e">the event args</param>
        private void btnDoXOR_Click(object sender, EventArgs e)
        {

            try
            {
                trainingNo = int.Parse(txtTraining.Text);
                txtResults.Text = "START OF TRAINING\r\n";
                setGuiState(false);
                trainingLoop = 0;
                initCharts();
                nn = new NeuralNetwork(2, 2, 1);
                nn.Change += new NeuralNetwork.ChangeHandler(nn_Change);
                NN_Trainer_XOR trainer = new NN_Trainer_XOR(ref nn);
                trainer.Change += new NN_Trainer_XOR.ChangeHandler(trainer_Change);
                btnDoXOR.Enabled = false;
                trainer.doTraining(trainingNo);
                txtResults.Text += "END OF TRAINING " + "\r\n";
                trainer.doActualRun();
                btnDoXOR.Enabled = true;
                setGuiState(true);
                fillCharts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("you must enter an integer for the traning number",
                                 "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Initislaise the charts
        /// </summary>
        private void initCharts()
        {
            initialiseChartData(
                "Neural Network training for XOR",
                ref SC_Training,
                ref series_Training,
                ref chartTraining);
            initialiseChartData(
                "Neural Network trained for XOR",
                ref SC_Trained,
                ref series_Trained,
                ref chartTrained);
        }

        /// <summary>
        /// Fill the charts with data
        /// </summary>
        private void fillCharts()
        {
            fillGraphDataTargsVsOuts(ref SC_Training,
                ref series_Training,
                ref chartTraining);
            fillGraphDataTargsVsOuts(ref SC_Trained,
                ref series_Trained,
                ref chartTrained);
        }

        /// <summary>
        /// Fills a chart object using the parameters provided
        /// </summary>
        /// <param name="SC">The chart series collection</param>
        /// <param name="series">The chart series</param>
        /// <param name="chart">The chart</param>
        private void fillGraphDataTargsVsOuts(ref SeriesCollection SC,
                                              ref Series[] series, ref Chart chart)
        {
            SC.Clear();
            chart.SeriesCollection.Clear();
            SC.Add(series[0]);
            SC.Add(series[1]);
            chart.SeriesCollection.Add(SC);
            chart.RefreshChart();
        }


        /// <summary>
        /// Fills a chart object using the parameters provided
        /// </summary>
        /// <param name="SC">The chart series collection</param>
        /// <param name="series">The chart series</param>
        /// <param name="chart">The chart</param>
        private void fillGraphDataErrors(ref SeriesCollection SC,
                                              ref Series[] series, ref Chart chart)
        {
            SC.Clear();
            chart.SeriesCollection.Clear();
            SC.Add(series[2]);
            chart.SeriesCollection.Add(SC);
            chart.RefreshChart();
        }


        /// <summary>
        /// Configures a chart object using the parameters provided
        /// </summary>
        /// <param name="title">The chart title</param>
        /// <param name="SC">The chart series collection</param>
        /// <param name="series">The chart series</param>
        /// <param name="chart">The chart</param>
        private void initialiseChartData(String title, ref SeriesCollection SC, 
                                                    ref Series[] series, ref Chart chart)
        {
            SC = new SeriesCollection();
            //create some new series data for the chart
            series = new Series[3];
            series[0] = new Series("Target");
            series[1] = new Series("Output");
            series[2] = new Series("Error");
            // nullify the chart data
            chart.SeriesCollection.Clear();
            SC.Clear();
            //set the  formatting properties
            chart.Title = title;
            chart.Type = ChartType.Scatter;
            chart.Use3D = false;
            chart.DefaultSeries.DefaultElement.Transparency = 20;
            chart.DefaultSeries.DefaultElement.Marker.Type = ElementMarkerType.None;
            chart.ChartArea.LegendBox.Template = "%Name%Icon";
            chart.DefaultSeries.Line.Width = 2;

        }

        /// <summary>
        /// Sets the tab and view config button
        /// enabled state to be the state of the enabled 
        /// input parameter
        /// </summary>
        /// <param name="enabled">the enabled state</param>
        private void setGuiState(bool enabled)
        {
            tabs.Enabled = enabled;
            btnViewNNConfig.Enabled = enabled;
        }

        /// <summary>
        /// The Change event which is raised by the 
        /// <see cref="NN_Trainer_XOR">trainer</see>
        /// This method simplu updates the trainingLoop with
        /// the current trainers loop number
        /// </summary>
        /// <param name="sender">The <see cref="NN_Trainer_XOR">trainer</see></param>
        /// <param name="nne">The event args</param>
        private void trainer_Change(object sender, TrainerEventArgs te)
        {
            trainingLoop = te.TrainingLoop;
        }

        /// <summary>
        /// Uses the data contained in the event args, to add to the 
        /// live values text box, and also create the required chart
        /// data from the data
        /// </summary>
        /// <param name="nne">The event args</param>
        private void show_NeuralNetwork_Results(NeuralNetworkEventArgs nne)
        {
            this.Invoke(new EventHandler(delegate
            {
                GenerateChartData(nne);
                double[] targOuts = nne.TargetOuts;
                double[] outputs = nne.Outputs;
                for (int i = 0; i < targOuts.Length; i++)
                {
                    txtResults.Text += (NeuralNetwork.isInTraining ? "Training Loop " + trainingLoop + " " : "");
                    txtResults.Text += "Output : " + outputs[i].ToString("#,##0.00")
                        + " / Target Output : " + targOuts[i] + "\r\n";
                }
            }));
            this.Invalidate();
            Application.DoEvents();
        }
        

        /// <summary>
        /// The Change event which is raised by the 
        /// <see cref="NeuralNetwork">NeuralNetwork</see>
        /// This method then calls the show_NeuralNetwork_Results()
        /// method
        /// </summary>
        /// <param name="sender">The <see cref="NeuralNetwork">NeuralNetwork</see></param>
        /// <param name="nne">The event args</param>
        private void nn_Change(object sender, NeuralNetworkEventArgs nne)
        {
            if (NeuralNetwork.isInTraining)
            {
                //only show every 100th result
                if (trainingLoop % 100 == 0)
                {
                    show_NeuralNetwork_Results(nne);
                }
            }
            else
            {
                //not in traning, so show ALL results
                show_NeuralNetwork_Results(nne);
            }
        }

        /// <summary>
        /// Calls the setGuiState with a false parameter
        /// </summary>
        /// <param name="sender">The rmMain</param>
        /// <param name="e">the event args</param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            setGuiState(false);
            txtTraining.Focus();
        }

        /// <summary>
        /// Shows a new <see cref="frmNeuralNetworkConfig">frmNeuralNetworkConfig</see> 
        /// form for the current <see cref="NeuralNetwork">NeuralNetwork</see> 
        /// </summary>
        /// </summary></see> 
        /// </summary>
        /// <param name="sender">btnViewNNConfig</param>
        /// <param name="e">the event args</param>
        private void btnViewNNConfig_Click(object sender, EventArgs e)
        {
            frmNeuralNetworkConfig fNNCfg = new frmNeuralNetworkConfig();
            fNNCfg.NeuralNetworkCfg = this.nn;
            fNNCfg.ShowDialog(this);
        }




        /// <summary>
        /// Create chart data based on the results from the 
        /// <see cref="NeuralNetwork">NeuralNetwork</see> 
        /// </summary>
        /// <param name="nne"></param>
        private void GenerateChartData(NeuralNetworkEventArgs nne)
        {

            Element eTarget = new Element("Training loop No " + trainingLoop + " target ", DateTime.Now, nne.TargetOuts[0]);
            Element eOutput = new Element("Training loop No " + trainingLoop + " output ", DateTime.Now, nne.Outputs[0]);
            Element eError = new Element("Training loop No " + trainingLoop + " error ", 
                                            DateTime.Now, Math.Sqrt(Math.Pow((nne.TargetOuts[0] - nne.Outputs[0]),2)));

            if (NeuralNetwork.isInTraining)
            {
                series_Training[0].Elements.Add(eTarget);
                series_Training[1].Elements.Add(eOutput);
                series_Training[2].Elements.Add(eError);
            }
            else
            {
                series_Trained[0].Elements.Add(eTarget);
                series_Trained[1].Elements.Add(eOutput);
                series_Trained[2].Elements.Add(eError);
            }
        }

        /// <summary>
        /// Fills the chartTraining chart, with the training targets/outputs
        /// </summary>
        /// <param name="sender">the btnTrainingTargetVsOuts button</param>
        /// <param name="e">the event args</param>
        private void btnTrainingTargetVsOuts_Click(object sender, EventArgs e)
        {
            fillGraphDataTargsVsOuts(ref SC_Training,
                                     ref series_Training,
                                     ref chartTraining);
        }

        /// <summary>
        /// Fills the chartTraining chart, with the training errors
        /// </summary>
        /// <param name="sender">the btnTrainingErrors button</param>
        /// <param name="e">the event args</param>
        private void btnTrainingErrors_Click(object sender, EventArgs e)
        {
            fillGraphDataErrors(ref SC_Training,
                                ref series_Training,
                                ref chartTraining);
        }

        /// <summary>
        /// Fills the chartTrained chart, with the trained targets/outputs
        /// </summary>
        /// <param name="sender">the btnTrainedTargetVsOuts button</param>
        /// <param name="e">the event args</param>
        private void btnTrainedTargetVsOuts_Click(object sender, EventArgs e)
        {
            fillGraphDataTargsVsOuts(ref SC_Trained,
                                     ref series_Trained,
                                     ref chartTrained);
        }

        /// <summary>
        /// Fills the chartTrained chart, with the training errors
        /// </summary>
        /// <param name="sender">the btnTrainedErrors button</param>
        /// <param name="e">the event args</param>
        private void btnTrainedErrors_Click(object sender, EventArgs e)
        {
            fillGraphDataErrors(ref SC_Trained,
                                ref series_Trained,
                                ref chartTrained);
        }
        #endregion

    }
    #endregion
}