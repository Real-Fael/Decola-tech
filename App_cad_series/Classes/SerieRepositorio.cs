using System;
using System.Collections.Generic;
using DIO.Series.Interfaces;


namespace DIO.Series
{
	public class SerieRepositorio : IRepositorio<Serie>
	{
        private List<Serie> listaSerie = new List<Serie>();


		public void Atualiza(int id, Serie objeto)
		{
			listaSerie[id] = objeto;
		}

		public void Exclui(int id)
		{
			if (id>= listaSerie.Count || id<0) //tratando erro de acessar posições inválidas no vetor
				throw new ArgumentOutOfRangeException(" item nao existe na lista de séries");
			listaSerie[id].Excluir();
		}

		public void Restaurar(int id){
			if (id>= listaSerie.Count || id<0) //tratando erro de acessar posições inválidas no vetor
				throw new ArgumentOutOfRangeException(" item nao existe na lista de séries");
			listaSerie[id].Restaurar();
		}

		public void Insere(Serie objeto)
		{
			listaSerie.Add(objeto);
		}

		public string listarElementos() //com este método mantemos o listaSerie privada
		{
			
			if (listaSerie.Count == 0)  return "Nenhuma série cadastrada.";
    
			string dadosSerie="";
            foreach (var serie in listaSerie)
            {


                dadosSerie+= $"#ID {serie.retornaId()}: - {serie.retornaTitulo()} {(serie.retornaExcluido() ? "*Excluído*" : " ")} \r\n";
            }
			return dadosSerie;


		}

		public int ProximoId()
		{
			return listaSerie.Count;
		}

		public Serie RetornaPorId(int id)
		{
			if (id>= listaSerie.Count || id<0) //tratando erro de acessar posições inválidas no vetor
				throw new ArgumentOutOfRangeException(" item nao existe na lista de séries");
			return listaSerie[id];
		}
	}
}