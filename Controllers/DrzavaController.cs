using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Travel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DrzavaController : ControllerBase
    {
    
        public TravelContext Context{get;set;}

        

        public DrzavaController(TravelContext context)
        {
            Context = context;
        }

        [Route("Preuzmi")]
        [HttpGet]
        public ActionResult Preuzmi()
        {
            return Ok(Context.Drzave);
        }
        
        [Route("Dodaj")]
        [HttpPost]
        public async Task<ActionResult> DodajDrzavu([FromBody]Drzava drzava)
        {
            if(string.IsNullOrWhiteSpace(drzava.Naziv) || drzava.Naziv.Length > 40)
                return BadRequest("Pogresno ime");
            try{
                // if(drzava.PodrzaneVakcine == null)
                //     drzava.PodrzaneVakcine = new List<Vakcina>(); NZM STO NE RADI OVAKO
                Context.Drzave.Add(drzava);

                await Context.SaveChangesAsync();

                return Ok($"Drzava uspesno dodata! ID drzave: {drzava.ID}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }    
        }

        [Route("Izbrisi/{naziv}")]
        [HttpDelete]
        public async Task<ActionResult> izbrisiDrzavu(string naziv)
        {
            if(string.IsNullOrWhiteSpace(naziv) || naziv.Length > 40)
            {
                return BadRequest("Nevalidno ime!");
            }
            try{

                var drzava = Context.Drzave.Where(p => p.Naziv == naziv).FirstOrDefault();
                if(drzava == null)
                    return BadRequest("Drzava sa prosledjenim imenom ne postoji u bazi! ");
                Context.Drzave.Remove(drzava);
                await Context.SaveChangesAsync();
                return Ok("Drzava uspesno izbrisana");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // [Route("PromeniNazivDrzave/{stariNaziv}/{noviNaziv}")]
        // [HttpPut]
        // public async Task<ActionResult> PromeniNazivDrzave(string stariNaziv,string noviNaziv)
        // {
        //     if(string.IsNullOrWhiteSpace(stariNaziv) || stariNaziv.Length > 40)
        //         return BadRequest("Pogresno unet parametar \"stariNaziv\"");
        //     if(string.IsNullOrWhiteSpace(noviNaziv) || noviNaziv.Length > 40)
        //         return BadRequest("Pogresno unet parametar \"noviNaziv\"");
            
            

        //     try{
        //     var drzava = Context.Drzave.Where(p => p.Naziv == stariNaziv).FirstOrDefault();
        //     if(drzava == null)
        //         return BadRequest("Drzava sa takvim nazivom nije pronadjena.");
        //     drzava.Naziv = noviNaziv;

        //     await Context.SaveChangesAsync();
        //     return Ok("Uspesno izmenjen naziv!");

        //     }
        //     catch(Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }

        [Route("DodajVazecuVakcinu/{drzava}/{nazivVakcine}/{doza}")]
        [HttpPut]
        public async Task<ActionResult> DodajVazecuVakcinu(string drzava,string nazivVakcine, int doza)
        {
            //provere
            if(string.IsNullOrWhiteSpace(nazivVakcine) || nazivVakcine.Length > 50)
                return BadRequest("Nevalidan naziv proizvodjaca vakcine");
            if(string.IsNullOrWhiteSpace(drzava) || drzava.Length > 40)
                return BadRequest("Nevalidan naziv drzave");
            if(doza< 0)
                return BadRequest("Nevalidna doza");

            try{
                var drzavaZaPromenu = Context.Drzave.Where(p => p.Naziv == drzava).FirstOrDefault();
                if(drzavaZaPromenu == null)
                    return BadRequest("Takva drzava ne postoji");
                
                var vakcina = Context.Vakcine.Where(p => p.Proizvodjac == nazivVakcine && p.Doza == doza).FirstOrDefault();
                if(vakcina == null)
                    return BadRequest("Takva vakcina ne postoji!");

                if(drzavaZaPromenu.PodrzaneVakcine == null)
                    drzavaZaPromenu.PodrzaneVakcine = new List<Vakcina>();
                     
                    drzavaZaPromenu.PodrzaneVakcine.Add(vakcina);
                
                await Context.SaveChangesAsync();
                return Ok("Dodata vazeca vakcina");                
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }





        [Route("DodajVazeciTest/{nazivDrzave}/{test}/{starostTesta}")]
        [HttpPut]
        public async Task<ActionResult> DodajVazeciTest(string nazivDrzave, string test,int starostTesta)
        {
            //provere
           
            if(!validanTip(test))
                return BadRequest("Nije uneto validno ime testa");
            if(starostTesta<0)
                return BadRequest("Nevalidna vrednost starosti testa");
            if(string.IsNullOrWhiteSpace(nazivDrzave) || nazivDrzave.Length > 40)
                return BadRequest("Nevalidan naziv drzave");

            try{
            var drzavaZaPromenu = Context.Drzave.Where(p=> p.Naziv == nazivDrzave).FirstOrDefault();
            
            if(drzavaZaPromenu == null)
                return BadRequest("Nevalidna drzava");

            TipTesta tipTesta = odrediTip(test);
        
            var noviTest = Context.Testovi.Where(p => p.Tip == tipTesta && p.Starost <= starostTesta).FirstOrDefault();
            if(noviTest == null)
                return BadRequest("Nevalidan test");

            if(drzavaZaPromenu.PodrzaniTestovi == null)
                drzavaZaPromenu.PodrzaniTestovi = new List<Test>();
            
            drzavaZaPromenu.PodrzaniTestovi.Add(noviTest);
            await Context.SaveChangesAsync();
            return Ok("Uspesno dodat vazeci test");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("DodajVazeciPasos/{nazivDrzave}/{pasos}")]
        [HttpPut]
        public async Task<ActionResult> DodajVazeciPasos(string nazivDrzave,string pasos)
        {
            if(string.IsNullOrWhiteSpace(nazivDrzave) || nazivDrzave.Length>40)
                return BadRequest("Nevalidan naziv drzave");
            if(string.IsNullOrWhiteSpace(pasos) || pasos.Length>50)
                return BadRequest("Nevalidan pasos");
            try{
                var trazeniPasos = Context.Pasosi.Where(p => p.Drzavljanstvo == pasos).FirstOrDefault();
                if(trazeniPasos == null)
                    return BadRequest("Pasos ne postoji u bazi podataka");
                var drzava = Context.Drzave.Where(p => p.Naziv == nazivDrzave).FirstOrDefault();
                
                if(drzava == null)
                    return BadRequest("Trazena drzava nije u bazi podataka");
                
                if(drzava.PodrzaniPasosi == null)
                    drzava.PodrzaniPasosi = new List<Pasos>();
                drzava.PodrzaniPasosi.Add(trazeniPasos);
                await Context.SaveChangesAsync();
                return Ok("Dodat novi vazeci pasos");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PreuzmiPodrzano/{pasos}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiPodrzano(string pasos,[FromQuery]string nazivVakcine,[FromQuery]int? doza,[FromQuery]string tipTesta,[FromQuery]int? starostTesta)
        {
            //provere
            if(string.IsNullOrWhiteSpace(pasos) || pasos.Length > 50)
                return BadRequest("Nevalidan pasos");
         
            try{
                //pasos je obavezan da se unese
                var trazeniPasos = Context.Pasosi.Where(p=> p.Drzavljanstvo == pasos).FirstOrDefault();
                if(trazeniPasos == null)
                    return BadRequest("Uneti pasos ne postoji u bazi podataka");
                
                if(nazivVakcine== null && tipTesta == null)
                    return BadRequest("Svaki unos mora da sadrzi ili vakcinu ili tip testa ili oba.");
                TipTesta tip;
                Vakcina vakcina = null;
                Test trazeniTest = null;
                
                if(tipTesta != null){

                if(starostTesta == null)
                        return BadRequest("Mora se uneti starost testa");

                tip = odrediTip(tipTesta);

                 trazeniTest = Context.Testovi.Where(p => p.Tip == tip && p.Starost>=starostTesta).FirstOrDefault();
                if(trazeniTest == null)
                    return BadRequest("Trazeni test ne postoji u bazi podataka");
                }
                if(nazivVakcine != null){
                    if(doza == null)
                        return BadRequest("Mora se uneti doza vakcine");
                    vakcina = Context.Vakcine.Where(p => p.Proizvodjac==nazivVakcine && p.Doza == doza).FirstOrDefault();
                if(vakcina == null)
                    return BadRequest("Takva vakcina ne postoji u bazi");
                }
                
                
                var drzave = Context.Drzave.Where(p => p.PodrzaniPasosi.Contains(trazeniPasos));
                var drzaveVakcina = drzave;
                var drzaveTest = drzave;
                //iz var drzave izdvojiti samo drzave koje sadrze "trazeniTest" ili "vakcina"
                if(tipTesta != null)
                    drzaveTest = drzave.Where(p=> p.PodrzaniTestovi.Contains(trazeniTest));
                if(nazivVakcine != null)
                    drzaveVakcina = drzave.Where(p=> p.PodrzaneVakcine.Contains(vakcina));
                
                
                if(nazivVakcine != null && tipTesta != null)
                    drzave = drzaveVakcina.Union(drzaveTest);
                else if(nazivVakcine == null && tipTesta != null)
                    drzave = drzaveTest;
                else
                    drzave = drzaveVakcina;

                return Ok(drzave);


                
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }


/////////////PRIVATNE FUNKCIJE
        private bool validanTip(string tip)
        {
            string[] testovi = {"pcr","PCR","Pcr","antigenski","ANTIGENSKI","Antigenski","antigenski","ANTITELA","antitela","Antitela"};
            return testovi.Contains(tip);

        }
        private TipTesta odrediTip(string tipTesta)
        {
            TipTesta tip;
            if(tipTesta == "PCR" || tipTesta == "pcr" || tipTesta =="Pcr")
                tip = TipTesta.PCR;
            
            else if(tipTesta == "ANTIGENSKI" || tipTesta == "antigenski" || tipTesta == "Antigenski")
                tip = TipTesta.ANTIGENSKI;
            
            else 
                tip = TipTesta.ANTITELA;
        return tip;

        }

      
    }
}
