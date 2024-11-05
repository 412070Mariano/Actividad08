
using Microsoft.EntityFrameworkCore;

namespace Actividad8.Negocio.Data.Repositories
{
    public class RepositoryTurno : IRepositoryTurno
    {
        private Turnos_context _context;
        public RepositoryTurno(Turnos_context context)
        {
            _context = context;
        }

        public bool ActualizarTurno(TTurno turno, int id)
        {
            var turnoUpdate = _context.TTurnos.Find(turno.IdTurno);
            if (turno != null && turnoUpdate != null)
            {
                turnoUpdate.Cliente = turno.Cliente;
                turnoUpdate.Hora = turno.Hora;
                turnoUpdate.Fecha = turno.Fecha;
                _context.TTurnos.Update(turnoUpdate);
                return _context.SaveChanges() == 1;
            }
            else
            {
                return false;
            }
        }

        public bool AgregarTurno(TTurno turno)
        {
            if (turno != null)
            {
                _context.TTurnos.Add(turno);
                return _context.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        public bool EliminarTurno(int id)
        {
            var turnoDel = _context.TTurnos.Find(id);
            if (turnoDel != null)
            {
                foreach (var t in _context.TDetallesTurnos.Where(x => x.IdTurno == id).ToList())
                {
                    _context.TDetallesTurnos.Remove(t);
                }

                _context.TTurnos.Remove(turnoDel);
                return _context.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        public List<TTurno> GetTurnos()
        {
            return _context.TTurnos.ToList();
        }

        public TTurno? TraerTurnosConId(int id)
        {
            return _context.TTurnos.Find(id);
        }
    }
}
