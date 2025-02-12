using Microsoft.EntityFrameworkCore;
using MvcCoreEF.Data;
using MvcCoreEF.Models;

namespace MvcCoreEF.Repositories
{
    public class RepositoryHospital
    {
        private HospitalContext context;

        public RepositoryHospital(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<Hospital>> GetHospitalesAsync()
        {
            var consulta = from datos in this.context.Hospitales select datos;
            return await consulta.ToListAsync();
        }

        public async Task<Hospital> FindHospitalAsync(int idHospital)
        {
            var consulta = from datos in this.context.Hospitales where datos.IdHospital == idHospital select datos;
            //SI NO ENCUENTRA ALGO, DEBE DEVOLVER UN NULL
            return await consulta.FirstOrDefaultAsync();
        }

        public async Task InsertHospitalAsync(int idHospital, string nombre, string direccion, string telefono, int camas)
        {
            //CREAMOS UN MODEL
            Hospital h = new Hospital();
            //ASIGNAMOS SUS PROPIEDADES
            h.IdHospital = idHospital;
            h.Nombre = nombre;
            h.Direccion = direccion;
            h.Telefono = telefono;
            h.Camas = camas;
            //AÑADIMOS NUESTRO MODEL A LA COLECCION DBSET DEL CONTEXT
            await this.context.Hospitales.AddAsync(h);
            //INDICAMOS QUE ALMAENE LOS DATOS EN LA BBDD
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteHospitalAsync(int idHospital)
        {
            //BUSCAMOS EL MODEL PARA ELIMINARLO
            Hospital h = await this.FindHospitalAsync(idHospital);
            //ELIMINAMOS DE LA COLECCION DBSET<T> DEL CONTEXT
            this.context.Hospitales.Remove(h);
            //ACTUALIZAMOS LA BASE DE DATOS
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateHospitalAsync(int idHospital, string nombre, string direccion, string telefono, int camas)
        {
            //BUSCAMOS EL OBJETO HOSPITAL A MODIFICAR
            Hospital h = await this.FindHospitalAsync(idHospital);
            //PODEMOS MODIFICAR TODO LO QUE DESEEMOS EXCEPTO EL CAMPO [key]
            h.Nombre = nombre;
            h.Direccion = direccion;
            h.Telefono = telefono;
            h.Camas = camas;
            //NO TENEMOS NINGUN METODO PARA REALIZAR UN UPDATE DENTRO DEL CONTEXT Y DBSET<T>
            await this.context.SaveChangesAsync();
        }
    }
}
