export class Event {
  // itemID: number;
  // categoryID: number;
  // unitID: number;
  // displayOrder: number;
  // name: string;
  // categoryName: string;
  // arabicName: string;
  // description: string;
  // image: string;
  // barcode: string;
  // sku: string;
  // price: number;
  // cost: number;
  // itemType: string;
  // lastUpdatedBy: string;
  // lastUpdatedDate: string;
  // statusID: number;
  // isFeatured: boolean;
  // calories: number;
  // modifiers: string;
  // isApplyDiscount: boolean;
  // itemSettingTitle:string;
  // isItemSetting:boolean;
  
eventID: number;
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
  
}
export class EventImageJunc {
  eventImageID: number;
  eventID: number;
  image: string;
}
