// Transcript.cs - Chapter 14 version.

// Copyright 2004 by Jacquie Barker and Grant Palmer - all rights reserved.

// A MODEL class.

using System;
using System.Collections;

public class Transcript {
  //------------
  // Fields.
  //------------

  private ArrayList transcriptEntries; // of TranscriptEntry object references
  private Student studentOwner;

  //----------------
  // Constructor(s).
  //----------------

  // Initialize the field values using the set 
  // accessor of the associated property.

  public Transcript(Student s) {
    this.StudentOwner = s;

    // Need to instantiate a new ArrayList.

    transcriptEntries = new ArrayList();
  }

  //-----------------
  // properties.
  //-----------------

  public Student StudentOwner {
    get {
      return studentOwner;
    }
    set {
      studentOwner = value;
    }
  }

  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  public bool VerifyCompletion(Course c) {
    bool outcome = false;

    // Step through all TranscriptEntries, looking for one
    // which reflects a Section of the Course of interest.

    for (int i=0; i<transcriptEntries.Count; i++) {
      TranscriptEntry te = (TranscriptEntry)transcriptEntries[i];
      Section s = te.Section;

      if ( s.IsSectionOf(c) ) {
        // Ensure that the grade was high enough.

        if ( TranscriptEntry.PassingGrade(te.Grade) ) {
          outcome = true;

          // We've found one, so we can afford to
          // terminate the loop now.

          break;
        }
      }
    }

    return outcome;
  }

  public void AddTranscriptEntry(TranscriptEntry te) {
    transcriptEntries.Add(te);
  }

  // Used for testing purposes.

  public void Display() {
    Console.WriteLine("Transcript for:  " +
                       this.StudentOwner.ToString());

    if ( transcriptEntries.Count == 0 ) {
      Console.WriteLine("\t(no entries)");
    }  
    else {
      for (int i=0; i<transcriptEntries.Count; i++) {
        TranscriptEntry te = (TranscriptEntry)transcriptEntries[i];
        Section sec = te.Section;
        Course c = sec.RepresentedCourse;
        ScheduleOfClasses soc = sec.OfferedIn;

        Console.WriteLine("\tSemester:        "+soc.Semester);
        Console.WriteLine("\tCourse No.:      "+c.CourseNo);
        Console.WriteLine("\tCredits:         "+c.Credits);
        Console.WriteLine("\tGrade Received:  "+te.Grade);
        Console.WriteLine("\t-----");
      }
    }
  }
}
