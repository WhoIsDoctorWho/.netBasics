import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Beer } from '../models/beer'

@Injectable()
export class DataService {

    private url = "/api/products"; 

    constructor(private http: HttpClient) {
    }
     
    getProducts() {
        return this.http.get(this.url);
    }

    getProduct(id: number) { 
        return this.http.get(this.url + '/' + id);
    }
    createProduct(beer: Beer) {       
        return this.http.post(this.url, beer);
    }
    uploadPhoto(fd: FormData) {
        return this.http.post("/api/upload", fd);
    }
    updateProduct(beer: Beer) { 

        return this.http.put(this.url, beer);
    }
    deleteProduct(id: number) {
        return this.http.delete(this.url + '/' + id);
    }
}