// CourseCatalog.cs - Chapter 15 version.

// Copyright 2004 by Jacquie Barker and Grant Palmer - all rights reserved.

// An IMPLEMENTATION class.

using System;
using System.Collections;
using System.IO;

public class CourseCatalog : CollectionWrapper {
  //------------
  // Attributes.
  //------------

  // This Hashtable stores Course object references, using
  // the (String) course no. of the Course as the key.

  private Hashtable courses;

  //----------------
  // Constructor(s).
  //----------------

  public CourseCatalog() {
    // Instantiate a new Hashtable.

    courses = new Hashtable();
  }

  //-----------------
  // properties.
  //-----------------


  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  // Used for testing purposes.
	
  public void Display() {
    Console.WriteLine("Course Catalog:");
    Console.WriteLine("");

    // Step through the Hashtable and display all entries.

    IDictionaryEnumerator e = courses.GetEnumerator();

    while (e.MoveNext()) {
      Course c = (Course)e.Value;
     c.Display();
      Console.WriteLine("");
    }
  }

  public void AddCourse(Course c) {
    // We use the course no. as the key.

    string key = c.CourseNo;
    courses.Add(key, c);
  }

  public override void ParseData(string line) {
    // We're going to parse tab-delimited records into
    // three attributes -- courseNo, courseName, and credits --
    // and then call the Course constructor to fabricate a new
    // course.

    // We'll use the Split() method of the String class to split
    // the line we read from the file into substrings using tabs 
    // as the delimiter.

    string[] strings = line.Split('\t');

    // Now assign the value of the attributes to the appropriate
    // substring

    string courseNo = strings[0];
    string courseName = strings[1];
    string creditValue = strings[2];

    // We have to convert the last value into a number,
    // using a static method on the Double class to do so.

    double credits = Convert.ToDouble(creditValue);

    // Finally, we call the Course constructor to create
    // an appropriate Course object, and store it in our
    // collection.

    Course c = new Course(courseNo, courseName, credits);
    AddCourse(c);
  }

  public Course FindCourse(string courseNo) {
    return (Course)courses[courseNo];
  }

  // We must read a second file defining the prerequisites, in
  // order to "hook" Course objects together.
  // This next version is used when reading in prerequisites.

  public override void ParseData2(string line) {
    // We're going to parse tab-delimited records into
    // two values, representing the courseNo "A" of 
    // a course that serves as a prerequisite for 
    // courseNo "B".

    // Once again we'll make use of the Split() method to split
    // the line into substrings using tabs as the delimiter

    string[] strings = line.Split('\t');

    // Now assign the value of the attributes to the appropriate
    // substring

    string courseNoA = strings[0];
    string courseNoB = strings[1];

    // Look these two courses up in the CourseCatalog.

    Course a = FindCourse(courseNoA); 
    Course b = FindCourse(courseNoB); 
    if (a != null && b != null) {
      b.AddPrerequisite(a);
    }
  }

  // Test scaffold.
  public static void Main(string[] args) {
    // We instantiate a CourseCatalog object ...

    CourseCatalog cc = new CourseCatalog();

    // ... cause it to read both the CourseCatalog.dat and
    // Prerequisites.dat files, thereby testing both
    // the ParseData() and ParseData2() methods internally
    // to the InitializeObjects() method ...

    cc.InitializeObjects("CourseCatalog.dat", true);
    cc.InitializeObjects("Prerequisites.dat", false);

    // ... and use its Display() method to demonstrate the
    // results!

    cc.Display();
  }
}
