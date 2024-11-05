namespace Actividad8.Negocio.Data.Repositories
{
    public class RepositoryServicio : IRepositoryServicio
    {
        private Turnos_context _context;
        public RepositoryServicio(Turnos_context context)
        {
            _context = context;
        }

        public void ActualizarServicio(TServicio servicio, int id)
        {
            if (servicio != null)
            {
                var serv = _context.TServicios.Find(id);
                if (serv != null)
                {
                    if (servicio.Costo != 0)
                        serv.Costo = servicio.Costo;
                    if (servicio.Nombre != null)
                        serv.Nombre = servicio.Nombre;
                    if (servicio.EnPromocion != null)
                        serv.EnPromocion = servicio.EnPromocion;
                    _context.TServicios.Update(serv);
                    _context.SaveChanges();
                }

            }
        }

        public void AgregarServicio(TServicio servicio)
        {
            _context.TServicios.Add(servicio);
            _context.SaveChanges();
        }




        public void EliminarServicio(int id)
        {
            var serviciosEliminados = TraerServicio(id);
            if (serviciosEliminados != null)
            {
                _context.TServicios.Update(serviciosEliminados);
                _context.SaveChanges();
            }
        }



        public List<TServicio> GetServicios()
        {
            return _context.TServicios.ToList();
        }

        public TServicio? TraerServicio(int id)
        {
            return _context.TServicios.Find(id);
        }

        public List<TServicio>? TraerServicioFiltrado(string? nombre, string? enPromo)
        {
            if (nombre != null && enPromo != null)
                return _context.TServicios.Where(x => x.Nombre.Contains(nombre) && x.EnPromocion == enPromo).ToList();
            else if (nombre != null)
                return _context.TServicios.Where(x => x.Nombre.Contains(nombre)).ToList();
            else
                return _context.TServicios.Where(x => x.EnPromocion == enPromo).ToList();
        }

       
    }
    
}
