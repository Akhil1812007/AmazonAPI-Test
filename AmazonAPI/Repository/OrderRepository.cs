using AmazonAPI.Models;


namespace AmazonAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AmazonContext _context;
        public OrderRepository(AmazonContext context)
        {
            _context = context;
        }

        public async  Task<OrderDetail> AddOrderDetail(OrderDetail orderDetail)
        {
            
            _context.Add(orderDetail);
            await _context.SaveChangesAsync();
            return orderDetail;
        }

        public async Task<OrderMaster> AddOrderMaster(OrderMaster orderMaster)
        {
            
            var IncompleteOrderMaster= (from i in _context.OrderMasters where i.CustomerId==orderMaster.CustomerId select i).OrderBy(x=>x.CustomerId).ToList();
            if (IncompleteOrderMaster != null)
            {
                foreach (var incompleteOrderMaster in IncompleteOrderMaster)
                {
                    if (incompleteOrderMaster.AmountPaid == null || incompleteOrderMaster.CardNumber == null)
                    {
                        _context.Remove(incompleteOrderMaster);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            
            
            _context.Add(orderMaster);
            await _context.SaveChangesAsync();
            return orderMaster;
        }

        public async Task<OrderDetail> GetOrderDetailById(int orderDetailId)
        {
            var od = await _context.OrderDetails.FindAsync(orderDetailId);
            return od;
        }

        public async  Task<OrderMaster> GetOrderMasterById(int orderMasterId)
        {

            var od = await  _context.OrderMasters.FindAsync(orderMasterId);
            return od;
        }
       
        public async  Task<OrderMaster> UpdateOrderMaster( OrderMaster orderMaster)
        {
            if (orderMaster.AmountPaid == orderMaster.total)
            {
                _context.Update(orderMaster);
                await _context.SaveChangesAsync();
                List<Cart> c=(from i in _context.carts where i.CustomerId==orderMaster.CustomerId select i ).ToList();

                foreach (Cart cart in c)
                {
                    Product p = (from j in _context.Products where j.ProductId == cart.ProductId select j).Single();
                    p.ProductQnt -= cart.ProductQuantity;
                    _context.Products.Update(p);

                    _context.carts.Remove(cart);
                    _context.SaveChanges(); 
                }
                return orderMaster;                       
            }
            else
            {
                return null;
            }
        }
    }
}
