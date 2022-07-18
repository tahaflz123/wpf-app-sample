using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WpfApp1.Data;
using WpfApp1.Model;
namespace WpfApp1.Service
{
    public class OrderService
    {

        private UserWPFContext _context { get; set; }


        public OrderService()
        {
            _context = new UserWPFContext();
        }

        public OrderService(UserWPFContext userWPFContext)
        {
            _context = userWPFContext;
        }

        public List<Order> refreshOrders()
        {
            return findAll();
        }

        public Order saveOrder(Order order)
        {
            if (order is null)
                return null;


            Order saved = _context.orders.Add(order).Entity;
            _context.SaveChanges();

            return saved;
        }

        public List<Order> findAll()
        {
            return _context.orders.Select(e => e)
                                  .Include(e => e.creator)
                                  .ToList();
        }

        public void deleteOrder(Order order)
        {
            if (order == null)
            {
                return;
            }
            _context.orders.Remove(order);
            _context.SaveChanges();



        }

}
}
