// SRS.cs - Chapter 15 version.

// Copyright 2004 by Jacquie Barker and Grant Palmer - all rights reserved.

// A main driver for the command-line driven version of the SRS, with
// file persistence added.

using System;
using System.Collections;

public class SRS {
  // We can effectively create "global" data by declaring
  // PUBLIC STATIC attributes in the main class.  

  // Entry points/"roots" for getting at objects.  

  public static Faculty faculty = new Faculty();
  public static CourseCatalog courseCatalog = new CourseCatalog(); 
  public static ScheduleOfClasses scheduleOfClasses = 
		      new ScheduleOfClasses("SP2004");

  // We don't create a collection for Student objects, because
  // we're only going to handle one Student at a time -- namely,
  // whichever Student is logged on.
  //? public static StudentBody studentBody = new StudentBody();

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

    // Let's temporarily create Students this way as a test,
    // to simulate Students logging on.  Note that only the
    // first Student has "preregistered" for courses based
    // on the content of his/her ssn.dat file (see Student.cs
    // for details).

    Student s1 = new Student("111-11-1111");
    Student s2 = new Student("222-22-2222");
    Student s3 = new Student("333-33-3333");

    // Establish some prerequisites (c1 => c2 => c3 => c4).
    // Setting the second argument to false causes the
    // InitializeObjects() method to use the ParseData2()
    // method instead of ParseData().

    courseCatalog.InitializeObjects("Prerequisites.dat", false);

    // Recruit a professor to teach each of the sections.
    // Setting the second argument to false causes the
    // InitializeObjects() method to use the ParseData2()
    // method instead of ParseData().

    faculty.InitializeObjects("TeachingAssignments.dat", false);

    // Let's have one Student try enrolling in something, so
    // that we can simulate his/her logging off and persisting
    // the enrollment data in the ssn.dat file (see Student.cs
    // for details).

    Section sec = scheduleOfClasses.FindSection("ART101 - 1");
    sec.Enroll(s2);
    s2.Persist();  // Check contents of 222-22-2222.dat!

    // Let's see if everything got initialized properly
    // by calling various display methods!
		
    Console.WriteLine("====================");
    Console.WriteLine("Course Catalog:");
    Console.WriteLine("====================");
    Console.WriteLine("");
    courseCatalog.Display();

    Console.WriteLine("====================");
    Console.WriteLine("Schedule of Classes:");
    Console.WriteLine("====================");
    Console.WriteLine("");
    scheduleOfClasses.Display();

    Console.WriteLine("======================");
    Console.WriteLine("Professor Information:");
    Console.WriteLine("======================");
    Console.WriteLine("");
    faculty.Display();

    Console.WriteLine("====================");
    Console.WriteLine("Student Information:");
    Console.WriteLine("====================");
    Console.WriteLine("");
    s1.Display();
    Console.WriteLine("");
    s2.Display();
    Console.WriteLine("");
    s3.Display();
  }
}
