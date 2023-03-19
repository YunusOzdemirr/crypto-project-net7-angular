import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContentListComponent } from './content-list/content-list.component';
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
  declarations: [ContentListComponent],
  exports: [ContentListComponent],
  imports: [CommonModule, BrowserModule],
})
export class ContentModule {}
