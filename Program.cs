using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("DCIT318 Assignment 3 - C# Programming");
        Console.WriteLine("=====================================");
        Console.WriteLine();
        
        Console.WriteLine("Select which question to run:");
        Console.WriteLine("1. Finance Management System");
        Console.WriteLine("2. Healthcare Management System");
        Console.WriteLine("3. Warehouse Inventory Management");
        Console.WriteLine("4. School Grading System");
        Console.WriteLine("5. Inventory Records System");
        Console.WriteLine("0. Run All Questions");
        Console.WriteLine();
        Console.Write("Enter your choice (0-5): ");
        
        string? input = Console.ReadLine();
        Console.WriteLine();

        switch (input)
        {
            case "1":
                Console.WriteLine("Running Question 1 - Finance Management System");
                Console.WriteLine("===============================================");
                Question1.FinanceApp.RunDemo();
                break;
            case "2":
                Console.WriteLine("Running Question 2 - Healthcare Management System");
                Console.WriteLine("=================================================");
                Question2.HealthSystemApp.RunDemo();
                break;
            case "3":
                Console.WriteLine("Running Question 3 - Warehouse Inventory Management");
                Console.WriteLine("===================================================");
                Question3.WareHouseManager.RunDemo();
                break;
            case "4":
                Console.WriteLine("Running Question 4 - School Grading System");
                Console.WriteLine("===========================================");
                Question4.StudentResultProcessor.RunDemo();
                break;
            case "5":
                Console.WriteLine("Running Question 5 - Inventory Records System");
                Console.WriteLine("==============================================");
                Question5.InventoryApp.RunDemo();
                break;
            case "0":
                RunAllQuestions();
                break;
            default:
                Console.WriteLine("Invalid choice. Please run the program again and select a valid option.");
                break;
        }
        
        Console.WriteLine();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
    
    static void RunAllQuestions()
    {
        Console.WriteLine("Running All Questions");
        Console.WriteLine("====================");
        
        try
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("QUESTION 1 - FINANCE MANAGEMENT SYSTEM");
            Console.WriteLine(new string('=', 60));
            Question1.FinanceApp.RunDemo();
            
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("QUESTION 2 - HEALTHCARE MANAGEMENT SYSTEM");
            Console.WriteLine(new string('=', 60));
            Question2.HealthSystemApp.RunDemo();
            
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("QUESTION 3 - WAREHOUSE INVENTORY MANAGEMENT");
            Console.WriteLine(new string('=', 60));
            Question3.WareHouseManager.RunDemo();
            
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("QUESTION 4 - SCHOOL GRADING SYSTEM");
            Console.WriteLine(new string('=', 60));
            Question4.StudentResultProcessor.RunDemo();
            
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("QUESTION 5 - INVENTORY RECORDS SYSTEM");
            Console.WriteLine(new string('=', 60));
            Question5.InventoryApp.RunDemo();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error running questions: {ex.Message}");
        }
        
        Console.WriteLine("\n" + new string('=', 60));
        Console.WriteLine("ALL QUESTIONS COMPLETED");
        Console.WriteLine(new string('=', 60));
    }
}
