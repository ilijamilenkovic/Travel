using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Travel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VakcinaController : ControllerBase
    {
    
        public TravelContext Context{get;set;}

        

        public VakcinaController(TravelContext context)
        {
            Context = context;
        }

        [Route("DodajNovuVakcinu")]
        [HttpPost]
        public async Task<ActionResult> DodajNovuVakcinu([FromBody]Vakcina vakcina)
        {
            if(string.IsNullOrWhiteSpace(vakcina.Proizvodjac) || vakcina.Proizvodjac.Length > 50)
                return BadRequest("Nevalidno ime proizvodjaca vakcine!");
            
            
            try{
                Context.Vakcine.Add(vakcina);
                await Context.SaveChangesAsync();
                return Ok($"Vakcina uspesno dodata! ID dodate vakcine je {vakcina.ID}");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [Route("IzbrisiVakcinu/{nazivProizvodjaca}/{doza}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiVakcinu(string nazivProizvodjaca)
        {
            if(string.IsNullOrWhiteSpace(nazivProizvodjaca) || nazivProizvodjaca.Length > 50)
                return BadRequest("Nevalidno ime proizvodjaca vakcine!");
            
            try{
                var vakcina = Context.Vakcine.Where(p => p.Proizvodjac == nazivProizvodjaca).FirstOrDefault();
                if(vakcina == null)
                    return BadRequest("Nije pronadjena prosledjena vakcina!");
                
                Context.Vakcine.Remove(vakcina);
                await Context.SaveChangesAsync();
                return Ok("Uspesno izbrisana vakcina!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // [Route("IzmeniVakcinu")]
        // [HttpPut]
        // public async Task<ActionResult> IzmeniVakcinu([FromBody]Vakcina vakcina)
        // {
        //     if(string.IsNullOrWhiteSpace(vakcina.Proizvodjac) || vakcina.Proizvodjac.Length > 50)
        //         return BadRequest("Nevalidno ime proizvodjaca vakcine!");
        //     if(vakcina.Doza < 1 || vakcina.Doza>4)
        //         return BadRequest("Nevalidna vrednost za primljenu dozu vakcine");
        //     try{
        //         Context.Vakcine.Update(vakcina);
        //         await Context.SaveChangesAsync();
        //         return Ok("Uspesno izmenjena vakcina!");
        //     }
        //     catch(Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }
        //[EnableCors("CORS")]
        [Route("PrikaziVakcine")]
        [HttpGet]
        public async Task<ActionResult> PrikaziVakcine()
        {
            try{
                return Ok( await Context.Vakcine.Select(p => 
                        new{
                            Naziv = p.Proizvodjac,
                            Id = p.ID
                        }
                        ).ToListAsync());
        

        //         return Ok( await Context.Rokovi.Select(p => 
                     // new{
        //              Naziv = p.Naziv,
        //              Id = p.ID
        //              }
        //          ).ToListAsync());
        
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // [Route("DodajPodrzanuDrzavu/{nazivDrzave}/{nazivVakcine}/{doza}")]
        // [HttpPut]
        // public async Task<ActionResult> DodajPodrzanuDrzavu(string nazivDrzave,string nazivVakcine,int doza)
        // {
        //     //provere
        //     try{
        //         var drzava = Context.Drzave.Where(p => p.Naziv == nazivDrzave).FirstOrDefault();
        //         if(drzava == null)
        //             return BadRequest("Nevalidna drzava");
        //         var vakcina = Context.Vakcine.Where(p => p.Proizvodjac == nazivVakcine && p.Doza == doza).FirstOrDefault();
        //         if(vakcina == null)
        //             return BadRequest("Nevalidna vakcina");
        //         if(vakcina.PodrzaneDrzave == null)
        //             vakcina.PodrzaneDrzave = new List<Drzava>();
        //         vakcina.PodrzaneDrzave.Add(drzava);
        //         await Context.SaveChangesAsync();
        //         return Ok("Uspesno dodata podrzana drzava");

        //     }
        //     catch(Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }
    }
}