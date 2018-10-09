import { Component, OnInit, ViewChild } from '@angular/core';
import { UploadsService } from './service/uploads.service';
import { FileModel } from './models/FileModel';
import { FileType } from './models/FileType';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  images: FileModel[];
  documents: FileModel[];
  files: File[];

  displayedColumns: string[] = [/*'id',*/ 'name', 'size'];
  @ViewChild('filesInput') filesInput;

  get isFilesVisible() {
    return this.files && this.files.length > 0;
  }

  canUpload = false;

  hasError = false;

  constructor(private service: UploadsService) {
  }

  ngOnInit() {
    this.loadFiles();
  }

  fetchData(data: FileModel[]) {
    this.images.push(...data.filter(t => t.type === FileType.Image));
    this.images = this.images.slice(0);
    this.documents.push(...data.filter(t => t.type === FileType.Document));
    this.documents = this.documents.slice(0);
  }

  loadFiles() {
    this.images = [];
    this.documents = [];
    this.service.getAll()
      .subscribe((data: FileModel[]) => {
        this.fetchData(data);
      });
  }

  checkCanUpload() {
    this.canUpload = this.files.length > 0;
    this.hasError = false;
    this.files.forEach(t => {
      if (!this.hasError) {
        this.hasError = t.size > 10 * 1024 * 1024;
      }
    });
  }

  onFilesSelected() {
    this.files = Array.from(this.filesInput.nativeElement.files);
    this.checkCanUpload();
  }

  removeFromFiles(element: File) {
    const index = this.files.indexOf(element);
    this.files.splice(index, 1);
    this.checkCanUpload();
  }

  upload() {
    if (this.isFilesVisible) {
      const formData = new FormData();
      for (let i = 0; i < this.files.length; i++) {
        formData.append('uploads', this.files[i], this.files[i].name);
      }

      this.service.insert(formData).subscribe((data: FileModel[]) => {
        this.fetchData(data);
      });

      this.files = [];
      this.canUpload = false;
    }
  }
}
