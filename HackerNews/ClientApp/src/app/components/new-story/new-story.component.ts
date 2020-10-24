import { Component, OnInit } from '@angular/core';
import { Story } from '../../models/Story';
import { StoryService } from '../../services/story.service';

@Component({
  selector: 'app-new-story',
  templateUrl: './new-story.component.html',
  styleUrls: ['./new-story.component.css'],
})
export class NewStoryComponent implements OnInit {

  public stories: Story[];
  private textUrl = "https://news.ycombinator.com/item?id=";

  constructor(private storyService:StoryService) { }

  ngOnInit() {
    this.storyService.getNewStories().subscribe(result => {
      this.stories = result;
    }, error => console.error(error));
  }
}
