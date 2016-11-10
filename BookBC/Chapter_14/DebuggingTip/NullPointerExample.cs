using System;

public class NullPointerExample {
  public static void Main(String[] args) {
    Section s = new Section(1);
    // We forgot to set the instructor attribute, so when we call
    // the Display() method, we're in trouble!
    s.Display();
  }
}
