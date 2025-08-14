using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Question5
{
    // Marker interface for inventory entities
    public interface IInventoryEntity
    {
        int Id { get; }
    }

    // Immutable Inventory Record
    public record InventoryItem(int Id, string Name, int Quantity, DateTime DateAdded) : IInventoryEntity;

    // Generic Inventory Logger
    public class InventoryLogger<T> where T : IInventoryEntity
    {
        private List<T> _log = new List<T>();
        private string _filePath;

        public InventoryLogger(string filePath)
        {
            _filePath = filePath;
        }

        public void Add(T item)
        {
            _log.Add(item);
            Console.WriteLine($"Added item: {item}");
        }

        public List<T> GetAll()
        {
            return new List<T>(_log);
        }

        public void SaveToFile()
        {
            try
            {
                using (var writer = new StreamWriter(_filePath))
                {
                    string json = JsonConvert.SerializeObject(_log, Formatting.Indented);
                    writer.Write(json);
                }
                Console.WriteLine($"Data saved to {_filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

        public void LoadFromFile()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    using (var reader = new StreamReader(_filePath))
                    {
                        string json = reader.ReadToEnd();
                        if (!string.IsNullOrWhiteSpace(json))
                        {
                            var loadedItems = JsonConvert.DeserializeObject<List<T>>(json);
                            if (loadedItems != null)
                            {
                                _log = loadedItems;
                                Console.WriteLine($"Data loaded from {_filePath}. Items count: {_log.Count}");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"File {_filePath} does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }
    }

    // Integration Layer - InventoryApp
    public class InventoryApp
    {
        private InventoryLogger<InventoryItem> _logger;

        public InventoryApp()
        {
            _logger = new InventoryLogger<InventoryItem>("inventory_data.json");
        }

        public void SeedSampleData()
        {
            Console.WriteLine("=== Seeding Sample Data ===");
            
            var item1 = new InventoryItem(1, "Laptop Computer", 25, DateTime.Now.AddDays(-10));
            var item2 = new InventoryItem(2, "Wireless Mouse", 150, DateTime.Now.AddDays(-5));
            var item3 = new InventoryItem(3, "USB Drive 32GB", 75, DateTime.Now.AddDays(-3));
            var item4 = new InventoryItem(4, "Monitor 24-inch", 40, DateTime.Now.AddDays(-1));
            var item5 = new InventoryItem(5, "Mechanical Keyboard", 60, DateTime.Now);

            _logger.Add(item1);
            _logger.Add(item2);
            _logger.Add(item3);
            _logger.Add(item4);
            _logger.Add(item5);
        }

        public void SaveData()
        {
            Console.WriteLine("\n=== Saving Data to File ===");
            _logger.SaveToFile();
        }

        public void LoadData()
        {
            Console.WriteLine("\n=== Loading Data from File ===");
            _logger.LoadFromFile();
        }

        public void PrintAllItems()
        {
            Console.WriteLine("\n=== All Inventory Items ===");
            var items = _logger.GetAll();
            
            if (items.Count == 0)
            {
                Console.WriteLine("No items found in inventory.");
                return;
            }

            foreach (var item in items)
            {
                Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}, Date Added: {item.DateAdded:yyyy-MM-dd}");
            }
            
            Console.WriteLine($"\nTotal items: {items.Count}");
        }

        public void ClearMemory()
        {
            Console.WriteLine("\n=== Simulating Memory Clear ===");
            _logger = new InventoryLogger<InventoryItem>("inventory_data.json");
            Console.WriteLine("Memory cleared - simulating new session.");
        }

        public static void RunDemo()
        {
            Console.WriteLine("=== Inventory Records Management System ===");
            
            var app = new InventoryApp();
            
            // Step 1: Seed sample data
            app.SeedSampleData();
            
            // Step 2: Save data to file
            app.SaveData();
            
            // Step 3: Clear memory and simulate new session
            app.ClearMemory();
            
            // Step 4: Load data from file
            app.LoadData();
            
            // Step 5: Print all items to confirm data recovery
            app.PrintAllItems();
            
            Console.WriteLine("\n=== System Operation Complete ===");
        }
    }
}