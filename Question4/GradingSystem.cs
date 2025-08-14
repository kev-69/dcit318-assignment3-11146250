using System;
using System.Collections.Generic;
using System.IO;

namespace Question4
{
    // Student class
    public class Student
    {
        public int Id { get; }
        public string FullName { get; }
        public int Score { get; }

        public Student(int id, string fullName, int score)
        {
            Id = id;
            FullName = fullName;
            Score = score;
        }

        public string GetGrade()
        {
            return Score switch
            {
                >= 80 and <= 100 => "A",
                >= 70 and < 80 => "B",
                >= 60 and < 70 => "C",
                >= 50 and < 60 => "D",
                < 50 => "F",
                _ => "Invalid"
            };
        }

        public override string ToString()
        {
            return $"{FullName} (ID: {Id}): Score = {Score}, Grade = {GetGrade()}";
        }
    }

    // Custom Exception Classes
    public class InvalidScoreFormatException : Exception
    {
        public InvalidScoreFormatException(string message) : base(message) { }
    }

    public class MissingFieldException : Exception
    {
        public MissingFieldException(string message) : base(message) { }
    }

    // Student Result Processor class
    public class StudentResultProcessor
    {
        public List<Student> ReadStudentsFromFile(string inputFilePath)
        {
            var students = new List<Student>();

            using (var reader = new StreamReader(inputFilePath))
            {
                string? line;
                int lineNumber = 0;
                
                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;
                    
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var fields = line.Split(',');
                    
                    if (fields.Length != 3)
                    {
                        throw new MissingFieldException($"Line {lineNumber}: Expected 3 fields but found {fields.Length}");
                    }

                    try
                    {
                        int id = int.Parse(fields[0].Trim());
                        string fullName = fields[1].Trim();
                        
                        if (!int.TryParse(fields[2].Trim(), out int score))
                        {
                            throw new InvalidScoreFormatException($"Line {lineNumber}: Invalid score format '{fields[2].Trim()}'");
                        }

                        students.Add(new Student(id, fullName, score));
                    }
                    catch (FormatException)
                    {
                        throw new InvalidScoreFormatException($"Line {lineNumber}: Invalid ID or score format");
                    }
                }
            }

            return students;
        }

        public void WriteReportToFile(List<Student> students, string outputFilePath)
        {
            using (var writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine("=== Student Grade Report ===");
                writer.WriteLine($"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                writer.WriteLine(new string('=', 50));

                foreach (var student in students)
                {
                    writer.WriteLine(student.ToString());
                }
                
                writer.WriteLine(new string('=', 50));
                writer.WriteLine($"Total students processed: {students.Count}");
            }
        }

        private static void CreateSampleInputFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                using (var writer = new StreamWriter(fileName))
                {
                    writer.WriteLine("101,Alice Smith,84");
                    writer.WriteLine("102,Bob Johnson,72");
                    writer.WriteLine("103,Charlie Brown,91");
                    writer.WriteLine("104,Diana Prince,67");
                    writer.WriteLine("105,Eve Wilson,45");
                    writer.WriteLine("106,Frank Miller,88");
                }
                Console.WriteLine($"Sample input file '{fileName}' created.");
            }
        }

        public static void RunDemo()
        {
            Console.WriteLine("=== School Grading System ===");
            
            var processor = new StudentResultProcessor();
            string inputFile = "students.txt";
            string outputFile = "grade_report.txt";

            try
            {
                // Create sample input file if it doesn't exist
                CreateSampleInputFile(inputFile);
                
                var students = processor.ReadStudentsFromFile(inputFile);
                processor.WriteReportToFile(students, outputFile);
                
                Console.WriteLine($"Successfully processed {students.Count} students.");
                Console.WriteLine($"Grade report written to: {outputFile}");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"File not found: {ex.Message}");
            }
            catch (InvalidScoreFormatException ex)
            {
                Console.WriteLine($"Score format error: {ex.Message}");
            }
            catch (MissingFieldException ex)
            {
                Console.WriteLine($"Missing field error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}