using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Calculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int nCurrentAction;         // Whether or not we are entering the first or second number
        string szFirstNumber;       // The string value of the first operand
        string szSecondNumber;      // The string value of the second operand
        string szOperator;          // The string value of the operator

        // Constructor
        //  -Initialize member variables to default values
        public MainPage()
        {
            this.InitializeComponent();
            //nCurrentAction = 0;
            //szFirstNumber = "0";
            //szSecondNumber = "";
            //szOperator = "";
            //tb_Display.Text = "0";

            Reset("0");
        }

        // UpdateDisplay
        //  -Fill in information on the main text box based on current values entered in
        private void UpdateDisplay()
        {
            if (nCurrentAction == 0)
            {
                tb_Display.Text = szFirstNumber;
            }
            else if (nCurrentAction == 1)
            {
                if (szSecondNumber == "")
                {
                    tb_Display.Text = "(" + szFirstNumber + ") " + szOperator + " " + szFirstNumber;
                }
                else
                {
                    tb_Display.Text = "(" + szFirstNumber + ") " + szOperator + " " + szSecondNumber;
                }
            }
        }

        // ConcatNumber
        //  -Used to add on digits to the first or second number. Also handles decimals, negation, and default values
        private void ConcatNumber(string nValue, int nFirstOrSecond)
        {
            if (nFirstOrSecond == 0)
            {
                // If the default value of 0 is all that is in the display, clear it
                if (szFirstNumber == "0")
                {
                    szFirstNumber = "";
                }

                // If there is already a decimal, don't add another one
                if (nValue == "." && szFirstNumber.Contains("."))
                {
                    return;
                }

                // Toggle the - sign on the number
                if (nValue == "±")
                {
                    if (szFirstNumber.Contains("-"))
                    {
                        szFirstNumber = szFirstNumber.Substring(1);
                    }
                    else
                    {
                        szFirstNumber = "-" + szFirstNumber;
                    }
                    UpdateDisplay();
                    return;
                }

                // Add on numbers
                szFirstNumber += nValue;
            }
            else if (nFirstOrSecond == 1)
            {
                // If there is already a decimal, don't add another one
                if (nValue == "." && szSecondNumber.Contains("."))
                {
                    return;
                }

                // Toggle the - sign on the number
                if (nValue == "±")
                {
                    if (szSecondNumber.Contains("-"))
                    {
                        szSecondNumber = szSecondNumber.Substring(1);
                    }
                    else
                    {
                        szSecondNumber = "-" + szSecondNumber;
                    }
                    UpdateDisplay();
                    return;
                }

                // Add on numbers
                szSecondNumber += nValue;
            }
            UpdateDisplay();
        }

        // Reset
        //  -Set member variables to default values and clear the screen
        private void Reset(String szNumber)
        {
            szFirstNumber = szNumber;
            szSecondNumber = "";
            szOperator = "";
            nCurrentAction = 0;
            UpdateDisplay();
        }

        // NumberClick
        //  -Parse the value on this button and concat it onto the appropriate string
        private void NumberClick(object sender, RoutedEventArgs e)
        {
            string szValue = ((Button)sender).Tag.ToString();
            ConcatNumber(szValue, nCurrentAction);
        }

        // OperatorClick
        //  -Parse the value on this button, save it in the szOperator variable, and then move onto the next number
        private void OperatorClick(object sender, RoutedEventArgs e)
        {
            if (nCurrentAction == 0)
            {
                szOperator = ((Button)sender).Tag.ToString();
                nCurrentAction = 1;
                UpdateDisplay();
            }
        }

        // Calculate
        //  -Using the first/second number and operator, determine what the final result will be
        private void Calculate(object sender, RoutedEventArgs e)
        {
            if (nCurrentAction == 1)
            {
                double result = 0.0;
                if (szSecondNumber == "")
                {
                    szSecondNumber = szFirstNumber;
                }


                if (szOperator == "+")
                {
                    result = double.Parse(szFirstNumber) + double.Parse(szSecondNumber);
                }
                if (szOperator == "-")
                {
                    result = double.Parse(szFirstNumber) - double.Parse(szSecondNumber);
                }
                if (szOperator == "x")
                {
                    result = double.Parse(szFirstNumber) * double.Parse(szSecondNumber);
                }
                if (szOperator == "/")
                {
                    result = double.Parse(szFirstNumber) / double.Parse(szSecondNumber);
                }
                
                Reset(result.ToString());
            }
        }

        // Clear
        //  -Reset the display
        private void Clear(object sender, RoutedEventArgs e)
        {
            Reset("0");
            tb_Display.Text = "0";
        }
    }
}
