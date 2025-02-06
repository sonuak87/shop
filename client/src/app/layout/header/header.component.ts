import { BusyService } from './../../core/services/busy.service';
import { Component, inject } from '@angular/core';
import { MatBadge } from '@angular/material/badge'
import { MatButton } from '@angular/material/button'
import { MatIcon } from '@angular/material/icon'
import { MatProgressBar } from '@angular/material/progress-bar';
import { RouterLink, RouterModule } from '@angular/router';
@Component({
  selector: 'app-header',
  imports: [
    MatBadge,MatButton,MatIcon,RouterLink,RouterModule,MatProgressBar
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  busyService = inject(BusyService);
}
