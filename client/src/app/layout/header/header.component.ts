import { Component } from '@angular/core';
import { MatBadge } from '@angular/material/badge'
import { MatButton } from '@angular/material/button'
import { MatIcon } from '@angular/material/icon'
import { RouterLink, RouterModule } from '@angular/router';
@Component({
  selector: 'app-header',
  imports: [
    MatBadge,MatButton,MatIcon,RouterLink,RouterModule
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {

}
