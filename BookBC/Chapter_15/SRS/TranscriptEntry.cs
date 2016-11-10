// TranscriptEntry.cs - Chapter 15 version.

// Copyright 2004 by Jacquie Barker and Grant Palmer - all rights reserved.

// A MODEL class.

using System;

public class TranscriptEntry {
  //------------
  // Attributes.
  //------------

  private string grade;
  private Student student;
  private Section section;
  private Transcript transcript;

  //----------------
  // Constructor(s).
  //----------------

  // Initialize the attribute values using the set 
  // accessor of the associated property.

  public TranscriptEntry(Student s, string grade, Section se) {
    this.Student = s;
    this.Section = se;
    this.Grade = grade;

    // Obtain the Student's transcript ...

    Transcript t = s.Transcript;

    // ... and then hook the Transcript and the TranscriptEntry
    // together bidirectionally.

    this.Transcript = t;
    t.AddTranscriptEntry(this);
  }

  //-----------------
  // properties.
  //-----------------

  public Student Student {
    get {
      return student;
    }
    set {
      student = value;
    }
  }

  public Section Section {
    get {
      return section;
    }
    set {
      section = value;
    }
  }

  public string Grade {
    get {
      return grade;
    }
    set {
      grade = value;
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

  // These next two methods are declared to be static, so that they
  // may be used as utility methods.

  public static bool ValidateGrade(string grade) {
    bool outcome = false;

    if (grade.Equals("F") || grade.Equals("I")) {
      outcome = true;
    }

    if (grade.StartsWith("A") || grade.StartsWith("B") ||
        grade.StartsWith("C") || grade.StartsWith("D")) {
      if (grade.Length == 1) {
        outcome = true;
      } 
      else {
        if (grade.Length > 2) {
          outcome = false;
        } 
        else {
          if (grade.EndsWith("+") || grade.EndsWith("-")) {
            outcome = true;
          }  
          else  {
            outcome = false; 
          }
        }
      }
    }

    return outcome;
  }

  public static bool PassingGrade(string grade) {
    // First, make sure it is a valid grade.

    if ( !ValidateGrade(grade) ) {
      return false;
    }

    // Next, make sure that the grade is a D or better.

    if ( grade.StartsWith("A") || grade.StartsWith("B") ||
         grade.StartsWith("C") || grade.StartsWith("D")) {
      return true;
    } 
    else {
      return false;
    }
  }
}
