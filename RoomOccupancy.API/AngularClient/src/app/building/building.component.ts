import { Component, OnInit} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-building',
  templateUrl: './building.component.html',
  styleUrls: ['./building.component.scss']
})
export class BuildingComponent implements OnInit {

  public selectedBuilding: string;

  constructor( private readonly route : ActivatedRoute) { }

  ngOnInit() {
   this.route.paramMap.subscribe(params => this.selectedBuilding = params.get('id'));
  }
}
