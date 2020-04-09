import { Component, OnInit } from '@angular/core';
import { DataService } from '../server-communication/data.service';
import { Beer } from '../models/beer';

@Component({
    templateUrl: './beer-list.component.html',
    styleUrls: ['./beer-list.component.css'],
    providers: [DataService]
})
export class BeerListComponent implements OnInit {

    products: Beer[];
    constructor(private dataService: DataService) { }
     
    ngOnInit() {
        this.load();  
    }
    load() {
        this.dataService.getProducts().subscribe((data: Beer[]) => this.products = data);
    }     
}