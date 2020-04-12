using System;
using System.Collections.Generic;
using System.Text;

namespace TI_Filas.Entities
{
    class Lista
    {
        public Elemento primeiro, ultimo;
        private int quantidade;
        public Lista()
        {
            this.primeiro = new Elemento(null);
            this.ultimo = this.primeiro;
            this.quantidade = 0;
        }

        /*
        Adiciona no começo da lista
        */
        public void add (IDados fresh)
        {
            Elemento frshElement = new Elemento(fresh);
            this.ultimo.proximo = frshElement;
            this.ultimo = frshElement;
            this.quantidade += 1;
        }

        /*
        Remove o primeiro da fila
        */
        public IDados remove()
        {
            if(this.primeiro == this.ultimo)
            {
                return null;
            }
            Elemento aux = this.primeiro.proximo;
            this.primeiro.proximo = aux.proximo;
            if(aux == this.ultimo)
            {
                this.ultimo = this.primeiro;
            }
            this.quantidade -= 1;
            return aux.cliente;
        }
        
        /*
        Realiza a contagem de quantas pessoas 
        estão no total para serem atendidas antes das filas
        */
        public int count(){
            int quantidade = 0;
            Elemento aux = this.primeiro.proximo;
            while(aux != null)
            {
                quantidade += 1;
                aux = aux.proximo;
            }
            return quantidade;
        }

        /*
        Imprime o nome de todas as pessoas da fila
        */
        public string printAll()
        {
            StringBuilder texto = new StringBuilder();
            Elemento aux = this.primeiro.proximo;
            while(aux != null)
            {
                texto.Append(aux.cliente.getInfoCliente() + " \n");
                aux = aux.proximo;
            }
            return texto.ToString();
        }
    }
}
