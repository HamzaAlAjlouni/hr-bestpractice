/* tslint:disable */
export class Projects {
  id: number;
  evaluations: number[];
  Approvals: number[];
  Code: string;
  Name: string;
  CompanyId: number;
  Order: string;
  Weight: number;
  WeigthValue: number;
  UnitId: number;
  Category: number;
  Target: number;
  KPICycleId: number;
  KPITypeId: number;
  ResultUnitId: number;
  StratigicObjectiveId: number;
  BranchId: number;
  Description: string;
  KPI: string = "";
  CreatedBy: string;
  CreatedDate: Date;
  ModifiedBy: string;
  ModifiedDate: string;
  Q1_Target: Number;
  Q2_Target: Number;
  Q3_Target: Number;
  Q4_Target: Number;
  Q1_Disabled: boolean = true;
  Q2_Disabled: boolean = true;
  Q3_Disabled: boolean = true;
  Q4_Disabled: boolean = true;
  ActualCost: number;
  PlannedCost: number;
  p_type: number;
  Q1_Target_Required: boolean = false;
  Q2_Target_Required: boolean = false;
  Q3_Target_Required: boolean = false;
  Q4_Target_Required: boolean = false;


}
