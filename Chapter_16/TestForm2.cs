// TestForm2.cs

using System;
using System.Windows.Forms;
using System.Drawing;

public class TestForm2 {
  static void Main() {
    // Create a Form  by calling the Form constructor
    Form simpleForm = new Form();

    // Set the size of the Form using the Height
    // and Width properties
    simpleForm.Height = 200;
    simpleForm.Width = 200;

    // Add a title to the Form using the Text property
    simpleForm.Text = "Whee!!!";

    // Center the Form on the desktop
    simpleForm.StartPosition = FormStartPosition.CenterScreen;
    simpleForm.Visible = true;

    // Use the Application class Run() method to launch
    // the Form
    Application.Run(simpleForm);
  }
}
