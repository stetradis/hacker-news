import { Component, OnInit } from '@angular/core';
import { Story } from '../../models/Story';
import { StoryService } from '../../services/story.service';

@Component({
  selector: 'app-top-story',
  templateUrl: './top-story.component.html',
  styleUrls: ['./top-story.component.css']
})
export class TopStoryComponent implements OnInit {

  public stories: Story[];
  private textUrl = "https://news.ycombinator.com/item?id=";

  constructor(private storyService: StoryService) { }

  ngOnInit() {
    this.storyService.getTopStories().subscribe(result => {
      this.stories = result;
    }, error => console.error(error));
  }

}
