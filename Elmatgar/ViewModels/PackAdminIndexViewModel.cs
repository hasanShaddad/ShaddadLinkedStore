using Elmatgar.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Elmatgar.ViewModels
{
    public class PackAdminIndexViewModel
    {
        readonly StoreDbContext _context = new StoreDbContext();
        //private IEnumerable<Orders> Orders { get; set; }
        //private IEnumerable<SupplyOrders> SupplyOrders { get; set; }

        public int OrdersFirstWeek { get; set; }
        public int OrdersSecondWeek { get; set; }
        public int OrdersThirdWeek { get; set; }
        public int OrdersForthWeek { get; set; }
        public int OrdersLastWeek { get; set; }
        public int SupplyOrdersFirstWeek { get; set; }
        public int SupplyOrdersSecondWeek { get; set; }
        public int SupplyOrdersThirdWeek { get; set; }
        public int SupplyOrdersForthWeek { get; set; }
        public int SupplyOrdersLastWeek { get; set; }
        public List<string> CustomerGeoCity  { get; set; }
        public List<int> CustomerGeoCount { get; set; }
 
        //public ILookup<int?, int> GeoCustomer { get; set; }


        public PackAdminIndexViewModel()
        {
            /////// Customer geo  
            // GeoCustomer = _context.Customers.ToLookup(x => x.CCityId, x => x.Id.Count());
            var geo = _context.Customers.GroupBy(x => x.Cities.CtCityName).Select(grp => new { name = grp.Key ?? "UnKnown", total = grp.Count() });

            CustomerGeoCity = geo.Select(x => x.name).ToList();
            CustomerGeoCount = geo.Select(x => x.total).ToList();
            /////////////////////////////
            var currentdate = DateTime.Now;
            var firstweek5 = currentdate.AddDays(-7);
            var firstweek4 = currentdate.AddDays(-14);
            var firstweek3 = currentdate.AddDays(-21);
            var firstweek2 = currentdate.AddDays(-28);
            var firstWeek1 = DateTime.Today.AddDays(-35);
            ///// orders
            var orders = _context.Orders
                  .Where(e => e.OrderStatus == OrderStatus.Promoted.ToString()
                  && e.LastUpdateDate >= firstWeek1)
                  .OrderBy(e => e.LastUpdateDate);
            OrdersFirstWeek =
                orders.Count(e => e.LastUpdateDate < firstweek2 && e.LastUpdateDate >= firstWeek1);
            OrdersSecondWeek =
                orders.Count(e => e.LastUpdateDate < firstweek3 && e.LastUpdateDate >= firstweek2);
            OrdersThirdWeek =
                orders.Count(e => e.LastUpdateDate < firstweek4 && e.LastUpdateDate >= firstweek3);
            OrdersForthWeek =
                orders.Count(e => e.LastUpdateDate < firstweek5 && e.LastUpdateDate >= firstweek4);
            OrdersLastWeek =
                orders.Count(e => e.LastUpdateDate <= currentdate && e.LastUpdateDate >= firstweek5);

            ////////////////////////////////// SupplyOrder
            var supplyOrders = _context.SupplyOrder.Where(e => e.OrderDate >= firstWeek1)
                   .OrderBy(e => e.OrderDate);
            SupplyOrdersFirstWeek =
            supplyOrders.Count(e => e.OrderDate < firstweek2 && e.OrderDate >= firstWeek1);
            SupplyOrdersSecondWeek =
                supplyOrders.Count(e => e.OrderDate < firstweek3 && e.OrderDate >= firstweek2);
            SupplyOrdersThirdWeek =
                supplyOrders.Count(e => e.OrderDate < firstweek4 && e.OrderDate >= firstweek3);
            SupplyOrdersForthWeek =
                supplyOrders.Count(e => e.OrderDate < firstweek5 && e.OrderDate >= firstweek4);
            SupplyOrdersLastWeek =
                supplyOrders.Count(e => e.OrderDate <= currentdate && e.OrderDate >= firstweek5);

          

        }
    }
}