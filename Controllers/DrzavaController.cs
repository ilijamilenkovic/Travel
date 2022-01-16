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

        public TravelContext Context { get; set; }



        public DrzavaController(TravelContext context)
        {
            Context = context;
        }

        [Route("Preuzmi")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi()
        {
            return Ok(await Context.Drzave.Select(p =>
            new
            {
                naziv = p.Naziv,
                id = p.ID
            }).ToListAsync());
        }

        [Route("Dodaj")]
        [HttpPost]
        public async Task<ActionResult> DodajDrzavu([FromBody] Drzava drzava)
        {
            if (string.IsNullOrWhiteSpace(drzava.Naziv) || drzava.Naziv.Length > 40)
                return BadRequest("Pogresno ime");
            try
            {
                // if(drzava.PodrzaneVakcine == null)
                //     drzava.PodrzaneVakcine = new List<Vakcina>(); NZM STO NE RADI OVAKO
                Context.Drzave.Add(drzava);

                await Context.SaveChangesAsync();

                return Ok($"Drzava uspesno dodata! ID drzave: {drzava.ID}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("Izbrisi/{naziv}")]
        [HttpDelete]
        public async Task<ActionResult> izbrisiDrzavu(string naziv)
        {
            if (string.IsNullOrWhiteSpace(naziv) || naziv.Length > 40)
            {
                return BadRequest("Nevalidno ime!");
            }
            try
            {

                var drzava = Context.Drzave.Where(p => p.Naziv == naziv).FirstOrDefault();
                if (drzava == null)
                    return BadRequest("Drzava sa prosledjenim imenom ne postoji u bazi! ");
                Context.Drzave.Remove(drzava);
                await Context.SaveChangesAsync();
                return Ok("Drzava uspesno izbrisana");
            }
            catch (Exception e)
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

        [Route("DodajVazecuVakcinu/{idDrzave}/{idVakcine}/{doza}")]
        [HttpPost]
        public async Task<ActionResult> DodajVazecuVakcinu(int idDrzave, int idVakcine, int doza)
        {
            //provere
            // if(string.IsNullOrWhiteSpace(nazivVakcine) || nazivVakcine.Length > 50)
            //     return BadRequest("Nevalidan naziv proizvodjaca vakcine");
            // if(string.IsNullOrWhiteSpace(drzava) || drzava.Length > 40)
            //     return BadRequest("Nevalidan naziv drzave");
            if (doza < 0)
                return BadRequest("Nevalidna doza");

            try
            {
                var drzavaZaPromenu = Context.Drzave.Where(p => p.ID == idDrzave).FirstOrDefault();
                if (drzavaZaPromenu == null)
                    return BadRequest("Takva drzava ne postoji");

                var vakcina = Context.Vakcine.Where(p => p.ID == idVakcine).FirstOrDefault();
                if (vakcina == null)
                    return BadRequest("Takva vakcina ne postoji!");



                DrzavaVakcina veza = new DrzavaVakcina();
                veza.doza = doza;
                veza.drzava = drzavaZaPromenu;
                veza.vakcina = vakcina;

                if (Context.Spoj.Any(v => v.drzava == veza.drzava && v.vakcina == veza.vakcina))
                    return BadRequest("Vakcina je vec podrzana u toj drzavi!");

                Context.Spoj.Add(veza);

                await Context.SaveChangesAsync();
                return Ok("Dodata vazeca vakcina");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("PreuzmiDozu/{drzavaId}/{vakcinaId}")]
        [HttpGet]
        public ActionResult PreuzmiDozu(int drzavaId, int vakcinaId)
        {
            //provere
            try
            {
                var spojevi = Context.Spoj.Include(p => p.drzava)
                                              .Include(p => p.vakcina);

                var potrebanSpoj = spojevi.Where(p => p.drzava.ID == drzavaId && p.vakcina.ID == vakcinaId).FirstOrDefault();

                if (potrebanSpoj == null)
                {
                    return BadRequest("Prosledjena drzava ne podrzava prosledjenu vakcinu!");
                }
                return Ok(new
                {
                    doza = potrebanSpoj.doza
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [Route("AzurirajDozu/{drzavaId}/{vakcinaId}/{doza}")]
        [HttpPut]
        public async Task<ActionResult> AzurirajDozu(int drzavaId, int vakcinaId, int doza)
        {
            try
            {
                var spojevi = Context.Spoj.Include(p => p.drzava)
                                          .Include(p => p.vakcina);
                var spoj = spojevi.Where(p => p.drzava.ID == drzavaId && p.vakcina.ID == vakcinaId).FirstOrDefault();
                if (spoj == null)
                    return BadRequest("Drzava ne podrzava datu vakcinu");
                spoj.doza = doza;
                await Context.SaveChangesAsync();
                return Ok("Uspesna izmena");



            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("ObrisiVazecuVakcinu/{drzavaId}/{vakcinaId}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiVazecuVakcinu(int drzavaId, int vakcinaId)
        {
            //provere
            try
            {
                var spojevi = Context.Spoj.Include(p => p.drzava)
                                              .Include(p => p.vakcina);
                var spojZaBrisanje = spojevi.Where(p => p.drzava.ID == drzavaId && p.vakcina.ID == vakcinaId).FirstOrDefault();
                Context.Spoj.Remove(spojZaBrisanje);
                await Context.SaveChangesAsync();
                return Ok("Uspesno obrisana vazeca vakcina!");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }





        [Route("DodajVazeciTest/{idDrzave}/{test}/{starostTesta}")]
        [HttpPost]
        public async Task<ActionResult> DodajVazeciTest(int idDrzave, string test, int starostTesta)
        {
            //provere

            if (!validanTip(test))
                return BadRequest("Nije uneto validno ime testa");
            if (starostTesta < 0)
                return BadRequest("Nevalidna vrednost starosti testa");


            try
            {
                var drzavaZaPromenu = Context.Drzave.Where(p => p.ID == idDrzave).Include(p => p.PodrzaniTestovi)
                                                                                        .FirstOrDefault();

                if (drzavaZaPromenu == null)
                    return BadRequest("Nevalidna drzava");

                TipTesta tipTesta = odrediTip(test);

                var noviTest = Context.Testovi.Where(p => p.Tip == tipTesta && p.Starost == starostTesta).FirstOrDefault();

                if (noviTest == null)
                    return BadRequest("Nevalidan test");

                if (drzavaZaPromenu.PodrzaniTestovi == null)
                    drzavaZaPromenu.PodrzaniTestovi = new List<Test>();




                //proveri da l drzava vec ima taj test
                foreach (var postojeci in drzavaZaPromenu.PodrzaniTestovi)
                {
                    if (postojeci.Tip == noviTest.Tip)
                        return BadRequest("Takav tip testa vec postoji, obrisati ga prvo!");
                }
                drzavaZaPromenu.PodrzaniTestovi.Add(noviTest);
                await Context.SaveChangesAsync();
                return Ok("Uspesno dodat");





            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzbrisiVazeciTest/{idDrzave}/{test}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiVazeciTest(int idDrzave, string test)
        {
            try
            {
                if (!validanTip(test))
                    return BadRequest("Nije uneto validno ime testa");
                var drzavaZaPromenu = Context.Drzave.Where(p => p.ID == idDrzave).Include(p => p.PodrzaniTestovi)
                                                                                        .FirstOrDefault();

                if (drzavaZaPromenu == null)
                    return BadRequest("Nevalidna drzava");

                TipTesta tipTesta = odrediTip(test);
                foreach (var postojeci in drzavaZaPromenu.PodrzaniTestovi)
                {
                    if (postojeci.Tip == tipTesta){
                        drzavaZaPromenu.PodrzaniTestovi.Remove(postojeci);
                        await Context.SaveChangesAsync();
                        return Ok("Uspesno uklonjeno!");
                    }

                }
                return BadRequest("Takav test ne postoji medju podrzanim!");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }




        }
        [Route("PreuzmiPodrzanuStarost/{idDrzave}/{test}")]
        [HttpGet]
        public ActionResult PreuzmiPodrzanuStarost(int idDrzave, string test)
        {
            try{
            if (!validanTip(test))
                    return BadRequest("Nije uneto validno ime testa");
                var drzavaZaPromenu = Context.Drzave.Where(p => p.ID == idDrzave).Include(p => p.PodrzaniTestovi)
                                                                                        .FirstOrDefault();

                if (drzavaZaPromenu == null)
                    return BadRequest("Nevalidna drzava");

                TipTesta tipTesta = odrediTip(test);
                foreach (var postojeci in drzavaZaPromenu.PodrzaniTestovi)
                {
                    if (postojeci.Tip == tipTesta){
                        return Ok(
                            new{
                                starost = postojeci.Starost
                            }
                        );
                    }
                }
                return BadRequest("Prosledjena drzava ne podrzava test!");
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }


        }
        [Route("AzurirajVazecuStarost/{idDrzave}/{test}/{starost}")]
        [HttpPut]
        public async Task<ActionResult> AzurirajVazecuStarost(int idDrzave,string test, int starost){
            try{
                if (!validanTip(test))
                    return BadRequest("Nije uneto validno ime testa");
                if(starost != 24 && starost != 48)
                    return BadRequest("Nevalidna vrednost za starost testa!");

                var drzavaZaPromenu = Context.Drzave.Where(p => p.ID == idDrzave).Include(p => p.PodrzaniTestovi)
                                                                                        .FirstOrDefault();

                
                if (drzavaZaPromenu == null)
                    return BadRequest("Nevalidna drzava");

                TipTesta tipTesta = odrediTip(test);
                //pronalazi test koji treba da ubacimo u listu podrzanih testova
                var noviTest = Context.Testovi.Where(p=>p.Tip == tipTesta && p.Starost == starost).FirstOrDefault();
                foreach (var postojeci in drzavaZaPromenu.PodrzaniTestovi)
                {
                    if (postojeci.Tip == tipTesta){
                        drzavaZaPromenu.PodrzaniTestovi.Remove(postojeci);//izbrisi iz liste vec postojeci test
                        drzavaZaPromenu.PodrzaniTestovi.Add(noviTest); //dodaj novi test
                        await Context.SaveChangesAsync();
                        return Ok("Uspesna izmena!");
                    }
                }
                return BadRequest("Prosledjena drzava ne podrzava test!");

            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }


        [Route("DodajVazeciPasos/{nazivDrzave}/{pasos}")]
        [HttpPut]
        public async Task<ActionResult> DodajVazeciPasos(string nazivDrzave, string pasos)
        {
            if (string.IsNullOrWhiteSpace(nazivDrzave) || nazivDrzave.Length > 40)
                return BadRequest("Nevalidan naziv drzave");
            if (string.IsNullOrWhiteSpace(pasos) || pasos.Length > 50)
                return BadRequest("Nevalidan pasos");
            try
            {
                var trazeniPasos = Context.Pasosi.Where(p => p.Drzavljanstvo == pasos).FirstOrDefault();
                if (trazeniPasos == null)
                    return BadRequest("Pasos ne postoji u bazi podataka");
                var drzava = Context.Drzave.Where(p => p.Naziv == nazivDrzave).FirstOrDefault();

                if (drzava == null)
                    return BadRequest("Trazena drzava nije u bazi podataka");

                if (drzava.PodrzaniPasosi == null)
                    drzava.PodrzaniPasosi = new List<Pasos>();
                drzava.PodrzaniPasosi.Add(trazeniPasos);
                await Context.SaveChangesAsync();
                return Ok("Dodat novi vazeci pasos");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PreuzmiPodrzano/{pasosId}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiPodrzano(int pasosId, [FromQuery] int? vakcinaId, [FromQuery] int? doza, [FromQuery] string tipTesta, [FromQuery] int? starostTesta)
        {
            //provere
            // if(string.IsNullOrWhiteSpace(pasos) || pasos.Length > 50)
            //     return BadRequest("Nevalidan pasos");

            try
            {
                //pasos je obavezan da se unese
                var trazeniPasos = Context.Pasosi.Where(p => p.ID == pasosId).FirstOrDefault();
                if (trazeniPasos == null)
                    return BadRequest("Uneti pasos ne postoji u bazi podataka");

                if (vakcinaId == null && tipTesta == null)
                    return BadRequest("Svaki unos mora da sadrzi ili vakcinu ili tip testa ili oba.");
                TipTesta tip;
                Vakcina vakcina = null;
                Test trazeniTest = null;

                //nadji listu drzava koje podrzavaju prosledjeni test
                //nadji listu drzava koje podrzavaju prosledjenu vakcinu
                //spoji ih
                if (vakcinaId != null)
                {
                    vakcina = Context.Vakcine.Where(p => p.ID == vakcinaId).FirstOrDefault();
                    if (vakcina == null)
                        return BadRequest("Prosledjena vakcina ne postoji");
                }
                if (tipTesta != null)
                {
                    tip = odrediTip(tipTesta);
                    trazeniTest = Context.Testovi.Where(p => p.Tip == tip && p.Starost == starostTesta).FirstOrDefault();
                    if (trazeniTest == null)
                        return BadRequest("Trazeni test ne postoji u bazi podataka!");

                }


                var drzave = Context.Drzave.Where(p => p.PodrzaniPasosi.Contains(trazeniPasos));
                var drzavaTest = drzave;
                var drzavaVakcina = drzave;

                if (trazeniTest != null)
                    drzavaTest = drzave.Where(p => p.PodrzaniTestovi.Contains(trazeniTest));

                var spojevi = Context.Spoj.Include(p => p.drzava)
                                          .Include(p => p.vakcina);


                var potrebniSpojevi = spojevi.Where(p => vakcina == null || p.vakcina.ID == vakcina.ID);


                if (vakcina != null && trazeniTest != null)
                {
                    drzave = drzavaTest;
                    var saDozom = potrebniSpojevi.Where(p => p.doza == doza);
                    drzavaVakcina = saDozom.Select(p => p.drzava);
                    drzave = drzave.Union(drzavaVakcina);
                }

                else if (vakcina == null && trazeniTest != null)
                    drzave = drzavaTest;

                else
                {
                    var saDozom = potrebniSpojevi.Where(p => p.doza == doza);
                    drzave = saDozom.Select(p => p.drzava);

                }



                return Ok(await drzave.Select(p =>
                    new
                    {
                        Drzava = p.Naziv,
                        SVGId = p.SVGId,
                        ID = p.ID
                    }).ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




        /////////////PRIVATNE FUNKCIJE
        private bool validanTip(string tip)
        {
            string[] testovi = { "pcr", "antigenski", "antitela" };
            return testovi.Contains(tip, StringComparer.InvariantCultureIgnoreCase);

        }
        private TipTesta odrediTip(string tipTesta)
        {
            TipTesta tip;
            if (string.Equals(tipTesta, "pcr", StringComparison.InvariantCultureIgnoreCase))
                tip = TipTesta.PCR;

            else if (string.Equals(tipTesta, "antigenski", StringComparison.InvariantCultureIgnoreCase))
                tip = TipTesta.ANTIGENSKI;

            else
                tip = TipTesta.ANTITELA;
            return tip;

        }


    }
}
