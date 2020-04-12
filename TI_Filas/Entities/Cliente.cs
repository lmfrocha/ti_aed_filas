using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TI_Filas.Entities
{
    class Cliente : IDados
    {
        public string cpf { get; set; }
        public string nome { get; set; }
        public int tempo_de_atendimento_previsto { get; set; }
        public int intervalo_de_leitura_a_seguir { get; set; }
        
        public Cliente(){}
        public Cliente(string cpf, string nome, int tempo_de_atendimento_previsto,
            int intervalo_de_leitura_a_seguir)
        {
            this.cpf = cpf;
            this.nome = nome;
            this.tempo_de_atendimento_previsto = tempo_de_atendimento_previsto;
            this.intervalo_de_leitura_a_seguir = intervalo_de_leitura_a_seguir;
        }

       /*
        Metodo para carregar a lista de pessoas do arquivo 
        E preencher a lista passada por parametro que veio do metodo Main 
       */
       public static void Data(string CAMINHO_DO_ARQUIVO, List<Cliente> lista)
        {
            try
            {   
                using (StreamReader sr = File.OpenText(CAMINHO_DO_ARQUIVO))
                {   
                    string[] lines = File.ReadAllLines(CAMINHO_DO_ARQUIVO);
                    foreach (string line in lines)
                    {
                        string[] dadas = line.Split(';');
                        string cpf = dadas[0];
                        string nome = dadas[1];
                        int tempo_de_atendimento_previsto = int.Parse(dadas[2]);
                        int intervalo_de_leitura_a_seguir = int.Parse(dadas[3]);

                        //Cria o cliente com cada linha do arquivo
                        Cliente client = new Cliente(cpf, nome, 
                            tempo_de_atendimento_previsto, 
                                intervalo_de_leitura_a_seguir);
                        
                        lista.Add(client);
                    }
                    sr.Close();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(e.Message);
            }
        }

        public int getTime()
        {
            return tempo_de_atendimento_previsto;
        }

        public string getInfoCliente()
        {
            return this.cpf + " / " + this.nome + " / " + this.tempo_de_atendimento_previsto + " / " + this.intervalo_de_leitura_a_seguir; 
        }
    }
}
