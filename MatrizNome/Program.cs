using System;
using System.IO;

namespace MatrizNome
{
    internal class Program
    {
        public static int MenuPrincipal()
        {
            bool flag = true;
            int opcao = 0;
            string escolha;
            do
            {
                Console.Clear();
                Console.WriteLine("Matriz Nomes");
                Console.WriteLine("1 - Inserir os nome da matriz:\n2 - Imprimir os nomes da matriz:\n3 - Imprimir os nomes de uma determinada linha:\n" +
                    "4 - Imprimir os nomes de uma determinada coluna:\n5 - Procurar um nome:\n6 - Ordenar os nomes dentro de cada linha:" +
                    "\n7 - Gravar dados em um arquivo:\n8 - Ler dados gravados no arquivo:\n9 - Sair");
                escolha = Console.ReadLine();

                int.TryParse(escolha, out opcao);
                if ((opcao < 1) || (opcao > 9))
                {
                    Console.WriteLine("Opção inválida");
                    Console.WriteLine("Pressione ENTER para voltar...");
                    Console.ReadKey();
                }
                else
                {
                    flag = false;
                }
            } while (flag);
            return opcao;
        }

        static void Main(string[] args)
        {
            bool flag = true;
            int opcao = 0;
            string[,] matrizNomes = new string[3, 3];

            void InserirNomes()
            {
                for (int linha = 0; linha < matrizNomes.GetLength(0); linha++)
                {
                    for (int coluna = 0; coluna < matrizNomes.GetLength(1); coluna++)
                    {
                        Console.WriteLine($"Por favor insira o nome na matriz posição: [{linha},{coluna}]: ");
                        matrizNomes[linha, coluna] = Console.ReadLine();
                    }
                }
                gravarMatrizEmArquivo();
            }

            void ImprimirNomes()
            {
                for (int linha = 0; linha < matrizNomes.GetLength(0); linha++)
                {
                    for (int coluna = 0; coluna < matrizNomes.GetLength(1); coluna++)
                    {
                        Console.Write($"Linha {linha} - Coluna {coluna}: {matrizNomes[linha, coluna]} ");
                    }
                    Console.ReadLine();
                }
            }

            void ImprimirLinha()
            {
                Console.WriteLine("Qual linha deseja imprimir:\nLinha de 0 a 2");
                int linha = int.Parse(Console.ReadLine());
                for (int i = linha; i < matrizNomes.GetLength(0); i++)
                {
                    for (int coluna = 0; coluna < matrizNomes.GetLength(1); coluna++)
                    {
                        Console.Write($"Linha {linha} - Coluna {coluna}: {matrizNomes[linha, coluna]} ");
                    }
                    Console.ReadLine();
                    break;
                }
            }

            void ImprimirColuna()
            {
                Console.WriteLine("Qual coluna deseja imprimir:\nColunas de 0 a 2");
                int coluna = int.Parse(Console.ReadLine());
                for (int linha = 0; linha < matrizNomes.GetLength(0); linha++)
                {
                    for (int j = coluna; j < matrizNomes.GetLength(1); j++)
                    {
                        Console.Write($"Linha {linha} - Coluna {coluna}: {matrizNomes[linha, coluna]} ");
                        break;
                    }
                    Console.ReadLine();
                }
            }

            void BuscarNome()
            {
                StringComparer order = StringComparer.CurrentCultureIgnoreCase;
                bool nomeNaoEncontrado = true;
                Console.WriteLine("Informe qual nome deseja pesquisar: ");
                string buscaNome = Console.ReadLine();
                for (int linha = 0; linha < matrizNomes.GetLength(0); linha++)
                {
                    for (int coluna = 0; coluna < matrizNomes.GetLength(1); coluna++)
                    {
                        if (order.Compare(buscaNome, matrizNomes[linha, coluna]) == 0)
                        {
                            Console.WriteLine($"O nome {buscaNome} está na matriz na posição [{linha},{coluna}]");
                            Console.ReadLine();
                            nomeNaoEncontrado = false;
                        }
                    }
                }
                if (nomeNaoEncontrado == true)
                {
                    Console.WriteLine($"O nome {buscaNome} não foi encontrado na matriz");
                    Console.ReadLine();
                }
            }

            void ordenarNomes()
            {
                StringComparer order = StringComparer.CurrentCultureIgnoreCase;
                Console.WriteLine("Insira a linha que deseja ordenar:\nLinha de 0 a 2");
                int linha = int.Parse(Console.ReadLine());

                for (int coluna = 0; coluna < matrizNomes.GetLength(1); coluna++)
                {
                    for (int aux = 0; aux < matrizNomes.GetLength(1); aux++)
                    {
                        if (order.Compare(matrizNomes[linha, aux], matrizNomes[linha, coluna]) == 1)
                        {
                            string suporte = matrizNomes[linha, aux];
                            matrizNomes[linha, aux] = matrizNomes[linha, coluna];
                            matrizNomes[linha, coluna] = suporte;
                        }
                    }
                }

                for (linha = linha; linha < matrizNomes.GetLength(0); linha++)
                {
                    for (int coluna = 0; coluna < matrizNomes.GetLength(1); coluna++)
                    {
                        Console.Write($"Linha {linha} - Coluna {coluna}: {matrizNomes[linha, coluna]} ");
                    }
                    Console.ReadLine();
                    break;
                }
            }

            void gravarMatrizEmArquivo()
            {
                try
                {
                    StreamWriter sw = new StreamWriter("C:\\Users\\Gustavo Carl\\OneDrive\\5by5\\Arquivos\\matrizNomes.txt");
                    for (int linha = 0; linha < matrizNomes.GetLength(0); linha++)
                    {
                        for (int coluna = 0; coluna < matrizNomes.GetLength(1); coluna++)
                        {
                            sw.WriteLine(matrizNomes[linha, coluna]);
                        }
                    }
                    sw.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                    Console.ReadLine();
                }
                finally
                {
                    Console.WriteLine("Arquivo gravado com sucesso");
                    Console.ReadLine();
                }
            }

            void lerMatrizDoArquivo()
            {
                string line;
                try
                {
                    StreamReader sr = new StreamReader("C:\\Users\\Gustavo Carl\\OneDrive\\5by5\\Arquivos\\matrizNomes.txt");
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        for (int linha = 0; linha < matrizNomes.GetLength(0); linha++)
                        {
                            for (int coluna = 0; coluna < matrizNomes.GetLength(1); coluna++)
                            {
                                matrizNomes[linha, coluna] = line;
                                line = sr.ReadLine();
                            }
                        }
                    }
                    sr.Close();
                }
                catch (Exception e)
                {
                    Console.Write("Exception: " + e.Message);
                    Console.ReadLine();
                }
                finally
                {
                    Console.WriteLine("Leitura do Arquivo realizada com sucesso");
                    Console.ReadLine();
                }
            }

            opcao = MenuPrincipal();
            do
            {
                switch (opcao)
                {
                    case 1:
                        InserirNomes();
                        opcao = MenuPrincipal();
                        break;
                    case 2:
                        ImprimirNomes();
                        opcao = MenuPrincipal();
                        break;
                    case 3:
                        ImprimirLinha();
                        opcao = MenuPrincipal();
                        break;
                    case 4:
                        ImprimirColuna();
                        opcao = MenuPrincipal();
                        break;
                    case 5:
                        BuscarNome();
                        opcao = MenuPrincipal();
                        break;
                    case 6:
                        ordenarNomes();
                        opcao = MenuPrincipal();
                        break;
                    case 7:
                        gravarMatrizEmArquivo();
                        opcao = MenuPrincipal();
                        break;
                    case 8:
                        lerMatrizDoArquivo();
                        opcao = MenuPrincipal();
                        break;
                    case 9:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida, pressione ENTER para continuar...");
                        Console.ReadKey();
                        break;
                }
            } while (flag);
            Console.Clear();
            Console.WriteLine("Fim do Programa");
        }
    }
}