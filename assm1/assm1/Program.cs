using System;
using System.Collections.Generic;
using System.Linq;

public class Customer
{
    public string Name { get; set; }
    public double LastMonthReading { get; set; }
    public double ThisMonthReading { get; set; }
    public double Consumption => ThisMonthReading - LastMonthReading;

    public virtual double CalculateTotalBill()
    {
        // Base implementation
        return 0;
    }
}

public class HouseholdCustomer : Customer
{
    public int NumberOfPeople { get; set; }

    public override double CalculateTotalBill()
    {
        double consumptionPerPerson = Consumption / NumberOfPeople;
        double pricePerM3;

        if (consumptionPerPerson <= 10)
            pricePerM3 = 5973;
        else if (consumptionPerPerson <= 20)
            pricePerM3 = 7052;
        else if (consumptionPerPerson <= 30)
            pricePerM3 = 8699;
        else
            pricePerM3 = 15929;

        double baseAmount = Consumption * pricePerM3;
        double environmentFee = baseAmount * 0.10;
        double vat = (baseAmount + environmentFee) * 0.10;
        return baseAmount + environmentFee + vat;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Customer> customers = new List<Customer>();

        Console.WriteLine("Enter the number of customers:");
        int numberOfCustomers = int.Parse(Console.ReadLine());

        for (int i = 0; i < numberOfCustomers; i++)
        {
            Console.WriteLine($"Enter details for customer {i + 1}:");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Last month's reading: ");
            double lastMonthReading = double.Parse(Console.ReadLine());

            Console.Write("This month's reading: ");
            double thisMonthReading = double.Parse(Console.ReadLine());

            Console.Write("Number of people (for household): ");
            int numberOfPeople = int.Parse(Console.ReadLine());

            HouseholdCustomer customer = new HouseholdCustomer
            {
                Name = name,
                LastMonthReading = lastMonthReading,
                ThisMonthReading = thisMonthReading,
                NumberOfPeople = numberOfPeople
            };

            customers.Add(customer);
        }

        Console.WriteLine("Customer Bill Details:");
        foreach (var customer in customers)
        {
            Console.WriteLine($"Name: {customer.Name}");
            Console.WriteLine($"Last Month Reading: {customer.LastMonthReading}");
            Console.WriteLine($"This Month Reading: {customer.ThisMonthReading}");
            Console.WriteLine($"Consumption: {customer.Consumption}");
            Console.WriteLine($"Total Bill: {customer.CalculateTotalBill():C}");
            Console.WriteLine();
        }

        // Optional: Implement sorting and searching features
        Console.Write("Enter name to search: ");
        string searchName = Console.ReadLine();
        var foundCustomer = customers.FirstOrDefault(c => c.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));

        if (foundCustomer != null)
        {
            Console.WriteLine($"Customer found: {foundCustomer.Name}");
            Console.WriteLine($"Total Bill: {foundCustomer.CalculateTotalBill():C}");
        }
        else
        {
            Console.WriteLine("Customer not found.");
        }
    }
}
