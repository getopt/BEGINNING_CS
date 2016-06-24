// CollectionWrapper.cs - Chapter 15 version.

// Copyright 2004 by Jacquie Barker and Grant Palmer - all rights reserved.

// An IMPLEMENTATION class.

using System;
using System.IO;

public abstract class CollectionWrapper {
  private string pathToFile;

  public bool InitializeObjects(string pathToFile, bool primary) {
    this.pathToFile = pathToFile;
    string line = null;
    StreamReader srIn = null;
    bool outcome = true;

    try {
      // Open the file. 
      srIn = new StreamReader(new FileStream(pathToFile, FileMode.Open));

      line = srIn.ReadLine();
      while (line != null) {
        if (primary) {
          ParseData(line);
        }
        else  {
          ParseData2(line);
        }  
        line = srIn.ReadLine();
      }

      srIn.Close();
    }
    catch (FileNotFoundException f) {
      Console.WriteLine(f);
      outcome = false;
    }
    catch (IOException i) {
      Console.WriteLine(i);
      outcome = false;
    }

    return outcome;
  }

  public abstract void ParseData(string line);
  public abstract void ParseData2(string line);
}
