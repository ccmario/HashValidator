import { Component, OnInit, Inject } from '@angular/core';
import 'rxjs/add/operator/map';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Component({
  selector: 'app-hash-component',
  templateUrl: './hash.component.html'
})

export class HashComponent implements OnInit {
  private http: HttpClient;
  private baseUrl: string;
  private reader: FileReader;
  public hashResult;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl + 'api/Hash';
    this.http = http;
    this.reader = new FileReader();
  }

  ngOnInit() { }

  validateFile() {
    const headerOptions = new HttpHeaders({ 'Content-Type': 'application/raw' });
    var body = (<HTMLInputElement>document.getElementById("file-content")).value;
    this.http.post<HashResult>(this.baseUrl, body, {
      headers: headerOptions
    })
      .toPromise()
      .then(res => {
        this.hashResult = res;
      });
  }

  fileUpload(event) {
    this.reader.readAsText(event.srcElement.files[0], 'ISO-8859-1');
    this.reader.onloadend = () => this.loadFile();
  }

  loadFile() {
    (<HTMLInputElement>document.getElementById("file-content")).value = this.reader.result.toString();
  }
}

interface HashResult {
  reportedHash: string;
  calculatedHash: string;
  content: string;
}

