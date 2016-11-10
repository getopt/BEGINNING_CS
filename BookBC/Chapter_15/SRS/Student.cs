// Student.cs - Chapter 15 version.

// Copyright 2004 by Jacquie Barker and Grant Palmer - all rights reserved.

// A MODEL class.

using System;
using System.Collections;
using System.IO;

public class Student : Person {
  //------------
  // Attributes.
  //------------

  private string major;
  private string degree;
  private Transcript transcript;
  private ArrayList attends; // of Sections
	
  //----------------
  // Constructor(s).
  //----------------

  public Student(string ssn) : this(){
    // First, construct a "dummy" Student object.  Then, 
    // attempt to pull this Student's information from the 
    // appropriate file (ssn.dat:  e.g., 111-11-1111.dat).  
    // The file consists of a header record, containing 
    // the student's basic info. (ssn, name, etc.), and 
    // 0 or more subsequent records representing a list of 
    // the sections that he/she is currently registered for.

    string line = null;
    StreamReader srIn = null;
  
    // Formulate the file name.

    string pathToFile = ssn + ".dat";

    try {
      // Open the file. 

      srIn = new StreamReader(new FileStream(pathToFile,FileMode.Open));

      // The first line in the file contains the header 
      // information, so we use ParseData() to process it.

      line = srIn.ReadLine();
      if (line != null) {
        ParseData(line);
      }

      // Remaining lines (if there are any) contain 
      // section references.  Note that we must 
      // instantiate an empty vector so that the 
      // ParseData2() method may insert
      // items into the ArrayList.

      attends = new ArrayList();

      line = srIn.ReadLine();

      // If there were no secondary records in the file,
      // this "while" loop won't execute at all.

      while (line != null) {
        ParseData2(line);
        line = srIn.ReadLine();
      }

      srIn.Close();
    }
    catch (FileNotFoundException f) {
      // Since we are encoding a "dummy" Student to begin
      // with, the fact that his/her name will be equal
      // to "???" flags an error.  We have included
      // a boolean method SuccessfullyInitialized() 
      // which allows client code to verify the success
      // or failure of this constructor (see code below).
      // So, we needn't do anything special in this
      // "catch" clause!
      Console.WriteLine(f);
    }
    catch (IOException i) {
      // See comments for FileNotFoundException above;
      // we needn't do anything special in this
      // "catch" clause, either!
      Console.WriteLine(i);
    }

    // Create a brand new Transcript.
    // (Ideally, we'd read in an existing Transcript from 
    // a file, but we're not bothering to do so in this
    // example).

    this.Transcript = new Transcript(this);
  }

  // A second form of constructor, used when a Student's data
  // file cannot be found for some reason.

  public Student() : base("???", "???") {
    // Reuse the code of the parent's (Person) constructor.
    // Question marks indicate that something went wrong!

    this.Major = "???";
    this.Degree = "???";

    // Placeholders for the remaining attributes (this
    // Student is invalid anyway).
		
    this.Transcript = new Transcript(this);
    attends = new ArrayList();
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

  public void ParseData(string line) {
    // We're going to parse tab-delimited records into
    // four attributes -- ssn, name, major, and degree. 

    // We'll use the Split() method of the String class to split
    // the line we read from the file into substrings using tabs 
    // as the delimiter.

    string[] strings = line.Split('\t');

    // Now assign the value of the attributes to the appropriate
    // substring

    this.Ssn = strings[0];
    this.Name = strings[1];
    this.Major = strings[2];
    this.Degree = strings[3];
  }

  public void ParseData2(string line) {
    // The full section number is a concatenation of the
    // course no. and section no., separated by a hyphen;
    // e.g., "ART101 - 1".

    string fullSectionNo = line.Trim();
    Section s = SRS.scheduleOfClasses.FindSection(fullSectionNo);

    // Note that we are using the Section class's enroll()
    // method to ensure that bidirectionality is established
    // between the Student and the Section.
    s.Enroll(this);
  }
	
	// Used after the constructor is called to verify whether or not
	// there were any file access errors.

  // Used after the constructor is called to verify whether or not
  // there were any file access errors.

  public bool StudentSuccessfullyInitialized() {
    if (this.Name.Equals("???")) {
      return false;
    }
    else {
      return true;
    }
  }

  // This method writes out all of the student's information to
  // his/her ssn.dat file when he/she logs off.
	
  public bool Persist() {
    FileStream fs = null;
    StreamWriter sw = null;
    try {
      // Attempt to create the ssn.dat file.  Note that
      // it will overwrite one if it already exists, which
      // is what we want to have happen.

      string file = this.Ssn + ".dat";
      fs = new FileStream(file, FileMode.OpenOrCreate);
      sw = new StreamWriter(fs);

      // First, we output the header record as a tab-delimited
      // record.

      sw.WriteLine(this.Ssn + "\t" + this.Name + "\t" +
                   this.Major + "\t" + this.Degree);

      // Then, we output one record for every Section that
      // the Student is enrolled in.

      for (int i = 0; i < attends.Count; i++) {
        Section s = (Section)attends[i];
        sw.WriteLine(s.GetFullSectionNo());
      }

      sw.Close();
    }
    catch (IOException e) {
      // Signal that an error has occurred.
      Console.WriteLine(e);
      return false;
    }
		
    // All is well!

    return true;
  }
}
