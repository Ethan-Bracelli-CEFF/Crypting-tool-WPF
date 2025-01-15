using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Crypting> cryptingMethods;

        public MainWindow()
        {
            InitializeComponent();

            cryptingMethods = CryptingFactory.GetAllCryptingMethods();

            foreach (var method in cryptingMethods)
            {
                combobox_algorithm.Items.Add(method.Name);
            }

            if (combobox_algorithm.Items.Count > 0)
            {
                combobox_algorithm.SelectedIndex = 0;
            }
        }

        private void btn_encrypt_Click(object sender, RoutedEventArgs e)
        {
            if (combobox_algorithm.SelectedItem == null || textbox_input.Text == "")
            {
                label_alerte.Content = "You need to put text and choose an algorithm.";
                return;
            }

            string selectedAlgorithm = combobox_algorithm.SelectedItem.ToString();
            var cryptingMethod = cryptingMethods.FirstOrDefault(c => c.Name == selectedAlgorithm);

            if (cryptingMethod != null)
            {
                try
                {
                    int shift = (int)slider.Value;
                    string key = textbox_key.Text;

                    if (selectedAlgorithm == "Hex64" && !IsHex(textbox_key.Text))
                    {
                        throw new ArgumentException("This key is not in the correct format to be used by this algorithm.");
                    }

                    textbox_output.Text = cryptingMethod.Encyrypt(textbox_input.Text, shift, key);
                    label_alerte.Content = "";
                }
                catch (ArgumentException ex)
                {
                    label_alerte.Content = ex.Message;
                }
                catch (Exception)
                {
                    label_alerte.Content = "An error occured during the decryption process.";
                }
            }
        }

        private void btn_decrypt_Click(object sender, RoutedEventArgs e)
        {
            if (combobox_algorithm.SelectedItem == null || textbox_input.Text == "")
            {
                label_alerte.Content = "You need to put text and choose an algorithm.";
                return;
            }

            string selectedAlgorithm = combobox_algorithm.SelectedItem.ToString();
            var cryptingMethod = cryptingMethods.FirstOrDefault(c => c.Name == selectedAlgorithm);

            if (cryptingMethod != null)
            {
                try
                {
                    int shift = (int)slider.Value;
                    string key = textbox_key.Text;

                    if (selectedAlgorithm == "Ethan Crypt" && !IsHex(textbox_input.Text))
                    {
                        throw new ArgumentException("This text is not in the correct format to be decrypted by this algorithm.");
                    }

                    if (selectedAlgorithm == "Hex64" && !IsHex(textbox_key.Text))
                    {
                        throw new ArgumentException("This key is not in the correct format to be used by this algorithm.");
                    }

                    textbox_output.Text = cryptingMethod.Decyrypt(textbox_input.Text, shift, key);
                    label_alerte.Content = "";
                }
                catch (ArgumentException ex)
                {
                    label_alerte.Content = ex.Message;
                }
                catch (Exception)
                {
                    label_alerte.Content = "An error occured during the decryption process.";
                }
            }
        }

        private bool IsHex(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length % 2 != 0)
            {
                return false;
            }

            foreach (char c in input)
            {
                if (!Uri.IsHexDigit(c))
                {
                    return false;
                }
            }

            return true;
        }


        private void btn_copy_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textbox_output.Text?.ToString()))
            {
                Clipboard.SetText(textbox_output.Text);
                label_alerte.Content = "Output succesfuly copied to your clipboard.";
            }
            else
            {
                label_alerte.Content = "There is no Output to copy.";
            }
        }

        private void combobox_algorithm_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            textbox_output.Text = "";
            
            string selectedAlgorithm = combobox_algorithm.SelectedItem?.ToString();

            var cryptingMethod = cryptingMethods.FirstOrDefault(c => c.Name == selectedAlgorithm);
            label_description.Content = cryptingMethod.Description;

            slider.Visibility = Visibility.Hidden;
            label_shift.Visibility = Visibility.Hidden;

            btn_generate_key.Visibility = Visibility.Hidden;
            textbox_key.Visibility = Visibility.Hidden;
            btn_copy_key.Visibility = Visibility.Hidden;
            label_key.Visibility = Visibility.Hidden;


            if (selectedAlgorithm == "Caesar Cipher")
            {
                slider.Visibility = Visibility.Visible;
                label_shift.Visibility = Visibility.Visible;
            }
            if (selectedAlgorithm == "Hex64")
            {
                label_key.Visibility = Visibility.Visible;
                btn_generate_key.Visibility = Visibility.Visible;


                textbox_key.Visibility = Visibility.Visible;
                btn_copy_key.Visibility = Visibility.Visible;

            }
        }

        private void btn_switch_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_output.Text == null)
            {
                label_alerte.Content = "You need to have an Output first.";
                return;
            }
            label_alerte.Content = "";
            string original_output = textbox_output.Text.ToString();
            textbox_input.Text = original_output;
            textbox_output.Text = "";
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (label_shift.Visibility == Visibility.Visible)
            {
                label_shift.Content = $"Shift : ({slider.Value.ToString()})";
            }
            textbox_output.Text = "";
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            textbox_output.Text = "";
            label_alerte.Content = "";
            slider.Value = 1;
            label_shift.Content = "Shift : (1)";
            textbox_input.Text = "";
            textbox_key.Text = "";
        }

        private void btn_copy_key_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textbox_key.Text?.ToString()))
            {
                Clipboard.SetText(textbox_key.Text);
                label_alerte.Content = "Key succesfuly copied to your clipboard.";
            }
            else
            {
                label_alerte.Content = "There is no Key to copy.";
            }
        }

        private void btn_generate_key_Click(object sender, RoutedEventArgs e)
        {
            Hex64 hex64 = new Hex64();
            textbox_key.Text = hex64.GenerateKey();
        }
    }
}