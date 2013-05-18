using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace course_work
{
    public class Promotions: Products
    {
        private double discountPrice;

        public double DiscountPrice
        {
            get { return discountPrice; }
            set { discountPrice = value; }
        }
        private int discount;

        public int Discount
        {
            get { return discount; }
            set { discount = value; }
        }
    }
}
