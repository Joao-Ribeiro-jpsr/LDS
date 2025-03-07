import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Recintos } from 'src/app/models/Recintos';
import { RecintosRestService } from 'src/app/services/recintos-rest.service';

@Component({
  selector: 'app-recintos-descriptions',
  templateUrl: './recintos-descriptions.component.html',
  styleUrls: ['./recintos-descriptions.component.css']
})
export class RecintosDescriptionsComponent implements OnInit {

  @Input() recinto?: Recintos;

  constructor(private rest: RecintosRestService, private route: ActivatedRoute, private router: Router, private http: HttpClient) {
  }

  ngOnInit(): void {
    var idTemp = this.route.snapshot.params['id'];
    this.rest.getRecinto(idTemp).subscribe((data: Recintos) => {
      this.recinto = data;
    })
  }

}
