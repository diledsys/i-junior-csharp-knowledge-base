using System;
using System.Collections.Generic;

namespace shop {
    class Program {
        static void Main(string[] args) {
            const string CommandShowSeller = "1";
            const string CommandShowBuyer = "2";
            const string CommandBuy = "3";
            const string CommandExit = "4";

            Shop shop = new Shop();
            bool isWork = true;

            Console.WriteLine("ДЗ: Магазин Вам надо создать магазин с продавцом  и покупателем. Продавец имеет список своих товаров, которые может показать и продавать их." +
                "Продажа заключается в передаче покупателю товара и увеличение у себя денег.Покупатель также имеет список товаров, что он купил, количество своих денег и " +
                "всё это может показать.Продавец может только продавать, а покупатель - только покупать.В задаче понадобится использовать наследование.");

            while (isWork) {
                Console.Clear();
                shop.ShowSellerInfo();
                shop.ShowBuyerInfo();
                Console.WriteLine($"Меню: \n{CommandShowSeller} - Показать имущество продавца\n{CommandShowBuyer} - Показать имущество покупателя" +
                    $"\n{CommandBuy} - Купить\n{CommandExit} - Выход");

                switch (Console.ReadLine()) {
                    case CommandShowSeller:
                        shop.ShowSellerInfo();
                        break;

                    case CommandShowBuyer:
                        shop.ShowBuyerInfo();
                        break;

                    case CommandBuy:
                        shop.TradeThings();
                        break;

                    case CommandExit:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Введите пункт из меню!");
                        break;
                }

                Console.ReadKey();
            }
        }
    }

    class Shop {
        private Buyer _buyer = new Buyer();
        private Seller _seller = new Seller();

        public void ShowSellerInfo() {
            _seller.PrintInfo();
        }

        public void ShowBuyerInfo() {
            _buyer.PrintInfo();
        }

        public void TradeThings() {
            if (_seller.TryGetProduct(out Product product)) {
                if (_buyer.CanBuy(product.Price)) {
                    _buyer.Buy(product);
                    _seller.Sell(product);
                }
            }
            else {
                Console.WriteLine("Товаров НЕТ! ");
            }
        }
    }

    class Citizen {
        protected List<Product> Products = new List<Product>();
        public int Money { get; protected set; }
        protected string Name;

        public void PrintInfo() {
            Console.WriteLine($"Имущество {Name} || деньги: {Money}");

            for (int i = 0; i < Products.Count; i++) {
                Product product = Products[i];

                Console.WriteLine($"№{i + 1} - ТОВАР: {product.Name} / ЦЕНА: {product.Price} ");
            }
        }
    }

    class Seller : Citizen {
        public Seller() {
            Name = "Продавец";
            Money = 0;
            Fill();
        }

        public void Sell(Product product) {
            if (product == null) {
                Console.WriteLine("Продукта нет!");
            }
            else {
                Money += product.Price;
                Products.Remove(product);
            }
        }

        public bool TryGetProduct(out Product product) {
            product = null;

            if (Products.Count == 0) {
                Console.WriteLine("В базе данных нет информации!");
                return false;
            }

            Console.Write("Выберите номер продукта для покупки: ");
            int numberProduct = Utils.ReadInteger();

            if (numberProduct <= 0 || numberProduct > Products.Count) {
                Console.WriteLine("Число за пределами номеров покупок!");
                return false;
            }

            product = Products[numberProduct - 1];
            return true;
        }

        private void Fill() {
            Products.Add(new Product("Макароны", 111));
            Products.Add(new Product("Водка", 222));
            Products.Add(new Product("Селедка", 333));
            Products.Add(new Product("Яйцо", 444));
        }
    }

    class Buyer : Citizen {
        public Buyer() {
            Name = "Покупатель";
            Money = 9999;
        }

        public bool CanBuy(int price) {
            return Money >= price;
        }

        public void Buy(Product product) {
            if (product == null) {
                Console.WriteLine("Продукта нет!");
            }
            else {
                Money = Money - product.Price;
                Products.Add(product);
            }
        }
    }

    class Product {
        public Product(string name, int price) {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; }
        public int Price { get; private set; }
    }

    class Utils {
        public static int ReadInteger() {
            int number;

            while (( int.TryParse(Console.ReadLine(), out number) == false )) {
                Console.WriteLine("Ошибка ввода, введите целое положительное число!");
            }

            return number;
        }
    }
}
