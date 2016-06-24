using System;
using System.IO;

public class IOExample
{
  static void Main() {   
    FileStream fs;
    StreamReader srIn;
    StreamWriter sw;

    // Read operations should be placed in a try-catch block.
    try {
      // Create a FileStream and a StreamReader
      fs = new FileStream("data.dat", FileMode.Open );
      srIn = new StreamReader(fs);

      // Read the first line from the file. 
      string line = srIn.ReadLine();

      Console.WriteLine("line = "+line);
      srIn.Close();

      // Open a StreamWriter based on the same FileStream. 
//      fs = new FileStream("data.dat", FileMode.Open );
//      sw = new StreamWriter(fs);

      // write a line to the file
//      string newLine = "Not so different from you and me";
//      sw.WriteLine(newLine);

      // close the streams
//      fs.Close();
//      srIn.Close();
//      sw.Close();
    }
//    catch (FileNotFoundException fnfe) {
//Console.WriteLine("FileNotFoundException occurred: "+fnfe);
      // Perform exception handling details omitted.
//    }
    catch (IOException ioe) {
Console.WriteLine("IOException occurred: "+ioe.Message);
      // Perform exception handling details omitted.
    }
  }
}
