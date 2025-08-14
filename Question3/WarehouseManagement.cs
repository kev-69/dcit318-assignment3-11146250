using System;
using System.Collections.Generic;
using System.Linq;

namespace Question3
{
    // Marker interface for inventory items
    public interface IInventoryItem
    {
        int Id { get; }
        string Name { get; }
        int Quantity { get; set; }
    }

    // Electronic Item class
    public class ElectronicItem : IInventoryItem
    {
        public int Id { get; }
        public string Name { get; }
        public int Quantity { get; set; }
        public string Brand { get; }
        public int WarrantyMonths { get; }

        public ElectronicItem(int id, string name, int quantity, string brand, int warrantyMonths)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Brand = brand;
            WarrantyMonths = warrantyMonths;
        }

        public override string ToString()
        {
            return $"Electronic: {Name} (ID: {Id}, Brand: {Brand}, Warranty: {WarrantyMonths} months, Qty: {Quantity})";
        }
    }

    // Grocery Item class
    public class GroceryItem : IInventoryItem
    {
        public int Id { get; }
        public string Name { get; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; }

        public GroceryItem(int id, string name, int quantity, DateTime expiryDate)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            ExpiryDate = expiryDate;
        }

        public override string ToString()
        {
            return $"Grocery: {Name} (ID: {Id}, Expiry: {ExpiryDate:yyyy-MM-dd}, Qty: {Quantity})";
        }
    }

    // Custom Exceptions
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException(string message) : base(message) { }
    }

    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message) : base(message) { }
    }

    public class InvalidQuantityException : Exception
    {
        public InvalidQuantityException(string message) : base(message) { }
    }

    // Generic Inventory Repository
    public class InventoryRepository<T> where T : IInventoryItem
    {
        private Dictionary<int, T> _items = new Dictionary<int, T>();

        public void AddItem(T item)
        {
            if (_items.ContainsKey(item.Id))
            {
                throw new DuplicateItemException($"Item with ID {item.Id} already exists.");
            }
            _items[item.Id] = item;
        }

        public T GetItemById(int id)
        {
            if (!_items.ContainsKey(id))
            {
                throw new ItemNotFoundException($"Item with ID {id} not found.");
            }
            return _items[id];
        }

        public void RemoveItem(int id)
        {
            if (!_items.ContainsKey(id))
            {
                throw new ItemNotFoundException($"Item with ID {id} not found.");
            }
            _items.Remove(id);
        }

        public List<T> GetAllItems()
        {
            return _items.Values.ToList();
        }

        public void UpdateQuantity(int id, int newQuantity)
        {
            if (newQuantity < 0)
            {
                throw new InvalidQuantityException("Quantity cannot be negative.");
            }
            
            if (!_items.ContainsKey(id))
            {
                throw new ItemNotFoundException($"Item with ID {id} not found.");
            }
            
            _items[id].Quantity = newQuantity;
        }
    }

    // Warehouse Manager class
    public class WareHouseManager
    {
        private InventoryRepository<ElectronicItem> _electronics = new InventoryRepository<ElectronicItem>();
        private InventoryRepository<GroceryItem> _groceries = new InventoryRepository<GroceryItem>();

        public void SeedData()
        {
            try
            {
                // Add electronic items
                _electronics.AddItem(new ElectronicItem(1, "Laptop", 10, "Dell", 24));
                _electronics.AddItem(new ElectronicItem(2, "Smartphone", 25, "Samsung", 12));
                _electronics.AddItem(new ElectronicItem(3, "Tablet", 15, "Apple", 12));

                // Add grocery items
                _groceries.AddItem(new GroceryItem(1, "Milk", 50, DateTime.Now.AddDays(7)));
                _groceries.AddItem(new GroceryItem(2, "Bread", 30, DateTime.Now.AddDays(3)));
                _groceries.AddItem(new GroceryItem(3, "Eggs", 40, DateTime.Now.AddDays(14)));

                Console.WriteLine("Sample data seeded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding data: {ex.Message}");
            }
        }

        public void PrintAllItems<T>(InventoryRepository<T> repo) where T : IInventoryItem
        {
            try
            {
                var items = repo.GetAllItems();
                foreach (var item in items)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error printing items: {ex.Message}");
            }
        }

        public void IncreaseStock<T>(InventoryRepository<T> repo, int id, int quantity) where T : IInventoryItem
        {
            try
            {
                var item = repo.GetItemById(id);
                repo.UpdateQuantity(id, item.Quantity + quantity);
                Console.WriteLine($"Stock increased for item {id}. New quantity: {item.Quantity + quantity}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error increasing stock: {ex.Message}");
            }
        }

        public void RemoveItemById<T>(InventoryRepository<T> repo, int id) where T : IInventoryItem
        {
            try
            {
                repo.RemoveItem(id);
                Console.WriteLine($"Item {id} removed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing item: {ex.Message}");
            }
        }

        public static void RunDemo()
        {
            Console.WriteLine("=== Warehouse Inventory Management System ===");
            var manager = new WareHouseManager();
            
            manager.SeedData();
            
            Console.WriteLine("\n=== All Grocery Items ===");
            manager.PrintAllItems(manager._groceries);
            
            Console.WriteLine("\n=== All Electronic Items ===");
            manager.PrintAllItems(manager._electronics);
            
            Console.WriteLine("\n=== Testing Error Handling ===");
            
            // Try to add duplicate item
            try
            {
                manager._electronics.AddItem(new ElectronicItem(1, "Duplicate Laptop", 5, "HP", 12));
            }
            catch (DuplicateItemException ex)
            {
                Console.WriteLine($"Duplicate item error: {ex.Message}");
            }
            
            // Try to remove non-existent item
            manager.RemoveItemById(manager._groceries, 999);
            
            // Try to update with invalid quantity
            try
            {
                manager._electronics.UpdateQuantity(1, -5);
            }
            catch (InvalidQuantityException ex)
            {
                Console.WriteLine($"Invalid quantity error: {ex.Message}");
            }
        }
    }
}