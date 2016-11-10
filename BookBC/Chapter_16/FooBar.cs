using System;
using System.Windows.Forms;
using System.Drawing;

public class FooBar : Form {

  Button pushMeButton;
  Button barButton;

  public FooBar() {

    // Create Button
    pushMeButton = new Button(); 
    pushMeButton.Text = "Foo";
    pushMeButton.Height = 35;
    pushMeButton.Width = 80;
    pushMeButton.Top = 10;
    pushMeButton.Left = 28;

    barButton = new Button(); 
    barButton.Text = "Bar";
    barButton.Height = 35;
    barButton.Width = 80;
    barButton.Top = 60;
    barButton.Left = 28;

    // Add the Button to the Form
    this.Controls.Add(pushMeButton);
    this.Controls.Add(barButton);

    // Size the Form and make it visible
    this.Height = 130;
    this.Width = 145;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Visible = true;
  }

  static void Main() {
    Application.Run(new FooBar());
  }
}
