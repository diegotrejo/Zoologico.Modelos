using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Zoologico.Modelos;

namespace Zoologico.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspeciesController : ControllerBase
    {
        private readonly ZoologicoAPIContext _context;

        public EspeciesController(ZoologicoAPIContext context)
        {
            _context = context;
        }

        // GET: api/Especies
        [HttpGet]
        public async Task<ActionResult<ApiResult>> GetEspecie()
        {
            try
            {
                var data = await _context.Especies.ToListAsync();
                return ApiResult.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult.Fail(ex.Message);
            }
            
        }

        // GET: api/Especies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult>> GetEspecie(int id)
        {
            try
            {
                var especie = await _context
                    .Especies
                    .Include(e => e.Animales)
                    .FirstOrDefaultAsync(e => e.Codigo == id);

                if (especie == null)
                {
                    return ApiResult.Fail("Datos no encontrados");
                }

                return ApiResult.Ok(especie);
            }
            catch (Exception ex)
            {
                return ApiResult.Fail(ex.Message);
            }
        }

        // PUT: api/Especies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult>> PutEspecie(int id, Especie especie)
        {
            if (id != especie.Codigo)
            {
                return ApiResult.Fail("No coinciden los identificadores");
            }

            _context.Entry(especie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EspecieExists(id))
                {
                    return ApiResult.Fail("Datos no encontrados");
                }
                else
                {
                    return ApiResult.Fail(ex.Message);
                }
            }

            return ApiResult.Ok(null);
        }

        // POST: api/Especies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult>> PostEspecie(Especie especie)
        {
            try
            {
                _context.Especies.Add(especie);
                await _context.SaveChangesAsync();

                return ApiResult.Ok(especie);
            }
            catch (Exception ex)
            {
                return ApiResult.Fail(ex.Message);
            }
        }

        // DELETE: api/Especies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult>> DeleteEspecie(int id)
        {
            try
            {
                var especie = await _context.Especies.FindAsync(id);
                if (especie == null)
                {
                    return ApiResult.Fail("datos no encontrados");
                }

                _context.Especies.Remove(especie);
                await _context.SaveChangesAsync();

                return ApiResult.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult.Fail(ex.Message);
            }
        }

        private bool EspecieExists(int id)
        {
            return _context.Especies.Any(e => e.Codigo == id);
        }
    }
}
