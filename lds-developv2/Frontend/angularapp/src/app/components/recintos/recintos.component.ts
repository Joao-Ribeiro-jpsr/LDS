import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Recintos } from 'src/app/models/Recintos';
import { RecintosRestService } from 'src/app/services/recintos-rest.service';


@Component({
  selector: 'app-recintos',
  templateUrl: './recintos.component.html',
  styleUrls: ['./recintos.component.css']
})
export class RecintosComponent {

  recintos: any;
  concelho: string = '';
  message: string = '';

  constructor(private rest: RecintosRestService, private route: ActivatedRoute, private router: Router, private http: HttpClient) {
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.concelho = params['concelho'];

      if ( this.concelho !== undefined) {
        this.CheckByCounty();
      } else {
        this.getRecintos();
      }
    });
  }

  CheckByCounty(): void {
    this.rest.getRecintos().subscribe((data: any) => {
      this.recintos = data;

      this.recintos = this.recintos?.filter(((r: { concelho: string; }) => r.concelho === this.concelho));

      if (this.recintos?.length === 0) {
        this.message = 'Não existem recintos nesta zona.';
      } else {
        this.message = '';
      }
    })
  }

  getRecintos(): void {
    this.rest.getRecintos().subscribe((data: any) => {
      this.recintos = data;
    })
    if (this.recintos === null) {
      this.message = 'Não existem recintos disponíveis.';
    } else {
      this.message = '';
    }
  }

  view(id: string) {
    this.router.navigate(['/recintos/' + id]);
  }

}
