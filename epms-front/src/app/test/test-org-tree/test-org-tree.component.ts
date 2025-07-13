import { Component, OnInit } from '@angular/core';


declare var OrgChart: any;
declare var BALKANGraph: any;

@Component({
    selector: 'app-test-org-tree',
    templateUrl: './test-org-tree.component.html',
    styleUrls: ['./test-org-tree.component.css']
})
export class TestOrgTreeComponent implements OnInit {
    
    constructor() {

    }

    ngOnInit() {
    }

    empList =
        [
            { id: 1,  name: "Denny Curtis", title: "CEO",img: "assets/dist/img/user1-128x128.jpg" },
            { id: 2, pid: 1, name: "Ashley Barnett", title: "Sales Manager" },
            { id: 3, pid: 1, name: "Caden Ellison", title: "Dev Manager" },
            { id: 4, pid: 1, name: "Elliot Patel", title: "Sales" },
            { id: 5, pid: 1, name: "Tanner May", title: "Developer" }, 
        ]

    drawchart() { 

        let chart = new OrgChart(document.getElementById("tree"), {
            lazyLoading: true,
             mouseScrool: OrgChart.action.zoom,
            //nodeMouseClick: OrgChart.action.none,
            //mouseScrool: OrgChart.action.none,
            enableDragDrop: true,
            template: "belinda",
            layout: OrgChart.tree,
            details: OrgChart.action.none,
            //scaleInitial:BALKANGraph.match.width,
            menu: {
                pdfPreview: {
                    text: "PDF Preview",
                    icon: OrgChart.icon.pdf(24, 24, '#7A7A7A'),
                    onClick: preview
                },
                pdf: { text: "Export PDF" },
                png: { text: "Export PNG" },
                svg: { text: "Export SVG" },
                csv: { text: "Export CSV" }
            },
            nodeMenu: {
                pdf: { text: "Export PDF" },
                png: { text: "Export PNG" },
                svg: { text: "Export SVG" },
                details: {
                    text: "Add Employee",
                    onClick: this.ngAddEmployee
                },
                remove: {
                    text: "Remove Employee",
                    onClick: (nodeID) => {
                        if (confirm("Are you sure remove emplyee?")) {
                            for (let i = 0; i < this.empList.length; i++) {
                                if (this.empList[i].id == nodeID) {
                                    this.empList.splice(i, 1);
                                }
                            }
                            this.drawchart();
                        }
                    }
                }
            },

            dragDropMenu: {
                addAsChild: {
                    text: "Add as child",
                    onClick: (nodeID, dropOnNodeId) => {
                        for (let i = 0; i < this.empList.length; i++) {
                            if (this.empList[i].id == nodeID) {
                                this.empList[i].pid = parseInt(dropOnNodeId);
                            }
                        }
                        this.drawchart();
                    }
                },

            },

            nodeBinding: {
                field_0: "name",
                field_1: "title",
                img_0: "img"
            },
        });

        function preview() {
            OrgChart.pdfPrevUI.show(chart, {
                format: 'A4'
            });
        }

        for (let i = 0; i < this.empList.length; i++) {
            chart.add(this.empList[i]);
        }

        chart.draw(BALKANGraph.action.init);
    }

    ngAddEmployee(nodeid) {
        document.getElementById("addEmpModal").style.display = 'block';
        document.getElementById("addEmpModal").className = "modal fade in";
    }


    SaveEmployee() {
        this.empList.push({ id: 6, pid: 3, name: "Zeyad Abed", title: "TTL" });
        document.getElementById("addEmpModal").className = "modal fade";
        document.getElementById("addEmpModal").style.display = 'none';
        this.drawchart();
    }

    closeEmployeeModal() {
        document.getElementById("addEmpModal").className = "modal fade";
        document.getElementById("addEmpModal").style.display = 'none';
    }

    savetree() {
        console.log(this.empList);
    }

}
