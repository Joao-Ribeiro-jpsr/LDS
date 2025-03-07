export class Recintos {

  constructor(
    public recintoID?: number, // id do recinto 
    public name?: string, // Nome do recinto 
    public concelho?: string, // Concelho do recinto 
    public latitude?: string, // latitude do recinto 
    public longitude?: string, // longitude do recinto 
    public modalidade?: string, // modalidade do recinto 
    public preco?: number, // preco do recinto 
    public imagem?: string, // imagem do recinto 
    public description?: string) { } // descrição do recinto 
}
