# DCIT318 Assignment 3 - C# Programming

This repository contains solutions for Assignment 3 of DCIT318, implementing various C# programming concepts including interfaces, records, sealed classes, generics, collections, exception handling, and file operations.

## Project Structure

```
dcit318-assignment3-11146250/
??? Question1/
?   ??? FinanceManagement.cs       # Finance Management System
??? Question2/
?   ??? HealthcareSystem.cs        # Healthcare Management System
??? Question3/
?   ??? WarehouseManagement.cs     # Warehouse Inventory Management
??? Question4/
?   ??? GradingSystem.cs           # School Grading System
??? Question5/
?   ??? InventoryRecords.cs        # Inventory Records System
??? Program.cs                     # Main application entry point
??? DCIT318Assignment.csproj       # Project file
??? README.md                      # This file
```

## Questions Implemented

### Question 1: Finance Management System

**Technologies:** Records, Interfaces, Sealed Classes, Inheritance Control

- **Record:** `Transaction` - Immutable financial transaction data
- **Interface:** `ITransactionProcessor` - Payment behavior abstraction
- **Implementations:** BankTransferProcessor, MobileMoneyProcessor, CryptoWalletProcessor
- **Base Class:** `Account` - General account functionality
- **Sealed Class:** `SavingsAccount` - Specialized account with overdraft protection
- **Features:** Transaction processing, balance management, insufficient funds handling

### Question 2: Healthcare Management System

**Technologies:** Generics, Collections (List, Dictionary), Type Safety

- **Generic Repository:** `Repository<T>` - Type-safe entity storage and retrieval
- **Entities:** Patient and Prescription classes
- **Collections:** Dictionary for grouping prescriptions by patient ID
- **Features:** Patient management, prescription tracking, data relationships

### Question 3: Warehouse Inventory Management System

**Technologies:** Generics, Collections, Custom Exception Handling

- **Interface:** `IInventoryItem` - Marker interface for inventory items
- **Implementations:** ElectronicItem, GroceryItem
- **Generic Repository:** `InventoryRepository<T>` with type constraints
- **Custom Exceptions:** DuplicateItemException, ItemNotFoundException, InvalidQuantityException
- **Features:** Inventory management, stock control, error handling

### Question 4: School Grading System

**Technologies:** File I/O, Exception Handling, Data Validation

- **Student Class:** Grade calculation logic
- **Custom Exceptions:** InvalidScoreFormatException, MissingFieldException
- **File Operations:** Reading CSV input, writing formatted reports
- **Features:** Student data processing, grade assignment, report generation

### Question 5: Inventory Records System

**Technologies:** Records, Generics, File Serialization, JSON

- **Record:** `InventoryItem` - Immutable inventory data
- **Interface:** `IInventoryEntity` - Marker interface for logging
- **Generic Logger:** `InventoryLogger<T>` with type constraints
- **File Operations:** JSON serialization/deserialization using Newtonsoft.Json
- **Features:** Data persistence, memory simulation, data recovery

## How to Run

### Prerequisites

- .NET 9.0 SDK
- Visual Studio 2022 or VS Code

### Building the Project

```bash
dotnet build
```

### Running the Application

```bash
dotnet run
```

The application will present a menu allowing you to:

1. Run individual questions (1-5)
2. Run all questions at once (option 0)

### Sample Input/Output

When running the application, you'll see:

```
DCIT318 Assignment 3 - C# Programming
=====================================

Select which question to run:
1. Finance Management System
2. Healthcare Management System
3. Warehouse Inventory Management
4. School Grading System
5. Inventory Records System
0. Run All Questions

Enter your choice (0-5):
```

## Key Programming Concepts Demonstrated

### Object-Oriented Programming

- **Inheritance:** Account ? SavingsAccount
- **Polymorphism:** ITransactionProcessor implementations
- **Encapsulation:** Protected setters, private fields
- **Abstraction:** Interfaces and abstract contracts

### Modern C# Features

- **Records:** Immutable data structures (Transaction, InventoryItem)
- **Sealed Classes:** Preventing further inheritance (SavingsAccount)
- **Pattern Matching:** Switch expressions for grade calculation
- **Nullable Reference Types:** Enabled throughout the project

### Generics and Collections

- **Generic Classes:** Repository<T>, InventoryRepository<T>, InventoryLogger<T>
- **Type Constraints:** where T : IInventoryItem, where T : IInventoryEntity
- **Collections:** List<T>, Dictionary<TKey, TValue>
- **LINQ:** FirstOrDefault, Any, grouping operations

### Exception Handling

- **Custom Exceptions:** Domain-specific error types
- **Try-Catch Blocks:** Graceful error handling
- **Using Statements:** Proper resource disposal
- **File Operations:** Safe file I/O with error handling

### File Operations

- **Text Files:** CSV reading/writing
- **JSON Serialization:** Using Newtonsoft.Json
- **Stream Operations:** StreamReader, StreamWriter with using statements

## Dependencies

The project uses the following NuGet package:

- **Newtonsoft.Json 13.0.1** - For JSON serialization in Question 5

## Sample Files Generated

When running the application, the following files may be created:

- `students.txt` - Sample student data for Question 4
- `grade_report.txt` - Generated grade report for Question 4
- `inventory_data.json` - Serialized inventory data for Question 5

## Design Patterns and Best Practices

1. **Repository Pattern:** Generic data access layer
2. **Strategy Pattern:** Different transaction processors
3. **Factory Pattern:** Object creation in demo methods
4. **Single Responsibility:** Each class has one clear purpose
5. **Dependency Injection:** Constructor injection for dependencies
6. **Error Handling:** Comprehensive exception management
7. **Resource Management:** Proper disposal using 'using' statements
8. **Immutability:** Records for unchangeable data
9. **Type Safety:** Generics for compile-time type checking
10. **Separation of Concerns:** Each question in separate namespace

## Author

Student ID: 11146250
Course: DCIT318 - Programming 2
