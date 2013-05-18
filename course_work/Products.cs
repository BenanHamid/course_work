using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace course_work
{
    [Serializable]

    public class Products
    {
        // Това тук отначало са само get/set-овете
        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        private int promotions;
        public int Promotions
        {
            get { return promotions; }
            set { promotions = value; }
        }

        private string brand;
        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }

        private double price;
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        private string inventoryID;
        public string InventoryID
        {
            get { return inventoryID; }
            set { inventoryID = value; }
        }

        private string category;
        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        // Четене на данните от products_db.sql и импортирането им в DataGridView
        public static List<Products> LoadUserListFromFile(string filePath)
        {
            var loadProductsData = new List<Products>();

            foreach (var line in File.ReadAllLines(filePath))
            {
                var columns = line.Split('\t');
                loadProductsData.Add(new Products
                {
                    InventoryID = columns[0],
                    Brand = columns[1],
                    Category = columns[2],
                    Description = columns[3],
                    Promotions = Convert.ToInt32(columns[4]),
                    Quantity = Convert.ToInt32(columns[5]),
                    Price = Convert.ToDouble(columns[6])
                    //Promotions = columns[4] == "1",       // ТОВА Е КАК ДА СЕ НАПРАВИ ЗА BOOL? ПП: Не, няма нуждa! ^^
                    //Age = Convert.ToInt32(columns[6]]),
                    //Balance = Convert.ToDecimal(columns[6])
                });
            }

            // Стойността, която връща тук, се използва във Form1.cs в LoadForm1, и вкарва тези данни в DataGridView
            return loadProductsData;
        }

    }
}
