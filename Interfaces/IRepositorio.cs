using System.Collections.Generic;

namespace DIO.Series.Interfaces
{
    public interface IRepositorio<T>
    {
        //este método nao faz sentido pois se a lista é privada nao tem porque retorna-la
        //List<T> Lista();
        string listarElementos();
        T RetornaPorId(int id);        
        void Insere(T entidade);        
        void Exclui(int id);     
        void Restaurar(int id);   
        void Atualiza(int id, T entidade);
        int ProximoId();
    }
}