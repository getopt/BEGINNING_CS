using System;
using System.Windows.Forms;
using System.Drawing;

public class PushMe2 : Form {

  Button pushMeButton;

  public PushMe2() {

    // Create Button
    pushMeButton = new Button(); 
    pushMeButton.Text = "Push Me";
    pushMeButton.Height = 60;
    pushMeButton.Width = 80;
    pushMeButton.Top = 60;
    pushMeButton.Left = 60;

    // Assign an event handler to the Button
    pushMeButton.Click += new EventHandler(ButtonClicked);

    // Add the Button to the Form
    this.Controls.Add(pushMeButton);

    // Size the Form and make it visible
    this.Height = 200;
    this.Width = 200;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Visible = true;
  }

  // event handling method for the "Push Me" Button
  public void ButtonClicked(object source, EventArgs e) {
    Button b = (Button)source;
    if ( b.Text == "Push Me" ) {
      b.Text = "Ouch";
    }
    else {
      b.Text = "Push Me";
    }
  }

  static void Main() {
    Application.Run(new PushMe2());
  }
}
