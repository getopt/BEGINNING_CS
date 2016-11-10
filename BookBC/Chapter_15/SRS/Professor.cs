// Professor.cs - Chapter 15 version.

// Copyright 2004 by Jacquie Barker and Grant Palmer - all rights reserved.

// A MODEL class.

using System;
using System.Collections;

public class Professor : Person {
  //------------
  // Attributes.
  //------------

  private string title;
  private string department;
  private ArrayList teaches; // of Sections

  //----------------
  // Constructor(s).
  //----------------

  // Reuse the parent constructor with two arguments.
  // Initialize the attribute values using the set 
  // accessor of the associated property.

  public Professor(string name, string ssn, 
                   string title, string dept) : base(name, ssn) {

    this.Title = title;
    this.Department = dept;

    // Note that we must instantiate an empty vector.

    teaches = new ArrayList();
  }
		
  //-----------------
  // properties.
  //-----------------

  public string Title {
    get {
      return title;
    }
    set {
      title = value;
    }
  }

  public string Department {
    get {
      return department;
    }
    set {
      department = value;
    }
  }

  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  // Used for testing purposes.

  public override void Display() {
    // First, let's display the generic Person info.

    base.Display();
		
    // Then, display Professor-specific info.

    Console.WriteLine("Professor-Specific Information:");
    Console.WriteLine("\tTitle:  " + this.Title);
    Console.WriteLine("\tTeaches for Dept.:  " + this.Department);
    DisplayTeachingAssignments();
  }
	
  // We are forced to program this method because it is specified
  // as an abstract method in our parent class (Person); failing to
  // do so would render the Professor class abstract, as well.
  //
  // For a Professor, we wish to return a String as follows:
  //
  // 	Josephine Blow (Associate Professor, Math)

  public override string ToString() {
    return this.Name + " (" + this.Title + ", " +
		       this.Department + ")";
  }

  public void DisplayTeachingAssignments() {
    Console.WriteLine("Teaching Assignments for " + this.Name + ":");
		
    // We'll step through the teaches ArrayList, processing
    // Section objects one at a time.

    if (teaches.Count == 0) {
      Console.WriteLine("\t(none)");
    } 
    else {
      for (int i=0; i<teaches.Count; i++) {
        // Because we are going to need to delegate a lot of the effort
        // of satisfying this request to the various Section objects that
        // comprise the Professor's teaching load, we "pull" the Section 
        // object once, and refer to it by a temporarily handle.

        Section s = (Section)teaches[i];

        // Note how we call upon the Section object to do
        // a lot of the work for us!

        Console.WriteLine("\tCourse No.:  "+
                           s.RepresentedCourse.CourseNo);
        Console.WriteLine("\tSection No.:  "+s.SectionNo);
        Console.WriteLine("\tCourse Name:  "+
                           s.RepresentedCourse.CourseName);
        Console.WriteLine("\tDay and Time:  "+
                           s.DayOfWeek+" - "+s.TimeOfDay);
			Console.WriteLine("\t-----");
      }
    }
  }
	
  public void AgreeToTeach(Section s) {
    teaches.Add(s);

    // We need to link this bidirectionally.
    s.Instructor = this;
  }
}
