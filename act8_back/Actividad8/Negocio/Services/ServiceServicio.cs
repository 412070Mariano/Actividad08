using Actividad8.Negocio.Data;
using Actividad8.Negocio.Data.Repositories;

namespace Actividad8.Negocio.Services
{
    public class ServiceServicio : IServiceServicio
    {
        private readonly IRepositoryServicio _repository;
        public ServiceServicio(IRepositoryServicio repository)
        {
            _repository = repository;
        }
        public void ActualizarServicio(TServicio servicio, int id)
        {
            _repository.ActualizarServicio(servicio, id);
        }

        public void AgregarServicio(TServicio servicio)
        {
            _repository.AgregarServicio(servicio);
        }

        public void EliminarServicio(int id)
        {
            _repository.EliminarServicio(id);
        }

        public TServicio? TraerServicio(int id)
        {
            return _repository.TraerServicio(id);
        }

        public List<TServicio>? TraerServicioFiltrado(string? nombre, string? enPromo)
        {
            return _repository.TraerServicioFiltrado(nombre, enPromo);
        }

        public List<TServicio> TraerTodosServicios()
        {
            return _repository.GetServicios();
        }
    }
}
