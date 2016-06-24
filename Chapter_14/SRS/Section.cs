// Section.cs - Chapter 14 version.

// Copyright 2004 by Jacquie Barker and Grant Palmer - all rights reserved.

// A MODEL class.

using System;
using System.Collections;

public class Section {
  //------------
  // Fields.
  //------------

  private int sectionNo;
  private char dayOfWeek;
  private string timeOfDay;
  private string room;
  private int seatingCapacity;
  private Course representedCourse;
  private ScheduleOfClasses offeredIn;
  private Professor instructor;

  // The enrolledStudents Hashtable stores Student object references,
  // using each Student's ssn as a String key.

  private Hashtable enrolledStudents; 

  // The assignedGrades Hashtable stores TranscriptEntry object
  // references, using a reference to the Student to whom it belongs 
  // as the key.

  private Hashtable assignedGrades; 
	
  // We use const int attributes to declare status codes 
  // that other classes can then inspect/interpret (see method
  // ReportStatus() in class SRS.java for an example of how these
  // are used).

  public const int SUCCESSFULLY_ENROLLED = 0;
  public const int SECTION_FULL = 1;
  public const int PREREQ_NOT_SATISFIED = 2;
  public const int PREVIOUSLY_ENROLLED = 3;

  //----------------
  // Constructor(s).
  //----------------

  // Initialize the field values using the set 
  // accessor of the associated property.

  public Section(int sNo, char day, string time, Course course,
		 string room, int capacity) {
    this.SectionNo = sNo;
    this.DayOfWeek = day;
    this.TimeOfDay = time;
    this.RepresentedCourse = course;
    this.Room = room;
    this.SeatingCapacity = capacity;

    // A Professor has not yet been identified.

    this.Instructor = null;

    // Note that we must instantiate empty hash tables.

    enrolledStudents = new Hashtable();
    assignedGrades = new Hashtable();
  }
									
  //-----------------
  // properties.
  //-----------------

  public int SectionNo {
    get {
      return sectionNo;
    }
    set {
      sectionNo = value;
    }
  }

  public char DayOfWeek {
    get {
      return dayOfWeek;
    }
    set {
      dayOfWeek = value;
    }
  }

  public string TimeOfDay {
    get {
      return timeOfDay;
    }
    set {
      timeOfDay = value;
    }
  }

  public Professor Instructor {
    get {
      return instructor;
    }
    set {
      instructor = value;
    }
  }

  public Course RepresentedCourse {
    get {
      return representedCourse;
    }
    set {
      representedCourse = value;
    }
  }

  public string Room {
    get {
      return room;
    }
    set {
      room = value;
    }
  }

  public int SeatingCapacity {
    get {
      return seatingCapacity;
    }
    set {
      seatingCapacity = value;
    }
  }

  public ScheduleOfClasses OfferedIn {
    get {
      return offeredIn;
    }
    set {
      offeredIn = value;
    }
  }

  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  // For a Section, we want its String representation to look
  // as follows:
  //
  //	MATH101 - 1 - M - 8:00 AM

  public override string ToString() {
    return this.RepresentedCourse.CourseNo+" - "+
           this.SectionNo+" - "+this.DayOfWeek+" - "+
           this.TimeOfDay;
  }

  // The full section number is a concatenation of the
  // course no. and section no., separated by a hyphen;
  // e.g., "ART101 - 1".

  public string GetFullSectionNo() {
    return this.RepresentedCourse.CourseNo+ 
           " - "+this.SectionNo;
  }

  public int Enroll(Student s) {
    // First, make sure that this Student is not already
    // enrolled for this Section, has not already enrolled
    // in another section of this class and that he/she has
    // NEVER taken and passed the course before.  
		
    Transcript transcript = s.Transcript;

    if (s.IsEnrolledIn(this) || 
        s.IsCurrentlyEnrolledInSimilar(this) ||
        transcript.VerifyCompletion(this.RepresentedCourse)) {
      return PREVIOUSLY_ENROLLED;
    }

    // If there are any prerequisites for this course,
    // check to ensure that the Student has completed them.

    Course c = this.RepresentedCourse;
    if (c.HasPrerequisites()) {
      IEnumerator e = c.GetPrerequisites(); 
      while ( e.MoveNext() ) {
        Course pre = (Course)e.Current;
	
        // See if the Student's Transcript reflects
        // successful completion of the prerequisite.

        if (!transcript.VerifyCompletion(pre)) {
          return PREREQ_NOT_SATISFIED;
        }
      }
    }
		
    // If the total enrollment is already at the
    // the capacity for this Section, we reject this 
    // enrollment request.

    if (!ConfirmSeatAvailability()) {
      return SECTION_FULL;
    }
		
    // If we made it to here in the code, we're ready to
    // officially enroll the Student.

    // Note bidirectionality:  this Section holds
    // onto the Student via the Hashtable, and then
    // the Student is given a handle on this Section.

    enrolledStudents.Add(s.Ssn, s);
    s.AddSection(this);
    return SUCCESSFULLY_ENROLLED;
  }
	
  // A private "housekeeping" method.

  private bool ConfirmSeatAvailability() {
    if ( enrolledStudents.Count < this.SeatingCapacity ) {
      return true;
    }  
    else {
      return false;
    }
  }

  public bool Drop(Student s) {
    // We may only drop a student if he/she is enrolled.

    if (!s.IsEnrolledIn(this)) {
      return false;
    }  
    else {
      // Find the student in our Hashtable, and remove it.

      enrolledStudents.Remove(s.Ssn);

      // Note bidirectionality.

      s.DropSection(this);
      return true;
    }
  }

  public int GetTotalEnrollment() {
    return enrolledStudents.Count;
  }	

  // Used for testing purposes.

  public void Display() {
    Console.WriteLine("Section Information:");
    Console.WriteLine("\tSemester:  "+this.OfferedIn.Semester);
    Console.WriteLine("\tCourse No.:  "+
                       this.RepresentedCourse.CourseNo);
    Console.WriteLine("\tSection No:  "+this.SectionNo);
    Console.WriteLine("\tOffered:  "+this.DayOfWeek + 
				   " at "+this.TimeOfDay);
    Console.WriteLine("\tIn Room:  "+this.Room);
    if ( this.Instructor != null ) {
      Console.WriteLine("\tProfessor:  "+this.Instructor.Name);
    }
    DisplayStudentRoster();
  }
	
  // Used for testing purposes.

  public void DisplayStudentRoster() {
    Console.Write("\tTotal of "+GetTotalEnrollment()+ 
                  " students enrolled");

    // How we punctuate the preceding message depends on 
    // how many students are enrolled (note that we used
    // a Write() vs. WriteLine() call above).

    if ( GetTotalEnrollment() == 0 ) {
      Console.WriteLine(".");
    }  
    else {
      Console.WriteLine(", as follows:");
    }

    // Use an Enumeration object to "walk through" the
    // hash table.

    IDictionaryEnumerator e = enrolledStudents.GetEnumerator();
    while ( e.MoveNext() ) {
      Student s = (Student)e.Value;
      Console.WriteLine("\t\t"+s.Name);
    }
  }

  // This method returns the value null if the Student has not
  // been assigned a grade.

  public string GetGrade(Student s) {
    // Retrieve the Student's transcript entry object, if one
    // exists, and in turn retrieve its assigned grade.
    // If we found no TranscriptEntry for this Student, return
    // a null value to signal this.

    TranscriptEntry te = (TranscriptEntry)assignedGrades[s];
    if (te != null) {
      string grade = te.Grade; 
      return grade;
    }  
    else {
      return null;
    }
  }

  public bool PostGrade(Student s, string grade) {
    // Make sure that we haven't previously assigned a
    // grade to this Student by looking in the Hashtable
    // for an entry using this Student as the key.  If
    // we discover that a grade has already been assigned,
    // we return a value of false to indicate that
    // we are at risk of overwriting an existing grade.  
    // (A different method, EraseGrade(), can then be written
    // to allow a Professor to change his/her mind.)

    if ( assignedGrades[s] != null ) {
      return false;
    }

    // First, we create a new TranscriptEntry object.  Note
    // that we are passing in a reference to THIS Section,
    // because we want the TranscriptEntry object,
    // as an association class ..., to maintain
    // "handles" on the Section as well as on the Student.
    // (We'll let the TranscriptEntry constructor take care of
    // "hooking" this T.E. to the correct Transcript.)

    TranscriptEntry te = new TranscriptEntry(s, grade, this);

    // Then, we "remember" this grade because we wish for
    // the connection between a T.E. and a Section to be
    // bidirectional.

    assignedGrades.Add(s, te);

    return true;
  }
	
  public bool IsSectionOf(Course c) {
    if (c == this.RepresentedCourse) {
      return true;
    } 
    else {
      return false;
    }
  }
}
