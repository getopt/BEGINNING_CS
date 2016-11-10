using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

public class PasswordForm : Form {

  private TextBox passwordTextBox; 
  private Label passwordLabel;
  private string password;

  public PasswordForm() {

    int componentTop = 15;

    // create label component
    passwordLabel = new Label();
    passwordLabel.Text = "Password:";
    passwordLabel.Top = componentTop;
    passwordLabel.Left = 15;
    passwordLabel.Width = 70;
    passwordLabel.Font = new Font(passwordLabel.Font, FontStyle.Bold);

    // Create TextBox component
    passwordTextBox = new TextBox();
    passwordTextBox.Height = 40;
    passwordTextBox.Width = 100;
    passwordTextBox.Top = componentTop;
    passwordTextBox.Left = passwordLabel.Right;
    passwordTextBox.PasswordChar = '*';

    // Assign an event handler to the SSN TextBox
    passwordTextBox.KeyUp += new KeyEventHandler(PasswordKeyUp);

    // Add the GUI components to the form
    this.Controls.Add(passwordLabel);
    this.Controls.Add(passwordTextBox);

    this.Text = "Enter Password";
    this.Height = 80;
    this.Width = 240;
    this.MinimumSize = this.Size;
    this.StartPosition = FormStartPosition.CenterScreen;
  }

  // Property
  public string Password {
    get {
      return password;
    }
  }

  // Event handling method
  public void PasswordKeyUp(object source, KeyEventArgs e) {
    if ( e.KeyCode == Keys.Enter ) {
      password = passwordTextBox.Text.Trim();
      this.Visible = false;
    }
  }

  // Test scaffold

  public static void Main(string[] args) {
    PasswordForm passwordForm = new PasswordForm();
    passwordForm.Visible = true;
    Application.Run(passwordForm);
  }
}
