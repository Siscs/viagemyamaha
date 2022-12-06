import { Component } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { RotaService } from '../../services/rota.service';
import { PostRotaRequest } from '../../models/postRotaRequest.model';

@Component({
  selector: 'app-adicionarrota',
  templateUrl: './adicionarrota.component.html',
  styleUrls: ['./adicionarrota.component.css']
})
export class AdicionarrotaComponent {

  origem: string = '';
  destino: string = '';
  valor: number = 0;
  escala: string = '';
  escalas: string[] = [];
  mensagemErro: string = '';
  mensagemSucesso: string = '';

  constructor(private rotaService: RotaService) {}

  adicionarEscala() {

    if(this.escala == '')
      return;

    let exists = this.escalas.indexOf(this.escala);
    
    if(exists > -1)
      return;

    this.escalas.push(this.escala);
    this.escala = '';

  }

  salvarRota() {
    
    this.limparMensagens();

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

    if(this.valor <= 0)
    {
      this.mensagemErro = 'Informe o valor.'
      return;
    }

    let novaRota: PostRotaRequest = new PostRotaRequest();

    novaRota.origem = this.origem;
    novaRota.destino = this.destino;
    novaRota.valor = this.valor;
    novaRota.escalas= this.escalas;

    this.rotaService.adicionarRota(novaRota)
    .subscribe({
      next: (response) => {
        console.log('retorno', response);
        this.mensagemSucesso = "Salvo com sucesso.";
        this.limpar();
      },
      error: (responseError: HttpErrorResponse) => {
        debugger;
        this.handleError(responseError);
      }
    });
    
  }

  limpar()
  {
    this.origem = '';
    this.destino = '';
    this.valor = 0;
    this.escala = '';
    this.escalas = [];
    this.mensagemErro = '';
  }

  limparMensagens() {
    this.mensagemErro = '';
    this.mensagemSucesso = '';
  }

  // simples handle de erro, mas pode ter um serviço para tratamento de erros
  // ou via interceptor
  handleError(error: HttpErrorResponse) {
    console.log('error', error);

    if(error.status == 400)
    {
      this.mensagemErro =  error.error.Error;
    }
    else{
      this.mensagemErro =  `Ocorreu um erro: ${error.status} - ${error.message}`;
    }
  }

}
