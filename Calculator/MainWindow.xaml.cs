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

namespace Calculator
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastAnswer;
        int caretIndex;

        public MainWindow()
        {
            InitializeComponent();
        }

        void Write(string s)
        {
            caretIndex = textBox.CaretIndex;
            textBox.Text = textBox.Text.Insert(caretIndex, s);
            textBox.CaretIndex += caretIndex + s.Length;
            textBox1.Text = "";
        }

        void Write(string s, int skip)
        {
            caretIndex = textBox.CaretIndex;
            textBox.Text = textBox.Text.Insert(caretIndex, s);
            textBox.CaretIndex += caretIndex + skip;
            textBox1.Text = "";
        }

        private void buttonSinus_Click(object sender, RoutedEventArgs e)
        {
            Write("sin()", 4);
        }

        private void buttonTangens_Click(object sender, RoutedEventArgs e)
        {
            Write("tan()", 4);
        }

        private void buttonCosinus_Click(object sender, RoutedEventArgs e)
        {
            Write("cos()", 4);
        }

        private void buttonLastAnswer_Click(object sender, RoutedEventArgs e)
        {
            Write(lastAnswer.ToString());
        }

        private void buttonOffeneKlammer_Click(object sender, RoutedEventArgs e)
        {
            Write("(");
        }

        private void buttonFakultät_Click(object sender, RoutedEventArgs e)
        {
            Write("!");
        }

        private void buttonLogarithmus_Click(object sender, RoutedEventArgs e)
        {
            Write("log()", 4);
        }

        private void buttonPi_Click(object sender, RoutedEventArgs e)
        {
            Write("π");
        }

        private void buttonGeschlosseneKlammer_Click(object sender, RoutedEventArgs e)
        {
            Write(")");
        }

        private void buttonProzent_Click(object sender, RoutedEventArgs e)
        {
            Write("%");
        }

        private void buttonNatürlicherLogarithmus_Click(object sender, RoutedEventArgs e)
        {
            Write("ln()", 3);
        }

        private void buttonE_Click(object sender, RoutedEventArgs e)
        {
            Write("e");
        }

        private void buttonSieben_Click(object sender, RoutedEventArgs e)
        {
            Write("7");
        }

        private void buttonVier_Click(object sender, RoutedEventArgs e)
        {
            Write("4");
        }

        private void buttonEins_Click(object sender, RoutedEventArgs e)
        {
            Write("1");
        }

        private void buttonNull_Click(object sender, RoutedEventArgs e)
        {
            Write("0");
        }

        private void buttonAcht_Click(object sender, RoutedEventArgs e)
        {
            Write("8");
        }

        private void buttonFünf_Click(object sender, RoutedEventArgs e)
        {
            Write("5");
        }

        private void buttonZwei_Click(object sender, RoutedEventArgs e)
        {
            Write("2");
        }

        private void buttonKomma_Click(object sender, RoutedEventArgs e)
        {
            Write(",");
        }

        private void buttonNeun_Click(object sender, RoutedEventArgs e)
        {
            Write("9");
        }

        private void buttonSechs_Click(object sender, RoutedEventArgs e)
        {
            Write("6");
        }

        private void buttonDrei_Click(object sender, RoutedEventArgs e)
        {
            Write("3");
        }

        private void buttonGleich_Click(object sender, RoutedEventArgs e)
        {
            double ergebnis = Math.Calculate(textBox.Text);
            textBox1.Text = ergebnis.ToString();
            lastAnswer = ergebnis;
        }

        private void buttonClearElement_Click(object sender, RoutedEventArgs e)
        {
            if(textBox.Text != "")
            {
                caretIndex = textBox.CaretIndex;
                textBox1.Text = "";
                if (caretIndex == 0)
                {
                    textBox.Text = textBox.Text.Remove(caretIndex, 1);
                    textBox.CaretIndex = caretIndex;
                }
                else
                {
                    textBox.Text = textBox.Text.Remove(caretIndex - 1, 1);
                    textBox.CaretIndex = caretIndex - 1;
                }
            }
        }

        private void buttonHoch_Click(object sender, RoutedEventArgs e)
        {
            Write("^");
        }

        private void buttonMal_Click(object sender, RoutedEventArgs e)
        {
            Write("*");
        }

        private void buttonPlus_Click(object sender, RoutedEventArgs e)
        {
            Write("+");
        }

        private void buttonAllClear_Click(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";
            textBox1.Text = "";
        }

        private void buttonWurzel_Click(object sender, RoutedEventArgs e)
        {
            Write("√");
        }

        private void buttonGeteilt_Click(object sender, RoutedEventArgs e)
        {
            Write("/");
        }

        private void buttonMinus_Click(object sender, RoutedEventArgs e)
        {
            Write("-");
        }
    }
}
