// Student.cs - Chapter 14 version.

// Copyright 2004 by Jacquie Barker and Grant Palmer - all rights reserved.

// A MODEL class.

using System;
using System.Collections;

public class Student : Person {
  //------------
  // Fields.
  //------------

  private string major;
  private string degree;
  private Transcript transcript;
  private ArrayList attends; // of Sections
	
  //----------------
  // Constructor(s).
  //----------------

  // Reuse the code of the parent's constructor.
  // Initialize the field values using the set 
  // accessor of the associated property.

  public Student(string name, string ssn, 
                 string major, string degree) : base(name, ssn) {

    this.Major = major;
    this.Degree = degree;

    // Create a brand new Transcript.

    this.Transcript = new Transcript(this);

    // Note that we must instantiate an empty ArrayList.

    attends = new ArrayList();
  }
	
  // A second form of constructor, used when a Student has not yet
  // declared a major or degree.
  // Reuse the code of the other Student constructor.

  public Student(string name, string ssn) : this(name, ssn, "TBD", "TBD"){
  }

  //-----------------
  // properties.
  //-----------------

  public string Major {
    get {
      return major;
    }
    set {
      major = value;
    }
  }

  public string Degree {
    get {
      return degree;
    }
    set {
      degree = value;
    }
  }

  public Transcript Transcript {
    get {
      return transcript;
    }
    set {
      transcript = value;
    }
  }

  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  // Used for testing purposes.

  public override void Display() {
    // First, let's display the generic Person info.

    base.Display();
		
    // Then, display Student-specific info.

    Console.WriteLine("Student-Specific Information:");
    Console.WriteLine("\tMajor:  "+this.Major);
    Console.WriteLine("\tDegree:  "+this.Degree);
    DisplayCourseSchedule();
    PrintTranscript();
  }	
	
  // We are forced to program this method because it is specified
  // as an abstract method in our parent class (Person); failing to
  // do so would render the Student class abstract, as well.
  //
  // For a Student, we wish to return a String as follows:
  //
  // 	Joe Blow (123-45-6789) [Master of Science - Math]

  public override string ToString() {
    return this.Name + " (" + this.Ssn + ") [" + this.Degree +
           " - " + this.Major + "]";
  }

  public void DisplayCourseSchedule() {
    // Display a title first.

    Console.WriteLine("Course Schedule for " + this.Name);
		
    // Step through the ArrayList of Section objects, 
    // processing these one by one.

    for (int i=0; i<attends.Count; i++) {
      Section s = (Section)attends[i];

      // Since the attends ArrayList contains Sections that the
      // Student took in the past as well as those for which
      // the Student is currently enrolled, we only want to
      // report on those for which a grade has not yet been
      // assigned.
            
      if ( s.GetGrade(this) == null ) {
        Console.WriteLine("\tCourse No.:  "+ 
                           s.RepresentedCourse.CourseNo);
        Console.WriteLine("\tSection No.:  "+s.SectionNo);
        Console.WriteLine("\tCourse Name:  "+ 
                           s.RepresentedCourse.CourseName);
        Console.WriteLine("\tMeeting Day and Time Held:  "+
                          s.DayOfWeek + " - "+
                          s.TimeOfDay);
        Console.WriteLine("\tRoom Location:  "+s.Room);
        Console.WriteLine("\tProfessor's Name:  "+
                           s.Instructor.Name);
        Console.WriteLine("\t-----");
      }
    }
  }
	
  public void AddSection(Section s) {
    attends.Add(s);
  }
	
  public void DropSection(Section s) {
    attends.Remove(s);
  }
	
  // Determine whether the Student is already enrolled in THIS
  // EXACT Section.

  public bool IsEnrolledIn(Section s) {
    if (attends.Contains(s)) {
      return true;
    } 
    else {
      return false;
    }
  }
		
  public void PrintTranscript() {
    this.Transcript.Display();
  }

  // Determine whether the Student is already enrolled in ANOTHER
  // Section of this SAME Course.

  public bool IsCurrentlyEnrolledInSimilar(Section s1) {
    bool foundMatch = false;
    Course c1 = s1.RepresentedCourse;
    IEnumerator e = GetEnrolledSections();
    while ( e.MoveNext() ) {
      Section s2 = (Section)e.Current;
      Course c2 = s2.RepresentedCourse;
      if (c1 == c2) {
        // There is indeed a Section in the attends
        // ArrayList representing the same Course.
        // Check to see if the Student is CURRENTLY
        // ENROLLED (i.e., whether or not he has
        // yet received a grade).  If there is no
        // grade, he/she is currently enrolled; if
        // there is a grade, then he/she completed
        // the course some time in the past.
        if ( s2.GetGrade(this) == null ) {
          // No grade was assigned!  This means
          // that the Student is currently
          // enrolled in a Section of this
          // same Course.
          foundMatch = true;
          break;
        }
      }
    }

    return foundMatch;
  }
		
  public IEnumerator GetEnrolledSections() {
    return attends.GetEnumerator();
  }
}
