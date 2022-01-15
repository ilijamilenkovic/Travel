export class Test{
    constructor(tip,starost,id)
    {
        this.tip = tip;
        this.starost = starost;
        this.id = id;
    }

    oderdiTest(test_enum){
        switch(test_enum){
            case 0: return "PCR";
            case 1: return "Antigenski";
            case 2: return "Antitela";
        }
    }
}