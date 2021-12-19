using DIO.SERIES.Repositorios;
using System;

namespace DIO.SERIES
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();

        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                try
                {
                    switch (opcaoUsuario)
                    {
                        case "1":
                            ListarSeries();
                            break;
                        case "2":
                            InserirSerie();
                            break;
                        case "3":
                            AtualizarSerie();
                            break;
                        case "4":
                            ExcluirSerie();
                            break;
                        case "5":
                            VisualizarSerie();
                            break;
                        case "C":
                            Console.Clear();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("==> Erro: {0}", ex.Message);
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Obrigado por utilizar nossos serviços.");
            Console.ReadLine();
        }

        private static void ListarGeneros()
        {
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }
        }

        private static void ListarSeries()
        {
            Console.WriteLine("Listar séries");
            Console.WriteLine();

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.RetornaExcluido();

                Console.WriteLine("#ID {0}: - {1} {2}", serie.RetornaId(), serie.RetornaTitulo(), (excluido ? "- Excluido" : ""));
                Console.WriteLine("---------------------------------------------------------");
            }
        }

        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série");

            ListarGeneros();

            Console.Write("Digite o genero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            var tamanhoListaGenero = Enum.GetValues(typeof(Genero)).Length;

            if (entradaGenero <= tamanhoListaGenero)
            {
                Console.Write("Digite o Título da Série: ");
                string entradaTitulo = Console.ReadLine();

                Console.Write("Digite o Ano de  Início da Série: ");
                int entradaAno = int.Parse(Console.ReadLine());

                Console.Write("Digite a Descrição da Série:");
                string entradaDescricao = Console.ReadLine();

                Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                    genero: (Genero)entradaGenero,
                    titulo: entradaTitulo,
                    ano: entradaAno,
                    descricao: entradaDescricao);

                repositorio.Insere(novaSerie);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("==> Aviso: O valor deve conter na lista de generos");
                return;
            }
        }

        private static void AtualizarSerie()
        {
            Console.WriteLine("Atualizar série");

            Console.Write("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(indiceSerie);

            if (!serie.RetornaExcluido())
            {
                ListarGeneros();

                Console.Write("Digite o genero entre as opções acima: ");
                int entradaGenero = int.Parse(Console.ReadLine());

                var tamanhoListaGenero = Enum.GetValues(typeof(Genero)).Length;

                if (entradaGenero <= tamanhoListaGenero)
                {
                    Console.Write("Digite o Título da Série: ");
                    string entradaTitulo = Console.ReadLine();

                    Console.Write("Digite o Ano de  Início da Série: ");
                    int entradaAno = int.Parse(Console.ReadLine());

                    Console.Write("Digite a Descrição da Série:");
                    string entradaDescricao = Console.ReadLine();

                    Serie atualizaSerie = new Serie(id: indiceSerie,
                        genero: (Genero)entradaGenero,
                        titulo: entradaTitulo,
                        ano: entradaAno,
                        descricao: entradaDescricao);

                    repositorio.Atualizar(indiceSerie, atualizaSerie);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("==> Aviso: O valor deve conter na lista de generos");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Série excluida não pode ser alterada.");
            }
        }

        private static void ExcluirSerie()
        {
            Console.WriteLine("Excluir série");

            Console.Write("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            repositorio.Excluir(indiceSerie);
        }

        private static void VisualizarSerie()
        {
            Console.Write("Digite o id a série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(indiceSerie);

            Console.WriteLine(serie);
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Séries a seu dispor!!!");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1- Listar séries");
            Console.WriteLine("2- Inserir nova série");
            Console.WriteLine("3- Atualizar série");
            Console.WriteLine("4- Excluir série");
            Console.WriteLine("5- Visualizar série");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper().Trim();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}
