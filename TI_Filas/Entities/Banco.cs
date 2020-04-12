using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace TI_Filas.Entities
{
    static class Banco
    {
        private static List<Cliente> fila1 = new List<Cliente>();
        private static List<Cliente> fila2 = new List<Cliente>();
        private static List<Cliente> fila3 = new List<Cliente>();
        private static List<Cliente> fila4 = new List<Cliente>();
        private static List<Cliente> fila5 = new List<Cliente>();
        private static int turno = 0;

        /*
            Função principal do algoritimo, ele coordena os atendimentos enquanto o banco estiver com cliente
        */
        public static void inicializaBanco(List<Cliente> pessoasCarregadas)
        {
            Boolean temGente = false; //variavel pra saber se tem gente no banco ainda.
            do{
                Cliente aux =  new Cliente(); //variavel Cliente auxiliar para pegar sempre o "primeiro" da fila carregada pelo arquivo.
                aux = pessoasCarregadas.Count != 0 ? pessoasCarregadas[0] : null; //validação caso o cliente seja null (acontece quando é adicionado todos)
                Boolean foiAdicionado = false; //variavel de controle                        os clientes foram adicionados o "DO" tenta pegar alguem da fila
                temGente = validarPessoasEmAtendimento(fila1,fila2,fila3,fila4,fila5); //Método para saber se existe alguma pessoa na fila
               
                if(aux != null && aux.intervalo_de_leitura_a_seguir == 0) //Se a variavel intervalo_de_leitura_a_seguir == 0 o cliente vai pra fila na hora
                {
                    if(adicionaClienteEmUmaFila(aux))
                    {
                        foiAdicionado = true; //sinaliza que foi adicionado
                        pessoasCarregadas.RemoveAt(0); //remove o primeiro da fila ou "o cliente"
                    }
                    turno+=1;
                }
                else if(aux != null && aux.intervalo_de_leitura_a_seguir != 0) //Se a variavel intervalo_de_leitura_a_seguir != 0 o cliente espera 
                {
                    Thread.Sleep(aux.tempo_de_atendimento_previsto*100); //tempo de espera tempo_de_atendimento_previsto * 1000 = segundos esperando ir pra fila
                    if(adicionaClienteEmUmaFila(aux))
                    {
                        foiAdicionado = true; //sinaliza que foi adicionado
                        pessoasCarregadas.RemoveAt(0); //remove o primeiro da fila ou "o cliente"
                    }
                   turno+=1;
                }
                
                if(pessoasCarregadas.Count != 0 && !foiAdicionado && turno > 0) //se tem pessoas a adicionar, banco cheio e turno >0
                {   
                    atendimento(fila1,1); //atendimento (fila + um inteiro só pra sinalizar na mensagem)
                    atendimento(fila2,2);
                    atendimento(fila3,3);
                    atendimento(fila4,4);
                    atendimento(fila5,5);
                    temGente = validarPessoasEmAtendimento(fila1,fila2,fila3,fila4,fila5); //validação se existe alguem ainda na fila
                    Console.WriteLine("\n");                               
                } 
                else if (pessoasCarregadas.Count == 0 && validarPessoasEmAtendimento(fila1,fila2,fila3,fila4,fila5))
                    {
                        atendimento(fila1,1);
                        atendimento(fila2,2);
                        atendimento(fila3,3);
                        atendimento(fila4,4);
                        atendimento(fila5,5);
                        temGente = validarPessoasEmAtendimento(fila1,fila2,fila3,fila4,fila5);
                        Console.WriteLine("\n"); 
                    } 
            }//Enquanto estiver pessoas na lista principal (não na fila, "tiver gente" ou pessoas em atendimento o programa fica realizando essas chamadas acima)
            while(pessoasCarregadas.Count != 0 && temGente || validarPessoasEmAtendimento(fila1,fila2,fila3,fila4,fila5)); 
            
        }

        /*
            Metodo para validar se existe pessoas em atendimento, se alguma fila estiver com uma pessoa ele retorna true,
            caso não tenha retorna false
        */
        public static Boolean validarPessoasEmAtendimento(List<Cliente> l1, List<Cliente> l2,List<Cliente> l3,List<Cliente> l4,List<Cliente> l5)
        {
            Boolean isValid = false;
            if(l1.Count != 0 || l2.Count !=0 || l3.Count !=0 || l4.Count != 0|| l5.Count !=0) isValid = true;
            return isValid;
        }

        /*
            Método para adicionar uma pessoa em alguma fila
            caso a fila contenha 6 pessoas, é passado 3 pessoas
            desta fila com 6 para uma proxima caso o tamanho dela seja 
            menor ou igual a 3
        */
        public static Boolean adicionaClienteEmUmaFila(Cliente pessoa)
        {   
            Boolean foiAdicionado = false; //variavel de controle, caso a pessoa foi adicionada ela muda para true
            if(fila1.Count == 0 || fila1.Count < 6) //Validação da fila 1, adiciona com fila vazia ou < 6
            {
                fila1.Add(pessoa);
                Console.WriteLine("[FILA]: Caixa: 1 - " + pessoa.nome);
                foiAdicionado = true;
            }
            else if(fila1.Count == 6 && fila2.Count <=3) //método que controla a fila, se a fila tem 6 pessoas é passada para proxima as pos 2 4 6
            {                
                fila2.Add(fila1[1]);
                fila2.Add(fila1[3]);
                fila2.Add(fila1[5]);
                removeDaFila(fila1); 
                Console.WriteLine("\n[INFO]: Caixa: 1 - Transferindo " + fila2[0].nome + "/" + fila2[1].nome + "/" + fila2[2].nome + " para Caixa 2." );
                fila1.Add(pessoa);
                Console.WriteLine("[FILA]: Caixa: 1 - " + pessoa.nome);
            }
            else if(fila2.Count == 0 && (fila2.Count < 6 && !foiAdicionado)) //Validação da fila 1, adiciona com fila vazia ou < 6
            {
                fila2.Add(pessoa);
                Console.WriteLine("[FILA]: Caixa: 2 - " + pessoa.nome);
                foiAdicionado = true;
            }
            else if(fila2.Count == 6 && fila3.Count <=3) //método que controla a fila, se a fila tem 6 pessoas é passada para proxima as pos 2 4 6
            {                
                fila3.Add(fila2[1]);
                fila3.Add(fila2[3]);
                fila3.Add(fila2[5]);
                removeDaFila(fila2);
                Console.WriteLine("\n[INFO]: Caixa: 2 - Transferindo " + fila3[0].nome + "/" + fila3[1].nome + "/" + fila3[2].nome + " para Caixa 3." );
                fila2.Add(pessoa);
                Console.WriteLine("[FILA]: Caixa: 2 - " + pessoa.nome);
            }
            else if(fila3.Count == 0 && (fila3.Count < 6 && !foiAdicionado)) //Validação da fila 1, adiciona com fila vazia ou < 6
            {
                fila3.Add(pessoa);
                Console.WriteLine("[FILA]: Caixa: 3 - " + pessoa.nome);
                foiAdicionado = true;
            }
            else if(fila3.Count == 6 && fila4.Count <=3) //método que controla a fila, se a fila tem 6 pessoas é passada para proxima as pos 2 4 6
            {                
                fila4.Add(fila3[1]);
                fila4.Add(fila3[3]);
                fila4.Add(fila3[5]);
                removeDaFila(fila3);
                Console.WriteLine("\n[INFO]: Caixa: 3 - Transferindo " + fila4[0].nome + "/" + fila4[1].nome + "/" + fila4[2].nome + " para Caixa 4." );
                fila3.Add(pessoa);
                Console.WriteLine("[FILA]: Caixa: 3 - " + pessoa.nome);
            }
            else if(fila4.Count == 0 && (fila4.Count < 6 && !foiAdicionado)) //Validação da fila 1, adiciona com fila vazia ou < 6
            {
                fila4.Add(pessoa);
                Console.WriteLine("[FILA]: Caixa: 4 - " + pessoa.nome);
                foiAdicionado = true;
            }
            else if(fila4.Count == 6 && fila5.Count <=3) //método que controla a fila, se a fila tem 6 pessoas é passada para proxima as pos 2 4 6
            {                
                fila5.Add(fila4[1]);
                fila5.Add(fila4[3]);
                fila5.Add(fila4[5]);
                removeDaFila(fila4);
                Console.WriteLine("\n[INFO]: Caixa: 4 - Transferindo " + fila5[0].nome + "/" + fila5[1].nome + "/" + fila5[2].nome + " para Caixa 5." );
                fila4.Add(pessoa);
                Console.WriteLine("[FILA]: Caixa: 4 - " + pessoa.nome);
            }
            else if(fila5.Count == 0 && (fila5.Count < 6 && !foiAdicionado)) //Validação da fila 1, adiciona com fila vazia ou < 6
            {
                fila4.Add(pessoa);
                Console.WriteLine("[FILA]: Caixa: 5 - " + pessoa.nome);
                foiAdicionado = true;
            }
            else 
            {   //Se todas as filas tiverem 6 pessoas o banco fica cheio e retorna essa mensagem
                Console.WriteLine("\n====================================================================");
                Console.WriteLine("[INFO]: Todos os caixas estão lotados, aguarde na fila o atendimento.");
                Console.WriteLine("======================================================================\n");
            }
            return foiAdicionado;
        }

        /*
            Metodo para realizar o atendimento do caixa
            parametro uma fila, e o numero do caixa 
            se o cliente for atendido o metodo retorna um 
            Boolean caso o caixa esteja vazio ele retorna um 
            false, feito pra controlar a chamada no do-while
            em outro metodo.

            Utiliza da Thread para criar um temporizador usando tempo_de_atendimento_previsto * 1000 = 1 segundo
        */
        public static Boolean atendimento(List<Cliente> fila, int caixa)
        {
            Boolean atendido = false;
            if(fila.Count != 0){ //esse if valida se a fila está vazia... se esiver é printado no console caixa vazio.
                Thread.Sleep(fila[0].tempo_de_atendimento_previsto*10); //temporizador simulando tempo de atendimento
                Console.WriteLine("[ATENDIMENTO]: Caixa: " + caixa + " O Cliente: " + fila[0].nome + " atendido.");
                fila.RemoveAt(0); //função pra remover da fila.
                atendido = true; // variavel de controle pra saber se foi atendido ou não
            }else {
                Console.WriteLine("[ATENDIMENTO]: Caixa: " + caixa + " - Caixa vazio.");
            }
            return atendido; // retorno da variavel de controle.
        }
       
        /*
            Metodo para remover os numeros "Pares da fila"
            fila cheia = 0,1,2,3,4,5 (6 pessoas)
            o que este metodo faz é
            o 1 = segunda pessoa
            o 2 = quarta pessoa
            o 3 = sexta pessoa
        */
        private static void removeDaFila(List<Cliente> fila)
        {
            fila.RemoveAt(1);
            fila.RemoveAt(2);
            fila.RemoveAt(3);
        }

    }
}
