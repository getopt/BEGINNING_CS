// SRS.cs - Chapter 16 version.

// Copyright 2004 by Jacquie Barker and Grant Palmer - all rights reserved.

// A main driver for the GUI version of the SRS.

using System;
using System.Collections;
using System.Windows.Forms;

public class SRS {
  // We can effectively create "global" data by declaring
  // public static attributes in the main class.  

  // Entry points/"roots" for getting at objects.  

  public static Faculty faculty = new Faculty();
  public static CourseCatalog courseCatalog = new CourseCatalog(); 
  public static ScheduleOfClasses scheduleOfClasses = 
		      new ScheduleOfClasses("SP2004");

  // We don't create a collection for Student objects, because
  // we're only going to handle one Student at a time -- namely,
  // whichever Student is logged on.

  static void Main() {
    // Initialize the key objects by reading data from files.
    // Setting the second argument to true causes the
    // InitializeObjects() method to use the ParseData()
    // method instead of ParseData2().

    faculty.InitializeObjects("Faculty.dat", true);
    courseCatalog.InitializeObjects("CourseCatalog.dat", true);
    scheduleOfClasses.InitializeObjects("SoC_SP2004.dat", true);

    // We'll handle the students differently:  that is,
    // rather than loading them all in at application outset,
    // we'll pull in the data that we need just for one
    // Student when that Student logs on -- see the Student
    // class constructor for the details.

    // Establish some prerequisites (c1 => c2 => c3 => c4).
    // Setting the second argument to false causes the
    // InitializeObjects() method to use the ParseData2()
    // method instead of ParseData().

    courseCatalog.InitializeObjects("Prerequisites.dat", false);

    // Recruit a professor to teach each of the sections.

    faculty.InitializeObjects("TeachingAssignments.dat", false);

    // Create and display an instance of the main GUI window

    Application.Run(new MainForm());
  }
}
