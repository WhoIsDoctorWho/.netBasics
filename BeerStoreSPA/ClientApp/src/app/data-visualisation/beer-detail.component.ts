import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from '../server-communication/data.service';
import { Beer } from '../models/beer';

@Component({
    templateUrl: './beer-detail.component.html',
    styleUrls: ['./beer-detail.component.css'],
    providers: [DataService]
})
export class BeerDetailComponent implements OnInit {     
    id: number;
    beer: Beer; 
    loaded: boolean = false;   

    constructor(private dataService: DataService, private router: Router, activeRoute: ActivatedRoute) {
        this.id = Number.parseInt(activeRoute.snapshot.params["id"]);
    }

    ngOnInit() {
        if (this.id)
            this.dataService.getProduct(this.id)
                .subscribe((data: Beer) => { this.beer= data; this.loaded = true; });
    } 
    delete() { 
        // to reload data from server, src: https://stackoverflow.com/a/49509706        
        this.dataService.deleteProduct(this.beer.id)
            .subscribe(data => this.router.navigateByUrl('/?reload=1', { skipLocationChange: true })
            .then(() =>this.router.navigate(['/']))); 
    }

}