namespace Actividad8.Negocio.Data.Repositories
{
    public interface IRepositoryServicio
    {
        List<TServicio> GetServicios();
        TServicio? TraerServicio(int id);
        List<TServicio>? TraerServicioFiltrado(string? nombre, string? enPromo);
        void AgregarServicio(TServicio servicio);
        void ActualizarServicio(TServicio servicio, int id);
        void EliminarServicio(int id);
    }
}
