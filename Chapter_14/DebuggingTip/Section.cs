// Section.cs - Chapter 14 version.

using System;

public class Section {
  private int sectionNo;
  private Professor instructor;
  // etc.
  // Constructor.
  public Section(int sNo) {
    this.SectionNo = sNo;

    // A Professor has not yet been identified.
    this.Instructor = null;
  }

  //-----------------
  // Properties.
  //-----------------

  public int SectionNo {
    get {
      return sectionNo;
    }
    set {
      sectionNo = value;
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

  // Used for testing purposes.
  public void Display() {
    Console.WriteLine("\tSection No.:  " + this.SectionNo);
    Console.WriteLine("\tProfessor:  " + this.Instructor.Name);
//    Professor p = this.Instructor;
//    if (p != null) Console.WriteLine("\tProfessor:  " + p.Name);
  }

  // etc.

}

public class Professor
{
  private String name;

  public Professor(string n) {
    this.Name = n;
  }

  public string Name {
    get {
      return name;
    }
    set {
      name = value;
    }
  }

}

