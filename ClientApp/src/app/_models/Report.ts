export class Report {

}
export class EventdetailReport{
  eventID: string;
  eventCategoryID: number;
  organizerID: number;
  name : string;
  type : string;
  description: string;
  location: string;
  fromDate:string;
  toDate:string;
  eventDate : string;
  eventCity : string;
  locationLink : string;
  statusID : number;
  doorTime : string;
  phoneNo : string;
  email : string;
  remainingTicket :number;
  facebook : string;
  instagram : string;
  twitter : string;
  image : string;
  displayOrder:number;
  eventTime:string;

  AttendeesID:number;
  FullName:string;
  Email:string;
  PhoneNo:string;
  StatusID:number;

  userID: number;
  userName: string; 
  address: string;
  contactNo: string;
  password: string;
}
export class SummaryReport {
  totalSales: string;
  totalTax: string;
  totalDiscount: string;
  totalReturn: string;
  totalNetSales: string;
  totalSalesOrders: string;
  totalDeliveryOrders: string;
  totalPickUpOrders: string;
  totalCancelOrders: string;
  brandID: number;
}

export class SalesdetailReport {
  orderNo: number;
  transactionNo: number;
  customerID: string;
  customerName: string;
  customerMobile: string;
  orderDate: string;
  statusID: number;
  orderID: number;
  amountTotal: number;
  grandTotal: number;
  serviceCharges: number;
  discountAmount: number;
  tax: number;
}
export class SalesitemwiseReport {
  itemName: string;
  quantity: string;
  cost: string;
  price: string;
  profit: string;
  itemID: string;
}
export class SalescustomerwiseReport {
  orderNo: number;
  transactionNo: number;
  customerID: string;
  customerName: string;
  customerMobile: string;
  orderDate: string;
  statusID: number;
  orderID: number;
  amountTotal: number;
  grandTotal: number;
  serviceCharges: number;
  tax: number;
}
export class SalesuserwiseReport {
  orderNo: string;
  transactionNo: string;
  customerName: string;
  customerContact: string;
  orderDate: string;
  statusID: string;
  amountTotal: string;
  orderID: string;
}
export class SalescategorywiseReport {
  categoryName: string;
  quantity: string;
  cost: string;
  price: string;
  profit: string;
  itemID: string;
}