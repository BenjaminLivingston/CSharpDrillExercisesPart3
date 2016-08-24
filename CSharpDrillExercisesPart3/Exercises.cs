using System;
using System.IO;


namespace CSharpDrillExercisesPart3
{
    class Exercises
    {
        public string logPath = "~/../../../ErrorLog.txt";

        static void Main(string[] args)
        {

            Exercises ex = new Exercises();
            Resources rsrc = new Resources();

            // A very basic example use of a try/catch/finally block
            try
            {
                double num1 = 0;    // THIS IS AN EXAMPLE OF A VALUE TYPE
                double num2 = 0;
                Console.Write("Please enter the length of the rectangle: ");
                num1 = double.Parse(Console.ReadLine());
                Console.Write("Please enter the width of the rectangle: ");
                num2 = double.Parse(Console.ReadLine());

                Rectangle rect = new Rectangle(num1, num2);     // Using an ABSTRACT class
                Console.WriteLine("\nThe area of the rectangle is: {0}", rect.area());
            }
            catch (Exception e)
            {
                // Suppress the error
                Console.WriteLine("\n\nError: {0}", e.GetType());
                Console.WriteLine("\nYou did not enter a valid number.");

                // Example of recording the error in a log file
                using (StreamWriter w = File.AppendText(ex.logPath))
                {
                    ex.LogError(e, w);
                }

                using (StreamReader r = File.OpenText(ex.logPath))
                {
                    ex.recordLog(r);
                }

            }
            finally
            {
                // The actual use for a finally statement is to clear the buffer
                rsrc.clrScreen();
            }



            // Example of serializing an object to a BLOB (Binary Large Object)
            RegularPolygon poly1 = new RegularPolygon("Hexagon", 4.2, 6);   //Created object using INTERFACE class

            string binPath = "~/../../Shapes.bin";

            // Store the object info in a binary object
            SerializeBinaryFile.WriteToFile<RegularPolygon>(binPath, poly1, false);


            // Notify the user and clear the screen after writing the object
            Console.WriteLine("Binary file was successfully created.");
            rsrc.clrScreen();

            // Read the file stored in the binary object
            RegularPolygon poly2 = SerializeBinaryFile.ReadFromFile<RegularPolygon>(binPath);
            Console.WriteLine("Binary file was successfully loaded: \n\n");
            poly2.showShapeInfo();


            // Clear the screen after user input
            rsrc.clrScreen();
        }


        // Error Log Handlers
        private void LogError(Exception e, TextWriter w)
        {
            w.Write("\r\nLog Entry:");
            w.WriteLine("Date: {0}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss"));
            w.WriteLine("Error Type: {0}", e.GetType());
            w.WriteLine("Message: {0}", e.Message);
            w.WriteLine("Stack Trace: {0}", e.StackTrace);
            w.WriteLine("Source: {0}", e.Source);
            w.WriteLine("TargetSite: {0}", e.TargetSite);
            w.WriteLine("----------------------------------------------------------------");
        }

        public void recordLog(StreamReader r)
        {
            string log;     //THIS IS AN EXAMPLE OF A REFERENCE TYPE
            while ((log = r.ReadLine()) != null)
            {
                // Uncomment below to record error in console window
                //Console.WriteLine(log);
            }
        }


        // Serialize BLOB Class
        public static class SerializeBinaryFile
        {
            // Write File
            public static void WriteToFile<T>(string filePath, T objectToWrite, bool append = false)
            {
                using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    binaryFormatter.Serialize(stream, objectToWrite);
                }
            }

            
            // Read File
            public static T ReadFromFile<T>(string filePath)
            {
                using (Stream stream = File.Open(filePath, FileMode.Open))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return (T)binaryFormatter.Deserialize(stream);
                }
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

        public override double area()   // Using override of an abstract
        {
            return length * width;
        }

    }


    // Interface Class example
    public interface IRegularPolygon
    {
        // Members of Shape Interface
        void showShapeInfo();
        int getNumberOfSides();
        double getShapeArea();
    }

    [Serializable]
    public class RegularPolygon : IRegularPolygon
    {
        private string shapeName;
        private double sideLength;
        private int nSides;
        private double sArea;

        public RegularPolygon()
        {
            shapeName = " ";
            sideLength = 0.0;
            nSides = 0;
            sArea = 0.0;
        }

        public RegularPolygon(string sn, double sl, int ns)
        {
            shapeName = sn;
            sideLength = sl;
            nSides = ns;
            double sApo = (sl / (2 * Math.Tan((180 / ns))));
            double sPerim = (sl * ns);
            sArea = ((sApo * sPerim) / 2);
        }

        public int getNumberOfSides()
        {
            return nSides;
        }

        public double getShapeArea()
        {
            return sArea;
        }

        public void showShapeInfo()
        {
            Console.WriteLine("\nA regular polygon of the {0} type", shapeName);
            Console.WriteLine("with equal side lengths of {0}", sideLength);
            Console.WriteLine("and {0} sides", nSides);
            Console.WriteLine("has an area of {0}", sArea);
        }
    }
}
