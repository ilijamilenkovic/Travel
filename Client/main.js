import { Drzava } from "./Drzava.js";
import { Pasos } from "./Pasos.js";
import { Test } from "./Test.js";
import { Travel } from "./Travel.js";
import { Vakcina } from "./Vakcina.js";


var listaVakcina = [];
var listaTestova = [];
var listaPasosa = [];
var listaDrzava = [];
fetch("https://localhost:5001/Vakcina/PrikaziVakcine")
    .then(p => {
        p.json().then(vakcine => {
            vakcine.forEach(vakcina => {
                //var vakc = new Vakcina(vakcina.proizvodjac,vakcina.id);
                var novaVakcina = new Vakcina(vakcina.naziv, vakcina.id);
                listaVakcina.push(novaVakcina);
                console.log(novaVakcina);
            });
            fetch("https://localhost:5001/Test/PreuzmiTestove")
                .then(p => {
                    p.json().then(testovi => {
                        testovi.forEach(test => {
                            var noviTest = new Test(test.tip, test.starost, test.id);
                            listaTestova.push(noviTest);
                        });
                        fetch("https://localhost:5001/Pasos/PreuzmiPasose")
                            .then(p => {
                                p.json().then(pasosi => {
                                    pasosi.forEach(pasos => {
                                        var noviPasos = new Pasos(pasos.drzavljanstvo, pasos.id);
                                        listaPasosa.push(noviPasos);
                                        //console.log(noviPasos);
                                    });
                                    fetch("https://localhost:5001/Drzava/Preuzmi")
                            .then(p => {
                                p.json().then(drzave => {
                                    drzave.forEach(drzava => {
                                        var novaDrzava = new Drzava(drzava.naziv, drzava.id);
                                        listaDrzava.push(novaDrzava);
                                        //console.log(noviPasos);
                                        console.log(novaDrzava);
                                    });
                                    var app = new Travel(listaPasosa,listaVakcina, listaTestova,listaDrzava);
                                    app.crtaj(document.body);
                                })
                            })
                                    //var app = new Travel(listaPasosa,listaVakcina, listaTestova);
                                   // app.crtaj(document.body);
                                })
                            })

                    })
                })



        })
    })