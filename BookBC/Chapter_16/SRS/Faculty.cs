// Faculty.cs - Chapter 15 version.

// Copyright 2004 by Jacquie Barker and Grant Palmer - all rights reserved.

// An IMPLEMENTATION class.

using System;
using System.Collections;
using System.IO;

public class Faculty : CollectionWrapper {
  //------------
  // Attributes.
  //------------

  // This Hashtable stores Professor object references, using
  // the (String) ssn of the Professor as the key.

  private Hashtable professors;

  //----------------
  // Constructor(s).
  //----------------

  public Faculty() {
    // Instantiate a new Hashtable.

    professors = new Hashtable();
  }

  //-----------------
  // properties.
  //-----------------


  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  // Used for testing purposes.
	
  public void Display() {
    Console.WriteLine("Faculty:");
    Console.WriteLine("");

    // Step through the Hashtable and display all entries.

    IDictionaryEnumerator e = professors.GetEnumerator();

    while (e.MoveNext()) {
      Professor p = (Professor)e.Value;
      p.Display();
      Console.WriteLine("");
    }
  }

  public void AddProfessor(Professor p) {
    professors.Add(p.Ssn, p);
  }

  public override void ParseData(string line) {
    // We're going to parse tab-delimited records into
    // four attributes -- name, ssn, title, and dept --
    // and then call the Professor constructor to fabricate a new
    // professor.

    // We'll use the Split() method of the String class to split
    // the line we read from the file into substrings using tabs 
    // as the delimiter.

    string[] strings = line.Split('\t');

    // Now assign the value of the attributes to the appropriate
    // substring

    string name = strings[0];
    string ssn = strings[1];
    string title = strings[2];
    string dept = strings[3];

    // Call the constructor ...
    Professor p = new Professor(name, ssn, title, dept);
    AddProfessor(p);
  }

  public Professor FindProfessor(string ssn) {
    return (Professor)professors[ssn];
  }

  // We have to read a second file containing the teaching
  // assignments.
  // This next version is used when reading in the file that defines
  // teaching assignments.

  public override void ParseData2(string line) {
    // We're going to parse tab-delimited records into
    // two values, representing the professor's SSN
    // and the section number that he/she is going to teach.

    // Once again we'll make use of the Split() method to split
    // the line into substrings using tabs as the delimiter

    string[] strings = line.Split('\t');

    // Now assign the value of the attributes to the appropriate
    // substring

    string ssn = strings[0];

    // The full section number is a concatenation of the
    // course no. and section no., separated by a hyphen;
    // e.g., "ART101 - 1".

    string fullSectionNo = strings[1];

    // Look these two objects up in the appropriate collections.
    // Note that having made scheduleOfClasses a public
    // static attribute of the SRS class helps!
  
    Professor p = FindProfessor(ssn); 
    Section s = SRS.scheduleOfClasses.FindSection(fullSectionNo); 
    if (p != null && s != null) {
      p.AgreeToTeach(s);
    }
  }

  // Test scaffold.
  public static void Main(string[] args) {
    Faculty f = new Faculty();
    f.InitializeObjects("Faculty.dat", true);

    // We cannot test the next feature, because the code
    // of parseData2() expects the SRS.scheduleOfClasses
    // collection object to have been instantiated, but 
    // it will not have been if we are running this test
    // scaffold instead.
    // f.InitializeObjects("TeachingAssignments.dat", false);

    f.Display();
  }
}
