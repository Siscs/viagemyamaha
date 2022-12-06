import { Component } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { RotaService } from '../../services/rota.service';

@Component({
  selector: 'app-rotas',
  templateUrl: './rotas.component.html',
  styleUrls: ['./rotas.component.css']
})
export class RotasComponent {

  origem: string = '';
  destino: string = '';
  melhorRota: string = '';
  mensagemErro: string = '';

  constructor(private rotaService: RotaService) {}

  pesquisarRota() {
    
    this.mensagemErro = '';

    if(this.origem == '')
    {
      this.mensagemErro = 'Informe a origem.'
      return;
    }

    if(this.destino == '')
    {
      this.mensagemErro = 'Informe o destino.'
      return;
    }

    this.rotaService.obterMelhorRota(this.origem, this.destino)
      .subscribe({
        next: (response) => {
          console.log('retorno', response);
          this.melhorRota = response.data;
        },
        error: (responseError: HttpErrorResponse) => {
          this.handleError(responseError);
        }
      });

    
  }

  // simples handle de erro, mas pode ter um serviço para tratamento de erros
  handleError(error: HttpErrorResponse) {
    console.log('error', error);

    if(error.status == 404)
    {
      this.mensagemErro =  'Rota não encontrada';
    }
    else{
      this.mensagemErro =  `Ocorreu um erro: ${error.status} - ${error.message}`;
    }
  }

}
