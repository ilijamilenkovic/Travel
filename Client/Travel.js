

export class Travel {
    constructor(listaPasosa, listaVakcina, listaTestova, listaDrzava) {
        this.listaPasosa = listaPasosa;
        this.listaVakcina = listaVakcina;
        this.listaTestova = listaTestova;
        this.listaDrzava = listaDrzava;
        this.container = null;
    }


    crtaj(host) {
        this.container = document.createElement("div");
        this.container.className = "glavniKontejner";
        host.appendChild(this.container);
        var testL = document.createElement("label");

        let formaKontejner = document.createElement("div");
        formaKontejner.className = "forma";
        this.container.appendChild(formaKontejner);

        let mapaKontejner = document.createElement("div");
        mapaKontejner.className = "mapaK";
        this.container.appendChild(mapaKontejner);

        this.crtajFormu(formaKontejner);
        this.crtajMapu(mapaKontejner);
    }

    crtajFormu(host) {
        let formaPrikazi = document.createElement("div");
        host.appendChild(formaPrikazi);
        formaPrikazi.className = "formaPrikazi";
        this.crtajLabelu(formaPrikazi,"Pasos: ");

        let opcijeKont = document.createElement("div");
        opcijeKont.className = "pasosOpcije";
        formaPrikazi.appendChild(opcijeKont);

        let opcije = document.createElement("select");
        opcijeKont.appendChild(opcije);
        let nultaOp = document.createElement("option");
        nultaOp.innerHTML = "----";
        nultaOp.value = "none";
        opcije.appendChild(nultaOp);
        this.listaPasosa.forEach(pasos => {
            let op = document.createElement("option");
            op.innerHTML = pasos.drzavljanstvo;
            op.value = pasos.id;
            opcije.appendChild(op);
        });


        this.crtajLabelu(formaPrikazi,"Vakcina: ");

        opcijeKont = document.createElement("div");
        opcijeKont.className = "vakcinaOpcije";
        formaPrikazi.appendChild(opcijeKont);

        opcije = document.createElement("select");
        opcijeKont.appendChild(opcije);
        nultaOp = document.createElement("option");
        nultaOp.innerHTML = "----";
        nultaOp.value = "none";
        opcije.appendChild(nultaOp);
        this.listaVakcina.forEach(vakcina => {
            let op = document.createElement("option");
            op.innerHTML = vakcina.proizvodjac;
            op.value = vakcina.ID;
            opcije.appendChild(op);
        });

        this.crtajLabelu(formaPrikazi,"Doza: ");

        let dozaKont = document.createElement("div");
        formaPrikazi.appendChild(dozaKont);
        let dozaInput = document.createElement("input");
        dozaInput.type = "number";
        dozaInput.value = 0;
        dozaInput.className = "dozaInput";
        dozaKont.appendChild(dozaInput);


        this.crtajLabelu(formaPrikazi,"Test: ");

        opcijeKont = document.createElement("div");
        opcijeKont.className = "testOpcije";
        formaPrikazi.appendChild(opcijeKont);
        opcije = document.createElement("select");
        opcijeKont.appendChild(opcije);
        let starostiTestaKont = document.createElement("div");
        starostiTestaKont.className = "starostiKont";
        formaPrikazi.appendChild(starostiTestaKont);
        let dodatiTestovi = [];
        let dodateStarostiTesta = [];
        let appended = false;
        nultaOp = document.createElement("option");
        nultaOp.innerHTML = "----";
        nultaOp.value = "none";
        opcije.appendChild(nultaOp);
        this.listaTestova.forEach(test => {

            let op = document.createElement("option");
            let tipTesta = test.oderdiTest(test.tip);
            if (!dodatiTestovi.includes(tipTesta)) {
                dodatiTestovi.push(tipTesta);
                console.log(dodatiTestovi);
                op.innerHTML = tipTesta;
                op.value = tipTesta;
                opcije.appendChild(op);
            }

            if (!appended) {
                this.crtajLabelu(starostiTestaKont,"Starost testa (h): ");
                appended = true;

            }

            if (!dodateStarostiTesta.includes(test.starost)) {
                dodateStarostiTesta.push(test.starost);
                let l = document.createElement("label");
                l.innerHTML = test.starost;
                starostiTestaKont.appendChild(l);
                let rbt = document.createElement("input");
                rbt.type = "radio";
                rbt.value = test.starost;
                rbt.name = "starost";
                starostiTestaKont.appendChild(rbt);

            }
        });

        let dugmeKont = document.createElement("div");
        dugmeKont.className = "dugmeKont";
        formaPrikazi.appendChild(dugmeKont);
        let dugmePrikazi = document.createElement("button");
        dugmePrikazi.innerHTML = "Prikazi";
        dugmeKont.appendChild(dugmePrikazi);
        dugmePrikazi.onclick = (ev) => this.preuzmiVrednostiGet();


        let drugaFormaKont = document.createElement("div");
        drugaFormaKont.className = "formaPostDelete";
        host.appendChild(drugaFormaKont);
        
        this.crtajFormuPostDeleteVakcina(drugaFormaKont);
        

        


    }

    crtajFormuPostDeleteVakcina(host){//sredi divove
        this.crtajLabelu(host,"Dodaj/Obrisi podrzanu vakcinu: ");

        this.crtajLabelu(host, "Drzava: ");

        let opcijeKont = document.createElement("div");
        host.appendChild(opcijeKont);

        let opcije = document.createElement("select");
        opcijeKont.appendChild(opcije);
        opcije.className ="opcijeDrzava";
        let nultaOp = document.createElement("option");
        nultaOp.innerHTML = "----";
        nultaOp.value = "none";
        opcije.appendChild(nultaOp);
        this.listaDrzava.forEach(drzava => {
            let op = document.createElement("option");
            op.innerHTML = drzava.naziv;
            op.value = drzava.id;
            opcije.appendChild(op);
        });

        let zaZamenuKont = document.createElement("div");
        zaZamenuKont.className = "zaZamenuKont";
        host.appendChild(zaZamenuKont);

        let zaZamenuKontVakcina = document.createElement("div");
        zaZamenuKontVakcina.className = "zaZamenuKontVakcina";
        zaZamenuKont.appendChild(zaZamenuKontVakcina);


        
        this.crtajLabelu(zaZamenuKontVakcina,"Vakcina: ");


        opcijeKont = document.createElement("div");
        //opcijeKont.className = "vakcinaOpcijeUpdate";
        zaZamenuKontVakcina.appendChild(opcijeKont);
        
        opcije = document.createElement("select");
        opcije.className = "vakcinaOpcijeUpdate";
        opcijeKont.appendChild(opcije);
        nultaOp = document.createElement("option");
        nultaOp.innerHTML = "----";
        nultaOp.value = "none";
        opcije.appendChild(nultaOp);
        this.listaVakcina.forEach(vakcina => {
            let op = document.createElement("option");
            op.innerHTML = vakcina.proizvodjac;
            op.value = vakcina.ID;
            opcije.appendChild(op);
        });

        
        this.crtajLabelu(zaZamenuKontVakcina,"Doza: ");

        let dozaKont = document.createElement("div");
        zaZamenuKontVakcina.appendChild(dozaKont);
        let dozaInput = document.createElement("input");
        dozaInput.type = "number";
        dozaInput.value = 0;
        dozaInput.className = "dozaInputUpdate";
        dozaKont.appendChild(dozaInput);

        let zaZamenuKontTest = document.createElement("div");
        zaZamenuKontTest.className = "zaZamenuKontTest";
        zaZamenuKont.appendChild(zaZamenuKontTest);

        let kont = document.createElement("div"); //za dugmice
        kont.className = "dugmadRed";
        zaZamenuKontVakcina.appendChild(kont);

        let dugme = document.createElement("button");
        dugme.innerHTML = "Obrisi";
        dugme.onclick = (ev) => this.preuzmiVrednostiVakcine("obrisi");
        kont.appendChild(dugme);

        dugme = document.createElement("button");
        dugme.innerHTML = "Dodaj";
        dugme.onclick = (ev) => this.preuzmiVrednostiVakcine("dodajVakcinu");
        kont.appendChild(dugme);

        kont = document.createElement("div"); //za dugmice
        kont.className = "dugmadRed";
        zaZamenuKontVakcina.appendChild(kont);

        dugme = document.createElement("button");
        dugme.innerHTML = "Vrati dozu";
        dugme.onclick = (ev) => this.preuzmiVrednostiVakcine("vratiDozu");
        kont.appendChild(dugme);

        dugme = document.createElement("button");
        dugme.innerHTML = "Azuriraj dozu";
        dugme.onclick = (ev) => this.preuzmiVrednostiVakcine("azurirajDozu");
        kont.appendChild(dugme);







        this.crtajFormuPostDeleteTest(zaZamenuKontTest);


    }

    crtajFormuPostDeleteTest(host){

        this.crtajLabelu(host,"Test: ");

        let opcijeKont = document.createElement("div");
        //opcijeKont.className = "testOpcijeUpdate";
        host.appendChild(opcijeKont);
        let opcije = document.createElement("select");
        opcije.className = "testOpcijeUpdate";
        opcijeKont.appendChild(opcije);
        let starostiTestaKont = document.createElement("div");
        starostiTestaKont.className = "starostiKontUpdate";
        host.appendChild(starostiTestaKont);
        let dodatiTestovi = [];
        let dodateStarostiTesta = [];
        let appended = false;
        let nultaOp = document.createElement("option");
        nultaOp.innerHTML = "----";
        nultaOp.value = "none";
        opcije.appendChild(nultaOp);
        this.listaTestova.forEach(test => {

            let op = document.createElement("option");
            let tipTesta = test.oderdiTest(test.tip);
            if (!dodatiTestovi.includes(tipTesta)) {
                dodatiTestovi.push(tipTesta);
                console.log(dodatiTestovi);
                op.innerHTML = tipTesta;
                op.value = tipTesta;
                opcije.appendChild(op);
            }

            if (!appended) {
                this.crtajLabelu(starostiTestaKont,"Starost testa (h): ");
                appended = true;

            }

            if (!dodateStarostiTesta.includes(test.starost)) {
                dodateStarostiTesta.push(test.starost);
                let l = document.createElement("label");
                l.innerHTML = test.starost;
                starostiTestaKont.appendChild(l);
                let rbt = document.createElement("input");
                rbt.type = "radio";
                rbt.value = test.starost;
                rbt.name = "starost";
                starostiTestaKont.appendChild(rbt);

            }
        });

        let kont = document.createElement("div"); //za dugmice
        kont.className = "dugmadRed";
        host.appendChild(kont);

        let dugme = document.createElement("button");
        dugme.innerHTML = "Obrisi";
        dugme.onclick = (ev) => this.preuzmiVrednostiTesta("obrisi");
        kont.appendChild(dugme);

        dugme = document.createElement("button");
        dugme.innerHTML = "Dodaj";
        dugme.onclick = (ev) => this.preuzmiVrednostiTesta("dodaj");
        kont.appendChild(dugme);

        kont = document.createElement("div"); //za dugmice
        kont.className = "dugmadRed";
        host.appendChild(kont);

        dugme = document.createElement("button");
        dugme.innerHTML = "Vrati starost";
        dugme.onclick = (ev) => this.preuzmiVrednostiTesta("vratiStarost");
        kont.appendChild(dugme);

        dugme = document.createElement("button");
        dugme.innerHTML = "Azuriraj starost";
        dugme.onclick = (ev) => this.preuzmiVrednostiTesta("azurirajStarost");
        kont.appendChild(dugme);
    }

    crtajLabelu(host,innerText){
        let l = document.createElement("label");
        l.innerHTML = innerText;
        let labKont = document.createElement("div");
        labKont.appendChild(l);
        host.appendChild(labKont);
        
    }

    crtajMapu(host) {
       
        //<object id="mapa" type="image/svg+xml" data="Europe.svg">
        let slika = document.createElement("object");
        slika.id = "mapa";
        slika.type = "image/svg+xml";
        slika.data = "Europe.svg";
        slika.className = "svgObj";

        host.appendChild(slika);
    }


    preuzmiVrednostiGet() {
        let pasosOpcije = this.container.querySelector(".pasosOpcije");
        let vakcinaOpcije = this.container.querySelector(".vakcinaOpcije");
        let testOpcije = this.container.querySelector(".testOpcije");
        let starostiKont = this.container.querySelector(".starostiKont");
        let doza = this.container.querySelector(".dozaInput");

        let pasosId = pasosOpcije.querySelector("option:checked").value;

        let vakcinaId = vakcinaOpcije.querySelector("option:checked").value;

        let test = testOpcije.querySelector("option:checked").value;

        let starostTesta = starostiKont.querySelector("input:checked");

        if (pasosId == "none") {
            alert("Mora se uneti pasos");
            return;
        }

        if (vakcinaId != "none" && (doza.value < 0 || doza.value > 5)) {
            alert("Nevalidna doza!");
            return;
        }
        if (test != "none" && starostTesta == null) {
            alert("Mora se uneti starost testa!");
            return;
        }

        if ((vakcinaId != "none" && doza.value > 0 && doza.value < 5) || (test != "none" && starostTesta != null)) {
            console.log(pasosId);
            console.log(vakcinaId);
            console.log(test);
            if (starostTesta != null)
                console.log(starostTesta.value);
            if (doza != null)
                console.log(doza.value);
            if (vakcinaId == "none")
                vakcinaId = null;
            if (test == "none")
                test = null;

            this.nadjiDrzave(pasosId, vakcinaId, doza, test, starostTesta);


        }
        else {
            alert("Neispravan unos!");

        }
    }
    nadjiDrzave(pasosId, vakcinaId, doza, tipTesta, starostTesta) {
        let dodajNaStr = pasosId + "?";
        if (vakcinaId != null) { //onda je i doza validna
            dodajNaStr = dodajNaStr + "vakcinaId=" + vakcinaId + "&doza=" + doza.value;
            if (tipTesta != null)
                dodajNaStr += "&";
        }
        if (tipTesta != null) {
            dodajNaStr = dodajNaStr + "tipTesta=" + tipTesta + "&starostTesta=" + starostTesta.value;
        }
        console.log(dodajNaStr);
        this.obrisiMapu();
        var mapa = document.querySelector(".svgObj");
        mapa.addEventListener('load', () => {
            console.log('SVG loaded.');
            var drzave = mapa.contentDocument;
            fetch("https://localhost:5001/Drzava/PreuzmiPodrzano/" + dodajNaStr,
            {
                method: "GET"
            }).then(d => {
                if (d.ok) {
                    d.json().then(data => {
                        data.forEach(d => {
                            
                            
                            
                            drzave.getElementById(d.svgId).style.fill = "#4cfb00";
                            
                            //var dozvoljena = drzave.getElementById(d.svgId).className ="dozvoljeneDrzave";
                            //Uncaught (in promise) TypeError: setting getter-only property "className"
                            
                            
                        })
                    })
                }
            })
            
          })

        

    }

    obrisiMapu(){
        var map = this.container.querySelector(".svgObj");
        var roditelj = map.parentNode;
        roditelj.removeChild(map);
        this.crtajMapu(roditelj);
        
    }

    preuzmiVrednostiVakcine(akcija){
        let opcije = this.container.querySelector(".opcijeDrzava");

        let drzava = opcije.querySelector("option:checked");
        console.log(drzava.value);

        opcije = this.container.querySelector(".vakcinaOpcijeUpdate");
        let vakcina = opcije.querySelector("option:checked");
        console.log(vakcina.value);



        let doza = this.container.querySelector(".dozaInputUpdate").value;
        //let doza = opcije.querySelector("option:checked");
        console.log(doza);
        

        //this.upisiVakcinu(drzava.value,vakcina.value,doza);

        if(akcija == "dodajVakcinu" && drzava.value!="none" && vakcina.value != "none" && doza > 0 && doza < 5)
        {
            this.upisiVakcinu(drzava.value,vakcina.value,doza);
        }

        if(akcija == "obrisi" && drzava.value!="none" && vakcina.value != "none")
        {
            this.obrisiVakcinu(drzava.value,vakcina.value)
        }
        if(akcija=="vratiDozu" && drzava.value!="none" && vakcina.value != "none"){
            this.vratiDozu(drzava.value,vakcina.value);
        }
        if(akcija=="azurirajDozu" && drzava.value!="none" && vakcina.value != "none" && doza > 0 && doza < 5)
            this.azurirajDozu(drzava.value,vakcina.value,doza);



    }

    upisiVakcinu(drzavaId, vakcinaId, doza){
        fetch("https://localhost:5001/Drzava/DodajVazecuVakcinu/"+drzavaId+"/"+vakcinaId+"/"+doza,
        {
            method:"POST"
        }).then(s=>{
            if(s.ok){
                alert("Uspesno dodata vakcina");
            }
            else
                alert("Neuspesno dodavanje nove vakcine");
            

        })
    }

    obrisiVakcinu(drzavaId,vakcinaId){
        fetch("https://localhost:5001/Drzava/ObrisiVazecuVakcinu/" + drzavaId + "/" + vakcinaId,
        {
            method: "DELETE"
        }).then(s=>{
            if(s.ok){
                alert("Uspesno obrisana vazeca vakcina!");
            }
            else{
                alert("Neuspesno brisanje!");
            }
        })
    }

    vratiDozu(drzavaId,vakcinaId){
        fetch("https://localhost:5001/Drzava/PreuzmiDozu/"+drzavaId +"/"+vakcinaId,
        {
            method: "GET"
        }).then(s=>{
            if(s.ok){
                s.json().then(data=>{
                    let doza = this.container.querySelector(".dozaInputUpdate");
                    doza.value = data.doza;
                })
            }
        })
    }
    azurirajDozu(drzavaId,vakcinaId,doza){
        fetch("https://localhost:5001/Drzava/AzurirajDozu/" + drzavaId + "/"+vakcinaId+"/"+doza,
        {
            method: "PUT"
        }).then(s=>{
            if(s.ok){
                alert("Uspesna izmena!");
            }
            else
                alert("Neuspesna izmena!(Drzava ne podrzava vakcinu)");
        })
    }



    preuzmiVrednostiTesta(akcija){
        let opcije = this.container.querySelector(".opcijeDrzava");

        let drzava = opcije.querySelector("option:checked");
        console.log(drzava.value);

        opcije = this.container.querySelector(".testOpcijeUpdate");
        let test = opcije.querySelector("option:checked");
        console.log(test.value);

        opcije = this.container.querySelector(".starostiKontUpdate");
        let starost = opcije.querySelector("input:checked");

        console.log(starost.value);



    }
    
}