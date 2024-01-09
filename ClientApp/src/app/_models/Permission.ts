export class Permission {
  permissionID: number;
  roleName: string;
  formName: string;
  formAccess: boolean;

  permissionForm:PermissionForms[];
}

export class PermissionForms {
  formPermissionID: number;
  roleName: string;
  notification: number;
  doctor: number;
  mamjiUser: number;
  pharmacy: number;
  reception: number;
  diagnostic: number;
  reports: number;
  setting: number;

  
}
