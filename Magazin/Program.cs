using System;
using System.Collections.Generic;

namespace Magazin
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var seller = new Seller("Алексей", 1000m);
            seller.AddProduct(new Product("Телефон", 500m));
            seller.AddProduct(new Product("Наушники", 150m));
            seller.AddProduct(new Product("Ноутбук", 1200m));

            var buyer = new Buyer("Дмитрий", 700m);
            var shop = new Shop(seller);

            var view = new ShopView(shop, buyer);
            view.Run();
        }
    }

    abstract class Person
    {
        protected Person(string name, decimal money)
        {
            Name = name;
            Money = money;
        }

        public string Name { get; }
        public decimal Money { get; protected set; }
    }

    class Seller : Person
    {
        private readonly List<Product> _products = new List<Product>();

        public Seller(string name, decimal money) : base(name, money) { }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public IReadOnlyList<Product> Products => _products.AsReadOnly();

        public void ShowProducts()
        {
            Console.WriteLine($"\nТовары продавца {Name}:");

            if (_products.Count == 0)
            {
                Console.WriteLine("Нет товаров в наличии.");
                return;
            }

            for (int i = 0; i < _products.Count; i++)
                Console.WriteLine($"{i + 1}. {_products[i]}");
        }

        public bool TrySell(int index, out Product product)
        {
            if (index < 0 || index >= _products.Count)
            {
                product = null;
                return false;
            }

            product = _products[index];
            _products.RemoveAt(index);
            return true;
        }

        public void ReceivePayment(decimal amount)
        {
            Money += amount;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"\nПродавец {Name}, баланс: {Money}₪, товаров: {_products.Count}");
        }
    }

    class Buyer : Person
    {
        private readonly List<Product> _purchases = new List<Product>();

        public Buyer(string name, decimal money) : base(name, money) { }

        public bool TrySpendMoney(decimal price)
        {
            if (Money < price)
                return false;

            Money -= price;
            return true;
        }

        public void AddPurchase(Product product)
        {
            _purchases.Add(product);
        }

        public void ShowPurchases()
        {
            Console.WriteLine($"\nПокупатель: {Name}");
            Console.WriteLine($"Баланс: {Money}₪");

            if (_purchases.Count == 0)
            {
                Console.WriteLine("Пока ничего не куплено.");
                return;
            }

            Console.WriteLine("Купленные товары:");
            foreach (var item in _purchases)
                Console.WriteLine($"- {item}");
        }
    }

    class Product
    {
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; }
        public decimal Price { get; }

        public override string ToString() => $"{Name} - {Price}₪";
    }

    class Shop
    {
        private readonly Seller _seller;

        public Shop(Seller seller)
        {
            _seller = seller;
        }

        public void ShowGoods()
        {
            _seller.ShowProducts();
        }

        public void ShowInfo()
        {
            _seller.ShowInfo();
        }

        public void Trade(Buyer buyer)
        {
            _seller.ShowProducts();

            Console.Write("\nВведите номер товара для покупки: ");
            if (int.TryParse(Console.ReadLine(), out int index) == false)
            {
                Console.WriteLine("Ошибка: неверный ввод.");
                return;
            }

            if (_seller.TrySell(index - 1, out var product) == false)
            {
                Console.WriteLine("Ошибка: такого товара нет.");
                return;
            }

            if (buyer.TrySpendMoney(product.Price) == false)
            {
                Console.WriteLine($"{buyer.Name} не хватает денег на {product.Name}!");
                _seller.AddProduct(product);
                return;
            }

            _seller.ReceivePayment(product.Price);
            buyer.AddPurchase(product);

            Console.WriteLine($"{buyer.Name} купил {product.Name} за {product.Price}₪");
            //Console.WriteLine($"Остаток денег у покупателя: {buyer.Money}₪");
        }
    }

    public enum MenuOption
    {
        ShowGoods = 1,
        ShowShopInfo,
        BuyProduct,
        ShowBuyerPurchases,
        Exit
    }

    class ShopView
    {
        
        private readonly Shop _shop;
        private readonly Buyer _buyer;

        public ShopView(Shop shop, Buyer buyer)
        {
            _shop = shop;
            _buyer = buyer;
        }

        public void Run()
        {
            bool isRunning = true;

            while (isRunning == true)
            {
                Show();

                Console.Write("\nВведите пункт меню: ");
                string input = Console.ReadLine();
                Console.Clear();

                if (int.TryParse(input, out int choiceNum) == false ||
                    Enum.IsDefined(typeof(MenuOption), choiceNum) == false)
                {
                    Console.WriteLine("Ошибка: неверный пункт меню!");
                    continue;
                }

                MenuOption choice = (MenuOption)choiceNum;

                switch (choice)
                {
                    case MenuOption.ShowGoods:
                        _shop.ShowGoods();
                        break;

                    case MenuOption.ShowShopInfo:
                        _shop.ShowInfo();
                        break;

                    case MenuOption.BuyProduct:
                        _shop.Trade(_buyer);
                        break;

                    case MenuOption.ShowBuyerPurchases:
                        _buyer.ShowPurchases();
                        break;

                    case MenuOption.Exit:
                        isRunning = false;
                        break;
                }
            }

            Console.WriteLine("Выход из программы...");
        }
        

        private void Show()
        {
            Console.WriteLine("\n=== МАГАЗИН ===");
            Console.WriteLine($"{(int)MenuOption.ShowGoods}. Показать товары");
            Console.WriteLine($"{(int)MenuOption.ShowShopInfo}. Показать информацию о магазине");
            Console.WriteLine($"{(int)MenuOption.BuyProduct}. Купить товар");
            Console.WriteLine($"{(int)MenuOption.ShowBuyerPurchases}. Показать покупки покупателя");
            Console.WriteLine($"{(int)MenuOption.Exit}. Выход");
        }
    }
}
