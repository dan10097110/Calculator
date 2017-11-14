using System.Windows;
using DLib;

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
            Write("Sin()", 4);
        }

        private void buttonTangens_Click(object sender, RoutedEventArgs e)
        {
            Write("Tan()", 4);
        }

        private void buttonCosinus_Click(object sender, RoutedEventArgs e)
        {
            Write("Cos()", 4);
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
            Write("Log()", 4);
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
            Write("Ln()", 3);
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
            double ergebnis = DLib.Math.Calculator.Solve(textBox.Text);
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
