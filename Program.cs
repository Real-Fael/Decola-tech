using System;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
			string opcaoUsuario;
            do //utilizei do while para nao solicitar opção 2 vezes ao usuário.
            {
                opcaoUsuario = ObterOpcaoUsuario();
                rotas(opcaoUsuario); // separei as funções da main deixando-a mais limpa

            } while (opcaoUsuario != "X");

            Console.WriteLine("Obrigado por utilizar nossos serviços.");
            Console.ReadLine();
        }

		private static void rotas(string opc){
			switch (opc)
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
					RestaurarSerie();
					break;
				case "6":
					VisualizarSerie();
				break;
				case "C":
					Console.Clear();
					break;
				default:
					Console.WriteLine("OPS: Parece que você digitou uma opção inválida, tente novamente!!!");
				 	break;
			}
		}

 		private static void RestaurarSerie()
        {
            Console.Write("Digite o id da série: ");
			try{
            int indiceSerie = int.Parse(Console.ReadLine());

            repositorio.Restaurar(indiceSerie);
        
			}catch(ArgumentOutOfRangeException err){// trata a excessão de digitar um numero fora do range.
				Console.WriteLine("Ops, Voce digitou um numero inválido :",err.Source);
				return;
			}catch(Exception err){ //trata outros erros como por exemplo digitar uma string.
				Console.WriteLine("Ops, Voce digitou uma opção inválida :",err.Source);
				return;
			}

			Console.WriteLine("item restaurado com sucesso.");//caso nao ocorra exceptions entao a exclusao ocorreu com sucesso
			
		}
        private static void ExcluirSerie()
        {
            Console.Write("Digite o id da série: (apenas números)");
			try{
            int indiceSerie = int.Parse(Console.ReadLine());

            repositorio.Exclui(indiceSerie);
        
			}catch(ArgumentOutOfRangeException err){// trata a excessão de digitar um numero fora do range.
				Console.WriteLine("Ops, Voce digitou um numero inválido :",err.Source);
				return;
			}catch(Exception err){ //trata outros erros como por exemplo digitar uma string.
				Console.WriteLine("Ops, Voce digitou uma opção inválida :",err.Source);
				return;
			}

			Console.WriteLine("item excluido com sucesso.");//caso nao ocorra exceptions entao a exclusao ocorreu com sucesso
			
		}

        private static void VisualizarSerie()
        {
            Console.Write("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(indiceSerie);

            Console.WriteLine(serie);
        }

		private static Serie coletaDados (int indiceSerie){ //como a trechos repetidos utilizamos o DRY 
			
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
            // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.Write("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.Write("Digite o Título da Série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o Ano de Início da Série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a Descrição da Série: ");
            string entradaDescricao = Console.ReadLine();

            Serie atualizaSerie = new Serie(id: indiceSerie,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);
			return atualizaSerie;
		}
        private static void AtualizarSerie()
        {
			int indiceSerie;
			Serie atualizaSerie;
			try{ //evitar erros de digitação e se houver erro nao vai alterar o objeto
				Console.Write("Digite o id da série: ");
				indiceSerie = int.Parse(Console.ReadLine());

         	    atualizaSerie = coletaDados(indiceSerie);
			}catch(Exception err){
				Console.WriteLine("FAIL: parece que houve um problema por favor tente novamente.",err.Source);
				return;
			}
            repositorio.Atualiza(indiceSerie, atualizaSerie);
			Console.WriteLine("OK: atualizado com Sucesso");
        }
        private static void ListarSeries()
        { // se a lista é privada não faz sentido um método que a retorne 
            Console.WriteLine("Listar séries");

            var lista = repositorio.listarElementos();

			Console.WriteLine(lista);
            
        }

        private static void InserirSerie()
        {
			Serie novaSerie;
            Console.WriteLine("Inserir nova série");
			try{// 
				novaSerie = coletaDados( repositorio.ProximoId());
			}catch(Exception err){
				Console.WriteLine("FAIL: parece que houve um problema por favor tente novamente.",err.Source);
				return;
			}
            repositorio.Insere(novaSerie);
			Console.WriteLine("OK: atualizado com Sucesso");
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
            Console.WriteLine("5- Restaurar série");
			Console.WriteLine("6- Visualizar série");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}
