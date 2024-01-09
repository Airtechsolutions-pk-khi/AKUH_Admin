import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class FileUploadService {
  // API url
  baseApiUrl = 'api/laboratory/insert';

  constructor(private http: HttpClient) {}

  // Returns an observable
  upload(selectedFile): Observable<any> {
    debugger
    // Create form data
    const formData = new FormData();

    // Store form name as "file" with file data
    formData.append('file', selectedFile, selectedFile.name);

    // Make http post request over api
    // with formData as req
    return this.http.post(this.baseApiUrl, formData);
  }
}