using Actividad8.Negocio.Data;

namespace Actividad8.Negocio.Services
{
    public interface IServiceTurno
    {
        List<TTurno> GetTurnos();
        TTurno? TraerTurnosConId(int id);
        void AgregarTurno(TTurno turno);
        void ActualizarTurno(TTurno turno, int id);
        void EliminarTurno(int id);
    }
}
