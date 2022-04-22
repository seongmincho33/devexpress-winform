using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDMS_TEST
{
    // The basic Customer class.
    public class Customer : System.Object
    {
        private string custName = "";
        protected ArrayList custOrders = new ArrayList();

        public Customer(string customername)
        {
            this.custName = customername;
        }

        public string CustomerName
        {
            get { return this.custName; }
            set { this.custName = value; }
        }

        public ArrayList CustomerOrders
        {
            get { return this.custOrders; }
        }
    } // End Customer class 

    // The basic customer Order class.
    public class Order : System.Object
    {
        private string ordID = "";

        public Order(string orderid)
        {
            this.ordID = orderid;
        }

        public string OrderID
        {
            get { return this.ordID; }
            set { this.ordID = value; }
        }
    } // End Order class
}
