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
    public class TestController : ControllerBase
    {
    
        public TravelContext Context{get;set;}

        

        public TestController(TravelContext context)
        {
            Context = context;
        }

        [Route("PreuzmiTestove")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiTestove()
        {
            return Ok(Context.Testovi);
        }

        [Route("DodajTestFromBody")]
        [HttpPost]
        public async Task<ActionResult> DodajTestFromBody([FromBody]Test test)
        {
            if(test.Starost < 0)
                return BadRequest("Nevalidna vrednost za starost testa!");
            try{
                Context.Testovi.Add(test);
                await Context.SaveChangesAsync();
                return Ok("Uspesno dodat test!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
       
        [Route("DodajTest/{naziv}/{starost}")]
        [HttpPost]
        public async Task<ActionResult> DodajTest(string naziv,int starost)
        {
            if(starost < 0){
                return BadRequest("Nevalidna vrednost za starost testa!");
            }
            string[] validniTestovi = { 
                        "PCR", 
                        "ANTIGENSKI", 
                        "ANTITELA",
                         "pcr", 
                        "antigenski", 
                        "antitela"};


            if(!validniTestovi.Contains(naziv)){
              return BadRequest($"Nevalidan naziv testa! PROSLEDJENI ARGUMENT {naziv}");
            }
            try{
                Test test = new Test();
                test.Starost = starost;
                if(naziv == "pcr" || naziv == "PCR")
                    test.Tip = TipTesta.PCR;
                else if(naziv == "ANTIGENSKI" || naziv == "antigenski")
                    test.Tip = TipTesta.ANTIGENSKI;
                    else if(naziv == "ANTITELA" || naziv == "antitela")
                    test.Tip = TipTesta.ANTITELA;


                Context.Testovi.Add(test);
                await Context.SaveChangesAsync();
                return Ok("Uspesno dodat test!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
    