// ScheduleOfClasses.cs - Chapter 14 version.

// Copyright 2004 by Jacquie Barker and Grant Palmer - all rights reserved.

// A MODEL class.

using System;
using System.Collections;

public class ScheduleOfClasses {
  //------------
  // Fields.
  //------------

  private string semester;

  // This Hashtable stores Section object references, using
  // a String concatenation of course no. and section no. as the
  // key, e.g., "MATH101 - 1".

  private Hashtable sectionsOffered; 

  //----------------
  // Constructor(s).
  //----------------

  // Initialize the field values using the set 
  // accessor of the associated property.

  public ScheduleOfClasses(string semester) {
    this.Semester = semester;
		
    // Instantiate a new Hashtable.

    sectionsOffered = new Hashtable();
  }

  //-----------------
  // properties.
  //-----------------

  public string Semester {
    get {
      return semester;
    }
    set {
      semester = value;
    }
  }

  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  // Used for testing purposes.
	
  public void Display() {
    Console.WriteLine("Schedule of Classes for "+this.Semester);
    Console.WriteLine("");

    // Step through the Hashtable and display all entries.

    IDictionaryEnumerator e = sectionsOffered.GetEnumerator();

    while ( e.MoveNext() ) {
      Section s = (Section)e.Value;
      s.Display();
      Console.WriteLine("");
    }
  }

  public void AddSection(Section s) {
    // We formulate a key by concatenating the course no.
    // and section no., separated by a hyphen.

    string key = s.RepresentedCourse.CourseNo+ 
                 " - "+s.SectionNo;
    sectionsOffered.Add(key, s);

    // Bidirectionally hook the ScheduleOfClasses back to the Section.

    s.OfferedIn = this;
  }
}
