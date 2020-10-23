import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-new-story',
  templateUrl: './new-story.component.html',
  styleUrls: ['./new-story.component.css']
})
export class NewStoryComponent implements OnInit {

  public stories: Story[];
  private textUrl = "https://news.ycombinator.com/item?id=";

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Story[]>(baseUrl + 'api/Story/stories').subscribe(result => {
      this.stories = result;
    }, error => console.error(error));
  }

  ngOnInit() {
    
  }

}
interface Story {
  id: number;
  title: string;
  url: string;
  by: string;
  score: number;
}
