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

namespace SteelEvalutor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
        }
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SteelPart mySteel = new SteelPart(); //Piece instanced

                //Textbox values
                mySteel.length = Convert.ToDouble(Length.Text);
                mySteel.height = Convert.ToDouble(Height.Text);
                mySteel.linesQty = Convert.ToInt32(Lines.Text);
                mySteel.postQty = Convert.ToInt32(Posts.Text);


                //Combo box values
                //Grade
                if (Grade.SelectedIndex == 0)
                {
                    mySteel.steelGrade = "Schedule 40";
                }
                else if (Grade.SelectedIndex == 1)
                {
                    mySteel.steelGrade = "Schedule 80";
                }
                else
                {
                    MessageBox.Show("Please select a grade");
                }

                //Diameter
                if (Diameter.SelectedIndex == 0)
                {
                    mySteel.diameter = 1;
                }
                else if (Diameter.SelectedIndex == 1)
                {
                    mySteel.diameter = 1.25;
                }
                else if (Diameter.SelectedIndex == 2)
                {
                    mySteel.diameter = 1.5;
                }
                else
                {
                    MessageBox.Show("Please select a diameter");
                }

                //Math
                double dFactor = 0;
                if (mySteel.diameter == 1 && mySteel.steelGrade == "Schedule 40")
                {
                    dFactor = 1.68;
                }
                else if (mySteel.diameter == 1.25 && mySteel.steelGrade == "Schedule 40")
                {
                    dFactor = 2.27;
                }
                else if (mySteel.diameter == 1.5 && mySteel.steelGrade == "Schedule 40")
                {
                    dFactor = 2.72;
                }
                else if (mySteel.diameter == 1 && mySteel.steelGrade == "Schedule 80")
                {
                    dFactor = 2.17;
                }
                else if (mySteel.diameter == 1.25 && mySteel.steelGrade == "Schedule 80")
                {
                    dFactor = 3.0;
                }
                else if (mySteel.diameter == 1.5 && mySteel.steelGrade == "Schedule 80")
                {
                    dFactor = 3.63;
                }

                double dimensions = (mySteel.height * mySteel.postQty) + (mySteel.length * mySteel.linesQty);
                double fWeight = dimensions * dFactor;
                int sticks = StickCount(mySteel.length, mySteel.height, mySteel.linesQty, mySteel.postQty);

                //Box values are filled
                FinalWeight.Text = $"{fWeight.ToString()} pounds";
                Joints.Text = $"{mySteel.linesQty * mySteel.postQty} joints";
                LinealFeet.Text = $"{dimensions} lineal feet";
                SticksQty.Text = $"{sticks} sticks needed";
            }
            catch
            {
                MessageBox.Show("One of the integer values is missing");
            }

        }
        static int StickCount(double length, double height, int lineQty, int postQty)
        {
            //Declares basic variables, 
            double mLength = (length % 20 - 20) * -1;
            double mHeight = (height % 20 - 20) * -1;
            double linealFt = length * lineQty + height * postQty;
            //If statements cancel out nubs and perfect length values
            if (mLength == 20 || mLength < 3)
            {
                mLength = 0;
            }
            if (mHeight == 20 || mHeight < 3)
            {
                mHeight = 0;
            }
            //Updates missing lengths for each pole
            mLength = mLength * lineQty;
            mHeight = mHeight * postQty;
            //Completed is how many pieces can be consolodated together
            int completed = 1;
            while (linealFt > 20)
            {
                linealFt -= 20;
                completed++;
            }
            //Waste and sticks needed are calculated
            int sticks = completed;
            //Results
            return sticks;
        }
    }
}
