namespace Actividad8.Negocio.Data.Repositories
{
    public interface IRepositoryTurno
    {
        List<TTurno> GetTurnos();
        TTurno? TraerTurnosConId(int id);
        bool AgregarTurno(TTurno turno);
        bool ActualizarTurno(TTurno turno, int id);
        bool EliminarTurno(int id);
    }
}
