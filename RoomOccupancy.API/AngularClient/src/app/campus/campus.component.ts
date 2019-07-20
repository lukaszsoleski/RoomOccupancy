import { Component, OnInit } from '@angular/core';
declare const $:any;
@Component({
  selector: 'app-campus',
  templateUrl: './campus.component.html',
  styleUrls: ['./campus.component.scss']
})
export class CampusComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  
    this.mapSetup();
    
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
