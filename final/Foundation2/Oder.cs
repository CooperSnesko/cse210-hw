using System.Collections.Generic;

namespace Foundation2
{
    public class Order
    {
        private List<Product> _products;
        private Customer _customer;

        public Order(Customer customer)
        {
            _customer = customer;
            _products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public double GetTotalCost()
        {
            double total = 0;

            foreach (var product in _products)
            {
                total += product.GetTotalCost();
            }

            // Add shipping cost
            total += _customer.LivesInUSA() ? 5 : 35;

            return total;
        }

        public string GetPackingLabel()
        {
            string label = "Packing Label:\n";
            foreach (var product in _products)
            {
                label += $"- {product.GetProductDetails()}\n";
            }

            return label;
        }

        public string GetShippingLabel()
        {
            return $"Shipping Label:\n{_customer.GetName()}\n{_customer.GetAddress().GetFullAddress()}";
        }
    }
}