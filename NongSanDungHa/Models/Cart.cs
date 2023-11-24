using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NongSanDungHa.Models
{
    public class CartItem
    {
        public product _shopping_product { get; set; }
        public double _shopping_quantity { get; set; }
       
    }
    public class Cart
    {
        public static decimal shipping_fee = 30;
        List<CartItem> items= new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        
        public void Add(product _pro, double _quantity)
        {
            var item = items.FirstOrDefault(s => s._shopping_product.product_id == _pro.product_id);
            if(item == null)
            {
                items.Add(new CartItem
                {
                    _shopping_product = _pro,
                    _shopping_quantity = _quantity
                }) ;
                

            }
            else
            {
                item._shopping_quantity += _quantity;
            } 
                
            
        }
        public void Update_Quantity_Increase(int id, double _quantity)
        {
            var item = items.Find(x=> x._shopping_product.product_id == id);
            if (item!= null)
            {
                _quantity += 0.5;
                item._shopping_quantity = _quantity;
            }
        }
        public void Update_Quantity_Decrease(int id, double _quantity)
        {
            var item = items.Find(x => x._shopping_product.product_id == id);
            if (item != null && _quantity >0 && _quantity != 0.5)
            {
                _quantity -= 0.5;
                item._shopping_quantity = _quantity;
            }
        }
        public void Delete(int id)
        {
            items.RemoveAll(x => x._shopping_product.product_id == id);
        }
        public decimal Total_Money()
        {
            decimal total = Convert.ToDecimal(items.Sum(x => x._shopping_product.product_price * Convert.ToDecimal(x._shopping_quantity)));
            return total + shipping_fee;
        }

    }
}