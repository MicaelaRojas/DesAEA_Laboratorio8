using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        public static DataClasses1DataContext context = new DataClasses1DataContext();
        static void Main(string[] args)
        {
            DataSource();
            Console.Read();
        }

        //<IntroToLINQ>
        static void IntroToLINQ()
        {
            // The Three Parts of a LINQ Query:
            //1. Data source.
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6, };

            //2. Query creation.
            //numQuery is an IEnumerable<int>
            var numQuery =
                from num in numbers
                where (num % 2) == 0 
                select num;

            //3. Query execution
            foreach(int num in numQuery)
            {
                Console.Write("{0,1}", num);
            }
        }

        static void IntroToLINQLAMBDA()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6};
            var nums=numbers.Where(x => x % 2 == 0);
            foreach(int num in nums)
            {
                Console.Write("{0,1}", num);
            }
        }

        //</IntroToLINQ>

        /// </DataSource>
        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes
                                    select cust;
            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void DataSourceLAMBDA()
        {
            var queryAllCustomers = context.clientes;
            foreach(var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        /// </DataSource>

        ///<Filtering>
        static void Filtering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;
            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }

        static void FilteringLAMBDA()
        {
            var queryLondonCustomers = context.clientes.Where(x => x.Ciudad == "londres");
            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }

        }
        ///</Filtering>


        ///<Ordering>
        static void Ordering()
        {
            var queryLondonCustomers3 = from cust in context.clientes
                                        where cust.Ciudad == "London"
                                        orderby cust.NombreCompañia ascending
                                        select cust;
            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void OrderingLAMBDA()
        {
            var queryLondonCustomers3 = context.clientes.Where(x => x.Ciudad == "Londres").OrderBy(x => x.NombreCompañia);
            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        ///</Ordering>



        ///<Gruping>
        static void Gruping()
        {
            var queryCustomersByCity =
                from cust in context.clientes
                group cust by cust.Ciudad;

            // customerGroup is an IGrouping<string,Customer>
            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine("  {0}", customer.NombreCompañia);
                }
            }
        }

        static void GrupingLAMBDA()
        {
           

            var queryCustomersByCity = context.clientes.GroupBy(x => x.Ciudad);

            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine("  {0}", customer.NombreCompañia);
                }
            }
        }

        ///</Gruping>



        ///<Grouping2>
        static void Grouping2()
        {
            var custQuery =
                from cust in context.clientes
                group cust by cust.Ciudad into custGroup
                where custGroup.Count() > 0
                orderby custGroup.Key
                select custGroup;
            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }
        static void Grouping2LAMBDA()
        {
            var custQuery = context.clientes
                .GroupBy(x => x.Ciudad)
                .Where(x => x.Count() > 0)
                .OrderBy(x => x.Key);
            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }
        ///</Grouping2>


        ///<Joining>
        static void Joining()
        {
            var innerJoinQuery =
                from cust in context.clientes
                join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };
            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }
        static void JoiningLAMBDA()
        {
  
            var innerJoinQuery = context.clientes.Join(context.Pedidos,
                cust => cust.idCliente,
                dist => dist.IdCliente,
            (cust, dist) => new
            {
                CustomerName = cust.NombreCompañia,
                DistributorName = dist.PaisDestinatario,
            });

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }
        ///</Joining>



    }
}
