import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
declare const $:any;
@Component({
  selector: 'app-campus',
  templateUrl: './campus.component.html',
  styleUrls: ['./campus.component.scss']
})
export class CampusComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  
    this.mapSetup();
    
  }
  // Use event binding instead of using routerLink directly, as the page reloads when using the area marker.
  private onBuildingSelected(event, buildingNo: number){
    event.preventDefault();
    this.router.navigate([`/building/${buildingNo}`]);
  }
  private mapSetup() : void{
    $(function(){
	
      $("img[usemap]").mapify({
      hoverClass: "custom-hover",
      popOver: {
        content: function(zone){ 
            return "<strong>"+zone.attr("data-title")+"</strong>"+zone.attr("data-translate");
        },
        delay: 0.7,
        margin: "5px",
        height: "130px",
        width: "250px"
      }
      });
  });
  }
}