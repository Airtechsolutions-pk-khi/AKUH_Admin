export class Event {
  
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
eventTime:string;
  
}
export class EventImageJunc {
  eventImageID: number;
  eventID: number;
  image: string;
}
