// MainForm.cs - Chapter 16 version.

// Copyright 2004 by Jacquie Barker and Grant Palmer - all rights reserved.

// A GUI class.

using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

public class MainForm : Form {

  private Button dropButton, saveButton;
  private Button addButton, logOffButton;
  private TextBox ssnTextBox, nameTextBox, totalTextBox; 
  private ListBox scheduleListBox, registeredListBox; 
  private Label classScheduleLabel, ssnLabel;
  private Label nameLabel, totalCourseLabel;
  private Label registeredLabel;
  private PasswordForm passwordDialog;

  // Maintain a handle on the Student who is logged in.
  // (Whenever this is set to null, nobody is officially logged on.)
  private Student currentUser;

  public MainForm() {

    currentUser = null;

    // Create left-hand side labels
    int labelVertSpace = 40;
    int labelLeft = 5;

    ssnLabel = new Label();
    ssnLabel.Text = "SSN:";
    ssnLabel.Font = new Font(ssnLabel.Font, FontStyle.Bold);
    ssnLabel.AutoSize = true;
    ssnLabel.Top = 5;
    ssnLabel.Left = labelLeft;

    nameLabel = new Label();
    nameLabel.Text = "Name:";
    nameLabel.Font = new Font(nameLabel.Font, FontStyle.Bold);
    nameLabel.AutoSize = true;
    nameLabel.Top = ssnLabel.Top + labelVertSpace;
    nameLabel.Left = labelLeft;

    totalCourseLabel = new Label();
    totalCourseLabel.Text = "Total Courses:";
    totalCourseLabel.Font = new Font(totalCourseLabel.Font, FontStyle.Bold);
    totalCourseLabel.AutoSize = true;
    totalCourseLabel.Top = nameLabel.Top + labelVertSpace;
    totalCourseLabel.Left = labelLeft;

    registeredLabel = new Label();
    registeredLabel.Text = "Registered For:";
    registeredLabel.Font = new Font(registeredLabel.Font, FontStyle.Bold);
    registeredLabel.AutoSize = true;
    registeredLabel.Top = totalCourseLabel.Top + labelVertSpace;
    registeredLabel.Left = labelLeft;

    // Create TextBox components
    ssnTextBox = new TextBox();
    ssnTextBox.Width = 140;
    ssnTextBox.AutoSize = true;
    ssnTextBox.Top = ssnLabel.Top;
    ssnTextBox.Left = totalCourseLabel.Right + 15;

    // Assign an event handler to the SSN TextBox
    ssnTextBox.KeyUp += new KeyEventHandler(SsnTextBoxKeyUp);

    nameTextBox = new TextBox();
    nameTextBox.Width = 140;
    nameTextBox.AutoSize = true;
    nameTextBox.Top = nameLabel.Top;
    nameTextBox.Left = totalCourseLabel.Right + 15;
    nameTextBox.ReadOnly = true;

    totalTextBox = new TextBox();
    totalTextBox.Width = 20;
    totalTextBox.AutoSize = true;  
    totalTextBox.Top = totalCourseLabel.Top;
    totalTextBox.Left = totalCourseLabel.Right + 15;
    totalTextBox.ReadOnly = true;

    // Create right-hand side labels
    classScheduleLabel = new Label();
    classScheduleLabel.Text = "--- Schedule of Classes ---";
    classScheduleLabel.Font = new Font(classScheduleLabel.Font, FontStyle.Bold);
    classScheduleLabel.AutoSize = true;
    classScheduleLabel.Top = 5;
    classScheduleLabel.Left = ssnTextBox.Right + 55;

    // Create "Schedule of Classes" ListBox Component
    scheduleListBox = new ListBox();
    scheduleListBox.Width = 210;
    scheduleListBox.Height = 225;
    scheduleListBox.Top = classScheduleLabel.Bottom + 5;
    scheduleListBox.Left = ssnTextBox.Right + 30;

    // Display an alphabetically sorted course catalog list
    // in the scheduleListBox component.
    ArrayList sortedSections = SRS.scheduleOfClasses.GetSortedSections();
    for(int i = 0; i < sortedSections.Count; ++i) {
      scheduleListBox.Items.Add(sortedSections[i]);
    }

    // Create "Registered For" ListBox Component
    registeredListBox = new ListBox();
    registeredListBox.Width = 210;
    registeredListBox.Top = registeredLabel.Bottom + 5;
    registeredListBox.Height = scheduleListBox.Bottom - registeredListBox.Top + 3;
    registeredListBox.Left = labelLeft;

    // Add event handlers to the ListBox components
    scheduleListBox.SelectedIndexChanged += 
                new EventHandler(ScheduleSelectionChanged);
    registeredListBox.SelectedIndexChanged += 
                new EventHandler(RegisteredSelectionChanged);

    // Create buttons
    int buttonHeight = 50;
    int buttonWidth = 80;
    int buttonTop = 260;

    dropButton = new Button(); 
    dropButton.Text = "Drop";
    dropButton.Height = buttonHeight;
    dropButton.Width = buttonWidth;
    dropButton.Top = buttonTop;
    dropButton.Left = 10;

    saveButton = new Button(); 
    saveButton.Text = "Save My Schedule";
    saveButton.Height = buttonHeight;
    saveButton.Width = buttonWidth;
    saveButton.Top = buttonTop;
    saveButton.Left = dropButton.Right + 5;

    addButton = new Button(); 
    addButton.Text = "Add";
    addButton.Height = buttonHeight;
    addButton.Width = buttonWidth;
    addButton.Top = buttonTop;
    addButton.Left = saveButton.Right + 100;

    logOffButton = new Button(); 
    logOffButton.Text = "Log Off";
    logOffButton.Height = buttonHeight;
    logOffButton.Width = buttonWidth;
    logOffButton.Top = buttonTop;
    logOffButton.Left = addButton.Right + 5;

    // Assign event handlers to the Buttons
    addButton.Click += new EventHandler(AddButtonClicked);
    dropButton.Click += new EventHandler(DropButtonClicked);
    saveButton.Click += new EventHandler(SaveButtonClicked);
    logOffButton.Click += new EventHandler(LogOffButtonClicked);

    // Initialize the buttons to their proper enabled/disabled
    // state.
    ResetButtons();

    // Add the GUI components to the form
    this.Controls.Add(dropButton);
    this.Controls.Add(saveButton);
    this.Controls.Add(addButton);
    this.Controls.Add(logOffButton);
    this.Controls.Add(classScheduleLabel);
    this.Controls.Add(ssnLabel);
    this.Controls.Add(ssnTextBox);
    this.Controls.Add(nameLabel);
    this.Controls.Add(nameTextBox);
    this.Controls.Add(totalCourseLabel);
    this.Controls.Add(totalTextBox);
    this.Controls.Add(registeredLabel);
    this.Controls.Add(scheduleListBox);
    this.Controls.Add(registeredListBox);

    // Set some appearance properties for the Form
    this.Text = "Student Registration System";
    this.Height = 350;
    this.Width = 500;
    this.MinimumSize = this.Size;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Visible = true;
  }

  // event handling method for the "Drop" Button
  public void DropButtonClicked(object source, EventArgs e) {
    // Determine which section is selected (note that we must
    // cast it, as it is returned as an Object reference).
    Section selected = (Section)registeredListBox.SelectedItem;

    // Drop the course.
    selected.Drop(currentUser);

    // Display a confirmation message.
    string message = "Course " + 
       selected.RepresentedCourse.CourseNo + " dropped.";
    MessageBox.Show(message, "Request Successful",
            MessageBoxButtons.OK, MessageBoxIcon.Information);

    // Update the list of sections that 
    // this student is registered for.
    registeredListBox.Items.Clear();
    IEnumerator ie = currentUser.GetEnrolledSections();
    while ( ie.MoveNext() ) {
      registeredListBox.Items.Add((Section)ie.Current);
    }

    // Update the field representing student's course total.
    totalTextBox.Text = "" + currentUser.GetCourseTotal();

    // Check states of the various buttons.
    ResetButtons();
  }

  // event handling method for the "Save" Button
  public void SaveButtonClicked(object source, EventArgs e) {
    bool success = currentUser.Persist();
    if (success) {
      // Let the user know that his/her
      // schedule was successfully saved.
      MessageBox.Show("Schedule saved", "Schedule Saved",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    else {
      // Let the user know that there was a problem.
      string message = "Problem saving your " +
                       "schedule; please contact " +
                       "SRS Support Staff for assistance.";
      MessageBox.Show(message, "Problem Saving Schedule",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
  }

  // event handling method for the "Add" Button
  public void AddButtonClicked(object source, EventArgs e) {
    // Determine which section is selected (note that we must
    // cast it, as it is returned as an Object reference).
    Section selected = (Section)scheduleListBox.SelectedItem;

    // Attempt to enroll the student in the section, noting
    // the status code that is returned.
    int status = selected.Enroll(currentUser);

    // Report the status to the user.
    if (status == Section.SECTION_FULL) {
      MessageBox.Show("Sorry - that section is full.", "Request Denied",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
    else {
      if (status == Section.PREREQ_NOT_SATISFIED) {
        string message = "You haven't satisfied all " +
                  "of the prerequisites for this course.";
        MessageBox.Show(message, "Request Denied",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
      else {
        if (status == Section.PREVIOUSLY_ENROLLED) {
          string message = "You are enrolled in or have successfully " +
                    "completed a section of this course.";
          MessageBox.Show(message, "Request Denied",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        else {  // success!
          string message = "Seat confirmed in " +
             selected.RepresentedCourse.CourseNo + ".";
          MessageBox.Show(message, "Request Successful",
                 MessageBoxButtons.OK, MessageBoxIcon.Information);

          // Update the list of sections 
          // that this student is registered for.
          registeredListBox.Items.Clear();
          IEnumerator ie = currentUser.GetEnrolledSections();
          while ( ie.MoveNext() ) {
            registeredListBox.Items.Add((Section)ie.Current);
          }

          // Update the field representing student's course total.
          totalTextBox.Text = "" + currentUser.GetCourseTotal();

          // Clear the selection in the schedule of classes list.
          scheduleListBox.SelectedItem = null;
        }
      }
    }

    // Check states of the various buttons.
    ResetButtons();
  }

  // event handling method for "Log Off" Button
  public void LogOffButtonClicked(object source, EventArgs e) {
    ClearFields();
    ssnTextBox.Text = "";
    currentUser = null;

    // Clear the selection in the
    // schedule of classes list.
    scheduleListBox.SelectedItem = null;

    // Check states of the various buttons.
    ResetButtons();

  }

  // event handling method for the "Schedule of Classes" ListBox
  public void ScheduleSelectionChanged(object source, EventArgs e) {
    // When an item is selected in this list,
    // we clear the selection in the other list.
    if (scheduleListBox.SelectedItem != null)  {
      registeredListBox.SelectedItem = null;
    }

    // reset the enabled state of the buttons
    ResetButtons();
  }

  // event handling method for the "Registered For:" ListBox
  public void RegisteredSelectionChanged(object source, EventArgs e) {
    // When an item is selected in this list,
    // we clear the selection in the other list.
    if (registeredListBox.SelectedItem != null)  {
      scheduleListBox.SelectedItem = null;
    }

    // reset the enabled state of the buttons
    ResetButtons();
  }

  // event handling method for the ssn TextBox
  public void SsnTextBoxKeyUp(object source, KeyEventArgs e) {
    // We only want to act if the Enter key is pressed
    if ( e.KeyCode == Keys.Enter ) {
  
      // First, clear the fields reflecting the
      // previous student's information.
      ClearFields();

      // We'll try to construct a Student based on
      // the ssn we read, and if a file containing
      // Student's information cannot be found,
      // we have a problem.

      currentUser = new Student(ssnTextBox.Text);

      // Test to see if the Student fields were properly
      // initialized. If not, reset currentUser to null
      // and display a message box

      if (!currentUser.StudentSuccessfullyInitialized()) {
        // Drat!  The ID was invalid.
        currentUser = null;

        // Let the user know that login failed,
        string message = "Invalid student ID; please try again.";
        MessageBox.Show(message, "Invalid Student ID",
                 MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
      else {
        // Hooray!  We found one!  Now, we need
        // to request and validate the password.
        passwordDialog = new PasswordForm();
        passwordDialog.ShowDialog(this);

        string password = passwordDialog.Password;
        passwordDialog.Dispose();

        if (currentUser.ValidatePassword(password)) {
          // Let the user know that the
          // login succeeded.
          string message = 
               "Log in succeeded for " + currentUser.Name + ".";
          MessageBox.Show(message, "Log In Succeeded",
                 MessageBoxButtons.OK, MessageBoxIcon.Information);

          // Load the data for the current user into the TextBox and
          // ListBox components.
          SetFields(currentUser);
        }
        else {
          // The ssn was okay, but the password validation failed;
          // notify the user of this.
          string message = "Invalid password; please try again.";
          MessageBox.Show(message, "Invalid Password",
                 MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
      }
      // Check states of the various buttons.
      ResetButtons();
    }
  }

  // These are private housekeeping methods

  // Because there are so many different situations in which one or
  // more buttons need to be (de)activated, and because the logic is
  // so complex, we centralize it here and then just call this method
  // whenever we need to check the state of one or more of the buttons.  
  // It is a tradeoff of code elegance for execution efficiency:  
  // we are doing a bit more work each time (because we don't need to 
  // reset all four buttons every time), but since the execution time
  // is minimal, this seems like a reasonable tradeoff.
  private void ResetButtons() {
    // There are four conditions which collectively govern the
    // state of each button:
    //	
    // o  Whether a user is logged on or not.
    bool isLoggedOn;
    if (currentUser != null) {
      isLoggedOn = true;
    }
    else {
      isLoggedOn = false;
    }
		
    // o   Whether the user is registered for at least one course.
    bool atLeastOne;
    if (currentUser != null && currentUser.GetCourseTotal() > 0) {
      atLeastOne = true;
    }
    else {
      atLeastOne = false;
    }


    // o   Whether a registered course has been selected.
    bool courseSelected;
    if (registeredListBox.SelectedItem == null) {
      courseSelected = false;
    }
    else {
      courseSelected = true;
    }
		
    // o   Whether an item is selected in the Schedule of Classes.
    bool catalogSelected;
    if (scheduleListBox.SelectedItem == null)  {
      catalogSelected = false;
    }
    else {
      catalogSelected = true;
    }

    // Now, verify the conditions on a button-by-button basis.

    // Drop button:
    if (isLoggedOn && atLeastOne && courseSelected) {
      dropButton.Enabled = true;
    }
    else {
      dropButton.Enabled = false;
    }

    // Add button:
    if (isLoggedOn && catalogSelected) {
      addButton.Enabled = true;
    }
    else {
      addButton.Enabled = false;
    }

    // Save My Schedule button:
    if (isLoggedOn) {
      saveButton.Enabled = true;
    }
    else {
      saveButton.Enabled = false;
    }

    // Log Off button:
    if (isLoggedOn) {
      logOffButton.Enabled = true;  
    }
    else {
      logOffButton.Enabled = false;  
    }
  }

  // Called whenever a user is logged off.
  private void ClearFields() {
    nameTextBox.Text = "";
    totalTextBox.Text = "";
    registeredListBox.Items.Clear();
  }

  // Set the various fields, lists, etc. to reflect the information
  // associated with a particular student.  (Used when logging in.)
  private void SetFields(Student theStudent) {
    nameTextBox.Text = theStudent.Name;
    int total = theStudent.GetCourseTotal();
    totalTextBox.Text = ""+total;

    // If the student is registered for any courses, list these, too.
    if (total > 0) {
      // Use the GetEnrolledSections() method to obtain a list
      // of the sections that the student is registered for and
      // add the sections to the registered ListBox

      IEnumerator e = theStudent.GetEnrolledSections();
      while ( e.MoveNext() ) {
        registeredListBox.Items.Add((Section)e.Current);
      }
    }
  }
}
