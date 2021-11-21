using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task1._2
{
    class Program
    {
        static string[] productNames = new string[15]
        {
            "Двойной воппер", "Желейные конфеты", "Салат столичный", 
            "Мятные леденцы", "Круассан", "Шоколад", 
            "Вешенки", "Запеканка", "Кассата", 
            "Сальморехо", "Баклажаны", "Огурцы",
            "Зефир", "Розмарин", "Нисуаз"
        };

        static List<string> manufacturerNames = new List<string>() 
        { 
            "Русь", "Глобус", "Северный", "Богатырь", "Пикник",
            "Премьер", "Водный", "Карамель", "Семерочка", "Кольцо"
        };

        static List<string> dealerNames = new List<string>()
        {
            "Руслан", "Марк", "Ева", "Платон", "Анастасия"
        };

        static List<string> addresses = new List<string>()
        {
            "пр. Сталина, 47", "пл. Чехова, 31", "бульвар Гоголя, 10", "пр. Ладыгина, 32", "пер. Славы, 14",
            "шоссе Ладыгина, 50", "проезд Космонавтов, 85", "пр. Космонавтов, 23", "шоссе 1905 года, 35", "л. Будапештсткая, 59"
        };

        static Random random = new Random();
        const int start = 10000;
        const int end = 100000;

        static void Main(string[] args)
        {
            Console.WriteLine("Введите название, артикул или их части товара.");
            string input = Console.ReadLine().ToLower();
            var foundManufacturers = Search(input, out List<Dealer> foundDealers, out Product product);
            Print(product, foundManufacturers, foundDealers);
        }

        static List<Manufacturer> Search(string searchBy, out List<Dealer> foundDealers, out Product product)
        {
            List<Manufacturer> manufacturers = GetManufacturers();
            List<Dealer> dealers = GetDealers(manufacturers);
            List<Manufacturer> foundManufacturers = new List<Manufacturer>();
            foundDealers = new List<Dealer>();
            product = new Product(searchBy);
            for (int i = 0; i < manufacturers.Count; i++)
            {
                if (manufacturers[i].Nomenclature.Contains(product))
                {
                    foundManufacturers.Add(manufacturers[i]);                    
                }
            }
            for (int i = 0; i < dealers.Count; i++)
            {
                for (int j = 0; j < foundManufacturers.Count; j++)
                {
                    if (dealers[i].Manufacturer.Equals(foundManufacturers[j]))
                    {
                        foundDealers.Add(dealers[i]);
                        break;
                    }
                }
            }
            return foundManufacturers;
        }

        static void Print(Product prod,List<Manufacturer> manufacturers, List<Dealer> dealers)
        {
            if (manufacturers.Count == 0) Console.WriteLine("Выбранный товар не найден!");
            else
            {
                Console.WriteLine();
                Console.WriteLine(new string('*', Console.WindowWidth));
                Console.WriteLine();
                foreach (var manufacturer in manufacturers)
                {
                    var product = manufacturer.Nomenclature.FirstOrDefault(pr => pr.Equals(prod));
                    Console.WriteLine(manufacturer);
                    Console.WriteLine("Товар: " + product.Name);
                    Console.WriteLine("Стоимость: " + product.Price);
                    Console.WriteLine();
                    Console.WriteLine(new string('*', Console.WindowWidth));
                    Console.WriteLine();
                }

                foreach (var dealer in dealers)
                {
                    var product = dealer.Manufacturer.Nomenclature.FirstOrDefault(pr => pr.Equals(prod));
                    Console.WriteLine(dealer);
                    Console.WriteLine("Товар: " + product.Name);
                    Console.WriteLine("Стоимость с наценкой: " + dealer.ExtraCharge(product));
                    Console.WriteLine();
                    Console.WriteLine(new string('*', Console.WindowWidth));
                    Console.WriteLine();
                }
            }
        }

        static List<Product> GetMockProducts()
        {
            int count = random.Next(10, 15);
            List<string> pNames = new List<string>();
            for (int i = 0; i < count; i++)
            {
                var pName = productNames[random.Next(0, productNames.Length - 1)];
                if(!pNames.Contains(pName))
                {
                    pNames.Add(pName);
                }
                else
                {
                    i--;
                }
            }
            List<Product> products = new List<Product>();            
            for (int i = 0; i < count; i++)
            {
                products.Add(new Product(
                    random.Next(start, end).ToString() + random.Next(start, end).ToString() + random.Next(start, end).ToString(),
                    pNames[i],
                    (decimal)(random.NextDouble() * 100)
                    ));
            }
            return products;
        }

        static List<Manufacturer> GetManufacturers()
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            for (int i = 0; i < 5; i++)
            {
                var randomIndex = random.Next(0, manufacturerNames.Count - 1);
                manufacturers.Add(new Manufacturer(
                    random.Next(start, end).ToString() + random.Next(start, end).ToString(),
                    manufacturerNames[randomIndex],
                    addresses[randomIndex],
                    GetMockProducts()
                    ));
                manufacturerNames.RemoveAt(randomIndex);
                addresses.RemoveAt(randomIndex);
            }
            return manufacturers;
        }

        static List<Dealer> GetDealers(List<Manufacturer> list)
        {
            List<Dealer> dealers = new List<Dealer>();
            for (int i = 0; i < 3; i++)
            {
                var manufacturer = list[random.Next(0, list.Count)];
                var randomIndex = random.Next(0, dealerNames.Count - 1);
                dealers.Add(new Dealer(
                    random.Next(start, end).ToString() + random.Next(start, end).ToString(),
                    dealerNames[randomIndex],
                    addresses[randomIndex],
                    manufacturer,
                    random.Next(5, 31)
                    ));
                dealerNames.RemoveAt(randomIndex);
                addresses.RemoveAt(randomIndex);
            }
            return dealers;
        }
    }

    class Manufacturer : Provider
    {
        public List<Product> Nomenclature { get; } = new List<Product>();

        public Manufacturer(string inn, string name, string address, List<Product> nomenclature) :
            base(inn, name, address)
        {
            Nomenclature = nomenclature;
        }

        public override bool Equals(object obj)
        {
            return obj is Manufacturer manufacturer &&
                Name == manufacturer.Name;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("ИНН: " + INN + "\n");
            sb.Append("Название: " + Name + "\n");
            sb.Append("Адрес: " + Address);
            return sb.ToString();
        }
    }

    class Dealer : Provider
    {
        public Manufacturer Manufacturer { get; set; }

        private int percent;

        public Dealer(string inn, string name, string address, Manufacturer manufacturer, int percent) :
            base(inn, name, address)
        {
            Manufacturer = manufacturer;
            this.percent = percent;
        }

        public decimal ExtraCharge(Product product)
        {
            return product.Price * (1 + (percent / 100));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("ИНН: " + INN + "\n");
            sb.Append("Название: " + Name + "\n");
            sb.Append("Адрес: " + Address + "\n");
            sb.Append("Производитель: " + Manufacturer.Name);
            return sb.ToString();
        }
    }

    abstract class Provider
    {
        public string INN { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public Provider(string inn, string name, string address)
        {
            INN = inn;
            Name = name;
            Address = address;
        }
    }

    class Product
    {
        private string vendorCode;
        public string VendorCode
        {
            get => vendorCode;
            set
            {
                if (value.Length < 10)
                {
                    vendorCode = (new string('0', 10 - value.Length)) + value;
                }
                else if (value.Length > 15)
                {
                    throw new Exception("Слишком большой артикул. Артикул должен содержать максимум 15 цифр");
                }
                else
                {
                    vendorCode = value;
                }
            }
        }

        public string Name { get; set; }
        private int price;
        public decimal Price
        {
            get => (decimal) price / 100;
            set
            {
                price = (int)(value * 100);
            }
        }

        public Product(string vc, string name, decimal price)
        {
            VendorCode = vc;
            Name = name;
            Price = price;
        }

        public Product(string searchBy)
        {
            if (long.TryParse(searchBy, out _)) VendorCode = searchBy;
            else Name = searchBy;
        }

        public override bool Equals(object obj)
        {
            return obj is Product product &&
                (product.Name != null ? Name.ToLower().Contains(product.Name.ToLower()) : VendorCode.Contains(product.VendorCode));
        }
    }
}
