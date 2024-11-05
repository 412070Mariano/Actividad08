using Actividad8.Negocio.Data;
using Actividad8.Negocio.Data.Repositories;

namespace Actividad8.Negocio.Services
{
    public class ServiceTurno : IServiceTurno
    {

        private readonly IRepositoryTurno _repository;
        public ServiceTurno(IRepositoryTurno repository)
        {
            _repository = repository;
        }

        public void ActualizarTurno(TTurno turno, int id)
        {
             _repository.ActualizarTurno(turno, id);
        }

        public void AgregarTurno(TTurno turno)
        {
            _repository.AgregarTurno(turno);
        }

        public void EliminarTurno(int id)
        {
            _repository.EliminarTurno(id);
        }

        public List<TTurno> GetTurnos()
        {
            return _repository.GetTurnos();
        }

        public TTurno? TraerTurnosConId(int id)
        {
            return _repository.TraerTurnosConId(id);
        }
    }
}
