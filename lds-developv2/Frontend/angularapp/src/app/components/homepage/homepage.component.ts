import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Recintos } from 'src/app/models/Recintos';
import { RecintosRestService } from 'src/app/services/recintos-rest.service';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {

  recintos: any;

  constructor(private rest: RecintosRestService, private route: ActivatedRoute, private router: Router, private http: HttpClient) {
  }

  ngOnInit(): void {
    this.rest.getRecintos().subscribe((data:any) => {
      this.recintos = data;
    })
  }

  view(id: any) {
    this.router.navigate(['/recintos/' + id]);
  }

}
