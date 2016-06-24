// Course.cs - Chapter 15 version.

// Copyright 2004 by Jacquie Barker  and Grant Palmer - all rights reserved.

// A MODEL class.

using System;
using System.Collections;

public class Course {
  //------------
  // Attributes.
  //------------

  private string courseNo;
  private string courseName;
  private double credits;
  private ArrayList offeredAsSection; // of Section object references
  private ArrayList prerequisites; // of Course object references
	
  //----------------
  // Constructor(s).
  //----------------

  // Initialize the attribute values using the set 
  // accessor of the associated property.

  public Course(string cNo, string cName, double credits) {
    this.CourseNo = cNo;
    this.CourseName = cName;
    this.Credits = credits;

    // Note that we must instantiate empty ArrayLists.

    offeredAsSection = new ArrayList();
    prerequisites = new ArrayList();
  }

  //-----------------
  // properties.
  //-----------------

  public string CourseNo {
    get {
      return courseNo;
    }
    set {
      courseNo = value;
    }
  }

  public string CourseName {
    get {
      return courseName;
    }
    set {
      courseName = value;
    }
  }

  public double Credits {
    get {
      return credits;
    }
    set {
      credits = value;
    }
  }

  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  // Used for testing purposes.

  public void Display() {
    Console.WriteLine("Course Information:");
    Console.WriteLine("\tCourse No.:  "+this.CourseNo);
    Console.WriteLine("\tCourse Name:  "+this.CourseName);
    Console.WriteLine("\tCredits:  "+this.Credits);
    Console.WriteLine("\tPrerequisite Courses:");

    // We take advantage of another of the Course class's
    // methods in stepping through all of the prerequisites.

    IEnumerator e = GetPrerequisites();
    while ( e.MoveNext() ) {
      Course c = (Course)e.Current;
      Console.WriteLine("\t\t" + c.ToString());
    }

    // Note use of Write vs. WriteLine in next line of code.

    Console.Write("\tOffered As Section(s):  ");
    for(int i=0; i<offeredAsSection.Count; i++) {
      Section s = (Section)offeredAsSection[i];
      Console.Write(s.SectionNo + " ");
    }

    // Print a blank line.
    Console.WriteLine("");
  }
	
  public override string ToString() {
    return this.CourseNo + ":  "+this.CourseName;
  }

  public void AddPrerequisite(Course c) {
    prerequisites.Add(c);
  }

  public bool HasPrerequisites() {
    if ( prerequisites.Count > 0 ) {
      return true;
    }  
    else {
      return false;
    }
  }

  public IEnumerator GetPrerequisites() {
    return prerequisites.GetEnumerator();
  }

  public Section ScheduleSection(int secNo, char day, string time, 
                                 string room, int capacity) {
    // Create a new Section (note the creative way in
    // which we are assigning a section number) ...
    Section s = new Section(secNo, day, time, this, room, capacity);
		
    // ... and then remember it!
    offeredAsSection.Add(s);
		
    return s;
  }
}
