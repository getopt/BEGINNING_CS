// TestForm3.cs

using System;
using System.Windows.Forms;
using System.Drawing;

public class TestForm3 : Form {

  // (When we add eventually attach components to the Form - labels, buttons,
  // and so forth -- they will be declared here, as FIELDS of the 
  // TestForm4 class.)
 
  // Constructor.
  public TestForm3() {
    // Set the size of the Form using the Height
    // and Width properties.
    this.Height = 200;
    this.Width = 200;

    // Add a title to the Form using the Text property.
    this.Text = "Whee!!!";

    // Center the Form on the desktop.
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Visible = true;
  }

  // Note that there is no Main() method.
}
