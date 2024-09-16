using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double oldNumber;
        private double newNumber;

        private string operatorString;

        private string oldString;
        public string OldString
        {
            get { return oldString; }
            set
            {
                oldString = value;
                OldNumberTxtBox.Text = OldString;
            }
        }
        private string newString;

        public string NewString
        {
            get { return newString; }
            set
            {
                newString = value;
                ResultsTxtBox.Text = NewString;
                if (Regex.IsMatch(newString, @"^-?\d+(\.\d+)?$"))
                {
                    newNumber = double.Parse(newString);
                }
                else
                {
                    newNumber = 0;
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            NewString = "0";
        }

        private void NumberButtonClick(object sender, RoutedEventArgs e)
        {
            var recievedNumberToCast = (System.Windows.Controls.Button)sender;
            string number = recievedNumberToCast.Content.ToString();
            if (NewString == "0")
            {
                NewString = number;
                newNumber = double.Parse(NewString);
            }
            else
            {
                NewString += number;
                newNumber = double.Parse(NewString);
            }
        }

        private void OperatorButtonClick(object sender, RoutedEventArgs e)
        {
            var recievedOperatorToCast = (System.Windows.Controls.Button)sender;
            operatorString = recievedOperatorToCast.Content.ToString();
            oldNumber = double.Parse(NewString);
            OldString = NewString + " " + operatorString;
            NewString = "0";
        }

        private void RemoveCharacterButtonClick(object sender, RoutedEventArgs e)
        {
            string removedCharString = NewString.Remove(NewString.Length - 1);
            if (Regex.IsMatch(removedCharString, @"^-?\d*\,?\d+$"))
            {
                NewString = removedCharString;
            }
            else if (removedCharString.Length > 1 && removedCharString.Last() == ',')
            {
                NewString = removedCharString.Remove(removedCharString.Length - 1);
            }
            else
            {
                NewString = "0";
            }
        }

        private void ClearEverythingButtonClick(object sender, RoutedEventArgs e)
        {
            oldNumber = 0;
            newNumber = 0;
            NewString = "0";
            OldString = "";
        }

        private void InvertValueButtonClick(object sender, RoutedEventArgs e)
        {
            if (NewString != "0")
            {
                NewString = (double.Parse(NewString) * -1).ToString();
            }
        }

        private void AddDecimalButtonClick(object sender, RoutedEventArgs e)
        {
            if (!NewString.Contains(","))
            {
                NewString += ",";
            }
        }

        private void CalculateButtonClick(object sender, RoutedEventArgs e)
        {
            if (NewString != "" && OldString != "")
            {

                switch (operatorString)
                {
                    case "+":
                        OldString = oldNumber.ToString() + " " + operatorString + " " + newNumber.ToString();
                        NewString = (oldNumber + newNumber).ToString();
                        break;

                    case "*":
                        OldString = oldNumber.ToString() + " " + operatorString + " " + newNumber.ToString();
                        NewString = (oldNumber * newNumber).ToString();
                        break;

                    case "-":
                        OldString = oldNumber.ToString() + " " + operatorString + " " + newNumber.ToString();
                        NewString = (oldNumber - newNumber).ToString();
                        break;

                    case "/":
                        if (oldNumber != 0)
                        {
                            OldString = oldNumber.ToString() + " " + operatorString + " " + newNumber.ToString();
                            NewString = (oldNumber / newNumber).ToString();
                        }
                        break;

                    default:
                        break;
                }
                operatorString = "";
            }
        }

        private void PercentagemButtonClick(object sender, RoutedEventArgs e)
        {
            NewString = (newNumber / 100).ToString();
        }
    }
}