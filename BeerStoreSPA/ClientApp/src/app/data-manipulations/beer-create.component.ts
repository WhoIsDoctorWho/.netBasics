import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from '../server-communication/data.service';
import { Beer } from '../models/beer'; 

@Component({
    templateUrl: './beer-create.component.html'
})
export class BeerCreateComponent {
    beer: Beer = new Beer(); 
    file: FormData = new FormData();
     
    constructor(private dataService: DataService, private router: Router) { }    
     
    save() { 
        this.dataService.uploadPhoto(this.file)
            .subscribe(data => {                
                this.beer.imageUrl = data.img;                            
                return this.dataService.createProduct(this.beer).subscribe(data => {
                    return this.router.navigateByUrl("/");
            })
    });
           
    } 
}