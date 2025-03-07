import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Recintos } from 'src/app/models/Recintos';
import { RecintosRestService } from 'src/app/services/recintos-rest.service';

@Component({
  selector: 'app-recintos-show',
  templateUrl: './recintos-show.component.html',
  styleUrls: ['./recintos-show.component.css']
})
export class RecintosShowComponent implements OnInit {

  @Input() recinto?: Recintos;
  @Output() tabSelected = new EventEmitter<string>();
  option?: string;

  constructor(private rest: RecintosRestService, private route: ActivatedRoute, private router: Router, private http: HttpClient) {
  }

  ngOnInit(): void {
    var idTemp = this.route.snapshot.params['id'];
    this.rest.getRecinto(idTemp).subscribe((data: Recintos) => {
      this.recinto = data;
    })
  }


  selectTab(tab: string): void {
    this.tabSelected.emit(tab);
    this.option = tab;
  }

}
