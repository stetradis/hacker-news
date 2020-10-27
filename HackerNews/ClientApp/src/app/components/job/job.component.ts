import { Component, OnInit } from '@angular/core';
import { Story } from '../../models/Story';
import { StoryService } from '../../services/story.service';

@Component({
  selector: 'app-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.css']
})
export class JobComponent implements OnInit {

  public jobs: Story[];
  private page: number;
  private jobPageSize: number;
  private textUrl: string;
  filter: string = "";

  constructor(private storyService: StoryService) {
    this.page = storyService.page;
    this.jobPageSize = storyService.jobPageSize;
    this.textUrl = storyService.textUrl;
  }

  ngOnInit() {
    this.storyService.getJobs().subscribe(result => {
      this.jobs = result;
    }, error => console.error(error));
  }

  handlePageChange(event) {
    this.page = event;
  }
}
