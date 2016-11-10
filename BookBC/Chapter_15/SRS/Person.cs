// Person.cs - Chapter 15 version.

// Copyright 2004 by Jacquie Barker and Grant Palmer - all rights reserved.

// A MODEL class.

using System;

// We are making this class abstract because we do not wish for it
// to be instantiated.

public abstract class Person {
  //------------
  // Attributes.
  //------------

  private string name;
  private string ssn;
	
  //----------------
  // Constructor(s).
  //----------------

  // Initialize the attribute values using the set 
  // accessor of the associated property.

  public Person(string name, string ssn) {
    this.Name = name;
    this.Ssn = ssn;
  }
	
  // We're replacing the default constructor that got "wiped out"
  // as a result of having created a constructor above. We reuse
  // the two-argument constructor with dummy values.

  public Person() : this("?", "???-??-????")  {
  }

  //-----------------
  // Properties.
  //-----------------

  public string Name {
    get {
      return name;
    }
    set {
      name = value;
    }
  }

  public string Ssn {
    get {
      return ssn;
    }
    set {
      ssn = value;
    }
  }

  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  // We'll let each subclass determine how it wishes to be
  // represented as a String value.

  public abstract override string ToString(); 

  // Used for testing purposes.

  public virtual void Display() {
    Console.WriteLine("Person Information:");
    Console.WriteLine("\tName:  " + this.Name);
    Console.WriteLine("\tSoc. Security No.:  " + this.Ssn);
  }
}	
