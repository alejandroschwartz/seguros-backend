using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SegurosApi.Context;
using SegurosApi.Models;

namespace SegurosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutosController : ControllerBase
    {
        private readonly AutosContext _context;

        public AutosController(AutosContext context)
        {
            _context = context;
        }

        // GET: api/Autos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autos>>> GetAutos()
        {
            return await _context.Autos.ToListAsync();
        }

        // GET: api/Autos/5
        [HttpGet("{id}")]
        public ActionResult GetAutos(long id)
        {
            var auto =  _context.Autos.FindAsync(id);

            return Ok(auto);
        }

        // GET: api/Autos/5
        [HttpGet("{id}/{year}")]
        public ActionResult GetAutos(long id, int year)
        {
            var auto = _context.Autos.FindAsync(id);

            static string cuota(int valorAuto, int edad)
            {
                int antiguedad = Int32.Parse( DateTime.Now.ToString("yyy") ) - edad;
                double depreciacion = 1 - antiguedad * 0.05;

                string calculoCuota = ((valorAuto / 120)*depreciacion).ToString();
                string respuesta = $"El valor de la cuota es de: {calculoCuota}";
                return respuesta;
            }

            return Ok(cuota(auto.Result.Precio, year));
        }

        // PUT: api/Autos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutos(long id, Autos autos)
        {
            if (id != autos.Id)
            {
                return BadRequest();
            }

            _context.Entry(autos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Autos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Autos>> PostAutos(Autos autos)
        {
            _context.Autos.Add(autos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAutos", new { id = autos.Id }, autos);
        }

        // DELETE: api/Autos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutos(long id)
        {
            var autos = await _context.Autos.FindAsync(id);
            if (autos == null)
            {
                return NotFound();
            }

            _context.Autos.Remove(autos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AutosExists(long id)
        {
            return _context.Autos.Any(e => e.Id == id);
        }




        //// GET: api/Autos/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Autos>> GetAutos(long id)
        //{
        //    var autos = await _context.Autos.FindAsync(id);

        //    if (autos == null)
        //    {
        //        return NotFound();
        //    }

        //    return autos;
        //}


        //// GET: api/Autos/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Autos>> GetAutos(long id)
        //{
        //    var autos = await _context.Autos.FindAsync(id);

        //    // metodo de prueba
        //    static int cuota(int valorAuto)
        //    {
        //        return valorAuto / 60;
        //    }

        //    //var cuota = autos.Precio / 60;
        //    var valor = cuota(autos.Precio);

        //    if (autos == null)
        //    {
        //        return NotFound();
        //    }

        //    return valor;
        //}
    }
}
