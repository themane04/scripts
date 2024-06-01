using System.Windows.Forms;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace User_Manager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            // Initialize the form components
            InitializeComponent();

            // Load the application name from the resource file
            ResourceManager rm = new ResourceManager("User_Manager.Properties.Resources", typeof(Form1).Assembly);

            // Get the application name from the resource file
            string appName = rm.GetString("AppName", CultureInfo.CurrentCulture);

            // Application settings
            Text = appName;
            MaximizeBox = false;
            MinimizeBox = false;
            MaximumSize = Size;
            MinimumSize = Size;
            ActiveControl = username_label;
        }

        // Variables for the form components
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,      // x-coordinate of upper-left corner
            int nTopRect,       // y-coordinate of upper-left corner
            int nRightRect,     // x-coordinate of lower-right corner
            int nBottomRect,    // y-coordinate of lower-right corner
            int nWidthEllipse,  // height of ellipse
            int nHeightEllipse  // width of ellipse
        );

        // Rounded corners for the input fields
        public class RoundedTextBox : TextBox
        {
            protected override void OnCreateControl()
            {
                base.OnCreateControl();
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15)); // 10 is the radius of the corners
            }

            protected override void OnSizeChanged(EventArgs e)
            {
                base.OnSizeChanged(e);
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15)); // Update region when size changes
            }
        }
        public class RoundedListBox : ListBox
        {
            protected override void OnCreateControl()
            {
                base.OnCreateControl();
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15)); // Applying a radius of 10 to corners
            }

            protected override void OnSizeChanged(EventArgs e)
            {
                base.OnSizeChanged(e);
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15)); // Update region when size changes
            }
        }
        
        // A class to apply styles to the form components
        public static class FormStyles
        {
            public static void ApplyPlaceholder(TextBox textBox, string placeholderText)
            {
                textBox.Text = placeholderText;
                textBox.ForeColor = Color.Gray;
                textBox.Enter += (sender, e) => RemovePlaceholder(textBox, placeholderText);
                textBox.Leave += (sender, e) => SetPlaceholder(textBox, placeholderText);
            }

            private static void RemovePlaceholder(TextBox textBox, string placeholderText)
            {
                if (textBox.Text == placeholderText)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                    if (textBox.Name == "password_input") // Specific logic for password box
                        textBox.PasswordChar = '*';
                }
            }

            private static void SetPlaceholder(TextBox textBox, string placeholderText)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.ForeColor = Color.Gray;
                    textBox.Text = placeholderText;
                    if (textBox.Name == "password_input") // Specific logic for password box
                        textBox.PasswordChar = '\0';
                }
            }

            public static void SetupPasswordVisibilityToggle(CheckBox checkBox, TextBox passwordTextBox)
            {
                // Handler for checkbox state changes
                checkBox.CheckedChanged += (sender, e) =>
                {
                    TogglePasswordChar(passwordTextBox, checkBox.Checked);
                };

                // Also handle when password textbox is entered or left to ensure correct state
                passwordTextBox.Enter += (sender, e) =>
                {
                    // If entering the textbox and checkbox is checked, ensure visibility is toggled correctly
                    TogglePasswordChar(passwordTextBox, checkBox.Checked);
                };

                passwordTextBox.Leave += (sender, e) =>
                {
                    // Ensure correct state when leaving the textbox
                    TogglePasswordChar(passwordTextBox, checkBox.Checked);
                };

                // Helper method to toggle password character based on checkbox state and current text
                void TogglePasswordChar(TextBox textBox, bool isChecked)
                {
                    string passwordPlaceholder = "Password";

                    // Apply password character based on whether the textbox has placeholder or actual password
                    if (textBox.Text == passwordPlaceholder)
                    {
                        // If the placeholder is visible, do not use the password char, unless leaving with checkbox unchecked
                        textBox.PasswordChar = '\0';
                    }
                    else
                    {
                        // Apply or remove the password character based on checkbox state
                        textBox.PasswordChar = isChecked ? '\0' : '*';
                    }
                }
            }
        }

        // Boolean to check if the user has been created
        private bool _isUserCreated = false;

        // Count how many users have been created
        private int _userCount = 0;

        // Boolean to check if the username and password fields are empty
        private bool _checkIfPasswordAndUsernameAreEmpty(string username, string password)
        {
            string usernamePlaceholder = "Username";
            string passwordPlaceholder = "Password";

            if (string.IsNullOrEmpty(username) || username == usernamePlaceholder ||
                string.IsNullOrEmpty(password) || password == passwordPlaceholder)
            {
                displayBox.Items.Add("Username and password fields cannot be empty.");
                return true;
            }

            return false;
        }

        // Get the file filter from the resource file
        private string TxtOrCsvDialog()
        {
            ResourceManager rm = new ResourceManager("User_Manager.Properties.Resources", typeof(Form1).Assembly);
            string fileFilter = rm.GetString("FileFilter", CultureInfo.CurrentCulture);
            return fileFilter;
        }

        // Boolean that checks if the password meets the requirements
        private bool ValidatePassword(string password, string username)
        {
            bool isValid = true;

            // Check if the password is between 8 and 12 characters
            if (password.Length < 8 || password.Length > 12)
            {
                displayBox.Items.Add($"Password for user {username} must be between 8 and 12 characters.");
                isValid = false;
            }

            // Check if the password contains at least one lowercase letter
            if (!password.Any(char.IsLower))
            {
                displayBox.Items.Add($"Password for user {username} must contain at least one lowercase letter.");
                isValid = false;
            }

            // Check if the password contains at least one uppercase letter
            if (!password.Any(char.IsUpper))
            {
                displayBox.Items.Add($"Password for user {username} must contain at least one uppercase letter.");
                isValid = false;
            }

            // Check if the password contains at least one digit
            if (!password.Any(char.IsDigit))
            {
                displayBox.Items.Add($"Password for user {username} must contain at least one digit.");
                isValid = false;
            }

            // Check if the password contains at least one special character
            if (password.All(char.IsLetterOrDigit))
            {
                displayBox.Items.Add($"Password for user {username} must contain at least one special character.");
                isValid = false;
            }

            return isValid;
        }

        // Handle manual user creation
        private void CreateUserManually(object sender, EventArgs e)
        {
            displayBox.Items.Clear();

            string username = username_input.Text.Trim(); // TextBox for username
            string password = password_input.Text.Trim(); // TextBox for password


            // Check if the username or password field are empty
            if (_checkIfPasswordAndUsernameAreEmpty(username, password))
            {
                return;
            }

            // Validate the password
            if (!ValidatePassword(password, username))
            {
                return;
            }


            using (System.Management.Automation.PowerShell powerShellInstance =
                   System.Management.Automation.PowerShell.Create())
            {
                // Check if the username already exists
                powerShellInstance.AddScript($"net user {username}");
                var userCheckOutput = powerShellInstance.Invoke();

                // Clear the display box initially
                displayBox.Items.Clear();

                // Check PowerShell error stream specifically for critical errors
                if (powerShellInstance.Streams.Error.Any())
                {
                    var isUserNotFound = powerShellInstance.Streams.Error.Any(err =>
                        err.Exception.Message.Contains("The user name could not be found"));

                    if (!isUserNotFound)
                    {
                        foreach (var errorRecord in powerShellInstance.Streams.Error)
                        {
                            displayBox.Items.Add(errorRecord.ToString());
                        }

                        return; // Stop further processing if there is a real error
                    }
                }

                // No critical errors found, proceed to create user if the username is available
                if (userCheckOutput.Count == 0 || powerShellInstance.Streams.Error.Any(err =>
                        err.Exception.Message.Contains("The user name could not be found")))
                {
                    // Clear previous errors related to user not found, as this is an expected scenario when creating a new user
                    powerShellInstance.Streams.Error.Clear();

                    // Prepare the create user command
                    string createUserScript =
                        $"net user {username} {password} /add /comment:\"User created through PowerShell script\"";
                    powerShellInstance.Commands.Clear();
                    powerShellInstance.AddScript(createUserScript);
                    powerShellInstance.Invoke();

                    if (powerShellInstance.Streams.Error.Count > 0)
                    {
                        foreach (var errorRecord in powerShellInstance.Streams.Error)
                        {
                            displayBox.Items.Add("Error creating user: " + errorRecord);
                        }
                    }
                    else
                    {
                        displayBox.Items.Add("User created successfully: " + username);
                        _isUserCreated = true;
                        _userCount++;
                    }
                }
                else
                {
                    displayBox.Items.Add($"The User '{username}' already exists. Please choose a different username.");
                }
            }
        }

        // Handle saving the username and password to a file when creating user manually
        private void SaveToFile(object sender, EventArgs e)
        {
            // Get the file filter from the resource file
            string fileFilter = TxtOrCsvDialog();

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = fileFilter,
                FilterIndex = 1
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the file path from the save file dialog
                string filePath = saveFileDialog.FileName;
                string username = username_input.Text.Trim();
                string password = password_input.Text.Trim();

                // Check if the username or password field are empty
                if (_checkIfPasswordAndUsernameAreEmpty(username, password))
                {
                    return;
                }

                // Check if the user has been created
                if (!_isUserCreated)
                {
                    displayBox.Items.Clear();
                    displayBox.Items.Add("A user must be created before saving the username and password to a file.");
                    return;
                }

                // Write the username and password to the file
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    // Add a user counter on the left side of the username like 1. username, password
                    writer.WriteLine($"{_userCount}. {username}, {password}");
                    displayBox.Items.Add("Username and password saved to file successfully.");
                }
            }
        }

        // Handle user creation from a file
        private void CreateUserWithFile(object sender, EventArgs e)
        {
            displayBox.Items.Clear();
            string fileFilter = TxtOrCsvDialog();

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = fileFilter,
                FilterIndex = 1,
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                using (StreamReader reader = new StreamReader(filePath))
                {
                    // Skip the header line if the file is a CSV
                    if (Path.GetExtension(filePath).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                    {
                        reader.ReadLine();
                    }

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 2)
                        {
                            string username = parts[0].Trim();
                            string password = parts[1].Trim();
                            if (!ValidatePassword(password, username))
                            {
                                displayBox.Items.Add($"Password for user {username} does not meet requirements.");
                                displayBox.Items.Add("");
                                continue;
                            }

                            Task.Run(() =>
                            {
                                using (System.Management.Automation.PowerShell powerShellInstance =
                                       System.Management.Automation.PowerShell.Create())
                                {
                                    // Check if the username already exists
                                    powerShellInstance.AddScript($"net user {username}");
                                    powerShellInstance.Invoke();

                                    bool isUserNotFound = powerShellInstance.Streams.Error.Any(err =>
                                        err.Exception.Message.Contains("The user name could not be found"));

                                    if (powerShellInstance.Streams.Error.Any() && !isUserNotFound)
                                    {
                                        // Handle other errors
                                        foreach (var errorRecord in powerShellInstance.Streams.Error)
                                        {
                                            displayBox.Invoke((Action)(() =>
                                                displayBox.Items.Add("Error: " + errorRecord)));
                                        }

                                        return;
                                    }

                                    if (isUserNotFound)
                                    {
                                        powerShellInstance.Streams.Error.Clear();
                                    }

                                    if (isUserNotFound)
                                    {
                                        // Prepare the create user command if the user doesn't exist
                                        string createUserScript =
                                            $"net user {username} {password} /add /comment:\"User created through PowerShell script\"";
                                        powerShellInstance.Commands.Clear();
                                        powerShellInstance.AddScript(createUserScript);
                                        powerShellInstance.Invoke();

                                        if (powerShellInstance.Streams.Error.Count > 0)
                                        {
                                            foreach (var errorRecord in powerShellInstance.Streams.Error)
                                            {
                                                displayBox.Invoke((Action)(() =>
                                                    displayBox.Items.Add("Error creating user: " + errorRecord)));
                                            }
                                        }
                                        else
                                        {
                                            displayBox.Invoke((Action)(() =>
                                                displayBox.Items.Add("User created successfully: " + username)));
                                        }
                                    }
                                    else
                                    {
                                        displayBox.Invoke((Action)(() => displayBox.Items.Add(
                                            $"The User '{username}' already exists. Please choose a different username.")));
                                        displayBox.Items.Add("");
                                    }
                                }
                            });
                        }
                    }
                }
            }
        }
    }
}