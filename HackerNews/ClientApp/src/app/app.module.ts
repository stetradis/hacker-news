import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { StoryService } from './services/story.service';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { NewStoryComponent } from './components/new-story/new-story.component';
import { TopStoryComponent } from './components/top-story/top-story.component';
import { ShortDomainPipe } from './pipes/short-domain.pipe';
import { JobComponent } from './components/job/job.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    NewStoryComponent,
    TopStoryComponent,
    ShortDomainPipe,
    JobComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgxPaginationModule,
    Ng2SearchPipeModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'new-story', component: NewStoryComponent },
      { path: 'top-story', component: TopStoryComponent },
      { path: 'job', component: JobComponent },
    ])
  ],
  providers: [StoryService],
  bootstrap: [AppComponent]
})
export class AppModule { }
