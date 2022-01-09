import { Vakcina } from "./Vakcina.js";


var listaVakcina =[];
fetch("https://localhost:5001/Vakcina/PrikaziVakcine")
.then(p=>{
    p.json().then(vakcine=>{
        vakcine.forEach(vakcina => {
            //var vakc = new Vakcina(vakcina.proizvodjac,vakcina.id);
            console.log(vakcina);
        });

    })
})