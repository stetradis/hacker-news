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
  private page: number;
  private pageSize: number;
  private textUrl: string;
  filter: string = "";

  constructor(private storyService: StoryService) {
    this.page = storyService.page;
    this.pageSize = storyService.pageSize;
    this.textUrl = storyService.textUrl;
  }

  ngOnInit() {
    this.storyService.getTopStories().subscribe(result => {
      this.stories = result;
    }, error => console.error(error));
  }

  handlePageChange(event) {
    this.page = event;
  }

}
