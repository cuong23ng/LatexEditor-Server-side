using System.Diagnostics;
using Hustex_backend.Models;

namespace Hustex_backend.Helpers
{
    public static class Compiler
    {
        public static bool PDFConverter(string location, string filename)
        {
            // Specify the path to your .tex file
            string texFilePath = location + filename + ".tex";
            Console.WriteLine(texFilePath);

            // Specify the path to pdflatex executable
            string pdflatexPath = @"C:\Users\cuong\AppData\Local\Programs\MiKTeX\miktex\bin\x64\pdflatex.exe";

            // Create a process to run pdflatex
            Process pdflatexProcess = new Process();
            pdflatexProcess.StartInfo.FileName = pdflatexPath;
            pdflatexProcess.StartInfo.Arguments = texFilePath;
            pdflatexProcess.StartInfo.WorkingDirectory = location;
            pdflatexProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            pdflatexProcess.StartInfo.RedirectStandardOutput = true;

            try
            {
                // Start the process
                pdflatexProcess.Start();

                // Read the output (optional)
                string output = pdflatexProcess.StandardOutput.ReadToEnd();
                Console.WriteLine(output); // Display the output if needed

                // Wait for the process to exit
                pdflatexProcess.WaitForExit();

                if (System.IO.File.Exists(location + filename + ".aux"))
                {
                    System.IO.File.Delete(location + filename + ".aux");
                }

                if (System.IO.File.Exists(location + filename + ".lof"))
                {
                    System.IO.File.Delete(location + filename + ".lof");
                }

                if (System.IO.File.Exists(location + filename + ".lot"))
                {
                    System.IO.File.Delete(location + filename + ".lot");
                }

                // Check the exit code (0 indicates success)
                if (pdflatexProcess.ExitCode == 0)
                {
                    Console.WriteLine("Compilation successful! PDF created.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Compilation failed. Check the log for errors.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
    }
}