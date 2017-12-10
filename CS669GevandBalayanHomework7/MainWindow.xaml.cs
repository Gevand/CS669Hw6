using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CS669GevandBalayanHomework7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region supervariables
        double a = 5;
        double epochCounter = 10;
        double eta = .2;
        #endregion
        #region variables
        Random random = new Random();
        double[,] w = new double[2, 2];
        double[] u = new double[2];
        double y1 = 0;
        double y2 = 0;
        #endregion
        Output myOutput = new Output();
        public MainWindow()
        {
            InitializeComponent();
            myOutput.Show();
            myOutput.WriteToOuput("------------Starting generation of random weights.---------------");

            w[0, 0] = GetRandomNumber(-.5, .5);
            w[0, 1] = GetRandomNumber(-.5, .5);
            w[1, 0] = GetRandomNumber(-.5, .5);
            w[1, 1] = GetRandomNumber(-.5, .5);
            u[0] = GetRandomNumber(-.5, .5);
            u[1] = GetRandomNumber(-.5, .5);
            SetWeights();
            PrintWeights();
        }


        #region Public UI Functions
        private void btnTrain_Click(object sender, RoutedEventArgs e)
        {
            btnCalc.IsEnabled = true;
            for (int i = 1; i <= epochCounter; i++)
            {
                //training set -1 ^ -1 = -1
                double z = CalculateXor(-1, -1);
                double t = -1;
                //if (Math.Abs(Math.Pow(z, 2) - Math.Pow(t, 2)) > .1)
                {
                    u[0] = u[0] - (eta * CalculatePartialU(t, z, y1));
                    u[1] = u[1] - (eta * CalculatePartialU(t, z, y2));
                    w[0, 0] = w[0, 0] - (eta * CalculatePartialW(t, z, y1, u[0], -1));
                    w[0, 1] = w[0, 1] - (eta * CalculatePartialW(t, z, y2, u[1], -1));
                    w[1, 0] = w[1, 0] - (eta * CalculatePartialW(t, z, y1, u[0], -1));
                    w[1, 1] = w[1, 1] - (eta * CalculatePartialW(t, z, y2, u[1], -1));
                }
                //training set 1 ^ 1 = -1
                z = CalculateXor(1, 1);
                t = -1;
                // if (Math.Abs(Math.Pow(z, 2) - Math.Pow(t, 2)) > .1)
                {
                    u[0] = u[0] - (eta * CalculatePartialU(t, z, y1));
                    u[1] = u[1] - (eta * CalculatePartialU(t, z, y2));
                    w[0, 0] = w[0, 0] - (eta * CalculatePartialW(t, z, y1, u[0], 1));
                    w[0, 1] = w[0, 1] - (eta * CalculatePartialW(t, z, y2, u[1], 1));
                    w[1, 0] = w[1, 0] - (eta * CalculatePartialW(t, z, y1, u[0], 1));
                    w[1, 1] = w[1, 1] - (eta * CalculatePartialW(t, z, y2, u[1], 1));
                }
                //training set -1 ^ 1 = 1
                z = CalculateXor(-1, 1);
                t = 1;
                //if (Math.Abs(Math.Pow(z, 2) - Math.Pow(t, 2)) > .1)
                {
                    u[0] = u[0] - (eta * CalculatePartialU(t, z, y1));
                    u[1] = u[1] - (eta * CalculatePartialU(t, z, y2));
                    w[0, 0] = w[0, 0] - (eta * CalculatePartialW(t, z, y1, u[0], -1));
                    w[0, 1] = w[0, 1] - (eta * CalculatePartialW(t, z, y2, u[1], -1));
                    w[1, 0] = w[1, 0] - (eta * CalculatePartialW(t, z, y1, u[0], 1));
                    w[1, 1] = w[1, 1] - (eta * CalculatePartialW(t, z, y2, u[1], 1));
                }
                //training set 1 ^ -1 = 1
                z = CalculateXor(1, -1);
                t = 1;
                // if (Math.Abs(Math.Pow(z, 2) - Math.Pow(t, 2)) > .1)
                {
                    u[0] = u[0] - (eta * CalculatePartialU(t, z, y1));
                    u[1] = u[1] - (eta * CalculatePartialU(t, z, y2));
                    w[0, 0] = w[0, 0] - (eta * CalculatePartialW(t, z, y1, u[0], 1));
                    w[0, 1] = w[0, 1] - (eta * CalculatePartialW(t, z, y2, u[1], 1));
                    w[1, 0] = w[1, 0] - (eta * CalculatePartialW(t, z, y1, u[0], -1));
                    w[1, 1] = w[1, 1] - (eta * CalculatePartialW(t, z, y2, u[1], -1));
                }
                myOutput.WriteToOuput(String.Format("------------Starting generation of weights for gen {0}.---------------", i));
                PrintWeights();
            }
            SetWeights();
        }


        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            if ((txtX1.Text != "-1" && txtX1.Text != "1") || (txtX2.Text != "-1" && txtX2.Text != "1"))
            {
                MessageBox.Show("Please make sure that X1 and X2 input are 1 or -1");
                myOutput.WriteToOuput("Please make sure that X1 and X2 input are 1 or -1");
                return;
            }
            txtResult.Text = CalculateXor(Convert.ToInt32(txtX1.Text), Convert.ToInt32(txtX2.Text)).ToString("F3");
        }
        #endregion
        #region Helpers
        public double GetRandomNumber(double minimum, double maximum)
        {

            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        private double CalculatePartialU(double t, double z, double y)
        {

            return a * (t - z) * (z + 1) * (z - 1) * y;
        }
        private double CalculatePartialW(double t, double z, double y, double u, double x)
        {
            return a * (-.5 * a) * (t - z) * (z + 1) * (z - 1) * (y + 1) * (y - 1) * u * x;
        }

        private double CalculateXor(int x1, int x2)
        {
            double s1 = x1 * w[0, 0] + x2 * w[1, 0];
            double s2 = x1 * w[0, 1] + x2 * w[1, 1];
            y1 = CalculateSigmoid(s1);
            y2 = CalculateSigmoid(s2);
            double r1 = (y1 * u[0]) + (y2 * u[1]);
            //returns z1
            return CalculateSigmoid(r1);
        }
        private double CalculateSigmoid(double s)
        {
            return (2 / (1 + Math.Pow(Math.E, (-1 * a * s)))) - 1;
        }
        private void PrintWeights()
        {
            string formatedString =
String.Format(@"
| W11           | {0}
| W12           | {1}
| W21           | {2}
| W22           | {3}
| U1            | {4}
| U2            | {5}
", w[0, 0].ToString("F3"), w[0, 1].ToString("F3"), w[1, 0].ToString("F3"), w[1, 1].ToString("F3"), u[0].ToString("F3"), u[1].ToString("F3"));
            myOutput.WriteToOuput(formatedString);

        }
        private void SetWeights()
        {
            txtW11.Text = w[0, 0].ToString("F3");
            txtW12.Text = w[0, 1].ToString("F3");
            txtW21.Text = w[1, 0].ToString("F3");
            txtW22.Text = w[1, 1].ToString("F3");
            txtU1.Text = u[0].ToString("F3");
            txtU2.Text = u[1].ToString("F3");
        }
        #endregion


    }
}
