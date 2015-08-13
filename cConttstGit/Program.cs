using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.ComponentModel;


namespace cCont4
{
    class Program
    {

        private static Customer _customer;
        private static DateTime _lastUpdated;

        static void Main(string[] args)
        {
            _customer = new Customer();
            _customer.PropertyChanging += new PropertyChangingEventHandler(PropertyChanging);
            _customer.PropertyChanged += new PropertyChangedEventHandler(PropertyChanged);
            Do("Setting the Id to 0", p => p.Id = 0);
            Do("Setting the Id to 10", p => p.Id = 10);
            Do("Setting the Name to null", p => p.Name = null);
            Do("Setting the Name to The Big Lebowski", p => p.Name = "The Big Lebowski");
            Do("Setting the DOB to tomorrow", p => p.DOB = DateTime.Now.AddDays(1));
            Do("Setting the DOB to yesterday", p => p.DOB = DateTime.Now.AddDays(-1));
            Do("Setting the account id to 0", p => p.AccountId = 0);
            Do("Setting the account id to 1", p => p.AccountId = 1);
            Console.ReadKey();
        }

        static void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Ensure that the last update has been set. This is a post condition test.
            Contract.Ensures(_customer.LastUpdated > _lastUpdated);
            // Note the position of this. It must come after the contract is set up.
            Console.WriteLine("\t-- The property {0} has been updated", e.PropertyName);
        }

        static void PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            _lastUpdated = _customer.LastUpdated;
        }

        private static void Do(string message, Action<Customer> action)
        {
            bool passed = false;
            Console.WriteLine(message);
            try
            {
                action.Invoke(_customer);
                passed = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\t{0}", ex.Message);
            }
            if (passed)
            {
                Console.WriteLine("\tThe test has passed");
            }
            Console.WriteLine(Environment.NewLine);
        }
    }
}

