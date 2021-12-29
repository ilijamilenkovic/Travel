using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace Travel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PasosController : ControllerBase
    {
    
        public TravelContext Context{get;set;}

        

        public PasosController(TravelContext context)
        {
            Context = context;
        }

        [Route("DodajPasos/{drzavljanstvo}")]
        [HttpPost]
        public async Task<ActionResult> DodajPasos(string drzavljanstvo)
        {
            if(string.IsNullOrWhiteSpace(drzavljanstvo) || drzavljanstvo.Length > 50)
                return BadRequest("Nevalidna vrednost za drzavljanstvo");
            
            try{
                Pasos p = new Pasos{Drzavljanstvo = drzavljanstvo};
                Context.Pasosi.Add(p);
                await Context.SaveChangesAsync();
                return Ok("Uspesno dodat pasos");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("Preuzmi pasose")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiPasose()
        {
            return Ok(Context.Pasosi);
        }
    }
}