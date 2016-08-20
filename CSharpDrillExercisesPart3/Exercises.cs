using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDrillExercisesPart3
{
    class Exercises
    {
        static void Main(string[] args)
        {

            Exercises ex = new Exercises();
            Resources rsrc = new Resources();


            // A very basic example use of a try/catch/finally block
            try
            {
                double num1 = 0;
                double num2 = 0;
                Console.Write("Please enter the length of the rectangle: ");
                num1 = double.Parse(Console.ReadLine());
                Console.Write("Please enter the width of the rectangle: ");
                num2 = double.Parse(Console.ReadLine());

                Rectangle rect = new Rectangle(num1, num2);
                Console.WriteLine("\nThe area of the rectangle is: {0}", rect.area());
            }
            catch (Exception e)
            {
                // Suppress the error
                Console.WriteLine("\n\nError: {0}", e.GetType());
                Console.WriteLine("\nYou did not enter a valid number.");
            }
            finally
            {
                // The actual use for a finally statement is to clear the buffer
                rsrc.clrScreen();
            }
            
            
        }
    }



    // Abstract Example
    abstract class Shape
    {
        abstract public double area();
    }

    class Rectangle : Shape
    {
        double length = 0;
        double width = 0;

        public Rectangle(double l, double w)
        {
            length = l;
            width = w;
        }

        public override double area()
        {
            return length * width;
        }

    }


}
