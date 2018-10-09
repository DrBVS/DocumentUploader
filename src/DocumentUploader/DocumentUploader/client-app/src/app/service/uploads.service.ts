import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FileModel } from '../models/fileModel';

@Injectable({
  providedIn: 'root'
})
export class UploadsService {

  private url = 'https://localhost:44379/api/uploads';
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(this.url);
  }

  getById(id: string) {
    return this.http.get(this.url + '/' + id);
  }

  insert(formData: FormData) {
    return this.http.post(this.url, formData);
  }

  updata(file: FileModel) {
    return this.http.put(this.url, file);
  }

  delete(id: string) {
    return this.http.delete(this.url + '/' + id);
  }
}
