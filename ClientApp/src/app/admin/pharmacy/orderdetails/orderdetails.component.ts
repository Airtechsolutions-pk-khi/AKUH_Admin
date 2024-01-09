import { Component, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerOrders, OrderCheckout, OrderDetails, Orders } from 'src/app/_models/Orders';
import { ToastService } from 'src/app/_services/toastservice';
import { Location } from 'src/app/_models/Location';
import { OrdersService } from 'src/app/_services/orders.service';
@Component({
  selector: 'app-orderdetails',
  templateUrl: './orderdetails.component.html',
  providers: []
})

export class OrderdetailsComponent implements OnInit {
  public order = new Orders();
  private selectedBrand;
  StatusMsg="";
  Locations: Location[] = [];
  selectedLocations = [];
  locationID = 0;
  public orderDetails = new OrderDetails();
  public orderOrderCheckout = new Orders();
  public orderCustomerInfo = new CustomerOrders();

  locationSubscription: Subscription;
  constructor(public service: OrdersService,
    public ls: LocalStorageService,
    public ts: ToastService,
    public router: Router,
    private route: ActivatedRoute) {    
  }

  ngOnInit() {
    this.setSelectedOrder();

  }

  setSelectedOrder() {
    debugger
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.service.getById(sid).subscribe(res => {
          this.editForm(res);
        });
      }
    })
  }

  updateOrder(order, status) {
    debugger
    order.statusID = status;
    order.statusMsg = this.StatusMsg;
    //Update 
    this.service.update(order).subscribe(data => {

      if (data != 0) {
        this.ts.showSuccess("Success", "Record updated successfully.")
        this.router.navigate(['/admin/orders']);
      }
    }, error => {
      this.ts.showError("Error", "Failed to update record.")
    });
  }
  private editForm(obj) {
    debugger
    this.order = obj.order;
    this.orderDetails = obj.orderDetails;
    this.orderCustomerInfo = obj.customerOrders;
    this.orderOrderCheckout = obj.orderCheckouts;
  }
}
