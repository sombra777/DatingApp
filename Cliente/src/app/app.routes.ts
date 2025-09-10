import { Routes } from '@angular/router';
import { Home } from '../features/home/home';
import { MemberList } from '../features/members/member-list/member-list';
import { MembeDetaild } from '../features/members/membe-detaild/membe-detaild';
import { List } from '../features/list/list';
import { Messages } from '../features/messages/messages';
import { authGuard } from '../core/guards/auth-guard';

export const routes: Routes = [
  { path: '', component: Home },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      { path: 'members', component: MemberList },
      { path: 'members/:id', component: MembeDetaild },
      { path: 'list', component: List },
      { path: 'messages', component: Messages },
    ],
  },

  { path: '**', component: Home },
];
