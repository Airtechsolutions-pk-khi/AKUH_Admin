import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AlertModule } from './_alert/alert.module'

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './admin/dashboard/dashboard.component';
import { LayoutComponent } from './layout/layout.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CategoryComponent } from './admin/menu/category/category.component';
import { AddcategoryComponent } from './admin/menu/category/addcategory/addcategory.component';
import { ImageuploadComponent } from './imageupload/imageupload.component';
import { ImageViewComponent } from './imageview/imageview.component';
import { ItemsComponent } from './admin/menu/items/items.component';
import { AdditemsComponent } from './admin/menu/items/additem/additem.component';
import { ModifiersComponent } from './admin/menu/modifiers/modifiers.component';
import { AddmodifierComponent } from './admin/menu/modifiers/addmodifier/addmodifier.component';

import { CustomersComponent } from './admin/reception/customers/customers.component';
import { AddcustomerComponent } from './admin/reception/customers/addcustomers/addcustomer.component';

import { LocationsComponent } from './admin/company/locations/locations.component';
import { AddlocationComponent } from './admin/company/locations/addlocation/addlocation.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { AddbrandComponent } from './admin/company/brands/addbrand/addbrand.component';

/*import { NgApexchartsModule } from 'ng-apexcharts';*/
import { ToastrModule } from 'ngx-toastr';
import { BrandComponent } from './admin/company/brands/brands.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SummaryComponent } from './admin/report/summary/summary.component';
import { NgbdDatepickerRangePopup } from './datepicker-range/datepicker-range-popup';
import { BannerComponent } from './admin/settings/banner/banner.component';
import { AddbannerComponent } from './admin/settings/banner/addbanner/addbanner.component';
import { SalesdetailComponent } from './admin/report/salesdetail/salesdetail.component';
import { SalesuserwiseComponent } from './admin/report/salesuserwise/salesuserwise.component';
import { SalescustomerwiseComponent } from './admin/report/salescustomerwise/salescustomerwise.component';
import { SalescategorywiseComponent } from './admin/report/salescategorywise/salescategorywise.component';
import { SalesitemwiseComponent } from './admin/report/salesitemwise/salesitemwise.component';
import { PromotionComponent } from './admin/settings/promotion/promotion.component';
import { AddpromotionComponent } from './admin/settings/promotion/addpromotion/addpromotion.component';

import { ItemsettingsComponent } from './admin/menu/items/itemsettings/itemsettings.component';
import { ExcelService } from 'src/ExportExcel/excel.service';
import { DeliveryComponent } from './admin/settings/delivery/delivery.component';


import { AppsettingComponent } from './admin/settings/appsettings/appsettings.component';
import { AddsettingsComponent } from './admin/settings/appsettings/addappsettings/addsettings.component';

import { AddonsComponent } from './admin/menu/addons/addons.component';
import { AddaddonsComponent } from './admin/menu/addons/addaddons/addaddons.component';
import { DoctorComponent } from './admin/managedoctor/doctor/doctor.component';
import { AdddoctorsComponent } from './admin/managedoctor/doctor/adddoctors/adddoctors.component';

import { PrescriptionComponent } from './admin/pharmacy/prescription/prescription.component';
import { AddprescriptionComponent } from './admin/pharmacy/prescription/add/addprescription.component'

import { AppointmentComponent } from './admin/reception/appointment/appointment.component';
import { AppointmentdetailsComponent } from './admin/reception/appointmentdetails/appointmentdetails.component';

import { UploadreportComponent } from './admin/laboratory/uploadreport/uploadreport.component';
import { AddreportsComponent } from './admin/laboratory/uploadreport/addreports/addreports.component';

import { DiagnosticCategoriesComponent } from './admin/laboratory/diagnosticcategories/diagnosticcategories.component';
import { AddCategoryComponent } from './admin/laboratory/diagnosticcategories/add/addcategory.component';

import { DeliverydetailComponent } from './admin/pharmacy/deliverydetail/deliverydetail.component';
import { CouponComponent } from './admin/settings/coupon/coupon.component';
import { AddCouponComponent } from './admin/settings/coupon/addcoupon/addcoupon.component';
import { MedicineComponent } from './admin/pharmacy/medicine/medicine.component';
import { AddmedicineComponent } from './admin/pharmacy/medicine/addmedicines/addmedicine.component'
import { OrdersComponent } from './admin/pharmacy/orders/orders.component';
import { OrderdetailsComponent } from './admin/pharmacy/orderdetails/orderdetails.component';
import { ServiceComponent } from './admin/settings/medicalservices/service.component';
import { AddServiceComponent } from './admin/settings/medicalservices/add/addservice.component';
import { MedicalServicetypeComponent } from './admin/settings/medicalservicetype/medicalservicetype.component';
import { AddMedicalServicetypeComponent } from './admin/settings/medicalservicetype/add/addmedicalservicetype.component';
import { NursingAppointmentComponent } from './admin/reception/nursingappointment/nursingappointment.component';
import { AddnursingappointmentComponent } from './admin/reception/nursingappointment/addnursingappointment/addnursingappointment.component';
import { NotificationComponent } from './admin/notification/notification.component';
import { SpecialityComponent } from '././admin/settings/speciality/speciality.component'
import { AddSpecialityComponent } from '././admin/settings/speciality/add/addspeciality.component'
import { UserComponent } from './admin/user/user.component';
import { AdduserComponent } from './admin/user/add/adduser.component';
import { HasRoleGuard } from './has-role.guard';
import { isAbsolute } from 'path';
import { UserService } from './_services/user.service';
import { NgApexchartsModule } from 'ng-apexcharts';
import { PermissionComponent } from './admin/user/permission/permission.component';
import { AddComponent } from './admin/user/permission/add/add.component';
import { TimeSlotComponent } from './admin/settings/timeslot/timeslot.component';
import { AddTimeSlotComponent } from './admin/settings/timeslot/add/addtimeslot.component';
import { FormPermissionComponent } from './admin/permission/formpermission.component';
import { AdddeliveryComponent } from './admin/settings/delivery/adddelivery/adddelivery.component';
import { CorporateClientComponent } from './admin/settings/corporateclient/corporateclient.component';
import { addcorporateclientComponent } from './admin/settings/corporateclient/addcorporateclient/addcorporateclient.component';
import { SpeakerComponent } from './admin/speaker/speaker.component';
import { AddSpeakerComponent } from './admin/speaker/addspeaker/addspeaker.component';
import { OrganizerComponent } from './admin/organizer/organizer.component';
import { AddOrganizerComponent } from './admin/organizer/add/addorganizer.component';
import { PartnerComponent } from './admin/partner/partner.component';
import { AddPartnerComponent } from './admin/partner/add/addpartner.component';
import { EventCategoryComponent } from './admin/eventCategory/eventcategory.component';
import { AddEventCategoryComponent } from './admin/eventCategory/add/addeventcategory.component';
import { AddEventComponent } from './admin/event/addevent/addevent.component';
import { EventComponent } from './admin/event/event.component';
import { FaqComponent } from './admin/faq/faq.component';
import { AddFaqComponent } from './admin/faq/add/addfaq.component';
import { EventdetailComponent } from './admin/eventdetail/eventdetail.component';
import { ConfirmlistreportComponent } from './admin/confirmlistreport/confirmlistreport.component';
import { EventAttendeesComponent } from './admin/eventattendees/eventattendees.component';
import { AddEventAttendeesComponent } from './admin/eventattendees/addeventattendees/addeventattendees.component';
import { UserEventReportComponent } from './admin/usereventreport/usereventreport.component';
// Imported Syncfusion RichTextEditorModule from Rich Text Editor package
import { RichTextEditorModule } from '@syncfusion/ej2-angular-richtexteditor';
import { EventattendeedetailsComponent } from './admin/eventattendeedetails/eventattendeedetails.component';
import { UserDetailComponent } from './admin/userdetails/userdetails.component';
import { MessageComponent } from './admin/message/message.component';
import { AddMessageComponent } from './admin/message/add/addmessage.component';
import { OrganisingCommitteeComponent } from './admin/organisingcommittee/organisingcommittee.component';
import { AddOrganisingCommitteeComponent } from './admin/organisingcommittee/addorganisingcommittee/addorganisingcommittee.component';
import { WorkshopComponent } from './admin/workshop/workshop.component';
import { AddWorkshopComponent } from './admin/workshop/addworkshop/addworkshop.component';
import { CommonModule } from '@angular/common';
import { PopupBannerComponent } from './admin/settings/popupbanner/popupbanner.component';
import { AddPopupBannerComponent } from './admin/settings/popupbanner/add/addpopupbanner.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    DashboardComponent,
    LayoutComponent,
    CounterComponent,
    LoginComponent,
    FetchDataComponent,
    CategoryComponent,
    AddcategoryComponent,
    ItemsComponent,
    AdditemsComponent,
    EventComponent,
    AddEventComponent,
    ModifiersComponent,
    AddmodifierComponent,
    CustomersComponent,
    AddcustomerComponent,
    BrandComponent,
    AddbrandComponent,
    LocationsComponent,
    AddlocationComponent,
    ImageuploadComponent,
    ImageViewComponent,
    SummaryComponent,
    NgbdDatepickerRangePopup,
    BannerComponent,
    AddbannerComponent,
    PromotionComponent,
    AddpromotionComponent,
    SalesdetailComponent,
    SalescategorywiseComponent,
    SalescustomerwiseComponent,
    SalesitemwiseComponent,
    SalesuserwiseComponent,
    ItemsettingsComponent,
    DeliveryComponent,
    AdddeliveryComponent,
    AppsettingComponent,
    AddonsComponent,
    AddaddonsComponent,
    DoctorComponent,
    AdddoctorsComponent,
    CouponComponent,
    AddCouponComponent,
    PrescriptionComponent,
    AddprescriptionComponent,
    AppointmentComponent,
    AppointmentdetailsComponent,
    NursingAppointmentComponent,
    AddnursingappointmentComponent,
    UploadreportComponent,
    AddreportsComponent,
    MedicineComponent,
    AddmedicineComponent,
    OrdersComponent,
    OrderdetailsComponent,
    AddsettingsComponent,
    ServiceComponent,
    AddServiceComponent,
    MedicalServicetypeComponent,
    AddMedicalServicetypeComponent,
    UserComponent,
    AdduserComponent,
    NotificationComponent,
    AddSpecialityComponent,
    SpecialityComponent,
    PermissionComponent,
    AddComponent,
    DiagnosticCategoriesComponent,
    AddCategoryComponent,
    TimeSlotComponent,
    AddTimeSlotComponent,
    FormPermissionComponent,
    CorporateClientComponent,
    addcorporateclientComponent,
    SpeakerComponent,
    AddSpeakerComponent,
    OrganizerComponent,
    AddOrganizerComponent,
    EventCategoryComponent,
    AddEventCategoryComponent,
    PartnerComponent,
    AddPartnerComponent,
    FaqComponent,
    AddFaqComponent,
    EventdetailComponent,
    ConfirmlistreportComponent,
    EventAttendeesComponent,
    AddEventAttendeesComponent,
    UserEventReportComponent,
    EventattendeedetailsComponent,
    UserDetailComponent,
    MessageComponent,
    AddMessageComponent,
    OrganisingCommitteeComponent,
    AddOrganisingCommitteeComponent,
    WorkshopComponent,
    AddWorkshopComponent,
    PopupBannerComponent,
    AddPopupBannerComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    CommonModule,
    NgApexchartsModule,
    FormsModule,
    NgSelectModule,
    ReactiveFormsModule,
    RichTextEditorModule,
    AlertModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    /*    MatDialogModule,*/
    /*    NgApexchartsModule,*/
    RouterModule.forRoot([
      { path: '', component: LoginComponent, pathMatch: 'full' },
      {
        path: 'admin', component: LayoutComponent,
        children: [
          { path: 'dashboard', component: DashboardComponent },
          { path: 'category', component: CategoryComponent },
          { path: 'category/add', component: AddcategoryComponent },
          { path: 'category/edit/:id', component: AddcategoryComponent },

          { path: 'item', component: ItemsComponent },
          { path: 'item/add', component: AdditemsComponent },
          { path: 'item/settings', component: ItemsettingsComponent },
          { path: 'item/edit/:id', component: AdditemsComponent },

          { path: 'event', component: EventComponent },
          { path: 'event/add', component: AddEventComponent },
          { path: 'event/edit/:id', component: AddEventComponent },

          { path: 'eventattendees', component: EventAttendeesComponent },
          { path: 'eventattendees/add', component: AddEventAttendeesComponent },
          { path: 'eventattendees/edit/:id', component: AddEventAttendeesComponent },

          { path: 'modifier', component: ModifiersComponent },
          { path: 'modifier/add', component: AddmodifierComponent },
          { path: 'modifier/edit/:id', component: AddmodifierComponent },

          { path: 'notification', component: NotificationComponent },

          { path: 'location', component: LocationsComponent },
          { path: 'location/add', component: AddlocationComponent },
          { path: 'location/edit/:id', component: AddlocationComponent },

          { path: 'eventattendeedetails/edit/:id', component: EventattendeedetailsComponent },

          { path: 'brand', component: BrandComponent },
          { path: 'brand/add', component: AddbrandComponent },
          { path: 'brand/edit/:id', component: AddbrandComponent },

          { path: 'settings/banner', component: BannerComponent },
          { path: 'settings/banner/add', component: AddbannerComponent },
          { path: 'settings/banner/edit/:id', component: AddbannerComponent },

          { path: 'settings/popupbanner', component: PopupBannerComponent },
          { path: 'settings/popupbanner/add', component: AddPopupBannerComponent },
          { path: 'settings/popupbanner/edit/:id', component: AddPopupBannerComponent },

          { path: 'settings/corporateclient', component: CorporateClientComponent },
          { path: 'settings/corporateclient/add', component: addcorporateclientComponent },
          { path: 'settings/corporateclient/edit/:id', component: addcorporateclientComponent },

          { path: 'promotion', component: PromotionComponent },
          { path: 'promotion/add', component: AddpromotionComponent },
          { path: 'promotion/edit/:id', component: AddpromotionComponent },

          { path: 'report/summary', component: SummaryComponent },
          { path: 'report/salesdetail', component: SalesdetailComponent },
          { path: 'report/salesuserwise', component: SalesuserwiseComponent },
          { path: 'report/salescustomerwise', component: SalescustomerwiseComponent },
          { path: 'report/salescategorywise', component: SalescategorywiseComponent },
          { path: 'report/salesitemwise', component: SalesitemwiseComponent },
          //{ path: 'report/userwise', component: UserWiseEventComponent },

          { path: 'delivery', component: DeliveryComponent },
          { path: 'delivery/add', component: AdddeliveryComponent },
          { path: 'delivery/edit/:id', component: AdddeliveryComponent },

          { path: 'settings/appsettings', component: AppsettingComponent },
          { path: 'settings/appsettings/add', component: AddsettingsComponent },
          { path: 'settings/appsettings/edit/:id', component: AddsettingsComponent },

          { path: 'addons', component: AddonsComponent },
          { path: 'addons/add', component: AddaddonsComponent },
          { path: 'addons/edit/:id', component: AddaddonsComponent },

          { path: 'managedoctor/doctor', component: DoctorComponent },
          { path: 'managedoctor/doctor/adddoctors', component: AdddoctorsComponent },
          { path: 'managedoctor/doctor/edit/:id', component: AdddoctorsComponent },

          { path: 'pharmacy/prescription', component: PrescriptionComponent },
          { path: 'pharmacy/prescription/addprescription', component: AddprescriptionComponent },
          { path: 'pharmacy/prescription/edit/:id', component: AddprescriptionComponent },

          { path: 'reception/customers', component: CustomersComponent },
          { path: 'reception/customers/addcustomers', component: AddcustomerComponent },
          { path: 'reception/customers/edit/:id', component: AddcustomerComponent },

          { path: 'reception/appointment', component: AppointmentComponent },
          { path: 'appointment/view/:id', component: AppointmentdetailsComponent },

          { path: 'reception/nursingappointment', component: NursingAppointmentComponent },
          { path: 'reception/nursingappointment/addnursingappointment', component: AddnursingappointmentComponent },
          { path: 'reception/nursingappointment/edit/:id', component: AddnursingappointmentComponent },

          { path: 'laboratory/uploadreport', component: UploadreportComponent },
          { path: 'laboratory/uploadreport/addreports', component: AddreportsComponent },
          { path: 'laboratory/uploadreport/edit/:id', component: AddreportsComponent },

          { path: 'laboratory/diagnosticcategory', component: DiagnosticCategoriesComponent },
          { path: 'laboratory/diagnosticcategory/add', component: AddCategoryComponent },
          { path: 'laboratory/diagnosticcategory/edit/:id', component: AddCategoryComponent },

          { path: 'pharmacy/deliverydetail', component: DeliverydetailComponent },

          { path: 'settings/coupon', component: CouponComponent },
          { path: 'settings/coupon/add', component: AddCouponComponent },
          { path: 'settings/coupon/edit/:id', component: AddCouponComponent },

          {
            path: 'pharmacy/medicine',
            component: MedicineComponent,
            canActivate: [HasRoleGuard],
            data: {
              type: ['SuperAdmin']
            }
          },
          { path: 'pharmacy/medicine/addmedicines', component: AddmedicineComponent },
          { path: 'pharmacy/medicine/edit/:id', component: AddmedicineComponent },

          { path: 'pharmacy/orders', component: OrdersComponent },
          { path: 'orders/view/:id', component: OrderdetailsComponent },

          { path: 'settings/medicalservices', component: ServiceComponent },
          { path: 'settings/medicalservices/add', component: AddServiceComponent },
          { path: 'settings/medicalservices/edit/:id', component: AddServiceComponent },

          { path: 'settings/medicalservicetype', component: MedicalServicetypeComponent },
          { path: 'settings/medicalservicetype/add', component: AddMedicalServicetypeComponent },
          { path: 'settings/medicalservicetype/edit/:id', component: AddMedicalServicetypeComponent },

          { path: 'user', component: UserComponent },
          { path: 'user/add', component: AdduserComponent },
          { path: 'user/edit/:id', component: AdduserComponent },
          { path: 'user/view/:id', component: UserDetailComponent },

          { path: 'permission', component: PermissionComponent },
          { path: 'permission/add', component: AddComponent },
          { path: 'permission/edit/:id', component: AddComponent },

          { path: 'settings/speciality', component: SpecialityComponent },
          { path: 'settings/speciality/add', component: AddSpecialityComponent },
          { path: 'settings/speciality/edit/:id', component: AddSpecialityComponent },

          { path: 'settings/timeslot', component: TimeSlotComponent },
          { path: 'settings/timeslot/add', component: AddTimeSlotComponent },
          { path: 'settings/timeslot/edit/:id', component: AddTimeSlotComponent },

          { path: 'formpermission', component: FormPermissionComponent },

          { path: 'speaker', component: SpeakerComponent },
          { path: 'speaker/add', component: AddSpeakerComponent },
          { path: 'speaker/edit/:id', component: AddSpeakerComponent },

          { path: 'organisingcommittee', component: OrganisingCommitteeComponent },
          { path: 'organisingcommittee/add', component: AddOrganisingCommitteeComponent },
          { path: 'organisingcommittee/edit/:id', component: AddOrganisingCommitteeComponent },

          { path: 'organizer', component: OrganizerComponent },
          { path: 'organizer/add', component: AddOrganizerComponent },
          { path: 'organizer/edit/:id', component: AddOrganizerComponent },

          { path: 'workshop', component: WorkshopComponent },
          { path: 'workshop/add', component: AddWorkshopComponent },
          { path: 'workshop/edit/:id', component: AddWorkshopComponent },

          { path: 'message', component: MessageComponent },
          { path: 'message/add', component: AddMessageComponent },
          { path: 'message/edit/:id', component: AddMessageComponent },

          { path: 'partner', component: PartnerComponent },
          { path: 'partner/add', component: AddPartnerComponent },
          { path: 'partner/edit/:id', component: AddPartnerComponent },

          { path: 'eventcategory', component: EventCategoryComponent },
          { path: 'eventcategory/add', component: AddEventCategoryComponent },
          { path: 'eventcategory/edit/:id', component: AddEventCategoryComponent },

          { path: 'eventdetail', component: EventdetailComponent },

          { path: 'confirmlistreport', component: ConfirmlistreportComponent },

          { path: 'userdetailreport', component: UserEventReportComponent },

          { path: 'faq', component: FaqComponent },
          { path: 'faq/add', component: AddFaqComponent },
          { path: 'faq/edit/:id', component: AddFaqComponent },
        ]
      }
    ]),
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),
    NgbModule
  ],
  providers: [
    [HasRoleGuard],
  ],
  exports: [NgbdDatepickerRangePopup],
  bootstrap: [AppComponent, NgbdDatepickerRangePopup]
})
export class AppModule { }
