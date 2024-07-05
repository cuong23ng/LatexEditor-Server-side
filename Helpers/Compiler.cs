using System.Diagnostics;

namespace Hustex_backend.Helpers
{
    public static class Compiler
    {
        public static bool PDFConverter(string location, string filename)
        {
            // Specify the path to your .tex file
            string texFilePath = Path.Combine(@"D:\git-repos\project2\Hustex-backend", location, filename + ".tex");
            Console.WriteLine(texFilePath);

            // Specify the path to pdflatex executable
            string pdflatexPath = @"C:\Users\cuong\AppData\Local\Programs\MiKTeX\miktex\bin\x64\pdflatex.exe";

            // Create a process to run pdflatex
            Process pdflatexProcess = new Process();
            pdflatexProcess.StartInfo.FileName = pdflatexPath;
            pdflatexProcess.StartInfo.Arguments = texFilePath;
            pdflatexProcess.StartInfo.WorkingDirectory = Path.Combine(@"D:\git-repos\project2\Hustex-backend", location);
            pdflatexProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            pdflatexProcess.StartInfo.RedirectStandardOutput = true;
            pdflatexProcess.StartInfo.RedirectStandardError = true;

            try
            {
                // Start the process
                pdflatexProcess.Start();

                // int timeoutMilliseconds = 10000;
                // if (pdflatexProcess.WaitForExit(timeoutMilliseconds))
                // {
                    // Read the output (optional)
                    string output = pdflatexProcess.StandardOutput.ReadToEnd();
                    string error = pdflatexProcess.StandardError.ReadToEnd();
                    Console.WriteLine(output); // Display the output if needed
                    Console.WriteLine(error); // Display the error if needed

                    // Wait for the process to exit
                    pdflatexProcess.WaitForExit();

                    if (File.Exists(Path.Combine(@"D:\git-repos\project2\Hustex-backend", location, filename + ".aux")))
                    {
                        File.Delete(Path.Combine(@"D:\git-repos\project2\Hustex-backend", location, filename + ".aux"));
                    }

                    if (File.Exists(Path.Combine(@"D:\git-repos\project2\Hustex-backend", location, filename + ".lof")))
                    {
                        File.Delete(Path.Combine(@"D:\git-repos\project2\Hustex-backend", location, filename + ".lof"));
                    }

                    if (File.Exists(Path.Combine(@"D:\git-repos\project2\Hustex-backend", location, filename + ".lot")))
                    {
                        File.Delete(Path.Combine(@"D:\git-repos\project2\Hustex-backend", location, filename + ".lot"));
                    }

                    if (File.Exists(Path.Combine(@"D:\git-repos\project2\Hustex-backend", location, filename + ".log")))
                    {
                        File.Delete(Path.Combine(@"D:\git-repos\project2\Hustex-backend", location, filename + ".log"));
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
                // }
                // else
                // {
                //     // If the process didn't exit in the allotted time, kill it
                //     pdflatexProcess.Kill();
                //     Console.WriteLine("Compilation timed out and was terminated.");
                //     return false;
                // }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
    }
}