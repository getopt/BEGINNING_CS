// ScheduleOfClasses.cs - Chapter 15 version.

// Copyright 2004 by Jacquie Barker and Grant Palmer - all rights reserved.

// A MODEL class.

using System;
using System.Collections;

public class ScheduleOfClasses : CollectionWrapper {
  //------------
  // Attributes.
  //------------

  private string semester;

  // This Hashtable stores Section object references, using
  // a String concatenation of course no. and section no. as the
  // key, e.g., "MATH101 - 1".

  private Hashtable sectionsOffered; 

  //----------------
  // Constructor(s).
  //----------------

  // Initialize the attribute values using the set 
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

  public override void ParseData(string line) {
    // We're going to parse tab-delimited records into
    // six attributes -- courseNo, sectionNo, dayOfWeek, 
    // timeOfDay, room, and capacity.  We'll use courseNo to 
    // look up the appropriate Course object, and will then
    // call the ScheduleSection() method to fabricate a
    // new Section object.

    // We'll use the Split() method of the String class to split
    // the line we read from the file into substrings using tabs 
    // as the delimiter.

    string[] strings = line.Split('\t');

    // Now assign the value of the attributes to the appropriate
    // substring

    string courseNo = strings[0];
    string sectionNumber = strings[1];
    string dayOfWeek = strings[2];
    string timeOfDay = strings[3];
    string room = strings[4];
    string capacityValue = strings[5];

    // We need to convert the sectionNumber and capacityValue
    // Strings to ints

    int sectionNo = Convert.ToInt32(sectionNumber);
    int capacity = Convert.ToInt32(capacityValue);

    // Look up the Course object in the Course Catalog.
    // Having made courseCatalog a public static attribute
    // of the SRS class comes in handy!

    Course c = SRS.courseCatalog.FindCourse(courseNo);

    // Schedule the Section.

    Section s = c.ScheduleSection(sectionNo, dayOfWeek[0], 
                                  timeOfDay, room, capacity);
    string key = courseNo + " - " + s.SectionNo;
    AddSection(s);
  }

  // The full section number is a concatenation of the
  // course no. and section no., separated by a hyphen;
  // e.g., "ART101 - 1".

  public Section FindSection(string fullSectionNo) {
    return (Section)sectionsOffered[fullSectionNo];
  }

  // We don't need this method, but we have to provide it because
  // we inherited an abstract signature.

  public override void ParseData2(string line) { }

  // This next method was added to the ScheduleOfClasses
  // class for use with the SRS GUI

  // Convert the contents of the sectionsOffered Hashtable
  // into an ArrayList of Section objects that is sorted in
  // alphabetical order

  public ArrayList GetSortedSections() {
    ArrayList sortedKeys = new ArrayList();
    ArrayList sortedSections = new ArrayList();

    // Get an IDictionaryEnumertor of the key-value pairs
    // contained in the sectionsOffered Hashtable. Load the
    // sortedKeys ArrayList with the keys
    IDictionaryEnumerator e = sectionsOffered.GetEnumerator();

    while ( e.MoveNext() ) {
      string key = (string)e.Key;
      sortedKeys.Add(key);
    }

    // Sort the keys in the ArrayList alphabetically
    sortedKeys.Sort();

    // Load the value corresponding to the sorted keys into
    // the sortedSections ArrayList
    for(int i = 0; i < sortedKeys.Count; ++i) {
      string key = (string)sortedKeys[i];
      Section s = (Section)sectionsOffered[key];
      sortedSections.Add(s);
    }

    // Return the ArrayList containing the sorted Sections
    return sortedSections;
  }
}
