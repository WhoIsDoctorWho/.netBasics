import { Component, Input } from '@angular/core';
import { Beer } from '../models/beer';

@Component({
    selector: "beer-form",
    templateUrl: './beer-form.component.html'
})
export class BeerFormComponent {   

    @Input() beer: Beer;    
    @Input() file: FormData = new FormData();

    onFileSelected(event) {                
        const f : File = <File>event.target.files[0];
        this.file.append('image', f, f.name);        
    } 
}