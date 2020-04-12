using System;
using System.IO;
using System.Collections.Generic;
using System.Timers;

using TI_Filas.Entities;

namespace TI_Filas
{
    class Program
    {
       static string CAMINHO_DO_ARQUIVO = @"C:\var\cliente.txt";  //Caminho do arquivo Usar o @"CAMINHO_DO_ARQUIVO_NO_SEU_PC.txt"
      
        static void Main(string[] args)
        {
            /*
            Carrega os dados para a lista generica
            */
            List<Cliente> pessoasList = new List<Cliente>();

             //Lista de pessoas
            Cliente.Data(CAMINHO_DO_ARQUIVO, pessoasList); //Método para carregar as pessoas           
            Console.WriteLine("=============================[]=============================");
            Console.WriteLine("[INFO]: Inicializando sistema.....");
            Console.WriteLine("[INFO]: Aluno que vai querer copiar esse sistema...");
            Console.WriteLine("=============================[]============================="); 

            //Classe estatica que realiza praticamente tudo no sistema
            Banco.inicializaBanco(pessoasList);
            
            
            Console.WriteLine("=============================[]=============================");
            Console.WriteLine("[INFO]: Finalizando sistema.....");
            Console.WriteLine("[INFO]: Emitindo Relatório.");
            Console.WriteLine("=============================[]============================="); 


            
            /*
            Algoritimo do banco
            • Inicialmente, o banco tem um caixa aberto, 
                com uma fila para atendimento.
            */
            
             /*
            Algoritimo do banco
            • Ao ler um cliente do arquivo, ele deve ser direcionado 
                para alguma das filas de atendimento abertas;
            */

             /*
            Algoritimo do banco
            • Se a fila de um caixa ultrapassar 5 clientes, 
                deve ser aberto um novo caixa com uma nova fila, 
                até o número máximo de 5 caixas abertos. 
            */

             /*
            Algoritimo do banco
            • Quando um novo caixa é aberto, a maior fila deve ser dividida em 
                duas de acordo com a regra abaixo, gerando clientes para o novo caixa.
            */

             /*
            Algoritimo do banco
            • Uma fila deve ser dividida entre posições pares e ímpares. 
                Por exemplo, numa fila com 6 clientes, os clientes nas posições 1, 3 e 5 
                permanecem na fila atual e os clientes 2, 4 e 6 vão para uma nova fila.
            */

             /*
            Algoritimo do banco
            • Para simular a passagem de um turno, o sistema deve esperar um 
                comando do usuário que o estejacontrolando.
            */
            
             /*
            Algoritimo do banco
            • Uma fila deve ser dividida entre posições pares e ímpares. 
                Por exemplo, numa fila com 6 clientes, os clientes
                nas posições 1, 3 e 5 permanecem na fila atual e os clientes 2, 4 e 6 vão para uma nova fila.
            */
            
             /*
            Algoritimo do banco
            Ao final da simulação, mostrar o tempo total de execução e 
                o tempo médio simulado de espera, bem como o tempo do cliente que mais esperou. 
            */

        }
      
    }
}


