import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DataService } from '../server-communication/data.service';
import { Beer } from '../models/beer';

@Component({
    templateUrl: './beer-edit.component.html'
})
export class BeerEditComponent implements OnInit {

    id: number;
    beer: Beer; 
    loaded: boolean = false;
    file: FormData = new FormData();

    constructor(private dataService: DataService, private router: Router, activeRoute: ActivatedRoute) {
        this.id = Number.parseInt(activeRoute.snapshot.params["id"]);
    }

    ngOnInit() {
        if (this.id)
            this.dataService.getProduct(this.id)
                .subscribe((data: Beer) => {
                    this.beer = data;
                    if (this.beer!= null) this.loaded = true;
                });
    }

    save() {
        if (this.file.has("image"))
            this.dataService.uploadPhoto(this.file)
                .subscribe(data => {
                    this.beer.imageUrl = data.img;
                    return this.dataService.updateProduct(this.beer)
                        .subscribe(data => this.router.navigateByUrl("/"));
                });
        this.dataService.updateProduct(this.beer)
            .subscribe(data => this.router.navigateByUrl("/"));        
    }
}