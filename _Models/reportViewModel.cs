using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
    public class reportViewModel
    {
    }
    public class EventDetailsBLL
    {
        public int? EventID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string EventTime { get; set; }
        public int? AttendeesID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Occupation { get; set; }
        public string Gender { get; set; }
        public int StatusID { get; set; }
    }
    public class salesSummarytBLL
    {
        public float TotalSales { get; set; }
        public float TotalTax { get; set; }
        public float TotalDiscount { get; set; }
        public float TotalReturn { get; set; }
        public float TotalNetSales { get; set; }
        public int TotalSalesOrders { get; set; }
        public int TotalDeliveryOrders { get; set; }

        public int TotalPickUpOrders { get; set; }
        public int TotalCancelOrders { get; set; }

    }
    public class SalesDetailBLL
    {
        public int OrderNo { get; set; }
        public int TransactionNo { get; set; }
        public float GrandTotal { get; set; }
        public float Tax { get; set; }
        public float ServiceCharges { get; set; }
        public float AmountTotal { get; set; }
        public float DiscountAmount { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerMobile { get; set; }
        public int CustomerID { get; set; }
        public int StatusID { get; set; }
        public int OrderID { get; set; }
        public DateTime OrderDate{ get; set; }

    }
    public class SalesUserwiseBLL
    {
        public int OrderNo { get; set; }
        public int TransactionNo { get; set; }
        public string customerName { get; set; }
        public string customerContact { get; set; }
        public DateTime orderDate { get; set; }
        public int statusID { get; set; }
        public float amountTotal { get; set; }

        public int orderID { get; set; }

    }
    public class SalesCustomerwiseBLL
    {
        public int OrderNo { get; set; }
        public int TransactionNo { get; set; }
        public float GrandTotal { get; set; }
        public float Tax { get; set; }
        public float ServiceCharges { get; set; }
        public float AmountTotal { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerMobile { get; set; }
        public int CustomerID { get; set; }
        public int StatusID { get; set; }
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }

    }
    public class SalesItemwiseBLL
    {
        public string ItemName { get; set; }
        public int ItemID { get; set; }
        public float? Quantity { get; set; }
        public float? Cost { get; set; }
        public float? Price { get; set; }
        public float? Profit { get; set; }
    }
    public class SalesCategorywiseBLL
    {
        public string CategoryName { get; set; }
        public int ItemID { get; set; }
        public float? Quantity { get; set; }
        public float? Cost { get; set; }
        public float? Price { get; set; }
        public float? Profit { get; set; }

    }
}
