import {
  Component,
  OnInit,
  Input,
  AfterViewInit,
  EventEmitter,
  Output,
} from "@angular/core";
import {PagerService} from "./pager.service";

@Component({
  selector: "DataGrid",
  templateUrl: "./data-grid.component.html",
  providers: [PagerService],
})
export class DataGrid implements OnInit {
  @Input()
  ShowButtons;
  @Input()
  RowSelection;
  @Input()
  AllowExportExcel;
  @Input()
  HideFooter = false;

  @Input()
  CheckBoxHeder = "";
  @Input()
  ShowCheckBoxHeder = false;

  GridID;

  @Output()
  GridEvent = new EventEmitter<string[]>();

  @Output()
  selectedRowsEmit = new EventEmitter<string[]>();

  @Output()
  ExportToExcelEmit = new EventEmitter<any>();

  selectedRecord = "";
  selectedIndex;

  GridEventHandler(value, action, index, DisabledIfEmpty?) {
    if (
      DisabledIfEmpty !== undefined &&
      (DisabledIfEmpty == null || DisabledIfEmpty === "")
    ) {
      return;
    }

    const event = [value, action];
    this.GridEvent.emit(event);
    this.selectedIndex = index;
    this.selectedRecord = "selectedRow";
  }

  getStyleObj(strStyle) {
    return JSON.parse(strStyle);
  }

  // tslint:disable-next-line:member-ordering
  SelectedRows: string[] = [];

  DataSource: any;
  Columns: any;
  Buttons: any;
  HighlightedID = -1;

  dsGridList;
  // -----------------------------------------------//
  GridPager: any = {};
  GridPagedItems: any[];

  setGridPage(page: number, pageSize = 10) {
    console.log(page, Number(pageSize));

    this.dsGridList = this.DataSource;
    // get pager object from service
    this.GridPager = this.pagerService.getPager(
      this.dsGridList.length,
      page,
      pageSize);
    console.log(page === 1 ? page : (page - 1) * Number(pageSize), Number(pageSize));
    // get current page of items
    this.GridPagedItems = this.dsGridList.slice(
      page === 1 ? page-1 : (page - 1) * Number(pageSize)
      , page * Number(pageSize)
    );

    if (this.HideFooter) {
      this.GridPagedItems = this.dsGridList;
    }
  }

  sortDirection = "";
  sortedColumn = "";

  DrawSortIcon(col) {
    if (this.sortedColumn === col) {
      if (this.sortDirection === "asc") {
        return '<i class="fa fa-sort-asc" aria-hidden="true"></i>';
      } else if (this.sortDirection === "desc") {
        return '<i class="fa fa-sort-desc" aria-hidden="true"></i>';
      } else {
        return '<i class="fa fa-sort" aria-hidden="true"></i>';
      }
    }
    return '<i class="fa fa-sort" aria-hidden="true"></i>';
  }

  CalcSum(Col): string {
    return this.dsGridList[Col].reduce((sum, item) => sum + item, 0);
  }

  calc(col) {
    let sum = 0;
    for (let i = 0; i < this.dsGridList.length; i++) {
      sum += this.dsGridList[i][col];
    }
    return sum.toFixed(2);
  }

  calcAvg(col) {
    let sum = 0;
    for (let i = 0; i < this.dsGridList.length; i++) {
      sum += this.dsGridList[i][col];
    }
    return (sum / this.dsGridList.length).toFixed(2);
  }

  SortList(Col) {
    this.sortedColumn = Col;

    if (this.sortDirection === "") {
      this.sortDirection = "desc";
    }
    this.dsGridList = this.dsGridList.sort((leftSide, rightSide) => {
      if (leftSide[Col] === rightSide[Col]) {
        if (leftSide[Col] == null) {
          leftSide[Col] = "";
        }
        if (rightSide[Col] == null) {
          rightSide[Col] = "";
        }
      }

      if (this.sortDirection === "asc") {
        if (leftSide[Col] > rightSide[Col]) {
          return -1;
        }
        if (leftSide[Col] < rightSide[Col]) {
          return 1;
        }
      } else if (this.sortDirection === "desc") {
        if (leftSide[Col] < rightSide[Col]) {
          return -1;
        }
        if (leftSide[Col] > rightSide[Col]) {
          return 1;
        }
      }
      return 0;
    });

    this.setGridPage(1);
    if (this.sortDirection === "asc") {
      this.sortDirection = "desc";
    } else if (this.sortDirection === "desc") {
      this.sortDirection = "asc";
    }
  }

  constructor(private pagerService: PagerService) {
  }

  ngOnInit() {
  }

  bind(cols, dsList, gridID, dsButtons, SelectedRows = [], HighlightedID = -1) {
    this.selectedIndex = -1;
    this.selectedRecord = "";
    this.sortDirection = "";
    this.sortedColumn = "";
    this.SelectedRows = SelectedRows;
    this.HighlightedID = HighlightedID;
    if (!cols || cols.length === 0) {
      alert("Make sure to set grid columns");
      return;
    } else {
      // this.isEdit = this.EditColumn;
      this.GridID = gridID;
      this.DataSource = dsList;
      this.Columns = cols;

      this.Buttons = dsButtons;

      this.setGridPage(1);
    }
  }

  CheckAll(grdName, event) {
    this.SelectedRows = [];
    const checkboxs = document
      .getElementById(grdName)
      .getElementsByTagName("input");
    let elem;
    for (let i = 0; i < checkboxs.length; i++) {
      elem = checkboxs[i];
      if (elem != null) {
        if (elem.type === "checkbox") {
          elem.checked = event.currentTarget.checked;
          if (event.currentTarget.name !== elem.name) {
            if (elem.checked) {
              this.SelectedRows.push(elem.value);
            } else {
              const index = this.SelectedRows.findIndex(
                (x) => x === elem.value
              );
              if (index > -1) {
                this.SelectedRows.splice(index, 1);
              }
            }
          }
        }
      }
    }
    this.selectedRowsEmit.emit(this.SelectedRows);
  }

  HandleCheckedRows(event) {
    if (event.currentTarget.checked) {
      this.SelectedRows.push(event.currentTarget.value);
    } else {
      const index = this.SelectedRows.findIndex(
        (x) => x === event.currentTarget.value
      );
      if (index > -1) {
        this.SelectedRows.splice(index, 1);
      }
    }
    this.selectedRowsEmit.emit(this.SelectedRows);
  }

  parseInt(val) {
    return parseInt(val);
  }

  setPageSize(pageSize) {
    this.setGridPage(1, pageSize);
  }

  ExportExcel(val) {
    this.ExportToExcelEmit.emit(this.Columns);
  }
}
