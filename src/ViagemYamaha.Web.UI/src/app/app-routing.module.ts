import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent  } from './app.component';
import { DefaultComponent  } from './components/default/default.component'
import { RotasComponent  } from './components/rotas/rotas.component'
import { AdicionarrotaComponent  } from './components/adicionarrota/adicionarrota.component'

const routes: Routes = [
  {
    path: '',
    component: DefaultComponent,
    children: [
      { path: 'consultarrota', component: RotasComponent },
      { path: 'adicionarrota', component: AdicionarrotaComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
