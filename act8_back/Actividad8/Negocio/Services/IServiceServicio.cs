using Actividad8.Negocio.Data;

namespace Actividad8.Negocio.Services
{
    public interface IServiceServicio
    {
        List<TServicio> TraerTodosServicios();
        TServicio? TraerServicio(int id);
        List<TServicio>? TraerServicioFiltrado(string? nombre, string? enPromo);
        void AgregarServicio(TServicio servicio);
        void ActualizarServicio(TServicio servicio, int id);
        void EliminarServicio(int id);
    }
}
